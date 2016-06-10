using Mine.NET.block;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mine.NET.material
{
    /**
     * Represents a vine
     */
    public class Vine : MaterialData
    {
        private static readonly int VINE_NORTH = 0x4;
        private static readonly int VINE_EAST = 0x8;
        private static readonly int VINE_WEST = 0x2;
        private static readonly int VINE_SOUTH = 0x1;
        BlockFaces[] possibleFaces = new BlockFaces[] { BlockFaces.WEST, BlockFaces.NORTH, BlockFaces.SOUTH, BlockFaces.EAST };

        public Vine() : base(Materials.VINE)
        {
        }

        private List<BlockFaces> faces;
        public Vine(params BlockFaces[] faces) : this()
        {
            if (!faces.All(f => possibleFaces.Contains(f)))
                throw new ArgumentException(); //TODO?
            /*byte data = 0;

            if (faces.Contains(BlockFaces.WEST))
            {
                data |= (byte)VINE_WEST;
            }

            if (faces.Contains(BlockFaces.NORTH))
            {
                data |= (byte)VINE_NORTH;
            }

            if (faces.Contains(BlockFaces.SOUTH))
            {
                data |= (byte)VINE_SOUTH;
            }

            if (faces.Contains(BlockFaces.EAST))
            {
                data |= (byte)VINE_EAST;
            }

            setData(data);*/
            this.faces = new List<BlockFaces>(faces);
        }

        /**
         * Check if the vine is attached to the specified face of an adjacent
         * block. You can check two faces at once by passing e.g. {@link
         * BlockFaces#NORTH_EAST}.
         *
         * @param face The face to check.
         * @return Whether it is attached to that face.
         */
        public bool isOnFace(BlockFaces face)
        {
            /*switch (face)
            {
                case BlockFaces.WEST:
                    return (getData() & VINE_WEST) == VINE_WEST;
                case BlockFaces.NORTH:
                    return (getData() & VINE_NORTH) == VINE_NORTH;
                case BlockFaces.SOUTH:
                    return (getData() & VINE_SOUTH) == VINE_SOUTH;
                case BlockFaces.EAST:
                    return (getData() & VINE_EAST) == VINE_EAST;
                case BlockFaces.NORTH_EAST:
                    return isOnFace(BlockFaces.EAST) && isOnFace(BlockFaces.NORTH);
                case BlockFaces.NORTH_WEST:
                    return isOnFace(BlockFaces.WEST) && isOnFace(BlockFaces.NORTH);
                case BlockFaces.SOUTH_EAST:
                    return isOnFace(BlockFaces.EAST) && isOnFace(BlockFaces.SOUTH);
                case BlockFaces.SOUTH_WEST:
                    return isOnFace(BlockFaces.WEST) && isOnFace(BlockFaces.SOUTH);
                case BlockFaces.UP: // It's impossible to be accurate with this since it's contextual
                    return true;
                default:
                    return false;
            }*/
            return faces.Contains(face);
        }

        /**
         * Attach the vine to the specified face of an adjacent block.
         *
         * @param face The face to attach.
         */
        public void putOnFace(BlockFaces face)
        {
            /*switch (face)
            {
                case BlockFaces.WEST:
                    setData((byte)(getData() | VINE_WEST));
                    break;
                case BlockFaces.NORTH:
                    setData((byte)(getData() | VINE_NORTH));
                    break;
                case BlockFaces.SOUTH:
                    setData((byte)(getData() | VINE_SOUTH));
                    break;
                case BlockFaces.EAST:
                    setData((byte)(getData() | VINE_EAST));
                    break;
                case BlockFaces.NORTH_WEST:
                    putOnFace(BlockFaces.WEST);
                    putOnFace(BlockFaces.NORTH);
                    break;
                case BlockFaces.SOUTH_WEST:
                    putOnFace(BlockFaces.WEST);
                    putOnFace(BlockFaces.SOUTH);
                    break;
                case BlockFaces.NORTH_EAST:
                    putOnFace(BlockFaces.EAST);
                    putOnFace(BlockFaces.NORTH);
                    break;
                case BlockFaces.SOUTH_EAST:
                    putOnFace(BlockFaces.EAST);
                    putOnFace(BlockFaces.SOUTH);
                    break;
                case BlockFaces.UP:
                    break;
                default:
                    throw new ArgumentException("Vines can't go on face " + face.ToString());
            }*/
            if (!faces.Contains(face))
                faces.Add(face);
        }

        /**
         * Detach the vine from the specified face of an adjacent block.
         *
         * @param face The face to detach.
         */
        public void removeFromFace(BlockFaces face)
        {
            /*switch (face)
            {
                case BlockFaces.WEST:
                    setData((byte)(getData() & ~VINE_WEST));
                    break;
                case BlockFaces.NORTH:
                    setData((byte)(getData() & ~VINE_NORTH));
                    break;
                case BlockFaces.SOUTH:
                    setData((byte)(getData() & ~VINE_SOUTH));
                    break;
                case BlockFaces.EAST:
                    setData((byte)(getData() & ~VINE_EAST));
                    break;
                case BlockFaces.NORTH_WEST:
                    removeFromFace(BlockFaces.WEST);
                    removeFromFace(BlockFaces.NORTH);
                    break;
                case BlockFaces.SOUTH_WEST:
                    removeFromFace(BlockFaces.WEST);
                    removeFromFace(BlockFaces.SOUTH);
                    break;
                case BlockFaces.NORTH_EAST:
                    removeFromFace(BlockFaces.EAST);
                    removeFromFace(BlockFaces.NORTH);
                    break;
                case BlockFaces.SOUTH_EAST:
                    removeFromFace(BlockFaces.EAST);
                    removeFromFace(BlockFaces.SOUTH);
                    break;
                case BlockFaces.UP:
                    break;
                default:
                    throw new ArgumentException("Vines can't go on face " + face.ToString());
            }*/
            faces.Remove(face);
        }

        public override string ToString()
        {
            return "VINE";
        }

        public new Vine Clone() { return (Vine)base.Clone(); }
    }
}
