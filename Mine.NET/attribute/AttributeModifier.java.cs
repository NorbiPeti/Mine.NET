using System;
using System.Collections.Generic;

namespace Mine.NET.attribute
{
    /**
     * Concrete implementation of an attribute modifier.
     */
    public class AttributeModifier : ConfigurationSerializable {

        private readonly Guid uuid;
        private readonly String name;
        private readonly double amount;
        private readonly Operation operation;

        public AttributeModifier(String name, double amount, Operation operation) {
            new AttributeModifier(Guid.NewGuid(), name, amount, operation);
        }

        public AttributeModifier(Guid uuid, String name, double amount, Operation operation) {
            if (uuid == null) throw new ArgumentNullException("uuid");
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be empty");

            this.uuid = uuid;
            this.name = name;
            this.amount = amount;
            this.operation = operation;
        }

        /**
         * Get the unique ID for this modifier.
         *
         * @return unique id
         */
        public Guid getUniqueId() {
            return uuid;
        }

        /**
         * Get the name of this modifier.
         *
         * @return name
         */
        public String getName() {
            return name;
        }

        /**
         * Get the amount by which this modifier will apply its {@link Operation}.
         *
         * @return modification amount
         */
        public double getAmount() {
            return amount;
        }

        /**
         * Get the operation this modifier will apply.
         *
         * @return operation
         */
        public Operation getOperation() {
            return operation;
        }
        
    public override Dictionary<String, Object> serialize() {
            Dictionary<String, Object> data = new Dictionary<string, object>();
            data.Add("uuid", uuid);
            data.Add("name", name);
            data.Add("operation", (int)operation));
            data.Add("amount", amount);
            return data;
        }

        public static AttributeModifier deserialize(Dictionary<String, Object> args) {
            return new AttributeModifier((Guid)args["uuid"], (String)args["name"], NumberConversions.toDouble(args["amount"]), Enum.GetValues(typeof(Operation))[Convert.ToInt32(args["operation"])]);
        }

        /**
         * Enumerable operation to be applied.
         */
        public enum Operation {

            /**
             * Adds (or subtracts) the specified amount to the base value.
             */
            ADD_NUMBER,
            /**
             * Adds this scalar of amount to the base value.
             */
            ADD_SCALAR,
            /**
             * Multiply amount by this value, after adding 1 to it.
             */
            MULTIPLY_SCALAR_1;
        }
    }
}
