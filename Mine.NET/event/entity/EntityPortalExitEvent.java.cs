using Mine.NET.entity;
using Mine.NET.util;

namespace Mine.NET.Event.entity
{
    /**
     * Called before an entity exits a portal.
     * <p>
     * This event allows you to modify the velocity of the entity after they have
     * successfully exited the portal.
     */
    public class EntityPortalExitEventArgs : EntityTeleportEventArgs
    {
        private Vector before;
        private Vector after;

        public EntityPortalExitEventArgs(Entity entity, Location from, Location to, Vector before, Vector after) :
            base(entity, from, to)
        {
            this.before = before;
            this.after = after;
        }

        /**
         * Gets a copy of the velocity that the entity has before entering the
         * portal.
         *
         * @return velocity of entity before entering the portal
         */
        public Vector getBefore()
        {
            return this.before.Clone();
        }

        /**
         * Gets a copy of the velocity that the entity will have after exiting the
         * portal.
         *
         * @return velocity of entity after exiting the portal
         */
        public Vector getAfter()
        {
            return this.after.Clone();
        }

        /**
         * Sets the velocity that the entity will have after exiting the portal.
         * 
         * @param after the velocity after exiting the portal
         */
        public void setAfter(Vector after)
        {
            this.after = after.Clone();
        }
    }
}
