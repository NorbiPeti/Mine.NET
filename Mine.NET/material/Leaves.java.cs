namespace Mine.NET.material;

import org.bukkit.Materials;
import org.bukkit.TreeSpecies;

/**
 * Represents the different types of leaf block that may be permanent or can
 * decay when too far from a log.
 *
 * @see Materials#LEAVES
 * @see Materials#LEAVES_2
 */
public class Leaves : Wood {
    protected static readonly Materials DEFAULT_TYPE = Materials.LEAVES;
    protected static readonly bool DEFAULT_DECAYABLE = true;

    /**
     * Constructs a leaf block.
     */
    public Leaves() {
        this(DEFAULT_TYPE, DEFAULT_SPECIES, DEFAULT_DECAYABLE);
    }

    /**
     * Constructs a leaf block of the given tree species.
     *
     * @param species the species of the wood block
     */
    public Leaves(TreeSpecies species) {
        this(DEFAULT_TYPE, species, DEFAULT_DECAYABLE);
    }

    /**
     * Constructs a leaf block of the given tree species and flag for whether
     * this leaf block will disappear when too far from a log.
     *
     * @param species the species of the wood block
     * @param isDecayable whether the block is permanent or can disappear
     */
    public Leaves(TreeSpecies species, bool isDecayable) {
        this(DEFAULT_TYPE, species, isDecayable);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Leaves(int type) {
        base(type);
    }

    /**
     * Constructs a leaf block of the given type.
     *
     * @param type the type of leaf block
     */
    public Leaves(Materials type) {
        this(type, DEFAULT_SPECIES, DEFAULT_DECAYABLE);
    }

    /**
     * Constructs a leaf block of the given type and tree species.
     *
     * @param type the type of leaf block
     * @param species the species of the wood block
     */
    public Leaves(Materials type, TreeSpecies species) {
        this(type, species, DEFAULT_DECAYABLE);
    }

    /**
     * Constructs a leaf block of the given type and tree species and flag for
     * whether this leaf block will disappear when too far from a log.
     *
     * @param type the type of leaf block
     * @param species the species of the wood block
     * @param isDecayable whether the block is permanent or can disappear
     */
    public Leaves(Materials type, TreeSpecies species, bool isDecayable) {
        base(type, species);
        setDecayable(isDecayable);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Leaves(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Leaves(Materials type, readonly byte data) {
        base(type, data);
    }

    /**
     * Checks if this leaf block is in the process of decaying
     *
     * @return true if the leaf block is in the process of decaying
     */
    public bool isDecaying() {
        return (getData() & 0x8) != 0;
    }

    /**
     * Set whether this leaf block is in the process of decaying
     *
     * @param isDecaying whether the block is decaying or not
     */
    public void setDecaying(bool isDecaying) {
        setData((byte) ((getData() & 0x3) | (isDecaying
                ? 0x8 // Clear the permanent flag to make this a decayable flag and set the decaying flag
                : (getData() & 0x4)))); // Only persist the decayable flag if this is not a decaying block
    }

    /**
     * Checks if this leaf block is permanent or can decay when too far from a
     * log
     *
     * @return true if the leaf block is permanent or can decay when too far
     * from a log
     */
    public bool isDecayable() {
        return (getData() & 0x4) == 0;
    }

    /**
     * Set whether this leaf block will disappear when too far from a log
     *
     * @param isDecayable whether the block is permanent or can disappear
     */
    public void setDecayable(bool isDecayable) {
        setData((byte) ((getData() & 0x3) | (isDecayable
                ? (getData() & 0x8) // Only persist the decaying flag if this is a decayable block
                : 0x4)));
    }

    public override string ToString() {
        return getSpecies() + (isDecayable() ? " DECAYABLE " : " PERMANENT ") + (isDecaying() ? " DECAYING " : " ") + base.ToString();
    }

    public override Leaves clone() {
        return (Leaves) base.clone();
    }
}
