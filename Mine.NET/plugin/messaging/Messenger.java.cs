using Mine.NET.entity;
using System;
using System.Collections.Generic;

namespace Mine.NET.plugin.messaging
{

    /**
     * A class responsible for managing the registrations of plugin channels and
     * their listeners.
     */
    public interface Messenger
    {

        /**
         * Represents the largest size that an individual Plugin Message may be.
         */
        public static readonly int MAX_MESSAGE_SIZE = 32766;

        /**
         * Represents the largest size that a Plugin Channel may be.
         */
        public static readonly int MAX_CHANNEL_SIZE = 20;

        /**
         * Checks if the specified channel is a reserved name.
         *
         * @param channel Channel name to check.
         * @return True if the channel is reserved, otherwise false.
         * @throws ArgumentException Thrown if channel is null.
         */
        bool isReservedChannel(String channel);

        /**
         * Registers the specific plugin to the requested outgoing plugin channel,
         * allowing it to send messages through that channel to any clients.
         *
         * @param plugin Plugin that wishes to send messages through the channel.
         * @param channel Channel to register.
         * @throws ArgumentException Thrown if plugin or channel is null.
         */
        void registerOutgoingPluginChannel(Plugin plugin, String channel);

        /**
         * Unregisters the specific plugin from the requested outgoing plugin
         * channel, no longer allowing it to send messages through that channel to
         * any clients.
         *
         * @param plugin Plugin that no longer wishes to send messages through the
         *     channel.
         * @param channel Channel to unregister.
         * @throws ArgumentException Thrown if plugin or channel is null.
         */
        void unregisterOutgoingPluginChannel(Plugin plugin, String channel);

        /**
         * Unregisters the specific plugin from all outgoing plugin channels, no
         * longer allowing it to send any plugin messages.
         *
         * @param plugin Plugin that no longer wishes to send plugin messages.
         * @throws ArgumentException Thrown if plugin is null.
         */
        void unregisterOutgoingPluginChannel(Plugin plugin);

        /**
         * Registers the specific plugin for listening on the requested incoming
         * plugin channel, allowing it to act upon any plugin messages.
         *
         * @param plugin Plugin that wishes to register to this channel.
         * @param channel Channel to register.
         * @param listener Listener to receive messages on.
         * @return The resulting registration that was made as a result of this
         *     method.
         * @throws ArgumentException Thrown if plugin, channel or listener
         *     is null, or the listener is already registered for this channel.
         */
        PluginMessageListenerRegistration registerIncomingPluginChannel(Plugin plugin, String channel, PluginMessageListener listener);

        /**
         * Unregisters the specific plugin's listener from listening on the
         * requested incoming plugin channel, no longer allowing it to act upon
         * any plugin messages.
         *
         * @param plugin Plugin that wishes to unregister from this channel.
         * @param channel Channel to unregister.
         * @param listener Listener to stop receiving messages on.
         * @throws ArgumentException Thrown if plugin, channel or listener
         *     is null.
         */
        void unregisterIncomingPluginChannel(Plugin plugin, String channel, PluginMessageListener listener);

        /**
         * Unregisters the specific plugin from listening on the requested
         * incoming plugin channel, no longer allowing it to act upon any plugin
         * messages.
         *
         * @param plugin Plugin that wishes to unregister from this channel.
         * @param channel Channel to unregister.
         * @throws ArgumentException Thrown if plugin or channel is null.
         */
        void unregisterIncomingPluginChannel(Plugin plugin, String channel);

        /**
         * Unregisters the specific plugin from listening on all plugin channels
         * through all listeners.
         *
         * @param plugin Plugin that wishes to unregister from this channel.
         * @throws ArgumentException Thrown if plugin is null.
         */
        void unregisterIncomingPluginChannel(Plugin plugin);

        /**
         * Gets a set containing all the outgoing plugin channels.
         *
         * @return List of all registered outgoing plugin channels.
         */
        HashSet<String> getOutgoingChannels();

        /**
         * Gets a set containing all the outgoing plugin channels that the
         * specified plugin is registered to.
         *
         * @param plugin Plugin to retrieve channels for.
         * @return List of all registered outgoing plugin channels that a plugin
         *     is registered to.
         * @throws ArgumentException Thrown if plugin is null.
         */
        HashSet<String> getOutgoingChannels(Plugin plugin);

        /**
         * Gets a set containing all the incoming plugin channels.
         *
         * @return List of all registered incoming plugin channels.
         */
        HashSet<String> getIncomingChannels();

        /**
         * Gets a set containing all the incoming plugin channels that the
         * specified plugin is registered for.
         *
         * @param plugin Plugin to retrieve channels for.
         * @return List of all registered incoming plugin channels that the plugin
         *     is registered for.
         * @throws ArgumentException Thrown if plugin is null.
         */
        HashSet<String> getIncomingChannels(Plugin plugin);

        /**
         * Gets a set containing all the incoming plugin channel registrations
         * that the specified plugin has.
         *
         * @param plugin Plugin to retrieve registrations for.
         * @return List of all registrations that the plugin has.
         * @throws ArgumentException Thrown if plugin is null.
         */
        HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(Plugin plugin);

        /**
         * Gets a set containing all the incoming plugin channel registrations
         * that are on the requested channel.
         *
         * @param channel Channel to retrieve registrations for.
         * @return List of all registrations that are on the channel.
         * @throws ArgumentException Thrown if channel is null.
         */
        HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(String channel);

        /**
         * Gets a set containing all the incoming plugin channel registrations
         * that the specified plugin has on the requested channel.
         *
         * @param plugin Plugin to retrieve registrations for.
         * @param channel Channel to filter registrations by.
         * @return List of all registrations that the plugin has.
         * @throws ArgumentException Thrown if plugin or channel is null.
         */
        HashSet<PluginMessageListenerRegistration> getIncomingChannelRegistrations(Plugin plugin, String channel);

        /**
         * Checks if the specified plugin message listener registration is valid.
         * <p>
         * A registration is considered valid if it has not be unregistered and
         * that the plugin is still enabled.
         *
         * @param registration Registration to check.
         * @return True if the registration is valid, otherwise false.
         */
        bool isRegistrationValid(PluginMessageListenerRegistration registration);

        /**
         * Checks if the specified plugin has registered to receive incoming
         * messages through the requested channel.
         *
         * @param plugin Plugin to check registration for.
         * @param channel Channel to test for.
         * @return True if the channel is registered, else false.
         */
        bool isIncomingChannelRegistered(Plugin plugin, String channel);

        /**
         * Checks if the specified plugin has registered to send outgoing messages
         * through the requested channel.
         *
         * @param plugin Plugin to check registration for.
         * @param channel Channel to test for.
         * @return True if the channel is registered, else false.
         */
        bool isOutgoingChannelRegistered(Plugin plugin, String channel);

        /**
         * Dispatches the specified incoming message to any registered listeners.
         *
         * @param source Source of the message.
         * @param channel Channel that the message was sent by.
         * @param message Raw payload of the message.
         */
        void dispatchIncomingMessage(Player source, String channel, byte[] message);
    }
}
