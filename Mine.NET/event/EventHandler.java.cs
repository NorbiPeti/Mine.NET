using System;

namespace Mine.NET.Event
{
    /**
     * An annotation to mark methods as being event handler methods
     */
    /*[AttributeUsage(AttributeTargets.Method)]
    public class EventHandler : Attribute {*/

        /**
         * Define the priority of the event.
         * <p>
         * First priority to the last priority executed:
         * <ol>
         * <li>LOWEST
         * <li>LOW
         * <li>NORMAL
         * <li>HIGH
         * <li>HIGHEST
         * <li>MONITOR
         * </ol>
         * 
         * @return the priority
         */
        //EventPriority priority() default EventPriority.NORMAL; //TODO

    /**
     * Define if the handler ignores a cancelled event.
     * <p>
     * If ignoreCancelled is true and the event is cancelled, the method is
     * not called. Otherwise, the method is always called.
     * 
     * @return whether cancelled events should be ignored
     */
    /*bool ignoreCancelled() default false;
    }*/
}
