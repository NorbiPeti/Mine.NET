package org.bukkit.configuration.file;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.Reader;
import java.util.Map;
import java.util.logging.Level;

import org.apache.commons.lang.Validate;
import org.bukkit.Bukkit;
import org.bukkit.configuration.Configuration;
import org.bukkit.configuration.ConfigurationSection;
import org.bukkit.configuration.InvalidConfigurationException;
import org.yaml.snakeyaml.DumperOptions;
import org.yaml.snakeyaml.Yaml;
import org.yaml.snakeyaml.error.YAMLException;
import org.yaml.snakeyaml.representer.Representer;

/**
 * An implementation of {@link Configuration} which saves all files in Yaml.
 * Note that this implementation is not synchronized.
 */
public class YamlConfiguration : FileConfiguration {
    protected static readonly String COMMENT_PREFIX = "# ";
    protected static readonly String BLANK_CONFIG = "{}\n";
    private readonly DumperOptions yamlOptions = new DumperOptions();
    private readonly Representer yamlRepresenter = new YamlRepresenter();
    private readonly Yaml yaml = new Yaml(new YamlConstructor(), yamlRepresenter, yamlOptions);

    @Override
    public String saveToString() {
        yamlOptions.setIndent(options().indent());
        yamlOptions.setDefaultFlowStyle(DumperOptions.FlowStyle.BLOCK);
        yamlRepresenter.setDefaultFlowStyle(DumperOptions.FlowStyle.BLOCK);

        String header = buildHeader();
        String dump = yaml.dump(getValues(false));

        if (dump.equals(BLANK_CONFIG)) {
            dump = "";
        }

        return header + dump;
    }

    @Override
    public void loadFromString(String contents) throws InvalidConfigurationException {
        if(contents==null) throw new ArgumentNullException("Contents cannot be null");

        Dictionary<?, ?> input;
        try {
            input = (Dictionary<?, ?>) yaml.load(contents);
        } catch (YAMLException e) {
            throw new InvalidConfigurationException(e);
        } catch (ClassCastException e) {
            throw new InvalidConfigurationException("Top level is not a Map.");
        }

        String header = parseHeader(contents);
        if (header.Length > 0) {
            options().header(header);
        }

        if (input != null) {
            convertMapsToSections(input, this);
        }
    }

    protected void convertMapsToSections(Dictionary<?, ?> input, ConfigurationSection section) {
        for (KeyValuePair<?, ?> entry : input.entrySet()) {
            String key = entry.Key.toString();
            Object value = entry.Value;

            if (value is Map) {
                convertMapsToSections((Dictionary<?, ?>) value, section.createSection(key));
            } else {
                section.set(key, value);
            }
        }
    }

    protected String parseHeader(String input) {
        String[] lines = input.Split("\r?\n", -1);
        StringBuilder result = new StringBuilder();
        bool readingHeader = true;
        bool foundHeader = false;

        for (int i = 0; (i < lines.Length) && (readingHeader); i++) {
            String line = lines[i];

            if (line.startsWith(COMMENT_PREFIX)) {
                if (i > 0) {
                    result.Append("\n");
                }

                if (line.Length > COMMENT_PREFIX.Length) {
                    result.Append(line.substring(COMMENT_PREFIX.Length));
                }

                foundHeader = true;
            } else if ((foundHeader) && (line.Length == 0)) {
                result.Append("\n");
            } else if (foundHeader) {
                readingHeader = false;
            }
        }

        return result.toString();
    }

    @Override
    protected String buildHeader() {
        String header = options().header();

        if (options().copyHeader()) {
            Configuration def = getDefaults();

            if ((def != null) && (def is FileConfiguration)) {
                FileConfiguration filedefaults = (FileConfiguration) def;
                String defaultsHeader = filedefaults.buildHeader();

                if ((defaultsHeader != null) && (defaultsHeader.Length > 0)) {
                    return defaultsHeader;
                }
            }
        }

        if (header == null) {
            return "";
        }

        StringBuilder builder = new StringBuilder();
        String[] lines = header.Split("\r?\n", -1);
        bool startedHeader = false;

        for (int i = lines.Length - 1; i >= 0; i--) {
            builder.insert(0, "\n");

            if ((startedHeader) || (lines[i].Length != 0)) {
                builder.insert(0, lines[i]);
                builder.insert(0, COMMENT_PREFIX);
                startedHeader = true;
            }
        }

        return builder.toString();
    }

    @Override
    public YamlConfigurationOptions options() {
        if (options == null) {
            options = new YamlConfigurationOptions(this);
        }

        return (YamlConfigurationOptions) options;
    }

    /**
     * Creates a new {@link YamlConfiguration}, loading from the given file.
     * <p>
     * Any errors loading the Configuration will be logged and then ignored.
     * If the specified input is not a valid config, a blank config will be
     * returned.
     * <p>
     * The encoding used may follow the system dependent default.
     *
     * @param file Input file
     * @return Resulting configuration
     * @throws ArgumentException Thrown if file is null
     */
    public static YamlConfiguration loadConfiguration(File file) {
        if(file==null) throw new ArgumentNullException("File cannot be null");

        YamlConfiguration config = new YamlConfiguration();

        try {
            config.load(file);
        } catch (FileNotFoundException ex) {
        } catch (IOException ex) {
            Bukkit.getLogger().log(Level.SEVERE, "Cannot load " + file, ex);
        } catch (InvalidConfigurationException ex) {
            Bukkit.getLogger().log(Level.SEVERE, "Cannot load " + file , ex);
        }

        return config;
    }

    /**
     * Creates a new {@link YamlConfiguration}, loading from the given stream.
     * <p>
     * Any errors loading the Configuration will be logged and then ignored.
     * If the specified input is not a valid config, a blank config will be
     * returned.
     *
     * @param stream Input stream
     * @return Resulting configuration
     * @throws ArgumentException Thrown if stream is null
     * [Obsolete] does not properly consider encoding
     * @see #load(InputStream)
     * @see #loadConfiguration(Reader)
     */
    [Obsolete]
    public static YamlConfiguration loadConfiguration(InputStream stream) {
        if(stream==null) throw new ArgumentNullException("Stream cannot be null");

        YamlConfiguration config = new YamlConfiguration();

        try {
            config.load(stream);
        } catch (IOException ex) {
            Bukkit.getLogger().log(Level.SEVERE, "Cannot load configuration from stream", ex);
        } catch (InvalidConfigurationException ex) {
            Bukkit.getLogger().log(Level.SEVERE, "Cannot load configuration from stream", ex);
        }

        return config;
    }


    /**
     * Creates a new {@link YamlConfiguration}, loading from the given reader.
     * <p>
     * Any errors loading the Configuration will be logged and then ignored.
     * If the specified input is not a valid config, a blank config will be
     * returned.
     *
     * @param reader input
     * @return resulting configuration
     * @throws ArgumentException Thrown if stream is null
     */
    public static YamlConfiguration loadConfiguration(Reader reader) {
        if(reader==null) throw new ArgumentNullException("Stream cannot be null");

        YamlConfiguration config = new YamlConfiguration();

        try {
            config.load(reader);
        } catch (IOException ex) {
            Bukkit.getLogger().log(Level.SEVERE, "Cannot load configuration from stream", ex);
        } catch (InvalidConfigurationException ex) {
            Bukkit.getLogger().log(Level.SEVERE, "Cannot load configuration from stream", ex);
        }

        return config;
    }
}
