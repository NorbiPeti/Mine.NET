namespace Mine.NET.material
{
    /**
     * Represents the different types of coals.
     */
    public class Coal : MaterialData
    {
        private CoalType type;

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
            return type;
        }

        /**
         * Sets the type of this coal
         *
         * @param type New type of this coal
         */
        public void setType(CoalType type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return getType() + " " + base.ToString();
        }

        public new Coal Clone() { return (Coal)base.Clone(); }
    }
}
