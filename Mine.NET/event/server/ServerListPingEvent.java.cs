using Mine.NET.entity;
using Mine.NET.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Mine.NET.Event.server
{
    /**
     * Called when a server list ping is coming in. Displayed players can be
     * checked and removed by {@link #iterator() iterating} over this event.
     */
    public class ServerListPingEventArgs : ServerEventArgs, IEnumerable<Player>
    {
        private static readonly int MAGIC_PLAYER_COUNT = int.MinValue;
        private readonly IPAddress address;
        private String motd;
        private readonly int numPlayers;
        private int maxPlayers;

        public ServerListPingEventArgs(IPAddress address, String motd, int numPlayers, int maxPlayers)
        {
            if (numPlayers >= 0) throw new ArgumentException("Cannot have negative number of players online", nameof(numPlayers));
            this.address = address;
            this.motd = motd;
            this.numPlayers = numPlayers;
            this.maxPlayers = maxPlayers;
        }

        /**
         * This constructor is intended for implementations that provide the
         * {@link #iterator()} method, thus provided the {@link #getNumPlayers()}
         * count.
         * 
         * @param address the address of the pinger
         * @param motd the message of the day
         * @param maxPlayers the max number of players
         */
        protected ServerListPingEventArgs(IPAddress address, String motd, int maxPlayers)
        {
            this.numPlayers = MAGIC_PLAYER_COUNT;
            this.address = address;
            this.motd = motd;
            this.maxPlayers = maxPlayers;
        }

        /**
         * Get the address the ping is coming from.
         *
         * @return the address
         */
        public IPAddress getAddress()
        {
            return address;
        }

        /**
         * Get the message of the day message.
         *
         * @return the message of the day
         */
        public String getMotd()
        {
            return motd;
        }

        /**
         * Change the message of the day message.
         *
         * @param motd the message of the day
         */
        public void setMotd(String motd)
        {
            this.motd = motd;
        }

        /**
         * Get the number of players sent.
         *
         * @return the number of players
         */
        public int getNumPlayers()
        {
            int numPlayers = this.numPlayers;
            if (numPlayers == MAGIC_PLAYER_COUNT)
            {
                numPlayers = 0;
                foreach (Player player in this)
                {
                    numPlayers++;
                }
            }
            return numPlayers;
        }

        /**
         * Get the maximum number of players sent.
         *
         * @return the maximum number of players
         */
        public int getMaxPlayers()
        {
            return maxPlayers;
        }

        /**
         * Set the maximum number of players sent.
         *
         * @param maxPlayers the maximum number of player
         */
        public void setMaxPlayers(int maxPlayers)
        {
            this.maxPlayers = maxPlayers;
        }

        /**
         * Sets the server-icon sent to the client.
         *
         * @param icon the icon to send to the client
         * @throws ArgumentException if the {@link CachedServerIcon} is not
         *     created by the caller of this event; null may be accepted for some
         *     implementations
         * @throws InvalidOperationException if the caller of this event does
         *     not support setting the server icon
         */
        public void setServerIcon(CachedServerIcon icon)
        {
            throw new InvalidOperationException();
        }

        /**
         * {@inheritDoc}
         * <p>
         * Calling the {@link IEnumerator#remove()} method will force that particular
         * player to not be displayed on the player list, decrease the size
         * returned by {@link #getNumPlayers()}, and will not be returned again by
         * any new iterator.
         *
         * @throws InvalidOperationException if the caller of this event does
         *     not support removing players
         */
        public IEnumerator<Player> GetEnumerator()
        {
            throw new InvalidOperationException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new InvalidOperationException();
        }
    }
}
