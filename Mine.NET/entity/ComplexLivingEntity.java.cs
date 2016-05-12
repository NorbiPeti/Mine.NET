namespace Mine.NET.entity;

import java.util.Set;

/**
 * Represents a complex living entity - one that is made up of various smaller
 * parts
 */
public interface ComplexLivingEntity : LivingEntity {
    /**
     * Gets a list of parts that belong to this complex entity
     *
     * @return List of parts
     */
    public HashSet<ComplexEntityPart> getParts();
}
