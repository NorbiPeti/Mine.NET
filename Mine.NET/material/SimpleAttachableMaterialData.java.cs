using Mine.NET.block;
using System;

namespace Mine.NET.material
{

    /**
     * Simple utility class for attachable MaterialData subclasses
     */
    public abstract class SimpleAttachableMaterialData : MaterialData, Attachable
    {
        public SimpleAttachableMaterialData(Materials type, BlockFaces direction) : this(type)
        {
            setFacingDirection(direction);
        }

        public SimpleAttachableMaterialData(Materials type) : base(type)
        {
        }

        public BlockFaces getFacing()
        {
            BlockFaces attachedFace = getAttachedFace();
            return BlockFace.getOppositeFace(attachedFace);
        }

        public override string ToString()
        {
            return base.ToString() + " facing " + getFacing();
        }

        public new SimpleAttachableMaterialData Clone() { return (SimpleAttachableMaterialData)base.Clone(); }

        public abstract BlockFaces getAttachedFace();
        public abstract void setFacingDirection(BlockFaces face);
    }
}
