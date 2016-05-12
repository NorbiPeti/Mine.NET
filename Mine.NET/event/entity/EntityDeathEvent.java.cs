using Mine.NET.entity;
using Mine.NET.inventory;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Thrown whenever a LivingEntity dies
     */
    public class EntityDeathEvent : EntityEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly List<ItemStack> drops;
        private int dropExp = 0;

        public EntityDeathEvent(LivingEntity entity, List<ItemStack> drops) :
            this(entity, drops, 0)
        {
        }

        public EntityDeathEvent(LivingEntity what, List<ItemStack> drops, int droppedExp) :
            base(what)
        {
            this.drops = drops;
            this.dropExp = droppedExp;
        }

        public override LivingEntity getEntity()
        {
            return (LivingEntity)entity;
        }

        /**
         * Gets how much EXP should be dropped from this death.
         * <p>
         * This does not indicate how much EXP should be taken from the entity in
         * question, merely how much should be created after its death.
         *
         * @return Amount of EXP to drop.
         */
        public int getDroppedExp()
        {
            return dropExp;
        }

        /**
         * Sets how much EXP should be dropped from this death.
         * <p>
         * This does not indicate how much EXP should be taken from the entity in
         * question, merely how much should be created after its death.
         *
         * @param exp Amount of EXP to drop.
         */
        public void setDroppedExp(int exp)
        {
            this.dropExp = exp;
        }

        /**
         * Gets all the items which will drop when the entity dies
         *
         * @return Items to drop when the entity dies
         */
        public List<ItemStack> getDrops()
        {
            return drops;
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
