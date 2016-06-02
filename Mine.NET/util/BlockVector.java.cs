using Mine.NET.configuration.serialization;
using System;
using System.Collections.Generic;

namespace Mine.NET.util
{
    /**
     * A vector with a hash function that floors the X, Y, Z components, a la
     * BlockVector in WorldEdit. BlockVectors can be used in hash sets and
     * hash maps. Be aware that BlockVectors are mutable, but it is important
     * that BlockVectors are never changed once put into a hash set or hash map.
     */
    [SerializableAs("BlockVector")]
    public class BlockVector : Vector
    {
        /**
         * Construct the vector with all components as 0.
         */
        public BlockVector()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        /**
         * Construct the vector with another vector.
         *
         * @param vec The other vector.
         */
        public BlockVector(Vector vec)
        {
            this.x = vec.getX();
            this.y = vec.getY();
            this.z = vec.getZ();
        }

        /**
         * Construct the vector with provided int components.
         *
         * @param x X component
         * @param y Y component
         * @param z Z component
         */
        public BlockVector(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /**
         * Construct the vector with provided double components.
         *
         * @param x X component
         * @param y Y component
         * @param z Z component
         */
        public BlockVector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /**
         * Construct the vector with provided float components.
         *
         * @param x X component
         * @param y Y component
         * @param z Z component
         */
        public BlockVector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /**
         * Checks if another object is equivalent.
         *
         * @param obj The other object
         * @return whether the other object is equivalent
         */
        public override bool Equals(Object obj)
        {
            if (!(obj is BlockVector))
            {
                return false;
            }
            BlockVector other = (BlockVector)obj;

            return (int)other.getX() == (int)this.x && (int)other.getY() == (int)this.y && (int)other.getZ() == (int)this.z;

        }

        /**
         * Returns a hash code for this vector.
         *
         * @return hash code
         */
        public override int GetHashCode()
        {
            return (x.GetHashCode() >> 13) ^ (y.GetHashCode() >> 7) ^ (z.GetHashCode());
        }

        /**
         * Get a new block vector.
         *
         * @return vector
         */
        public override BlockVector clone()
        {
            return (BlockVector)base.clone(); //TODO
        }

        public static new BlockVector deserialize(Dictionary<String, Object> args)
        {
            double x = 0;
            double y = 0;
            double z = 0;

            if (args.ContainsKey("x"))
            {
                x = (Double)args["x"];
            }
            if (args.ContainsKey("y"))
            {
                y = (Double)args["y"];
            }
            if (args.ContainsKey("z"))
            {
                z = (Double)args["z"];
            }

            return new BlockVector(x, y, z);
        }
    }
}
