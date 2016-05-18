using Mine.NET.inventory;
using System;

namespace Mine.NET.material
{
    /**
     * Handles specific metadata for certain items or blocks
     */
    public class MaterialData<TData> : MaterialData
    {
        private readonly Materials type;
        private TData data;

        public MaterialData(Materials type, TData data)
        {
            this.type = type;
            this.data = data;
        }
            
        /**
         * Gets the raw data in this Materials
         *
         * @return Raw data
         * [Obsolete] Magic value
         */
        public TData getData()
        {
            return data;
        }

        /**
         * Sets the raw data of this Materials
         *
         * @param data New raw data
         * [Obsolete] Magic value
         */
        public void setData(TData data)
        {
            this.data = data;
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
        public ItemStack<TData> toItemStack()
        {
            return new ItemStack<TData>(type, 0, data);
        }

        /**
         * Creates a new ItemStack based on this MaterialData
         *
         * @param amount The stack size of the new stack
         * @return New ItemStack containing a copy of this MaterialData
         */
        public ItemStack<TData> toItemStack(int amount)
        {
            return new ItemStack<TData>(type, amount, data);
        }

        public override string ToString()
        {
            return getItemType() + "(" + getData() + ")";
        }

        public override int GetHashCode()
        {
            return ((getItemTypeId() << 8) ^ getData());
        }

        public override bool Equals(Object obj)
        {
            if (obj != null && obj is MaterialData)
            {
                MaterialData md = (MaterialData)obj;

                return (md.getItemTypeId() == getItemTypeId() && md.getData() == getData());
            }
            else
            {
                return false;
            }
        }

        public MaterialData Clone()
        {
            return null; //TODO
        }
    }

    public abstract class MaterialData
    {
    }
}
