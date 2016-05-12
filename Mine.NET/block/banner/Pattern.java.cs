using Mine.NET.configuration.serialization;
using System;
using System.Collections.Generic;

namespace Mine.NET.block.banner
{
    [SerializableAs("Pattern")]
    public class Pattern : ConfigurationSerializable {

        private static readonly String COLOR = "color";
        private static readonly String PATTERN = "pattern";

        private readonly DyeColor color;
        private readonly PatternType pattern;

        /**
         * Creates a new pattern from the specified color and
         * pattern type
         *
         * @param color   the pattern color
         * @param pattern the pattern type
         */
        public Pattern(DyeColor color, PatternType pattern) {
            this.color = color;
            this.pattern = pattern;
        }

        /**
         * Constructor for deserialization.
         *
         * @param map the map to deserialize from
         */
        public Pattern(Dictionary<String, Object> map) {
            color = DyeColor.AllColors[(DyeColor.Colors)Enum.Parse(typeof(DyeColor.Colors), getString(map, COLOR))];
            pattern = PatternType.getByIdentifier(getString(map, PATTERN));
        }

        private static String getString<T,V>(Dictionary<T,V> map, T key) {
            object str = map[key];
            if (str is String) {
                return (String)str;
            }
            throw new KeyNotFoundException(map + " does not contain " + key);
        }
        
        public override Dictionary<String, Object> serialize() {
            return new Dictionary<string, object>
            {
                { COLOR, color.ToString() },
                { PATTERN, pattern.getIdentifier() }
            };
        }

        /**
         * Returns the color of the pattern
         *
         * @return the color of the pattern
         */
        public DyeColor getColor() {
            return color;
        }

        /**
         * Returns the type of pattern
         *
         * @return the pattern type
         */
        public PatternType getPattern() {
            return pattern;
        }

        public override int GetHashCode() {
            int hash = 3;
            hash = 97 * hash + (this.color != null ? this.color.GetHashCode() : 0);
            hash = 97 * hash + this.pattern.GetHashCode();
            return hash;
        }

        public override bool Equals(Object obj) {
            if (obj == null) {
                return false;
            }
            if (GetType() != obj.GetType()) {
                return false;
            }
            Pattern other = (Pattern)obj;
            return this.color == other.color && this.pattern == other.pattern;
        }
    }
}
