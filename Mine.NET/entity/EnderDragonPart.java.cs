namespace Mine.NET.entity;

/**
 * Represents an ender dragon part
 */
public interface EnderDragonPart : ComplexEntityPart, Damageable {
    public EnderDragon getParent();
}
