namespace Mine.NET.material
{
    /**
     * Represents the different types of coals.
     */
    public class Coal : MaterialData
    {
        public Coal() : base(Materials.COAL)
        {
        }

        public Coal(CoalType type) : this()
        {
            setType(type);
        }

        public Coal(Materials type) : base(type)
        {
        }

        /**
         * Gets the current type of this coal
         *
         * @return CoalType of this coal
         */
        public CoalType getType()
        {
            return (CoalType)getData();
        }

        /**
         * Sets the type of this coal
         *
         * @param type New type of this coal
         */
        public void setType(CoalType type)
        {
            setData((byte)type);
        }

        public override string ToString()
        {
            return getType() + " " + base.ToString();
        }

        public override Coal clone()
        {
            return (Coal)base.clone();
        }
    }
}
