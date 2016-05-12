using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mine.NET
{
    public class SimpleCommandMap : CommandMap
    {
        private static readonly Regex PATTERN_ON_SPACE = new Regex(" ");
        protected readonly Dictionary<String, Command> knownCommands = new Dictionary<String, Command>();
        private readonly Server server;

        public SimpleCommandMap(Server server)
        {
            this.server = server;
            setDefaultCommands();
        }

        private void setDefaultCommands()
        {
            register("bukkit", new VersionCommand("version"));
            register("bukkit", new ReloadCommand("reload"));
            register("bukkit", new PluginsCommand("plugins"));
            register("bukkit", new TimingsCommand("timings"));
        }

        public void setFallbackCommands()
        {
            register("bukkit", new HelpCommand());
        }

        /**
         * {@inheritDoc}
         */
        public void registerAll(String fallbackPrefix, List<Command> commands)
        {
            if (commands != null)
            {
                foreach (Command c in commands)
                {
                    register(fallbackPrefix, c);
                }
            }
        }

        /**
         * {@inheritDoc}
         */
        public bool register(String fallbackPrefix, Command command)
        {
            return register(command.getName(), fallbackPrefix, command);
        }

        /**
         * {@inheritDoc}
         */
        public bool register(String label, String fallbackPrefix, Command command)
        {
            label = label.ToLower().Trim();
            fallbackPrefix = fallbackPrefix.ToLower().Trim();
            bool registered = register(label, command, false, fallbackPrefix);

            command.getAliases().RemoveAll(a => !register(a, command, true, fallbackPrefix));

            // If we failed to register under the real name, we need to set the command label to the direct address
            if (!registered)
            {
                command.setLabel(fallbackPrefix + ":" + label);
            }

            // Register to us so further updates of the commands label and aliases are postponed until its reregistered
            command.register(this);

            return registered;
        }

        /**
         * Registers a command with the given name is possible. Also uses
         * fallbackPrefix to create a unique name.
         *
         * @param label the name of the command, without the '/'-prefix.
         * @param command the command to register
         * @param isAlias whether the command is an alias
         * @param fallbackPrefix a prefix which is prepended to the command for a
         *     unique address
         * @return true if command was registered, false otherwise.
         */
        private bool register(String label, Command command, bool isAlias, String fallbackPrefix)
        {
            knownCommands.Add(fallbackPrefix + ":" + label, command);
            if ((command is VanillaCommand || isAlias) && knownCommands.ContainsKey(label))
            {
                // Request is for an alias/fallback command and it conflicts with
                // a existing command or previous alias ignore it
                // Note: This will mean it gets removed from the commands list of active aliases
                return false;
            }

            bool registered = true;

            // If the command exists but is an alias we overwrite it, otherwise we return
            Command conflict = knownCommands[label];
            if (conflict != null && conflict.getLabel().Equals(label))
            {
                return false;
            }

            if (!isAlias)
            {
                command.setLabel(label);
            }
            knownCommands.Add(label, command);

            return registered;
        }

        /**
         * {@inheritDoc}
         */
        public bool dispatch(CommandSender sender, String commandLine)
        {
            String[] args = PATTERN_ON_SPACE.Split(commandLine);

            if (args.Length == 0)
            {
                return false;
            }

            String sentCommandLabel = args[0].ToLower();
            Command target = getCommand(sentCommandLabel);

            if (target == null)
            {
                return false;
            }

            try
            {
                // Note: we don't return the result of target.execute as thats success / failure, we return handled (true) or not handled (false)
                var arr = new string[args.Length - 1];
                Array.Copy(args, 1, arr, 0, args.Length);
                target.execute(sender, sentCommandLabel, arr);
            }
            catch (CommandException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommandException("Unhandled exception executing '" + commandLine + "' in " + target, ex);
            }

            // return true as command was handled
            return true;
        }

        public void clearCommands()
        {
            foreach (KeyValuePair<String, Command> entry in knownCommands)
            {
                entry.Value.unregister(this);
            }
            knownCommands.Clear();
            setDefaultCommands();
        }

        public Command getCommand(String name)
        {
            Command target = knownCommands[name.ToLower()];
            return target;
        }

        public List<String> tabComplete(CommandSender sender, String cmdLine)
        {
            if (sender == null) throw new ArgumentNullException("Sender cannot be null");
            if (cmdLine == null) throw new ArgumentNullException("Command line cannot null");

            int spaceIndex = cmdLine.IndexOf(' ');

            if (spaceIndex == -1)
            {
                List<String> completions = new List<String>();
                Dictionary<String, Command> knownCommands = this.knownCommands;

                String prefix = (sender is Player ? "/" : "");

                foreach (KeyValuePair<String, Command> commandEntry in knownCommands)
                {
                    Command command = commandEntry.Value;

                    if (!command.testPermissionSilent(sender))
                    {
                        continue;
                    }

                    String name = commandEntry.Key; // Use the alias, not command name

                    if (StringUtil.startsWithIgnoreCase(name, cmdLine))
                    {
                        completions.Add(prefix + name);
                    }
                }

                completions.Sort();
                return completions;
            }

            String commandName = cmdLine.Substring(0, spaceIndex);
            Command target = getCommand(commandName);

            if (target == null)
            {
                return null;
            }

            if (!target.testPermissionSilent(sender))
            {
                return null;
            }

            String argLine = cmdLine.Substring(spaceIndex + 1, cmdLine.Length);
            String[] args = PATTERN_ON_SPACE.Split(argLine, -1);

            try
            {
                return target.tabComplete(sender, commandName, args);
            }
            catch (CommandException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommandException("Unhandled exception executing tab-completer for '" + cmdLine + "' in " + target, ex);
            }
        }

        public ReadOnlyCollection<Command> getCommands()
        {
            return new ReadOnlyCollection<Command>(knownCommands.Values.ToList());
        }

        public void registerServerAliases()
        {
            Dictionary<String, String[]> values = server.getCommandAliases();

            foreach (String alias in values.Keys)
            {
                if (alias.Contains(":") || alias.Contains(" "))
                {
                    server.getLogger().Warning("Could not register alias " + alias + " because it contains illegal chars");
                    continue;
                }

                String[] commandStrings = values[alias];
                List<String> targets = new List<String>();
                StringBuilder bad = new StringBuilder();

                foreach (String commandString in commandStrings)
                {
                    String[] commandArgs = commandString.Split(' ');
                    Command command = getCommand(commandArgs[0]);

                    if (command == null)
                    {
                        if (bad.Length > 0)
                        {
                            bad.Append(", ");
                        }
                        bad.Append(commandString);
                    }
                    else
                    {
                        targets.Add(commandString);
                    }
                }

                if (bad.Length > 0)
                {
                    server.getLogger().Warning("Could not register alias " + alias + " because it contains commands that do not exist: " + bad);
                    continue;
                }

                // We register these as commands so they have absolute priority.
                if (targets.Count > 0)
                {
                    knownCommands.Add(alias.ToLower(), new FormattedCommandAlias(alias.ToLower(), targets.ToArray()));
                }
                else
                {
                    knownCommands.Remove(alias.ToLower());
                }
            }
        }
    }
}
