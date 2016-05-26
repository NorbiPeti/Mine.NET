using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block is formed by entities.
     * <p>
     * Examples:
     * <ul>
     * <li>Snow formed by a {@link org.bukkit.entity.Snowman}.
     * <li>Frosted Ice formed by the Frost Walker enchantment.
     * </ul>
     */
    public class EntityBlockFormEventArgs : BlockFormEventArgs
    {
        private readonly Entity entity;

        public EntityBlockFormEventArgs(Entity entity, Block block, BlockState blockstate) : base(block, blockstate)
        {
            this.entity = entity;
        }

        /**
         * Get the entity that formed the block.
         *
         * @return Entity involved in event
         */
        public Entity getEntity()
        {
            return entity;
        }
    }
}
