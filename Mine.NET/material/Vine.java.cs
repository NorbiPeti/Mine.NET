package org.bukkit.material;

import java.util.Arrays;
import java.util.EnumSet;

import org.bukkit.Material;
import org.bukkit.block.BlockFace;

/**
 * Represents a vine
 */
public class Vine : MaterialData {
    private static readonly int VINE_NORTH = 0x4;
    private static readonly int VINE_EAST = 0x8;
    private static readonly int VINE_WEST = 0x2;
    private static readonly int VINE_SOUTH = 0x1;
    EnumSet<BlockFace> possibleFaces = EnumSet.of(BlockFace.WEST, BlockFace.NORTH, BlockFace.SOUTH, BlockFace.EAST);

    public Vine() {
        base(Material.VINE);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Vine(int type, byte data){
        base(type, data);
    }

    /**
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Vine(byte data) {
        base(Material.VINE, data);
    }

    public Vine(BlockFace... faces) {
        this(EnumSet.copyOf(Arrays.asList(faces)));
    }

    public Vine(EnumSet<BlockFace> faces) {
        this((byte) 0);
        faces.retainAll(possibleFaces);

        byte data = 0;

        if (faces.contains(BlockFace.WEST)) {
            data |= VINE_WEST;
        }

        if (faces.contains(BlockFace.NORTH)) {
            data |= VINE_NORTH;
        }

        if (faces.contains(BlockFace.SOUTH)) {
            data |= VINE_SOUTH;
        }

        if (faces.contains(BlockFace.EAST)) {
            data |= VINE_EAST;
        }

        setData(data);
    }

    /**
     * Check if the vine is attached to the specified face of an adjacent
     * block. You can check two faces at once by passing e.g. {@link
     * BlockFace#NORTH_EAST}.
     *
     * @param face The face to check.
     * @return Whether it is attached to that face.
     */
    public bool isOnFace(BlockFace face) {
        switch (face) {
            case WEST:
                return (getData() & VINE_WEST) == VINE_WEST;
            case NORTH:
                return (getData() & VINE_NORTH) == VINE_NORTH;
            case SOUTH:
                return (getData() & VINE_SOUTH) == VINE_SOUTH;
            case EAST:
                return (getData() & VINE_EAST) == VINE_EAST;
            case NORTH_EAST:
                return isOnFace(BlockFace.EAST) && isOnFace(BlockFace.NORTH);
            case NORTH_WEST:
                return isOnFace(BlockFace.WEST) && isOnFace(BlockFace.NORTH);
            case SOUTH_EAST:
                return isOnFace(BlockFace.EAST) && isOnFace(BlockFace.SOUTH);
            case SOUTH_WEST:
                return isOnFace(BlockFace.WEST) && isOnFace(BlockFace.SOUTH);
            case UP: // It's impossible to be accurate with this since it's contextual
                return true;
            default:
                return false;
        }
    }

    /**
     * Attach the vine to the specified face of an adjacent block.
     *
     * @param face The face to attach.
     */
    public void putOnFace(BlockFace face) {
        switch(face) {
            case WEST:
                setData((byte) (getData() | VINE_WEST));
                break;
            case NORTH:
                setData((byte) (getData() | VINE_NORTH));
                break;
            case SOUTH:
                setData((byte) (getData() | VINE_SOUTH));
                break;
            case EAST:
                setData((byte) (getData() | VINE_EAST));
                break;
            case NORTH_WEST:
                putOnFace(BlockFace.WEST);
                putOnFace(BlockFace.NORTH);
                break;
            case SOUTH_WEST:
                putOnFace(BlockFace.WEST);
                putOnFace(BlockFace.SOUTH);
                break;
            case NORTH_EAST:
                putOnFace(BlockFace.EAST);
                putOnFace(BlockFace.NORTH);
                break;
            case SOUTH_EAST:
                putOnFace(BlockFace.EAST);
                putOnFace(BlockFace.SOUTH);
                break;
            case UP:
                break;
            default:
                throw new ArgumentException("Vines can't go on face " + face.ToString());
        }
    }

    /**
     * Detach the vine from the specified face of an adjacent block.
     *
     * @param face The face to detach.
     */
    public void removeFromFace(BlockFace face) {
        switch(face) {
            case WEST:
                setData((byte) (getData() & ~VINE_WEST));
                break;
            case NORTH:
                setData((byte) (getData() & ~VINE_NORTH));
                break;
            case SOUTH:
                setData((byte) (getData() & ~VINE_SOUTH));
                break;
            case EAST:
                setData((byte) (getData() & ~VINE_EAST));
                break;
            case NORTH_WEST:
                removeFromFace(BlockFace.WEST);
                removeFromFace(BlockFace.NORTH);
                break;
            case SOUTH_WEST:
                removeFromFace(BlockFace.WEST);
                removeFromFace(BlockFace.SOUTH);
                break;
            case NORTH_EAST:
                removeFromFace(BlockFace.EAST);
                removeFromFace(BlockFace.NORTH);
                break;
            case SOUTH_EAST:
                removeFromFace(BlockFace.EAST);
                removeFromFace(BlockFace.SOUTH);
                break;
            case UP:
                break;
            default:
                throw new ArgumentException("Vines can't go on face " + face.ToString());
        }
    }

    public override string ToString() {
        return "VINE";
    }

    public override Vine clone() {
        return (Vine) base.clone();
    }
}
