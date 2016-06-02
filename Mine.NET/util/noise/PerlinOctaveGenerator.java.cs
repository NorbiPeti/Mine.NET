using System;

namespace Mine.NET.util.noise
{
    /**
     * Creates perlin noise through unbiased octaves
     */
    public class PerlinOctaveGenerator : OctaveGenerator
    {

        /**
         * Creates a perlin octave generator for the given world
         *
         * @param world World to construct this generator for
         * @param octaves Amount of octaves to create
         */
        public PerlinOctaveGenerator(World world, int octaves) :
            this(new JavaRand(world.getSeed()), octaves)
        {
        }

        /**
         * Creates a perlin octave generator for the given world
         *
         * @param seed Seed to construct this generator for
         * @param octaves Amount of octaves to create
         */
        public PerlinOctaveGenerator(long seed, int octaves) :
            this(new JavaRand(seed), octaves)
        {
        }

        /**
         * Creates a perlin octave generator for the given {@link JavaRand}
         *
         * @param rand JavaRand object to construct this generator for
         * @param octaves Amount of octaves to create
         */
        public PerlinOctaveGenerator(JavaRand rand, int octaves) :
            base(createOctaves(rand, octaves))
        {
        }

        private static NoiseGenerator[] createOctaves(JavaRand rand, int octaves)
        {
            NoiseGenerator[] result = new NoiseGenerator[octaves];

            for (int i = 0; i < octaves; i++)
            {
                result[i] = new PerlinNoiseGenerator(rand);
            }

            return result;
        }
    }
}
