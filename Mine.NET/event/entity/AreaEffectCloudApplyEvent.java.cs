using Mine.NET.entity;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a lingering potion applies it's effects. Happens
     * once every 5 ticks
     */
    public class AreaEffectCloudApplyEvent : EntityEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly List<LivingEntity> affectedEntities;

        public AreaEffectCloudApplyEvent(AreaEffectCloud entity, List<LivingEntity> affectedEntities) : base(entity)
        {
            this.affectedEntities = affectedEntities;
        }

        public override AreaEffectCloud getEntity()
        {
            return (AreaEffectCloud)entity;
        }

        /**
         * Retrieves a mutable list of the effected entities
         * <p>
         * It is important to note that not every entity in this list
         * is guaranteed to be effected.  The cloud may die during the
         * application of its effects due to the depletion of {@link AreaEffectCloud#getDurationOnUse()}
         * or {@link AreaEffectCloud#getRadiusOnUse()}
         *
         * @return the affected entity list
         */
        public List<LivingEntity> getAffectedEntities()
        {
            return affectedEntities;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
