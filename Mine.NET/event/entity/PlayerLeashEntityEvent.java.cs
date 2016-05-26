using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called immediately prior to a creature being leashed by a player.
     */
    public class PlayerLeashEntityEventArgs : GameEventArgs, Cancellable {
        private readonly Entity leashHolder;
        private readonly Entity entity;
        private bool cancelled = false;
        private readonly Player player;

        public PlayerLeashEntityEventArgs(Entity what, Entity leashHolder, Player leasher) {
            this.leashHolder = leashHolder;
            this.entity = what;
            this.player = leasher;
        }

        /**
         * Returns the entity that is holding the leash.
         *
         * @return The leash holder
         */
        public Entity getLeashHolder() {
            return leashHolder;
        }

        /**
         * Returns the entity being leashed.
         *
         * @return The entity
         */
        public Entity getEntity() {
            return entity;
        }

        /**
         * Returns the player involved in this event
         *
         * @return Player who is involved in this event
         */
        public Player getPlayer() {
            return player;
        }

        public bool isCancelled() {
            return this.cancelled;
        }

        public void setCancelled(bool cancel) {
            this.cancelled = cancel;
        }
    }
}
