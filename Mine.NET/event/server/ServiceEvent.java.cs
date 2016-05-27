using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * An event relating to a registered service. This is called in a {@link
     * org.bukkit.plugin.ServicesManager}
     */
    public abstract class ServiceEventArgs<T> : ServerEventArgs
    {
        private readonly RegisteredServiceProvider<T> provider;

        public ServiceEventArgs(RegisteredServiceProvider<T> provider)
        {
            this.provider = provider;
        }

        public RegisteredServiceProvider<T> getProvider()
        {
            return provider;
        }
    }
}
