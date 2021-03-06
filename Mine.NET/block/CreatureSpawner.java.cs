using Mine.NET.entity;
using System;

namespace Mine.NET.block
{
    /**
     * Represents a creature spawner.
     */
    public interface CreatureSpawner : BlockState
    {

        /**
         * Get the spawner's creature type.
         *
         * @return The creature type.
         */
        EntityType getSpawnedType();

        /**
         * Set the spawner's creature type.
         *
         * @param creatureType The creature type.
         */
        void setSpawnedType(EntityType creatureType);

        /**
         * Set the spawner mob type.
         *
         * @param creatureType The creature type's name.
         */
        void setCreatureTypeByName(String creatureType);

        /**
         * Get the spawner's creature type.
         *
         * @return The creature type's name.
         */
        String getCreatureTypeName();

        /**
         * Get the spawner's delay.
         *
         * @return The delay.
         */
        int getDelay();

        /**
         * Set the spawner's delay.
         *
         * @param delay The delay.
         */
        void setDelay(int delay);
    }
}
