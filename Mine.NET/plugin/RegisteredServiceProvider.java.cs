using System;

namespace Mine.NET.plugin
{
    /**
     * A registered service provider.
     *
     * @param <T> Service
     */
    public class RegisteredServiceProvider<T> : RegisteredServiceProvider {
        private Plugin plugin;
        private T provider;
        private ServicePriority priority;

        //public RegisteredServiceProvider(Class<T> service, T provider, ServicePriority priority, Plugin plugin)
        public RegisteredServiceProvider(T provider, ServicePriority priority, Plugin plugin)
        {
            this.plugin = plugin;
            this.provider = provider;
            this.priority = priority;
        }

        /*public Class<T> getService() {
            return service;
        }*/

        public Plugin getPlugin() {
            return plugin;
        }

        public T getProvider() {
            return provider;
        }

        public override ServicePriority getPriority() {
            return priority;
        }

        public override int CompareTo(RegisteredServiceProvider other) {
            if (priority == other.getPriority()) {
                return 0;
            } else {
                return priority < other.getPriority() ? 1 : -1;
            }
        }
    }

    public abstract class RegisteredServiceProvider : IComparable<RegisteredServiceProvider>
    {
        public abstract int CompareTo(RegisteredServiceProvider other);

        public abstract ServicePriority getPriority();
    }
}
