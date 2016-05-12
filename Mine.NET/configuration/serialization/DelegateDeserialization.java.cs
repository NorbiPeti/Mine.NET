namespace Mine.NET.configuration.serialization
{
/**
 * Applies to a {@link ConfigurationSerializable} that will delegate all
 * deserialization to another {@link ConfigurationSerializable}.
 */
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface DelegateDeserialization {
    /**
     * Which class should be used as a delegate for this classes
     * deserialization
     *
     * @return Delegate class
     */
    public Class<? : ConfigurationSerializable> value();
}
}
