namespace Mine.NET
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
