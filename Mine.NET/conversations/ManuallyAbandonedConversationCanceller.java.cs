namespace Mine.NET.conversations;

/**
 * The ManuallyAbandonedConversationCanceller is only used as part of a {@link
 * ConversationAbandonedEvent} to indicate that the conversation was manually
 * abandoned by programmatically calling the abandon() method on it.
 */
public class ManuallyAbandonedConversationCanceller : ConversationCanceller{
    public void setConversation(Conversation conversation) {
        throw new UnsupportedOperationException();
    }

    public bool cancelBasedOnInput(ConversationContext context, String input) {
        throw new UnsupportedOperationException();
    }

    public ConversationCanceller clone() {
        throw new UnsupportedOperationException();
    }
}
