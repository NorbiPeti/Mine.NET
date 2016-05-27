using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * This event is called when a service is registered.
     * <p>
     * Warning: The order in which register and unregister events are called
     * should not be relied upon.
     */
    public class ServiceRegisterEventArgs<T> : ServiceEventArgs<T>
    {
        public ServiceRegisterEventArgs(RegisteredServiceProvider<T> registeredProvider) :
            base(registeredProvider)
        {
        }
    }
}
