using Mine.NET.command;
using Mine.NET.command.defaults;
using System.Linq;
using System.Text;

namespace Mine.NET.help
{
    /**
     * Lacking an alternative, the help system will create instances of
     * GenericCommandHelpTopic for each command in the server's CommandMap. You
     * can use this class as a base class for custom help topics, or as an example
     * for how to write your own.
     */
    public class GenericCommandHelpTopic : HelpTopic
    {

        protected Command command;

        public GenericCommandHelpTopic(Command command)
        {
            this.command = command;

            if (command.getLabel().StartsWith("/"))
            {
                name = command.getLabel();
            }
            else
            {
                name = "/" + command.getLabel();
            }

            // The short text is the first line of the description
            int i = command.getDescription().IndexOf("\n");
            if (i > 1)
            {
                shortText = command.getDescription().Substring(0, i - 1);
            }
            else
            {
                shortText = command.getDescription();
            }

            // Build full text
            StringBuilder sb = new StringBuilder();

            sb.Append(ChatColors.GOLD);
            sb.Append("Description: ");
            sb.Append(ChatColors.WHITE);
            sb.Append(command.getDescription());

            sb.Append("\n");

            sb.Append(ChatColors.GOLD);
            sb.Append("Usage: ");
            sb.Append(ChatColors.WHITE);
            sb.Append(command.getUsage().Replace("<command>", name.Substring(1)));

            if (command.getAliases().Count > 0)
            {
                sb.Append("\n");
                sb.Append(ChatColors.GOLD);
                sb.Append("Aliases: ");
                sb.Append(ChatColors.WHITE);
                sb.Append(ChatColors.WHITE + command.getAliases().Aggregate((a, b) => a + ", " + b));
            }
            fullText = sb.ToString();
        }

        public bool canSee(CommandSender sender)
        {
            if (!command.isRegistered() && !(command is VanillaCommand))
            {
                // Unregistered commands should not show up in the help (ignore VanillaCommands)
                return false;
            }

            if (sender is ConsoleCommandSender)
            {
                return true;
            }

            if (amendedPermission != null)
            {
                return sender.hasPermission(amendedPermission);
            }
            else
            {
                return command.testPermissionSilent(sender);
            }
        }
    }
}
