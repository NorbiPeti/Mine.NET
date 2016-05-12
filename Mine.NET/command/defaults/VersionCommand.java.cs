using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace Mine.NET
{
    public class VersionCommand : BukkitCommand
    {
        public VersionCommand(String name) : base(name)
        {
            this.description = "Gets the version of this server including any plugins in use";
            this.usageMessage = "/version [plugin name]";
            this.setPermission("bukkit.command.version");
            this.setAliases(new List<string> { "ver", "about" });
        }

        public override bool execute(CommandSender sender, String currentAlias, String[] args)
        {
            if (!testPermission(sender)) return true;

            if (args.Length == 0)
            {
                sender.sendMessage("This server is running " + Bukkit.getName() + " version " + Bukkit.getVersion() + " (Implementing API version " + Bukkit.getBukkitVersion() + ")");
                sendVersion(sender);
            }
            else
            {
                StringBuilder name = new StringBuilder();

                foreach (String arg in args)
                {
                    if (name.Length > 0)
                    {
                        name.Append(' ');
                    }

                    name.Append(arg);
                }

                String pluginName = name.ToString();
                Plugin exactPlugin = Bukkit.getPluginManager().getPlugin(pluginName);
                if (exactPlugin != null)
                {
                    describeToSender(exactPlugin, sender);
                    return true;
                }

                bool found = false;
                pluginName = pluginName.ToLower();
                foreach (Plugin plugin in Bukkit.getPluginManager().getPlugins())
                { //Find: "for\s*\((.+):(.+)\)" - Replace: "foreach ($1 in $2)"
                    if (plugin.getName().ToLower().contains(pluginName))
                    {
                        describeToSender(plugin, sender);
                        found = true;
                    }
                }

                if (!found)
                {
                    sender.sendMessage("This server is not running any plugin by that name.");
                    sender.sendMessage("Use /plugins to get a list of plugins.");
                }
            }
            return true;
        }

        private void describeToSender(Plugin plugin, CommandSender sender)
        {
            PluginDescriptionFile desc = plugin.getDescription();
            sender.sendMessage(ChatColors.GREEN + desc.getName() + ChatColors.WHITE + " version " + ChatColors.GREEN + desc.getVersion());

            if (desc.getDescription() != null)
            {
                sender.sendMessage(desc.getDescription());
            }

            if (desc.getWebsite() != null)
            {
                sender.sendMessage("Website: " + ChatColors.GREEN + desc.getWebsite());
            }

            if (!desc.getAuthors().isEmpty())
            {
                if (desc.getAuthors().Count == 1)
                {
                    sender.sendMessage("Author: " + getAuthors(desc));
                }
                else
                {
                    sender.sendMessage("Authors: " + getAuthors(desc));
                }
            }
        }

        private String getAuthors(PluginDescriptionFile desc)
        {
            StringBuilder result = new StringBuilder();
            List<String> authors = desc.getAuthors();

            for (int i = 0; i < authors.Count; i++)
            {
                if (result.Length > 0)
                {
                    result.Append(ChatColors.WHITE);

                    if (i < authors.Count - 1)
                    {
                        result.Append(", ");
                    }
                    else
                    {
                        result.Append(" and ");
                    }
                }

                result.Append(ChatColors.GREEN);
                result.Append(authors[i]);
            }

            return result.ToString();
        }

        public override List<String> tabComplete(CommandSender sender, String alias, String[] args)
        {
            if (sender == null) throw new ArgumentNullException("Sender cannot be null");
            if (args == null) throw new ArgumentNullException("Arguments cannot be null");
            if (alias == null) throw new ArgumentNullException("Alias cannot be null");

            if (args.Length == 1)
            {
                List<String> completions = new List<String>();
                String toComplete = args[0].ToLower();
                foreach (Plugin plugin in Bukkit.getPluginManager().getPlugins())
                {
                    if (StringUtil.StartsWithIgnoreCase(plugin.getName(), toComplete))
                    {
                        completions.Add(plugin.getName());
                    }
                }
                return completions;
            }
            return new List<string>();
        }

        private readonly object versionLock = new object();
        private bool hasVersion = false;
        private String versionMessage = null;
        private readonly HashSet<CommandSender> versionWaiters = new HashSet<CommandSender>();
        private bool versionTaskStarted = false;
        private long lastCheck = 0;

        private void sendVersion(CommandSender sender)
        {
            if (hasVersion)
            {
                if (Environment.TickCount - lastCheck > 21600000)
                {
                    lastCheck = Environment.TickCount;
                    hasVersion = false;
                }
                else
                {
                    sender.sendMessage(versionMessage);
                    return;
                }
            }
            Monitor.Enter(versionLock);
            try
            {
                if (hasVersion)
                {
                    sender.sendMessage(versionMessage);
                    return;
                }
                versionWaiters.Add(sender);
                sender.sendMessage("Checking version, please wait...");
                if (!versionTaskStarted)
                {
                    versionTaskStarted = true;
                    new Thread(delegate ()
                    {
                        obtainVersion();
                    }
                    ).Start();
                }
            }
            finally
            {
                Monitor.Exit(versionLock);
            }
        }

        private void obtainVersion()
        {
            String version = Bukkit.getVersion();
            if (version == null) version = "Custom";
            if (version.StartsWith("git-Spigot-"))
            {
                String[] parts = version.Substring("git-Spigot-".Length).Split('-');
                int cbVersions = getDistance("craftbukkit", parts[1].Substring(0, parts[1].IndexOf(' ')));
                int spigotVersions = getDistance("spigot", parts[0]);
                if (cbVersions == -1 || spigotVersions == -1)
                {
                    setVersionMessage("Error obtaining version information");
                }
                else
                {
                    if (cbVersions == 0 && spigotVersions == 0)
                    {
                        setVersionMessage("You are running the latest version");
                    }
                    else
                    {
                        setVersionMessage("You are " + (cbVersions + spigotVersions) + " version(s) behind");
                    }
                }

            }
            else if (version.StartsWith("git-Bukkit-"))
            { //TODO
                version = version.Substring("git-Bukkit-".Length);
                int cbVersions = getDistance("craftbukkit", version.Substring(0, version.IndexOf(' ')));
                if (cbVersions == -1)
                {
                    setVersionMessage("Error obtaining version information");
                }
                else
                {
                    if (cbVersions == 0)
                    {
                        setVersionMessage("You are running the latest version");
                    }
                    else
                    {
                        setVersionMessage("You are " + cbVersions + " version(s) behind");
                    }
                }
            }
            else
            {
                setVersionMessage("Unknown version, custom build?");
            }
        }

        private void setVersionMessage(String msg)
        {
            lastCheck = Environment.TickCount;
            versionMessage = msg;
            Monitor.Enter(versionLock);
            try
            {
                hasVersion = true;
                versionTaskStarted = false;
                foreach (CommandSender sender in versionWaiters)
                {
                    sender.sendMessage(versionMessage);
                }
                versionWaiters.Clear();
            }
            finally
            {
                Monitor.Exit(versionLock);
            }
        }

        private static int getDistance(String repo, String hash)
        {
            try
            {
                var client = new WebClient(); //TODO: Encode hash - And change to own URL
                string str = client.DownloadString("https://hub.spigotmc.org/stash/rest/api/1.0/projects/SPIGOT/repos/" + repo + "/commits?since=" + (hash) + "&withCounts=true");
                JObject obj = new JObject(str);
                return (int)obj["totalCount"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }
    }
}
