using Mine.NET.block;
using System;

namespace Mine.NET.material
{

    /**
     * Represents a hopper in an active or deactivated state and facing in a
     * specific direction.
     *
     * @see Materials#HOPPER
     */
    public class Hopper : MaterialData<byte>, Directional, Redstone
    {

        protected static readonly BlockFaces DEFAULT_DIRECTION = BlockFaces.DOWN;
        protected static readonly bool DEFAULT_ACTIVE = true;

        /**
         * Constructs a hopper facing the default direction (down) and initially
         * active.
         */
        public Hopper() : this(DEFAULT_DIRECTION, DEFAULT_ACTIVE)
        {
        }

        /**
         * Constructs a hopper facing the specified direction and initially active.
         *
         * @param facingDirection the direction the hopper is facing
         *
         * @see BlockFaces
         */
        public Hopper(BlockFaces facingDirection) : this(facingDirection, DEFAULT_ACTIVE)
        {
        }

        /**
         * Constructs a hopper facing the specified direction and either active or
         * not.
         *
         * @param facingDirection the direction the hopper is facing
         * @param isActive True if the hopper is initially active, false if
         * deactivated
         *
         * @see BlockFaces
         */
        public Hopper(BlockFaces facingDirection, bool isActive) : base(Materials.HOPPER)
        {
            setFacingDirection(facingDirection);
            setActive(isActive);
        }

        public Hopper(Materials type) : base(type)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Hopper(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Sets whether the hopper is active or not.
         *
         * @param isActive True if the hopper is active, false if deactivated as if
         * powered by redstone
         */
        public void setActive(bool isActive)
        {
            setData((byte)(getData() & 0x7 | (isActive ? 0x0 : 0x8)));
        }

        /**
         * Checks whether the hopper is active or not.
         *
         * @return True if the hopper is active, false if deactivated
         */
        public bool isActive()
        {
            return (getData() & 0x8) == 0;
        }

        /**
         * Sets the direction this hopper is facing
         *
         * @param face The direction to set this hopper to
         *
         * @see BlockFaces
         */
        public void setFacingDirection(BlockFaces face)
        {
            int data = getData() & 0x8;

            switch (face)
            {
                case BlockFaces.DOWN:
                    data |= 0x0;
                    break;
                case BlockFaces.NORTH:
                    data |= 0x2;
                    break;
                case BlockFaces.SOUTH:
                    data |= 0x3;
                    break;
                case BlockFaces.WEST:
                    data |= 0x4;
                    break;
                case BlockFaces.EAST:
                    data |= 0x5;
                    break;
            }

            setData((byte)data);
        }

        /**
         * Gets the direction this hopper is facing
         *
         * @return The direction this hopper is facing
         *
         * @see BlockFaces
         */
        public BlockFaces getFacing()
        {
            byte data = (byte)(getData() & 0x7);

            switch (data)
            {
                default:
                case 0x0:
                    return BlockFaces.DOWN;
                case 0x2:
                    return BlockFaces.NORTH;
                case 0x3:
                    return BlockFaces.SOUTH;
                case 0x4:
                    return BlockFaces.WEST;
                case 0x5:
                    return BlockFaces.EAST;
            }
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing();
        }

        public new Hopper Clone() { return (Hopper)base.Clone(); }

        /**
         * Checks if the hopper is powered.
         *
         * @return true if the hopper is powered
         */
        public bool isPowered()
        {
            return (getData() & 0x8) != 0;
        }
    }
}
