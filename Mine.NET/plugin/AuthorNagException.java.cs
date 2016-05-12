namespace Mine.NET.plugin;

@SuppressWarnings("serial")
public class AuthorNagException : RuntimeException {
    private readonly String message;

    /**
     * Constructs a new AuthorNagException based on the given Exception
     *
     * @param message Brief message explaining the cause of the exception
     */
    public AuthorNagException(String message) {
        this.message = message;
    }

    public override String getMessage() {
        return message;
    }
}
