namespace Mine.NET.entity
{
    /**
     * Represents an entity that can age and breed.
     */
    public interface Ageable : Creature {
        /**
         * Gets the age of this animal.
         *
         * @return Age
         */
        int getAge();

        /**
         * Sets the age of this animal.
         *
         * @param age New age
         */
        void setAge(int age);

        /**
         * Lock the age of the animal, setting this will prevent the animal from
         * maturing or getting ready for mating.
         *
         * @param lock new lock
         */
        void setAgeLock(bool lock);

        /**
         * Gets the current agelock.
         *
         * @return the current agelock
         */
        bool getAgeLock();

        /**
         * Sets the age of the animal to a baby
         */
        void setBaby();

        /**
         * Sets the age of the animal to an adult
         */
        void setAdult();

        /**
         * Returns true if the animal is an adult.
         *
         * @return return true if the animal is an adult
         */
        bool isAdult();

        /**
         * Return the ability to breed of the animal.
         *
         * @return the ability to breed of the animal
         */
        bool canBreed();

        /**
         * Set breedability of the animal, if the animal is a baby and set to
         * breed it will instantly grow up.
         *
         * @param breed breedability of the animal
         */
        void setBreed(bool breed);
    }
}
