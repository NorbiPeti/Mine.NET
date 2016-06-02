using Mine.NET.block;

namespace Mine.NET.command
{
    public interface BlockCommandSender : CommandSender
    {

        /**
         * Returns the block this command sender belongs to
         *
         * @return Block for the command sender
         */
        Block getBlock();
    }
}
