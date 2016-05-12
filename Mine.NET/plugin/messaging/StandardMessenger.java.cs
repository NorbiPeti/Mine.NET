package org.bukkit.plugin.messaging;

import com.google.common.collect.ImmutableSet;
import com.google.common.collect.ImmutableSet.Builder;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import org.bukkit.entity.Player;
import org.bukkit.plugin.Plugin;

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
        synchronized (outgoingLock) {
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

            plugins.add(plugin);
            channels.add(channel);
        }
    }

    private void removeFromOutgoing(Plugin plugin, String channel) {
        synchronized (outgoingLock) {
            HashSet<Plugin> plugins = outgoingByChannel[channel];
            HashSet<String> channels = outgoingByPlugin[plugin];

            if (plugins != null) {
                plugins.remove(plugin);

                if (plugins.isEmpty()) {
                    outgoingByChannel.remove(channel);
                }
            }

            if (channels != null) {
                channels.remove(channel);

                if (channels.isEmpty()) {
                    outgoingByChannel.remove(channel);
                }
            }
        }
    }

    private void removeFromOutgoing(Plugin plugin) {
        synchronized (outgoingLock) {
            HashSet<String> channels = outgoingByPlugin[plugin];

            if (channels != null) {
                String[] toRemove = channels.toArray(new String[0]);

                outgoingByPlugin.remove(plugin);

                for (String channel : toRemove) {
                    removeFromOutgoing(plugin, channel);
                }
            }
        }
    }

    private void addToIncoming(PluginMessageListenerRegistration registration) {
        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByChannel[registration.getChannel(]);

            if (registrations == null) {
                registrations = new HashSet<PluginMessageListenerRegistration>();
                incomingByChannel.Add(registration.getChannel(), registrations);
            } else {
                if (registrations.contains(registration)) {
                    throw new ArgumentException("This registration already exists");
                }
            }

            registrations.add(registration);

            registrations = incomingByPlugin[registration.getPlugin(]);

            if (registrations == null) {
                registrations = new HashSet<PluginMessageListenerRegistration>();
                incomingByPlugin.Add(registration.getPlugin(), registrations);
            } else {
                if (registrations.contains(registration)) {
                    throw new ArgumentException("This registration already exists");
                }
            }

            registrations.add(registration);
        }
    }

    private void removeFromIncoming(PluginMessageListenerRegistration registration) {
        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByChannel[registration.getChannel(]);

            if (registrations != null) {
                registrations.remove(registration);

                if (registrations.isEmpty()) {
                    incomingByChannel.remove(registration.getChannel());
                }
            }

            registrations = incomingByPlugin[registration.getPlugin(]);

            if (registrations != null) {
                registrations.remove(registration);

                if (registrations.isEmpty()) {
                    incomingByPlugin.remove(registration.getPlugin());
                }
            }
        }
    }

    private void removeFromIncoming(Plugin plugin, String channel) {
        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

            if (registrations != null) {
                PluginMessageListenerRegistration[] toRemove = registrations.toArray(new PluginMessageListenerRegistration[0]);

                for (PluginMessageListenerRegistration registration : toRemove) {
                    if (registration.getChannel().equals(channel)) {
                        removeFromIncoming(registration);
                    }
                }
            }
        }
    }

    private void removeFromIncoming(Plugin plugin) {
        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

            if (registrations != null) {
                PluginMessageListenerRegistration[] toRemove = registrations.toArray(new PluginMessageListenerRegistration[0]);

                incomingByPlugin.remove(plugin);

                for (PluginMessageListenerRegistration registration : toRemove) {
                    removeFromIncoming(registration);
                }
            }
        }
    }

    public bool isReservedChannel(String channel) {
        validateChannel(channel);

        return channel.equals("REGISTER") || channel.equals("UNREGISTER");
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
        synchronized (outgoingLock) {
            HashSet<String> keys = outgoingByChannel.keySet();
            return ImmutableSet.copyOf(keys);
        }
    }

    public HashSet<String> getOutgoingChannels(Plugin plugin) {
        if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        }

        synchronized (outgoingLock) {
            HashSet<String> channels = outgoingByPlugin[plugin];

            if (channels != null) {
                return ImmutableSet.copyOf(channels);
            } else {
                return ImmutableSet.of();
            }
        }
    }

    public HashSet<String> getIncomingChannels() {
        synchronized (incomingLock) {
            HashSet<String> keys = incomingByChannel.keySet();
            return ImmutableSet.copyOf(keys);
        }
    }

    public HashSet<String> getIncomingChannels(Plugin plugin) {
        if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        }

        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

            if (registrations != null) {
                Builder<String> builder = ImmutableSet.builder();

                for (PluginMessageListenerRegistration registration : registrations) {
                    builder.add(registration.getChannel());
                }

                return builder.build();
            } else {
                return ImmutableSet.of();
            }
        }
    }

    public HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(Plugin plugin) {
        if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        }

        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

            if (registrations != null) {
                return ImmutableSet.copyOf(registrations);
            } else {
                return ImmutableSet.of();
            }
        }
    }

    public HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(String channel) {
        validateChannel(channel);

        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByChannel[channel];

            if (registrations != null) {
                return ImmutableSet.copyOf(registrations);
            } else {
                return ImmutableSet.of();
            }
        }
    }

    public HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(Plugin plugin, String channel) {
        if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        }
        validateChannel(channel);

        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

            if (registrations != null) {
                Builder<PluginMessageListenerRegistration> builder = ImmutableSet.builder();

                for (PluginMessageListenerRegistration registration : registrations) {
                    if (registration.getChannel().equals(channel)) {
                        builder.add(registration);
                    }
                }

                return builder.build();
            } else {
                return ImmutableSet.of();
            }
        }
    }

    public bool isRegistrationValid(PluginMessageListenerRegistration registration) {
        if (registration == null) {
            throw new ArgumentException("Registration cannot be null");
        }

        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[registration.getPlugin(]);

            if (registrations != null) {
                return registrations.contains(registration);
            }

            return false;
        }
    }

    public bool isIncomingChannelRegistered(Plugin plugin, String channel) {
        if (plugin == null) {
            throw new ArgumentException("Plugin cannot be null");
        }
        validateChannel(channel);

        synchronized (incomingLock) {
            HashSet<PluginMessageListenerRegistration> registrations = incomingByPlugin[plugin];

            if (registrations != null) {
                for (PluginMessageListenerRegistration registration : registrations) {
                    if (registration.getChannel().equals(channel)) {
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

        synchronized (outgoingLock) {
            HashSet<String> channels = outgoingByPlugin[plugin];

            if (channels != null) {
                return channels.contains(channel);
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

        for (PluginMessageListenerRegistration registration : registrations) {
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
        if (channel.Length > Messenger.MAX_CHANNEL_SIZE) {
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
        if (!source.isEnabled()) {
            throw new ArgumentException("Plugin must be enabled to send messages");
        }
        if (message == null) {
            throw new ArgumentException("Message cannot be null");
        }
        if (!messenger.isOutgoingChannelRegistered(source, channel)) {
            throw new ChannelNotRegisteredException(channel);
        }
        if (message.length > Messenger.MAX_MESSAGE_SIZE) {
            throw new MessageTooLargeException(message);
        }
        validateChannel(channel);
    }
}
