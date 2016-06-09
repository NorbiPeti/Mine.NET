using System;

namespace Mine.NET.plugin.messaging
{

    /**
     * Contains information about a {@link Plugin}s registration to a plugin
     * channel.
     */
    public sealed class PluginMessageListenerRegistration
    {
        private readonly Messenger messenger;
        private readonly Plugin plugin;
        private readonly String channel;
        private readonly PluginMessageListener listener;

        public PluginMessageListenerRegistration(Messenger messenger, Plugin plugin, String channel, PluginMessageListener listener)
        {
            if (messenger == null)
            {
                throw new ArgumentException("Messenger cannot be null!");
            }
            if (plugin == null)
            {
                throw new ArgumentException("Plugin cannot be null!");
            }
            if (channel == null)
            {
                throw new ArgumentException("Channel cannot be null!");
            }
            if (listener == null)
            {
                throw new ArgumentException("Listener cannot be null!");
            }

            this.messenger = messenger;
            this.plugin = plugin;
            this.channel = channel;
            this.listener = listener;
        }

        /**
         * Gets the plugin channel that this registration is about.
         *
         * @return Plugin channel.
         */
        public String getChannel()
        {
            return channel;
        }

        /**
         * Gets the registered listener described by this registration.
         *
         * @return Registered listener.
         */
        public PluginMessageListener getListener()
        {
            return listener;
        }

        /**
         * Gets the plugin that this registration is for.
         *
         * @return Registered plugin.
         */
        public Plugin getPlugin()
        {
            return plugin;
        }

        /**
         * Checks if this registration is still valid.
         *
         * @return True if this registration is still valid, otherwise false.
         */
        public bool isValid()
        {
            return messenger.isRegistrationValid(this);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            PluginMessageListenerRegistration other = (PluginMessageListenerRegistration)obj;
            if (this.messenger != other.messenger && (this.messenger == null || !this.messenger.Equals(other.messenger)))
            {
                return false;
            }
            if (this.plugin != other.plugin && (this.plugin == null || !this.plugin.Equals(other.plugin)))
            {
                return false;
            }
            if ((this.channel == null) ? (other.channel != null) : !this.channel.Equals(other.channel))
            {
                return false;
            }
            if (this.listener != other.listener && (this.listener == null || !this.listener.Equals(other.listener)))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 53 * hash + (this.messenger != null ? this.messenger.GetHashCode() : 0);
            hash = 53 * hash + (this.plugin != null ? this.plugin.GetHashCode() : 0);
            hash = 53 * hash + (this.channel != null ? this.channel.GetHashCode() : 0);
            hash = 53 * hash + (this.listener != null ? this.listener.GetHashCode() : 0);
            return hash;
        }
    }
}
