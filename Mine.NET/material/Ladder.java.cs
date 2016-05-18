namespace Mine.NET.material;

import org.bukkit.block.BlockFaces;
import org.bukkit.Materials;

/**
 * Represents Ladder data
 */
public class Ladder : SimpleAttachableMaterialData {
    public Ladder() {
        base(Materials.LADDER);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Ladder(int type) {
        base(type);
    }

    public Ladder(Materials type) {
        base(type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Ladder(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Ladder(Materials type, readonly byte data) {
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
        case 0x2:
            return BlockFaces.SOUTH;

        case 0x3:
            return BlockFaces.NORTH;

        case 0x4:
            return BlockFaces.EAST;

        case 0x5:
            return BlockFaces.WEST;
        }

        return null;
    }

    /**
     * Sets the direction this ladder is facing
     */
    public void setFacingDirection(BlockFaces face) {
        byte data = (byte) 0x0;

        switch (face) {
        case SOUTH:
            data = 0x2;
            break;

        case NORTH:
            data = 0x3;
            break;

        case EAST:
            data = 0x4;
            break;

        case WEST:
            data = 0x5;
            break;
        }

        setData(data);

    }

    public override Ladder clone() {
        return (Ladder) base.clone();
    }
}
