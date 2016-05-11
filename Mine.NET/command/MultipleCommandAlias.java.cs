using System;

namespace Mine.NET
{
    /**
     * Represents a command that delegates to one or more other commands
     */
    public class MultipleCommandAlias : Command {
        private Command[] commands;

        public MultipleCommandAlias(String name, Command[] commands) : base(name) {
            this.commands = commands;
        }

        /**
         * Gets the commands associated with the multi-command alias.
         *
         * @return commands associated with alias
         */
        public Command[] getCommands() {
            return commands;
        }
        
        public override bool execute(CommandSender sender, String commandLabel, String[] args) {
            bool result = false;

            foreach (Command command in commands) {
                result |= command.execute(sender, commandLabel, args);
            }

            return result;
        }
    }
}
