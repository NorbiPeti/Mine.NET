namespace Mine.NET.util.io{

class Wrapper<T : Dictionary<String, ?> & Serializable> : Serializable {
    private static readonly long serialVersionUID = -986209235411767547L;

    readonly T map;

    static Wrapper<ImmutableMap<String, ?>> newWrapper(ConfigurationSerializable obj) {
        return new Wrapper<ImmutableMap<String, ?>>(ImmutableMap.<String, Object>builder().Add(ConfigurationSerialization.SERIALIZED_TYPE_KEY, ConfigurationSerialization.getAlias(obj.getClass())).putAll(obj.serialize()).build());
    }

    private Wrapper(T map) {
        this.map = map;
    }
}
