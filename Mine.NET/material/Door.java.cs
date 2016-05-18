namespace Mine.NET.material{

import org.bukkit.Materials;
import org.bukkit.TreeSpecies;
import org.bukkit.block.BlockFaces;

/**
 * Represents a door.
 *
 * This class was previously deprecated, but has been retrofitted to
 * work with modern doors. Some methods are undefined dependant on <code>isTopHalf()</code>
 * due to Minecraft's internal representation of doors.
 *
 * @see Materials#WOODEN_DOOR
 * @see Materials#IRON_DOOR_BLOCK
 * @see Materials#SPRUCE_DOOR
 * @see Materials#BIRCH_DOOR
 * @see Materials#JUNGLE_DOOR
 * @see Materials#ACACIA_DOOR
 * @see Materials#DARK_OAK_DOOR
 */
public class Door : MaterialData : Directional, Openable {

    // This class breaks API contracts on Directional and Openable because
    // of the way doors are currently implemented. Beware!

    /**
     * [Obsolete] Artifact of old API, equivalent to new <code>Door(Materials.WOODEN_DOOR);</code>
     */
    [Obsolete]
    public Door() : base(Materials.WOODEN_DOOR) {
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Door(int type) : base(type) {
    }

    public Door(Materials type) : base(type) {
    }

    /**
     * Constructs the bottom half of a door of the given Materials type, facing the specified direction and set to closed
     *
     * @param type The type of Materials this door is made of. This must match the type of the block above.
     * @param face The direction the door is facing.
     *
     * @see Materials#WOODEN_DOOR
     * @see Materials#IRON_DOOR_BLOCK
     * @see Materials#SPRUCE_DOOR
     * @see Materials#BIRCH_DOOR
     * @see Materials#JUNGLE_DOOR
     * @see Materials#ACACIA_DOOR
     * @see Materials#DARK_OAK_DOOR
     *
     * @see BlockFaces#WEST
     * @see BlockFaces#NORTH
     * @see BlockFaces#EAST
     * @see BlockFaces#SOUTH
     */
    public Door(Materials type, BlockFaces face) : this(type, face, false) {
    }

    /**
     * Constructs the bottom half of a door of the given Materials type, facing the specified direction and set to open
     * or closed
     *
     * @param type The type of Materials this door is made of. This must match the type of the block above.
     * @param face The direction the door is facing.
     * @param isOpen Whether the door is currently opened.
     *
     * @see Materials#WOODEN_DOOR
     * @see Materials#IRON_DOOR_BLOCK
     * @see Materials#SPRUCE_DOOR
     * @see Materials#BIRCH_DOOR
     * @see Materials#JUNGLE_DOOR
     * @see Materials#ACACIA_DOOR
     * @see Materials#DARK_OAK_DOOR
     *
     * @see BlockFaces#WEST
     * @see BlockFaces#NORTH
     * @see BlockFaces#EAST
     * @see BlockFaces#SOUTH
     */
    public Door(Materials type, BlockFaces face, bool isOpen) : base(type) {
        setTopHalf(false);
        setFacingDirection(face);
        setOpen(isOpen);
    }

    /**
     * Constructs the top half of door of the given Materials type and with the hinge on the left or right
     *
     * @param type The type of Materials this door is made of. This must match the type of the block below.
     * @param isHingeRight True if the hinge is on the right hand side, false if the hinge is on the left hand side.
     *
     * @see Materials#WOODEN_DOOR
     * @see Materials#IRON_DOOR_BLOCK
     * @see Materials#SPRUCE_DOOR
     * @see Materials#BIRCH_DOOR
     * @see Materials#JUNGLE_DOOR
     * @see Materials#ACACIA_DOOR
     * @see Materials#DARK_OAK_DOOR
     */
    public Door(Materials type, bool isHingeRight) : base(type) {
        setTopHalf(true);
        setHinge(isHingeRight);
    }

    /**
     * Constructs the bottom half of a wooden door of the given species, facing the specified direction and set to
     * closed
     *
     * @param species The species this wooden door is made of. This must match the species of the block above.
     * @param face The direction the door is facing.
     *
     * @see TreeSpecies
     *
     * @see BlockFaces#WEST
     * @see BlockFaces#NORTH
     * @see BlockFaces#EAST
     * @see BlockFaces#SOUTH
     */
    public Door(TreeSpecies species, BlockFaces face) {
        this(getWoodDoorOfSpecies(species), face, false);
    }

    /**
     * Constructs the bottom half of a wooden door of the given species, facing the specified direction and set to open
     * or closed
     *
     * @param species The species this wooden door is made of. This must match the species of the block above.
     * @param face The direction the door is facing.
     * @param isOpen Whether the door is currently opened.
     *
     * @see TreeSpecies
     *
     * @see BlockFaces#WEST
     * @see BlockFaces#NORTH
     * @see BlockFaces#EAST
     * @see BlockFaces#SOUTH
     */
    public Door(TreeSpecies species, BlockFaces face, bool isOpen) {
        this(getWoodDoorOfSpecies(species), face, isOpen);
    }

    /**
     * Constructs the top half of a wooden door of the given species and with the hinge on the left or right
     *
     * @param species The species this wooden door is made of. This must match the species of the block below.
     * @param isHingeRight True if the hinge is on the right hand side, false if the hinge is on the left hand side.
     *
     * @see TreeSpecies
     */
    public Door(TreeSpecies species, bool isHingeRight) {
        this(getWoodDoorOfSpecies(species), isHingeRight);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Door(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public Door(Materials type, readonly byte data) : base(type, data) {
    }

    /**
     * Returns the item type of a wooden door for the given tree species.
     *
     * @param species The species of wood door required.
     * @return The item type for the given species.
     *
     * @see Materials#WOODEN_DOOR
     * @see Materials#SPRUCE_DOOR
     * @see Materials#BIRCH_DOOR
     * @see Materials#JUNGLE_DOOR
     * @see Materials#ACACIA_DOOR
     * @see Materials#DARK_OAK_DOOR
     */
    public static Materials getWoodDoorOfSpecies(TreeSpecies species) {
        switch (species) {
            default:
            case GENERIC:
                return Materials.WOODEN_DOOR;
            case BIRCH:
                return Materials.BIRCH_DOOR;
            case REDWOOD:
                return Materials.SPRUCE_DOOR;
            case JUNGLE:
                return Materials.JUNGLE_DOOR;
            case ACACIA:
                return Materials.ACACIA_DOOR;
            case DARK_OAK:
                return Materials.DARK_OAK_DOOR;
        }
    }

    /**
     * Result is undefined if <code>isTopHalf()</code> is true.
     */
    public bool isOpen() {
        return ((getData() & 0x4) == 0x4);
    }

    /**
     * Set whether the door is open. Undefined if <code>isTopHalf()</code> is true.
     */
    public void setOpen(bool isOpen) {
        setData((byte) (isOpen ? (getData() | 0x4) : (getData() & ~0x4)));
    }

    /**
     * @return whether this is the top half of the door
     */
    public bool isTopHalf() {
        return ((getData() & 0x8) == 0x8);
    }

    /**
     * Configure this part of the door to be either the top or the bottom half
     *
     * @param isTopHalf True to make it the top half.
     */
    public void setTopHalf(bool isTopHalf) {
        setData((byte) (isTopHalf ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    /**
     * @return BlockFaces.SELF
     * [Obsolete] This method should not be used; use hinge and facing accessors instead.
     */
    [Obsolete]
    public BlockFaces getHingeCorner() {
        return BlockFaces.SELF;
    }

    public override string ToString() {
        return (isTopHalf() ? "TOP" : "BOTTOM") + " half of " + base.ToString();
    }

    /**
     * Set the direction that this door should is facing.
     *
     * Undefined if <code>isTopHalf()</code> is true.
     *
     * @param face the direction
     */
    public void setFacingDirection(BlockFaces face) {
        byte data = (byte) (getData() & 0xC);
        switch (face) {
            case WEST:
                data |= 0x0;
                break;
            case NORTH:
                data |= 0x1;
                break;
            case EAST:
                data |= 0x2;
                break;
            case SOUTH:
                data |= 0x3;
                break;
        }
        setData(data);
    }

    /**
     * Get the direction that this door is facing.
     *
     * Undefined if <code>isTopHalf()</code> is true.
     *
     * @return the direction
     */
    public BlockFaces getFacing() {
        byte data = (byte) (getData() & 0x3);
        switch (data) {
            case 0:
                return BlockFaces.WEST;
            case 1:
                return BlockFaces.NORTH;
            case 2:
                return BlockFaces.EAST;
            case 3:
                return BlockFaces.SOUTH;
            default:
                throw new IllegalStateException("Unknown door facing (data: " + data + ")");
        }
    }

    /**
     * Returns the side of the door the hinge is on.
     *
     * Undefined if <code>isTopHalf()</code> is false.
     *
     * @return false for left hinge, true for right hinge
     */
    public bool getHinge() {
        return (getData() & 0x1) == 1;
    }

    /**
     * Set whether the hinge is on the left or right side. Left is false, right is true.
     *
     * Undefined if <code>isTopHalf()</code> is false.
     *
     * @param isHingeRight True if the hinge is on the right hand side, false if the hinge is on the left hand side.
     */
    public void setHinge(bool isHingeRight) {
        setData((byte) (isHingeRight ? (getData() | 0x1) : (getData() & ~0x1)));
    }

    public override Door clone() {
        return (Door) base.clone();
    }
}}
