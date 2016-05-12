namespace Mine.NET.entity;

public interface ShulkerBullet : Projectile {

    /**
     * Retrieve the target of this bullet.
     *
     * @return the targeted entity
     */
    Entity getTarget();

    /**
     * Sets the target of this bullet
     *
     * @param target the entity to target
     */
    void setTarget(Entity target);
}
