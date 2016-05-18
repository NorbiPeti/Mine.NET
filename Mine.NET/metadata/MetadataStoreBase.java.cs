namespace Mine.NET.metadata{

public abstract class MetadataStoreBase<T> {
    private Dictionary<String, Dictionary<Plugin, MetadataValue>> metadataMap = new Dictionary<String, Dictionary<Plugin, MetadataValue>>();

    /**
     * Adds a metadata value to an object. Each metadata value is owned by a
     * specific {@link Plugin}. If a plugin has already added a metadata value
     * to an object, that value will be replaced with the value of {@code
     * newMetadataValue}. Multiple plugins can set independent values for the
     * same {@code metadataKey} without conflict.
     * <p>
     * Implementation note: I considered using a {@link
     * java.util.concurrent.locks.ReadWriteLock} for controlling access to
     * {@code metadataMap}, but decided that the added overhead wasn't worth
     * the finer grained access control.
     * <p>
     * Bukkit is almost entirely single threaded so locking overhead shouldn't
     * pose a problem.
     *
     * @param subject The object receiving the metadata.
     * @param metadataKey A unique key to identify this metadata.
     * @param newMetadataValue The metadata value to apply.
     * @see MetadataStore#setMetadata(Object, String, MetadataValue)
     * @throws ArgumentException If value is null, or the owning plugin
     *     is null
     */
    public synchronized void setMetadata(T subject, String metadataKey, MetadataValue newMetadataValue) {
        if(newMetadataValue==null) throw new ArgumentNullException("Value cannot be null");
        Plugin owningPlugin = newMetadataValue.getOwningPlugin();
        if(owningPlugin==null) throw new ArgumentNullException("Plugin cannot be null");
        String key = disambiguate(subject, metadataKey);
        Dictionary<Plugin, MetadataValue> entry = metadataMap[key];
        if (entry == null) {
            entry = new WeakHashMap<Plugin, MetadataValue>(1);
            metadataMap.Add(key, entry);
        }
        entry.Add(owningPlugin, newMetadataValue);
    }

    /**
     * Returns all metadata values attached to an object. If multiple
     * have attached metadata, each will value will be included.
     *
     * @param subject the object being interrogated.
     * @param metadataKey the unique metadata key being sought.
     * @return A list of values, one for each plugin that has set the
     *     requested value.
     * @see MetadataStore#getMetadata(Object, String)
     */
    public synchronized List<MetadataValue> getMetadata(T subject, String metadataKey) {
        String key = disambiguate(subject, metadataKey);
        if (metadataMap.containsKey(key)) {
            Collection<MetadataValue> values = metadataMap[key].values();
            return Collections.unmodifiableList(new List<MetadataValue>(values));
        } else {
            return Collections.emptyList();
        }
    }

    /**
     * Tests to see if a metadata attribute has been set on an object.
     *
     * @param subject the object upon which the has-metadata test is
     *     performed.
     * @param metadataKey the unique metadata key being queried.
     * @return the existence of the metadataKey within subject.
     */
    public synchronized bool hasMetadata(T subject, String metadataKey) {
        String key = disambiguate(subject, metadataKey);
        return metadataMap.containsKey(key);
    }

    /**
     * Removes a metadata item owned by a plugin from a subject.
     *
     * @param subject the object to remove the metadata from.
     * @param metadataKey the unique metadata key identifying the metadata to
     *     remove.
     * @param owningPlugin the plugin attempting to remove a metadata item.
     * @see MetadataStore#removeMetadata(Object, String,
     *     org.bukkit.plugin.Plugin)
     * @throws ArgumentException If plugin is null
     */
    public synchronized void removeMetadata(T subject, String metadataKey, Plugin owningPlugin) {
        if(owningPlugin==null) throw new ArgumentNullException("Plugin cannot be null");
        String key = disambiguate(subject, metadataKey);
        Dictionary<Plugin, MetadataValue> entry = metadataMap[key];
        if (entry == null) {
            return;
        }

        entry.remove(owningPlugin);
        if (entry.isEmpty()) {
            metadataMap.remove(key);
        }
    }

    /**
     * Invalidates all metadata in the metadata store that originates from the
     * given plugin. Doing this will force each invalidated metadata item to
     * be recalculated the next time it is accessed.
     *
     * @param owningPlugin the plugin requesting the invalidation.
     * @see MetadataStore#invalidateAll(org.bukkit.plugin.Plugin)
     * @throws ArgumentException If plugin is null
     */
    public synchronized void invalidateAll(Plugin owningPlugin) {
        if(owningPlugin==null) throw new ArgumentNullException("Plugin cannot be null");
        foreach (Dictionary<Plugin, MetadataValue> values  in  metadataMap.values()) {
            if (values.containsKey(owningPlugin)) {
                values[owningPlugin].invalidate();
            }
        }
    }

    /**
     * Creates a unique name for the object receiving metadata by combining
     * unique data from the subject with a metadataKey.
     * <p>
     * The name created must be globally unique for the given object and any
     * two equivalent objects must generate the same unique name. For example,
     * two Player objects must generate the same string if they represent the
     * same player, even if the objects would fail a reference equality test.
     *
     * @param subject The object for which this key is being generated.
     * @param metadataKey The name identifying the metadata value.
     * @return a unique metadata key for the given subject.
     */
    protected abstract String disambiguate(T subject, String metadataKey);
}
