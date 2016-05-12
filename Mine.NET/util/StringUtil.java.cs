namespace Mine.NET.util;

import java.util.Collection;
import org.apache.commons.lang.Validate;

public class StringUtil {

    /**
     * Copies all elements from the iterable collection of originals to the
     * collection provided.
     *
     * @param <T> the collection of strings
     * @param token String to search for
     * @param originals An iterable collection of strings to filter.
     * @param collection The collection to add matches to
     * @return the collection provided that would have the elements copied
     *     into
     * @throws UnsupportedOperationException if the collection is immutable
     *     and originals contains a string which starts with the specified
     *     search string.
     * @throws ArgumentException if any parameter is is null
     * @throws ArgumentException if originals contains a null element.
     *     <b>Note: the collection may be modified before this is thrown</b>
     */
    public static <T : Collection<? base String>> T copyPartialMatches(String token, readonly Iterable<String> originals, readonly T collection) {
        if(token==null) throw new ArgumentNullException("Search token cannot be null");
        if(collection==null) throw new ArgumentNullException("Collection cannot be null");
        if(originals==null) throw new ArgumentNullException("Originals cannot be null");

        foreach (String string  in  originals) {
            if (StartsWithIgnoreCase(string, token)) {
                collection.add(string);
            }
        }

        return collection;
    }

    /**
     * This method uses a region to check case-insensitive equality. This
     * means the internal array does not need to be copied like a
     * ToLower() call would.
     *
     * @param string String to check
     * @param prefix Prefix of string to compare
     * @return true if provided string starts with, ignoring case, the prefix
     *     provided
     * @throws NullPointerException if prefix is null
     * @throws ArgumentException if string is null
     */
    public static bool StartsWithIgnoreCase(String string, readonly String prefix) {
        if(string==null) throw new ArgumentNullException("Cannot check a null string for a match");
        if (string.Length < prefix.Length) {
            return false;
        }
        return string.regionMatches(true, 0, prefix, 0, prefix.Length);
    }
}
