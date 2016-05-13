using Mine.NET.command;
using System;

namespace Mine.NET.Event.server
{
    /**
     * This event is called when a command is recieved over RCON. See the javadocs
     * of {@link ServerCommandEvent} for more information.
     */
    public class RemoteServerCommandEvent : ServerCommandEvent
    {
        private static readonly HandlerList handlers = new HandlerList();

        public RemoteServerCommandEvent(CommandSender sender, String command) : base(sender, command)
        {
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
