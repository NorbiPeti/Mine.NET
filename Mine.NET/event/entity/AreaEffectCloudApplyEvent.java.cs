using Mine.NET.entity;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a lingering potion applies it's effects. Happens
     * once every 5 ticks
     */
    public class AreaEffectCloudApplyEventArgs : EntityEventArgs<AreaEffectCloud>
    {
        private readonly List<LivingEntity> affectedEntities;

        public AreaEffectCloudApplyEventArgs(AreaEffectCloud entity, List<LivingEntity> affectedEntities) : base(entity)
        {
            this.affectedEntities = affectedEntities;
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
    }
}
