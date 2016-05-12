namespace Mine.NET.enchantments;

import java.util.HashMap;
import java.util.Map;

import org.bukkit.command.defaults.EnchantCommand;
import org.bukkit.inventory.ItemStack;

/**
 * The various type of enchantments that may be added to armour or weapons
 */
public abstract class Enchantment {
    /**
     * Provides protection against environmental damage
     */
    public static readonly Enchantment PROTECTION_ENVIRONMENTAL = new EnchantmentWrapper(0);

    /**
     * Provides protection against fire damage
     */
    public static readonly Enchantment PROTECTION_FIRE = new EnchantmentWrapper(1);

    /**
     * Provides protection against fall damage
     */
    public static readonly Enchantment PROTECTION_FALL = new EnchantmentWrapper(2);

    /**
     * Provides protection against explosive damage
     */
    public static readonly Enchantment PROTECTION_EXPLOSIONS = new EnchantmentWrapper(3);

    /**
     * Provides protection against projectile damage
     */
    public static readonly Enchantment PROTECTION_PROJECTILE = new EnchantmentWrapper(4);

    /**
     * Decreases the rate of air loss whilst underwater
     */
    public static readonly Enchantment OXYGEN = new EnchantmentWrapper(5);

    /**
     * Increases the speed at which a player may mine underwater
     */
    public static readonly Enchantment WATER_WORKER = new EnchantmentWrapper(6);

    /**
     * Damages the attacker
     */
    public static readonly Enchantment THORNS = new EnchantmentWrapper(7);

    /**
     * Increases walking speed while in water
     */
    public static readonly Enchantment DEPTH_STRIDER = new EnchantmentWrapper(8);

    /**
     * Freezes any still water adjacent to ice / frost which player is walking on
     */
    public static readonly Enchantment FROST_WALKER = new EnchantmentWrapper(9);

    /**
     * Increases damage against all targets
     */
    public static readonly Enchantment DAMAGE_ALL = new EnchantmentWrapper(16);

    /**
     * Increases damage against undead targets
     */
    public static readonly Enchantment DAMAGE_UNDEAD = new EnchantmentWrapper(17);

    /**
     * Increases damage against arthropod targets
     */
    public static readonly Enchantment DAMAGE_ARTHROPODS = new EnchantmentWrapper(18);

    /**
     * All damage to other targets will knock them back when hit
     */
    public static readonly Enchantment KNOCKBACK = new EnchantmentWrapper(19);

    /**
     * When attacking a target, has a chance to set them on fire
     */
    public static readonly Enchantment FIRE_ASPECT = new EnchantmentWrapper(20);

    /**
     * Provides a chance of gaining extra loot when killing monsters
     */
    public static readonly Enchantment LOOT_BONUS_MOBS = new EnchantmentWrapper(21);

    /**
     * Increases the rate at which you mine/dig
     */
    public static readonly Enchantment DIG_SPEED = new EnchantmentWrapper(32);

    /**
     * Allows blocks to drop themselves instead of fragments (for example,
     * stone instead of cobblestone)
     */
    public static readonly Enchantment SILK_TOUCH = new EnchantmentWrapper(33);

    /**
     * Decreases the rate at which a tool looses durability
     */
    public static readonly Enchantment DURABILITY = new EnchantmentWrapper(34);

    /**
     * Provides a chance of gaining extra loot when destroying blocks
     */
    public static readonly Enchantment LOOT_BONUS_BLOCKS = new EnchantmentWrapper(35);

    /**
     * Provides extra damage when shooting arrows from bows
     */
    public static readonly Enchantment ARROW_DAMAGE = new EnchantmentWrapper(48);

    /**
     * Provides a knockback when an entity is hit by an arrow from a bow
     */
    public static readonly Enchantment ARROW_KNOCKBACK = new EnchantmentWrapper(49);

    /**
     * Sets entities on fire when hit by arrows shot from a bow
     */
    public static readonly Enchantment ARROW_FIRE = new EnchantmentWrapper(50);

    /**
     * Provides infinite arrows when shooting a bow
     */
    public static readonly Enchantment ARROW_INFINITE = new EnchantmentWrapper(51);

    /**
     * Decreases odds of catching worthless junk
     */
    public static readonly Enchantment LUCK = new EnchantmentWrapper(61);

    /**
     * Increases rate of fish biting your hook
     */
    public static readonly Enchantment LURE = new EnchantmentWrapper(62);

    /**
     * Allows mending the item using experience orbs
     */
    public static readonly Enchantment MENDING = new EnchantmentWrapper(70);

    private static readonly Dictionary<int, Enchantment> byId = new Dictionary<int, Enchantment>();
    private static readonly Dictionary<String, Enchantment> byName = new Dictionary<String, Enchantment>();
    private static bool acceptingNew = true;
    private readonly int id;

    public Enchantment(int id) {
        this.id = id;
    }

    /**
     * Gets the unique ID of this enchantment
     *
     * @return Unique ID
     * [Obsolete] Magic value
     */
    [Obsolete]
    public int getId() {
        return id;
    }

    /**
     * Gets the unique name of this enchantment
     *
     * @return Unique name
     */
    public abstract String getName();

    /**
     * Gets the maximum level that this Enchantment may become.
     *
     * @return Maximum level of the Enchantment
     */
    public abstract int getMaxLevel();

    /**
     * Gets the level that this Enchantment should start at
     *
     * @return Starting level of the Enchantment
     */
    public abstract int getStartLevel();

    /**
     * Gets the type of {@link ItemStack} that may fit this Enchantment.
     *
     * @return Target type of the Enchantment
     */
    public abstract EnchantmentTarget getItemTarget();

    /**
     * Check if this enchantment conflicts with another enchantment.
     *
     * @param other The enchantment to check against
     * @return True if there is a conflict.
     */
    public abstract bool conflictsWith(Enchantment other);

    /**
     * Checks if this Enchantment may be applied to the given {@link
     * ItemStack}.
     * <p>
     * This does not check if it conflicts with any enchantments already
     * applied to the item.
     *
     * @param item Item to test
     * @return True if the enchantment may be applied, otherwise False
     */
    public abstract bool canEnchantItem(ItemStack item);

    public override bool Equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (!(obj is Enchantment)) {
            return false;
        }
        readonly Enchantment other = (Enchantment) obj;
        if (this.id != other.id) {
            return false;
        }
        return true;
    }

    public override int GetHashCode() {
        return id;
    }

    public override string ToString() {
        return "Enchantment[" + id + ", " + getName() + "]";
    }

    /**
     * Registers an enchantment with the given ID and object.
     * <p>
     * Generally not to be used from within a plugin.
     *
     * @param enchantment Enchantment to register
     */
    public static void registerEnchantment(Enchantment enchantment) {
        if (byId.containsKey(enchantment.id) || byName.containsKey(enchantment.getName())) {
            throw new ArgumentException("Cannot set already-set enchantment");
        } else if (!isAcceptingRegistrations()) {
            throw new IllegalStateException("No longer accepting new enchantments (can only be done by the server implementation)");
        }

        byId.Add(enchantment.id, enchantment);
        byName.Add(enchantment.getName(), enchantment);
    }

    /**
     * Checks if this is accepting Enchantment registrations.
     *
     * @return True if the server Implementation may add enchantments
     */
    public static bool isAcceptingRegistrations() {
        return acceptingNew;
    }

    /**
     * Stops accepting any enchantment registrations
     */
    public static void stopAcceptingRegistrations() {
        acceptingNew = false;
        EnchantCommand.buildEnchantments();
    }

    /**
     * Gets the Enchantment at the specified ID
     *
     * @param id ID to fetch
     * @return Resulting Enchantment, or null if not found
     * [Obsolete] Magic value
     */
    [Obsolete]
    public static Enchantment getById(int id) {
        return byId[id];
    }

    /**
     * Gets the Enchantment at the specified name
     *
     * @param name Name to fetch
     * @return Resulting Enchantment, or null if not found
     */
    public static Enchantment getByName(String name) {
        return byName[name];
    }

    /**
     * Gets an array of all the registered {@link Enchantment}s
     *
     * @return Array of enchantments
     */
    public static Enchantment[] values() {
        return byId.values().toArray(new Enchantment[byId.Count]);
    }
}
