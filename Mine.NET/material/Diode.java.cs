using Mine.NET.block;
using System;

namespace Mine.NET.material
{

    /**
     * Represents a diode/repeater in the on or off state, with a delay and facing
     * in a specific direction.
     *
     * @see Materials#DIODE_BLOCK_OFF
     * @see Materials#DIODE_BLOCK_ON
     */ //Find: "(namespace \S+);([^¤]+)([^¤]$)" - Replace: "$1{$2}$3"
    public class Diode : MaterialData<byte>, Directional, Redstone {

        protected static readonly BlockFaces DEFAULT_DIRECTION = BlockFaces.NORTH;
        protected static readonly int DEFAULT_DELAY = 1;
        protected static readonly bool DEFAULT_STATE = false;

        /**
         * Constructs a diode switched on, with a delay of 1 and facing the default
         * direction (north).
         *
         * By default this constructor creates a diode that is switched on for
         * backwards compatibility with past implementations.
         */
        public Diode() : this(DEFAULT_DIRECTION, DEFAULT_DELAY, true) {
        }

        /**
         * Constructs a diode switched off, with a delay of 1 and facing the
         * specified direction.
         *
         * @param facingDirection the direction the diode is facing
         *
         * @see BlockFaces
         */
        public Diode(BlockFaces facingDirection) : this(facingDirection, DEFAULT_DELAY, DEFAULT_STATE) {
        }

        /**
         * Constructs a diode switched off, with the specified delay and facing the
         * specified direction.
         *
         * @param facingDirection the direction the diode is facing
         * @param delay The number of ticks (1-4) before the diode turns on after
         * being powered
         *
         * @see BlockFaces
         */
        public Diode(BlockFaces facingDirection, int delay) : this(facingDirection, delay, DEFAULT_STATE) {
        }

        /**
         * Constructs a diode switched on or off, with the specified delay and
         * facing the specified direction.
         *
         * @param facingDirection the direction the diode is facing
         * @param delay The number of ticks (1-4) before the diode turns on after
         * being powered
         * @param state True if the diode is in the on state
         *
         * @see BlockFaces
         */
        public Diode(BlockFaces facingDirection, int delay, bool state) : base(state ? Materials.DIODE_BLOCK_ON : Materials.DIODE_BLOCK_OFF) {
            setFacingDirection(facingDirection);
            setDelay(delay);
        }

        public Diode(Materials type) : base(type) {
        }
        
        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Diode(Materials type, byte data) : base(type, data) {
        }

        /**
         * Sets the delay of the repeater.
         *
         * @param delay The new delay (1-4)
         */
        public void setDelay(int delay) {
            if (delay > 4) {
                delay = 4;
            }
            if (delay < 1) {
                delay = 1;
            }
            byte newData = (byte)(getData() & 0x3);

            setData((byte)(newData | ((delay - 1) << 2)));
        }

        /**
         * Gets the delay of the repeater in ticks.
         *
         * @return The delay (1-4)
         */
        public int getDelay() {
            return (getData() >> 2) + 1;
        }

        /**
         * Sets the direction this diode is facing.
         *
         * @param face The direction to set this diode to
         *
         * @see BlockFaces
         */
        public void setFacingDirection(BlockFaces face) {
            int delay = getDelay();
            byte data;

            switch (face) {
                case BlockFaces.EAST:
                    data = 0x1;
                    break;
                case BlockFaces.SOUTH:
                    data = 0x2;
                    break;
                case BlockFaces.WEST:
                    data = 0x3;
                    break;
                case BlockFaces.NORTH:
                default:
                    data = 0x0;
                    break;
            }

            setData(data);
            setDelay(delay);
        }

        /**
         * Gets the direction this diode is facing
         *
         * @return The direction this diode is facing
         *
         * @see BlockFaces
         */
        public BlockFaces getFacing() {
            byte data = (byte)(getData() & 0x3);

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
            }
        }

        public override string ToString() {
            return base.ToString() + " facing " + getFacing() + " with " + getDelay() + " ticks delay";
        }

        public override Diode clone() {
            return (Diode)base.clone();
        }

        /**
         * Checks if the diode is powered.
         *
         * @return true if the diode is powered
         */
        public bool isPowered() {
            return getItemType() == Materials.DIODE_BLOCK_ON;
        }
    }
}
