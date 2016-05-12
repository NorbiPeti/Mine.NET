namespace Mine.NET.conversations;

import java.util.EventListener;

/**
 */
public interface ConversationAbandonedListener : EventListener {
    /**
     * Called whenever a {@link Conversation} is abandoned.
     *
     * @param abandonedEvent Contains details about the abandoned
     *     conversation.
     */
    public void conversationAbandoned(ConversationAbandonedEvent abandonedEvent);
}
