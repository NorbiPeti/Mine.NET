using Mine.NET.util;

namespace Mine.NET.entity
{
    /**
     * Represents a Fireball.
     */
    public interface Fireball : Projectile, Explosive
    {

        /**
         * Fireballs fly straight and do not take setVelocity(...) well.
         *
         * @param direction the direction this fireball is flying toward
         */
        void setDirection(Vector direction);

        /**
         * Retrieve the direction this fireball is heading toward
         *
         * @return the direction
         */
        Vector getDirection();
    }
}
