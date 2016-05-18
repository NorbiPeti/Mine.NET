namespace Mine.NET.projectiles{

public interface BlockProjectileSource : ProjectileSource {

    /**
     * Gets the block this projectile source belongs to.
     *
     * @return Block for the projectile source
     */
    public Block getBlock();
}
