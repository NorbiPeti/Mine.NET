using System;

namespace Mine.NET.material
{
    /**
     * Represents the different types of sandstone.
     */
    public class Sandstone : MaterialData
    {
        public Sandstone() : base(Materials.SANDSTONE)
        {
        }

        public Sandstone(SandstoneType type) : this()
        {
            setType(type);
        }

        public Sandstone(Materials type) : base(type)
        {
        }

        private SandstoneType type;
        /**
         * Gets the current type of this sandstone
         *
         * @return SandstoneType of this sandstone
         */
        public SandstoneType getType()
        {
            return type;
        }

        /**
         * Sets the type of this sandstone
         *
         * @param type New type of this sandstone
         */
        public void setType(SandstoneType type)
        {
            //setData(type.getData());
            this.type = type;
        }

        public override string ToString()
        {
            return getType() + " " + base.ToString();
        }

        public new Sandstone Clone() { return (Sandstone)base.Clone(); }
    }
}
