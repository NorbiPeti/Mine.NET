using Mine.NET.material;

namespace Mine.NET.entity
{
    /**
     * Represents a Sheep.
     */
    public interface Sheep : Animals, Colorable
    {

        /**
         * @return Whether the sheep is sheared.
         */
        bool isSheared();

        /**
         * @param flag Whether to shear the sheep
         */
        void setSheared(bool flag);
    }
}
