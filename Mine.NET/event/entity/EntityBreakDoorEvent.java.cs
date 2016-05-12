using Mine.NET.block;
using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an {@link Entity} breaks a door
     * <p>
     * Cancelling the event will cause the event to be delayed
     */
    public class EntityBreakDoorEvent<T> : EntityChangeBlockEvent<T> where T : Entity
    {
        public EntityBreakDoorEvent(LivingEntity entity, Block targetBlock) :
            base(entity, targetBlock, Materials.AIR, (byte)0)
        {
        }

        public override LivingEntity getEntity()
        {
            return (LivingEntity)entity;
        }
    }
}
