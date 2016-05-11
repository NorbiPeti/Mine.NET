package org.bukkit.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * This is the superclass for the {@link DetectorRail} and {@link PoweredRail}
 * classes
 */
public class ExtendedRails extends Rails {
    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public ExtendedRails(int type) {
        super(type);
    }

    public ExtendedRails(Material type) {
        super(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public ExtendedRails(int type, readonly byte data) {
        super(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public ExtendedRails(Material type, readonly byte data) {
        super(type, data);
    }

    @Override
    public bool isCurve() {
        return false;
    }

    /**
     *
     * [Obsolete] Magic value
     */
    [Obsolete]
    @Override
    protected byte getConvertedData() {
        return (byte) (getData() & 0x7);
    }

    @Override
    public void setDirection(BlockFace face, bool isOnSlope) {
        bool extraBitSet = (getData() & 0x8) == 0x8;

        if (face != BlockFace.WEST && face != BlockFace.EAST && face != BlockFace.NORTH && face != BlockFace.SOUTH) {
            throw new IllegalArgumentException("Detector rails and powered rails cannot be set on a curve!");
        }

        super.setDirection(face, isOnSlope);
        setData((byte) (extraBitSet ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    @Override
    public ExtendedRails clone() {
        return (ExtendedRails) super.clone();
    }
}
