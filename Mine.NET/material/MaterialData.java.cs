using Mine.NET.inventory;
using System;

namespace Mine.NET.material
{
    /**
     * Handles specific metadata for certain items or blocks
     */
    public class MaterialData
    {
        private readonly Materials type;

        public MaterialData(Materials type)
        {
            this.type = type;
        }

        public MaterialData(MaterialData data)
        {
            this.type = data.type;
        }

        /**
         * Gets the Materials that this MaterialData represents
         *
         * @return Materials represented by this MaterialData
         */
        public Materials getItemType()
        {
            return type;
        }

        /**
         * Creates a new ItemStack based on this MaterialData
         *
         * @return New ItemStack containing a copy of this MaterialData
         */
        public ItemStack toItemStack()
        {
            return new ItemStack(type, 0);
        }

        /**
         * Creates a new ItemStack based on this MaterialData
         *
         * @param amount The stack size of the new stack
         * @return New ItemStack containing a copy of this MaterialData
         */
        public ItemStack toItemStack(int amount)
        {
            return new ItemStack(type, amount);
        }

        public override string ToString()
        {
            return getItemType().ToString();
        }

        public override int GetHashCode()
        {
            //return ((getItemTypeId() << 8) ^ getData());
            return (int)getItemType();
        }

        public override bool Equals(Object obj)
        {
            if (obj != null && obj is MaterialData)
            {
                MaterialData md = (MaterialData)obj;

                return (md.getItemType() == getItemType()); //TODO: Equality for subtypes
            }
            else
            {
                return false;
            }
        }

        public MaterialData Clone()
        {
            return new MaterialData(this); //TODO
        }
    }
}
