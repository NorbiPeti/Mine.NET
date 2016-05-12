namespace Mine.NET.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * This is the superclass for the {@link DetectorRail} and {@link PoweredRail}
 * classes
 */
public class ExtendedRails : Rails {
    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public ExtendedRails(int type) {
        base(type);
    }

    public ExtendedRails(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public ExtendedRails(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public ExtendedRails(Material type, readonly byte data) {
        base(type, data);
    }

    public override bool isCurve() {
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

    public override void setDirection(BlockFace face, bool isOnSlope) {
        bool extraBitSet = (getData() & 0x8) == 0x8;

        if (face != BlockFace.WEST && face != BlockFace.EAST && face != BlockFace.NORTH && face != BlockFace.SOUTH) {
            throw new ArgumentException("Detector rails and powered rails cannot be set on a curve!");
        }

        base.setDirection(face, isOnSlope);
        setData((byte) (extraBitSet ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    public override ExtendedRails clone() {
        return (ExtendedRails) base.clone();
    }
}
