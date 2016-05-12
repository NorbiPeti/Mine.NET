using System;
using System.Collections.Generic;
using System.IO;

namespace Mine.NET
{
    public class TimingsCommand : BukkitCommand {
        private static readonly List<String> TIMINGS_SUBCOMMANDS = new List<String> { "merged", "reset", "separate" };

        public TimingsCommand(String name) : base(name)
        {
            this.description = "Records timings for all plugin events";
            this.usageMessage = "/timings <reset|merged|separate>";
            this.setPermission("bukkit.command.timings");
        }
        
        public override bool execute(CommandSender sender, String currentAlias, String[] args) {
            if (!testPermission(sender)) return true;
            if (args.Length != 1) {
                sender.sendMessage(ChatColors.RED + "Usage: " + usageMessage);
                return false;
            }
            if (!sender.getServer().getPluginManager().useTimings()) {
                sender.sendMessage("Please enable timings by setting \"settings.plugin-profiling\" to true in bukkit.yml");
                return true;
            }

            bool separate = "separate".Equals(args[0], StringComparison.InvariantCultureIgnoreCase);
            if ("reset".Equals(args[0], StringComparison.InvariantCultureIgnoreCase)) {
                foreach (HandlerList handlerList in HandlerList.getHandlerLists()) {
                    foreach (RegisteredListener listener in handlerList.getRegisteredListeners()) {
                        if (listener is TimedRegisteredListener) {
                            ((TimedRegisteredListener)listener).reset();
                        }
                    }
                }
                sender.sendMessage("Timings reset");
            } else if ("merged".Equals(args[0], StringComparison.InvariantCultureIgnoreCase) || separate) {

                int index = 0;
                int pluginIdx = 0;
                DirectoryInfo timingFolder = new DirectoryInfo("timings");
                timingFolder.mkdirs();
                FileInfo timings = new FileInfo(Path.Combine(timingFolder.FullName, "timings.txt"));
                FileInfo names = null;
                while (timings.Exists) timings = new FileInfo(Path.Combine(timingFolder.FullName, "timings" + (++index) + ".txt"));
                StreamWriter fileTimings = null;
                StreamWriter fileNames = null;
                try {
                    fileTimings = new StreamWriter(timings.FullName);
                    if (separate) {
                        names = new FileInfo(Path.Combine(timingFolder.FullName, "names" + index + ".txt"));
                        fileNames = new StreamWriter(names.FullName);
                    }
                    foreach (Plugin plugin in Bukkit.getPluginManager().getPlugins()) {
                        pluginIdx++;
                        long totalTime = 0;
                        if (separate) {
                            fileNames.WriteLine(pluginIdx + " " + plugin.getDescription().getFullName());
                            fileTimings.WriteLine("Plugin " + pluginIdx);
                        }
                        else fileTimings.WriteLine(plugin.getDescription().getFullName());
                        foreach (RegisteredListener listener in HandlerList.getRegisteredListeners(plugin)) {
                            if (listener is TimedRegisteredListener) {
                                TimedRegisteredListener trl = (TimedRegisteredListener)listener;
                                long time = trl.getTotalTime();
                                int count = trl.getCount();
                                if (count == 0) continue;
                                long avg = time / count;
                                totalTime += time;
                                Type eventClass = trl.getEventClass();
                                if (count > 0 && eventClass != null) {
                                    fileTimings.WriteLine("    " + eventClass.getSimpleName() + (trl.hasMultiple() ? " (and sub-classes)" : "") + " Time: " + time + " Count: " + count + " Avg: " + avg);
                                }
                            }
                        }
                        fileTimings.WriteLine("    Total time " + totalTime + " (" + totalTime / 1000000000 + "s)");
                    }
                    sender.sendMessage("Timings written to " + timings.FullName);
                    if (separate) sender.sendMessage("Names written to " + names.FullName);
                } catch (IOException e) {
                } finally {
                    if (fileTimings != null) {
                        fileTimings.Close();
                    }
                    if (fileNames != null) {
                        fileNames.Close();
                    }
                }
            } else {
                sender.sendMessage(ChatColors.RED + "Usage: " + usageMessage);
                return false;
            }
            return true;
        }
        
        public override List<String> tabComplete(CommandSender sender, String alias, String[] args) {
            if (sender == null) throw new ArgumentNullException("Sender cannot be null");
            if (args == null) throw new ArgumentNullException("Arguments cannot be null");
            if (alias == null) throw new ArgumentNullException("Alias cannot be null");

            if (args.Length == 1) {
                return StringUtil.copyPartialMatches(args[0], TIMINGS_SUBCOMMANDS, new List<String>(TIMINGS_SUBCOMMANDS.Count));
            }
            return new List<string>();
        }
    }
}
