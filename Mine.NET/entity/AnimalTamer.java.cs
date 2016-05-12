using System;

namespace Mine.NET.entity
{
    public interface AnimalTamer
    {

        /**
         * This is the name of the specified AnimalTamer.
         *
         * @return The name to reference on tamed animals or null if a name cannot be obtained
         */
        String getName();

        /**
         * This is the Guid of the specified AnimalTamer.
         *
         * @return The Guid to reference on tamed animals
         */
        Guid getUniqueId();
    }
}
