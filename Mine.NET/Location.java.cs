using Mine.NET.block;
using Mine.NET.util;
using System;
using System.Collections.Generic;

namespace Mine.NET
{
    /**
    * Represents a 3-dimensional position in a world
*/
    public class Location : ICloneable
    {
        private World world;
        private double x;
        private double y;
        private double z;
        private float pitch;
        private float yaw;

        /**
         * Constructs a new Location with the given coordinates
         *
         * @param world The world in which this location resides
         * @param x The x-coordinate of this new location
         * @param y The y-coordinate of this new location
         * @param z The z-coordinate of this new location
         */
        public Location(World world, double x, double y, double z)
        {
            new Location(world, x, y, z, 0, 0);
        }

        /**
         * Constructs a new Location with the given coordinates and direction
         *
         * @param world The world in which this location resides
         * @param x The x-coordinate of this new location
         * @param y The y-coordinate of this new location
         * @param z The z-coordinate of this new location
         * @param yaw The absolute rotation on the x-plane, in degrees
         * @param pitch The absolute rotation on the y-plane, in degrees
         */
        public Location(World world, double x, double y, double z, float yaw, float pitch)
        {
            this.world = world;
            this.x = x;
            this.y = y;
            this.z = z;
            this.pitch = pitch;
            this.yaw = yaw;
        }

        /**
         * Sets the world that this location resides in
         *
         * @param world New world that this location resides in
         */
        public void setWorld(World world)
        {
            this.world = world;
        }

        /**
         * Gets the world that this location resides in
         *
         * @return World that contains this location
         */
        public World getWorld()
        {
            return world;
        }

        /**
         * Gets the chunk at the represented location
         *
         * @return Chunk at the represented location
         */
        public Chunk getChunk()
        {
            return world.getChunkAt(this);
        }

        /**
         * Gets the block at the represented location
         *
         * @return Block at the represented location
         */
        public Block getBlock()
        {
            return world.getBlockAt(this);
        }

        /**
         * Sets the x-coordinate of this location
         *
         * @param x X-coordinate
         */
        public void setX(double x)
        {
            this.x = x;
        }

        /**
         * Gets the x-coordinate of this location
         *
         * @return x-coordinate
         */
        public double getX()
        {
            return x;
        }

        /**
         * Gets the floored value of the X component, indicating the block that
         * this location is contained with.
         *
         * @return block X
         */
        public int getBlockX()
        {
            return locToBlock(x);
        }

        /**
         * Sets the y-coordinate of this location
         *
         * @param y y-coordinate
         */
        public void setY(double y)
        {
            this.y = y;
        }

        /**
         * Gets the y-coordinate of this location
         *
         * @return y-coordinate
         */
        public double getY()
        {
            return y;
        }

        /**
         * Gets the floored value of the Y component, indicating the block that
         * this location is contained with.
         *
         * @return block y
         */
        public int getBlockY()
        {
            return locToBlock(y);
        }

        /**
         * Sets the z-coordinate of this location
         *
         * @param z z-coordinate
         */
        public void setZ(double z)
        {
            this.z = z;
        }

        /**
         * Gets the z-coordinate of this location
         *
         * @return z-coordinate
         */
        public double getZ()
        {
            return z;
        }

        /**
         * Gets the floored value of the Z component, indicating the block that
         * this location is contained with.
         *
         * @return block z
         */
        public int getBlockZ()
        {
            return locToBlock(z);
        }

        /**
         * Sets the yaw of this location, measured in degrees.
         * <ul>
         * <li>A yaw of 0 or 360 represents the positive z direction.
         * <li>A yaw of 180 represents the negative z direction.
         * <li>A yaw of 90 represents the negative x direction.
         * <li>A yaw of 270 represents the positive x direction.
         * </ul>
         * Increasing yaw values are the equivalent of turning to your
         * right-facing, increasing the scale of the next respective axis, and
         * decreasing the scale of the previous axis.
         *
         * @param yaw new rotation's yaw
         */
        public void setYaw(float yaw)
        {
            this.yaw = yaw;
        }

        /**
         * Gets the yaw of this location, measured in degrees.
         * <ul>
         * <li>A yaw of 0 or 360 represents the positive z direction.
         * <li>A yaw of 180 represents the negative z direction.
         * <li>A yaw of 90 represents the negative x direction.
         * <li>A yaw of 270 represents the positive x direction.
         * </ul>
         * Increasing yaw values are the equivalent of turning to your
         * right-facing, increasing the scale of the next respective axis, and
         * decreasing the scale of the previous axis.
         *
         * @return the rotation's yaw
         */
        public float getYaw()
        {
            return yaw;
        }

        /**
         * Sets the pitch of this location, measured in degrees.
         * <ul>
         * <li>A pitch of 0 represents level forward facing.
         * <li>A pitch of 90 represents downward facing, or negative y
         *     direction.
         * <li>A pitch of -90 represents upward facing, or positive y direction.
         * </ul>
         * Increasing pitch values the equivalent of looking down.
         *
         * @param pitch new incline's pitch
         */
        public void setPitch(float pitch)
        {
            this.pitch = pitch;
        }

        /**
         * Gets the pitch of this location, measured in degrees.
         * <ul>
         * <li>A pitch of 0 represents level forward facing.
         * <li>A pitch of 90 represents downward facing, or negative y
         *     direction.
         * <li>A pitch of -90 represents upward facing, or positive y direction.
         * </ul>
         * Increasing pitch values the equivalent of looking down.
         *
         * @return the incline's pitch
         */
        public float getPitch()
        {
            return pitch;
        }

        /**
         * Gets a unit-vector pointing in the direction that this Location is
         * facing.
         *
         * @return a vector pointing the direction of this location's {@link
         *     #getPitch() pitch} and {@link #getYaw() yaw}
         */
        public Vector getDirection()
        {
            Vector vector = new Vector();

            double rotX = this.getYaw();
            double rotY = this.getPitch();

            vector.setY(-Math.Sin((Math.PI / 180) * rotY));

            double xz = Math.Cos((Math.PI / 180) * rotY);

            vector.setX(-xz * Math.Sin((Math.PI / 180) * rotX));
            vector.setZ(xz * Math.Cos((Math.PI / 180) * rotX));

            return vector;
        }

        /**
         * Sets the {@link #getYaw() yaw} and {@link #getPitch() pitch} to point
         * in the direction of the vector.
         * 
         * @param vector the direction vector
         * @return the same location
         */
        public Location setDirection(Vector vector)
        {
            /*
             * Sin = Opp / Hyp
             * Cos = Adj / Hyp
             * Tan = Opp / Adj
             *
             * x = -Opp
             * z = Adj
             */
            double _2PI = 2 * Math.PI;
            double x = vector.getX();
            double z = vector.getZ();

            if (x == 0 && z == 0)
            {
                pitch = vector.getY() > 0 ? -90 : 90;
                return this;
            }

            double theta = Math.Atan2(-x, z);
            yaw = (float)(((theta + _2PI) % _2PI) * (180.0 / Math.PI));

            double x2 = NumberConversions.square(x);
            double z2 = NumberConversions.square(z);
            double xz = Math.Sqrt(x2 + z2);
            pitch = (float)(Math.Atan(-vector.getY() / xz) * (180.0 / Math.PI));

            return this;
        }

        /**
         * Adds the location by another.
         *
         * @see Vector
         * @param vec The other location
         * @return the same location
         * @throws ArgumentException for differing worlds
         */
        public Location add(Location vec)
        {
            if (vec == null || vec.getWorld() != getWorld())
            {
                throw new ArgumentException("Cannot add Locations of differing worlds");
            }

            x += vec.x;
            y += vec.y;
            z += vec.z;
            return this;
        }

        /**
         * Adds the location by a vector.
         *
         * @see Vector
         * @param vec Vector to use
         * @return the same location
         */
        public Location add(Vector vec)
        {
            this.x += vec.getX();
            this.y += vec.getY();
            this.z += vec.getZ();
            return this;
        }

        /**
         * Adds the location by another. Not world-aware.
         *
         * @see Vector
         * @param x X coordinate
         * @param y Y coordinate
         * @param z Z coordinate
         * @return the same location
         */
        public Location add(double x, double y, double z)
        {
            this.x += x;
            this.y += y;
            this.z += z;
            return this;
        }

        /**
         * Subtracts the location by another.
         *
         * @see Vector
         * @param vec The other location
         * @return the same location
         * @throws ArgumentException for differing worlds
         */
        public Location subtract(Location vec)
        {
            if (vec == null || vec.getWorld() != getWorld())
            {
                throw new ArgumentException("Cannot add Locations of differing worlds");
            }

            x -= vec.x;
            y -= vec.y;
            z -= vec.z;
            return this;
        }

        /**
         * Subtracts the location by a vector.
         *
         * @see Vector
         * @param vec The vector to use
         * @return the same location
         */
        public Location subtract(Vector vec)
        {
            this.x -= vec.getX();
            this.y -= vec.getY();
            this.z -= vec.getZ();
            return this;
        }

        /**
         * Subtracts the location by another. Not world-aware and
         * orientation independent.
         *
         * @see Vector
         * @param x X coordinate
         * @param y Y coordinate
         * @param z Z coordinate
         * @return the same location
         */
        public Location subtract(double x, double y, double z)
        {
            this.x -= x;
            this.y -= y;
            this.z -= z;
            return this;
        }

        /**
         * Gets the magnitude of the location, defined as sqrt(x^2+y^2+z^2). The
         * value of this method is not cached and uses a costly square-root
         * function, so do not repeatedly call this method to get the location's
         * magnitude. NaN will be returned if the inner result of the sqrt()
         * function overflows, which will be caused if the length is too long. Not
         * world-aware and orientation independent.
         *
         * @see Vector
         * @return the magnitude
         */
        public double Length
        {
            get
            {
                return Math.Sqrt(x * x + y * y + z * z); //NumberConversions.square(x)?
            }
        }

        /**
         * Gets the magnitude of the location squared. Not world-aware and
         * orientation independent.
         *
         * @see Vector
         * @return the magnitude
         */
        public double LengthSquared
        {
            get
            {
                return x * x + y * y + z * z; //NumberConversions.square(x)?
            }
        }

        /**
         * Get the distance between this location and another. The value of this
         * method is not cached and uses a costly square-root function, so do not
         * repeatedly call this method to get the location's magnitude. NaN will
         * be returned if the inner result of the sqrt() function overflows, which
         * will be caused if the distance is too long.
         *
         * @see Vector
         * @param o The other location
         * @return the distance
         * @throws ArgumentException for differing worlds
         */
        public double distance(Location o)
        {
            return Math.Sqrt(distanceSquared(o));
        }

        /**
         * Get the squared distance between this location and another.
         *
         * @see Vector
         * @param o The other location
         * @return the distance
         * @throws ArgumentException for differing worlds
         */
        public double distanceSquared(Location o)
        {
            if (o == null)
            {
                throw new ArgumentException("Cannot measure distance to a null location");
            }
            else if (o.getWorld() == null || getWorld() == null)
            {
                throw new ArgumentException("Cannot measure distance to a null world");
            }
            else if (o.getWorld() != getWorld())
            {
                throw new ArgumentException("Cannot measure distance between " + getWorld().getName() + " and " + o.getWorld().getName());
            }

            return (x - o.x) * (x - o.x) + (y - o.y) * (y - o.y) + (z - o.z) * (z - o.z); //NumberConversions.square(x - o.x)?
        }

        /**
         * Performs scalar multiplication, multiplying all components with a
         * scalar. Not world-aware.
         *
         * @param m The factor
         * @see Vector
         * @return the same location
         */
        public Location multiply(double m)
        {
            x *= m;
            y *= m;
            z *= m;
            return this;
        }

        /**
         * Zero this location's components. Not world-aware.
         *
         * @see Vector
         * @return the same location
         */
        public Location zero()
        {
            x = 0;
            y = 0;
            z = 0;
            return this;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            Location other = (Location)obj;

            if (this.world != other.world && (this.world == null || !this.world.Equals(other.world)))
            {
                return false;
            }
            if (Math.Abs(this.x - other.x) > 0.0000001)
            {
                return false;
            }
            if (Math.Abs(this.y - other.y) > 0.0000001)
            {
                return false;
            }
            if (Math.Abs(this.z - other.z) > 0.0000001)
            {
                return false;
            }
            if (Math.Abs(this.pitch - other.pitch) > 0.0000001)
            {
                return false;
            }
            if (Math.Abs(this.yaw - other.yaw) > 0.0000001)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 3;
            //TODO
            hash = 19 * hash + (this.world != null ? this.world.GetHashCode() : 0);
            hash = 19 * hash + (int)(BitConverter.DoubleToInt64Bits(this.x) ^ (BitConverter.DoubleToInt64Bits(this.x) >> 32));
            hash = 19 * hash + (int)(BitConverter.DoubleToInt64Bits(this.y) ^ (BitConverter.DoubleToInt64Bits(this.y) >> 32));
            hash = 19 * hash + (int)(BitConverter.DoubleToInt64Bits(this.z) ^ (BitConverter.DoubleToInt64Bits(this.z) >> 32));
            hash = 19 * hash + this.pitch.FloatToInt32Bits();
            hash = 19 * hash + this.yaw.FloatToInt32Bits();
            return hash;
        }

        public override String ToString()
        {
            return "Location{" + "world=" + world + ",x=" + x + ",y=" + y + ",z=" + z + ",pitch=" + pitch + ",yaw=" + yaw + '}';
        }

        /**
         * Constructs a new {@link Vector} based on this Location
         *
         * @return New Vector containing the coordinates represented by this
         *     Location
         */
        public Vector toVector()
        {
            return new Vector(x, y, z);
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        /**
         * Safely converts a double (location coordinate) to an int (block
         * coordinate)
         *
         * @param loc Precise coordinate
         * @return Block coordinate
         */
        public static int locToBlock(double loc)
        {
            return NumberConversions.floor(loc);
        }

        //[Utility]
        public Dictionary<String, Object> Serialize()
        {
            Dictionary<String, Object> data = new Dictionary<string, object>();
            data.Add("world", this.world.getName());

            data.Add("x", this.x);
            data.Add("y", this.y);
            data.Add("z", this.z);

            data.Add("yaw", this.yaw);
            data.Add("pitch", this.pitch);

            return data;
        }

        /**
        * Required method for deserialization
        *
        * @param args map to deserialize
        * @return deserialized location
        * @throws ArgumentException if the world don't exists
        * @see ConfigurationSerializable
        */
        public static Location deserialize(Dictionary<String, Object> args)
        {
            World world = Bukkit.getWorld((String)args["world"]);
            if (world == null)
            {
                throw new ArgumentException("unknown world");
            }

            return new Location(world, NumberConversions.toDouble(args["x"]), NumberConversions.toDouble(args["y"]), NumberConversions.toDouble(args["z"]), NumberConversions.toFloat(args["yaw"]), NumberConversions.toFloat(args["pitch"]));
        }
    }
}
