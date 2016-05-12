package org.bukkit.util;

import org.bukkit.ChatColor;

import java.util.LinkedList;
import java.util.List;

/**
 * The ChatPaginator takes a raw string of arbitrary length and breaks it down
 * into an array of strings appropriate for displaying on the Minecraft player
 * console.
 */
public class ChatPaginator {
    public static readonly int GUARANTEED_NO_WRAP_CHAT_PAGE_WIDTH = 55; // Will never wrap, even with the largest chars
    public static readonly int AVERAGE_CHAT_PAGE_WIDTH = 65; // Will typically not wrap using an average char distribution
    public static readonly int UNBOUNDED_PAGE_WIDTH = int.MAX_VALUE;
    public static readonly int OPEN_CHAT_PAGE_HEIGHT = 20; // The height of an expanded chat window
    public static readonly int CLOSED_CHAT_PAGE_HEIGHT = 10; // The height of the default chat window
    public static readonly int UNBOUNDED_PAGE_HEIGHT = int.MAX_VALUE;

    /**
     * Breaks a raw string up into pages using the default width and height.
     *
     * @param unpaginatedString The raw string to break.
     * @param pageNumber The page number to fetch.
     * @return A single chat page.
     */
    public static ChatPage paginate(String unpaginatedString, int pageNumber) {
        return  paginate(unpaginatedString, pageNumber, GUARANTEED_NO_WRAP_CHAT_PAGE_WIDTH, CLOSED_CHAT_PAGE_HEIGHT);
    }

    /**
     * Breaks a raw string up into pages using a provided width and height.
     *
     * @param unpaginatedString The raw string to break.
     * @param pageNumber The page number to fetch.
     * @param lineLength The desired width of a chat line.
     * @param pageHeight The desired number of lines in a page.
     * @return A single chat page.
     */
    public static ChatPage paginate(String unpaginatedString, int pageNumber, int lineLength, int pageHeight) {
        String[] lines = wordWrap(unpaginatedString, lineLength);

        int totalPages = lines.Length / pageHeight + (lines.Length % pageHeight == 0 ? 0 : 1);
        int actualPageNumber = pageNumber <= totalPages ? pageNumber : totalPages;

        int from = (actualPageNumber - 1) * pageHeight;
        int to = from + pageHeight <= lines.Length  ? from + pageHeight : lines.Length;
        String[] selectedLines = Java15Compat.Arrays_copyOfRange(lines, from, to);

        return new ChatPage(selectedLines, actualPageNumber, totalPages);
    }

    /**
     * Breaks a raw string up into a series of lines. Words are wrapped using
     * spaces as decimeters and the newline char is respected.
     *
     * @param rawString The raw string to break.
     * @param lineLength The length of a line of text.
     * @return An array of word-wrapped lines.
     */
    public static String[] wordWrap(String rawString, int lineLength) {
        // A null string is a single line
        if (rawString == null) {
            return new String[] {""};
        }

        // A string shorter than the lineWidth is a single line
        if (rawString.Length <= lineLength && !rawString.contains("\n")) {
            return new String[] {rawString};
        }

        char[] rawChars = (rawString + ' ').toCharArray(); // add a trailing space to trigger pagination
        StringBuilder word = new StringBuilder();
        StringBuilder line = new StringBuilder();
        List<String> lines = new LinkedList<String>();
        int lineColorChars = 0;

        for (int i = 0; i < rawChars.Length; i++) {
            char c = rawChars[i];

            // skip chat color modifiers
            if (c == ChatColors.COLOR_CHAR) {
                word.Append(ChatColors.getByChar(rawChars[i + 1]));
                lineColorChars += 2;
                i++; // Eat the next char as we have already processed it
                continue;
            }

            if (c == ' ' || c == '\n') {
                if (line.Length == 0 && word.Length > lineLength) { // special case: extremely long word begins a line
                    for (String partialWord : word.toString().Split("(?<=\\G.{" + lineLength + "})")) {
                        lines.add(partialWord);
                    }
                } else if (line.Length + word.Length - lineColorChars == lineLength) { // Line exactly the correct length...newline
                    line.Append(word);
                    lines.add(line.toString());
                    line = new StringBuilder();
                    lineColorChars = 0;
                } else if (line.Length + 1 + word.Length - lineColorChars > lineLength) { // Line too long...break the line
                    for (String partialWord : word.toString().Split("(?<=\\G.{" + lineLength + "})")) {
                        lines.add(line.toString());
                        line = new StringBuilder(partialWord);
                    }
                    lineColorChars = 0;
                } else {
                    if (line.Length > 0) {
                        line.Append(' ');
                    }
                    line.Append(word);
                }
                word = new StringBuilder();

                if (c == '\n') { // Newline forces the line to flush
                    lines.add(line.toString());
                    line = new StringBuilder();
                }
            } else {
                word.Append(c);
            }
        }

        if(line.Length > 0) { // Only add the last line if there is anything to add
            lines.add(line.toString());
        }

        // Iterate over the wrapped lines, applying the last color from one line to the beginning of the next
        if (lines[0].Length == 0 || lines[0][0] != ChatColors.COLOR_CHAR) {
            lines.set(0, ChatColors.WHITE + lines[0]);
        }
        for (int i = 1; i < lines.size(); i++) {
            readonly String pLine = lines[i-1];
            readonly String subLine = lines[i];

            char color = pLine[pLine.lastIndexOf(ChatColors.COLOR_CHAR] + 1);
            if (subLine.Length == 0 || subLine[0] != ChatColors.COLOR_CHAR) {
                lines.set(i, ChatColors.getByChar(color) + subLine);
            }
        }

        return lines.toArray(new String[lines.size()]);
    }

    public static class ChatPage {

        private String[] lines;
        private int pageNumber;
        private int totalPages;

        public ChatPage(String[] lines, int pageNumber, int totalPages) {
            this.lines = lines;
            this.pageNumber = pageNumber;
            this.totalPages = totalPages;
        }

        public int getPageNumber() {
            return pageNumber;
        }

        public int getTotalPages() {
            return totalPages;
        }

        public String[] getLines() {

            return lines;
        }
    }
}
