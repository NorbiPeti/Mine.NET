using Mine.NET.block;

namespace Mine.NET.material
{
    /**
     * Represents a bed.
     */
    public class Bed : MaterialData, Directional
    {
        private bool ishead = false;
        private BlockFaces face;

        /**
         * Default constructor for a bed.
         */
        public Bed() :
            base(Materials.BED_BLOCK)
        {
        }

        /**
         * Instantiate a bed facing in a particular direction.
         *
         * @param direction the direction the bed's head is facing
         */
        public Bed(BlockFaces direction) : this()
        {
            setFacingDirection(direction);
        }

        public Bed(Materials type) : base(type)
        {
        }

        /**
         * Determine if this block represents the head of the bed
         *
         * @return true if this is the head of the bed, false if it is the foot
         */
        public bool isHeadOfBed()
        {
            //return (getData() & 0x8) == 0x8;
            return ishead;
        }

        /**
         * Configure this to be either the head or the foot of the bed
         *
         * @param isHeadOfBed True to make it the head.
         */
        public void setHeadOfBed(bool isHeadOfBed)
        {
            //setData((byte)(isHeadOfBed ? (getData() | 0x8) : (getData() & ~0x8)));
            ishead = isHeadOfBed;
        }

        /**
         * Set which direction the head of the bed is facing. Note that this will
         * only affect one of the two blocks the bed is made of.
         */
        public void setFacingDirection(BlockFaces face)
        {
            /*if (face == BlockFaces.SOUTH)
                data = 0x0;
            else if (face == BlockFaces.WEST)
                data = 0x1;
            else if (face == BlockFaces.NORTH)
                data = 0x2;
            else
                data = 0x3;

            if (isHeadOfBed())
            {
                data |= 0x8;
            }*/

            this.face = face;
        }

        /**
         * Get the direction that this bed's head is facing toward
         *
         * @return the direction the head of the bed is facing
         */
        public BlockFaces getFacing()
        {
            return face;
        }

        public override string ToString()
        {
            return (isHeadOfBed() ? "HEAD" : "FOOT") + " of " + base.ToString() + " facing " + getFacing();
        }

        public override Bed clone()
        {
            return (Bed)base.clone(); //TODo
        }
    }
}
