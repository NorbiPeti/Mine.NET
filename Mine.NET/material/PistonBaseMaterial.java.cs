namespace Mine.NET.material;

import org.bukkit.Material;
import org.bukkit.block.BlockFaces;

/**
 * Material data for the piston base block
 */
public class PistonBaseMaterial : MaterialData : Directional, Redstone {
    
    /**
     * Constructs a PistonBaseMaterial
     * 
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PistonBaseMaterial(int type) {
        base(type);
    }

    public PistonBaseMaterial(Material type) {
        base(type);
    }

    /**
     * Constructs a PistonBaseMaterial.
     * 
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PistonBaseMaterial(int type, readonly byte data) {
        base(type, data);
    }

    /**
     * Constructs a PistonBaseMaterial.
     * 
     * @param type the material type to use
     * @param data the raw data value 
     * [Obsolete] Magic value
     */
    [Obsolete]
    public PistonBaseMaterial(Material type, readonly byte data) {
        base(type, data);
    }

    public override void setFacingDirection(BlockFaces face) {
        byte data = (byte) (getData() & 0x8);

        switch (face) {
        case UP:
            data |= 1;
            break;
        case NORTH:
            data |= 2;
            break;
        case SOUTH:
            data |= 3;
            break;
        case WEST:
            data |= 4;
            break;
        case EAST:
            data |= 5;
            break;
        }
        setData(data);
    }

    public override BlockFaces getFacing() {
        byte dir = (byte) (getData() & 7);

        switch (dir) {
        case 0:
            return BlockFaces.DOWN;
        case 1:
            return BlockFaces.UP;
        case 2:
            return BlockFaces.NORTH;
        case 3:
            return BlockFaces.SOUTH;
        case 4:
            return BlockFaces.WEST;
        case 5:
            return BlockFaces.EAST;
        default:
            return BlockFaces.SELF;
        }
    }

    public override bool isPowered() {
        return (getData() & 0x8) == 0x8;
    }

    /**
     * Sets the current state of this piston
     *
     * @param powered true if the piston is extended {@literal &} powered, or false
     */
    public void setPowered(bool powered) {
        setData((byte) (powered ? (getData() | 0x8) : (getData() & ~0x8)));
    }

    /**
     * Checks if this piston base is sticky, and returns true if so
     *
     * @return true if this piston is "sticky", or false
     */
    public bool isSticky() {
        return this.getItemType() == Material.PISTON_STICKY_BASE;
    }

    public override PistonBaseMaterial clone() {
        return (PistonBaseMaterial) base.clone();
    }
}
