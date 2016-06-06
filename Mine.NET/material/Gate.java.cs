using Mine.NET.block;

namespace Mine.NET.material {

    /**
     * Represents a fence gate
     */ //Find: "(namespace \S+)(?:;)(?:[^¤]+import .+)" - Replace: "$1{"
    public class Gate : MaterialData<byte>, Directional, Openable {
        private static readonly byte OPEN_BIT = 0x4;
        private static readonly byte DIR_BIT = 0x3;
        private static readonly byte GATE_SOUTH = 0x0;
        private static readonly byte GATE_WEST = 0x1;
        private static readonly byte GATE_NORTH = 0x2;
        private static readonly byte GATE_EAST = 0x3;

        public Gate() : base(Materials.FENCE_GATE) {
        }

        public Gate(byte data) : base(Materials.FENCE_GATE, data) {
        }

        public void setFacingDirection(BlockFaces face) {
            byte data = (byte)(getData() & ~DIR_BIT);

            switch (face) {
                default:
                case BlockFaces.EAST:
                    data |= GATE_SOUTH;
                    break;
                case BlockFaces.SOUTH:
                    data |= GATE_WEST;
                    break;
                case BlockFaces.WEST:
                    data |= GATE_NORTH;
                    break;
                case BlockFaces.NORTH:
                    data |= GATE_EAST;
                    break;
            }

            setData(data);
        }

        public BlockFaces getFacing() {
            switch (getData() & DIR_BIT) {
                case BlockFaces.GATE_SOUTH:
                    return BlockFaces.EAST;
                case BlockFaces.GATE_WEST:
                    return BlockFaces.SOUTH;
                case BlockFaces.GATE_NORTH:
                    return BlockFaces.WEST;
                case BlockFaces.GATE_EAST:
                    return BlockFaces.NORTH;
            }

            return BlockFaces.EAST;
        }

        public bool isOpen() {
            return (getData() & OPEN_BIT) > 0;
        }

        public void setOpen(bool isOpen) {
            byte data = getData();

            if (isOpen) {
                data |= OPEN_BIT;
            } else {
                data &= (byte)~OPEN_BIT;
            }

            setData(data);
        }

        public override string ToString() {
            return (isOpen() ? "OPEN " : "CLOSED ") + " facing and opening " + getFacing();
        }

        public new Gate Clone() { return (Gate)base.Clone(); }
    }
}
