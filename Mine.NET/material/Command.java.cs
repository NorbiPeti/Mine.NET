using System;

namespace Mine.NET.material
{
    /**
     * Represents a command block
     */
    public class Command : MaterialData<byte>, Redstone
    {
        public Command() : base(Materials.COMMAND, 0)
        {
        }

        public Command(Materials type) : base(type, 0)
        {
        }

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public Command(Materials type, byte data) : base(type, data)
        {
        }

        /**
         * Gets the current state of this Material, indicating if it's powered or
         * unpowered
         *
         * @return true if powered, otherwise false
         */
        public bool isPowered()
        {
            return (getData() & 1) != 0;
        }

        /**
         * Sets the current state of this Material
         *
         * @param bool
         *            whether or not the command block is powered
         */
        public void setPowered(bool bool_)
        {
            setData((byte)(bool_ ? (getData() | 1) : (getData() & -2)));
        }

        public override string ToString()
        {
            return base.ToString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
        }

        public override Command clone()
        {
            return (Command)base.clone();
        }
    }
}
