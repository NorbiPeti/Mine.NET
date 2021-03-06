namespace Mine.NET.permissions
{
    /**
     * Represents an object that may become a server operator, such as a {@link
     * Player}
     */
    public interface ServerOperator
    {

        /**
         * Checks if this object is a server operator
         *
         * @return true if this is an operator, otherwise false
         */
        bool isOp();

        /**
         * Sets the operator status of this object
         *
         * @param value New operator value
         */
        void setOp(bool value);
    }
}
