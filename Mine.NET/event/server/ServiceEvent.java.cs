using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * An event relating to a registered service. This is called in a {@link
     * org.bukkit.plugin.ServicesManager}
     */
    public abstract class ServiceEventArgs : ServerEventArgs
    {
        private readonly RegisteredServiceProvider provider;

        public ServiceEventArgs(RegisteredServiceProvider provider)
        {
            this.provider = provider;
        }

        public RegisteredServiceProvider getProvider()
        {
            return provider;
        }
    }
}
