namespace Mine.NET.material{

/**
 * Represents a pumpkin.
 */
public class Pumpkin : MaterialData : Directional {

    public Pumpkin() : base(Materials.PUMPKIN) {
    }

    /**
     * Instantiate a pumpkin facing in a particular direction.
     *
     * @param direction the direction the pumkin's face is facing
     */
    public Pumpkin(BlockFaces direction) {
        this();
        setFacingDirection(direction);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Pumpkin(int type) : base(type) {
    }

    public Pumpkin(Materials type) : base(type) {
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Pumpkin(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Pumpkin(Materials type, readonly byte data) : base(type, data) {
    }

    public bool isLit() {
        return getItemType() == Materials.JACK_O_LANTERN;
    }

    public void setFacingDirection(BlockFaces face) {
        byte data;

        switch (face) {
        case NORTH:
            data = 0x0;
            break;

        case EAST:
            data = 0x1;
            break;

        case SOUTH:
            data = 0x2;
            break;

        case WEST:
        default:
            data = 0x3;
        }

        setData(data);
    }

    public BlockFaces getFacing() {
        byte data = getData();

        switch (data) {
        case 0x0:
            return BlockFaces.NORTH;

        case 0x1:
            return BlockFaces.EAST;

        case 0x2:
            return BlockFaces.SOUTH;

        case 0x3:
        default:
            return BlockFaces.EAST;
        }
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing() + " " + (isLit() ? "" : "NOT ") + "LIT";
    }

    public override Pumpkin clone() {
        return (Pumpkin) base.clone();
    }
}
