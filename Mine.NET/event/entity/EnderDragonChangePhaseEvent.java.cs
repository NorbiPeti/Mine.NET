using Mine.NET.entity;
using System;

namespace Mine.NET.Event.entity
{
    /**
     * Called when an EnderDragon switches controller phase.
     */
    public class EnderDragonChangePhaseEventArgs : EntityEventArgs<EnderDragon>, Cancellable
    {

        private bool cancel;
        private readonly EnderDragonPhase currentPhase;
        private EnderDragonPhase newPhase;

        public EnderDragonChangePhaseEventArgs(EnderDragon enderDragon, EnderDragonPhase currentPhase, EnderDragonPhase newPhase) : base(enderDragon)
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
        public EnderDragonPhase getCurrentPhase()
        {
            return currentPhase;
        }

        /**
         * Gets the new phase that the dragon will switch to.
         * 
         * @return the new dragon phase
         */
        public EnderDragonPhase getNewPhase()
        {
            return newPhase;
        }

        /**
         * Sets the new phase for the ender dragon.
         * 
         * @param newPhase the new dragon phase
         */
        public void setNewPhase(EnderDragonPhase newPhase)
        {
            this.newPhase = newPhase;
        }

        public bool isCancelled()
        {
            return cancel;
        }

        public void setCancelled(bool cancel)
        {
            this.cancel = cancel;
        }
    }
}
