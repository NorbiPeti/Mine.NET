namespace Mine.NET.entity;

public interface Guardian : Monster {

    /**
     * Check if the Guardian is an elder Guardian
     * 
     * @return true if the Guardian is an Elder Guardian, false if not
     */
    public bool isElder();

    /**
     * Set the Guardian to an elder Guardian or not
     *
     * @param shouldBeElder True if this Guardian should be a elder Guardian, false if not
     */
    public void setElder(bool shouldBeElder);
}
