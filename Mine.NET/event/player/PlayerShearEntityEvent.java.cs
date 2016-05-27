using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player shears an entity
     */
    public class PlayerShearEntityEventArgs : PlayerEventArgs, Cancellable
    {
        private bool cancel;
        private readonly Entity what;

        public PlayerShearEntityEventArgs(Player who, Entity what) : base(who)
        {
            this.cancel = false;
            this.what = what;
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
         * Gets the entity the player is shearing
         *
         * @return the entity the player is shearing
         */
        public Entity getEntity()
        {
            return what;
        }
    }
}
