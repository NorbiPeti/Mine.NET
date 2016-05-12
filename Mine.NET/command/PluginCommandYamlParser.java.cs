using System;
using System.Collections;
using System.Collections.Generic;

namespace Mine.NET
{
    public class PluginCommandYamlParser {

        public static List<Command> parse(Plugin plugin) {
            List<Command> pluginCmds = new List<Command>();

            Dictionary<String, Dictionary<String, Object>> map = plugin.getDescription().getCommands();

            if (map == null) {
                return pluginCmds;
            }

            foreach (KeyValuePair<String, Dictionary<String, Object>> entry in map) {
                if (entry.Key.Contains(":")) {
                    Bukkit.getServer().getLogger().Severe("Could not load command " + entry.Key + " for plugin " + plugin.getName() + ": Illegal chars");
                    continue;
                }
                Command newCmd = new PluginCommand(entry.Key, plugin);
                Object description = entry.Value["description"];
                Object usage = entry.Value["usage"];
                Object aliases = entry.Value["aliases"];
                Object permission = entry.Value["permission"];
                Object permissionMessage = entry.Value["permission-message"];

                if (description != null) {
                    newCmd.setDescription(description.ToString());
                }

                if (usage != null) {
                    newCmd.setUsage(usage.ToString());
                }

                if (aliases != null) {
                    List<String> aliasList = new List<String>();

                    if (aliases is IList && aliases.GetType().IsGenericType)
                    {
                        foreach (Object o in (IList) aliases) {
                            if (o.ToString().Contains(":")) {
                                Bukkit.getServer().getLogger().Severe("Could not load alias " + o.ToString() + " for plugin " + plugin.getName() + ": Illegal chars");
                                continue;
                            }
                            aliasList.Add(o.ToString());
                        }
                    } else {
                        if (aliases.ToString().Contains(":")) {
                            Bukkit.getServer().getLogger().Severe("Could not load alias " + aliases.ToString() + " for plugin " + plugin.getName() + ": Illegal chars");
                        } else {
                            aliasList.Add(aliases.ToString());
                        }
                    }

                    newCmd.setAliases(aliasList);
                }

                if (permission != null) {
                    newCmd.setPermission(permission.ToString());
                }

                if (permissionMessage != null) {
                    newCmd.setPermissionMessage(permissionMessage.ToString());
                }

                pluginCmds.Add(newCmd);
            }
            return pluginCmds;
        }
    }
}
    