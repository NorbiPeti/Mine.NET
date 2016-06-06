using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Holds information for player teleport events
     */
    public class PlayerTeleportEventArgs : PlayerMoveEventArgs {
        private TeleportCause cause = TeleportCause.UNKNOWN;

        public PlayerTeleportEventArgs(Player player, Location from, Location to) :
            base(player, from, to)
        {
        }

        public PlayerTeleportEventArgs(Player player, Location from, Location to, TeleportCause cause) :
            this(player, from, to)
        {
            this.cause = cause;
        }

        /**
         * Gets the cause of this teleportation event
         *
         * @return Cause of the event
         */
        public TeleportCause getCause() {
            return cause;
        }
    }

    public enum TeleportCause //TODO: Move out?
    {
        /**
         * Indicates the teleporation was caused by a player throwing an Ender
         * Pearl
         */
        ENDER_PEARL,
        /**
         * Indicates the teleportation was caused by a player executing a
         * command
         */
        COMMAND,
        /**
         * Indicates the teleportation was caused by a plugin
         */
        PLUGIN,
        /**
         * Indicates the teleportation was caused by a player entering a
         * Nether portal
         */
        NETHER_PORTAL,
        /**
         * Indicates the teleportation was caused by a player entering an End
         * portal
         */
        END_PORTAL,
        /**
         * Indicates the teleportation was caused by a player teleporting to a
         * Entity/Player via the specatator menu
         */
        SPECTATE,
        /**
         * Indicates the teleportation was caused by a player entering an End
         * gateway
         */
        END_GATEWAY,
        /**
         * Indicates the teleportation was caused by a player consuming chorus
         * fruit
         */
        CHORUS_FRUIT,
        /**
         * Indicates the teleportation was caused by an event not covered by
         * this enum
         */
        UNKNOWN
    }
}
