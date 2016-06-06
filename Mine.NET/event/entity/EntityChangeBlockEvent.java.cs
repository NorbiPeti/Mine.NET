using Mine.NET.block;
using Mine.NET.entity;
using Mine.NET.material;
using System;

namespace Mine.NET.Event.entity
{
    /**
     * Called when any Entity, excluding players, changes a block.
     */
    public class EntityChangeBlockEventArgs : EntityEventArgs<Entity>, Cancellable
    {
        private readonly Block block;
        private bool cancel;
        private readonly Materials to;
        private readonly MaterialData data;

        /**
         *
         * @param what the Entity causing the change
         * @param block the block (before the change)
         * @param to the future Materials being changed to
         * @param data the future block data
         * [Obsolete] Magic value
         */
        public EntityChangeBlockEventArgs(Entity what, Block block, Materials to, MaterialData data) : base(what)
        {
            this.block = block;
            this.cancel = false;
            this.to = to;
            this.data = data;
        }

        /**
         * Gets the block the entity is changing
         *
         * @return the block that is changing
         */
        public Block getBlock()
        {
            return block;
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
         * Gets the Materials that the block is changing into
         *
         * @return the Materials that the block is changing into
         */
        public Materials getTo()
        {
            return to;
        }

        /**
         * Gets the data for the block that would be changed into
         *
         * @return the data for the block that would be changed into
         * [Obsolete] Magic value
         */
        public MaterialData getData()
        {
            return data; //May be null
        }
    }
}
