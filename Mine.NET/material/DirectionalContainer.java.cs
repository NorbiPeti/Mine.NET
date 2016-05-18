namespace Mine.NET.material{

import org.bukkit.Materials;
import org.bukkit.block.BlockFaces;

/**
 * Represents a furnace or a dispenser.
 */
public class DirectionalContainer : MaterialData : Directional {
    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public DirectionalContainer(int type) : base(type) {
    }

    public DirectionalContainer(Materials type) : base(type) {
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public DirectionalContainer(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public DirectionalContainer(Materials type, readonly byte data) : base(type, data) {
    }

    public void setFacingDirection(BlockFaces face) {
        byte data;

        switch (face) {
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

    public BlockFaces getFacing() {
        byte data = getData();

        switch (data) {
        case 0x2:
            return BlockFaces.NORTH;

        case 0x3:
            return BlockFaces.SOUTH;

        case 0x4:
            return BlockFaces.WEST;

        case 0x5:
        default:
            return BlockFaces.EAST;
        }
    }

    public override string ToString() {
        return base.ToString() + " facing " + getFacing();
    }

    public override DirectionalContainer clone() {
        return (DirectionalContainer) base.clone();
    }
}}
