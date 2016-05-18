namespace Mine.NET.material;

import org.bukkit.block.BlockFaces;
import org.bukkit.Material;

/**
 * MaterialData for torches
 */
public class Torch : SimpleAttachableMaterialData {
    public Torch() {
        base(Material.TORCH);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Torch(int type) {
        base(type);
    }

    public Torch(Material type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Torch(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Torch(Material type, readonly byte data) {
        base(type, data);
    }

    /**
     * Gets the face that this block is attached on
     *
     * @return BlockFaces attached to
     */
    public BlockFaces getAttachedFace() {
        byte data = getData();

        switch (data) {
        case 0x1:
            return BlockFaces.WEST;

        case 0x2:
            return BlockFaces.EAST;

        case 0x3:
            return BlockFaces.NORTH;

        case 0x4:
            return BlockFaces.SOUTH;

        case 0x5:
        default:
            return BlockFaces.DOWN;
        }
    }

    public void setFacingDirection(BlockFaces face) {
        byte data;

        switch (face) {
        case EAST:
            data = 0x1;
            break;

        case WEST:
            data = 0x2;
            break;

        case SOUTH:
            data = 0x3;
            break;

        case NORTH:
            data = 0x4;
            break;

        case UP:
        default:
            data = 0x5;
        }

        setData(data);
    }

    public override Torch clone() {
        return (Torch) base.clone();
    }
}
