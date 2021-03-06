using Mine.NET.inventory.meta;

namespace Mine.NET.entity
{
    public interface Firework : Entity
    {

        /**
         * Get a copy of the fireworks meta
         *
         * @return A copy of the current Firework meta
         */
        FireworkMeta getFireworkMeta();

        /**
         * Apply the provided meta to the fireworks
         *
         * @param meta The FireworkMeta to apply
         */
        void setFireworkMeta(FireworkMeta meta);

        /**
         * Cause this firework to explode at earliest opportunity, as if it has no
         * remaining fuse.
         */
        void detonate();
    }
}
