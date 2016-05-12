namespace Mine.NET.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * Represents a dispenser.
 */
public class Dispenser : FurnaceAndDispenser {

    public Dispenser() {
        base(Material.DISPENSER);
    }

    public Dispenser(BlockFace direction) {
        this();
        setFacingDirection(direction);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Dispenser(int type) {
        base(type);
    }

    public Dispenser(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Dispenser(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Dispenser(Material type, readonly byte data) {
        base(type, data);
    }

    public void setFacingDirection(BlockFace face) {
        byte data;

        switch (face) {
            case DOWN:
                data = 0x0;
                break;

            case UP:
                data = 0x1;
                break;

            case NORTH:
                data = 0x2;
                break;

            case SOUTH:
                data = 0x3;
                break;

            case WEST:
                data = 0x4;
                break;

            case EAST:
            default:
                data = 0x5;
        }

        setData(data);
    }

    public BlockFace getFacing() {
        int data = getData() & 0x7;

        switch (data) {
            case 0x0:
                return BlockFace.DOWN;

            case 0x1:
                return BlockFace.UP;

            case 0x2:
                return BlockFace.NORTH;

            case 0x3:
                return BlockFace.SOUTH;

            case 0x4:
                return BlockFace.WEST;

            case 0x5:
            default:
                return BlockFace.EAST;
        }
    }

    public override Dispenser clone() {
        return (Dispenser) base.clone();
    }
}
