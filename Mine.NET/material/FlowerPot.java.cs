namespace Mine.NET.material{

import org.bukkit.GrassSpecies;
import org.bukkit.Materials;
import org.bukkit.TreeSpecies;

/**
 * Represents a flower pot.
 */
public class FlowerPot : MaterialData {

    /**
     * Default constructor for a flower pot.
     */
    public FlowerPot() : base(Materials.FLOWER_POT) {
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public FlowerPot(int type) : base(type) {
    }

    public FlowerPot(Materials type) : base(type) {
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public FlowerPot(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public FlowerPot(Materials type, readonly byte data) : base(type, data) {
    }

    /**
     * Get the Materials in the flower pot
     *
     * @return Materials MaterialData for the block currently in the flower pot
     *     or null if empty
     */
    public MaterialData getContents() {
        switch (getData()) {
            case 1:
                return new MaterialData(Materials.RED_ROSE);
            case 2:
                return new MaterialData(Materials.YELLOW_FLOWER);
            case 3:
                return new Tree(TreeSpecies.GENERIC);
            case 4:
                return new Tree(TreeSpecies.REDWOOD);
            case 5:
                return new Tree(TreeSpecies.BIRCH);
            case 6:
                return new Tree(TreeSpecies.JUNGLE);
            case 7:
                return new MaterialData(Materials.RED_MUSHROOM);
            case 8:
                return new MaterialData(Materials.BROWN_MUSHROOM);
            case 9:
                return new MaterialData(Materials.CACTUS);
            case 10:
                return new MaterialData(Materials.DEAD_BUSH);
            case 11:
                return new LongGrass(GrassSpecies.FERN_LIKE);
            default:
                return null;
        }
    }

    /**
     * Set the contents of the flower pot
     *
     * @param materialData MaterialData of the block to put in the flower pot.
     */
    public void setContents(MaterialData materialData) {
        Materials mat = materialData.getItemType();

        if (mat == Materials.RED_ROSE) {
            setData((byte) 1);
        } else if (mat == Materials.YELLOW_FLOWER) {
            setData((byte) 2);
        } else if (mat == Materials.RED_MUSHROOM) {
            setData((byte) 7);
        } else if (mat == Materials.BROWN_MUSHROOM) {
            setData((byte) 8);
        } else if (mat == Materials.CACTUS) {
            setData((byte) 9);
        } else if (mat == Materials.DEAD_BUSH) {
            setData((byte) 10);
        } else if (mat == Materials.SAPLING) {
            TreeSpecies species = ((Tree) materialData).getSpecies();

            if (species == TreeSpecies.GENERIC) {
                setData((byte) 3);
            } else if (species == TreeSpecies.REDWOOD) {
                setData((byte) 4);
            } else if (species == TreeSpecies.BIRCH) {
                setData((byte) 5);
            } else {
                setData((byte) 6);
            }
        } else if (mat == Materials.LONG_GRASS) {
            GrassSpecies species = ((LongGrass) materialData).getSpecies();

            if (species == GrassSpecies.FERN_LIKE) {
                setData((byte) 11);
            }
        }
    }

    public override string ToString() {
        return base.ToString() + " containing " + getContents();
    }

    public override FlowerPot clone() {
        return (FlowerPot) base.clone();
    }
}}
