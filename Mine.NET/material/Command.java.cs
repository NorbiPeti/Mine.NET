using System;

namespace Mine.NET.material
{
    /**
     * Represents a command block
     */
    public class Command : MaterialData, Redstone
    {
        private bool powered = false;

        public Command() : base(Materials.COMMAND)
        {
        }

        public Command(Materials type) : base(type)
        {
        }

        /**
         * Gets the current state of this Materials, indicating if it's powered or
         * unpowered
         *
         * @return true if powered, otherwise false
         */
        public bool isPowered()
        {
            //return (getData() & 1) != 0;
            return powered;
        }

        /**
         * Sets the current state of this Materials
         *
         * @param bool
         *            whether or not the command block is powered
         */
        public void setPowered(bool bool_)
        {
            //setData((byte)(bool_ ? (getData() | 1) : (getData() & -2)));
            powered = bool_;
        }

        public override string ToString()
        {
            return base.ToString() + " " + (isPowered() ? "" : "NOT ") + "POWERED";
        }

        public new Command Clone() { return (Command)base.Clone(); }
    }
}
