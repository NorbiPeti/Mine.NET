using System.Collections.Generic;

namespace Mine.NET.entity
{
    /**
     * Represents a complex living entity - one that is made up of various smaller
     * parts
     */
    public interface ComplexLivingEntity : LivingEntity
    {
        /**
         * Gets a list of parts that belong to this complex entity
         *
         * @return List of parts
         */
        HashSet<ComplexEntityPart> getParts();
    }
}
