namespace Mine.NET.material;

import org.bukkit.GrassSpecies;
import org.bukkit.Materials;

/**
 * Represents the different types of long grasses.
 */
public class LongGrass : MaterialData {
    public LongGrass() : base(Materials.LONG_GRASS) {
    }

    public LongGrass(GrassSpecies species) {
        this();
        setSpecies(species);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public LongGrass(int type) : base(type) {
    }

    public LongGrass(Materials type) : base(type) {
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public LongGrass(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public LongGrass(Materials type, readonly byte data) : base(type, data) {
    }

    /**
     * Gets the current species of this grass
     *
     * @return GrassSpecies of this grass
     */
    public GrassSpecies getSpecies() {
        return GrassSpecies.getByData(getData());
    }

    /**
     * Sets the species of this grass
     *
     * @param species New species of this grass
     */
    public void setSpecies(GrassSpecies species) {
        setData(species.getData());
    }

    public override string ToString() {
        return getSpecies() + " " + base.ToString();
    }

    public override LongGrass clone() {
        return (LongGrass) base.clone();
    }
}
