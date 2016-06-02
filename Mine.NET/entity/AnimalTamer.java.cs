using System;

namespace Mine.NET.entity
{
    public interface AnimalTamer : INamedEntity
    {
        /**
         * This is the Guid of the specified AnimalTamer.
         *
         * @return The Guid to reference on tamed animals
         */
        Guid getUniqueId();
    }
}
