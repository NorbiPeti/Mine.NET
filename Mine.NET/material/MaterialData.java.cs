using Mine.NET.inventory;
using System;

namespace Mine.NET.material
{
/**
 * Handles specific metadata for certain items or blocks
 */
public class MaterialData : ICloneable {
    private readonly Materials type;
    private byte data = 0;

        public MaterialData(Materials type, byte data)
        {
            this.type = type;
            this.data = data;
        }

        public MaterialData(Materials type) : this(type, 0)
        {
        }

        /**
         * Gets the raw data in this material
         *
         * @return Raw data
         * [Obsolete] Magic value
         */
        [Obsolete]
    public byte getData() {
        return data;
    }

    /**
     * Sets the raw data of this material
     *
     * @param data New raw data
     * [Obsolete] Magic value
     */
    [Obsolete]
    public void setData(byte data) {
        this.data = data;
    }

    /**
     * Gets the Material that this MaterialData represents
     *
     * @return Material represented by this MaterialData
     */
    public Materials getItemType() {
            return type;
    }

    /**
     * Creates a new ItemStack based on this MaterialData
     *
     * @return New ItemStack containing a copy of this MaterialData
     */
    public ItemStack toItemStack() {
        return new ItemStack(type, 0, data);
    }

    /**
     * Creates a new ItemStack based on this MaterialData
     *
     * @param amount The stack size of the new stack
     * @return New ItemStack containing a copy of this MaterialData
     */
    public ItemStack toItemStack(int amount) {
        return new ItemStack(type, amount, data);
    }

    public override string ToString() {
        return getItemType() + "(" + getData() + ")";
    }

    public override int GetHashCode() {
        return ((getItemTypeId() << 8) ^ getData());
    }

    public override bool Equals(Object obj) {
        if (obj != null && obj is MaterialData) {
            MaterialData md = (MaterialData) obj;

            return (md.getItemTypeId() == getItemTypeId() && md.getData() == getData());
        } else {
            return false;
        }
    }

    public MaterialData Clone() {
            return null; //TODO
    }
}
}
