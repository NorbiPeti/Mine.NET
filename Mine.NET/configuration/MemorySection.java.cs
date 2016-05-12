package org.bukkit.configuration;

import static org.bukkit.util.NumberConversions.*;

import java.util.List;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

import org.apache.commons.lang.Validate;
import org.bukkit.Color;
import org.bukkit.OfflinePlayer;
import org.bukkit.inventory.ItemStack;
import org.bukkit.util.Vector;

/**
 * A type of {@link ConfigurationSection} that is stored in memory.
 */
public class MemorySection : ConfigurationSection {
    protected readonly Dictionary<String, Object> map = new LinkedHashMap<String, Object>();
    private readonly Configuration root;
    private readonly ConfigurationSection parent;
    private readonly String path;
    private readonly String fullPath;

    /**
     * Creates an empty MemorySection for use as a root {@link Configuration}
     * section.
     * <p>
     * Note that calling this without being yourself a {@link Configuration}
     * will throw an exception!
     *
     * @throws IllegalStateException Thrown if this is not a {@link
     *     Configuration} root.
     */
    protected MemorySection() {
        if (!(this is Configuration)) {
            throw new IllegalStateException("Cannot construct a root MemorySection when not a Configuration");
        }

        this.path = "";
        this.fullPath = "";
        this.parent = null;
        this.root = (Configuration) this;
    }

    /**
     * Creates an empty MemorySection with the specified parent and path.
     *
     * @param parent Parent section that contains this own section.
     * @param path Path that you may access this section from via the root
     *     {@link Configuration}.
     * @throws ArgumentException Thrown is parent or path is null, or
     *     if parent contains no root Configuration.
     */
    protected MemorySection(ConfigurationSection parent, String path) {
        if(parent==null) throw new ArgumentNullException("Parent cannot be null");
        if(path==null) throw new ArgumentNullException("Path cannot be null");

        this.path = path;
        this.parent = parent;
        this.root = parent.getRoot();

        if(root==null) throw new ArgumentNullException("Path cannot be orphaned");

        this.fullPath = createPath(parent, path);
    }

    public HashSet<String> getKeys(bool deep) {
        HashSet<String> result = new LinkedHashSet<String>();

        Configuration root = getRoot();
        if (root != null && root.options().copyDefaults()) {
            ConfigurationSection defaults = getDefaultSection();

            if (defaults != null) {
                result.addAll(defaults.getKeys(deep));
            }
        }

        mapChildrenKeys(result, this, deep);

        return result;
    }

    public Dictionary<String, Object> getValues(bool deep) {
        Dictionary<String, Object> result = new LinkedHashMap<String, Object>();

        Configuration root = getRoot();
        if (root != null && root.options().copyDefaults()) {
            ConfigurationSection defaults = getDefaultSection();

            if (defaults != null) {
                result.putAll(defaults.getValues(deep));
            }
        }

        mapChildrenValues(result, this, deep);

        return result;
    }

    public bool contains(String path) {
        return get(path) != null;
    }

    public bool isSet(String path) {
        Configuration root = getRoot();
        if (root == null) {
            return false;
        }
        if (root.options().copyDefaults()) {
            return contains(path);
        }
        return get(path, null) != null;
    }

    public String getCurrentPath() {
        return fullPath;
    }

    public String getName() {
        return path;
    }

    public Configuration getRoot() {
        return root;
    }

    public ConfigurationSection getParent() {
        return parent;
    }

    public void addDefault(String path, Object value) {
        if(path==null) throw new ArgumentNullException("Path cannot be null");

        Configuration root = getRoot();
        if (root == null) {
            throw new IllegalStateException("Cannot add default without root");
        }
        if (root == this) {
            throw new UnsupportedOperationException("Unsupported addDefault(String, Object) implementation");
        }
        root.addDefault(createPath(this, path), value);
    }

    public ConfigurationSection getDefaultSection() {
        Configuration root = getRoot();
        Configuration defaults = root == null ? null : root.getDefaults();

        if (defaults != null) {
            if (defaults.isConfigurationSection(getCurrentPath())) {
                return defaults.getConfigurationSection(getCurrentPath());
            }
        }

        return null;
    }

    public void set(String path, Object value) {
        Validate.notEmpty(path, "Cannot set to an empty path");

        Configuration root = getRoot();
        if (root == null) {
            throw new IllegalStateException("Cannot use section without a root");
        }

        readonly char separator = root.options().pathSeparator();
        // i1 is the leading (higher) index
        // i2 is the trailing (lower) index
        int i1 = -1, i2;
        ConfigurationSection section = this;
        while ((i1 = path.indexOf(separator, i2 = i1 + 1)) != -1) {
            String node = path.substring(i2, i1);
            ConfigurationSection subSection = section.getConfigurationSection(node);
            if (subSection == null) {
                section = section.createSection(node);
            } else {
                section = subSection;
            }
        }

        String key = path.substring(i2);
        if (section == this) {
            if (value == null) {
                map.remove(key);
            } else {
                map.put(key, value);
            }
        } else {
            section.set(key, value);
        }
    }

    public Object get(String path) {
        return get(path, getDefault(path));
    }

    public Object get(String path, Object def) {
        if(path==null) throw new ArgumentNullException("Path cannot be null");

        if (path.Length == 0) {
            return this;
        }

        Configuration root = getRoot();
        if (root == null) {
            throw new IllegalStateException("Cannot access section without a root");
        }

        readonly char separator = root.options().pathSeparator();
        // i1 is the leading (higher) index
        // i2 is the trailing (lower) index
        int i1 = -1, i2;
        ConfigurationSection section = this;
        while ((i1 = path.indexOf(separator, i2 = i1 + 1)) != -1) {
            section = section.getConfigurationSection(path.substring(i2, i1));
            if (section == null) {
                return def;
            }
        }

        String key = path.substring(i2);
        if (section == this) {
            Object result = map.get(key);
            return (result == null) ? def : result;
        }
        return section.get(key, def);
    }

    public ConfigurationSection createSection(String path) {
        Validate.notEmpty(path, "Cannot create section at empty path");
        Configuration root = getRoot();
        if (root == null) {
            throw new IllegalStateException("Cannot create section without a root");
        }

        readonly char separator = root.options().pathSeparator();
        // i1 is the leading (higher) index
        // i2 is the trailing (lower) index
        int i1 = -1, i2;
        ConfigurationSection section = this;
        while ((i1 = path.indexOf(separator, i2 = i1 + 1)) != -1) {
            String node = path.substring(i2, i1);
            ConfigurationSection subSection = section.getConfigurationSection(node);
            if (subSection == null) {
                section = section.createSection(node);
            } else {
                section = subSection;
            }
        }

        String key = path.substring(i2);
        if (section == this) {
            ConfigurationSection result = new MemorySection(this, key);
            map.put(key, result);
            return result;
        }
        return section.createSection(key);
    }

    public ConfigurationSection createSection(String path, Dictionary<?, ?> map) {
        ConfigurationSection section = createSection(path);

        for (Map.Entry<?, ?> entry : map.entrySet()) {
            if (entry.getValue() is Map) {
                section.createSection(entry.getKey().toString(), (Dictionary<?, ?>) entry.getValue());
            } else {
                section.set(entry.getKey().toString(), entry.getValue());
            }
        }

        return section;
    }

    // Primitives
    public String getString(String path) {
        Object def = getDefault(path);
        return getString(path, def != null ? def.toString() : null);
    }

    public String getString(String path, String def) {
        Object val = get(path, def);
        return (val != null) ? val.toString() : def;
    }

    public bool isString(String path) {
        Object val = get(path);
        return val is String;
    }

    public int getInt(String path) {
        Object def = getDefault(path);
        return getInt(path, (def is Number) ? toInt(def) : 0);
    }

    public int getInt(String path, int def) {
        Object val = get(path, def);
        return (val is Number) ? toInt(val) : def;
    }

    public bool isInt(String path) {
        Object val = get(path);
        return val is int;
    }

    public bool getBoolean(String path) {
        Object def = getDefault(path);
        return getBoolean(path, (def is bool) ? (bool) def : false);
    }

    public bool getBoolean(String path, bool def) {
        Object val = get(path, def);
        return (val is bool) ? (bool) val : def;
    }

    public bool isBoolean(String path) {
        Object val = get(path);
        return val is bool;
    }

    public double getDouble(String path) {
        Object def = getDefault(path);
        return getDouble(path, (def is Number) ? toDouble(def) : 0);
    }

    public double getDouble(String path, double def) {
        Object val = get(path, def);
        return (val is Number) ? toDouble(val) : def;
    }

    public bool isDouble(String path) {
        Object val = get(path);
        return val is Double;
    }

    public long getLong(String path) {
        Object def = getDefault(path);
        return getLong(path, (def is Number) ? toLong(def) : 0);
    }

    public long getLong(String path, long def) {
        Object val = get(path, def);
        return (val is Number) ? toLong(val) : def;
    }

    public bool isLong(String path) {
        Object val = get(path);
        return val is Long;
    }

    // Java
    public List<?> getList(String path) {
        Object def = getDefault(path);
        return getList(path, (def is List) ? (List<?>) def : null);
    }

    public List<?> getList(String path, List<?> def) {
        Object val = get(path, def);
        return (List<?>) ((val is List) ? val : def);
    }

    public bool isList(String path) {
        Object val = get(path);
        return val is List;
    }

    public List<String> getStringList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<String>(0);
        }

        List<String> result = new List<String>();

        for (Object object : list) {
            if ((object is String) || (isPrimitiveWrapper(object))) {
                result.add(String.valueOf(object));
            }
        }

        return result;
    }

    public List<int> getIntegerList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<int>(0);
        }

        List<int> result = new List<int>();

        for (Object object : list) {
            if (object is int) {
                result.add((int) object);
            } else if (object is String) {
                try {
                    result.add(int.valueOf((String) object));
                } catch (Exception ex) {
                }
            } else if (object is Character) {
                result.add((int) ((Character) object).charValue());
            } else if (object is Number) {
                result.add(((Number) object).intValue());
            }
        }

        return result;
    }

    public List<bool> getBooleanList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<bool>(0);
        }

        List<bool> result = new List<bool>();

        for (Object object : list) {
            if (object is bool) {
                result.add((bool) object);
            } else if (object is String) {
                if (bool.TRUE.toString().equals(object)) {
                    result.add(true);
                } else if (bool.FALSE.toString().equals(object)) {
                    result.add(false);
                }
            }
        }

        return result;
    }

    public List<Double> getDoubleList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<Double>(0);
        }

        List<Double> result = new List<Double>();

        for (Object object : list) {
            if (object is Double) {
                result.add((Double) object);
            } else if (object is String) {
                try {
                    result.add(Double.valueOf((String) object));
                } catch (Exception ex) {
                }
            } else if (object is Character) {
                result.add((double) ((Character) object).charValue());
            } else if (object is Number) {
                result.add(((Number) object).doubleValue());
            }
        }

        return result;
    }

    public List<Float> getFloatList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<Float>(0);
        }

        List<Float> result = new List<Float>();

        for (Object object : list) {
            if (object is Float) {
                result.add((Float) object);
            } else if (object is String) {
                try {
                    result.add(Float.valueOf((String) object));
                } catch (Exception ex) {
                }
            } else if (object is Character) {
                result.add((float) ((Character) object).charValue());
            } else if (object is Number) {
                result.add(((Number) object).floatValue());
            }
        }

        return result;
    }

    public List<Long> getLongList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<Long>(0);
        }

        List<Long> result = new List<Long>();

        for (Object object : list) {
            if (object is Long) {
                result.add((Long) object);
            } else if (object is String) {
                try {
                    result.add(Long.valueOf((String) object));
                } catch (Exception ex) {
                }
            } else if (object is Character) {
                result.add((long) ((Character) object).charValue());
            } else if (object is Number) {
                result.add(((Number) object).longValue());
            }
        }

        return result;
    }

    public List<Byte> getByteList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<Byte>(0);
        }

        List<Byte> result = new List<Byte>();

        for (Object object : list) {
            if (object is Byte) {
                result.add((Byte) object);
            } else if (object is String) {
                try {
                    result.add(Byte.valueOf((String) object));
                } catch (Exception ex) {
                }
            } else if (object is Character) {
                result.add((byte) ((Character) object).charValue());
            } else if (object is Number) {
                result.add(((Number) object).byteValue());
            }
        }

        return result;
    }

    public List<Character> getCharacterList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<Character>(0);
        }

        List<Character> result = new List<Character>();

        for (Object object : list) {
            if (object is Character) {
                result.add((Character) object);
            } else if (object is String) {
                String str = (String) object;

                if (str.Length == 1) {
                    result.add(str[0]);
                }
            } else if (object is Number) {
                result.add((char) ((Number) object).intValue());
            }
        }

        return result;
    }

    public List<Short> getShortList(String path) {
        List<?> list = getList(path);

        if (list == null) {
            return new List<Short>(0);
        }

        List<Short> result = new List<Short>();

        for (Object object : list) {
            if (object is Short) {
                result.add((Short) object);
            } else if (object is String) {
                try {
                    result.add(Short.valueOf((String) object));
                } catch (Exception ex) {
                }
            } else if (object is Character) {
                result.add((short) ((Character) object).charValue());
            } else if (object is Number) {
                result.add(((Number) object).shortValue());
            }
        }

        return result;
    }

    public List<Map<?, ?>> getMapList(String path) {
        List<?> list = getList(path);
        List<Map<?, ?>> result = new List<Map<?, ?>>();

        if (list == null) {
            return result;
        }

        for (Object object : list) {
            if (object is Map) {
                result.add((Dictionary<?, ?>) object);
            }
        }

        return result;
    }

    // Bukkit
    public Vector getVector(String path) {
        Object def = getDefault(path);
        return getVector(path, (def is Vector) ? (Vector) def : null);
    }

    public Vector getVector(String path, Vector def) {
        Object val = get(path, def);
        return (val is Vector) ? (Vector) val : def;
    }

    public bool isVector(String path) {
        Object val = get(path);
        return val is Vector;
    }

    public OfflinePlayer getOfflinePlayer(String path) {
        Object def = getDefault(path);
        return getOfflinePlayer(path, (def is OfflinePlayer) ? (OfflinePlayer) def : null);
    }

    public OfflinePlayer getOfflinePlayer(String path, OfflinePlayer def) {
        Object val = get(path, def);
        return (val is OfflinePlayer) ? (OfflinePlayer) val : def;
    }

    public bool isOfflinePlayer(String path) {
        Object val = get(path);
        return val is OfflinePlayer;
    }

    public ItemStack getItemStack(String path) {
        Object def = getDefault(path);
        return getItemStack(path, (def is ItemStack) ? (ItemStack) def : null);
    }

    public ItemStack getItemStack(String path, ItemStack def) {
        Object val = get(path, def);
        return (val is ItemStack) ? (ItemStack) val : def;
    }

    public bool isItemStack(String path) {
        Object val = get(path);
        return val is ItemStack;
    }

    public Color getColor(String path) {
        Object def = getDefault(path);
        return getColor(path, (def is Color) ? (Color) def : null);
    }

    public Color getColor(String path, Color def) {
        Object val = get(path, def);
        return (val is Color) ? (Color) val : def;
    }

    public bool isColor(String path) {
        Object val = get(path);
        return val is Color;
    }

    public ConfigurationSection getConfigurationSection(String path) {
        Object val = get(path, null);
        if (val != null) {
            return (val is ConfigurationSection) ? (ConfigurationSection) val : null;
        }

        val = get(path, getDefault(path));
        return (val is ConfigurationSection) ? createSection(path) : null;
    }

    public bool isConfigurationSection(String path) {
        Object val = get(path);
        return val is ConfigurationSection;
    }

    protected bool isPrimitiveWrapper(Object input) {
        return input is int || input is bool ||
                input is Character || input is Byte ||
                input is Short || input is Double ||
                input is Long || input is Float;
    }

    protected Object getDefault(String path) {
        if(path==null) throw new ArgumentNullException("Path cannot be null");

        Configuration root = getRoot();
        Configuration defaults = root == null ? null : root.getDefaults();
        return (defaults == null) ? null : defaults.get(createPath(this, path));
    }

    protected void mapChildrenKeys(HashSet<String> output, ConfigurationSection section, bool deep) {
        if (section is MemorySection) {
            MemorySection sec = (MemorySection) section;

            for (Map.Entry<String, Object> entry : sec.map.entrySet()) {
                output.add(createPath(section, entry.getKey(), this));

                if ((deep) && (entry.getValue() is ConfigurationSection)) {
                    ConfigurationSection subsection = (ConfigurationSection) entry.getValue();
                    mapChildrenKeys(output, subsection, deep);
                }
            }
        } else {
            HashSet<String> keys = section.getKeys(deep);

            for (String key : keys) {
                output.add(createPath(section, key, this));
            }
        }
    }

    protected void mapChildrenValues(Dictionary<String, Object> output, ConfigurationSection section, bool deep) {
        if (section is MemorySection) {
            MemorySection sec = (MemorySection) section;

            for (Map.Entry<String, Object> entry : sec.map.entrySet()) {
                output.put(createPath(section, entry.getKey(), this), entry.getValue());

                if (entry.getValue() is ConfigurationSection) {
                    if (deep) {
                        mapChildrenValues(output, (ConfigurationSection) entry.getValue(), deep);
                    }
                }
            }
        } else {
            Dictionary<String, Object> values = section.getValues(deep);

            for (Map.Entry<String, Object> entry : values.entrySet()) {
                output.put(createPath(section, entry.getKey(), this), entry.getValue());
            }
        }
    }

    /**
     * Creates a full path to the given {@link ConfigurationSection} from its
     * root {@link Configuration}.
     * <p>
     * You may use this method for any given {@link ConfigurationSection}, not
     * only {@link MemorySection}.
     *
     * @param section Section to create a path for.
     * @param key Name of the specified section.
     * @return Full path of the section from its root.
     */
    public static String createPath(ConfigurationSection section, String key) {
        return createPath(section, key, (section == null) ? null : section.getRoot());
    }

    /**
     * Creates a relative path to the given {@link ConfigurationSection} from
     * the given relative section.
     * <p>
     * You may use this method for any given {@link ConfigurationSection}, not
     * only {@link MemorySection}.
     *
     * @param section Section to create a path for.
     * @param key Name of the specified section.
     * @param relativeTo Section to create the path relative to.
     * @return Full path of the section from its root.
     */
    public static String createPath(ConfigurationSection section, String key, ConfigurationSection relativeTo) {
        if(section==null) throw new ArgumentNullException("Cannot create path without a section");
        Configuration root = section.getRoot();
        if (root == null) {
            throw new IllegalStateException("Cannot create path without a root");
        }
        char separator = root.options().pathSeparator();

        StringBuilder builder = new StringBuilder();
        if (section != null) {
            for (ConfigurationSection parent = section; (parent != null) && (parent != relativeTo); parent = parent.getParent()) {
                if (builder.Length > 0) {
                    builder.insert(0, separator);
                }

                builder.insert(0, parent.getName());
            }
        }

        if ((key != null) && (key.Length > 0)) {
            if (builder.Length > 0) {
                builder.Append(separator);
            }

            builder.Append(key);
        }

        return builder.toString();
    }

    public override string ToString() {
        Configuration root = getRoot();
        return new StringBuilder()
            .Append(getClass().getSimpleName())
            .Append("[path='")
            .Append(getCurrentPath())
            .Append("', root='")
            .Append(root == null ? null : root.getClass().getSimpleName())
            .Append("']")
            .toString();
    }
}
