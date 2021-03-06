namespace Mine.NET.plugin
{
    /**
     * Extends RegisteredListener to include timing information
     */
    public class TimedRegisteredListener : RegisteredListener {
        private int count;
        private long totalTime;
        private Class<? : Event> eventClass;
        private bool multiple = false;

        public TimedRegisteredListener(Listener pluginListener, readonly EventExecutor eventExecutor, readonly EventPriority eventPriority, readonly Plugin registeredPlugin, readonly bool listenCancelled) : base(pluginListener, eventExecutor, eventPriority, registeredPlugin, listenCancelled) {
    }

    public override void callEvent(Event event) {
            if (event.isAsynchronous()) {
            base.callEvent(event);
            return;
        }
        count++;
        Class<? : Event> newEventClass = event.getClass();
        if (this.eventClass == null) {
            this.eventClass = newEventClass;
        } else if (!this.eventClass.equals(newEventClass)) {
            multiple = true;
            this.eventClass = getCommonSuperclass(newEventClass, this.eventClass).asSubclass(typeof(Event));
        }
        long start = System.nanoTime();
        base.callEvent(event);
        totalTime += System.nanoTime() - start;
        }

        private static Type getCommonSuperclass(Type class1, Type class2) {
            while (!class1.isAssignableFrom(class2)) {
                class1 = class1.getSuperclass();
            }
            return class1;
        }

        /**
         * Resets the call count and total time for this listener
         */
        public void reset() {
            count = 0;
            totalTime = 0;
        }

        /**
         * Gets the total times this listener has been called
         *
         * @return Times this listener has been called
         */
        public int getCount() {
            return count;
        }

        /**
         * Gets the total time calls to this listener have taken
         *
         * @return Total time for all calls of this listener
         */
        public long getTotalTime() {
            return totalTime;
        }

        /**
         * Gets the class of the events this listener handled. If it handled
         * multiple classes of event, the closest shared superclass will be
         * returned, such that for any event this listener has handled,
         * <code>this.getEventClass().isAssignableFrom(event.getClass())</code>
         * and no class <code>this.getEventClass().isAssignableFrom(clazz)
         * {@literal && this.getEventClass() != clazz &&}
         * event.getClass().isAssignableFrom(clazz)</code> for all handled events.
         *
         * @return the event class handled by this RegisteredListener
         */
        public Class<? : Event> getEventClass() {
            return eventClass;
        }

        /**
         * Gets whether this listener has handled multiple events, such that for
         * some two events, <code>eventA.getClass() != eventB.getClass()</code>.
         *
         * @return true if this listener has handled multiple events
         */
        public bool hasMultiple() {
            return multiple;
        }
    }
}
