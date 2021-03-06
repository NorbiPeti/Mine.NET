using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an Entity targets a {@link LivingEntity} and can only target
     * LivingEntity's.
     */
    public class EntityTargetLivingEntityEventArgs : EntityTargetEventArgs
    {
        public EntityTargetLivingEntityEventArgs(Entity entity, LivingEntity target, TargetReason reason) :
            base(entity, target, reason)
        {
        }

        /**
         * Set the Entity that you want the mob to target.
         * <p>
         * It is possible to be null, null will cause the entity to be
         * target-less.
         * <p>
         * Must be a LivingEntity, or null.
         *
         * @param target The entity to target
         */
        public override void setTarget(Entity target)
        {
            if (target == null || target is LivingEntity)
            {
                base.setTarget(target);
            }
        }
    }
}
