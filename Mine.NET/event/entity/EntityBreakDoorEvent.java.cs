using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an {@link Entity} breaks a door
     * <p>
     * Cancelling the event will cause the event to be delayed
     */
    public class EntityBreakDoorEventArgs<T> : EntityChangeBlockEventArgs<T> where T : Entity
    {
        public EntityBreakDoorEventArgs(LivingEntity entity, Block targetBlock) :
            base(entity, targetBlock, Materials.AIR, (byte)0)
        {
        }
    }
}
