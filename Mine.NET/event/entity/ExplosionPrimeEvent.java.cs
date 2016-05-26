using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an entity has made a decision to explode.
     */
    public class ExplosionPrimeEventArgs : EntityEventArgs<Explosive>, Cancellable
    {
        private bool cancel;
        private float radius;
        private bool fire;

        public ExplosionPrimeEventArgs(Entity what, float radius, bool fire) : base(what)
        {
            this.cancel = false;
            this.radius = radius;
            this.fire = fire;
        }

        public ExplosionPrimeEventArgs(Explosive explosive) :
            this(explosive, explosive.getYield(), explosive.isIncendiary())
        {
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        /**
         * Gets the radius of the explosion
         *
         * @return returns the radius of the explosion
         */
        public float getRadius()
        {
            return radius;
        }

        /**
         * Sets the radius of the explosion
         *
         * @param radius the radius of the explosion
         */
        public void setRadius(float radius)
        {
            this.radius = radius;
        }

        /**
         * Gets whether this explosion will create fire or not
         *
         * @return true if this explosion will create fire
         */
        public bool getFire()
        {
            return fire;
        }

        /**
         * Sets whether this explosion will create fire or not
         *
         * @param fire true if you want this explosion to create fire
         */
        public void setFire(bool fire)
        {
            this.fire = fire;
        }
    }
}
