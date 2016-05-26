using Mine.NET.entity;
using Mine.NET.inventory;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Thrown whenever a LivingEntity dies
     */
    public class EntityDeathEventArgs<T> : EntityEventArgs<T> where T : LivingEntity
    {
        private readonly List<ItemStack> drops;
        private int dropExp = 0;

        public EntityDeathEventArgs(LivingEntity entity, List<ItemStack> drops) :
            this(entity, drops, 0)
        {
        }

        public EntityDeathEventArgs(LivingEntity what, List<ItemStack> drops, int droppedExp) :
            base(what)
        {
            this.drops = drops;
            this.dropExp = droppedExp;
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
    }

    public class EntityDeathEventArgs : EntityDeathEventArgs<LivingEntity>
    {
        public EntityDeathEventArgs(LivingEntity entity, List<ItemStack> drops) :
            base(entity, drops, 0)
        {
        }

        public EntityDeathEventArgs(LivingEntity what, List<ItemStack> drops, int droppedExp) :
            base(what, drops, droppedExp)
        {
        }
    }
}
