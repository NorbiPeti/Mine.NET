using Mine.NET.configuration.serialization;
using Mine.NET.enchantments;
using Mine.NET.inventory.meta;
using Mine.NET.material;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mine.NET.inventory
{
/**
 * Represents a stack of items
 */
public class ItemStack : ICloneable, ConfigurationSerializable {
        private Materials type = null;
    private int amount = 0;
    private MaterialData data = null;
    private short durability = 0;
    private ItemMeta meta;
        
    protected ItemStack() {}

        /**
         * Defaults stack size to 1, with no extra data
         *
         * @param type item material
         */
        public ItemStack(Materials type) :
            this(type, 1)
        {
        }

        /**
         * An item stack with no extra data
         *
         * @param type item material
         * @param amount stack size
         */
        public ItemStack(Materials type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }

        /**
         * An item stack with the specified damage / durabiltiy
         *
         * @param type item material
         * @param amount stack size
         * @param damage durability / damage
         */
        public ItemStack(Materials type, int amount, short damage) {
        this.type = type;
        this.amount = amount;
        this.durability = damage;
    }

    /**
     * Creates a new item stack derived from the specified stack
     *
     * @param stack the stack to copy
     * @throws ArgumentException if the specified stack is null or
     *     returns an item meta not created by the item factory
     */
    public ItemStack(ItemStack stack) {
        if(stack==null) throw new ArgumentNullException("Cannot copy null stack");
            this.type = stack.getType();
        this.amount = stack.getAmount();
        this.durability = stack.getDurability();
        this.data = stack.getData();
        if (stack.hasItemMeta()) {
            setItemMeta0(stack.getItemMeta(), getType());
        }
    }

    /**
     * Gets the type of this item
     *
     * @return Type of the items in this stack
     */
    public Materials getType() {
            return type;
    }

    /**
     * Sets the type of this item
     * <p>
     * Note that in doing so you will reset the MaterialData for this stack
     *
     * @param type New type to set the items in this stack to
     */
    public void setType(Material type) {
        if(type==null) throw new ArgumentNullException("Material cannot be null");
            this.type = type;
            if (this.meta != null)
            {
                this.meta = Bukkit.getItemFactory().asMetaFor(meta, type);
            }
            createData((byte)0);
        }

    /**
     * Gets the amount of items in this stack
     *
     * @return Amount of items in this stick
     */
    public int getAmount() {
        return amount;
    }

    /**
     * Sets the amount of items in this stack
     *
     * @param amount New amount of items in this stack
     */
    public void setAmount(int amount) {
        this.amount = amount;
    }

    /**
     * Gets the MaterialData for this stack of items
     *
     * @return MaterialData for this item
     */
    public MaterialData getData() {
            Material mat = Material.AllMaterials[getType()];
        if (data == null && mat != null && mat.getData() != null) {
            data = mat.getNewData((byte) this.getDurability());
        }

        return data;
    }

        /**
         * Sets the MaterialData for this stack of items
         *
         * @param data New MaterialData for this item
         */
        public void setData(MaterialData data) {
            this.data = data;
        }

    /**
     * Sets the durability of this item
     *
     * @param durability Durability of this item
     */
    public void setDurability(short durability) {
        this.durability = durability;
    }

    /**
     * Gets the durability of this item
     *
     * @return Durability of this item
     */
    public short getDurability() {
        return durability;
    }

    /**
     * Get the maximum stacksize for the material hold in this ItemStack.
     * (Returns -1 if it has no idea)
     *
     * @return The maximum you can stack this material to.
     */
    public int getMaxStackSize() {
        Material material = getType();
        if (material != null) {
            return material.getMaxStackSize();
        }
        return -1;
    }

    private void createData(byte data) {
        if (type == null) {
            this.data = new MaterialData(type, data);
        } else {
            this.data = type.getNewData(data);
        }
    }
        
    public override String ToString() {
        StringBuilder toString = new StringBuilder("ItemStack{").Append(getType()).Append(" x ").Append(getAmount());
        if (hasItemMeta()) {
            toString.Append(", ").Append(getItemMeta());
        }
        return toString.Append('}').ToString();
    }
        
    public override bool Equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj is ItemStack)) {
            return false;
        }

        ItemStack stack = (ItemStack) obj;
        return getAmount() == stack.getAmount() && isSimilar(stack);
    }

    /**
     * This method is the same as equals, but does not consider stack size
     * (amount).
     *
     * @param stack the item stack to compare to
     * @return true if the two stacks are equal, ignoring the amount
     */
    public bool isSimilar(ItemStack stack) {
        if (stack == null) {
            return false;
        }
        if (stack == this) {
            return true;
        }
        return getType() == stack.getType() && getDurability() == stack.getDurability() && hasItemMeta() == stack.hasItemMeta() && (hasItemMeta() ? Bukkit.getItemFactory().equals(getItemMeta(), stack.getItemMeta()) : true);
    }

        public object Clone() {
            ItemStack itemStack = new ItemStack(this);

            if (this.meta != null) {
                itemStack.meta = this.meta.clone();
            }

            if (this.data != null) {
                itemStack.data = this.data.clone();
            }

            return itemStack;
        }
        
    public override int GetHashCode() {
        int hash = 1;

        hash = hash * 31 + getType().GetHashCode();
        hash = hash * 31 + getAmount();
        hash = hash * 31 + (getDurability() & 0xffff);
        hash = hash * 31 + (hasItemMeta() ? (meta == null ? getItemMeta().GetHashCode() : meta.GetHashCode()) : 0);

        return hash;
    }

    /**
     * Checks if this ItemStack contains the given {@link Enchantment}
     *
     * @param ench Enchantment to test
     * @return True if this has the given enchantment
     */
    public bool containsEnchantment(Enchantment ench) {
        return meta == null ? false : meta.hasEnchant(ench);
    }

    /**
     * Gets the level of the specified enchantment on this item stack
     *
     * @param ench Enchantment to check
     * @return Level of the enchantment, or 0
     */
    public int getEnchantmentLevel(Enchantment ench) {
        return meta == null ? 0 : meta.getEnchantLevel(ench);
    }

    /**
     * Gets a map containing all enchantments and their levels on this item.
     *
     * @return Map of enchantments.
     */
    public Dictionary<Enchantment, int> getEnchantments() {
        return meta == null ? new Dictionary<Enchantment, int>() : meta.getEnchants();
    }

    /**
     * Adds the specified enchantments to this item stack.
     * <p>
     * This method is the same as calling {@link
     * #addEnchantment(org.bukkit.enchantments.Enchantment, int)} for each
     * element of the map.
     *
     * @param enchantments Enchantments to add
     * @throws ArgumentException if the specified enchantments is null
     * @throws ArgumentException if any specific enchantment or level
     *     is null. <b>Warning</b>: Some enchantments may be added before this
     *     exception is thrown.
     */
    public void addEnchantments(Dictionary<Enchantment, int> enchantments) {
        if(enchantments==null) throw new ArgumentNullException("Enchantments cannot be null");
        foreach (KeyValuePair<Enchantment, int> entry  in  enchantments) {
            addEnchantment(entry.Key, entry.Value);
        }
    }

    /**
     * Adds the specified {@link Enchantment} to this item stack.
     * <p>
     * If this item stack already contained the given enchantment (at any
     * level), it will be replaced.
     *
     * @param ench Enchantment to add
     * @param level Level of the enchantment
     * @throws ArgumentException if enchantment null, or enchantment is
     *     not applicable
     */
    public void addEnchantment(Enchantment ench, int level) {
        if(ench==null) throw new ArgumentNullException("Enchantment cannot be null");
        if ((level < ench.getStartLevel()) || (level > ench.getMaxLevel())) {
            throw new ArgumentException("Enchantment level is either too low or too high (given " + level + ", bounds are " + ench.getStartLevel() + " to " + ench.getMaxLevel() + ")");
        } else if (!ench.canEnchantItem(this)) {
            throw new ArgumentException("Specified enchantment cannot be applied to this itemstack");
        }

        addUnsafeEnchantment(ench, level);
    }

    /**
     * Adds the specified enchantments to this item stack in an unsafe manner.
     * <p>
     * This method is the same as calling {@link
     * #addUnsafeEnchantment(org.bukkit.enchantments.Enchantment, int)} for
     * each element of the map.
     *
     * @param enchantments Enchantments to add
     */
    public void addUnsafeEnchantments(Dictionary<Enchantment, int> enchantments) {
        foreach (KeyValuePair<Enchantment, int> entry  in  enchantments) {
            addUnsafeEnchantment(entry.Key, entry.Value);
        }
    }

    /**
     * Adds the specified {@link Enchantment} to this item stack.
     * <p>
     * If this item stack already contained the given enchantment (at any
     * level), it will be replaced.
     * <p>
     * This method is unsafe and will ignore level restrictions or item type.
     * Use at your own discretion.
     *
     * @param ench Enchantment to add
     * @param level Level of the enchantment
     */
    public void addUnsafeEnchantment(Enchantment ench, int level) {
        (meta == null ? meta = Bukkit.getItemFactory().getItemMeta(getType()) : meta).addEnchant(ench, level, true);
    }

    /**
     * Removes the specified {@link Enchantment} if it exists on this
     * ItemStack
     *
     * @param ench Enchantment to remove
     * @return Previous level, or 0
     */
    public int removeEnchantment(Enchantment ench) {
        int level = getEnchantmentLevel(ench);
        if (level == 0 || meta == null) {
            return level;
        }
        meta.removeEnchant(ench);
        return level;
    }
        
    public Dictionary<String, Object> serialize() {
        Dictionary<String, Object> result = new Dictionary<String, Object>();

        result.Add("type", getType().ToString());

        if (getDurability() != 0) {
            result.Add("damage", getDurability());
        }

        if (getAmount() != 1) {
            result.Add("amount", getAmount());
        }

        ItemMeta meta = getItemMeta();
        if (!Bukkit.getItemFactory().equals(meta, null)) {
            result.Add("meta", meta);
        }

        return result;
    }

    /**
     * Required method for configuration serialization
     *
     * @param args map to deserialize
     * @return deserialized item stack
     * @see ConfigurationSerializable
     */
    public static ItemStack deserialize(Dictionary<String, Object> args) {
        Material type = Material.getMaterial((String) args["type"]);
        short damage = 0;
        int amount = 1;

        if (args.ContainsKey("damage")) {
                damage = ((short)args["damage"]);
        }

        if (args.ContainsKey("amount")) {
                amount = ((short)args["amount"]);
        }

        ItemStack result = new ItemStack(type, amount, damage);

            if (args.ContainsKey("enchantments"))
            { // Backward compatiblity, [Obsolete]
                Object raw = args["enchantments"];

                if (raw is Dictionary<Enchantment, int>) //TODO
                {
                    var map = (Dictionary<Enchantment, int>)raw;

                    foreach (KeyValuePair<Enchantment, int> entry in map)
                    {
                        Enchantment enchantment = Enchantment.getByName(entry.Key.ToString());

                        if (enchantment != null)
                        {
                            result.addUnsafeEnchantment(enchantment, (int)entry.Value);
                        }
                    }
                }
            }
            else if (args.ContainsKey("meta"))
            { // We cannot and will not have meta when enchantments (pre-ItemMeta) exist
                Object raw = args["meta"];
                if (raw is ItemMeta)
                {
                    result.setItemMeta((ItemMeta)raw);
                }
            }

        return result;
    }

    /**
     * Get a copy of this ItemStack's {@link ItemMeta}.
     *
     * @return a copy of the current ItemStack's ItemData
     */
    public ItemMeta getItemMeta() {
        return this.meta == null ? Bukkit.getItemFactory().getItemMeta(getType()) : this.meta.clone();
    }

    /**
     * Checks to see if any meta data has been defined.
     *
     * @return Returns true if some meta data has been set for this item
     */
    public bool hasItemMeta() {
        return !Bukkit.getItemFactory().equals(meta, null);
    }

    /**
     * Set the ItemMeta of this ItemStack.
     *
     * @param itemMeta new ItemMeta, or null to indicate meta data be cleared.
     * @return True if successfully applied ItemMeta, see {@link
     *     ItemFactory#isApplicable(ItemMeta, ItemStack)}
     * @throws ArgumentException if the item meta was not created by
     *     the {@link ItemFactory}
     */
    public bool setItemMeta(ItemMeta itemMeta) {
        return setItemMeta0(itemMeta, getType());
    }

    /*
     * Cannot be overridden, so it's safe for constructor call
     */
    private bool setItemMeta0(ItemMeta itemMeta, Material material) {
        if (itemMeta == null) {
            this.meta = null;
            return true;
        }
        if (!Bukkit.getItemFactory().isApplicable(itemMeta, material)) {
            return false;
        }
        this.meta = Bukkit.getItemFactory().asMetaFor(itemMeta, material);
        if (this.meta == itemMeta) {
            this.meta = itemMeta.clone();
        }

        return true;
    }
}
}
