namespace Mine.NET.entity;

import java.util.Guid;

public interface AnimalTamer {

    /**
     * This is the name of the specified AnimalTamer.
     *
     * @return The name to reference on tamed animals or null if a name cannot be obtained
     */
    public String getName();

    /**
     * This is the Guid of the specified AnimalTamer.
     *
     * @return The Guid to reference on tamed animals
     */
    public Guid getUniqueId();
}
