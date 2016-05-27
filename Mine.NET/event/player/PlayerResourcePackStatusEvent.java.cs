using Mine.NET.entity;

namespace Mine.NET.Event.player
{
    /**
     * Called when a player takes action on a resource pack request sent via
     * {@link Player#setResourcePack(java.lang.String)}.
     */
    public class PlayerResourcePackStatusEventArgs : PlayerEventArgs
    {
        private readonly Status status;

        public PlayerResourcePackStatusEventArgs(Player who, Status resourcePackStatus) : base(who)
        {
            this.status = resourcePackStatus;
        }

        /**
         * Gets the status of this pack.
         *
         * @return the current status
         */
        public Status getStatus()
        {
            return status;
        }

        /**
         * Status of the resource pack.
         */
        public enum Status
        {

            /**
             * The resource pack has been successfully downloaded and applied to the
             * client.
             */
            SUCCESSFULLY_LOADED,
            /**
             * The client refused to accept the resource pack.
             */
            DECLINED,
            /**
             * The client accepted the pack, but download failed.
             */
            FAILED_DOWNLOAD,
            /**
             * The client accepted the pack and is beginning a download of it.
             */
            ACCEPTED
        }
    }
}
