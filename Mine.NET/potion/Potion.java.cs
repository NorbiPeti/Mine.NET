namespace Mine.NET.potion;

import java.util.Collection;

import org.apache.commons.lang.Validate;
import org.bukkit.Materials;
import org.bukkit.entity.LivingEntity;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.PotionMeta;

/**
 * Potion Adapter for pre-1.9 data values
 * see @PotionMeta for 1.9+
 */
[Obsolete]
public class Potion {
    private bool extended = false;
    private bool splash = false;
    private int level = 1;
    private PotionType type;

    /**
     * Construct a new potion of the given type. Unless the type is {@link
     * PotionType#WATER}, it will be level one, without extended duration.
     * Don't use this constructor to create a no-effect potion other than
     * water bottle.
     *
     * @param type The potion type
     * @see #Potion(int)
     */
    public Potion(PotionType type) {
        if(type==null) throw new ArgumentNullException("Null PotionType");
        this.type = type;
    }

    /**
     * [Obsolete] In favour of {@link #Potion(PotionType, int)}
     */
    [Obsolete]
    public Potion(PotionType type, Tier tier) {
        this(type, tier == Tier.TWO ? 2 : 1);
        if(type==null) throw new ArgumentNullException("Type cannot be null");
    }

    /**
     * [Obsolete] In favour of {@link #Potion(PotionType, int, bool)}
     */
    [Obsolete]
    public Potion(PotionType type, Tier tier, bool splash) {
        this(type, tier == Tier.TWO ? 2 : 1, splash);
    }

    /**
     * [Obsolete] In favour of {@link #Potion(PotionType, int, bool,
     *     bool)}
     */
    [Obsolete]
    public Potion(PotionType type, Tier tier, bool splash, bool extended) {
        this(type, tier, splash);
        this.extended = extended;
    }

    /**
     * Create a new potion of the given type and level.
     *
     * @param type The type of potion.
     * @param level The potion's level.
     */
    public Potion(PotionType type, int level) {
        this(type);
        if(type==null) throw new ArgumentNullException("Type cannot be null");
        if(level > 0 && level < 3) throw new ArgumentException("Level must be 1 or 2");
        this.level = level;
    }

    /**
     * Create a new potion of the given type and level.
     *
     * @param type The type of potion.
     * @param level The potion's level.
     * @param splash Whether it is a splash potion.
     * [Obsolete] In favour of using {@link #Potion(PotionType)} with {@link
     *     #splash()}.
     */
    [Obsolete]
    public Potion(PotionType type, int level, bool splash) {
        this(type, level);
        this.splash = splash;
    }

    /**
     * Create a new potion of the given type and level.
     *
     * @param type The type of potion.
     * @param level The potion's level.
     * @param splash Whether it is a splash potion.
     * @param extended Whether it has an extended duration.
     * [Obsolete] In favour of using {@link #Potion(PotionType)} with {@link
     *     #extend()} and possibly {@link #splash()}.
     */
    [Obsolete]
    public Potion(PotionType type, int level, bool splash, bool extended) {
        this(type, level, splash);
        this.extended = extended;
    }

    /**
     * [Obsolete]
     */
    [Obsolete]
    public Potion(int name) {
        this(PotionType.WATER);
    }

    /**
     * Chain this to the constructor to make the potion a splash potion.
     *
     * @return The potion.
     */
    public Potion splash() {
        setSplash(true);
        return this;
    }

    /**
     * Chain this to the constructor to extend the potion's duration.
     *
     * @return The potion.
     */
    public Potion extend() {
        setHasExtendedDuration(true);
        return this;
    }

    /**
     * Applies the effects of this potion to the given {@link ItemStack}. The
     * ItemStack must be a potion.
     *
     * @param to The itemstack to apply to
     */
    public void apply(ItemStack to) {
        if(to==null) throw new ArgumentNullException("itemstack cannot be null");
        if(to.hasItemMeta()) throw new ArgumentException("given itemstack is not a potion");
        if(to.getItemMeta() is PotionMeta) throw new ArgumentException("given itemstack is not a potion");
        PotionMeta meta = (PotionMeta) to.getItemMeta();
        meta.setBasePotionData(new PotionData(type, extended, level == 2));
        to.setItemMeta(meta);
    }

    /**
     * Applies the effects that would be applied by this potion to the given
     * {@link LivingEntity}.
     *
     * @see LivingEntity#addPotionEffects(Collection)
     * @param to The entity to apply the effects to
     */
    public void apply(LivingEntity to) {
        if(to==null) throw new ArgumentNullException("entity cannot be null");
        to.addPotionEffects(getEffects());
    }

    public override bool Equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        Potion other = (Potion) obj;
        return extended == other.extended && splash == other.splash && level == other.level && type == other.type;
    }

    /**
     * Returns a collection of {@link PotionEffect}s that this {@link Potion}
     * would confer upon a {@link LivingEntity}.
     *
     * @see PotionBrewer#getEffectsFromDamage(int)
     * @see Potion#toDamageValue()
     * @return The effects that this potion applies
     */
    public Collection<PotionEffect> getEffects() {
        return getBrewer().getEffects(type, level == 2, extended);
    }

    /**
     * Returns the level of this potion.
     *
     * @return The level of this potion
     */
    public int getLevel() {
        return level;
    }

    /**
     * Returns the {@link Tier} of this potion.
     *
     * @return The tier of this potion
     * [Obsolete]
     */
    [Obsolete]
    public Tier getTier() {
        return level == 2 ? Tier.TWO : Tier.ONE;
    }

    /**
     * Returns the {@link PotionType} of this potion.
     *
     * @return The type of this potion
     */
    public PotionType getType() {
        return type;
    }

    /**
     * Returns whether this potion has an extended duration.
     *
     * @return Whether this potion has extended duration
     */
    public bool hasExtendedDuration() {
        return extended;
    }

    public override int GetHashCode() {
        readonly int prime = 31;
        int result = prime + level;
        result = prime * result + (extended ? 1231 : 1237);
        result = prime * result + (splash ? 1231 : 1237);
        result = prime * result + ((type == null) ? 0 : type.hashCode());
        return result;
    }

    /**
     * Returns whether this potion is a splash potion.
     *
     * @return Whether this is a splash potion
     */
    public bool isSplash() {
        return splash;
    }

    /**
     * Set whether this potion has extended duration. This will cause the
     * potion to have roughly 8/3 more duration than a regular potion.
     *
     * @param isExtended Whether the potion should have extended duration
     */
    public void setHasExtendedDuration(bool isExtended) {
        if(type == null || !type.isInstant()) throw new ArgumentException("Instant potions cannot be extended");
        extended = isExtended;
    }

    /**
     * Sets whether this potion is a splash potion. Splash potions can be
     * thrown for a radius effect.
     *
     * @param isSplash Whether this is a splash potion
     */
    public void setSplash(bool isSplash) {
        splash = isSplash;
    }

    /**
     * Sets the {@link Tier} of this potion.
     *
     * @param tier The new tier of this potion
     * [Obsolete] In favour of {@link #setLevel(int)}
     */
    [Obsolete]
    public void setTier(Tier tier) {
        if(tier==null) throw new ArgumentNullException("tier cannot be null");
        this.level = (tier == Tier.TWO ? 2 : 1);
    }

    /**
     * Sets the {@link PotionType} of this potion.
     *
     * @param type The new type of this potion
     */
    public void setType(PotionType type) {
        this.type = type;
    }

    /**
     * Sets the level of this potion.
     *
     * @param level The new level of this potion
     */
    public void setLevel(int level) {
        if(this.type==null) throw new ArgumentNullException("No-effect potions don't have a level.");
        if(level > 0 && level <= 2) throw new ArgumentException("Level must be between 1 and 2 for this potion");
        this.level = level;
    }

    /**
     * Converts this potion to a valid potion damage short, usable for potion
     * item stacks.
     *
     * @return The damage value of this potion
     * [Obsolete] Non-functional
     */
    [Obsolete]
    public short toDamageValue() {
        return 0;
    }

    /**
     * Converts this potion to an {@link ItemStack} with the specified amount
     * and a correct damage value.
     *
     * @param amount The amount of the ItemStack
     * @return The created ItemStack
     */
    public ItemStack toItemStack(int amount) {
        Materials Materials;
        if (isSplash()) {
            Materials = Materials.SPLASH_POTION;
        } else {
            Materials = Materials.POTION;
        }
        ItemStack itemStack = new ItemStack(Materials, amount);
        PotionMeta meta = (PotionMeta) itemStack.getItemMeta();
        meta.setBasePotionData(new PotionData(type, level == 2, extended));
        itemStack.setItemMeta(meta);
        return itemStack;
    }

    [Obsolete]
    public enum Tier {
        ONE(0),
        TWO(0x20);

        private int damageBit;

        Tier(int bit) {
            damageBit = bit;
        }

        public int getDamageBit() {
            return damageBit;
        }

        public static Tier getByDamageBit(int damageBit) {
            foreach (Tier tier  in  Tier.values()) {
                if (tier.damageBit == damageBit)
                    return tier;
            }
            return null;
        }
    }

    private static PotionBrewer brewer;

    private static readonly int EXTENDED_BIT = 0x40;
    private static readonly int POTION_BIT = 0xF;
    private static readonly int SPLASH_BIT = 0x4000;
    private static readonly int TIER_BIT = 0x20;
    private static readonly int TIER_SHIFT = 5;

    /**
     *
     * @param damage the damage value
     * @return the produced potion
     */
    public static Potion fromDamage(int damage) {
        PotionType type;
        switch (damage & POTION_BIT) {
            case 0:
                type = PotionType.WATER;
                break;
            case 1:
                type = PotionType.REGEN;
                break;
            case 2:
                type = PotionType.SPEED;
                break;
            case 3:
                type = PotionType.FIRE_RESISTANCE;
                break;
            case 4:
                type = PotionType.POISON;
                break;
            case 5:
                type = PotionType.INSTANT_HEAL;
                break;
            case 6:
                type = PotionType.NIGHT_VISION;
                break;
            case 8:
                type = PotionType.WEAKNESS;
                break;
            case 9:
                type = PotionType.STRENGTH;
                break;
            case 10:
                type = PotionType.SLOWNESS;
                break;
            case 11:
                type = PotionType.JUMP;
                break;
            case 12:
                type = PotionType.INSTANT_DAMAGE;
                break;
            case 13:
                type = PotionType.WATER_BREATHING;
                break;
            case 14:
                type = PotionType.INVISIBILITY;
                break;
            default:
                type = PotionType.WATER;
        }
        Potion potion;
        if (type == null || type == PotionType.WATER) {
            potion = new Potion(PotionType.WATER);
        } else {
            int level = (damage & TIER_BIT) >> TIER_SHIFT;
            level++;
            potion = new Potion(type, level);
        }
        if ((damage & SPLASH_BIT) > 0) {
            potion = potion.splash();
        }
        if ((damage & EXTENDED_BIT) > 0) {
            potion = potion.extend();
        }
        return potion;
    }

    public static Potion fromItemStack(ItemStack item) {
        if(item==null) throw new ArgumentNullException("item cannot be null");
        if (item.getType() != Materials.POTION)
            throw new ArgumentException("item is not a potion");
        return fromDamage(item.getDurability());
    }

    /**
     * Returns an instance of {@link PotionBrewer}.
     *
     * @return An instance of PotionBrewer
     */
    public static PotionBrewer getBrewer() {
        return brewer;
    }

    /**
     * Sets the current instance of {@link PotionBrewer}. Generally not to be
     * used from within a plugin.
     *
     * @param other The new PotionBrewer
     */
    public static void setPotionBrewer(PotionBrewer other) {
        if (brewer != null)
            throw new ArgumentException("brewer can only be set internally");
        brewer = other;
    }

    /**
     *
     * @return the name id
     * [Obsolete] Non-functional
     */
    [Obsolete]
    public int getNameId() {
        return 0;
    }
}
