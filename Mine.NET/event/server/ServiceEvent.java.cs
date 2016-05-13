using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * An event relating to a registered service. This is called in a {@link
     * org.bukkit.plugin.ServicesManager}
     */
    public abstract class ServiceEvent<T> : ServerEvent
    {
        private readonly RegisteredServiceProvider<T> provider;

        public ServiceEvent(RegisteredServiceProvider<T> provider)
        {
            this.provider = provider;
        }

        public RegisteredServiceProvider<T> getProvider()
        {
            return provider;
        }
    }
}
