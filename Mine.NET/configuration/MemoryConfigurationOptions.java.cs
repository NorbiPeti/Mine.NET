package org.bukkit.configuration;

/**
 * Various settings for controlling the input and output of a {@link
 * MemoryConfiguration}
 */
public class MemoryConfigurationOptions : ConfigurationOptions {
    protected MemoryConfigurationOptions(MemoryConfiguration configuration) {
        base(configuration);
    }

    @Override
    public MemoryConfiguration configuration() {
        return (MemoryConfiguration) base.configuration();
    }

    @Override
    public MemoryConfigurationOptions copyDefaults(bool value) {
        base.copyDefaults(value);
        return this;
    }

    @Override
    public MemoryConfigurationOptions pathSeparator(char value) {
        base.pathSeparator(value);
        return this;
    }
}
