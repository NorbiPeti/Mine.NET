namespace Mine.NET.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFaces;

/**
 * Represents a hopper in an active or deactivated state and facing in a
 * specific direction.
 *
 * @see Material#HOPPER
 */
public class Hopper : MaterialData : Directional, Redstone {

    protected static readonly BlockFaces DEFAULT_DIRECTION = BlockFaces.DOWN;
    protected static readonly bool DEFAULT_ACTIVE = true;

    /**
     * Constructs a hopper facing the default direction (down) and initially
     * active.
     */
    public Hopper() {
        this(DEFAULT_DIRECTION, DEFAULT_ACTIVE);
    }

    /**
     * Constructs a hopper facing the specified direction and initially active.
     *
     * @param facingDirection the direction the hopper is facing
     *
     * @see BlockFaces
     */
    public Hopper(BlockFaces facingDirection) {
        this(facingDirection, DEFAULT_ACTIVE);
    }

    /**
     * Constructs a hopper facing the specified direction and either active or
     * not.
     *
     * @param facingDirection the direction the hopper is facing
     * @param isActive True if the hopper is initially active, false if
     * deactivated
     *
     * @see BlockFaces
     */
    public Hopper(BlockFaces facingDirection, bool isActive) {
        base(Material.HOPPER);
        setFacingDirection(facingDirection);
        setActive(isActive);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Hopper(int type) {
        base(type);
    }

    public Hopper(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Hopper(int type, byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Hopper(Material type, byte data) {
        base(type, data);
    }

    /**
     * Sets whether the hopper is active or not.
     *
     * @param isActive True if the hopper is active, false if deactivated as if
     * powered by redstone
     */
    public void setActive(bool isActive) {
        setData((byte) (getData() & 0x7 | (isActive ? 0x0 : 0x8)));
    }

    /**
     * Checks whether the hopper is active or not.
     *
     * @return True if the hopper is active, false if deactivated
     */
    public bool isActive() {
        return (getData() & 0x8) == 0;
    }

    /**
     * Sets the direction this hopper is facing
     *
     * @param face The direction to set this hopper to
     *
     * @see BlockFaces
     */
    public override void setFacingDirection(BlockFaces face) {
        int data = getData() & 0x8;

        switch (face) {
            case DOWN:
                data |= 0x0;
                break;
            case NORTH:
                data |= 0x2;
                break;
            case SOUTH:
                data |= 0x3;
                break;
            case WEST:
                data |= 0x4;
                break;
            case EAST:
                data |= 0x5;
                break;
        }

        setData((byte) data);
    }

    /**
     * Gets the direction this hopper is facing
     *
     * @return The direction this hopper is facing
     *
     * @see BlockFaces
     */
    public override BlockFaces getFacing() {
        byte data = (byte) (getData() & 0x7);

        switch (data) {
            default:
            case 0x0:
                return BlockFaces.DOWN;
            case 0x2:
                return BlockFaces.NORTH;
            case 0x3:
                return BlockFaces.SOUTH;
            case 0x4:
                return BlockFaces.WEST;
            case 0x5:
                return BlockFaces.EAST;
        }
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing();
    }

    public override Hopper clone() {
        return (Hopper) base.clone();
    }

    /**
     * Checks if the hopper is powered.
     *
     * @return true if the hopper is powered
     */
    public override bool isPowered() {
        return (getData() & 0x8) != 0;
    }
}
