namespace Mine.NET.entity;

/**
 * Represents a Creeper
 */
public interface Creeper : Monster {

    /**
     * Checks if this Creeper is powered (Electrocuted)
     *
     * @return true if this creeper is powered
     */
    public bool isPowered();

    /**
     * Sets the Powered status of this Creeper
     *
     * @param value New Powered status
     */
    public void setPowered(bool value);
}
