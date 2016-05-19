namespace Mine.NET.Event
{
/**
 * A list of event handlers, stored per-event. Based on lahwran's fevents.
 */
public class HandlerList { //TODO

    /**
     * Handler array. This field being an array is the key to this system's
     * speed.
     */
    private volatile RegisteredListener[] handlers = null;

    /**
     * Dynamic handler lists. These are changed using register() and
     * unregister() and are automatically baked to the handlers array any time
     * they have changed.
     */
    private readonly EnumMap<EventPriority, List<RegisteredListener>> handlerslots;

    /**
     * List of all HandlerLists which have been created, for use in bakeAll()
     */
    private static List<HandlerList> allLists = new List<HandlerList>();

    /**
     * Bake all handler lists. Best used just after all normal event
     * registration is complete, ie just after all plugins are loaded if
     * you're using fevents in a plugin system.
     */
    public static void bakeAll() {
        (allLists) {
            foreach (HandlerList h  in  allLists) {
                h.bake();
            }
        }
    }

    /**
     * Unregister all listeners from all handler lists.
     */
    public static void unregisterAll() {
        (allLists) {
            foreach (HandlerList h  in  allLists) {
                (h) {
                    foreach (List<RegisteredListener> list  in  h.handlerslots.values()) {
                        list.clear();
                    }
                    h.handlers = null;
                }
            }
        }
    }

    /**
     * Unregister a specific plugin's listeners from all handler lists.
     *
     * @param plugin plugin to unregister
     */
    public static void unregisterAll(Plugin plugin) {
        (allLists) {
            foreach (HandlerList h  in  allLists) {
                h.unregister(plugin);
            }
        }
    }

    /**
     * Unregister a specific listener from all handler lists.
     *
     * @param listener listener to unregister
     */
    public static void unregisterAll(Listener listener) {
        (allLists) {
            foreach (HandlerList h  in  allLists) {
                h.unregister(listener);
            }
        }
    }

    /**
     * Create a new handler list and initialize using EventPriority.
     * <p>
     * The HandlerList is then added to meta-list for use in bakeAll()
     */
    public HandlerList() {
        handlerslots = new EnumMap<EventPriority, List<RegisteredListener>>(EventPriority.class);
        foreach (EventPriority o  in  EventPriority.values()) {
            handlerslots.Add(o, new List<RegisteredListener>());
        }
        (allLists) {
            allLists.add(this);
        }
    }

    /**
     * Register a new listener in this handler list
     *
     * @param listener listener to register
     */
    public void register(RegisteredListener listener) {
        if (handlerslots[listener.getPriority(]).contains(listener))
            throw new InvalidOperationException("This listener is already registered to priority " + listener.getPriority().ToString());
        handlers = null;
        handlerslots[listener.getPriority(]).add(listener);
    }

    /**
     * Register a collection of new listeners in this handler list
     *
     * @param listeners listeners to register
     */
    public void registerAll(Collection<RegisteredListener> listeners) {
        foreach (RegisteredListener listener  in  listeners) {
            register(listener);
        }
    }

    /**
     * Remove a listener from a specific order slot
     *
     * @param listener listener to remove
     */
    public void unregister(RegisteredListener listener) {
        if (handlerslots[listener.getPriority(]).remove(listener)) {
            handlers = null;
        }
    }

    /**
     * Remove a specific plugin's listeners from this handler
     *
     * @param plugin plugin to remove
     */
    public void unregister(Plugin plugin) {
        bool changed = false;
        foreach (List<RegisteredListener> list  in  handlerslots.values()) {
            for (ListIterator<RegisteredListener> i = list.listIterator(); i.hasNext();) {
                if (i.next().getPlugin().equals(plugin)) {
                    i.remove();
                    changed = true;
                }
            }
        }
        if (changed) handlers = null;
    }

    /**
     * Remove a specific listener from this handler
     *
     * @param listener listener to remove
     */
    public void unregister(Listener listener) {
        bool changed = false;
        foreach (List<RegisteredListener> list  in  handlerslots.values()) {
            for (ListIterator<RegisteredListener> i = list.listIterator(); i.hasNext();) {
                if (i.next().getListener().equals(listener)) {
                    i.remove();
                    changed = true;
                }
            }
        }
        if (changed) handlers = null;
    }

    /**
     * Bake HashMap and Lists to 2d array - does nothing if not necessary
     */
    public void bake() {
        if (handlers != null) return; // don't re-bake when still valid
        List<RegisteredListener> entries = new List<RegisteredListener>();
        foreach (Entry<EventPriority, List<RegisteredListener>> entry  in  handlerslots.entrySet()) {
            entries.addAll(entry.Value);
        }
        handlers = entries.toArray(new RegisteredListener[entries.Count]);
    }

    /**
     * Get the baked registered listeners associated with this handler list
     *
     * @return the array of registered listeners
     */
    public RegisteredListener[] getRegisteredListeners() {
        RegisteredListener[] handlers;
        while ((handlers = this.handlers) == null) bake(); // This prevents fringe cases of returning null
        return handlers;
    }

    /**
     * Get a specific plugin's registered listeners associated with this
     * handler list
     *
     * @param plugin the plugin to get the listeners of
     * @return the list of registered listeners
     */
    public static List<RegisteredListener> getRegisteredListeners(Plugin plugin) {
        List<RegisteredListener> listeners = new List<RegisteredListener>();
        (allLists) {
            foreach (HandlerList h  in  allLists) {
                (h) {
                    foreach (List<RegisteredListener> list  in  h.handlerslots.values()) {
                        foreach (RegisteredListener listener  in  list) {
                            if (listener.getPlugin().equals(plugin)) {
                                listeners.add(listener);
                            }
                        }
                    }
                }
            }
        }
        return listeners;
    }

    /**
     * Get a list of all handler lists for every event type
     *
     * @return the list of all handler lists
     */
    @SuppressWarnings("unchecked")
    public static List<HandlerList> getHandlerLists() {
        (allLists) {
            return (List<HandlerList>) allLists.clone();
        }
    }
}
}
