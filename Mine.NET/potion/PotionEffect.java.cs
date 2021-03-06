using Mine.NET.configuration.serialization;
using Mine.NET.entity;
using System;
using System.Collections.Generic;

namespace Mine.NET.potion
{
/**
 * Represents a potion effect, that can be added to a {@link LivingEntity}. A
 * potion effect has a duration that it will last for, an amplifier that will
 * enhance its effects, and a {@link PotionEffectType}, that represents its
 * effect on an entity.
 */
[SerializableAs("PotionEffect")]
public class PotionEffect : ConfigurationSerializable {
        private static readonly String AMPLIFIER = "amplifier";
        private static readonly String DURATION = "duration";
        private static readonly String TYPE = "effect";
        private static readonly String AMBIENT = "ambient";
        private static readonly String PARTICLES = "has-particles";
        private readonly int amplifier;
        private readonly int duration;
        private readonly PotionEffectType type;
        private readonly bool ambient;
        private readonly bool particles;
        private readonly Color color;

        /**
         * Creates a potion effect.
         * @param type effect type
         * @param duration measured in ticks, see {@link
         *     PotionEffect#getDuration()}
         * @param amplifier the amplifier, see {@link PotionEffect#getAmplifier()}
         * @param ambient the ambient status, see {@link PotionEffect#isAmbient()}
         * @param particles the particle status, see {@link PotionEffect#hasParticles()}
         * @param color the particle color, see {@link PotionEffect#getColor()}
         */
        public PotionEffect(PotionEffectType type, int duration, int amplifier, bool ambient, bool particles, Color color) {
            if (type == null) throw new ArgumentNullException("effect type cannot be null");
            this.type = type;
            this.duration = duration;
            this.amplifier = amplifier;
            this.ambient = ambient;
            this.particles = particles;
            this.color = color;
        }

        /**
         * Creates a potion effect with no defined color.
         *
         * @param type effect type
         * @param duration measured in ticks, see {@link
         *     PotionEffect#getDuration()}
         * @param amplifier the amplifier, see {@link PotionEffect#getAmplifier()}
         * @param ambient the ambient status, see {@link PotionEffect#isAmbient()}
         * @param particles the particle status, see {@link PotionEffect#hasParticles()}
         */
        public PotionEffect(PotionEffectType type, int duration, int amplifier, bool ambient, bool particles) : this(type, duration, amplifier, ambient, particles, null) {
        }

        /**
         * Creates a potion effect. Assumes that particles are visible
         *
         * @param type effect type
         * @param duration measured in ticks, see {@link
         *     PotionEffect#getDuration()}
         * @param amplifier the amplifier, see {@link PotionEffect#getAmplifier()}
         * @param ambient the ambient status, see {@link PotionEffect#isAmbient()}
         */
        public PotionEffect(PotionEffectType type, int duration, int amplifier, bool ambient) : this(type, duration, amplifier, ambient, true) {
        }

        /**
         * Creates a potion effect. Assumes ambient is true.
         *
         * @param type Effect type
         * @param duration measured in ticks
         * @param amplifier the amplifier for the effect
         * @see PotionEffect#PotionEffect(PotionEffectType, int, int, bool)
         */
        public PotionEffect(PotionEffectType type, int duration, int amplifier) : this(type, duration, amplifier, true) {
        }

        /**
         * Constructor for deserialization.
         *
         * @param map the map to deserialize from
         */
        public PotionEffect(Dictionary<String, Object> map) :
            this(getEffectType(map), getInt(map, DURATION), getInt(map, AMPLIFIER), getBool(map, AMBIENT, false), getBool(map, PARTICLES, true))
        {
        }

        private static PotionEffectType getEffectType(Dictionary<string,object> map) {
            int type = getInt(map, TYPE); //TODO: Remove IDs
            PotionEffectType effect = PotionEffectType.getById(type);
            if (effect != null) {
                return effect;
            }
            throw new KeyNotFoundException(map + " does not contain " + TYPE);
        }

        private static int getInt(Dictionary<string,object> map, string key) {
            Object num = map[key];
            if (num is int) {
                return (int)num;
            }
            throw new KeyNotFoundException(map + " does not contain " + key);
        }

        private static bool getBool(Dictionary<string,object> map, string key, bool def) {
            Object bool_ = map[key];
            if (bool_ is bool) {
                return (bool)bool_;
            }
            return def;
        }

        public Dictionary<String, Object> serialize() {
            return new Dictionary<string, object> {
                { TYPE, type },
                { DURATION, duration },
                { AMPLIFIER, amplifier },
                { AMBIENT, ambient },
                { PARTICLES, particles }
            };
        }

        /**
         * Attempts to add the effect represented by this object to the given
         * {@link LivingEntity}.
         *
         * @see LivingEntity#addPotionEffect(PotionEffect)
         * @param entity The entity to add this effect to
         * @return Whether the effect could be added
         */
        public bool apply(LivingEntity entity) {
            return entity.addPotionEffect(this);
        }

        public override bool Equals(Object obj) {
            if (this == obj) {
                return true;
            }
            if (!(obj is PotionEffect)) {
                return false;
            }
            PotionEffect that = (PotionEffect)obj;
            return this.type.Equals(that.type) && this.ambient == that.ambient && this.amplifier == that.amplifier && this.duration == that.duration && this.particles == that.particles;
        }

        /**
         * Returns the amplifier of this effect. A higher amplifier means the
         * potion effect happens more often over its duration and in some cases
         * has more effect on its target.
         *
         * @return The effect amplifier
         */
        public int getAmplifier() {
            return amplifier;
        }

        /**
         * Returns the duration (in ticks) that this effect will run for when
         * applied to a {@link LivingEntity}.
         *
         * @return The duration of the effect
         */
        public int getDuration() {
            return duration;
        }

        /**
         * Returns the {@link PotionEffectType} of this effect.
         *
         * @return The potion type of this effect
         */
        public PotionEffectType getType() {
            return type;
        }

        /**
         * Makes potion effect produce more, translucent, particles.
         *
         * @return if this effect is ambient
         */
        public bool isAmbient() {
            return ambient;
        }

        /**
         * @return whether this effect has particles or not
         */
        public bool hasParticles() {
            return particles;
        }

        /**
         * @return color of this potion's particles. May be null if the potion has no particles or defined color.
         */
        public Color getColor() {
            return color;
        }

        public override int GetHashCode() {
            int hash = 1;
            hash = hash * 31 + type.GetHashCode();
            hash = hash * 31 + amplifier;
            hash = hash * 31 + duration;
            hash ^= 0x22222222 >> (ambient ? 1 : -1);
            hash ^= 0x22222222 >> (particles ? 1 : -1);
            return hash;
        }

        public override string ToString() {
            return type.getName() + (ambient ? ":(" : ":") + duration + "t-x" + amplifier + (ambient ? ")" : "");
        }
    }
}
