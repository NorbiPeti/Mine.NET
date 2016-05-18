namespace Mine.NET.material;

/**
 * Indicated a Materials that may carry or create a Redstone current
 */
public interface Redstone {

    /**
     * Gets the current state of this Materials, indicating if it's powered or
     * unpowered
     *
     * @return true if powered, otherwise false
     */
    public bool isPowered();
}
