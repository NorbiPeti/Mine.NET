using System;

namespace Mine.NET.entity
{
    /**
     * Represents a falling block
     */
    public interface FallingBlock : Entity
    {

        /**
         * Get the Materials of the falling block
         *
         * @return Materials of the block
         */
        Materials getMaterial();

        /**
         * Get the ID of the falling block
         *
         * @return ID type of the block
         * [Obsolete] Magic value
         */
        [Obsolete]
        int getBlockId();

        /**
         * Get the data for the falling block
         *
         * @return data of the block
         * [Obsolete] Magic value
         */
        [Obsolete]
        byte getBlockData();

        /**
         * Get if the falling block will break into an item if it cannot be placed
         *
         * @return true if the block will break into an item when obstructed
         */
        bool getDropItem();

        /**
         * Set if the falling block will break into an item if it cannot be placed
         *
         * @param drop true to break into an item when obstructed
         */
        void setDropItem(bool drop);

        /**
         * Get the HurtEntities state of this block.
         *
         * @return whether entities will be damaged by this block.
         */
        bool canHurtEntities();

        /**
         * Set the HurtEntities state of this block.
         *
         * @param hurtEntities whether entities will be damaged by this block.
         */
        void setHurtEntities(bool hurtEntities);
    }
}
