namespace Mine.NET.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * Represents a comparator in the on or off state, in normal or subtraction mode and facing in a specific direction.
 *
 * @see Material#REDSTONE_COMPARATOR_OFF
 * @see Material#REDSTONE_COMPARATOR_ON
 */
public class Comparator : MaterialData : Directional, Redstone {
    protected static readonly BlockFace DEFAULT_DIRECTION = BlockFace.NORTH;
    protected static readonly bool DEFAULT_SUBTRACTION_MODE = false;
    protected static readonly bool DEFAULT_STATE = false;

    /**
     * Constructs a comparator switched off, with the default mode (normal) and facing the default direction (north).
     */
    public Comparator() {
        this(DEFAULT_DIRECTION, DEFAULT_SUBTRACTION_MODE, false);
    }

    /**
     * Constructs a comparator switched off, with the default mode (normal) and facing the specified direction.
     *
     * @param facingDirection the direction the comparator is facing
     *
     * @see BlockFace
     */
    public Comparator(BlockFace facingDirection) {
        this(facingDirection, DEFAULT_SUBTRACTION_MODE, DEFAULT_STATE);
    }

    /**
     * Constructs a comparator switched off, with the specified mode and facing the specified direction.
     *
     * @param facingDirection the direction the comparator is facing
     * @param isSubtraction True if the comparator is in subtraction mode, false for normal comparator operation
     *
     * @see BlockFace
     */
    public Comparator(BlockFace facingDirection, bool isSubtraction) {
    	this(facingDirection, isSubtraction, DEFAULT_STATE);
    }

    /**
     * Constructs a comparator switched on or off, with the specified mode and facing the specified direction.
     *
     * @param facingDirection the direction the comparator is facing
     * @param isSubtraction True if the comparator is in subtraction mode, false for normal comparator operation
     * @param state True if the comparator is in the on state
     *
     * @see BlockFace
     */
    public Comparator(BlockFace facingDirection, bool isSubtraction, bool state) {
        base(state ? Material.REDSTONE_COMPARATOR_ON : Material.REDSTONE_COMPARATOR_OFF);
        setFacingDirection(facingDirection);
        setSubtractionMode(isSubtraction);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Comparator(int type) {
        base(type);
    }

    public Comparator(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Comparator(int type, byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Comparator(Material type, byte data) {
        base(type, data);
    }

    /**
     * Sets whether the comparator is in subtraction mode.
     *
     * @param isSubtraction True if the comparator is in subtraction mode, false for normal comparator operation
     */
    public void setSubtractionMode(bool isSubtraction) {
        setData((byte)(getData() & 0xB | (isSubtraction ? 0x4 : 0x0)));
    }

    /**
     * Checks whether the comparator is in subtraction mode
     *
     * @return True if the comparator is in subtraction mode, false if normal comparator operation
     */
    public bool isSubtractionMode() {
        return (getData() & 0x4) != 0;
    }

    /**
     * Sets the direction this comparator is facing
     *
     * @param face The direction to set this comparator to
     *
     * @see BlockFace
     */
    public override void setFacingDirection(BlockFace face) {
        int data = getData() & 0xC;

        switch (face) {
        case EAST:
            data |= 0x1;
            break;

        case SOUTH:
            data |= 0x2;
            break;

        case WEST:
            data |= 0x3;
            break;

        case NORTH:
        default:
            data |= 0x0;
        }

        setData((byte)data);
    }

    /**
     * Gets the direction this comparator is facing
     *
     * @return The direction this comparator is facing
     *
     * @see BlockFace
     */
    public override BlockFace getFacing() {
        byte data = (byte) (getData() & 0x3);

        switch (data) {
        case 0x0:
        default:
            return BlockFace.NORTH;

        case 0x1:
            return BlockFace.EAST;

        case 0x2:
            return BlockFace.SOUTH;

        case 0x3:
            return BlockFace.WEST;
        }
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing() + " in " + (isSubtractionMode() ? "subtraction" : "comparator") + " mode";
    }

    public override Comparator clone() {
        return (Comparator) base.clone();
    }

    /**
     * Checks if the comparator is powered
     *
     * @return true if the comparator is powered
     */
    public override bool isPowered() {
        return getItemType() == Material.REDSTONE_COMPARATOR_ON;
    }

    /**
     * Checks if the comparator is being powered
     *
     * @return true if the comparator is being powered
     */
    public bool isBeingPowered() {
        return (getData() & 0x8) != 0;
    }
}
