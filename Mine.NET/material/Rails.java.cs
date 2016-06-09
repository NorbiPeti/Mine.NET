using Mine.NET.block;
using System;

namespace Mine.NET.material
{

    /**
     * Represents minecart rails.
     */
    public class Rails : MaterialData
    {

        public Rails() : base(Materials.RAILS)
        {
        }

        public Rails(Materials type) : base(type)
        {
        }

        private bool onslope;
        /**
         * @return the whether this track is set on a slope
         */
        public bool isOnSlope()
        {
            /*byte d = getConvertedData();

            return (d == 0x2 || d == 0x3 || d == 0x4 || d == 0x5);*/
            return onslope;
        }

        private bool curve;
        /**
         * @return the whether this track is set as a curve
         */
        public virtual bool isCurve()
        {
            /*byte d = getConvertedData();

            return (d == 0x6 || d == 0x7 || d == 0x8 || d == 0x9);*/
            return curve;
        }

        private BlockFaces direction;
        /**
         * @return the direction these tracks are set
         *     <p>
         *     Note that tracks are bidirectional and that the direction returned
         *     is the ascending direction if the track is set on a slope. If it is
         *     set as a curve, the corner of the track is returned.
         */
        public BlockFaces getDirection()
        {
            /*byte d = getConvertedData();

            switch (d) {
                case 0x0:
                default:
                    return BlockFaces.SOUTH;

                case 0x1:
                    return BlockFaces.EAST;

                case 0x2:
                    return BlockFaces.EAST;

                case 0x3:
                    return BlockFaces.WEST;

                case 0x4:
                    return BlockFaces.NORTH;

                case 0x5:
                    return BlockFaces.SOUTH;

                case 0x6:
                    return BlockFaces.NORTH_WEST;

                case 0x7:
                    return BlockFaces.NORTH_EAST;

                case 0x8:
                    return BlockFaces.SOUTH_EAST;

                case 0x9:
                    return BlockFaces.SOUTH_WEST;
            }*/
            return direction;
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getDirection() + (isCurve() ? " on a curve" : (isOnSlope() ? " on a slope" : ""));
        }

        /**
         * Set the direction of these tracks
         * <p>
         * Note that tracks are bidirectional and that the direction returned is
         * the ascending direction if the track is set on a slope. If it is set as
         * a curve, the corner of the track should be supplied.
         *
         * @param face the direction the track should be facing
         * @param isOnSlope whether or not the track should be on a slope
         */
        public virtual void setDirection(BlockFaces face, bool isOnSlope)
        {
            /*switch (face) {
                case BlockFaces.EAST:
                    setData((byte)(isOnSlope ? 0x2 : 0x1));
                    break;

                case BlockFaces.WEST:
                    setData((byte)(isOnSlope ? 0x3 : 0x1));
                    break;

                case BlockFaces.NORTH:
                    setData((byte)(isOnSlope ? 0x4 : 0x0));
                    break;

                case BlockFaces.SOUTH:
                    setData((byte)(isOnSlope ? 0x5 : 0x0));
                    break;

                case BlockFaces.NORTH_WEST:
                    setData((byte)0x6);
                    break;

                case BlockFaces.NORTH_EAST:
                    setData((byte)0x7);
                    break;

                case BlockFaces.SOUTH_EAST:
                    setData((byte)0x8);
                    break;

                case BlockFaces.SOUTH_WEST:
                    setData((byte)0x9);
                    break;
            }*/
            direction = face;
            onslope = isOnSlope;
        }

        public new Rails Clone() { return (Rails)base.Clone(); }
    }
}
