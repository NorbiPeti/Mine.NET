using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine.NET.util
{
    public sealed class JavaRand
    {
        public JavaRand()
        {
            _seed = new JavaRand().Next();
            _seed = NextLong();
        }

        public JavaRand(long seed)
        {
            _seed = (seed ^ LARGE_PRIME) & ((1L << 48) - 1);
        }

        public long NextLong()
        {
            return ((long)next(32) << 32) + next(32);
        }

        public double NextDouble()
        {
            return (((long)next(26) << 27) + next(27))
              / (double)(1L << 53);
        }

        public int NextInt(int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException("n", n, "n must be positive");

            if ((n & -n) == n)  // i.e., n is a power of 2
                return (int)((n * (long)next(31)) >> 31);

            int bits, val;

            do
            {
                bits = next(31);
                val = bits % n;
            } while (bits - val + (n - 1) < 0);
            return val;
        }

        private int next(int bits)
        {
            _seed = (_seed * LARGE_PRIME + SMALL_PRIME) & ((1L << 48) - 1);
            return (int)(((uint)_seed) >> (48 - bits));
        }

        private long _seed;

        private const long LARGE_PRIME = 0x5DEECE66DL;
        private const long SMALL_PRIME = 0xBL;
    }
}
