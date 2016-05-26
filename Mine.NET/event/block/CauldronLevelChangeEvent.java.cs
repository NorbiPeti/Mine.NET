using Mine.NET.block;
using Mine.NET.entity;
using System;

namespace Mine.NET.Event.block
{
    public class CauldronLevelChangeEventArgs : BlockEventArgs, Cancellable {
        
        private bool cancelled;
        //
        private readonly Entity entity;
        private readonly ChangeReason reason;
        private readonly int oldLevel;
        private int newLevel;

        public CauldronLevelChangeEventArgs(Block block, Entity entity, ChangeReason reason, int oldLevel, int newLevel) : base(block)
        {
            this.entity = entity;
            this.reason = reason;
            this.oldLevel = oldLevel;
            this.newLevel = newLevel;
        }

        /**
         * Get entity which did this. May be null.
         *
         * @return acting entity
         */
        public Entity getEntity() {
            return entity;
        }

        public ChangeReason getReason() {
            return reason;
        }

        public int getOldLevel() {
            return oldLevel;
        }

        public int getNewLevel() {
            return newLevel;
        }

        public void setNewLevel(int newLevel) {
            if (0 <= newLevel && newLevel <= 3) throw new ArgumentOutOfRangeException(nameof(newLevel), newLevel, "Cauldron level out of bounds 0 <= %s <= 3");
            this.newLevel = newLevel;
        }

        public bool isCancelled() {
            return cancelled;
        }

        public void setCancelled(bool cancelled) {
            this.cancelled = cancelled;
        }

        public enum ChangeReason {
            /**
             * Player emptying the cauldron by filling their bucket.
             */
            BUCKET_FILL,
            /**
             * Player filling the cauldron by emptying their bucket.
             */
            BUCKET_EMPTY,
            /**
             * Player emptying the cauldron by filling their bottle.
             */
            BOTTLE_FILL,
            /**
             * Player filling the cauldron by emptying their bottle.
             */
            BOTTLE_EMPTY,
            /**
             * Player cleaning their banner.
             */
            BANNER_WASH,
            /**
             * Player cleaning their armor.
             */
            ARMOR_WASH,
            /**
             * Entity being extinguished.
             */
            EXTINGUISH,
            /**
             * Evaporating due to biome dryness.
             */
            EVAPORATE,
            /**
             * Unknown.
             */
            UNKNOWN
        }
    }
}
