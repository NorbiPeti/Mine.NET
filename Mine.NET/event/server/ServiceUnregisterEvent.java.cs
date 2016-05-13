using Mine.NET.plugin;

namespace Mine.NET.Event.server
{
    /**
     * This event is called when a service is unregistered.
     * <p>
     * Warning: The order in which register and unregister events are called
     * should not be relied upon.
     */
    public class ServiceUnregisterEvent<T> : ServiceEvent<T> {
        private static readonly HandlerList handlers = new HandlerList();

        public ServiceUnregisterEvent(RegisteredServiceProvider<T> serviceProvider) :
            base(serviceProvider)
        {
        }

        public override HandlerList getHandlers() {
            return handlers;
        }

        public static HandlerList getHandlerList() {
            return handlers;
        }
    }
}
