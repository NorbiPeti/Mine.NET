using Mine.NET.block;
using System;

namespace Mine.NET.material
{
    /**
     * Represents a comparator in the on or off state, in normal or subtraction mode and facing in a specific direction.
     *
     * @see Materials#REDSTONE_COMPARATOR_OFF
     * @see Materials#REDSTONE_COMPARATOR_ON
     */
    public class Comparator : MaterialData, Directional, Redstone {
        protected static readonly BlockFaces DEFAULT_DIRECTION = BlockFaces.NORTH;
        protected static readonly bool DEFAULT_SUBTRACTION_MODE = false;
        protected static readonly bool DEFAULT_STATE = false;

        private bool subtraction = false;
        private BlockFaces face;
        private bool powered = false;
        private bool isbeingpowered;

        /**
         * Constructs a comparator switched off, with the default mode (normal) and facing the default direction (north).
         */
        public Comparator() :           this(DEFAULT_DIRECTION, DEFAULT_SUBTRACTION_MODE, false)
        {
        }

        /**
         * Constructs a comparator switched off, with the default mode (normal) and facing the specified direction.
         *
         * @param facingDirection the direction the comparator is facing
         *
         * @see BlockFaces
         */
        public Comparator(BlockFaces facingDirection) :
            this(facingDirection, DEFAULT_SUBTRACTION_MODE, DEFAULT_STATE)
        {
        }

        /**
         * Constructs a comparator switched off, with the specified mode and facing the specified direction.
         *
         * @param facingDirection the direction the comparator is facing
         * @param isSubtraction True if the comparator is in subtraction mode, false for normal comparator operation
         *
         * @see BlockFaces
         */
        public Comparator(BlockFaces facingDirection, bool isSubtraction) :
            this(facingDirection, isSubtraction, DEFAULT_STATE)
        {
        }

        /**
         * Constructs a comparator switched on or off, with the specified mode and facing the specified direction.
         *
         * @param facingDirection the direction the comparator is facing
         * @param isSubtraction True if the comparator is in subtraction mode, false for normal comparator operation
         * @param state True if the comparator is in the on state
         *
         * @see BlockFaces
         */
        public Comparator(BlockFaces facingDirection, bool isSubtraction, bool state) :
            base(state ? Materials.REDSTONE_COMPARATOR_ON : Materials.REDSTONE_COMPARATOR_OFF)
        {
            setFacingDirection(facingDirection);
            setSubtractionMode(isSubtraction);
        }

        public Comparator(Materials type) :            base(type)
        {
        }

        /**
         * Sets whether the comparator is in subtraction mode.
         *
         * @param isSubtraction True if the comparator is in subtraction mode, false for normal comparator operation
         */
        public void setSubtractionMode(bool isSubtraction) {
            //setData((byte)(getData() & 0xB | (isSubtraction ? 0x4 : 0x0)));
            subtraction = isSubtraction;
        }

        /**
         * Checks whether the comparator is in subtraction mode
         *
         * @return True if the comparator is in subtraction mode, false if normal comparator operation
         */
        public bool isSubtractionMode() {
            //return (getData() & 0x4) != 0;
            return subtraction;
        }

        /**
         * Sets the direction this comparator is facing
         *
         * @param face The direction to set this comparator to
         *
         * @see BlockFaces
         */
        public void setFacingDirection(BlockFaces face) {
            /*int data = getData() & 0xC;

            switch (face) {
                case BlockFaces.EAST:
                    data |= 0x1;
                    break;

                case BlockFaces.SOUTH:
                    data |= 0x2;
                    break;

                case BlockFaces.WEST:
                    data |= 0x3;
                    break;

                case BlockFaces.NORTH:
                default:
                    data |= 0x0;
                    break;
            }

            setData((byte)data);*/

            this.face = face;
        }

        /**
         * Gets the direction this comparator is facing
         *
         * @return The direction this comparator is facing
         *
         * @see BlockFaces
         */
        public BlockFaces getFacing() {
            /*byte data = (byte)(getData() & 0x3);

            switch (data) {
                case 0x0:
                default:
                    return BlockFaces.NORTH;

                case 0x1:
                    return BlockFaces.EAST;

                case 0x2:
                    return BlockFaces.SOUTH;

                case 0x3:
                    return BlockFaces.WEST;
            }*/

            return face;
        }

        public override string ToString() {
            return base.ToString() + " facing " + getFacing() + " in " + (isSubtractionMode() ? "subtraction" : "comparator") + " mode";
        }

        public new Comparator Clone() { return (Comparator)base.Clone(); }

        /**
         * Checks if the comparator is powered
         *
         * @return true if the comparator is powered
         */
        public bool isPowered() {
            //return getItemType() == Materials.REDSTONE_COMPARATOR_ON;
            return powered;
        }

        /**
         * Checks if the comparator is being powered
         *
         * @return true if the comparator is being powered
         */
        public bool isBeingPowered() {
            //return (getData() & 0x8) != 0;
            return isbeingpowered;
        }
    }
}
