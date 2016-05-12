namespace Mine.NET.command
{
    /**
     * This class is provided as a convenience to implement both TabCompleter and
     * CommandExecutor.
     */
    public interface TabExecutor : TabCompleter, CommandExecutor
    {
    }
}
