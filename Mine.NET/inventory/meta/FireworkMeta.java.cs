package org.bukkit.inventory.meta;

import java.util.List;

import org.bukkit.FireworkEffect;
import org.bukkit.Material;

/**
 * Represents a {@link Material#FIREWORK} and its effects.
 */
public interface FireworkMeta : ItemMeta {

    /**
     * Add another effect to this firework.
     *
     * @param effect The firework effect to add
     * @throws ArgumentException If effect is null
     */
    void addEffect(FireworkEffect effect) throws ArgumentException;

    /**
     * Add several effects to this firework.
     *
     * @param effects The firework effects to add
     * @throws ArgumentException If effects is null
     * @throws ArgumentException If any effect is null (may be thrown
     *     after changes have occurred)
     */
    void addEffects(FireworkEffect...effects) throws ArgumentException;

    /**
     * Add several firework effects to this firework.
     *
     * @param effects An iterable object whose iterator yields the desired
     *     firework effects
     * @throws ArgumentException If effects is null
     * @throws ArgumentException If any effect is null (may be thrown
     *     after changes have occurred)
     */
    void addEffects(Iterable<FireworkEffect> effects) throws ArgumentException;

    /**
     * Get the effects in this firework.
     *
     * @return An immutable list of the firework effects
     */
    List<FireworkEffect> getEffects();

    /**
     * Get the number of effects in this firework.
     *
     * @return The number of effects
     */
    int getEffectsSize();

    /**
     * Remove an effect from this firework.
     *
     * @param index The index of the effect to remove
     * @throws IndexOutOfBoundsException If index {@literal < 0 or index >} {@link
     *     #getEffectsSize()}
     */
    void removeEffect(int index) throws IndexOutOfBoundsException;

    /**
     * Remove all effects from this firework.
     */
    void clearEffects();

    /**
     * Get whether this firework has any effects.
     *
     * @return true if it has effects, false if there are no effects
     */
    bool hasEffects();

    /**
     * Gets the approximate height the firework will fly.
     *
     * @return approximate flight height of the firework.
     */
    int getPower();

    /**
     * Sets the approximate power of the firework. Each level of power is half
     * a second of flight time.
     *
     * @param power the power of the firework, from 0-128
     * @throws ArgumentException if {@literal height<0 or height>128}
     */
    void setPower(int power) throws ArgumentException;

    FireworkMeta clone();
}
