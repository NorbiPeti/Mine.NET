using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a block causes an entity to combust.
     */
    public class EntityCombustByBlockEventArgs<T> : EntityCombustEventArgs<T> where T : Entity
    {
        private readonly Block combuster;

        public EntityCombustByBlockEventArgs(Block combuster, Entity combustee, int duration) : base(combustee, duration)
        {
            this.combuster = combuster;
        }

        /**
         * The combuster can be lava or a block that is on fire.
         * <p>
         * WARNING: block may be null.
         *
         * @return the Block that set the combustee alight.
         */
        public Block getCombuster()
        {
            return combuster;
        }
    }
}
