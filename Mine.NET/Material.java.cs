
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
/**
* An enum of all material IDs accepted by the official server and client
*/
public class Material
{
    public enum Materials
    {
        AIR,
        STONE,
        GRASS,
        DIRT,
        COBBLESTONE,
        WOOD,
        SAPLING,
        BEDROCK,
        WATER,
        STATIONARY_WATER,
        LAVA,
        STATIONARY_LAVA,
        SAND,
        GRAVEL,
        GOLD_ORE,
        IRON_ORE,
        COAL_ORE,
        LOG,
        LEAVES,
        SPONGE,
        GLASS,
        LAPIS_ORE,
        LAPIS_BLOCK,
        DISPENSER,
        SANDSTONE,
        NOTE_BLOCK,
        BED_BLOCK,
        POWERED_RAIL,
        DETECTOR_RAIL,
        PISTON_STICKY_BASE,
        WEB,
        LONG_GRASS,
        DEAD_BUSH,
        PISTON_BASE,
        PISTON_EXTENSION,
        WOOL,
        PISTON_MOVING_PIECE,
        YELLOW_FLOWER,
        RED_ROSE,
        BROWN_MUSHROOM,
        RED_MUSHROOM,
        GOLD_BLOCK,
        IRON_BLOCK,
        DOUBLE_STEP,
        STEP,
        BRICK,
        TNT,
        BOOKSHELF,
        MOSSY_COBBLESTONE,
        OBSIDIAN,
        TORCH,
        FIRE,
        MOB_SPAWNER,
        WOOD_STAIRS,
        CHEST,
        REDSTONE_WIRE,
        DIAMOND_ORE,
        DIAMOND_BLOCK,
        WORKBENCH,
        CROPS,
        SOIL,
        FURNACE,
        BURNING_FURNACE,
        SIGN_POST,
        WOODEN_DOOR,
        LADDER,
        RAILS,
        COBBLESTONE_STAIRS,
        WALL_SIGN,
        LEVER,
        STONE_PLATE,
        IRON_DOOR_BLOCK,
        WOOD_PLATE,
        REDSTONE_ORE,
        GLOWING_REDSTONE_ORE,
        REDSTONE_TORCH_OFF,
        REDSTONE_TORCH_ON,
        STONE_BUTTON,
        SNOW,
        ICE,
        SNOW_BLOCK,
        CACTUS,
        CLAY,
        SUGAR_CANE_BLOCK,
        JUKEBOX,
        FENCE,
        PUMPKIN,
        NETHERRACK,
        SOUL_SAND,
        GLOWSTONE,
        PORTAL,
        JACK_O_LANTERN,
        CAKE_BLOCK,
        DIODE_BLOCK_OFF,
        DIODE_BLOCK_ON,
        STAINED_GLASS,
        TRAP_DOOR,
        MONSTER_EGGS,
        SMOOTH_BRICK,
        HUGE_MUSHROOM_1,
        HUGE_MUSHROOM_2,
        IRON_FENCE,
        THIN_GLASS,
        MELON_BLOCK,
        PUMPKIN_STEM,
        MELON_STEM,
        VINE,
        FENCE_GATE,
        BRICK_STAIRS,
        SMOOTH_STAIRS,
        MYCEL,
        WATER_LILY,
        NETHER_BRICK,
        NETHER_FENCE,
        NETHER_BRICK_STAIRS,
        NETHER_WARTS,
        ENCHANTMENT_TABLE,
        BREWING_STAND,
        CAULDRON,
        ENDER_PORTAL,
        ENDER_PORTAL_FRAME,
        ENDER_STONE,
        DRAGON_EGG,
        REDSTONE_LAMP_OFF,
        REDSTONE_LAMP_ON,
        WOOD_DOUBLE_STEP,
        WOOD_STEP,
        COCOA,
        SANDSTONE_STAIRS,
        EMERALD_ORE,
        ENDER_CHEST,
        TRIPWIRE_HOOK,
        TRIPWIRE,
        EMERALD_BLOCK,
        SPRUCE_WOOD_STAIRS,
        BIRCH_WOOD_STAIRS,
        JUNGLE_WOOD_STAIRS,
        COMMAND,
        BEACON,
        COBBLE_WALL,
        FLOWER_POT,
        CARROT,
        POTATO,
        WOOD_BUTTON,
        SKULL,
        ANVIL,
        TRAPPED_CHEST,
        GOLD_PLATE,
        IRON_PLATE,
        REDSTONE_COMPARATOR_OFF,
        REDSTONE_COMPARATOR_ON,
        DAYLIGHT_DETECTOR,
        REDSTONE_BLOCK,
        QUARTZ_ORE,
        HOPPER,
        QUARTZ_BLOCK,
        QUARTZ_STAIRS,
        ACTIVATOR_RAIL,
        DROPPER,
        STAINED_CLAY,
        STAINED_GLASS_PANE,
        LEAVES_2,
        LOG_2,
        ACACIA_STAIRS,
        DARK_OAK_STAIRS,
        SLIME_BLOCK,
        BARRIER,
        IRON_TRAPDOOR,
        PRISMARINE,
        SEA_LANTERN,
        HAY_BLOCK,
        CARPET,
        HARD_CLAY,
        COAL_BLOCK,
        PACKED_ICE,
        DOUBLE_PLANT,
        STANDING_BANNER,
        WALL_BANNER,
        DAYLIGHT_DETECTOR_INVERTED,
        RED_SANDSTONE,
        RED_SANDSTONE_STAIRS,
        DOUBLE_STONE_SLAB2,
        STONE_SLAB2,
        SPRUCE_FENCE_GATE,
        BIRCH_FENCE_GATE,
        JUNGLE_FENCE_GATE,
        DARK_OAK_FENCE_GATE,
        ACACIA_FENCE_GATE,
        SPRUCE_FENCE,
        BIRCH_FENCE,
        JUNGLE_FENCE,
        DARK_OAK_FENCE,
        ACACIA_FENCE,
        SPRUCE_DOOR,
        BIRCH_DOOR,
        JUNGLE_DOOR,
        ACACIA_DOOR,
        DARK_OAK_DOOR,
        END_ROD,
        CHORUS_PLANT,
        CHORUS_FLOWER,
        PURPUR_BLOCK,
        PURPUR_PILLAR,
        PURPUR_STAIRS,
        PURPUR_DOUBLE_SLAB,
        PURPUR_SLAB,
        END_BRICKS,
        BEETROOT_BLOCK,
        GRASS_PATH,
        END_GATEWAY,
        COMMAND_REPEATING,
        COMMAND_CHAIN,
        FROSTED_ICE,
        STRUCTURE_BLOCK,
        // ----- Item Separator -----
        IRON_SPADE,
        IRON_PICKAXE,
        IRON_AXE,
        FLINT_AND_STEEL,
        APPLE,
        BOW,
        ARROW,
        COAL,
        DIAMOND,
        IRON_INGOT,
        GOLD_INGOT,
        IRON_SWORD,
        WOOD_SWORD,
        WOOD_SPADE,
        WOOD_PICKAXE,
        WOOD_AXE,
        STONE_SWORD,
        STONE_SPADE,
        STONE_PICKAXE,
        STONE_AXE,
        DIAMOND_SWORD,
        DIAMOND_SPADE,
        DIAMOND_PICKAXE,
        DIAMOND_AXE,
        STICK,
        BOWL,
        MUSHROOM_SOUP,
        GOLD_SWORD,
        GOLD_SPADE,
        GOLD_PICKAXE,
        GOLD_AXE,
        STRING,
        FEATHER,
        SULPHUR,
        WOOD_HOE,
        STONE_HOE,
        IRON_HOE,
        DIAMOND_HOE,
        GOLD_HOE,
        SEEDS,
        WHEAT,
        BREAD,
        LEATHER_HELMET,
        LEATHER_CHESTPLATE,
        LEATHER_LEGGINGS,
        LEATHER_BOOTS,
        CHAINMAIL_HELMET,
        CHAINMAIL_CHESTPLATE,
        CHAINMAIL_LEGGINGS,
        CHAINMAIL_BOOTS,
        IRON_HELMET,
        IRON_CHESTPLATE,
        IRON_LEGGINGS,
        IRON_BOOTS,
        DIAMOND_HELMET,
        DIAMOND_CHESTPLATE,
        DIAMOND_LEGGINGS,
        DIAMOND_BOOTS,
        GOLD_HELMET,
        GOLD_CHESTPLATE,
        GOLD_LEGGINGS,
        GOLD_BOOTS,
        FLINT,
        PORK,
        GRILLED_PORK,
        PAINTING,
        GOLDEN_APPLE,
        SIGN,
        WOOD_DOOR,
        BUCKET,
        WATER_BUCKET,
        LAVA_BUCKET,
        MINECART,
        SADDLE,
        IRON_DOOR,
        REDSTONE,
        SNOW_BALL,
        BOAT,
        LEATHER,
        MILK_BUCKET,
        CLAY_BRICK,
        CLAY_BALL,
        SUGAR_CANE,
        PAPER,
        BOOK,
        SLIME_BALL,
        STORAGE_MINECART,
        POWERED_MINECART,
        EGG,
        COMPASS,
        FISHING_ROD,
        WATCH,
        GLOWSTONE_DUST,
        RAW_FISH,
        COOKED_FISH,
        INK_SACK,
        BONE,
        SUGAR,
        CAKE,
        BED,
        DIODE,
        COOKIE,
        /**
         * @see MapView
         */
        MAP,
        SHEARS,
        MELON,
        PUMPKIN_SEEDS,
        MELON_SEEDS,
        RAW_BEEF,
        COOKED_BEEF,
        RAW_CHICKEN,
        COOKED_CHICKEN,
        ROTTEN_FLESH,
        ENDER_PEARL,
        BLAZE_ROD,
        GHAST_TEAR,
        GOLD_NUGGET,
        NETHER_STALK,
        POTION,
        GLASS_BOTTLE,
        SPIDER_EYE,
        FERMENTED_SPIDER_EYE,
        BLAZE_POWDER,
        MAGMA_CREAM,
        BREWING_STAND_ITEM,
        CAULDRON_ITEM,
        EYE_OF_ENDER,
        SPECKLED_MELON,
        MONSTER_EGG,
        EXP_BOTTLE,
        FIREBALL,
        BOOK_AND_QUILL,
        WRITTEN_BOOK,
        EMERALD,
        ITEM_FRAME,
        FLOWER_POT_ITEM,
        CARROT_ITEM,
        POTATO_ITEM,
        BAKED_POTATO,
        POISONOUS_POTATO,
        EMPTY_MAP,
        GOLDEN_CARROT,
        SKULL_ITEM,
        CARROT_STICK,
        NETHER_STAR,
        PUMPKIN_PIE,
        FIREWORK,
        FIREWORK_CHARGE,
        ENCHANTED_BOOK,
        REDSTONE_COMPARATOR,
        NETHER_BRICK_ITEM,
        QUARTZ,
        EXPLOSIVE_MINECART,
        HOPPER_MINECART,
        PRISMARINE_SHARD,
        PRISMARINE_CRYSTALS,
        RABBIT,
        COOKED_RABBIT,
        RABBIT_STEW,
        RABBIT_FOOT,
        RABBIT_HIDE,
        ARMOR_STAND,
        IRON_BARDING,
        GOLD_BARDING,
        DIAMOND_BARDING,
        LEASH,
        NAME_TAG,
        COMMAND_MINECART,
        MUTTON,
        COOKED_MUTTON,
        BANNER,
        END_CRYSTAL,
        SPRUCE_DOOR_ITEM,
        BIRCH_DOOR_ITEM,
        JUNGLE_DOOR_ITEM,
        ACACIA_DOOR_ITEM,
        DARK_OAK_DOOR_ITEM,
        CHORUS_FRUIT,
        CHORUS_FRUIT_POPPED,
        BEETROOT,
        BEETROOT_SEEDS,
        BEETROOT_SOUP,
        DRAGONS_BREATH,
        SPLASH_POTION,
        SPECTRAL_ARROW,
        TIPPED_ARROW,
        LINGERING_POTION,
        SHIELD,
        ELYTRA,
        BOAT_SPRUCE,
        BOAT_BIRCH,
        BOAT_JUNGLE,
        BOAT_ACACIA,
        BOAT_DARK_OAK,
        GOLD_RECORD,
        GREEN_RECORD,
        RECORD_3,
        RECORD_4,
        RECORD_5,
        RECORD_6,
        RECORD_7,
        RECORD_8,
        RECORD_9,
        RECORD_10,
        RECORD_11,
        RECORD_12
    }

    public static Dictionary<Materials, Material> AllMaterials = new Dictionary<Materials, Material>
    {
        { Materials.AIR, new Material(0, 0) },
        { Materials.STONE, new Material(1) },
        { Materials.GRASS, new Material(2) },
        { Materials.DIRT, new Material(3) },
        { Materials.COBBLESTONE, new Material(4) },
        { Materials.WOOD, new Material(5, typeof(Wood)) },
        { Materials.SAPLING, new Material(6, typeof(Sapling)) },
        { Materials.BEDROCK, new Material(7) },
        { Materials.WATER, new Material(8, typeof(MaterialData)) },
        { Materials.STATIONARY_WATER, new Material(9, typeof(MaterialData)) },
        { Materials.LAVA, new Material(10, typeof(MaterialData)) },
        { Materials.STATIONARY_LAVA, new Material(11, typeof(MaterialData)) },
        { Materials.SAND, new Material(12) },
        { Materials.GRAVEL, new Material(13) },
        { Materials.GOLD_ORE, new Material(14) },
        { Materials.IRON_ORE, new Material(15) },
        { Materials.COAL_ORE, new Material(16) },
        { Materials.LOG, new Material(17, typeof(Tree)) },
        { Materials.LEAVES, new Material(18, typeof(Leaves)) },
        { Materials.SPONGE, new Material(19) },
        { Materials.GLASS, new Material(20) },
        { Materials.LAPIS_ORE, new Material(21) },
        { Materials.LAPIS_BLOCK, new Material(22) },
        { Materials.DISPENSER, new Material(23, typeof(Dispenser)) },
        { Materials.SANDSTONE, new Material(24, typeof(Sandstone)) },
        { Materials.NOTE_BLOCK, new Material(25) },
        { Materials.BED_BLOCK, new Material(26, typeof(Bed)) },
        { Materials.POWERED_RAIL, new Material(27, typeof(PoweredRail)) },
        { Materials.DETECTOR_RAIL, new Material(28, typeof(DetectorRail)) },
        { Materials.PISTON_STICKY_BASE, new Material(29, typeof(PistonBaseMaterial)) },
        { Materials.WEB, new Material(30) },
        { Materials.LONG_GRASS, new Material(31, typeof(LongGrass)) },
        { Materials.DEAD_BUSH, new Material(32) },
        { Materials.PISTON_BASE, new Material(33, typeof(PistonBaseMaterial)) },
        { Materials.PISTON_EXTENSION, new Material(34, typeof(PistonExtensionMaterial)) },
        { Materials.WOOL, new Material(35, typeof(Wool)) },
        { Materials.PISTON_MOVING_PIECE, new Material(36) },
        { Materials.YELLOW_FLOWER, new Material(37) },
        { Materials.RED_ROSE, new Material(38) },
        { Materials.BROWN_MUSHROOM, new Material(39) },
        { Materials.RED_MUSHROOM, new Material(40) },
        { Materials.GOLD_BLOCK, new Material(41) },
        { Materials.IRON_BLOCK, new Material(42) },
        { Materials.DOUBLE_STEP, new Material(43, typeof(Step)) },
        { Materials.STEP, new Material(44, typeof(Step)) },
        { Materials.BRICK, new Material(45) },
        { Materials.TNT, new Material(46) },
        { Materials.BOOKSHELF, new Material(47) },
        { Materials.MOSSY_COBBLESTONE, new Material(48) },
        { Materials.OBSIDIAN, new Material(49) },
        { Materials.TORCH, new Material(50, typeof(Torch)) },
        { Materials.FIRE, new Material(51) },
        { Materials.MOB_SPAWNER, new Material(52) },
        { Materials.WOOD_STAIRS, new Material(53, typeof(Stairs)) },
        { Materials.CHEST, new Material(54, typeof(Chest)) },
        { Materials.REDSTONE_WIRE, new Material(55, typeof(RedstoneWire)) },
        { Materials.DIAMOND_ORE, new Material(56) },
        { Materials.DIAMOND_BLOCK, new Material(57) },
        { Materials.WORKBENCH, new Material(58) },
        { Materials.CROPS, new Material(59, typeof(Crops)) },
        { Materials.SOIL, new Material(60, typeof(MaterialData)) },
        { Materials.FURNACE, new Material(61, typeof(Furnace)) },
        { Materials.BURNING_FURNACE, new Material(62, typeof(Furnace)) },
        { Materials.SIGN_POST, new Material(63, 64, typeof(Sign)) },
        { Materials.WOODEN_DOOR, new Material(64, typeof(Door)) },
        { Materials.LADDER, new Material(65, typeof(Ladder)) },
        { Materials.RAILS, new Material(66, typeof(Rails)) },
        { Materials.COBBLESTONE_STAIRS, new Material(67, typeof(Stairs)) },
        { Materials.WALL_SIGN, new Material(68, 64, typeof(Sign)) },
        { Materials.LEVER, new Material(69, typeof(Lever)) },
        { Materials.STONE_PLATE, new Material(70, typeof(PressurePlate)) },
        { Materials.IRON_DOOR_BLOCK, new Material(71, typeof(Door)) },
        { Materials.WOOD_PLATE, new Material(72, typeof(PressurePlate)) },
        { Materials.REDSTONE_ORE, new Material(73) },
        { Materials.GLOWING_REDSTONE_ORE, new Material(74) },
        { Materials.REDSTONE_TORCH_OFF, new Material(75, typeof(RedstoneTorch)) },
        { Materials.REDSTONE_TORCH_ON, new Material(76, typeof(RedstoneTorch)) },
        { Materials.STONE_BUTTON, new Material(77, typeof(Button)) },
        { Materials.SNOW, new Material(78) },
        { Materials.ICE, new Material(79) },
        { Materials.SNOW_BLOCK, new Material(80) },
        { Materials.CACTUS, new Material(81, typeof(MaterialData)) },
        { Materials.CLAY, new Material(82) },
        { Materials.SUGAR_CANE_BLOCK, new Material(83, typeof(MaterialData)) },
        { Materials.JUKEBOX, new Material(84) },
        { Materials.FENCE, new Material(85) },
        { Materials.PUMPKIN, new Material(86, typeof(Pumpkin)) },
        { Materials.NETHERRACK, new Material(87) },
        { Materials.SOUL_SAND, new Material(88) },
        { Materials.GLOWSTONE, new Material(89) },
        { Materials.PORTAL, new Material(90) },
        { Materials.JACK_O_LANTERN, new Material(91, typeof(Pumpkin)) },
        { Materials.CAKE_BLOCK, new Material(92, 64, typeof(Cake)) },
        { Materials.DIODE_BLOCK_OFF, new Material(93, typeof(Diode)) },
        { Materials.DIODE_BLOCK_ON, new Material(94, typeof(Diode)) },
        { Materials.STAINED_GLASS, new Material(95) },
        { Materials.TRAP_DOOR, new Material(96, typeof(TrapDoor)) },
        { Materials.MONSTER_EGGS, new Material(97, typeof(MonsterEggs)) },
        { Materials.SMOOTH_BRICK, new Material(98, typeof(SmoothBrick)) },
        { Materials.HUGE_MUSHROOM_1, new Material(99, typeof(Mushroom)) },
        { Materials.HUGE_MUSHROOM_2, new Material(100, typeof(Mushroom)) },
        { Materials.IRON_FENCE, new Material(101) },
        { Materials.THIN_GLASS, new Material(102) },
        { Materials.MELON_BLOCK, new Material(103) },
        { Materials.PUMPKIN_STEM, new Material(104, typeof(MaterialData)) },
        { Materials.MELON_STEM, new Material(105, typeof(MaterialData)) },
        { Materials.VINE, new Material(106, typeof(Vine)) },
        { Materials.FENCE_GATE, new Material(107, typeof(Gate)) },
        { Materials.BRICK_STAIRS, new Material(108, typeof(Stairs)) },
        { Materials.SMOOTH_STAIRS, new Material(109, typeof(Stairs)) },
        { Materials.MYCEL, new Material(110) },
        { Materials.WATER_LILY, new Material(111) },
        { Materials.NETHER_BRICK, new Material(112) },
        { Materials.NETHER_FENCE, new Material(113) },
        { Materials.NETHER_BRICK_STAIRS, new Material(114, typeof(Stairs)) },
        { Materials.NETHER_WARTS, new Material(115, typeof(NetherWarts)) },
        { Materials.ENCHANTMENT_TABLE, new Material(116) },
        { Materials.BREWING_STAND, new Material(117, typeof(MaterialData)) },
        { Materials.CAULDRON, new Material(118, typeof(Cauldron)) },
        { Materials.ENDER_PORTAL, new Material(119) },
        { Materials.ENDER_PORTAL_FRAME, new Material(120) },
        { Materials.ENDER_STONE, new Material(121) },
        { Materials.DRAGON_EGG, new Material(122) },
        { Materials.REDSTONE_LAMP_OFF, new Material(123) },
        { Materials.REDSTONE_LAMP_ON, new Material(124) },
        { Materials.WOOD_DOUBLE_STEP, new Material(125, typeof(Wood)) },
        { Materials.WOOD_STEP, new Material(126, typeof(WoodenStep)) },
        { Materials.COCOA, new Material(127, typeof(CocoaPlant)) },
        { Materials.SANDSTONE_STAIRS, new Material(128, typeof(Stairs)) },
        { Materials.EMERALD_ORE, new Material(129) },
        { Materials.ENDER_CHEST, new Material(130, typeof(EnderChest)) },
        { Materials.TRIPWIRE_HOOK, new Material(131, typeof(TripwireHook)) },
        { Materials.TRIPWIRE, new Material(132, typeof(Tripwire)) },
        { Materials.EMERALD_BLOCK, new Material(133) },
        { Materials.SPRUCE_WOOD_STAIRS, new Material(134, typeof(Stairs)) },
        { Materials.BIRCH_WOOD_STAIRS, new Material(135, typeof(Stairs)) },
        { Materials.JUNGLE_WOOD_STAIRS, new Material(136, typeof(Stairs)) },
        { Materials.COMMAND, new Material(137, typeof(Command)) },
        { Materials.BEACON, new Material(138) },
        { Materials.COBBLE_WALL, new Material(139) },
        { Materials.FLOWER_POT, new Material(140, typeof(FlowerPot)) },
        { Materials.CARROT, new Material(141, typeof(Crops)) },
        { Materials.POTATO, new Material(142, typeof(Crops)) },
        { Materials.WOOD_BUTTON, new Material(143, typeof(Button)) },
        { Materials.SKULL, new Material(144, typeof(Skull)) },
        { Materials.ANVIL, new Material(145) },
        { Materials.TRAPPED_CHEST, new Material(146, typeof(Chest)) },
        { Materials.GOLD_PLATE, new Material(147) },
        { Materials.IRON_PLATE, new Material(148) },
        { Materials.REDSTONE_COMPARATOR_OFF, new Material(149, typeof(Comparator)) },
        { Materials.REDSTONE_COMPARATOR_ON, new Material(150, typeof(Comparator)) },
        { Materials.DAYLIGHT_DETECTOR, new Material(151) },
        { Materials.REDSTONE_BLOCK, new Material(152) },
        { Materials.QUARTZ_ORE, new Material(153) },
        { Materials.HOPPER, new Material(154, typeof(Hopper)) },
        { Materials.QUARTZ_BLOCK, new Material(155) },
        { Materials.QUARTZ_STAIRS, new Material(156, typeof(Stairs)) },
        { Materials.ACTIVATOR_RAIL, new Material(157, typeof(PoweredRail)) },
        { Materials.DROPPER, new Material(158, typeof(Dispenser)) },
        { Materials.STAINED_CLAY, new Material(159) },
        { Materials.STAINED_GLASS_PANE, new Material(160) },
        { Materials.LEAVES_2, new Material(161, typeof(Leaves)) },
        { Materials.LOG_2, new Material(162, typeof(Tree)) },
        { Materials.ACACIA_STAIRS, new Material(163, typeof(Stairs)) },
        { Materials.DARK_OAK_STAIRS, new Material(164, typeof(Stairs)) },
        { Materials.SLIME_BLOCK, new Material(165) },
        { Materials.BARRIER, new Material(166) },
        { Materials.IRON_TRAPDOOR, new Material(167, typeof(TrapDoor)) },
        { Materials.PRISMARINE, new Material(168) },
        { Materials.SEA_LANTERN, new Material(169) },
        { Materials.HAY_BLOCK, new Material(170) },
        { Materials.CARPET, new Material(171) },
        { Materials.HARD_CLAY, new Material(172) },
        { Materials.COAL_BLOCK, new Material(173) },
        { Materials.PACKED_ICE, new Material(174) },
        { Materials.DOUBLE_PLANT, new Material(175) },
        { Materials.STANDING_BANNER, new Material(176, typeof(Banner)) },
        { Materials.WALL_BANNER, new Material(177, typeof(Banner)) },
        { Materials.DAYLIGHT_DETECTOR_INVERTED, new Material(178) },
        { Materials.RED_SANDSTONE, new Material(179) },
        { Materials.RED_SANDSTONE_STAIRS, new Material(180, typeof(Stairs)) },
        { Materials.DOUBLE_STONE_SLAB2, new Material(181) },
        { Materials.STONE_SLAB2, new Material(182) },
        { Materials.SPRUCE_FENCE_GATE, new Material(183, typeof(Gate)) },
        { Materials.BIRCH_FENCE_GATE, new Material(184, typeof(Gate)) },
        { Materials.JUNGLE_FENCE_GATE, new Material(185, typeof(Gate)) },
        { Materials.DARK_OAK_FENCE_GATE, new Material(186, typeof(Gate)) },
        { Materials.ACACIA_FENCE_GATE, new Material(187, typeof(Gate)) },
        { Materials.SPRUCE_FENCE, new Material(188) },
        { Materials.BIRCH_FENCE, new Material(189) },
        { Materials.JUNGLE_FENCE, new Material(190) },
        { Materials.DARK_OAK_FENCE, new Material(191) },
        { Materials.ACACIA_FENCE, new Material(192) },
        { Materials.SPRUCE_DOOR, new Material(193, typeof(Door)) },
        { Materials.BIRCH_DOOR, new Material(194, typeof(Door)) },
        { Materials.JUNGLE_DOOR, new Material(195, typeof(Door)) },
        { Materials.ACACIA_DOOR, new Material(196, typeof(Door)) },
        { Materials.DARK_OAK_DOOR, new Material(197, typeof(Door)) },
        { Materials.END_ROD, new Material(198) },
        { Materials.CHORUS_PLANT, new Material(199) },
        { Materials.CHORUS_FLOWER, new Material(200) },
        { Materials.PURPUR_BLOCK, new Material(201) },
        { Materials.PURPUR_PILLAR, new Material(202) },
        { Materials.PURPUR_STAIRS, new Material(203, typeof(Stairs)) },
        { Materials.PURPUR_DOUBLE_SLAB, new Material(204) },
        { Materials.PURPUR_SLAB, new Material(205) },
        { Materials.END_BRICKS, new Material(206) },
        { Materials.BEETROOT_BLOCK, new Material(207, typeof(Crops)) },
        { Materials.GRASS_PATH, new Material(208) },
        { Materials.END_GATEWAY, new Material(209) },
        { Materials.COMMAND_REPEATING, new Material(210) },
        { Materials.COMMAND_CHAIN, new Material(211) },
        { Materials.FROSTED_ICE, new Material(212) },
        { Materials.STRUCTURE_BLOCK, new Material(255) },
        // ----- Item Separator -----
        { Materials.IRON_SPADE, new Material(256, 1, 250) },
        { Materials.IRON_PICKAXE, new Material(257, 1, 250) },
        { Materials.IRON_AXE, new Material(258, 1, 250) },
        { Materials.FLINT_AND_STEEL, new Material(259, 1, 64) },
        { Materials.APPLE, new Material(260) },
        { Materials.BOW, new Material(261, 1, 384) },
            { Materials.ARROW, new Material(262) },
        { Materials.COAL, new Material(263, typeof(Coal)) },
        { Materials.DIAMOND, new Material(264) },
        { Materials.IRON_INGOT, new Material(265) },
        { Materials.GOLD_INGOT, new Material(266) },
        { Materials.IRON_SWORD, new Material(267, 1, 250) },
        { Materials.WOOD_SWORD, new Material(268, 1, 59) },
        { Materials.WOOD_SPADE, new Material(269, 1, 59) },
        { Materials.WOOD_PICKAXE, new Material(270, 1, 59) },
        { Materials.WOOD_AXE, new Material(271, 1, 59) },
        { Materials.STONE_SWORD, new Material(272, 1, 131) },
        { Materials.STONE_SPADE, new Material(273, 1, 131) },
        { Materials.STONE_PICKAXE, new Material(274, 1, 131) },
        { Materials.STONE_AXE, new Material(275, 1, 131) },
        { Materials.DIAMOND_SWORD, new Material(276, 1, 1561) },
        { Materials.DIAMOND_SPADE, new Material(277, 1, 1561) },
        { Materials.DIAMOND_PICKAXE, new Material(278, 1, 1561) },
        { Materials.DIAMOND_AXE, new Material(279, 1, 1561) },
        { Materials.STICK, new Material(280) },
        { Materials.BOWL, new Material(281) },
        { Materials.MUSHROOM_SOUP, new Material(282, 1) },
        { Materials.GOLD_SWORD, new Material(283, 1, 32) },
        { Materials.GOLD_SPADE, new Material(284, 1, 32) },
        { Materials.GOLD_PICKAXE, new Material(285, 1, 32) },
        { Materials.GOLD_AXE, new Material(286, 1, 32) },
        { Materials.STRING, new Material(287) },
        { Materials.FEATHER, new Material(288) },
        { Materials.SULPHUR, new Material(289) },
        { Materials.WOOD_HOE, new Material(290, 1, 59) },
        { Materials.STONE_HOE, new Material(291, 1, 131) },
        { Materials.IRON_HOE, new Material(292, 1, 250) },
        { Materials.DIAMOND_HOE, new Material(293, 1, 1561) },
        { Materials.GOLD_HOE, new Material(294, 1, 32) },
        { Materials.SEEDS, new Material(295) },
        { Materials.WHEAT, new Material(296) },
        { Materials.BREAD, new Material(297) },
        { Materials.LEATHER_HELMET, new Material(298, 1, 55) },
        { Materials.LEATHER_CHESTPLATE, new Material(299, 1, 80) },
        { Materials.LEATHER_LEGGINGS, new Material(300, 1, 75) },
        { Materials.LEATHER_BOOTS, new Material(301, 1, 65) },
        { Materials.CHAINMAIL_HELMET, new Material(302, 1, 165) },
        { Materials.CHAINMAIL_CHESTPLATE, new Material(303, 1, 240) },
        { Materials.CHAINMAIL_LEGGINGS, new Material(304, 1, 225) },
        { Materials.CHAINMAIL_BOOTS, new Material(305, 1, 195) },
        { Materials.IRON_HELMET, new Material(306, 1, 165) },
        { Materials.IRON_CHESTPLATE, new Material(307, 1, 240) },
        { Materials.IRON_LEGGINGS, new Material(308, 1, 225) },
        { Materials.IRON_BOOTS, new Material(309, 1, 195) },
        { Materials.DIAMOND_HELMET, new Material(310, 1, 363) },
        { Materials.DIAMOND_CHESTPLATE, new Material(311, 1, 528) },
        { Materials.DIAMOND_LEGGINGS, new Material(312, 1, 495) },
        { Materials.DIAMOND_BOOTS, new Material(313, 1, 429) },
        { Materials.GOLD_HELMET, new Material(314, 1, 77) },
        { Materials.GOLD_CHESTPLATE, new Material(315, 1, 112) },
        { Materials.GOLD_LEGGINGS, new Material(316, 1, 105) },
        { Materials.GOLD_BOOTS, new Material(317, 1, 91) },
        { Materials.FLINT, new Material(318) },
        { Materials.PORK, new Material(319) },
        { Materials.GRILLED_PORK, new Material(320) },
        { Materials.PAINTING, new Material(321) },
        { Materials.GOLDEN_APPLE, new Material(322) },
        { Materials.SIGN, new Material(323, 16) },
        { Materials.WOOD_DOOR, new Material(324, 64) },
        { Materials.BUCKET, new Material(325, 16) },
        { Materials.WATER_BUCKET, new Material(326, 1) },
        { Materials.LAVA_BUCKET, new Material(327, 1) },
        { Materials.MINECART, new Material(328, 1) },
        { Materials.SADDLE, new Material(329, 1) },
        { Materials.IRON_DOOR, new Material(330, 64) },
        { Materials.REDSTONE, new Material(331) },
        { Materials.SNOW_BALL, new Material(332, 16) },
        { Materials.BOAT, new Material(333, 1) },
        { Materials.LEATHER, new Material(334) },
        { Materials.MILK_BUCKET, new Material(335, 1) },
        { Materials.CLAY_BRICK, new Material(336) },
        { Materials.CLAY_BALL, new Material(337) },
        { Materials.SUGAR_CANE, new Material(338) },
        { Materials.PAPER, new Material(339) },
        { Materials.BOOK, new Material(340) },
        { Materials.SLIME_BALL, new Material(341) },
        { Materials.STORAGE_MINECART, new Material(342, 1) },
        { Materials.POWERED_MINECART, new Material(343, 1) },
        { Materials.EGG, new Material(344, 16) },
        { Materials.COMPASS, new Material(345) },
        { Materials.FISHING_ROD, new Material(346, 1, 64) },
        { Materials.WATCH, new Material(347) },
        { Materials.GLOWSTONE_DUST, new Material(348) },
        { Materials.RAW_FISH, new Material(349) },
        { Materials.COOKED_FISH, new Material(350) },
        { Materials.INK_SACK, new Material(351, typeof(Dye)) },
        { Materials.BONE, new Material(352) },
        { Materials.SUGAR, new Material(353) },
        { Materials.CAKE, new Material(354, 1) },
        { Materials.BED, new Material(355, 1) },
        { Materials.DIODE, new Material(356) },
        { Materials.COOKIE, new Material(357) },
        /**
         * @see MapView
         */
        { Materials.MAP, new Material(358, typeof(MaterialData)) },
        { Materials.SHEARS, new Material(359, 1, 238) },
        { Materials.MELON, new Material(360) },
        { Materials.PUMPKIN_SEEDS, new Material(361) },
        { Materials.MELON_SEEDS, new Material(362) },
        { Materials.RAW_BEEF, new Material(363) },
        { Materials.COOKED_BEEF, new Material(364) },
        { Materials.RAW_CHICKEN, new Material(365) },
        { Materials.COOKED_CHICKEN, new Material(366) },
        { Materials.ROTTEN_FLESH, new Material(367) },
        { Materials.ENDER_PEARL, new Material(368, 16) },
        { Materials.BLAZE_ROD, new Material(369) },
        { Materials.GHAST_TEAR, new Material(370) },
        { Materials.GOLD_NUGGET, new Material(371) },
        { Materials.NETHER_STALK, new Material(372) },
        { Materials.POTION, new Material(373, 1, typeof(MaterialData)) },
        { Materials.GLASS_BOTTLE, new Material(374) },
        { Materials.SPIDER_EYE, new Material(375) },
        { Materials.FERMENTED_SPIDER_EYE, new Material(376) },
        { Materials.BLAZE_POWDER, new Material(377) },
        { Materials.MAGMA_CREAM, new Material(378) },
        { Materials.BREWING_STAND_ITEM, new Material(379) },
        { Materials.CAULDRON_ITEM, new Material(380) },
        { Materials.EYE_OF_ENDER, new Material(381) },
        { Materials.SPECKLED_MELON, new Material(382) },
        { Materials.MONSTER_EGG, new Material(383, 64, typeof(SpawnEgg)) },
        { Materials.EXP_BOTTLE, new Material(384, 64) },
        { Materials.FIREBALL, new Material(385, 64) },
        { Materials.BOOK_AND_QUILL, new Material(386, 1) },
        { Materials.WRITTEN_BOOK, new Material(387, 16) },
        { Materials.EMERALD, new Material(388, 64) },
        { Materials.ITEM_FRAME, new Material(389) },
        { Materials.FLOWER_POT_ITEM, new Material(390) },
        { Materials.CARROT_ITEM, new Material(391) },
        { Materials.POTATO_ITEM, new Material(392) },
        { Materials.BAKED_POTATO, new Material(393) },
        { Materials.POISONOUS_POTATO, new Material(394) },
        { Materials.EMPTY_MAP, new Material(395) },
        { Materials.GOLDEN_CARROT, new Material(396) },
        { Materials.SKULL_ITEM, new Material(397) },
        { Materials.CARROT_STICK, new Material(398, 1, 25) },
        { Materials.NETHER_STAR, new Material(399) },
        { Materials.PUMPKIN_PIE, new Material(400) },
        { Materials.FIREWORK, new Material(401) },
        { Materials.FIREWORK_CHARGE, new Material(402) },
        { Materials.ENCHANTED_BOOK, new Material(403, 1) },
        { Materials.REDSTONE_COMPARATOR, new Material(404) },
        { Materials.NETHER_BRICK_ITEM, new Material(405) },
        { Materials.QUARTZ, new Material(406) },
        { Materials.EXPLOSIVE_MINECART, new Material(407, 1) },
        { Materials.HOPPER_MINECART, new Material(408, 1) },
        { Materials.PRISMARINE_SHARD, new Material(409) },
        { Materials.PRISMARINE_CRYSTALS, new Material(410) },
        { Materials.RABBIT, new Material(411) },
        { Materials.COOKED_RABBIT, new Material(412) },
        { Materials.RABBIT_STEW, new Material(413, 1) },
        { Materials.RABBIT_FOOT, new Material(414) },
        { Materials.RABBIT_HIDE, new Material(415) },
        { Materials.ARMOR_STAND, new Material(416, 16) },
        { Materials.IRON_BARDING, new Material(417, 1) },
        { Materials.GOLD_BARDING, new Material(418, 1) },
        { Materials.DIAMOND_BARDING, new Material(419, 1) },
        { Materials.LEASH, new Material(420) },
        { Materials.NAME_TAG, new Material(421) },
        { Materials.COMMAND_MINECART, new Material(422, 1) },
        { Materials.MUTTON, new Material(423) },
        { Materials.COOKED_MUTTON, new Material(424) },
        { Materials.BANNER, new Material(425, 16) },
        { Materials.END_CRYSTAL, new Material(426) },
        { Materials.SPRUCE_DOOR_ITEM, new Material(427) },
        { Materials.BIRCH_DOOR_ITEM, new Material(428) },
        { Materials.JUNGLE_DOOR_ITEM, new Material(429) },
        { Materials.ACACIA_DOOR_ITEM, new Material(430) },
        { Materials.DARK_OAK_DOOR_ITEM, new Material(431) },
        { Materials.CHORUS_FRUIT, new Material(432) },
        { Materials.CHORUS_FRUIT_POPPED, new Material(433) },
        { Materials.BEETROOT, new Material(434) },
        { Materials.BEETROOT_SEEDS, new Material(435) },
        { Materials.BEETROOT_SOUP, new Material(436, 1) },
        { Materials.DRAGONS_BREATH, new Material(437) },
        { Materials.SPLASH_POTION, new Material(438, 1) },
        { Materials.SPECTRAL_ARROW, new Material(439) },
        { Materials.TIPPED_ARROW, new Material(440) },
        { Materials.LINGERING_POTION, new Material(441, 1) },
        { Materials.SHIELD, new Material(442, 1, 336) },
        { Materials.ELYTRA, new Material(443, 1, 431) },
        { Materials.BOAT_SPRUCE, new Material(444, 1) },
        { Materials.BOAT_BIRCH, new Material(445, 1) },
        { Materials.BOAT_JUNGLE, new Material(446, 1) },
        { Materials.BOAT_ACACIA, new Material(447, 1) },
        { Materials.BOAT_DARK_OAK, new Material(448, 1) },
        { Materials.GOLD_RECORD, new Material(2256, 1) },
        { Materials.GREEN_RECORD, new Material(2257, 1) },
        { Materials.RECORD_3, new Material(2258, 1) },
        { Materials.RECORD_4, new Material(2259, 1) },
        { Materials.RECORD_5, new Material(2260, 1) },
        { Materials.RECORD_6, new Material(2261, 1) },
        { Materials.RECORD_7, new Material(2262, 1) },
        { Materials.RECORD_8, new Material(2263, 1) },
        { Materials.RECORD_9, new Material(2264, 1) },
        { Materials.RECORD_10, new Material(2265, 1) },
        { Materials.RECORD_11, new Material(2266, 1) },
        { Materials.RECORD_12, new Material(2267, 1) }
    };

    private readonly int id;
    //private readonly Constructor<? extends MaterialData> ctor;
    private readonly int maxStack;
    private readonly short durability;

    private Materials? EnumValue;

    private Material(int id) {
        new Material(id, 64);
    }

    private Material(int id, int stack) {
        new Material(id, stack, typeof(MaterialData));
    }

    private Material(int id, int stack, int durability) {
        new Material(id, stack, durability, typeof(MaterialData));
    }

    private Material(int id, Type data) {
        new Material(id, 64, data);
    }

    private Material(int id, int stack, Type data) {
        new Material(id, stack, 0, data);
    }

    private Type datatype;
    private Material(int id, int stack, int durability, Type data) {
        this.id = id;
        this.durability = (short) durability;
        this.maxStack = stack;
        if (!typeof(MaterialData).IsAssignableFrom(data))
            throw new ArgumentException("Data must be a MaterialData type", nameof(data));
        this.datatype = data;
    }

    /**
     * Gets the item ID or block ID of this Material
     *
     * @return ID of this material
     * [Obsolete] Magic value
     */
    [Obsolete]
    public int getId() {
        return id;
    }

    /**
     * Gets the maximum amount of this material that can be held in a stack
     *
     * @return Maximum stack size for this material
     */
    public int getMaxStackSize() {
        return maxStack;
    }

    /**
     * Gets the maximum durability of this material
     *
     * @return Maximum durability for this material
     */
    public short getMaxDurability() {
        return durability;
    }

    /**
     * Gets the MaterialData class associated with this Material
     *
     * @return MaterialData associated with this Material
     */
    public Type getData() {
        return datatype;
    }

    /**
     * Constructs a new MaterialData relevant for this Material, with the
     * given initial data
     *
     * @param raw Initial data to construct the MaterialData with
     * @return New MaterialData with the given data
     * [Obsolete] Magic value
     */
    [Obsolete]
    public MaterialData getNewData(byte raw) {
            return (MaterialData)Activator.CreateInstance(datatype, id, raw);
    }

    /**
     * Checks if this Material is a placable block
     *
     * @return true if this material is a block
     */
    public bool isBlock() {
        return id < 256;
    }

    /**
     * Checks if this Material is edible.
     *
     * @return true if this Material is edible.
     */
    public bool isEdible() {
        EnumValue = EnumValue ?? AllMaterials.First(m => m.Value == this).Key;
        switch (EnumValue) {
            case Materials.BREAD:
            case Materials.CARROT_ITEM:
            case Materials.BAKED_POTATO:
            case Materials.POTATO_ITEM:
            case Materials.POISONOUS_POTATO:
            case Materials.GOLDEN_CARROT:
            case Materials.PUMPKIN_PIE:
            case Materials.COOKIE:
            case Materials.MELON:
            case Materials.MUSHROOM_SOUP:
            case Materials.RAW_CHICKEN:
            case Materials.COOKED_CHICKEN:
            case Materials.RAW_BEEF:
            case Materials.COOKED_BEEF:
            case Materials.RAW_FISH:
            case Materials.COOKED_FISH:
            case Materials.PORK:
            case Materials.GRILLED_PORK:
            case Materials.APPLE:
            case Materials.GOLDEN_APPLE:
            case Materials.ROTTEN_FLESH:
            case Materials.SPIDER_EYE:
            case Materials.RABBIT:
            case Materials.COOKED_RABBIT:
            case Materials.RABBIT_STEW:
            case Materials.MUTTON:
            case Materials.COOKED_MUTTON:
            case Materials.BEETROOT:
            case Materials.CHORUS_FRUIT:
            case Materials.BEETROOT_SOUP:
                return true;
            default:
                return false;
        }
    }
    /**
     * Attempts to match the Material with the given name.
     * <p>
     * This is a match lookup; names will be converted to uppercase, then
     * stripped of special characters in an attempt to format it like the
     * enum.
     * <p>
     * Using this for match by ID is deprecated.
     *
     * @param name Name of the material to get
     * @return Material if found, or null
     */
    public static Material matchMaterial(String name) {
        if (name == null)
            throw new ArgumentNullException(nameof(name), "Name cannot be null");

        Material result = null;
        int iresult = 0;

        if (int.TryParse(name, out iresult))
            result = getMaterial(iresult);

        if (result == null) {
            String filtered = name.ToUpper();

            filtered = Regex.Replace(Regex.Replace(filtered, "\\s+", "_"), "\\W", "");
            result = AllMaterials[(Materials)Enum.Parse(typeof(Materials), (filtered))];
        }

        return result;
    }

    /**
     * @return True if this material represents a playable music disk.
     */
    public bool isRecord() {
        return id >= Materials.GOLD_RECORD.id && id <= Materials.RECORD_12.id;
    }

    /**
     * Check if the material is a block and solid (cannot be passed through by
     * a player)
     *
     * @return True if this material is a block and solid
     */
    public bool isSolid() {
        if (!isBlock() || id == 0) {
            return false;
        }
        EnumValue = EnumValue ?? AllMaterials.First(m => m.Value == this).Key;
        switch (EnumValue) {
            case STONE:
            case GRASS:
            case DIRT:
            case COBBLESTONE:
            case WOOD:
            case BEDROCK:
            case SAND:
            case GRAVEL:
            case GOLD_ORE:
            case IRON_ORE:
            case COAL_ORE:
            case LOG:
            case LEAVES:
            case SPONGE:
            case GLASS:
            case LAPIS_ORE:
            case LAPIS_BLOCK:
            case DISPENSER:
            case SANDSTONE:
            case NOTE_BLOCK:
            case BED_BLOCK:
            case PISTON_STICKY_BASE:
            case PISTON_BASE:
            case PISTON_EXTENSION:
            case WOOL:
            case PISTON_MOVING_PIECE:
            case GOLD_BLOCK:
            case IRON_BLOCK:
            case DOUBLE_STEP:
            case STEP:
            case BRICK:
            case TNT:
            case BOOKSHELF:
            case MOSSY_COBBLESTONE:
            case OBSIDIAN:
            case MOB_SPAWNER:
            case WOOD_STAIRS:
            case CHEST:
            case DIAMOND_ORE:
            case DIAMOND_BLOCK:
            case WORKBENCH:
            case SOIL:
            case FURNACE:
            case BURNING_FURNACE:
            case SIGN_POST:
            case WOODEN_DOOR:
            case COBBLESTONE_STAIRS:
            case WALL_SIGN:
            case STONE_PLATE:
            case IRON_DOOR_BLOCK:
            case WOOD_PLATE:
            case REDSTONE_ORE:
            case GLOWING_REDSTONE_ORE:
            case ICE:
            case SNOW_BLOCK:
            case CACTUS:
            case CLAY:
            case JUKEBOX:
            case FENCE:
            case PUMPKIN:
            case NETHERRACK:
            case SOUL_SAND:
            case GLOWSTONE:
            case JACK_O_LANTERN:
            case CAKE_BLOCK:
            case STAINED_GLASS:
            case TRAP_DOOR:
            case MONSTER_EGGS:
            case SMOOTH_BRICK:
            case HUGE_MUSHROOM_1:
            case HUGE_MUSHROOM_2:
            case IRON_FENCE:
            case THIN_GLASS:
            case MELON_BLOCK:
            case FENCE_GATE:
            case BRICK_STAIRS:
            case SMOOTH_STAIRS:
            case MYCEL:
            case NETHER_BRICK:
            case NETHER_FENCE:
            case NETHER_BRICK_STAIRS:
            case ENCHANTMENT_TABLE:
            case BREWING_STAND:
            case CAULDRON:
            case ENDER_PORTAL_FRAME:
            case ENDER_STONE:
            case DRAGON_EGG:
            case REDSTONE_LAMP_OFF:
            case REDSTONE_LAMP_ON:
            case WOOD_DOUBLE_STEP:
            case WOOD_STEP:
            case SANDSTONE_STAIRS:
            case EMERALD_ORE:
            case ENDER_CHEST:
            case EMERALD_BLOCK:
            case SPRUCE_WOOD_STAIRS:
            case BIRCH_WOOD_STAIRS:
            case JUNGLE_WOOD_STAIRS:
            case COMMAND:
            case BEACON:
            case COBBLE_WALL:
            case ANVIL:
            case TRAPPED_CHEST:
            case GOLD_PLATE:
            case IRON_PLATE:
            case DAYLIGHT_DETECTOR:
            case REDSTONE_BLOCK:
            case QUARTZ_ORE:
            case HOPPER:
            case QUARTZ_BLOCK:
            case QUARTZ_STAIRS:
            case DROPPER:
            case STAINED_CLAY:
            case HAY_BLOCK:
            case HARD_CLAY:
            case COAL_BLOCK:
            case STAINED_GLASS_PANE:
            case LEAVES_2:
            case LOG_2:
            case ACACIA_STAIRS:
            case DARK_OAK_STAIRS:
            case PACKED_ICE:
            case RED_SANDSTONE:
            case SLIME_BLOCK:
            case BARRIER:
            case IRON_TRAPDOOR:
            case PRISMARINE:
            case SEA_LANTERN:
            case DOUBLE_STONE_SLAB2:
            case RED_SANDSTONE_STAIRS:
            case STONE_SLAB2:
            case SPRUCE_FENCE_GATE:
            case BIRCH_FENCE_GATE:
            case JUNGLE_FENCE_GATE:
            case DARK_OAK_FENCE_GATE:
            case ACACIA_FENCE_GATE:
            case SPRUCE_FENCE:
            case BIRCH_FENCE:
            case JUNGLE_FENCE:
            case DARK_OAK_FENCE:
            case ACACIA_FENCE:
            case STANDING_BANNER:
            case WALL_BANNER:
            case DAYLIGHT_DETECTOR_INVERTED:
            case SPRUCE_DOOR:
            case BIRCH_DOOR:
            case JUNGLE_DOOR:
            case ACACIA_DOOR:
            case DARK_OAK_DOOR:
            case PURPUR_BLOCK:
            case PURPUR_PILLAR:
            case PURPUR_STAIRS:
            case PURPUR_DOUBLE_SLAB:
            case PURPUR_SLAB:
            case END_BRICKS:
            case GRASS_PATH:
            case STRUCTURE_BLOCK:
            case COMMAND_REPEATING:
            case COMMAND_CHAIN:
            case FROSTED_ICE:
                return true;
            default:
                return false;
        }
    }

    /**
     * Check if the material is a block and does not block any light
     *
     * @return True if this material is a block and does not block any light
     */
    public boolean isTransparent() {
        if (!isBlock()) {
            return false;
        }
        switch (this) {
            case AIR:
            case SAPLING:
            case POWERED_RAIL:
            case DETECTOR_RAIL:
            case LONG_GRASS:
            case DEAD_BUSH:
            case YELLOW_FLOWER:
            case RED_ROSE:
            case BROWN_MUSHROOM:
            case RED_MUSHROOM:
            case TORCH:
            case FIRE:
            case REDSTONE_WIRE:
            case CROPS:
            case LADDER:
            case RAILS:
            case LEVER:
            case REDSTONE_TORCH_OFF:
            case REDSTONE_TORCH_ON:
            case STONE_BUTTON:
            case SNOW:
            case SUGAR_CANE_BLOCK:
            case PORTAL:
            case DIODE_BLOCK_OFF:
            case DIODE_BLOCK_ON:
            case PUMPKIN_STEM:
            case MELON_STEM:
            case VINE:
            case WATER_LILY:
            case NETHER_WARTS:
            case ENDER_PORTAL:
            case COCOA:
            case TRIPWIRE_HOOK:
            case TRIPWIRE:
            case FLOWER_POT:
            case CARROT:
            case POTATO:
            case WOOD_BUTTON:
            case SKULL:
            case REDSTONE_COMPARATOR_OFF:
            case REDSTONE_COMPARATOR_ON:
            case ACTIVATOR_RAIL:
            case CARPET:
            case DOUBLE_PLANT:
            case END_ROD:
            case CHORUS_PLANT:
            case CHORUS_FLOWER:
            case BEETROOT_BLOCK:
            case END_GATEWAY:
                return true;
            default:
                return false;
        }
    }

    /**
     * Check if the material is a block and can catch fire
     *
     * @return True if this material is a block and can catch fire
     */
    public boolean isFlammable() {
        if (!isBlock()) {
            return false;
        }
        switch (this) {
            case WOOD:
            case LOG:
            case LEAVES:
            case NOTE_BLOCK:
            case BED_BLOCK:
            case LONG_GRASS:
            case DEAD_BUSH:
            case WOOL:
            case TNT:
            case BOOKSHELF:
            case WOOD_STAIRS:
            case CHEST:
            case WORKBENCH:
            case SIGN_POST:
            case WOODEN_DOOR:
            case WALL_SIGN:
            case WOOD_PLATE:
            case JUKEBOX:
            case FENCE:
            case TRAP_DOOR:
            case HUGE_MUSHROOM_1:
            case HUGE_MUSHROOM_2:
            case VINE:
            case FENCE_GATE:
            case WOOD_DOUBLE_STEP:
            case WOOD_STEP:
            case SPRUCE_WOOD_STAIRS:
            case BIRCH_WOOD_STAIRS:
            case JUNGLE_WOOD_STAIRS:
            case TRAPPED_CHEST:
            case DAYLIGHT_DETECTOR:
            case CARPET:
            case LEAVES_2:
            case LOG_2:
            case ACACIA_STAIRS:
            case DARK_OAK_STAIRS:
            case DOUBLE_PLANT:
            case SPRUCE_FENCE_GATE:
            case BIRCH_FENCE_GATE:
            case JUNGLE_FENCE_GATE:
            case DARK_OAK_FENCE_GATE:
            case ACACIA_FENCE_GATE:
            case SPRUCE_FENCE:
            case BIRCH_FENCE:
            case JUNGLE_FENCE:
            case DARK_OAK_FENCE:
            case ACACIA_FENCE:
            case STANDING_BANNER:
            case WALL_BANNER:
            case DAYLIGHT_DETECTOR_INVERTED:
            case SPRUCE_DOOR:
            case BIRCH_DOOR:
            case JUNGLE_DOOR:
            case ACACIA_DOOR:
            case DARK_OAK_DOOR:
                return true;
            default:
                return false;
        }
    }

    /**
     * Check if the material is a block and can burn away
     *
     * @return True if this material is a block and can burn away
     */
    public boolean isBurnable() {
        if (!isBlock()) {
            return false;
        }
        switch (this) {
            case WOOD:
            case LOG:
            case LEAVES:
            case LONG_GRASS:
            case WOOL:
            case YELLOW_FLOWER:
            case RED_ROSE:
            case TNT:
            case BOOKSHELF:
            case WOOD_STAIRS:
            case FENCE:
            case VINE:
            case WOOD_DOUBLE_STEP:
            case WOOD_STEP:
            case SPRUCE_WOOD_STAIRS:
            case BIRCH_WOOD_STAIRS:
            case JUNGLE_WOOD_STAIRS:
            case HAY_BLOCK:
            case COAL_BLOCK:
            case LEAVES_2:
            case LOG_2:
            case CARPET:
            case DOUBLE_PLANT:
            case DEAD_BUSH:
            case FENCE_GATE:
            case SPRUCE_FENCE_GATE:
            case BIRCH_FENCE_GATE:
            case JUNGLE_FENCE_GATE:
            case DARK_OAK_FENCE_GATE:
            case ACACIA_FENCE_GATE:
            case SPRUCE_FENCE:
            case BIRCH_FENCE:
            case JUNGLE_FENCE:
            case DARK_OAK_FENCE:
            case ACACIA_FENCE:
            case ACACIA_STAIRS:
            case DARK_OAK_STAIRS:
                return true;
            default:
                return false;
        }
    }

    /**
     * Check if the material is a block and completely blocks vision
     *
     * @return True if this material is a block and completely blocks vision
     */
    public boolean isOccluding() {
        if (!isBlock()) {
            return false;
        }
        switch (this) {
            case STONE:
            case GRASS:
            case DIRT:
            case COBBLESTONE:
            case WOOD:
            case BEDROCK:
            case SAND:
            case GRAVEL:
            case GOLD_ORE:
            case IRON_ORE:
            case COAL_ORE:
            case LOG:
            case SPONGE:
            case LAPIS_ORE:
            case LAPIS_BLOCK:
            case DISPENSER:
            case SANDSTONE:
            case NOTE_BLOCK:
            case WOOL:
            case GOLD_BLOCK:
            case IRON_BLOCK:
            case DOUBLE_STEP:
            case BRICK:
            case BOOKSHELF:
            case MOSSY_COBBLESTONE:
            case OBSIDIAN:
            case MOB_SPAWNER:
            case DIAMOND_ORE:
            case DIAMOND_BLOCK:
            case WORKBENCH:
            case FURNACE:
            case BURNING_FURNACE:
            case REDSTONE_ORE:
            case GLOWING_REDSTONE_ORE:
            case SNOW_BLOCK:
            case CLAY:
            case JUKEBOX:
            case PUMPKIN:
            case NETHERRACK:
            case SOUL_SAND:
            case JACK_O_LANTERN:
            case MONSTER_EGGS:
            case SMOOTH_BRICK:
            case HUGE_MUSHROOM_1:
            case HUGE_MUSHROOM_2:
            case MELON_BLOCK:
            case MYCEL:
            case NETHER_BRICK:
            case ENDER_STONE:
            case REDSTONE_LAMP_OFF:
            case REDSTONE_LAMP_ON:
            case WOOD_DOUBLE_STEP:
            case EMERALD_ORE:
            case EMERALD_BLOCK:
            case COMMAND:
            case QUARTZ_ORE:
            case QUARTZ_BLOCK:
            case DROPPER:
            case STAINED_CLAY:
            case HAY_BLOCK:
            case HARD_CLAY:
            case COAL_BLOCK:
            case LOG_2:
            case PACKED_ICE:
            case SLIME_BLOCK:
            case BARRIER:
            case PRISMARINE:
            case RED_SANDSTONE:
            case DOUBLE_STONE_SLAB2:
            case PURPUR_BLOCK:
            case PURPUR_PILLAR:
            case PURPUR_DOUBLE_SLAB:
            case END_BRICKS:
            case STRUCTURE_BLOCK:
            case COMMAND_REPEATING:
            case COMMAND_CHAIN:
                return true;
            default:
                return false;
        }
    }

    /**
     * @return True if this material is affected by gravity.
     */
    public boolean hasGravity() {
        if (!isBlock()) {
            return false;
        }
        switch (this) {
            case SAND:
            case GRAVEL:
            case ANVIL:
                return true;
            default:
                return false;
        }
    }
}
