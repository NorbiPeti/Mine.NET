using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mine.NET
{
    public class SimpleCommandMap : CommandMap {
        private static readonly Regex PATTERN_ON_SPACE = new Regex(" ");
        protected readonly Dictionary<String, Command> knownCommands = new Dictionary<String, Command>();
        private readonly Server server;

        public SimpleCommandMap(Server server) {
            this.server = server;
            setDefaultCommands();
        }

        private void setDefaultCommands() {
            register("bukkit", new VersionCommand("version"));
            register("bukkit", new ReloadCommand("reload"));
            register("bukkit", new PluginsCommand("plugins"));
            register("bukkit", new TimingsCommand("timings"));
        }

        public void setFallbackCommands() {
            register("bukkit", new HelpCommand());
        }

        /**
         * {@inheritDoc}
         */
        public void registerAll(String fallbackPrefix, List<Command> commands) {
            if (commands != null) {
                for (Command c : commands) {
                    register(fallbackPrefix, c);
                }
            }
        }

        /**
         * {@inheritDoc}
         */
        public bool register(String fallbackPrefix, Command command) {
            return register(command.getName(), fallbackPrefix, command);
        }

        /**
         * {@inheritDoc}
         */
        public bool register(String label, String fallbackPrefix, Command command) {
            label = label.toLowerCase().trim();
            fallbackPrefix = fallbackPrefix.toLowerCase().trim();
            bool registered = register(label, command, false, fallbackPrefix);

            IEnumerator<String> iterator = command.getAliases().iterator();
            while (iterator.hasNext()) {
                if (!register(iterator.next(), command, true, fallbackPrefix)) {
                    iterator.remove();
                }
            }

            // If we failed to register under the real name, we need to set the command label to the direct address
            if (!registered) {
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
        private synchronized bool register(String label, Command command, bool isAlias, String fallbackPrefix) {
            knownCommands.put(fallbackPrefix + ":" + label, command);
            if ((command is VanillaCommand || isAlias) && knownCommands.containsKey(label)) {
                // Request is for an alias/fallback command and it conflicts with
                // a existing command or previous alias ignore it
                // Note: This will mean it gets removed from the commands list of active aliases
                return false;
            }

            bool registered = true;

            // If the command exists but is an alias we overwrite it, otherwise we return
            Command conflict = knownCommands.get(label);
            if (conflict != null && conflict.getLabel().equals(label)) {
                return false;
            }

            if (!isAlias) {
                command.setLabel(label);
            }
            knownCommands.put(label, command);

            return registered;
        }

        /**
         * {@inheritDoc}
         */
        public bool dispatch(CommandSender sender, String commandLine) throws CommandException {
            String [] args = PATTERN_ON_SPACE.Split(commandLine);

        if (args.length == 0) {
                return false;
            }

            String sentCommandLabel = args [0].toLowerCase();
            Command target = getCommand(sentCommandLabel);

        if (target == null) {
                return false;
            }

        try {
                // Note: we don't return the result of target.execute as thats success / failure, we return handled (true) or not handled (false)
                target.execute(sender, sentCommandLabel, Arrays_copyOfRange(args, 1, args.length));
            } catch (CommandException ex) {
                throw ex;
            } catch (Throwable ex) {
                throw new CommandException("Unhandled exception executing '" + commandLine + "' in " + target, ex);
            }

        // return true as command was handled
        return true;
        }

        public synchronized void clearCommands() {
            for (Map.Entry<String, Command> entry : knownCommands.entrySet()) {
                entry.getValue().unregister(this);
            }
            knownCommands.clear();
            setDefaultCommands();
        }

        public Command getCommand(String name) {
            Command target = knownCommands.get(name.toLowerCase());
            return target;
        }

        public List<String> tabComplete(CommandSender sender, String cmdLine) {
            if (sender == null) throw new ArgumentNullException("Sender cannot be null");
            if (cmdLine == null) throw new ArgumentNullException("Command line cannot null");

            int spaceIndex = cmdLine.indexOf(' ');

            if (spaceIndex == -1) {
                List<String> completions = new List<String>();
                Dictionary<String, Command> knownCommands = this.knownCommands;

                readonly String prefix = (sender is Player ? "/" : "");

                for (Map.Entry<String, Command> commandEntry : knownCommands.entrySet()) {
                    Command command = commandEntry.getValue();

                    if (!command.testPermissionSilent(sender)) {
                        continue;
                    }

                    String name = commandEntry.getKey(); // Use the alias, not command name

                    if (StringUtil.startsWithIgnoreCase(name, cmdLine)) {
                        completions.add(prefix + name);
                    }
                }

                Collections.sort(completions, String.CASE_INSENSITIVE_ORDER);
                return completions;
            }

            String commandName = cmdLine.substring(0, spaceIndex);
            Command target = getCommand(commandName);

            if (target == null) {
                return null;
            }

            if (!target.testPermissionSilent(sender)) {
                return null;
            }

            String argLine = cmdLine.substring(spaceIndex + 1, cmdLine.Length);
            String[] args = PATTERN_ON_SPACE.Split(argLine, -1);

            try {
                return target.tabComplete(sender, commandName, args);
            } catch (CommandException ex) {
                throw ex;
            } catch (Throwable ex) {
                throw new CommandException("Unhandled exception executing tab-completer for '" + cmdLine + "' in " + target, ex);
            }
        }

        public Collection<Command> getCommands() {
            return Collections.unmodifiableCollection(knownCommands.values());
        }

        public void registerServerAliases() {
            Dictionary<String, String[]> values = server.getCommandAliases();

            for (String alias : values.keySet()) {
                if (alias.contains(":") || alias.contains(" ")) {
                    server.getLogger().warning("Could not register alias " + alias + " because it contains illegal characters");
                    continue;
                }

                String[] commandStrings = values.get(alias);
                List<String> targets = new List<String>();
                StringBuilder bad = new StringBuilder();

                for (String commandString : commandStrings) {
                    String[] commandArgs = commandString.Split(" ");
                    Command command = getCommand(commandArgs[0]);

                    if (command == null) {
                        if (bad.Length > 0) {
                            bad.Append(", ");
                        }
                        bad.Append(commandString);
                    } else {
                        targets.add(commandString);
                    }
                }

                if (bad.Length > 0) {
                    server.getLogger().warning("Could not register alias " + alias + " because it contains commands that do not exist: " + bad);
                    continue;
                }

                // We register these as commands so they have absolute priority.
                if (targets.size() > 0) {
                    knownCommands.put(alias.toLowerCase(), new FormattedCommandAlias(alias.toLowerCase(), targets.toArray(new String[targets.size()])));
                } else {
                    knownCommands.remove(alias.toLowerCase());
                }
            }
        }
    }
}
