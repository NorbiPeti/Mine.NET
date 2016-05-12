using Mine.NET.entity.minecart;
using System;
using System.Collections.Generic;

namespace Mine.NET.entity
{
    public enum EntityTypes {

        // These strings MUST match the strings in nms.EntityTypes and are case sensitive.
        /**
         * An item resting on the ground.
         * <p>
         * Spawn with {@link World#dropItem(Location, ItemStack)} or {@link
         * World#dropItemNaturally(Location, ItemStack)}
         */ //Find: "\([^\)]+\)" - Replace: ""
        DROPPED_ITEM,
        /**
         * An experience orb.
         */
        EXPERIENCE_ORB,
        /**
         * A leash attached to a fencepost.
         */
        LEASH_HITCH,
        /**
         * A painting on a wall.
         */
        PAINTING,
        /**
         * An arrow projectile; may get stuck in the ground.
         */
        ARROW,
        /**
         * A flying snowball.
         */
        SNOWBALL,
        /**
         * A flying large fireball, as thrown by a Ghast for example.
         */
        FIREBALL,
        /**
         * A flying small fireball, such as thrown by a Blaze or player.
         */
        SMALL_FIREBALL,
        /**
         * A flying ender pearl.
         */
        ENDER_PEARL,
        /**
         * An ender eye signal.
         */
        ENDER_SIGNAL,
        /**
         * A flying experience bottle.
         */
        THROWN_EXP_BOTTLE,
        /**
         * An item frame on a wall.
         */
        ITEM_FRAME,
        /**
         * A flying wither skull projectile.
         */
        WITHER_SKULL,
        /**
         * Primed TNT that is about to explode.
         */
        PRIMED_TNT,
        /**
         * A block that is going to or is about to fall.
         */
        FALLING_BLOCK,
        /**
         * Internal representation of a Firework once it has been launched.
         */
        FIREWORK,
        /**
         * Like {@link #ARROW} but tipped with a specific potion which is applied on contact.
         */
        TIPPED_ARROW,
        /**
         * Like {@link #TIPPED_ARROW} but causes the {@link PotionEffectType#GLOWING} effect on all team members.
         */
        SPECTRAL_ARROW,
        /**
         * Bullet fired by {@link #SHULKER}.
         */
        SHULKER_BULLET,
        /**
         * Like {@link #FIREBALL} but with added effects.
         */
        DRAGON_FIREBALL,
        /**
         * Mechanical entity with an inventory for placing weapons / armor into.
         */
        ARMOR_STAND,
        /**
         * @see CommandMinecart
         */
        MINECART_COMMAND,
        /**
         * A placed boat.
         */
        BOAT,
        /**
         * @see RideableMinecart
         */
        MINECART,
        /**
         * @see StorageMinecart
         */
        MINECART_CHEST,
        /**
         * @see PoweredMinecart
         */
        MINECART_FURNACE,
        /**
         * @see ExplosiveMinecart
         */
        MINECART_TNT,
        /**
         * @see HopperMinecart
         */
        MINECART_HOPPER,
        /**
         * @see SpawnerMinecart
         */
        MINECART_MOB_SPAWNER,
        CREEPER,
        SKELETON,
        SPIDER,
        GIANT,
        ZOMBIE,
        SLIME,
        GHAST,
        PIG_ZOMBIE,
        ENDERMAN,
        CAVE_SPIDER,
        SILVERFISH,
        BLAZE,
        MAGMA_CUBE,
        ENDER_DRAGON,
        WITHER,
        BAT,
        WITCH,
        ENDERMITE,
        GUARDIAN,
        SHULKER,
        PIG,
        SHEEP,
        COW,
        CHICKEN,
        SQUID,
        WOLF,
        MUSHROOM_COW,
        SNOWMAN,
        OCELOT,
        IRON_GOLEM,
        HORSE,
        RABBIT,
        VILLAGER,
        ENDER_CRYSTAL,
        // These don't have an entity ID in nms.EntityTypes.
        /**
         * A flying splash potion
         */
        SPLASH_POTION,
        /**
         * A flying lingering potion
         */
        LINGERING_POTION,
        AREA_EFFECT_CLOUD,
        /**
         * A flying chicken egg.
         */
        EGG,
        /**
         * A fishing line and bobber.
         */
        FISHING_HOOK,
        /**
         * A bolt of lightning.
         * <p>
         * Spawn with {@link World#strikeLightning}.
         */
        LIGHTNING,
        WEATHER,
        PLAYER,
        COMPLEX_PART,
        /**
         * An unknown entity without an Entity Class
         */
        UNKNOWN
    }












    public class EntityType
    {
        public static Dictionary<EntityTypes, EntityType> AllEntityTypes = new Dictionary<EntityTypes, EntityType>
        { //Find: "\*\/\s+([^\(]+)(\([^\(]+\))" - Replace: "*/\n{ $1, new EntityType$2 }" - Mostly worked - Aand I forgot the EntityTypes.
          /**
       * An item resting on the ground.
       * <p>
       * Spawn with {@link World#dropItem(Location, ItemStack)} or {@link
       * World#dropItemNaturally(Location, ItemStack)}
       */ //Find: "(\w+)\.class" - Replace: "typeof($1)"
            { EntityTypes.DROPPED_ITEM, new EntityType("Item", typeof(Item), 1, false) },
    /**
     * An experience orb.
     */
{ EntityTypes.EXPERIENCE_ORB, new EntityType("XPOrb", typeof(ExperienceOrb), 2) },
    /**
     * A leash attached to a fencepost.
     */
{ EntityTypes.LEASH_HITCH, new EntityType("LeashKnot", typeof(LeashHitch), 8) },
    /**
     * A painting on a wall.
     */
{ EntityTypes.PAINTING, new EntityType("Painting", typeof(Painting), 9) },
    /**
     * An arrow projectile; may get stuck in the ground.
     */
{ EntityTypes.ARROW, new EntityType("Arrow", typeof(Arrow), 10) },
    /**
     * A flying snowball.
     */
{ EntityTypes.SNOWBALL, new EntityType("Snowball", typeof(Snowball), 11) },
    /**
     * A flying large fireball, as thrown by a Ghast for example.
     */
{ EntityTypes.FIREBALL, new EntityType("Fireball", typeof(LargeFireball), 12) },
    /**
     * A flying small fireball, such as thrown by a Blaze or player.
     */
{ EntityTypes.SMALL_FIREBALL, new EntityType("SmallFireball", typeof(SmallFireball), 13) },
    /**
     * A flying ender pearl.
     */
{ EntityTypes.ENDER_PEARL, new EntityType("ThrownEnderpearl", typeof(EnderPearl), 14) },
    /**
     * An ender eye signal.
     */
{ EntityTypes.ENDER_SIGNAL, new EntityType("EyeOfEnderSignal", typeof(EnderSignal), 15) },
    /**
     * A flying experience bottle.
     */
{ EntityTypes.THROWN_EXP_BOTTLE, new EntityType("ThrownExpBottle", typeof(ThrownExpBottle), 17) },
    /**
     * An item frame on a wall.
     */
{ EntityTypes.ITEM_FRAME, new EntityType("ItemFrame", typeof(ItemFrame), 18) },
    /**
     * A flying wither skull projectile.
     */
{ EntityTypes.WITHER_SKULL, new EntityType("WitherSkull", typeof(WitherSkull), 19) },
    /**
     * Primed TNT that is about to explode.
     */
{ EntityTypes.PRIMED_TNT, new EntityType("PrimedTnt", typeof(TNTPrimed), 20) },
    /**
     * A block that is going to or is about to fall.
     */
{ EntityTypes.FALLING_BLOCK, new EntityType("FallingSand", typeof(FallingBlock), 21, false) },
    /**
     * Internal representation of a Firework once it has been launched.
     */
{ EntityTypes.FIREWORK, new EntityType("FireworksRocketEntity", typeof(Firework), 22, false) },
    /**
     * Like {@link #ARROW} but tipped with a specific potion which is applied on contact.
     */
{ EntityTypes.TIPPED_ARROW, new EntityType("TippedArrow", typeof(TippedArrow), 23) },
    /**
     * Like {@link #TIPPED_ARROW} but causes the {@link PotionEffectType#GLOWING} effect on all team members.
     */
{ EntityTypes.SPECTRAL_ARROW, new EntityType("SpectralArrow", typeof(SpectralArrow), 24) },
    /**
     * Bullet fired by {@link #SHULKER}.
     */
{ EntityTypes.SHULKER_BULLET, new EntityType("ShulkerBullet", typeof(ShulkerBullet), 25) },
    /**
     * Like {@link #FIREBALL} but with added effects.
     */
{ EntityTypes.DRAGON_FIREBALL, new EntityType("DragonFireball", typeof(DragonFireball), 26) },
    /**
     * Mechanical entity with an inventory for placing weapons / armor into.
     */
{ EntityTypes.ARMOR_STAND, new EntityType("ArmorStand", typeof(ArmorStand), 30, false) },
    /**
     * @see CommandMinecart
     */
{ EntityTypes.MINECART_COMMAND, new EntityType("MinecartCommandBlock", typeof(CommandMinecart), 40) },
    /**
     * A placed boat.
     */
{ EntityTypes.BOAT, new EntityType("Boat", typeof(Boat), 41) },
    /**
     * @see RideableMinecart
     */
{ EntityTypes.MINECART, new EntityType("MinecartRideable", typeof(RideableMinecart), 42) },
    /**
     * @see StorageMinecart
     */
{ EntityTypes.MINECART_CHEST, new EntityType("MinecartChest", typeof(StorageMinecart), 43) },
    /**
     * @see PoweredMinecart
     */
{ EntityTypes.MINECART_FURNACE, new EntityType("MinecartFurnace", typeof(PoweredMinecart), 44) },
    /**
     * @see ExplosiveMinecart
     */
{ EntityTypes.MINECART_TNT, new EntityType("MinecartTNT", typeof(ExplosiveMinecart), 45) },
    /**
     * @see HopperMinecart
     */
{ EntityTypes.MINECART_HOPPER, new EntityType("MinecartHopper", typeof(HopperMinecart), 46) },
    /**
     * @see SpawnerMinecart
     */
{ EntityTypes.MINECART_MOB_SPAWNER, new EntityType("MinecartMobSpawner", typeof(SpawnerMinecart), 47) },
    { EntityTypes.CREEPER, new EntityType("Creeper", typeof(Creeper), 50) },
    { EntityTypes.SKELETON, new EntityType("Skeleton", typeof(Skeleton), 51) },
    { EntityTypes.SPIDER, new EntityType("Spider", typeof(Spider), 52) },
    { EntityTypes.GIANT, new EntityType("Giant", typeof(Giant), 53) },
    { EntityTypes.ZOMBIE, new EntityType("Zombie", typeof(Zombie), 54) },
    { EntityTypes.SLIME, new EntityType("Slime", typeof(Slime), 55) },
    { EntityTypes.GHAST, new EntityType("Ghast", typeof(Ghast), 56) },
    { EntityTypes.PIG_ZOMBIE, new EntityType("PigZombie", typeof(PigZombie), 57) },
    { EntityTypes.ENDERMAN, new EntityType("Enderman", typeof(Enderman), 58) },
    { EntityTypes.CAVE_SPIDER, new EntityType("CaveSpider", typeof(CaveSpider), 59) },
    { EntityTypes.SILVERFISH, new EntityType("Silverfish", typeof(Silverfish), 60) },
    { EntityTypes.BLAZE, new EntityType("Blaze", typeof(Blaze), 61) },
    { EntityTypes.MAGMA_CUBE, new EntityType("LavaSlime", typeof(MagmaCube), 62) },
    { EntityTypes.ENDER_DRAGON, new EntityType("EnderDragon", typeof(EnderDragon), 63) },
    { EntityTypes.WITHER, new EntityType("WitherBoss", typeof(Wither), 64) },
    { EntityTypes.BAT, new EntityType("Bat", typeof(Bat), 65) },
    { EntityTypes.WITCH, new EntityType("Witch", typeof(Witch), 66) },
    { EntityTypes.ENDERMITE, new EntityType("Endermite", typeof(Endermite), 67) },
    { EntityTypes.GUARDIAN, new EntityType("Guardian", typeof(Guardian), 68) },
    { EntityTypes.SHULKER, new EntityType("Shulker", typeof(Shulker), 69) },
    { EntityTypes.PIG, new EntityType("Pig", typeof(Pig), 90) },
    { EntityTypes.SHEEP, new EntityType("Sheep", typeof(Sheep), 91) },
    { EntityTypes.COW, new EntityType("Cow", typeof(Cow), 92) },
    { EntityTypes.CHICKEN, new EntityType("Chicken", typeof(Chicken), 93) },
    { EntityTypes.SQUID, new EntityType("Squid", typeof(Squid), 94) },
    { EntityTypes.WOLF, new EntityType("Wolf", typeof(Wolf), 95) },
    { EntityTypes.MUSHROOM_COW, new EntityType("MushroomCow", typeof(MushroomCow), 96) },
    { EntityTypes.SNOWMAN, new EntityType("SnowMan", typeof(Snowman), 97) },
    { EntityTypes.OCELOT, new EntityType("Ozelot", typeof(Ocelot), 98) },
    { EntityTypes.IRON_GOLEM, new EntityType("VillagerGolem", typeof(IronGolem), 99) },
    { EntityTypes.HORSE, new EntityType("EntityHorse", typeof(Horse), 100) },
    { EntityTypes.RABBIT, new EntityType("Rabbit", typeof(Rabbit), 101) },
    { EntityTypes.VILLAGER, new EntityType("Villager", typeof(Villager), 120) },
    { EntityTypes.ENDER_CRYSTAL, new EntityType("EnderCrystal", typeof(EnderCrystal), 200) },
    // These don't have an entity ID in nms.EntityTypes.
    /**
     * A flying splash potion
     */
{ EntityTypes.SPLASH_POTION, new EntityType(null, typeof(SplashPotion), -1, false) },
    /**
     * A flying lingering potion
     */
{ EntityTypes.LINGERING_POTION, new EntityType(null, typeof(LingeringPotion), -1, false) },
    { EntityTypes.AREA_EFFECT_CLOUD, new EntityType(null, typeof(AreaEffectCloud), -1) },
    /**
     * A flying chicken egg.
     */
{ EntityTypes.EGG, new EntityType(null, typeof(Egg), -1, false) },
    /**
     * A fishing line and bobber.
     */
{ EntityTypes.FISHING_HOOK, new EntityType(null, typeof(Fish), -1, false) },
    /**
     * A bolt of lightning.
     * <p>
     * Spawn with {@link World#strikeLightning(Location)}.
     */
{ EntityTypes.LIGHTNING, new EntityType(null, typeof(LightningStrike), -1, false) },
    { EntityTypes.WEATHER, new EntityType(null, typeof(Weather), -1, false) },
    { EntityTypes.PLAYER, new EntityType(null, typeof(Player), -1, false) },
    { EntityTypes.COMPLEX_PART, new EntityType(null, typeof(ComplexEntityPart), -1, false) },
    /**
     * An unknown entity without an Entity Class
     */
{ EntityTypes.UNKNOWN, new EntityType(null, null, -1, false) }
            };

    private String name;
    private Type clazz; //Entity
    private short typeId;
    private bool independent, living;

    private static readonly Dictionary<String, EntityType> NAME_MAP = new Dictionary<String, EntityType>();
    private static readonly Dictionary<short, EntityType> ID_MAP = new Dictionary<short, EntityType>();

        private EntityType(String name, Type clazz, int typeId) : this(name, clazz, typeId, true)
        {
        }

    private EntityType(String name, Type clazz, int typeId, bool independent) {
        this.name = name;
        this.clazz = clazz;
        this.typeId = (short) typeId;
        this.independent = independent;
        if (clazz != null) {
            this.living = typeof(LivingEntity).IsAssignableFrom(clazz);
        }
    }

    /**
     *
     * @return the entity type's name
     * [Obsolete] Magic value
     */
    [Obsolete]
    public String getName() {
        return name;
    }

    public Type getEntityClass() {
        return clazz;
    }

    /**
     *
     * @return the raw type id 
     * [Obsolete] Magic value
     */
    [Obsolete]
    public short getTypeId() {
        return typeId;
    }

    /**
     *
     * @param name the entity type's name
     * @return the matching entity type or null
     * [Obsolete] Magic value
     */
    [Obsolete]
    public static EntityType fromName(String name) {
        if (name == null) {
            return null;
        }
        return NAME_MAP[name.ToLower()];
    }

    /**
     *
     * @param id the raw type id
     * @return the matching entity type or null
     * [Obsolete] Magic value
     */
    [Obsolete]
    public static EntityType fromId(int id) {
        if (id > short.MaxValue) {
            return null;
        }
        return ID_MAP[(short) id];
    }

    /**
     * Some entities cannot be spawned using {@link
     * World#spawnEntity(Location, EntityType)} or {@link
     * World#spawn(Location, Class)}, usually because they require additional
     * information in order to spawn.
     *
     * @return False if the entity type cannot be spawned
     */
    public bool isSpawnable() {
        return independent;
    }

    public bool isAlive() {
        return living;
    }
}
}
