using System;
using System.Collections.Generic;

namespace Mine.NET.configuration
{
    /**
     * This is a {@link Configuration} implementation that does not save or load
     * from any source, and stores all values in memory only.
     * This is useful for temporary Configurations for providing defaults.
     */
    public class MemoryConfiguration : MemorySection, Configuration<MemoryConfigurationOptions>
    {
        protected Configuration defaults;
        protected MemoryConfigurationOptions options_;

        /**
         * Creates an empty {@link MemoryConfiguration} with no default values.
         */
        public MemoryConfiguration() { }

        /**
         * Creates an empty {@link MemoryConfiguration} using the specified {@link
         * Configuration} as a source for all default values.
         *
         * @param defaults Default value provider
         * @throws ArgumentException Thrown if defaults is null
         */
        public MemoryConfiguration(Configuration defaults)
        {
            this.defaults = defaults;
        }

        public override void addDefault(String path, Object value)
        {
            if (path == null) throw new ArgumentNullException("Path may not be null");

            if (defaults == null)
            {
                defaults = new MemoryConfiguration();
            }

            defaults.set(path, value);
        }

        public override void addDefaults(Dictionary<String, Object> defaults)
        {
            if (defaults == null) throw new ArgumentNullException("Defaults may not be null");

            foreach (KeyValuePair<String, Object> entry in defaults)
            {
                addDefault(entry.Key, entry.Value);
            }
        }

        public void addDefaults(Configuration defaults)
        {
            if (defaults == null) throw new ArgumentNullException("Defaults may not be null");

            addDefaults(defaults.getValues(true));
        }

        public void setDefaults(Configuration defaults)
        {
            if (defaults == null) throw new ArgumentNullException("Defaults may not be null");

            this.defaults = defaults;
        }

        public Configuration getDefaults()
        {
            return defaults;
        }

        public override ConfigurationSection getParent()
        {
            return null;
        }

        public virtual MemoryConfigurationOptions options()
        {
            if (options == null)
            {
                options = new MemoryConfigurationOptions(this);
            }

            return options;
        }
    }
}
