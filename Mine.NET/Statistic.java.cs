using System.Collections.Generic;

namespace Mine.NET
{
    /**
    * Represents a countable statistic, which is tracked by the server.
    */
    public class Statistic
    {
        public enum Stats
        {
            DAMAGE_DEALT,
            DAMAGE_TAKEN,
            DEATHS,
            MOB_KILLS,
            PLAYER_KILLS,
            FISH_CAUGHT,
            ANIMALS_BRED,
            TREASURE_FISHED,
            JUNK_FISHED,
            LEAVE_GAME,
            JUMP,
            DROP,
            PICKUP,
            PLAY_ONE_TICK,
            WALK_ONE_CM,
            SWIM_ONE_CM,
            FALL_ONE_CM,
            SNEAK_TIME,
            CLIMB_ONE_CM,
            FLY_ONE_CM,
            DIVE_ONE_CM,
            MINECART_ONE_CM,
            BOAT_ONE_CM,
            PIG_ONE_CM,
            HORSE_ONE_CM,
            SPRINT_ONE_CM,
            CROUCH_ONE_CM,
            AVIATE_ONE_CM,
            MINE_BLOCK,
            USE_ITEM,
            BREAK_ITEM,
            CRAFT_ITEM,
            KILL_ENTITY,
            ENTITY_KILLED_BY,
            TIME_SINCE_DEATH,
            TALKED_TO_VILLAGER,
            TRADED_WITH_VILLAGER,
            CAKE_SLICES_EATEN,
            CAULDRON_FILLED,
            CAULDRON_USED,
            ARMOR_CLEANED,
            BANNER_CLEANED,
            BREWINGSTAND_INTERACTION,
            BEACON_INTERACTION,
            DROPPER_INSPECTED,
            HOPPER_INSPECTED,
            DISPENSER_INSPECTED,
            NOTEBLOCK_PLAYED,
            NOTEBLOCK_TUNED,
            FLOWER_POTTED,
            TRAPPED_CHEST_TRIGGERED,
            ENDERCHEST_OPENED,
            ITEM_ENCHANTED,
            RECORD_PLAYED,
            FURNACE_INTERACTION,
            CRAFTING_TABLE_INTERACTION,
            CHEST_OPENED,
            SLEEP_IN_BED
        }
        public static readonly Dictionary<Stats, Statistic> AllStats = new Dictionary<Stats, Statistic>()
    {
        { Stats.DAMAGE_DEALT, new Statistic() },
        { Stats.DAMAGE_TAKEN, new Statistic() },
        { Stats.DEATHS, new Statistic() },
        { Stats.MOB_KILLS, new Statistic() },
        { Stats.PLAYER_KILLS, new Statistic() },
        { Stats.FISH_CAUGHT, new Statistic() },
        { Stats.ANIMALS_BRED, new Statistic() },
        { Stats.TREASURE_FISHED, new Statistic() },
        { Stats.JUNK_FISHED, new Statistic() },
        { Stats.LEAVE_GAME, new Statistic() },
        { Stats.JUMP, new Statistic() },
        { Stats.DROP, new Statistic(StatisticType.ITEM) },
        { Stats.PICKUP, new Statistic(StatisticType.ITEM) },
        { Stats.PLAY_ONE_TICK, new Statistic() },
        { Stats.WALK_ONE_CM, new Statistic() },
        { Stats.SWIM_ONE_CM, new Statistic() },
        { Stats.FALL_ONE_CM, new Statistic() },
        { Stats.SNEAK_TIME, new Statistic() },
        { Stats.CLIMB_ONE_CM, new Statistic() },
        { Stats.FLY_ONE_CM, new Statistic() },
        { Stats.DIVE_ONE_CM, new Statistic() },
        { Stats.MINECART_ONE_CM, new Statistic() },
        { Stats.BOAT_ONE_CM, new Statistic() },
        { Stats.PIG_ONE_CM, new Statistic() },
        { Stats.HORSE_ONE_CM, new Statistic() },
        { Stats.SPRINT_ONE_CM, new Statistic() },
        { Stats.CROUCH_ONE_CM, new Statistic() },
        { Stats.AVIATE_ONE_CM, new Statistic() },
        { Stats.MINE_BLOCK, new Statistic(StatisticType.BLOCK) },
        { Stats.USE_ITEM, new Statistic(StatisticType.ITEM) },
        { Stats.BREAK_ITEM, new Statistic(StatisticType.ITEM) },
        { Stats.CRAFT_ITEM, new Statistic(StatisticType.ITEM) },
        { Stats.KILL_ENTITY, new Statistic(StatisticType.ENTITY) },
        { Stats.ENTITY_KILLED_BY, new Statistic(StatisticType.ENTITY) },
        { Stats.TIME_SINCE_DEATH, new Statistic() },
        { Stats.TALKED_TO_VILLAGER, new Statistic() },
        { Stats.TRADED_WITH_VILLAGER, new Statistic() },
        { Stats.CAKE_SLICES_EATEN, new Statistic() },
        { Stats.CAULDRON_FILLED, new Statistic() },
        { Stats.CAULDRON_USED, new Statistic() },
        { Stats.ARMOR_CLEANED, new Statistic() },
        { Stats.BANNER_CLEANED, new Statistic() },
        { Stats.BREWINGSTAND_INTERACTION, new Statistic() },
        { Stats.BEACON_INTERACTION, new Statistic() },
        { Stats.DROPPER_INSPECTED, new Statistic() },
        { Stats.HOPPER_INSPECTED, new Statistic() },
        { Stats.DISPENSER_INSPECTED, new Statistic() },
        { Stats.NOTEBLOCK_PLAYED, new Statistic() },
        { Stats.NOTEBLOCK_TUNED, new Statistic() },
        { Stats.FLOWER_POTTED, new Statistic() },
        { Stats.TRAPPED_CHEST_TRIGGERED, new Statistic() },
        { Stats.ENDERCHEST_OPENED, new Statistic() },
        { Stats.ITEM_ENCHANTED, new Statistic() },
        { Stats.RECORD_PLAYED, new Statistic() },
        { Stats.FURNACE_INTERACTION, new Statistic() },
        { Stats.CRAFTING_TABLE_INTERACTION, new Statistic() },
        { Stats.CHEST_OPENED, new Statistic() },
        { Stats.SLEEP_IN_BED, new Statistic() }
    };

        private readonly StatisticType type;

        private Statistic()
        {
            new Statistic(StatisticType.UNTYPED);
        }

        private Statistic(StatisticType type)
        {
            this.type = type;
        }

        /**
         * Gets the type of this statistic.
         *
         * @return the type of this statistic
         */
        public StatisticType getType()
        {
            return type;
        }

        /**
         * Checks if this is a substatistic.
         * <p>
         * A substatistic exists en masse for each block, item, or entitytype, depending on
         * {@link #getType()}.
         * <p>
         * This is a redundant method and equivalent to checking
         * <code>getType() != Type.UNTYPED</code>
         *
         * @return true if this is a substatistic
         */
        public bool isSubstatistic()
        {
            return type != StatisticType.UNTYPED;
        }

        /**
         * Checks if this is a substatistic dealing with blocks.
         * <p>
         * This is a redundant method and equivalent to checking
         * <code>getType() == Type.BLOCK</code>
         *
         * @return true if this deals with blocks
         */
        public bool isBlock()
        {
            return type == StatisticType.BLOCK;
        }

        /**
         * The type of statistic.
         *
         */
        public enum StatisticType
        {
            /**
             * Statistics of this type do not require a qualifier.
             */
            UNTYPED,

            /**
             * Statistics of this type require an Item Materials qualifier.
             */
            ITEM,

            /**
             * Statistics of this type require a Block Materials qualifier.
             */
            BLOCK,

            /**
             * Statistics of this type require an EntityType qualifier.
             */
            ENTITY
        }
    }
}
