using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.block
{
    /**
     * Called when a block is ignited. If you want to catch when a Player places
     * fire, you need to use {@link BlockPlaceEvent}.
     * <p>
     * If a Block Ignite event is cancelled, the block will not be ignited.
     */
    public class BlockIgniteEventArgs : BlockEventArgs, Cancellable
    {
        private readonly IgniteCause cause;
        private readonly Entity ignitingEntity;
        private readonly Block ignitingBlock;
        private bool cancel;

        public BlockIgniteEventArgs(Block theBlock, IgniteCause cause, Entity ignitingEntity) : this(theBlock, cause, ignitingEntity, null)
        {

        }

        public BlockIgniteEventArgs(Block theBlock, IgniteCause cause, Block ignitingBlock) : this(theBlock, cause, null, ignitingBlock)
        {

        }

        public BlockIgniteEventArgs(Block theBlock, IgniteCause cause, Entity ignitingEntity, Block ignitingBlock) : base(theBlock)
        {
            this.cause = cause;
            this.ignitingEntity = ignitingEntity;
            this.ignitingBlock = ignitingBlock;
            this.cancel = false;
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
         * Gets the cause of block ignite.
         *
         * @return An IgniteCause value detailing the cause of block ignition
         */
        public IgniteCause getCause()
        {
            return cause;
        }

        /**
         * Gets the player who ignited this block
         *
         * @return The Player that placed/ignited the fire block, or null if not ignited by a Player.
         */
        public Player getPlayer()
        {
            if (ignitingEntity is Player)
            {
                return (Player)ignitingEntity;
            }

            return null;
        }

        /**
         * Gets the entity who ignited this block
         *
         * @return The Entity that placed/ignited the fire block, or null if not ignited by a Entity.
         */
        public Entity getIgnitingEntity()
        {
            return ignitingEntity;
        }

        /**
         * Gets the block who ignited this block
         *
         * @return The Block that placed/ignited the fire block, or null if not ignited by a Block.
         */
        public Block getIgnitingBlock()
        {
            return ignitingBlock;
        }

        /**
         * An enum to specify the cause of the ignite
         */
        public enum IgniteCause
        {

            /**
             * Block ignition caused by lava.
             */
            LAVA,
            /**
             * Block ignition caused by a player or dispenser using flint-and-steel.
             */
            FLINT_AND_STEEL,
            /**
             * Block ignition caused by dynamic spreading of fire.
             */
            SPREAD,
            /**
             * Block ignition caused by lightning.
             */
            LIGHTNING,
            /**
             * Block ignition caused by an entity using a fireball.
             */
            FIREBALL,
            /**
             * Block ignition caused by an Ender Crystal.
             */
            ENDER_CRYSTAL,
            /**
             * Block ignition caused by explosion.
             */
            EXPLOSION,
        }
    }
}
