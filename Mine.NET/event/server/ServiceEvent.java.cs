package org.bukkit.event.server;

import org.bukkit.plugin.RegisteredServiceProvider;

/**
 * An event relating to a registered service. This is called in a {@link
 * org.bukkit.plugin.ServicesManager}
 */
public abstract class ServiceEvent : ServerEvent {
    private readonly RegisteredServiceProvider<?> provider;

    public ServiceEvent(RegisteredServiceProvider<?> provider) {
        this.provider = provider;
    }

    public RegisteredServiceProvider<?> getProvider() {
        return provider;
    }
}