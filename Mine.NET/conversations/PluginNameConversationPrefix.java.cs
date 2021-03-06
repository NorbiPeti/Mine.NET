namespace Mine.NET.conversations;

import org.bukkit.ChatColor;
import org.bukkit.command.CommandSender;
import org.bukkit.plugin.Plugin;

/**
 * PluginNameConversationPrefix is a {@link ConversationPrefix} implementation
 * that displays the plugin name in front of conversation output.
 */
public class PluginNameConversationPrefix : ConversationPrefix {
    
    protected String separator;
    protected ChatColor prefixColor;
    protected Plugin plugin;
    
    private String cachedPrefix;
    
    public PluginNameConversationPrefix(Plugin plugin) {
        this(plugin, " > ", ChatColors.LIGHT_PURPLE);
    }
    
    public PluginNameConversationPrefix(Plugin plugin, String separator, ChatColor prefixColor) {
        this.separator = separator;
        this.prefixColor = prefixColor;
        this.plugin = plugin;

        cachedPrefix = prefixColor + plugin.getDescription().getName() + separator + ChatColors.WHITE;
    }

    /**
     * Prepends each conversation message with the plugin name.
     *
     * @param context Context information about the conversation.
     * @return An empty string.
     */
    public String getPrefix(ConversationContext context) {
        return cachedPrefix;
    }
}
