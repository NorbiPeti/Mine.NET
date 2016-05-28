using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * This event is called when a service is unregistered.
     * <p>
     * Warning: The order in which register and unregister events are called
     * should not be relied upon.
     */
    public class ServiceUnregisterEventArgs : ServiceEventArgs
    {
        public ServiceUnregisterEventArgs(RegisteredServiceProvider serviceProvider) :
            base(serviceProvider)
        {
        }
    }
}
