package org.bukkit.command.defaults;

import java.util.List;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import org.apache.commons.lang.Validate;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;
import org.bukkit.inventory.ItemStack;
import org.bukkit.util.StringUtil;

import com.google.common.base.Joiner;
import com.google.common.collect.ImmutableList;

[Obsolete]
public class GiveCommand : VanillaCommand {
    private static List<String> materials;
    static {
        List<String> materialList = new List<String>();
        for (Material material : Material.values()) {
            materialList.add(material.name());
        }
        Collections.sort(materialList);
        materials = ImmutableList.copyOf(materialList);
    }

    public GiveCommand() {
        base("give");
        this.description = "Gives the specified player a certain amount of items";
        this.usageMessage = "/give <player> <item> [amount [data]]";
        this.setPermission("bukkit.command.give");
    }

    @Override
    public bool execute(CommandSender sender, String currentAlias, String[] args) {
        if (!testPermission(sender)) return true;
        if ((args.length < 2)) {
            sender.sendMessage(ChatColor.RED + "Usage: " + usageMessage);
            return false;
        }

        Player player = Bukkit.getPlayerExact(args[0]);

        if (player != null) {
            Material material = Material.matchMaterial(args[1]);

            if (material == null) {
                material = Bukkit.getUnsafe().getMaterialFromInternalName(args[1]);
            }

            if (material != null) {
                int amount = 1;
                short data = 0;

                if (args.length >= 3) {
                    amount = this.getInteger(sender, args[2], 1, 64);

                    if (args.length >= 4) {
                        try {
                            data = Short.parseShort(args[3]);
                        } catch (NumberFormatException ex) {}
                    }
                }

                ItemStack stack = new ItemStack(material, amount, data);

                if (args.length >= 5) {
                    try {
                        stack = Bukkit.getUnsafe().modifyItemStack(stack, Joiner.on(' ').join(Arrays.asList(args).subList(4, args.length)));
                    } catch (Throwable t) {
                        player.sendMessage("Not a valid tag");
                        return true;
                    }
                }

                player.getInventory().addItem(stack);

                Command.broadcastCommandMessage(sender, "Gave " + player.getName() + " some " + material.getId() + " (" + material + ")");
            } else {
                sender.sendMessage("There's no item called " + args[1]);
            }
        } else {
            sender.sendMessage("Can't find player " + args[0]);
        }

        return true;
    }

    @Override
    public List<String> tabComplete(CommandSender sender, String alias, String[] args) throws ArgumentException {
        if(sender==null) throw new ArgumentNullException("Sender cannot be null");
        if(args==null) throw new ArgumentNullException("Arguments cannot be null");
        if(alias==null) throw new ArgumentNullException("Alias cannot be null");

        if (args.length == 1) {
            return base.tabComplete(sender, alias, args);
        }
        if (args.length == 2) {
            readonly String arg = args[1];
            readonly List<String> materials = GiveCommand.materials;
            List<String> completion = new List<String>();

            readonly int size = materials.size();
            int i = Collections.binarySearch(materials, arg, String.CASE_INSENSITIVE_ORDER);

            if (i < 0) {
                // Insertion (start) index
                i = -1 - i;
            }

            for ( ; i < size; i++) {
                String material = materials.get(i);
                if (StringUtil.startsWithIgnoreCase(material, arg)) {
                    completion.add(material);
                } else {
                    break;
                }
            }

            return Bukkit.getUnsafe().tabCompleteInternalMaterialName(arg, completion);
        }
        return ImmutableList.of();
    }
}
