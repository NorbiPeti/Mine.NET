namespace Mine.NET.material;

import java.util.Arrays;
import java.util.EnumSet;

import org.bukkit.Materials;
import org.bukkit.block.BlockFaces;

/**
 * Represents a vine
 */
public class Vine : MaterialData {
    private static readonly int VINE_NORTH = 0x4;
    private static readonly int VINE_EAST = 0x8;
    private static readonly int VINE_WEST = 0x2;
    private static readonly int VINE_SOUTH = 0x1;
    EnumSet<BlockFaces> possibleFaces = EnumSet.of(BlockFaces.WEST, BlockFaces.NORTH, BlockFaces.SOUTH, BlockFaces.EAST);

    public Vine() {
        base(Materials.VINE);
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
        base(Materials.VINE, data);
    }

    public Vine(BlockFaces... faces) {
        this(EnumSet.copyOf(Arrays.asList(faces)));
    }

    public Vine(EnumSet<BlockFaces> faces) {
        this((byte) 0);
        faces.retainAll(possibleFaces);

        byte data = 0;

        if (faces.contains(BlockFaces.WEST)) {
            data |= VINE_WEST;
        }

        if (faces.contains(BlockFaces.NORTH)) {
            data |= VINE_NORTH;
        }

        if (faces.contains(BlockFaces.SOUTH)) {
            data |= VINE_SOUTH;
        }

        if (faces.contains(BlockFaces.EAST)) {
            data |= VINE_EAST;
        }

        setData(data);
    }

    /**
     * Check if the vine is attached to the specified face of an adjacent
     * block. You can check two faces at once by passing e.g. {@link
     * BlockFaces#NORTH_EAST}.
     *
     * @param face The face to check.
     * @return Whether it is attached to that face.
     */
    public bool isOnFace(BlockFaces face) {
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
                return isOnFace(BlockFaces.EAST) && isOnFace(BlockFaces.NORTH);
            case NORTH_WEST:
                return isOnFace(BlockFaces.WEST) && isOnFace(BlockFaces.NORTH);
            case SOUTH_EAST:
                return isOnFace(BlockFaces.EAST) && isOnFace(BlockFaces.SOUTH);
            case SOUTH_WEST:
                return isOnFace(BlockFaces.WEST) && isOnFace(BlockFaces.SOUTH);
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
    public void putOnFace(BlockFaces face) {
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
                putOnFace(BlockFaces.WEST);
                putOnFace(BlockFaces.NORTH);
                break;
            case SOUTH_WEST:
                putOnFace(BlockFaces.WEST);
                putOnFace(BlockFaces.SOUTH);
                break;
            case NORTH_EAST:
                putOnFace(BlockFaces.EAST);
                putOnFace(BlockFaces.NORTH);
                break;
            case SOUTH_EAST:
                putOnFace(BlockFaces.EAST);
                putOnFace(BlockFaces.SOUTH);
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
    public void removeFromFace(BlockFaces face) {
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
                removeFromFace(BlockFaces.WEST);
                removeFromFace(BlockFaces.NORTH);
                break;
            case SOUTH_WEST:
                removeFromFace(BlockFaces.WEST);
                removeFromFace(BlockFaces.SOUTH);
                break;
            case NORTH_EAST:
                removeFromFace(BlockFaces.EAST);
                removeFromFace(BlockFaces.NORTH);
                break;
            case SOUTH_EAST:
                removeFromFace(BlockFaces.EAST);
                removeFromFace(BlockFaces.SOUTH);
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
