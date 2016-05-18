namespace Mine.NET.material{

/**
 * Represents a skull.
 */
public class Skull : MaterialData : Directional {
    public Skull() : base(Materials.SKULL) {
    }

    /**
     * Instantiate a skull facing in a particular direction.
     *
     * @param direction the direction the skull's face is facing
     */
    public Skull(BlockFaces direction) {
        this();
        setFacingDirection(direction);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Skull(int type) : base(type) {
    }

    public Skull(Materials type) : base(type) {
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Skull(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Skull(Materials type, readonly byte data) : base(type, data) {
    }

    public void setFacingDirection(BlockFaces face) {
        int data;

        switch (face) {
            case SELF:
            default:
                data = 0x1;
                break;

            case NORTH:
                data = 0x2;
                break;

            case EAST:
                data = 0x4;
                break;

            case SOUTH:
                data = 0x3;
                break;

            case WEST:
                data = 0x5;
        }

        setData((byte) data);
    }

    public BlockFaces getFacing() {
        int data = getData();

        switch (data) {
            case 0x1:
            default:
                return BlockFaces.SELF;

            case 0x2:
                return BlockFaces.NORTH;

            case 0x3:
                return BlockFaces.SOUTH;

            case 0x4:
                return BlockFaces.EAST;

            case 0x5:
                return BlockFaces.WEST;
        }
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing();
    }

    public override Skull clone() {
        return (Skull) base.clone();
    }
}
