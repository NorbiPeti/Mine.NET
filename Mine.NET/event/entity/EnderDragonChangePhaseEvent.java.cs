using Mine.NET.entity;
using System;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an EnderDragon switches controller phase.
     */
    public class EnderDragonChangePhaseEvent : EntityEvent<EnderDragon>, Cancellable {

        private static readonly HandlerList handlers = new HandlerList();
        private bool cancel;
        private readonly EnderDragonPhase currentPhase;
        private EnderDragonPhase newPhase;

        public EnderDragonChangePhaseEvent(EnderDragon enderDragon, EnderDragon.Phase currentPhase, EnderDragon.Phase newPhase) : base(enderDragon)
        {    
            this.currentPhase = currentPhase;
            this.setNewPhase(newPhase);
        }

        /**
         * Gets the current phase that the dragon is in. This method will return null 
         * when a dragon is first spawned and hasn't yet been assigned a phase.
         * 
         * @return the current dragon phase
         */
        public EnderDragonPhase getCurrentPhase() {
            return currentPhase;
        }

        /**
         * Gets the new phase that the dragon will switch to.
         * 
         * @return the new dragon phase
         */
        public EnderDragonPhase getNewPhase() {
            return newPhase;
        }

        /**
         * Sets the new phase for the ender dragon.
         * 
         * @param newPhase the new dragon phase
         */
        public void setNewPhase(EnderDragonPhase newPhase) {
            if (newPhase == null) throw new ArgumentNullException("New dragon phase cannot be null");
            this.newPhase = newPhase;
        }

        public bool isCancelled() {
            return cancel;
        }

        public void setCancelled(bool cancel) {
            this.cancel = cancel;
        }

        public override HandlerList getHandlers() {
            return handlers;
        }

        public static HandlerList getHandlerList() {
            return handlers;
        }
    }
}
