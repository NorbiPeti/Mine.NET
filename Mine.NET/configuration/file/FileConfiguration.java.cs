using System;
using System.IO;
using System.Text;

namespace Mine.NET.configuration.file
{
    /**
     * This is a base class for all FileInfo based implementations of {@link
     * Configuration}
     */
    public abstract class FileConfiguration : MemoryConfiguration
    {

        /**
         * Creates an empty {@link FileConfiguration} with no default values.
         */
        public FileConfiguration() : base()
        {
        }

        /**
         * Creates an empty {@link FileConfiguration} using the specified {@link
         * Configuration} as a source for all default values.
         *
         * @param defaults Default value provider
         */
        public FileConfiguration(Configuration defaults) : base(defaults)
        {
        }

        /**
         * Saves this {@link FileConfiguration} to the specified location.
         * <p>
         * If the file does not exist, it will be created. If already exists, it
         * will be overwritten. If it cannot be overwritten or created, an
         * exception will be thrown.
         * <p>
         * This method will save using the system default encoding, or possibly
         * using UTF8.
         *
         * @param file FileInfo to save to.
         * @throws IOException Thrown when the given file cannot be written to for
         *     any reason.
         * @throws ArgumentException Thrown when file is null.
         */
        public void save(FileInfo file)
        { //Find: " throws .+Exception" - Replace: ""
            if (file == null) throw new ArgumentNullException("FileInfo cannot be null");

            file.Directory.Create();

            String data = saveToString();

            var writer = new StreamWriter(file.OpenWrite());

            try
            {
                writer.Write(data);
            }
            finally
            {
                writer.Close();
            }
        }

        /**
         * Saves this {@link FileConfiguration} to the specified location.
         * <p>
         * If the file does not exist, it will be created. If already exists, it
         * will be overwritten. If it cannot be overwritten or created, an
         * exception will be thrown.
         * <p>
         * This method will save using the system default encoding, or possibly
         * using UTF8.
         *
         * @param file FileInfo to save to.
         * @throws IOException Thrown when the given file cannot be written to for
         *     any reason.
         * @throws ArgumentException Thrown when file is null.
         */
        public void save(String file)
        {
            if (file == null) throw new ArgumentNullException("FileInfo cannot be null");

            save(new FileInfo(file));
        }

        /**
         * Saves this {@link FileConfiguration} to a string, and returns it.
         *
         * @return String containing this configuration.
         */
        public abstract String saveToString();

        /**
         * Loads this {@link FileConfiguration} from the specified location.
         * <p>
         * All the values contained within this configuration will be removed,
         * leaving only settings and defaults, and the new values will be loaded
         * from the given file.
         * <p>
         * If the file cannot be loaded for any reason, an exception will be
         * thrown.
         *
         * @param file FileInfo to load from.
         * @throws FileNotFoundException Thrown when the given file cannot be
         *     opened.
         * @throws IOException Thrown when the given file cannot be read.
         * @throws InvalidConfigurationException Thrown when the given file is not
         *     a valid Configuration.
         * @throws ArgumentException Thrown when file is null.
         */
        public void load(FileInfo file)
        {
            if (file == null) throw new ArgumentNullException("FileInfo cannot be null");

            FileStream stream = file.OpenRead();

            load(new StreamReader(stream));
        }

        /**
         * Loads this {@link FileConfiguration} from the specified stream.
         * <p>
         * All the values contained within this configuration will be removed,
         * leaving only settings and defaults, and the new values will be loaded
         * from the given stream.
         *
         * @param stream Stream to load from
         * @throws IOException Thrown when the given file cannot be read.
         * @throws InvalidConfigurationException Thrown when the given file is not
         *     a valid Configuration.
         * @throws ArgumentException Thrown when stream is null.
         * [Obsolete] This does not consider encoding
         * @see #load(Reader)
         */
        [Obsolete]
        public void load(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("Stream cannot be null");

            load(new StreamReader(stream));
        }

        /**
         * Loads this {@link FileConfiguration} from the specified reader.
         * <p>
         * All the values contained within this configuration will be removed,
         * leaving only settings and defaults, and the new values will be loaded
         * from the given stream.
         *
         * @param reader the reader to load from
         * @throws IOException thrown when underlying reader
         * @throws InvalidConfigurationException thrown when the reader does not
         *      represent a valid Configuration
         * @throws ArgumentException thrown when reader is null
         */
        public void load(StreamReader reader)
        {
            StringBuilder builder = new StringBuilder();

            try
            {
                String line;

                while ((line = reader.ReadLine()) != null)
                {
                    builder.Append(line);
                    builder.Append('\n');
                }
            }
            finally
            {
                reader.Close();
            }

            loadFromString(builder.ToString());
        }

        /**
         * Loads this {@link FileConfiguration} from the specified location.
         * <p>
         * All the values contained within this configuration will be removed,
         * leaving only settings and defaults, and the new values will be loaded
         * from the given file.
         * <p>
         * If the file cannot be loaded for any reason, an exception will be
         * thrown.
         *
         * @param file FileInfo to load from.
         * @throws FileNotFoundException Thrown when the given file cannot be
         *     opened.
         * @throws IOException Thrown when the given file cannot be read.
         * @throws InvalidConfigurationException Thrown when the given file is not
         *     a valid Configuration.
         * @throws ArgumentException Thrown when file is null.
         */
        public void load(String file)
        {
            if (file == null) throw new ArgumentNullException("FileInfo cannot be null");

            load(new FileInfo(file));
        }

        /**
         * Loads this {@link FileConfiguration} from the specified string, as
         * opposed to from file.
         * <p>
         * All the values contained within this configuration will be removed,
         * leaving only settings and defaults, and the new values will be loaded
         * from the given string.
         * <p>
         * If the string is invalid in any way, an exception will be thrown.
         *
         * @param contents Contents of a Configuration to load.
         * @throws InvalidConfigurationException Thrown if the specified string is
         *     invalid.
         * @throws ArgumentException Thrown if contents is null.
         */
        public abstract void loadFromString(String contents);

        /**
         * Compiles the header for this {@link FileConfiguration} and returns the
         * result.
         * <p>
         * This will use the header from {@link #options()} -&gt; {@link
         * FileConfigurationOptions#header()}, respecting the rules of {@link
         * FileConfigurationOptions#copyHeader()} if set.
         *
         * @return Compiled header
         */
        protected abstract String buildHeader();

        public override FileConfigurationOptions options()
        {
            if (options == null)
            {
                options = new FileConfigurationOptions(this);
            }

            return (FileConfigurationOptions)options;
        }
    }
}
