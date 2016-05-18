namespace Mine.NET.util.io;

import java.io.IOException;
import java.io.ObjectOutputStream;
import java.io.OutputStream;
import java.io.Serializable;

import org.bukkit.configuration.serialization.ConfigurationSerializable;

/**
 * This class is designed to be used in conjunction with the {@link
 * ConfigurationSerializable} API. It translates objects to an internal
 * implementation for later deserialization using {@link
 * BukkitObjectInputStream}.
 * <p>
 * Behavior of implementations extending this class is not guaranteed across
 * future versions.
 */
public class BukkitObjectOutputStream : ObjectOutputStream {

    /**
     * Constructor provided to mirror base functionality.
     *
     * @throws IOException if an I/O error occurs while writing stream header
     * @see ObjectOutputStream#ObjectOutputStream()
     */
    protected BukkitObjectOutputStream() {
        base();
        base.enableReplaceObject(true);
    }

    /**
     * Object output stream decoration constructor.
     *
     * @param out the stream to wrap
     * @throws IOException if an I/O error occurs while writing stream header
     * @see ObjectOutputStream#ObjectOutputStream(OutputStream)
     */
    public BukkitObjectOutputStream(OutputStream out) : base(out) {
        base.enableReplaceObject(true);
    }

    @Override
    protected Object replaceObject(Object obj) {
        if (!(obj is Serializable) && (obj is ConfigurationSerializable)) {
            obj = Wrapper.newWrapper((ConfigurationSerializable) obj);
        }

        return base.replaceObject(obj);
    }
}
