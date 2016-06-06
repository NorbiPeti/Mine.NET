using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * MaterialData for torches
     */
    public class Torch : SimpleAttachableMaterialData
    {
        public Torch() : base(Materials.TORCH)
        {
        }

        public Torch(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Torch(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Gets the face that this block is attached on
         *
         * @return BlockFaces attached to
         */
        public override BlockFaces getAttachedFace()
        {
            byte data = getData();

            switch (data)
            {
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

        public override void setFacingDirection(BlockFaces face)
        {
            byte data;

            switch (face)
            {
                case BlockFaces.EAST:
                    data = 0x1;
                    break;

                case BlockFaces.WEST:
                    data = 0x2;
                    break;

                case BlockFaces.SOUTH:
                    data = 0x3;
                    break;

                case BlockFaces.NORTH:
                    data = 0x4;
                    break;

                case BlockFaces.UP:
                default:
                    data = 0x5;
                    break;
            }

            setData(data);
        }

        public new Torch Clone() { return (Torch)base.Clone(); }
    }
}
