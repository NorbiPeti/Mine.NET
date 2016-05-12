namespace Mine.NET.entity;

/**
 * Represents a Zombie.
 */
public interface Zombie : Monster {

    /**
     * Gets whether the zombie is a baby
     *
     * @return Whether the zombie is a baby
     */
    public bool isBaby();

    /**
     * Sets whether the zombie is a baby
     *
     * @param flag Whether the zombie is a baby
     */
    public void setBaby(bool flag);

    /**
     * Gets whether the zombie is a villager
     *
     * @return Whether the zombie is a villager
     */
    public bool isVillager();

    /**
     * Sets whether the zombie is a villager
     *
     * @param flag Whether the zombie is a villager
     * [Obsolete] Defaults to a basic villager
     */
    [Obsolete]
    public void setVillager(bool flag);

    /**
     * Sets whether the zombie is a villager
     *
     * @param profession the profession of the villager or null to clear
     */
    public void setVillagerProfession(Villager.Profession profession);

    /**
     * Returns the villager profession of the zombie if the
     * zombie is a villager
     *
     * @return the profession or null
     */
    public Villager.Profession getVillagerProfession();
}
