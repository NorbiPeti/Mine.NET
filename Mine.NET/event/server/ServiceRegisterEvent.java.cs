namespace Mine.NET.Event.server
{
    /**
     * This event is called when a service is registered.
     * <p>
     * Warning: The order in which register and unregister events are called
     * should not be relied upon.
     */
    public class ServiceRegisterEvent<T> : ServiceEvent<T>
    {
        private static readonly HandlerList handlers = new HandlerList();

        public ServiceRegisterEvent(RegisteredServiceProvider<T> registeredProvider) :
            base(registeredProvider)
        {
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
