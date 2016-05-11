package org.bukkit.entity;

import org.bukkit.material.Colorable;

/**
 * Represents a Sheep.
 */
public interface Sheep : Animals, Colorable {

    /**
     * @return Whether the sheep is sheared.
     */
    public bool isSheared();

    /**
     * @param flag Whether to shear the sheep
     */
    public void setSheared(bool flag);
}
