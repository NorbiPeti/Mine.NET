using System;
using System.Collections.Generic;

namespace Mine.NET.inventory.meta
{
    /**
     * Represents a book ({@link Materials#BOOK_AND_QUILL} or {@link
     * Materials#WRITTEN_BOOK}) that can have a title, an author, and pages.
     */
    public interface BookMeta : ItemMeta<BookMeta> {

        /**
         * Checks for the existence of a title in the book.
         *
         * @return true if the book has a title
         */
        bool hasTitle();

        /**
         * Gets the title of the book.
         * <p>
         * Plugins should check that hasTitle() returns true before calling this
         * method.
         *
         * @return the title of the book
         */
        String getTitle();

        /**
         * Sets the title of the book.
         * <p>
         * Limited to 16 chars. Removes title when given null.
         *
         * @param title the title to set
         * @return true if the title was successfully set
         */
        bool setTitle(String title);

        /**
         * Checks for the existence of an author in the book.
         *
         * @return the author of the book
         */
        bool hasAuthor();

        /**
         * Gets the author of the book.
         * <p>
         * Plugins should check that hasAuthor() returns true before calling this
         * method.
         *
         * @return the author of the book
         */
        String getAuthor();

        /**
         * Sets the author of the book. Removes author when given null.
         *
         * @param author the author of the book
         */
        void setAuthor(String author);

        /**
         * Checks for the existence of pages in the book.
         *
         * @return true if the book has pages
         */
        bool hasPages();

        /**
         * Gets the specified page in the book. The given page must exist.
         *
         * @param page the page number to get
         * @return the page from the book
         */
        String getPage(int page);

        /**
         * Sets the specified page in the book. Pages of the book must be
         * contiguous.
         * <p>
         * The data can be up to 256 chars in length, additional chars
         * are truncated.
         *
         * @param page the page number to set
         * @param data the data to set for that page
         */
        void setPage(int page, String data);

        /**
         * Gets all the pages in the book.
         *
         * @return list of all the pages in the book
         */
        List<String> getPages();

        /**
         * Clears the existing book pages, and sets the book to use the provided
         * pages. Maximum 50 pages with 256 chars per page.
         *
         * @param pages A list of pages to set the book to use
         */
        void setPages(List<String> pages);

        /**
         * Clears the existing book pages, and sets the book to use the provided
         * pages. Maximum 50 pages with 256 chars per page.
         *
         * @param pages A list of strings, each being a page
         */
        void setPages(params String[] pages);

        /**
         * Adds new pages to the end of the book. Up to a maximum of 50 pages with
         * 256 chars per page.
         *
         * @param pages A list of strings, each being a page
         */
        void addPage(params String[] pages);

        /**
         * Gets the number of pages in the book.
         *
         * @return the number of pages in the book
         */
        int getPageCount();
    }
}
