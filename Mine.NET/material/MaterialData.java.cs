package org.bukkit.material;

import org.bukkit.inventory.ItemStack;
import org.bukkit.Material;

/**
 * Handles specific metadata for certain items or blocks
 */
public class MaterialData : Cloneable {
    private readonly int type;
    private byte data = 0;

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public MaterialData(int type) {
        this(type, (byte) 0);
    }

    public MaterialData(Material type) {
        this(type, (byte) 0);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public MaterialData(int type, readonly byte data) {
        this.type = type;
        this.data = data;
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public MaterialData(Material type, readonly byte data) {
        this(type.getId(), data);
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
    public Material getItemType() {
        return Material.getMaterial(type);
    }

    /**
     * Gets the Material Id that this MaterialData represents
     *
     * @return Material Id represented by this MaterialData
     * [Obsolete] Magic value
     */
    [Obsolete]
    public int getItemTypeId() {
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
        if (obj != null && obj instanceof MaterialData) {
            MaterialData md = (MaterialData) obj;

            return (md.getItemTypeId() == getItemTypeId() && md.getData() == getData());
        } else {
            return false;
        }
    }

    @Override
    public MaterialData clone() {
        try {
            return (MaterialData) super.clone();
        } catch (CloneNotSupportedException e) {
            throw new Error(e);
        }
    }
}
