using Mine.NET.command;
using System;

namespace Mine.NET.Event.server
{
    /**
     * This event is called when a command is recieved over RCON. See the javadocs
     * of {@link ServerCommandEvent} for more information.
     */
    public class RemoteServerCommandEventArgs : ServerCommandEventArgs
    {
        public RemoteServerCommandEventArgs(CommandSender sender, String command) : base(sender, command)
        {
        }
    }
}
