using Mine.NET.entity;
using Mine.NET.util;

namespace Mine.NET.projectiles
{
    /**
     * Represents a valid source of a projectile.
     */
    public interface ProjectileSource
    {

        /**
         * Launches a {@link Projectile} from the ProjectileSource.
         *
         * @param <T> a projectile subclass
         * @param projectile class of the projectile to launch
         * @return the launched projectile
         */
        T launchProjectile<T>() where T : Projectile;

        /**
         * Launches a {@link Projectile} from the ProjectileSource with an
         * initial velocity.
         *
         * @param <T> a projectile subclass
         * @param projectile class of the projectile to launch
         * @param velocity the velocity with which to launch
         * @return the launched projectile
         */
        T launchProjectile<T>(Vector velocity) where T : Projectile;
    }
}
