using System;
using System.Collections.Generic;
using System.Text;

namespace Mine.NET
{
/**
 * Represents a {@link Command} belonging to a plugin
 */
public sealed class PluginCommand : Command, PluginIdentifiableCommand {
    private readonly Plugin owningPlugin;
    private CommandExecutor executor;
    private TabCompleter completer;

    internal PluginCommand(String name, Plugin owner) : base(name) {
        this.executor = owner;
        this.owningPlugin = owner;
        this.usageMessage = "";
    }

    /**
     * Executes the command, returning its success
     *
     * @param sender Source object which is executing this command
     * @param commandLabel The alias of the command used
     * @param args All arguments passed to the command, split via ' '
     * @return true if the command was successful, otherwise false
     */
    public override bool execute(CommandSender sender, String commandLabel, String[] args) {
        bool success = false;

        if (!owningPlugin.isEnabled()) {
            return false;
        }

        if (!testPermission(sender)) {
            return true;
        }

        try {
            success = executor.onCommand(sender, this, commandLabel, args);
        } catch (Exception ex) {
            throw new CommandException("Unhandled exception executing command '" + commandLabel + "' in plugin " + owningPlugin.getDescription().getFullName(), ex);
        }

        if (!success && usageMessage.Length > 0) {
            foreach (String line in usageMessage.Replace("<command>", commandLabel).Split('\n')) {
                sender.sendMessage(line);
            }
        }

        return success;
    }

    /**
     * Sets the {@link CommandExecutor} to run when parsing this command
     *
     * @param executor New executor to run
     */
    public void setExecutor(CommandExecutor executor) {
        this.executor = executor == null ? owningPlugin : executor;
    }

    /**
     * Gets the {@link CommandExecutor} associated with this command
     *
     * @return CommandExecutor object linked to this command
     */
    public CommandExecutor getExecutor() {
        return executor;
    }

    /**
     * Sets the {@link TabCompleter} to run when tab-completing this command.
     * <p>
     * If no TabCompleter is specified, and the command's executor :
     * TabCompleter, then the executor will be used for tab completion.
     *
     * @param completer New tab completer
     */
    public void setTabCompleter(TabCompleter completer) {
        this.completer = completer;
    }

    /**
     * Gets the {@link TabCompleter} associated with this command.
     *
     * @return TabCompleter object linked to this command
     */
    public TabCompleter getTabCompleter() {
        return completer;
    }

    /**
     * Gets the owner of this PluginCommand
     *
     * @return Plugin that owns this command
     */
    public Plugin getPlugin() {
        return owningPlugin;
    }

    /**
     * {@inheritDoc}
     * <p>
     * Delegates to the tab completer if present.
     * <p>
     * If it is not present or returns null, will delegate to the current
     * command executor if it : {@link TabCompleter}. If a non-null
     * list has not been found, will default to standard player name
     * completion in {@link
     * Command#tabComplete(CommandSender, String, String[])}.
     * <p>
     * This method does not consider permissions.
     *
     * @throws CommandException if the completer or executor throw an
     *     exception during the process of tab-completing.
     * @throws ArgumentException if sender, alias, or args is null
     */
    public override List<String> tabComplete(CommandSender sender, String alias, String[] args) {
        if(sender==null) throw new ArgumentNullException("Sender cannot be null");
        if(args==null) throw new ArgumentNullException("Arguments cannot be null");
        if(alias==null) throw new ArgumentNullException("Alias cannot be null");

        List<String> completions = null;
        try {
            if (completer != null) {
                completions = completer.onTabComplete(sender, this, alias, args);
            }
            if (completions == null && executor is TabCompleter) {
                completions = ((TabCompleter) executor).onTabComplete(sender, this, alias, args);
            }
        } catch (Exception ex) {
            StringBuilder message = new StringBuilder();
            message.Append("Unhandled exception during tab completion for command '/").append(alias).append(' ');
            foreach (String arg in args) {
                message.Append(arg).Append(' ');
            }
            message.Remove(message.Length - 1, 1).Append("' in plugin ").Append(owningPlugin.getDescription().getFullName());
            throw new CommandException(message.ToString(), ex);
        }

        if (completions == null) {
            return base.tabComplete(sender, alias, args);
        }
        return completions;
    }

    public override string ToString() {
        StringBuilder stringBuilder = new StringBuilder(base.ToString());
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append(", ").Append(owningPlugin.getDescription().getFullName()).Append(')');
        return stringBuilder.ToString();
    }
}
}
