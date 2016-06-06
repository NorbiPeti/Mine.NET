using Mine.NET.block;

namespace Mine.NET.material
{
    public class Banner : MaterialData, Attachable
    {

        private BlockFaces face;
        public Banner() :
            base(Materials.BANNER)
        {
        }

        public Banner(Materials type) :
            base(type)
        {
        }

        public Banner(BlockFaces face) :
            base(Materials.BANNER)
        {
            this.face = face;
        }

        public Banner(BlockFaces face, Materials type) :
            base(type)
        {
            this.face = face;
        }

        public bool isWallBanner()
        {
            return getItemType() == Materials.WALL_BANNER;
        }

        public BlockFaces getAttachedFace()
        {
            if (isWallBanner())
            {
                switch (face)
                {
                    case BlockFaces.SOUTH:
                    case BlockFaces.NORTH:
                    case BlockFaces.EAST:
                    case BlockFaces.WEST:
                        return face;
                    default:
                        return BlockFaces.SELF;
                }
            }
            else
            {
                return BlockFaces.DOWN;
            }
        }

        public BlockFaces getFacing()
        {
            if (!isWallBanner())
            {
                return face;
            }
            else
            {
                return BlockFace.getOppositeFace(getAttachedFace());
            }
        }

        public void setFacingDirection(BlockFaces face)
        {
            this.face = face;
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing();
        }

        public new Banner Clone()
        {
            return (Banner)base.Clone();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Banner))
                return false;
            var data = (Banner)obj;
            return base.Equals(obj) && this.face == data.face;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + (int)face;
        }
    }
}
