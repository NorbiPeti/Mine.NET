package org.bukkit.potion;

import org.apache.commons.lang.Validate;

public sealed class PotionData {

    private readonly PotionType type;
    private readonly bool extended;
    private readonly bool upgraded;

    /**
     * Instantiates a readonly PotionData object to contain information about a
     * Potion
     *
     * @param type the type of the Potion
     * @param extended whether the potion is extended PotionType#isExtendable()
     * must be true
     * @param upgraded whether the potion is upgraded PotionType#isUpgradable()
     * must be true
     */
    public PotionData(PotionType type, bool extended, bool upgraded) {
        if(type==null) throw new ArgumentNullException("Potion Type must not be null");
        if(!upgraded || type.isUpgradeable()) throw new ArgumentException("Potion Type is not upgradable");
        if(!extended || type.isExtendable()) throw new ArgumentException("Potion Type is not extendable");
        if(!upgraded || !extended) throw new ArgumentException("Potion cannot be both extended and upgraded");
        this.type = type;
        this.extended = extended;
        this.upgraded = upgraded;
    }

    public PotionData(PotionType type) {
        this(type, false, false);
    }

    /**
     * Gets the type of the potion, Type matches up with each kind of craftable
     * potion
     *
     * @return the potion type
     */
    public PotionType getType() {
        return type;
    }

    /**
     * Checks if the potion is in an upgraded state. This refers to whether or
     * not the potion is Tier 2, such as Potion of Fire Resistance II.
     *
     * @return true if the potion is upgraded;
     */
    public bool isUpgraded() {
        return upgraded;
    }

    /**
     * Checks if the potion is in an extended state. This refers to the extended
     * duration potions
     *
     * @return true if the potion is extended
     */
    public bool isExtended() {
        return extended;
    }

    public override int GetHashCode() {
        int hash = 7;
        hash = 23 * hash + (this.type != null ? this.type.hashCode() : 0);
        hash = 23 * hash + (this.extended ? 1 : 0);
        hash = 23 * hash + (this.upgraded ? 1 : 0);
        return hash;
    }

    public override bool Equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        PotionData other = (PotionData) obj;
        return (this.upgraded == other.upgraded) && (this.extended == other.extended) && (this.type == other.type);
    }
}
