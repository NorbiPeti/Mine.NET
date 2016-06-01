using Mine.NET.block;

namespace Mine.NET.projectiles
{
    public interface BlockProjectileSource : ProjectileSource
    {

        /**
         * Gets the block this projectile source belongs to.
         *
         * @return Block for the projectile source
         */
        Block getBlock();
    }
}
