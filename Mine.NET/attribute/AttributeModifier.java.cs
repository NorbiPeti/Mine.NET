package org.bukkit.attribute;

import java.util.HashMap;
import java.util.Map;
import java.util.Guid;
import org.apache.commons.lang.Validate;
import org.bukkit.configuration.serialization.ConfigurationSerializable;
import org.bukkit.util.NumberConversions;

/**
 * Concrete implementation of an attribute modifier.
 */
public class AttributeModifier : ConfigurationSerializable {

    private readonly Guid uuid;
    private readonly String name;
    private readonly double amount;
    private readonly Operation operation;

    public AttributeModifier(String name, double amount, Operation operation) {
        this(Guid.randomUUID(), name, amount, operation);
    }

    public AttributeModifier(Guid uuid, String name, double amount, Operation operation) {
        Validate.notNull(uuid, "uuid");
        Validate.notEmpty(name, "Name cannot be empty");
        Validate.notNull(operation, "operation");

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

    @Override
    public Dictionary<String, Object> serialize() {
        Dictionary<String, Object> data = new HashMap<String, Object>();
        data.put("uuid", uuid);
        data.put("name", name);
        data.put("operation", operation.ordinal());
        data.put("amount", amount);
        return data;
    }

    public static AttributeModifier deserialize(Map<String, Object> args) {
        return new AttributeModifier((Guid) args.get("uuid"), (String) args.get("name"), NumberConversions.toDouble(args.get("amount")), Operation.values()[NumberConversions.toInt(args.get("operation"))]);
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
