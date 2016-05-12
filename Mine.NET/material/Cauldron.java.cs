namespace Mine.NET.material;

import org.bukkit.Material;

/**
 * Represents a cauldron
 */
public class Cauldron : MaterialData {
    private static readonly int CAULDRON_FULL = 3;
    private static readonly int CAULDRON_EMPTY = 0;

    public Cauldron() {
        base(Material.CAULDRON);
    }

    /**
     *
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Cauldron(int type, byte data){
        base(type, data);
    }

    /**
     *
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Cauldron(byte data) {
        base(Material.CAULDRON, data);
    }

    /**
     * Check if the cauldron is full.
     *
     * @return True if it is full.
     */
    public bool isFull() {
        return getData() >= CAULDRON_FULL;
    }

    /**
     * Check if the cauldron is empty.
     *
     * @return True if it is empty.
     */
    public bool isEmpty() {
        return getData() <= CAULDRON_EMPTY;
    }

    public override string ToString() {
        return (isEmpty() ? "EMPTY" : (isFull() ? "FULL" : getData() + "/3 FULL")) + " CAULDRON";
    }

    public override Cauldron clone() {
        return (Cauldron) base.clone();
    }
}
