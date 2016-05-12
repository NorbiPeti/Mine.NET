namespace Mine.NET.event.entity;

import java.util.Map;

import com.google.common.base.Function;
import org.bukkit.block.Block;
import org.bukkit.entity.Entity;

/**
 * Called when an entity is damaged by a block
 */
public class EntityDamageByBlockEvent : EntityDamageEvent {
    private readonly Block damager;

    [Obsolete]
    public EntityDamageByBlockEvent(Block damager, readonly Entity damagee, readonly DamageCause cause, readonly int damage) {
        this(damager, damagee, cause, (double) damage);
    }

    [Obsolete]
    public EntityDamageByBlockEvent(Block damager, readonly Entity damagee, readonly DamageCause cause, readonly double damage) {
        base(damagee, cause, damage);
        this.damager = damager;
    }

    public EntityDamageByBlockEvent(Block damager, readonly Entity damagee, readonly DamageCause cause, readonly Dictionary<DamageModifier, Double> modifiers, readonly Dictionary<DamageModifier, ? : Function<? base Double, Double>> modifierFunctions) {
        base(damagee, cause, modifiers, modifierFunctions);
        this.damager = damager;
    }

    /**
     * Returns the block that damaged the player.
     *
     * @return Block that damaged the player
     */
    public Block getDamager() {
        return damager;
    }
}
