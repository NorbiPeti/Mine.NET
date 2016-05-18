using System;

namespace Mine.NET.inventory.meta
{
    /**
     * Represents a skull ({@link Materials#SKULL_ITEM}) that can have an owner.
     */
    public interface SkullMeta : ItemMeta<SkullMeta>
    {

        /**
         * Gets the owner of the skull.
         *
         * @return the owner if the skull
         */
        String getOwner();

        /**
         * Checks to see if the skull has an owner.
         *
         * @return true if the skull has an owner
         */
        bool hasOwner();

        /**
         * Sets the owner of the skull.
         * <p>
         * Plugins should check that hasOwner() returns true before calling this
         * plugin.
         *
         * @param owner the new owner of the skull
         * @return true if the owner was successfully set
         */
        bool setOwner(String owner);
    }
}
