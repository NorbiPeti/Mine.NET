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

        /**
         * @param type the type
         * @param data the raw data value
         * [Obsolete] Magic value
         */
        [Obsolete]
        public SimpleAttachableMaterialData(Materials type, byte data) : base(type, data)
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

        public override SimpleAttachableMaterialData clone()
        {
            return (SimpleAttachableMaterialData)base.clone();
        }

        public abstract BlockFaces getAttachedFace();
        public abstract void setFacingDirection(BlockFaces face);
    }
}
