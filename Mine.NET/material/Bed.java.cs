package org.bukkit.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * Represents a bed.
 */
public class Bed : MaterialData : Directional {

    /**
     * Default constructor for a bed.
     */
    public Bed() {
        base(Material.BED_BLOCK);
    }

    /**
     * Instantiate a bed facing in a particular direction.
     *
     * @param direction the direction the bed's head is facing
     */
    public Bed(BlockFace direction) {
        this();
        setFacingDirection(direction);
    }

    /**
     *
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Bed(int type) {
        base(type);
    }

    public Bed(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Bed(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Bed(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * Determine if this block represents the head of the bed
     *
     * @return true if this is the head of the bed, false if it is the foot
     */
    public bool isHeadOfBed() {
        return (getData() & 0x8) == 0x8;
    }

    /**
     * Configure this to be either the head or the foot of the bed
     *
     * @param isHeadOfBed True to make it the head.
     */
    public void setHeadOfBed(bool isHeadOfBed) {
        setData((byte) (isHeadOfBed ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    /**
     * Set which direction the head of the bed is facing. Note that this will
     * only affect one of the two blocks the bed is made of.
     */
    public void setFacingDirection(BlockFace face) {
        byte data;

        switch (face) {
        case SOUTH:
            data = 0x0;
            break;

        case WEST:
            data = 0x1;
            break;

        case NORTH:
            data = 0x2;
            break;

        case EAST:
        default:
            data = 0x3;
        }

        if (isHeadOfBed()) {
            data |= 0x8;
        }

        setData(data);
    }

    /**
     * Get the direction that this bed's head is facing toward
     *
     * @return the direction the head of the bed is facing
     */
    public BlockFace getFacing() {
        byte data = (byte) (getData() & 0x7);

        switch (data) {
        case 0x0:
            return BlockFace.SOUTH;

        case 0x1:
            return BlockFace.WEST;

        case 0x2:
            return BlockFace.NORTH;

        case 0x3:
        default:
            return BlockFace.EAST;
        }
    }

    public override string ToString() {
        return (isHeadOfBed() ? "HEAD" : "FOOT") + " of " + base.ToString() + " facing " + getFacing();
    }

    public override Bed clone() {
        return (Bed) base.clone();
    }
}
