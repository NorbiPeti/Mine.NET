namespace Mine.NET.conversations;

/**
 * StringPrompt is the base class for any prompt that accepts an arbitrary
 * string from the user.
 */
public abstract class StringPrompt : Prompt{

    /**
     * Ensures that the prompt waits for the user to provide input.
     *
     * @param context Context information about the conversation.
     * @return True.
     */
    public bool blocksForInput(ConversationContext context) {
        return true;
    }
}
