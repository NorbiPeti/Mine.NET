namespace Mine.NET.potion{

/**
 * Represents a type of potion and its effect on an entity.
 */
public abstract class PotionEffectType {
    /**
     * Increases movement speed.
     */
    public static readonly PotionEffectType SPEED = new PotionEffectTypeWrapper(1);

    /**
     * Decreases movement speed.
     */
    public static readonly PotionEffectType SLOW = new PotionEffectTypeWrapper(2);

    /**
     * Increases dig speed.
     */
    public static readonly PotionEffectType FAST_DIGGING = new PotionEffectTypeWrapper(3);

    /**
     * Decreases dig speed.
     */
    public static readonly PotionEffectType SLOW_DIGGING = new PotionEffectTypeWrapper(4);

    /**
     * Increases damage dealt.
     */
    public static readonly PotionEffectType INCREASE_DAMAGE = new PotionEffectTypeWrapper(5);

    /**
     * Heals an entity.
     */
    public static readonly PotionEffectType HEAL = new PotionEffectTypeWrapper(6);

    /**
     * Hurts an entity.
     */
    public static readonly PotionEffectType HARM = new PotionEffectTypeWrapper(7);

    /**
     * Increases jump height.
     */
    public static readonly PotionEffectType JUMP = new PotionEffectTypeWrapper(8);

    /**
     * Warps vision on the client.
     */
    public static readonly PotionEffectType CONFUSION = new PotionEffectTypeWrapper(9);

    /**
     * Regenerates health.
     */
    public static readonly PotionEffectType REGENERATION = new PotionEffectTypeWrapper(10);

    /**
     * Decreases damage dealt to an entity.
     */
    public static readonly PotionEffectType DAMAGE_RESISTANCE = new PotionEffectTypeWrapper(11);

    /**
     * Stops fire damage.
     */
    public static readonly PotionEffectType FIRE_RESISTANCE = new PotionEffectTypeWrapper(12);

    /**
     * Allows breathing underwater.
     */
    public static readonly PotionEffectType WATER_BREATHING = new PotionEffectTypeWrapper(13);

    /**
     * Grants invisibility.
     */
    public static readonly PotionEffectType INVISIBILITY = new PotionEffectTypeWrapper(14);

    /**
     * Blinds an entity.
     */
    public static readonly PotionEffectType BLINDNESS = new PotionEffectTypeWrapper(15);

    /**
     * Allows an entity to see in the dark.
     */
    public static readonly PotionEffectType NIGHT_VISION = new PotionEffectTypeWrapper(16);

    /**
     * Increases hunger.
     */
    public static readonly PotionEffectType HUNGER = new PotionEffectTypeWrapper(17);

    /**
     * Decreases damage dealt by an entity.
     */
    public static readonly PotionEffectType WEAKNESS = new PotionEffectTypeWrapper(18);

    /**
     * Deals damage to an entity over time.
     */
    public static readonly PotionEffectType POISON = new PotionEffectTypeWrapper(19);

    /**
     * Deals damage to an entity over time and gives the health to the
     * shooter.
     */
    public static readonly PotionEffectType WITHER = new PotionEffectTypeWrapper(20);

    /**
     * Increases the maximum health of an entity.
     */
    public static readonly PotionEffectType HEALTH_BOOST = new PotionEffectTypeWrapper(21);

    /**
     * Increases the maximum health of an entity with health that cannot be
     * regenerated, but is refilled every 30 seconds.
     */
    public static readonly PotionEffectType ABSORPTION = new PotionEffectTypeWrapper(22);

    /**
     * Increases the food level of an entity each tick.
     */
    public static readonly PotionEffectType SATURATION = new PotionEffectTypeWrapper(23);

    /**
     * Outlines the entity so that it can be seen from afar.
     */
    public static readonly PotionEffectType GLOWING = new PotionEffectTypeWrapper(24);

    /**
     * Causes the entity to float into the air.
     */
    public static readonly PotionEffectType LEVITATION = new PotionEffectTypeWrapper(25);

    /**
     * Loot table luck.
     */
    public static readonly PotionEffectType LUCK = new PotionEffectTypeWrapper(26);

    /**
     * Loot table unluck.
     */
    public static readonly PotionEffectType UNLUCK = new PotionEffectTypeWrapper(27);

    private readonly int id;

    protected PotionEffectType(int id) {
        this.id = id;
    }

    /**
     * Creates a PotionEffect from this PotionEffectType, applying duration
     * modifiers and checks.
     *
     * @see PotionBrewer#createEffect(PotionEffectType, int, int)
     * @param duration time in ticks
     * @param amplifier the effect's amplifier
     * @return a resulting potion effect
     */
    public PotionEffect createEffect(int duration, int amplifier) {
        return new PotionEffect(this, isInstant() ? 1 : (int) (duration * getDurationModifier()), amplifier);
    }

    /**
     * Returns the duration modifier applied to effects of this type.
     *
     * @return duration modifier
     */
    public abstract double getDurationModifier();

    /**
     * Returns the unique ID of this type.
     *
     * @return Unique ID
     * [Obsolete] Magic value
     */
    [Obsolete]
    public int getId() {
        return id;
    }

    /**
     * Returns the name of this effect type.
     *
     * @return The name of this effect type
     */
    public abstract String getName();

    /**
     * Returns whether the effect of this type happens once, immediately.
     *
     * @return whether this type is normally instant
     */
    public abstract bool isInstant();

    public override bool Equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (!(obj is PotionEffectType)) {
            return false;
        }
        readonly PotionEffectType other = (PotionEffectType) obj;
        if (this.id != other.id) {
            return false;
        }
        return true;
    }

    public override int GetHashCode() {
        return id;
    }

    public override string ToString() {
        return "PotionEffectType[" + id + ", " + getName() + "]";
    }

    private static readonly PotionEffectType[] byId = new PotionEffectType[28];
    private static readonly Dictionary<String, PotionEffectType> byName = new Dictionary<String, PotionEffectType>();
    // will break on updates.
    private static bool acceptingNew = true;

    /**
     * Gets the effect type specified by the unique id.
     *
     * @param id Unique ID to fetch
     * @return Resulting type, or null if not found.
     * [Obsolete] Magic value
     */
    [Obsolete]
    public static PotionEffectType getById(int id) {
        if (id >= byId.Length || id < 0)
            return null;
        return byId[id];
    }

    /**
     * Gets the effect type specified by the given name.
     *
     * @param name Name of PotionEffectType to fetch
     * @return Resulting PotionEffectType, or null if not found.
     */
    public static PotionEffectType getByName(String name) {
        if(name==null) throw new ArgumentNullException("name cannot be null");
        return byName[name.ToLower(]);
    }

    /**
     * Registers an effect type with the given object.
     * <p>
     * Generally not to be used from within a plugin.
     *
     * @param type PotionType to register
     */
    public static void registerPotionEffectType(PotionEffectType type) {
        if (byId[type.id] != null || byName.containsKey(type.getName().ToLower())) {
            throw new ArgumentException("Cannot set already-set type");
        } else if (!acceptingNew) {
            throw new InvalidOperationException(
                    "No longer accepting new potion effect types (can only be done by the server implementation)");
        }

        byId[type.id] = type;
        byName.Add(type.getName().ToLower(), type);
    }

    /**
     * Stops accepting any effect type registrations.
     */
    public static void stopAcceptingRegistrations() {
        acceptingNew = false;
    }

    /**
     * Returns an array of all the registered {@link PotionEffectType}s.
     * This array is not necessarily in any particular order and may contain null.
     *
     * @return Array of types.
     */
    public static PotionEffectType[] values() {
        return byId.clone();
    }
}
