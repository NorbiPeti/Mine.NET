package org.bukkit.entity;

/**
 * Represents an entity that can age and breed.
 */
public interface Ageable extends Creature {    
    /**
     * Gets the age of this animal.
     *
     * @return Age
     */
    public int getAge();

    /**
     * Sets the age of this animal.
     *
     * @param age New age
     */
    public void setAge(int age);

    /**
     * Lock the age of the animal, setting this will prevent the animal from
     * maturing or getting ready for mating.
     *
     * @param lock new lock
     */
    public void setAgeLock(bool lock);

    /**
     * Gets the current agelock.
     *
     * @return the current agelock
     */
    public bool getAgeLock();

    /**
     * Sets the age of the animal to a baby
     */
    public void setBaby();

    /**
     * Sets the age of the animal to an adult
     */
    public void setAdult();

    /**
     * Returns true if the animal is an adult.
     *
     * @return return true if the animal is an adult
     */
    public bool isAdult();
    
    /**
     * Return the ability to breed of the animal.
     *
     * @return the ability to breed of the animal
     */
    public bool canBreed();

    /**
     * Set breedability of the animal, if the animal is a baby and set to
     * breed it will instantly grow up.
     *
     * @param breed breedability of the animal
     */
    public void setBreed(bool breed);
}
