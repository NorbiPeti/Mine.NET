using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mine.NET.configuration.serialization
{
    /**
     * Utility class for storing and retrieving classes for {@link Configuration}.
     */
    public class ConfigurationSerialization {
    public static readonly String SERIALIZED_TYPE_KEY = "==";
    private readonly Type clazz;
    private static Dictionary<String, Type> aliases = new Dictionary<String, Class<? : ConfigurationSerializable>>();

    static ConfigurationSerialization() {
        registerClass(typeof(Vector));
        registerClass(typeof(BlockVector));
        registerClass(typeof(ItemStack));
        registerClass(typeof(Color));
        registerClass(typeof(PotionEffect));
        registerClass(typeof(FireworkEffect));
        registerClass(typeof(Pattern));
        registerClass(typeof(Location));
    }

    protected ConfigurationSerialization(Type clazz) {
            if (!typeof(ConfigurationSerializable).IsAssignableFrom(clazz.GetType()))
                throw new ArgumentException("Parameter has to be ConfigurationSerializable");
        this.clazz = clazz;
    }

    protected MethodInfo getMethod(String name, bool isStatic) {
            /*try {
                Method method = clazz.getDeclaredMethod(name, Map.class);

                if (!ConfigurationSerializable.class.isAssignableFrom(method.getReturnType())) {
                    return null;
                }
                if (Modifier.isStatic(method.getModifiers()) != isStatic) {
                    return null;
                }

                return method;
            } catch (NoSuchMethodException ex) {
                return null;
            } catch (SecurityException ex) {
                return null;
            }*/
            throw new InvalidOperationException("I don't want to use reflection."); //TODO
    }

    protected ConfigurationSerializable deserializeViaMethod<T>(MethodInfo method, Dictionary<String, T> args) {
        try {
            ConfigurationSerializable result = (ConfigurationSerializable) method.Invoke(null, args);

            if (result == null) {
                Logger.getLogger(typeof(ConfigurationSerialization).Name).Severe("Could not call method '" + method.ToString() + "' of " + clazz + " for deserialization: method returned null");
            } else {
                return result;
            }
        } catch (Exception ex) {
            Logger.getLogger(typeof(ConfigurationSerialization).Name).Severe(
                    "Could not call method '" + method.ToString() + "' of " + clazz + " for deserialization");
        }

        return null;
    }

    protected ConfigurationSerializable deserializeViaCtor(Constructor<? : ConfigurationSerializable> ctor, Dictionary<String, ?> args) {
        try {
            return ctor.newInstance(args);
        } catch (Exception ex) {
            Logger.getLogger(ConfigurationSerialization.class.getName()).log(
                    Level.SEVERE,
                    "Could not call constructor '" + ctor.ToString() + "' of " + clazz + " for deserialization",
                    ex is InvocationTargetException ? ex.getCause() : ex);
        }

        return null;
    }

    public ConfigurationSerializable deserialize(Dictionary<String, ?> args) {
        if(args==null) throw new ArgumentNullException("Args must not be null");

        ConfigurationSerializable result = null;
        Method method = null;

        if (result == null) {
            method = getMethod("deserialize", true);

            if (method != null) {
                result = deserializeViaMethod(method, args);
            }
        }

        if (result == null) {
            method = getMethod("valueOf", true);

            if (method != null) {
                result = deserializeViaMethod(method, args);
            }
        }

        if (result == null) {
            Constructor<? : ConfigurationSerializable> constructor = getConstructor();

            if (constructor != null) {
                result = deserializeViaCtor(constructor, args);
            }
        }

        return result;
    }

    /**
     * Attempts to deserialize the given arguments into a new instance of the
     * given class.
     * <p>
     * The class must implement {@link ConfigurationSerializable}, including
     * the extra methods as specified in the javadoc of
     * ConfigurationSerializable.
     * <p>
     * If a new instance could not be made, an example being the class not
     * fully implementing the interface, null will be returned.
     *
     * @param args Arguments for deserialization
     * @param clazz Class to deserialize into
     * @return New instance of the specified class
     */
    public static ConfigurationSerializable deserializeObject(Dictionary<String, ?> args, Class<? : ConfigurationSerializable> clazz) {
        return new ConfigurationSerialization(clazz).deserialize(args);
    }

    /**
     * Attempts to deserialize the given arguments into a new instance of the
     * given class.
     * <p>
     * The class must implement {@link ConfigurationSerializable}, including
     * the extra methods as specified in the javadoc of
     * ConfigurationSerializable.
     * <p>
     * If a new instance could not be made, an example being the class not
     * fully implementing the interface, null will be returned.
     *
     * @param args Arguments for deserialization
     * @return New instance of the specified class
     */
    public static ConfigurationSerializable deserializeObject(Dictionary<String, ?> args) {
        Class<? : ConfigurationSerializable> clazz = null;

        if (args.containsKey(SERIALIZED_TYPE_KEY)) {
            try {
                String alias = (String) args[SERIALIZED_TYPE_KEY];

                if (alias == null) {
                    throw new ArgumentException("Cannot have null alias");
                }
                clazz = getClassByAlias(alias);
                if (clazz == null) {
                    throw new ArgumentException("Specified class does not exist ('" + alias + "')");
                }
            } catch (ClassCastException ex) {
                ex.fillInStackTrace();
                throw ex;
            }
        } else {
            throw new ArgumentException("Args doesn't contain type key ('" + SERIALIZED_TYPE_KEY + "')");
        }

        return new ConfigurationSerialization(clazz).deserialize(args);
    }

    /**
     * Registers the given {@link ConfigurationSerializable} class by its
     * alias
     *
     * @param clazz Class to register
     */
    public static void registerClass(Class<? : ConfigurationSerializable> clazz) {
        DelegateDeserialization delegate = clazz.getAnnotation(DelegateDeserialization.class);

        if (delegate == null) {
            registerClass(clazz, getAlias(clazz));
            registerClass(clazz, clazz.getName());
        }
    }

    /**
     * Registers the given alias to the specified {@link
     * ConfigurationSerializable} class
     *
     * @param clazz Class to register
     * @param alias Alias to register as
     * @see SerializableAs
     */
    public static void registerClass(Class<? : ConfigurationSerializable> clazz, String alias) {
        aliases.Add(alias, clazz);
    }

    /**
     * Unregisters the specified alias to a {@link ConfigurationSerializable}
     *
     * @param alias Alias to unregister
     */
    public static void unregisterClass(String alias) {
        aliases.remove(alias);
    }

    /**
     * Unregisters any aliases for the specified {@link
     * ConfigurationSerializable} class
     *
     * @param clazz Class to unregister
     */
    public static void unregisterClass(Class<? : ConfigurationSerializable> clazz) {
        while (aliases.values().remove(clazz)) {
            ;
        }
    }

    /**
     * Attempts to get a registered {@link ConfigurationSerializable} class by
     * its alias
     *
     * @param alias Alias of the serializable
     * @return Registered class, or null if not found
     */
    public static Class<? : ConfigurationSerializable> getClassByAlias(String alias) {
        return aliases[alias];
    }

    /**
     * Gets the correct alias for the given {@link ConfigurationSerializable}
     * class
     *
     * @param clazz Class to get alias for
     * @return Alias to use for the class
     */
    public static String getAlias(Class<? : ConfigurationSerializable> clazz) {
        DelegateDeserialization delegate = clazz.getAnnotation(DelegateDeserialization.class);

        if (delegate != null) {
            if ((delegate.value() == null) || (delegate.value() == clazz)) {
                delegate = null;
            } else {
                return getAlias(delegate.value());
            }
        }

        if (delegate == null) {
            SerializableAs alias = clazz.getAnnotation(SerializableAs.class);

            if ((alias != null) && (alias.value() != null)) {
                return alias.value();
            }
        }

        return clazz.getName();
    }
}
}
