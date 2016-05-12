namespace Mine.NET.configuration
{
    /**
     * Various settings for controlling the input and output of a {@link
     * Configuration}
     */
    public class ConfigurationOptions
    {
        private readonly Configuration configuration;

        protected ConfigurationOptions(Configuration configuration)
        {
            this.configuration = configuration;
        }

        /**
         * Gets the char that will be used to separate {@link
         * ConfigurationSection}s
         * <p>
         * This value does not affect how the {@link Configuration} is stored,
         * only in how you access the data. The default value is '.'.
         *
         * @return Path separator
         */
        public char pathSeparator { get; set; } = '.';

        /**
         * Checks if the {@link Configuration} should copy values from its default
         * {@link Configuration} directly.
         * <p>
         * If this is true, all values in the default Configuration will be
         * directly copied, making it impossible to distinguish between values
         * that were set and values that are provided by default. As a result,
         * {@link ConfigurationSection#contains(java.lang.String)} will always
         * return the same value as {@link
         * ConfigurationSection#isSet(java.lang.String)}. The default value is
         * false.
         *
         * @return Whether or not defaults are directly copied
         */
        public bool copyDefaults { get; set; }
    }
}
