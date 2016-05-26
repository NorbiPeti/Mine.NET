using Mine.NET.entity;

namespace Mine.NET.Event.hanging
{
    /**
     * Triggered when a hanging entity is removed by an entity
     */
    public class HangingBreakByEntityEventArgs : HangingBreakEventArgs
    {
        private readonly Entity remover;

        public HangingBreakByEntityEventArgs(Hanging hanging, Entity remover) :
                base(hanging, RemoveCause.ENTITY)
        {
            this.remover = remover;
        }

        /**
         * Gets the entity that removed the hanging entity
         *
         * @return the entity that removed the hanging entity
         */
        public Entity getRemover()
        {
            return remover;
        }
    }
}
