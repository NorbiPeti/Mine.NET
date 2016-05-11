package org.bukkit.event.entity;

import java.util.Map;

import com.google.common.base.Function;
import org.bukkit.entity.Entity;

/**
 * Called when an entity is damaged by an entity
 */
public class EntityDamageByEntityEvent : EntityDamageEvent {
    private readonly Entity damager;

    [Obsolete]
    public EntityDamageByEntityEvent(Entity damager, readonly Entity damagee, readonly DamageCause cause, readonly int damage) {
        this(damager, damagee, cause, (double) damage);
    }

    [Obsolete]
    public EntityDamageByEntityEvent(Entity damager, readonly Entity damagee, readonly DamageCause cause, readonly double damage) {
        super(damagee, cause, damage);
        this.damager = damager;
    }

    public EntityDamageByEntityEvent(Entity damager, readonly Entity damagee, readonly DamageCause cause, readonly Dictionary<DamageModifier, Double> modifiers, readonly Dictionary<DamageModifier, ? : Function<? super Double, Double>> modifierFunctions) {
        super(damagee, cause, modifiers, modifierFunctions);
        this.damager = damager;
    }

    /**
     * Returns the entity that damaged the defender.
     *
     * @return Entity that damaged the defender.
     */
    public Entity getDamager() {
        return damager;
    }
}
