using Mine.NET.entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mine.NET.plugin.messaging {

    /**
     * Standard implementation to {@link Messenger}
     */
    public class StandardMessenger : Messenger {
        private readonly Dictionary<String, HashSet<PluginMessageListenerRegistration>> incomingByChannel = new Dictionary<String, HashSet<PluginMessageListenerRegistration>>();
        private readonly Dictionary<Plugin, HashSet<PluginMessageListenerRegistration>> incomingByPlugin = new Dictionary<Plugin, HashSet<PluginMessageListenerRegistration>>();
        private readonly Dictionary<String, HashSet<Plugin>> outgoingByChannel = new Dictionary<String, HashSet<Plugin>>();
        private readonly Dictionary<Plugin, HashSet<String>> outgoingByPlugin = new Dictionary<Plugin, HashSet<String>>();
        private readonly Object incomingLock = new Object();
        private readonly Object outgoingLock = new Object();

        private void addToOutgoing(Plugin plugin, String channel) {
            lock (outgoingLock) {
                HashSet<Plugin> plugins = outgoingByChannel[channel];
                HashSet<String> channels = outgoingByPlugin[plugin];

                if (plugins == null) {
                    plugins = new HashSet<Plugin>();
                    outgoingByChannel.Add(channel, plugins);
                }

                if (channels == null) {
                    channels = new HashSet<String>();
                    outgoingByPlugin.Add(plugin, channels);
                }

                plugins.Add(plugin);
                channels.Add(channel);
            }
        }

        private void removeFromOutgoing(Plugin plugin, String channel) {
            lock (outgoingLock) {
                HashSet<Plugin> plugins = outgoingByChannel[channel];
                HashSet<String> channels = outgoingByPlugin[plugin];

                if (plugins != null) {
                    plugins.Remove(plugin);

                    if (plugins.Count == 0) {
                        outgoingByChannel.Remove(channel);
                    }
                }

                if (channels != null) {
                    channels.Remove(channel);

                    if (channels.Count == 0) {
                        outgoingByChannel.Remove(channel);
                    }
                }
            }
        }

        private void removeFromOutgoing(Plugin plugin) {
            lock (outgoingLock) {
                HashSet<String> channels = outgoingByPlugin[plugin];

                if (channels != null) {
                    String[] toRemove = channels.ToArray();

                    outgoingByPlugin.Remove(plugin);

                    foreach (String channel in toRemove) {
                        removeFromOutgoing(plugin, channel);
                    }
                }
            }
        }

        private void addToIncoming(PluginMessageListenerRegistration registration) {
            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByChannel[registration.getChannel()];

                if (registrations == null) {
                    registrations = new HashSet<PluginMessageListenerRegistration>();
                    incomingByChannel.Add(registration.getChannel(), registrations);
                } else {
                    if (registrations.Contains(registration)) {
                        throw new ArgumentException("This registration already exists");
                    }
                }

                registrations.Add(registration);

                registrations = incomingByPlugin[registration.getPlugin()];

                if (registrations == null) {
                    registrations = new HashSet<PluginMessageListenerRegistration>();
                    incomingByPlugin.Add(registration.getPlugin(), registrations);
                } else {
                    if (registrations.Contains(registration)) {
                        throw new ArgumentException("This registration already exists");
                    }
                }

                registrations.Add(registration);
            }
        }

        private void removeFromIncoming(PluginMessageListenerRegistration registration) {
            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByChannel[registration.getChannel()];

                if (registrations != null) {
                    registrations.Remove(registration);

                    if (registrations.Count == 0) {
                        incomingByChannel.Remove(registration.getChannel());
                    }
                }

                registrations = incomingByPlugin[registration.getPlugin()];

                if (registrations != null) {
                    registrations.Remove(registration);

                    if (registrations.Count == 0) {
                        incomingByPlugin.Remove(registration.getPlugin());
                    }
                }
            }
        }

        private void removeFromIncoming(Plugin plugin, String channel) {
            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

                if (registrations != null) {
                    PluginMessageListenerRegistration[] toRemove = registrations.ToArray();

                    foreach (PluginMessageListenerRegistration registration in toRemove) {
                        if (registration.getChannel().Equals(channel)) {
                            removeFromIncoming(registration);
                        }
                    }
                }
            }
        }

        private void removeFromIncoming(Plugin plugin) {
            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

                if (registrations != null) {
                    PluginMessageListenerRegistration[] toRemove = registrations.ToArray();

                    incomingByPlugin.Remove(plugin);

                    foreach (PluginMessageListenerRegistration registration in toRemove) {
                        removeFromIncoming(registration);
                    }
                }
            }
        }

        public bool isReservedChannel(String channel) {
            validateChannel(channel);

            return channel.Equals("REGISTER") || channel.Equals("UNREGISTER");
        }

        public void registerOutgoingPluginChannel(Plugin plugin, String channel) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }
            validateChannel(channel);
            if (isReservedChannel(channel)) {
                throw new ReservedChannelException(channel);
            }

            addToOutgoing(plugin, channel);
        }

        public void unregisterOutgoingPluginChannel(Plugin plugin, String channel) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }
            validateChannel(channel);

            removeFromOutgoing(plugin, channel);
        }

        public void unregisterOutgoingPluginChannel(Plugin plugin) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }

            removeFromOutgoing(plugin);
        }

        public PluginMessageListenerRegistration registerIncomingPluginChannel(Plugin plugin, String channel, PluginMessageListener listener) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }
            validateChannel(channel);
            if (isReservedChannel(channel)) {
                throw new ReservedChannelException(channel);
            }
            if (listener == null) {
                throw new ArgumentException("Listener cannot be null");
            }

            PluginMessageListenerRegistration result = new PluginMessageListenerRegistration(this, plugin, channel, listener);

            addToIncoming(result);

            return result;
        }

        public void unregisterIncomingPluginChannel(Plugin plugin, String channel, PluginMessageListener listener) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }
            if (listener == null) {
                throw new ArgumentException("Listener cannot be null");
            }
            validateChannel(channel);

            removeFromIncoming(new PluginMessageListenerRegistration(this, plugin, channel, listener));
        }

        public void unregisterIncomingPluginChannel(Plugin plugin, String channel) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }
            validateChannel(channel);

            removeFromIncoming(plugin, channel);
        }

        public void unregisterIncomingPluginChannel(Plugin plugin) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }

            removeFromIncoming(plugin);
        }

        public HashSet<String> getOutgoingChannels() {
            lock (outgoingLock) {
                var keys = outgoingByChannel.Keys;
                return new HashSet<string>(keys);
            }
        }

        public HashSet<String> getOutgoingChannels(Plugin plugin) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }

            lock (outgoingLock) {
                HashSet<String> channels = outgoingByPlugin[plugin];

                if (channels != null) {
                    return new HashSet<string>(channels);
                } else {
                    return new HashSet<string>();
                }
            }
        }

        public HashSet<String> getIncomingChannels() {
            lock (incomingLock) {
                var keys = incomingByChannel.Keys;
                return new HashSet<string>(keys);
            }
        }

        public HashSet<String> getIncomingChannels(Plugin plugin) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }

            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

                if (registrations != null) {
                    HashSet<String> builder = new HashSet<string>();

                    foreach (PluginMessageListenerRegistration registration in registrations) {
                        builder.Add(registration.getChannel());
                    }

                    return builder;
                } else {
                    return new HashSet<string>();
                }
            }
        }

        public HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(Plugin plugin) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }

            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

                if (registrations != null) {
                    return new HashSet<PluginMessageListenerRegistration>(registrations);
                } else {
                    return new HashSet<PluginMessageListenerRegistration>();
                }
            }
        }

        public HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(String channel) {
            validateChannel(channel);

            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByChannel[channel];

                if (registrations != null) {
                    return new HashSet<PluginMessageListenerRegistration>(registrations);
                } else {
                    return new HashSet<PluginMessageListenerRegistration>();
                }
            }
        }

        public HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(Plugin plugin, String channel) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }
            validateChannel(channel);

            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

                if (registrations != null) {
                    HashSet<PluginMessageListenerRegistration> builder = new HashSet<PluginMessageListenerRegistration>();

                    foreach (PluginMessageListenerRegistration registration in registrations) {
                        if (registration.getChannel().Equals(channel)) {
                            builder.Add(registration);
                        }
                    }

                    return builder;
                } else {
                    return new HashSet<PluginMessageListenerRegistration>();
                }
            }
        }

        public bool isRegistrationValid(PluginMessageListenerRegistration registration) {
            if (registration == null) {
                throw new ArgumentException("Registration cannot be null");
            }

            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[registration.getPlugin()];

                if (registrations != null) {
                    return registrations.Contains(registration);
                }

                return false;
            }
        }

        public bool isIncomingChannelRegistered(Plugin plugin, String channel) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }
            validateChannel(channel);

            lock (incomingLock) {
                HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

                if (registrations != null) {
                    foreach (PluginMessageListenerRegistration registration in registrations) {
                        if (registration.getChannel().Equals(channel)) {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public bool isOutgoingChannelRegistered(Plugin plugin, String channel) {
            if (plugin == null) {
                throw new ArgumentException("Plugin cannot be null");
            }
            validateChannel(channel);

            lock (outgoingLock) {
                HashSet<String> channels = outgoingByPlugin[plugin];

                if (channels != null) {
                    return channels.Contains(channel);
                }

                return false;
            }
        }

        public void dispatchIncomingMessage(Player source, String channel, byte[] message) {
            if (source == null) {
                throw new ArgumentException("Player source cannot be null");
            }
            if (message == null) {
                throw new ArgumentException("Message cannot be null");
            }
            validateChannel(channel);

            HashSet<PluginMessageListenerRegistration> registrations = getIncomingChannelRegistrations(channel);

            foreach (PluginMessageListenerRegistration registration in registrations) {
                registration.getListener().onPluginMessageReceived(channel, source, message);
            }
        }

        /**
         * Validates a Plugin Channel name.
         *
         * @param channel Channel name to validate.
         */
        public static void validateChannel(String channel) {
            if (channel == null) {
                throw new ArgumentException("Channel cannot be null");
            }
            if (channel.Length > MessengerConst.MAX_CHANNEL_SIZE) {
                throw new ChannelNameTooLongException(channel);
            }
        }

        /**
         * Validates the input of a Plugin Message, ensuring the arguments are all
         * valid.
         *
         * @param messenger Messenger to use for validation.
         * @param source Source plugin of the Message.
         * @param channel Plugin Channel to send the message by.
         * @param message Raw message payload to send.
         * @throws ArgumentException Thrown if the source plugin is
         *     disabled.
         * @throws ArgumentException Thrown if source, channel or message
         *     is null.
         * @throws MessageTooLargeException Thrown if the message is too big.
         * @throws ChannelNameTooLongException Thrown if the channel name is too
         *     long.
         * @throws ChannelNotRegisteredException Thrown if the channel is not
         *     registered for this plugin.
         */
        public static void validatePluginMessage(Messenger messenger, Plugin source, String channel, byte[] message) {
            if (messenger == null) {
                throw new ArgumentException("Messenger cannot be null");
            }
            if (source == null) {
                throw new ArgumentException("Plugin source cannot be null");
            }
            if (!source.Enabled) {
                throw new ArgumentException("Plugin must be enabled to send messages");
            }
            if (message == null) {
                throw new ArgumentException("Message cannot be null");
            }
            if (!messenger.isOutgoingChannelRegistered(source, channel)) {
                throw new ChannelNotRegisteredException(channel);
            }
            if (message.Length > MessengerConst.MAX_MESSAGE_SIZE) {
                throw new MessageTooLargeException(message);
            }
            validateChannel(channel);
        }
    }
}
