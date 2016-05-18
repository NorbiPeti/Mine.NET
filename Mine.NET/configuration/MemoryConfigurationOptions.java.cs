namespace Mine.NET.configuration;

/**
 * Various settings for controlling the input and output of a {@link
 * MemoryConfiguration}
 */
public class MemoryConfigurationOptions : ConfigurationOptions {
    protected MemoryConfigurationOptions(MemoryConfiguration configuration) : base(configuration) {
    }

    public override MemoryConfiguration configuration() {
        return (MemoryConfiguration) base.configuration();
    }

    public override MemoryConfigurationOptions copyDefaults(bool value) {
        base.copyDefaults(value);
        return this;
    }

    public override MemoryConfigurationOptions pathSeparator(char value) {
        base.pathSeparator(value);
        return this;
    }
}
