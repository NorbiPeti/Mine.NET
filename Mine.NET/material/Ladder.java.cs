using Mine.NET.block;
using System;

namespace Mine.NET.material{

/**
 * Represents Ladder data
 */
public class Ladder : SimpleAttachableMaterialData {
    public Ladder() : base(Materials.LADDER) {
    }

    public Ladder(Materials type) : base(type) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Ladder(Materials type, byte data) : base(type, data) {
    }

    /**
     * Gets the face that this block is attached on
     *
     * @return BlockFaces attached to
     */
    public override BlockFaces getAttachedFace() {
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

            return BlockFaces.SELF;
    }

    /**
     * Sets the direction this ladder is facing
     */
    public override void setFacingDirection(BlockFaces face) {
        byte data = (byte) 0x0;

        switch (face) {
        case BlockFaces.SOUTH:
            data = 0x2;
            break;

        case BlockFaces.NORTH:
            data = 0x3;
            break;

        case BlockFaces.EAST:
            data = 0x4;
            break;

        case BlockFaces.WEST:
            data = 0x5;
            break;
        }

        setData(data);

    }

    public override Ladder clone() {
        return (Ladder) base.clone();
    }
}
