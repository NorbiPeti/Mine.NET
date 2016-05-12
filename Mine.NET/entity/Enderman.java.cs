using Mine.NET.material;

namespace Mine.NET.entity
{
    /**
     * Represents an Enderman.
     */
    public interface Enderman : Monster
    {

        /**
         * Get the id and data of the block that the Enderman is carrying.
         *
         * @return MaterialData containing the id and data of the block
         */
        MaterialData getCarriedMaterial();

        /**
         * Set the id and data of the block that the Enderman is carring.
         *
         * @param material data to set the carried block to
         */
        void setCarriedMaterial(MaterialData material);
    }
}
