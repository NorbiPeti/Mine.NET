using System;

namespace Mine.NET.util
{
    /**
     * Utils for casting number types to other number types
     */
    public sealed class NumberConversions {
        private NumberConversions() { }

        public static int floor(double num) {
            int floor = (int)num;
            return floor == num ? floor : floor - (int)(Double.doubleToRawLongBits(num) >>> 63);
        }

        public static int ceil(double num) {
            int floor = (int)num;
            return floor == num ? floor : floor + (int)(~Double.doubleToRawLongBits(num) >>> 63);
        }

        public static int round(double num) {
            return floor(num + 0.5d);
        }

        public static double square(double num) {
            return num * num;
        }

        public static int toInt(Object object_) {
            if (object_.IsNumber()) {
                return (int)object_;
            }

            try {
                return int.Parse(object_.ToString());
            } catch (FormatException) {
            } catch (NullReferenceException) {
            }
            return 0;
        }

        public static float toFloat(Object object_) {
            if (object_.IsNumber())
            {
                return (float)object_;
            }

            try {
                return float.Parse(object_.ToString());
            } catch (FormatException) {
            } catch (NullReferenceException) {
            }
            return 0;
        }

        public static double toDouble(Object object_) {
            if (object_.IsNumber()) {
                return (double)object_;
            }

            try {
                return Double.Parse(object_.ToString());
            } catch (FormatException) {
            } catch (NullReferenceException) {
            }
            return 0;
        }

        public static long toLong(Object object_) {
            if (object_.IsNumber())
            {
                return (long)object_;
            }

            try {
                return long.Parse(object_.ToString());
            } catch (FormatException) {
            } catch (NullReferenceException) {
            }
            return 0;
        }

        public static short toShort(Object object_) {
            if (object_.IsNumber())
            {
                return (short)object_;
            }

            try {
                return short.Parse(object_.ToString());
            } catch (FormatException) {
            } catch (NullReferenceException) {
            }
            return 0;
        }

        public static byte toByte(Object object_) {
            if (object_.IsNumber())
            {
                return (byte)object_;
            }

            try {
                return Byte.Parse(object_.ToString());
            } catch (FormatException) {
            } catch (NullReferenceException) {
            }
            return 0;
        }

        public static bool isFinite(double d) {
            return Math.Abs(d) <= Double.MaxValue;
        }

        public static bool isFinite(float f) {
            return Math.Abs(f) <= float.MaxValue;
        }

        public static void checkFinite(double d, String message) {
            if (!isFinite(d)) {
                throw new ArgumentException(message);
            }
        }

        public static void checkFinite(float d, String message) {
            if (!isFinite(d)) {
                throw new ArgumentException(message);
            }
        }
    }
}
