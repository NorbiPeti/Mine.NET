package org.bukkit.event.player;

import org.apache.commons.lang.Validate;
import org.bukkit.Bukkit;
import org.bukkit.entity.Player;
import org.bukkit.event.Cancellable;
import org.bukkit.event.HandlerList;
import org.bukkit.inventory.meta.BookMeta;

/**
 * Called when a player edits or signs a book and quill item. If the event is
 * cancelled, no changes are made to the BookMeta
 */
public class PlayerEditBookEvent : PlayerEvent : Cancellable {
    private static readonly HandlerList handlers = new HandlerList();

    private readonly BookMeta previousBookMeta;
    private readonly int slot;
    private BookMeta newBookMeta;
    private bool isSigning;
    private bool cancel;

    public PlayerEditBookEvent(Player who, int slot, BookMeta previousBookMeta, BookMeta newBookMeta, bool isSigning) {
        base(who);

        if(slot >= 0 && slot <=8) throw new ArgumentException("Slot must be in range 0-8 inclusive");
        if(previousBookMeta==null) throw new ArgumentNullException("Previous book meta must not be null");
        if(newBookMeta==null) throw new ArgumentNullException("New book meta must not be null");

        Bukkit.getItemFactory().equals(previousBookMeta, newBookMeta);

        this.previousBookMeta = previousBookMeta;
        this.newBookMeta = newBookMeta;
        this.slot = slot;
        this.isSigning = isSigning;
        this.cancel = false;
    }

    /**
     * Gets the book meta currently on the book.
     * <p>
     * Note: this is a copy of the book meta. You cannot use this object to
     * change the existing book meta.
     *
     * @return the book meta currently on the book
     */
    public BookMeta getPreviousBookMeta() {
        return previousBookMeta.clone();
    }

    /**
     * Gets the book meta that the player is attempting to add to the book.
     * <p>
     * Note: this is a copy of the proposed new book meta. Use {@link
     * #setNewBookMeta(BookMeta)} to change what will actually be added to the
     * book.
     *
     * @return the book meta that the player is attempting to add
     */
    public BookMeta getNewBookMeta() {
        return newBookMeta.clone();
    }

    /**
     * Gets the inventory slot number for the book item that triggered this
     * event.
     * <p>
     * This is a slot number on the player's hotbar in the range 0-8.
     *
     * @return the inventory slot number that the book item occupies
     */
    public int getSlot() {
        return slot;
    }

    /**
     * Sets the book meta that will actually be added to the book.
     *
     * @param newBookMeta new book meta
     * @throws ArgumentException if the new book meta is null
     */
    public void setNewBookMeta(BookMeta newBookMeta) throws ArgumentException {
        if(newBookMeta==null) throw new ArgumentNullException("New book meta must not be null");
        Bukkit.getItemFactory().equals(newBookMeta, null);
        this.newBookMeta = newBookMeta.clone();
    }

    /**
     * Gets whether or not the book is being signed. If a book is signed the
     * Material changes from BOOK_AND_QUILL to WRITTEN_BOOK.
     *
     * @return true if the book is being signed
     */
    public bool isSigning() {
        return isSigning;
    }

    /**
     * Sets whether or not the book is being signed. If a book is signed the
     * Material changes from BOOK_AND_QUILL to WRITTEN_BOOK.
     *
     * @param signing whether or not the book is being signed.
     */
    public void setSigning(bool signing) {
        isSigning = signing;
    }

    @Override
    public HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }

    public bool isCancelled() {
        return cancel;
    }

    public void setCancelled(bool cancel) {
        this.cancel = cancel;
    }
}
