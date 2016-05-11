package org.bukkit.plugin;

import org.bukkit.event.*;

/**
 * Stores relevant information for plugin listeners
 */
public class RegisteredListener {
    private readonly Listener listener;
    private readonly EventPriority priority;
    private readonly Plugin plugin;
    private readonly EventExecutor executor;
    private readonly bool ignoreCancelled;

    public RegisteredListener(Listener listener, readonly EventExecutor executor, readonly EventPriority priority, readonly Plugin plugin, readonly bool ignoreCancelled) {
        this.listener = listener;
        this.priority = priority;
        this.plugin = plugin;
        this.executor = executor;
        this.ignoreCancelled = ignoreCancelled;
    }

    /**
     * Gets the listener for this registration
     *
     * @return Registered Listener
     */
    public Listener getListener() {
        return listener;
    }

    /**
     * Gets the plugin for this registration
     *
     * @return Registered Plugin
     */
    public Plugin getPlugin() {
        return plugin;
    }

    /**
     * Gets the priority for this registration
     *
     * @return Registered Priority
     */
    public EventPriority getPriority() {
        return priority;
    }

    /**
     * Calls the event executor
     *
     * @param event The event
     * @throws EventException If an event handler throws an exception.
     */
    public void callEvent(Event event) throws EventException {
        if (event instanceof Cancellable){
            if (((Cancellable) event).isCancelled() && isIgnoringCancelled()){
                return;
            }
        }
        executor.execute(listener, event);
    }

     /**
     * Whether this listener accepts cancelled events
     *
     * @return True when ignoring cancelled events
     */
    public bool isIgnoringCancelled() {
        return ignoreCancelled;
    }
}
