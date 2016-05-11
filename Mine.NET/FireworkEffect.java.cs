using System;
using System.Collections.Generic;

namespace Mine.NET
{
    /**
    * Represents a single firework effect.
*/
    [Serializable]
    public sealed class FireworkEffect : ConfigurationSerializable
    {

        /**
         * The type or shape of the effect.
         */
        public enum FireworkType
        {
            /**
             * A small ball effect.
             */
            BALL,
            /**
             * A large ball effect.
             */
            BALL_LARGE,
            /**
             * A star-shaped effect.
             */
            STAR,
            /**
             * A burst effect.
             */
            BURST,
            /**
             * A creeper-face effect.
             */
            CREEPER
        }

        /**
         * Construct a firework effect.
         *
         * @return A utility object for building a firework effect
         */
        public static Builder builder()
        {
            return new Builder();
        }

        /**
         * This is a builder for FireworkEffects.
         *
         * @see FireworkEffect#builder()
         */
        public sealed class Builder
        {
            bool flicker = false;
            bool trail = false;
            readonly List<Color> colors = new List<Color>();
            List<Color> fadeColors = null;
            FireworkType type = FireworkType.BALL;

            public Builder() { }

            /**
             * Specify the type of the firework effect.
             *
             * @param type The effect type
             * @return This object, for chaining
             * @throws ArgumentException If type is null
             */
            public Builder with(FireworkType type)
            {
                this.type = type;
                return this;
            }

            /**
             * Add a flicker to the firework effect.
             *
             * @return This object, for chaining
             */
            public Builder withFlicker()
            {
                flicker = true;
                return this;
            }

            /**
             * Set whether the firework effect should flicker.
             *
             * @param flicker true if it should flicker, false if not
             * @return This object, for chaining
             */
            public Builder SetFlicker(bool flicker)
            {
                this.flicker = flicker;
                return this;
            }

            /**
             * Add a trail to the firework effect.
             *
             * @return This object, for chaining
             */
            public Builder withTrail()
            {
                trail = true;
                return this;
            }

            /**
             * Set whether the firework effect should have a trail.
             *
             * @param trail true if it should have a trail, false for no trail
             * @return This object, for chaining
             */
            public Builder SetTrail(bool trail)
            {
                this.trail = trail;
                return this;
            }

            /**
             * Add a primary color to the firework effect.
             *
             * @param color The color to add
             * @return This object, for chaining
             * @throws ArgumentException If color is null
             */
            public Builder withColor(Color color)
            {
                if (color == null)
                    throw new ArgumentNullException("Cannot have null color");

                colors.Add(color);

                return this;
            }

            /**
             * Add several primary colors to the firework effect.
             *
             * @param colors The colors to add
             * @return This object, for chaining
             * @throws ArgumentException If colors is null
             * @throws ArgumentException If any color is null (may be
             *     thrown after changes have occurred)
             */
            public Builder withColor(params Color[] colors)
            {
                if (colors == null)
                    throw new ArgumentNullException("Cannot have null colors");
                if (colors.Length == 0)
                {
                    return this;
                }

                List<Color> list = this.colors;
                foreach (Color color in colors)
                {
                    if (color == null)
                        throw new ArgumentNullException("Color cannot be null");
                    list.Add(color);
                }

                return this;
            }

            /**
             * Add several primary colors to the firework effect.
             *
             * @param colors An iterable object whose iterator yields the desired
             *     colors
             * @return This object, for chaining
             * @throws ArgumentException If colors is null
             * @throws ArgumentException If any color is null (may be
             *     thrown after changes have occurred)
             */
            public Builder withColor(IEnumerable<Color> colors)
            {
                if (colors == null)
                    throw new ArgumentNullException("Cannot have null colors");

                this.colors.AddRange(colors);

                return this;
            }

            /**
             * Add a fade color to the firework effect.
             *
             * @param color The color to add
             * @return This object, for chaining
             * @throws ArgumentException If colors is null
             * @throws ArgumentException If any color is null (may be
             *     thrown after changes have occurred)
             */
            public Builder withFade(Color color)
            {
                if (colors == null)
                    throw new ArgumentNullException("Cannot have null color");

                if (fadeColors == null)
                {
                    fadeColors = new List<Color>();
                }

                fadeColors.Add(color);

                return this;
            }

            /**
             * Add several fade colors to the firework effect.
             *
             * @param colors The colors to add
             * @return This object, for chaining
             * @throws ArgumentException If colors is null
             * @throws ArgumentException If any color is null (may be
             *     thrown after changes have occurred)
             */
            public Builder withFade(params Color[] colors)
            {
                if (colors == null)
                    throw new ArgumentNullException("Cannot have null colors");
                if (colors.Length == 0)
                {
                    return this;
                }

                List<Color> list = this.fadeColors;
                if (list == null)
                {
                    list = this.fadeColors = new List<Color>();
                }

                foreach (Color color in colors)
                {
                    if (colors == null)
                        throw new ArgumentNullException("Color cannot be null");
                    list.Add(color);
                }

                return this;
            }

            /**
             * Add several fade colors to the firework effect.
             *
             * @param colors An iterable object whose iterator yields the desired
             *     colors
             * @return This object, for chaining
             * @throws ArgumentException If colors is null
             * @throws ArgumentException If any color is null (may be
             *     thrown after changes have occurred)
             */
            public Builder withFade(IEnumerable<Color> colors)
            {
                if (colors == null)
                    throw new ArgumentNullException("Cannot have null colors");

                this.colors.AddRange(colors);

                return this;
            }

            /**
             * Create a {@link FireworkEffect} from the current contents of this
             * builder.
             * <p>
             * To successfully build, you must have specified at least one color.
             *
             * @return The representative firework effect
             */
            public FireworkEffect build()
            {
                return new FireworkEffect(
                    flicker,
                    trail,
                    colors,
                    fadeColors == null ? new List<Color>() : fadeColors,
                    type
                );
            }
        }

        private static readonly String FLICKER = "flicker";
        private static readonly String TRAIL = "trail";
        private static readonly String COLORS = "colors";
        private static readonly String FADE_COLORS = "fade-colors";
        private static readonly String TYPE = "type";

        private readonly bool flicker;
        private readonly bool trail;
        private readonly List<Color> colors;
        private readonly List<Color> fadeColors;
        private readonly FireworkType type;
        private String string_ = null;

        FireworkEffect(bool flicker, bool trail, List<Color> colors, List<Color> fadeColors, FireworkType type)
        {
            if (colors.Count == 0)
            {
                throw new InvalidOperationException("Cannot make FireworkEffect without any color");
            }
            this.flicker = flicker;
            this.trail = trail;
            this.colors = colors;
            this.fadeColors = fadeColors;
            this.type = type;
        }

        /**
         * Get whether the firework effect flickers.
         *
         * @return true if it flickers, false if not
         */
        public bool hasFlicker()
        {
            return flicker;
        }

        /**
         * Get whether the firework effect has a trail.
         *
         * @return true if it has a trail, false if not
         */
        public bool hasTrail()
        {
            return trail;
        }

        /**
         * Get the primary colors of the firework effect.
         *
         * @return An immutable list of the primary colors
         */
        public List<Color> getColors()
        {
            return colors;
        }

        /**
         * Get the fade colors of the firework effect.
         *
         * @return An immutable list of the fade colors
         */
        public List<Color> getFadeColors()
        {
            return fadeColors;
        }

        /**
         * Get the type of the firework effect.
         *
         * @return The effect type
         */
        public FireworkType getType()
        {
            return type;
        }

        /**
         * @see ConfigurationSerializable
         * @param map the map to deserialize
         * @return the resulting serializable
         */
        public static ConfigurationSerializable deserialize(Dictionary<String, Object> map)
        {
            FireworkType type = FireworkType.BALL;
            if (!Enum.TryParse<FireworkType>((String)map[TYPE], out type))
            {
                throw new ArgumentException(map[TYPE] + " is not a valid Type");
            }

            return builder()
                .SetFlicker((bool)map[FLICKER])
                .SetTrail((bool)map[TRAIL])
                .withColor((IEnumerable<Color>)map[COLORS])
                .withFade((IEnumerable<Color>)map[FADE_COLORS])
                .with(type)
                .build();
        }

        public override Dictionary<String, Object> serialize()
        {
            return new Dictionary<string, object>
            {
            { FLICKER, flicker} ,
            { TRAIL, trail },
            { COLORS, colors },
            { FADE_COLORS, fadeColors },
            { TYPE, type.ToString() }
        };
        }

        public override String ToString()
        {
            String string_ = this.string_;
            if (string_ == null)
            {
                return this.string_ = "FireworkEffect:" + serialize();
            }
            return string_;
        }

        public override int GetHashCode()
        {
            /**
             * TRUE and FALSE as per bool.hashCode()
             */
            int PRIME = 31, TRUE = 1231, FALSE = 1237;
            int hash = 1;
            hash = hash * PRIME + (flicker ? TRUE : FALSE);
            hash = hash * PRIME + (trail ? TRUE : FALSE);
            hash = hash * PRIME + type.GetHashCode();
            hash = hash * PRIME + colors.GetHashCode();
            hash = hash * PRIME + fadeColors.GetHashCode();
            return hash;
        }

        public override bool Equals(Object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (!typeof(FireworkEffect).IsAssignableFrom(obj.GetType()))
            {
                return false;
            }

            FireworkEffect that = (FireworkEffect)obj;
            return this.flicker == that.flicker
                    && this.trail == that.trail
                    && this.type == that.type
                    && this.colors.Equals(that.colors)
                    && this.fadeColors.Equals(that.fadeColors);
        }
    }
}
