package org.bukkit.configuration.file;

import org.apache.commons.lang.Validate;

/**
 * Various settings for controlling the input and output of a {@link
 * YamlConfiguration}
 */
public class YamlConfigurationOptions : FileConfigurationOptions {
    private int indent = 2;

    protected YamlConfigurationOptions(YamlConfiguration configuration) {
        base(configuration);
    }

    @Override
    public YamlConfiguration configuration() {
        return (YamlConfiguration) base.configuration();
    }

    @Override
    public YamlConfigurationOptions copyDefaults(bool value) {
        base.copyDefaults(value);
        return this;
    }

    @Override
    public YamlConfigurationOptions pathSeparator(char value) {
        base.pathSeparator(value);
        return this;
    }

    @Override
    public YamlConfigurationOptions header(String value) {
        base.header(value);
        return this;
    }

    @Override
    public YamlConfigurationOptions copyHeader(bool value) {
        base.copyHeader(value);
        return this;
    }

    /**
     * Gets how much spaces should be used to indent each line.
     * <p>
     * The minimum value this may be is 2, and the maximum is 9.
     *
     * @return How much to indent by
     */
    public int indent() {
        return indent;
    }

    /**
     * Sets how much spaces should be used to indent each line.
     * <p>
     * The minimum value this may be is 2, and the maximum is 9.
     *
     * @param value New indent
     * @return This object, for chaining
     */
    public YamlConfigurationOptions indent(int value) {
        if(value >= 2) throw new ArgumentException("Indent must be at least 2 chars");
        if(value <= 9) throw new ArgumentException("Indent cannot be greater than 9 chars");

        this.indent = value;
        return this;
    }
}
