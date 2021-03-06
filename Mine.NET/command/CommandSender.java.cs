using Mine.NET.entity;
using System;

namespace Mine.NET.command
{
    public interface CommandSender : Permissible, INamedEntity
    {

        /**
         * Sends this sender a message
         *
         * @param message Message to be displayed
         */
        void sendMessage(String message);

        /**
         * Sends this sender multiple messages
         *
         * @param messages An array of messages to be displayed
         */
        void sendMessage(String[] messages);

        /**
         * Returns the server instance that this command is running on
         *
         * @return Server instance
         */
        Server getServer();
    }
}
