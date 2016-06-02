using System;
using System.IO;

namespace Mine.NET.util.io{

/**
 * This class is designed to be used in conjunction with the {@link
 * ConfigurationSerializable} API. It translates objects back to their
 * original implementation after being serialized by {@link
 * BukkitObjectInputStream}.
 * <p>
 * Behavior of implementations extending this class is not guaranteed across
 * future versions.
 */
public class BukkitObjectInputStream : ObjectInputStream {

    /**
     * Constructor provided to mirror base functionality.
     *
     * @throws IOException if an I/O error occurs while reading stream heade
     * @see ObjectInputStream#ObjectInputStream()
     */
    protected BukkitObjectInputStream() {
        base();
        base.enableResolveObject(true);
    }

    /**
     * Object input stream decoration constructor.
     *
     * @param in the input stream to wrap
     * @throws IOException if an I/O error occurs while reading stream header
     * @see ObjectInputStream#ObjectInputStream(InputStream)
     */
    public BukkitObjectInputStream(Stream in_) : base(in_) {
        base.enableResolveObject(true);
    }
        
    protected override Object resolveObject(Object obj) {
        if (obj is Wrapper) {
            try {
                (obj = ConfigurationSerialization.deserializeObject(((Wrapper<?>) obj).map)).getClass(); // NPE
            } catch (Exception ex) {
                throw newIOException("Failed to deserialize object", ex);
            }
        }

        return base.resolveObject(obj);
    }

    private static IOException newIOException(String string, Exception cause) {
        IOException exception = new IOException(string);
        exception.initCause(cause);
        return exception;
    }
}
