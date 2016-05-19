using Mine.NET.plugin;
using System;

namespace Mine.NET.metadata
{
    /**
     * Optional base class for facilitating MetadataValue implementations.
     * <p>
     * This provides all the conversion functions for MetadataValue so that
     * writing an implementation of MetadataValue is as simple as implementing
     * value() and invalidate().
     */
    public abstract class MetadataValueAdapter : MetadataValue
    {
        protected readonly WeakReference<Plugin> owningPlugin;

        protected MetadataValueAdapter(Plugin owningPlugin)
        {
            if (owningPlugin == null) throw new ArgumentNullException("owningPlugin cannot be null");
            this.owningPlugin = new WeakReference<Plugin>(owningPlugin);
        }

        public Plugin getOwningPlugin()
        {
            Plugin target = null;
            owningPlugin.TryGetTarget(out target);
            return target;
        }

        public int asInt()
        {
            return Convert.ToInt32(value());
        }

        public float asFloat()
        {
            return Convert.ToSingle(value());
        }

        public double asDouble()
        {
            return Convert.ToDouble(value());
        }

        public long asLong()
        {
            return Convert.ToInt64(value());
        }

        public short asShort()
        {
            return Convert.ToInt16(value());
        }

        public byte asByte()
        {
            return Convert.ToByte(value());
        }

        public bool asBoolean()
        {
            Object val = value();
            if (val is bool)
            {
                return (bool)val;
            }

            if (val.IsNumber())
            {
                return (int)val != 0;
            }

            if (val is String)
            {
                return bool.Parse((String)val);
            }

            return val != null;
        }

        public String asString()
        {
            Object val = value();

            if (val == null)
            {
                return "";
            }
            return val.ToString();
        }

        public abstract object value();
        public abstract void invalidate();
    }
}
