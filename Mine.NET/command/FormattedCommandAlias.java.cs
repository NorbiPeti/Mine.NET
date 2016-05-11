using System;
using System.Collections.Generic;
using System.Text;

namespace Mine.NET
{
    public class FormattedCommandAlias : Command {
        private readonly String[] formatStrings;

        public FormattedCommandAlias(String alias, String[] formatStrings) : base(alias) {
            this.formatStrings = formatStrings;
        }
        
        public override bool execute(CommandSender sender, String commandLabel, String[] args) {
            bool result = false;
            List<String> commands = new List<String>();
            foreach (String formatString in formatStrings) {
                try {
                    commands.Add(buildCommand(formatString, args));
                } catch (Exception throwable) {
                    if (throwable is ArgumentException) {
                        sender.sendMessage(throwable.Message);
                    } else {
                        sender.sendMessage(ChatColor.Colors.RED + "An internal error occurred while attempting to perform this command");
                    }
                    return false;
                }
            }

            foreach (String command in commands) {
                result |= Bukkit.dispatchCommand(sender, command);
            }

            return result;
        }

        private String buildCommand(String formatString, String[] args) {
            int index = formatString.IndexOf("$");
            while (index != -1) {
                int start = index;

                if (index > 0 && formatString[start - 1] == '\\') {
                    formatString = formatString.Substring(0, start - 1) + formatString.Substring(start);
                    index = formatString.IndexOf("$", index);
                    continue;
                }

                bool required = false;
                if (formatString[index + 1] == '$') {
                    required = true;
                    // Move index past the second $
                    index++;
                }

                // Move index past the $
                index++;
                int argStart = index;
                while (index < formatString.Length && inRange(((int)formatString[index]) - 48, 0, 9)) {
                    // Move index past current digit
                    index++;
                }

                // No numbers found
                if (argStart == index) {
                    throw new ArgumentException("Invalid replacement token");
                }

                int position = int.Parse(formatString.Substring(argStart, index));

                // Arguments are not 0 indexed
                if (position == 0) {
                    throw new ArgumentException("Invalid replacement token");
                }

                // Convert position to 0 index
                position--;

                bool rest = false;
                if (index < formatString.Length && formatString[index] == '-') {
                    rest = true;
                    // Move index past the -
                    index++;
                }

                int end = index;

                if (required && position >= args.Length) {
                    throw new ArgumentException("Missing required argument " + (position + 1));
                }

                StringBuilder replacement = new StringBuilder();
                if (rest && position < args.Length) {
                    for (int i = position; i < args.Length; i++) {
                        if (i != position) {
                            replacement.Append(' ');
                        }
                        replacement.Append(args[i]);
                    }
                } else if (position < args.Length) {
                    replacement.Append(args[position]);
                }

                formatString = formatString.Substring(0, start) + replacement.ToString() + formatString.Substring(end);
                // Move index past the replaced data so we don't process it again
                index = start + replacement.Length;

                // Move to the next replacement token
                index = formatString.IndexOf("$", index);
            }

            return formatString;
        }

        private static bool inRange(int i, int j, int k) {
            return i >= j && i <= k;
        }
    }
}
