using Mine.NET.entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a splash potion hits an area
     */
    public class PotionSplashEvent : ProjectileHitEvent<ThrownPotion>, Cancellable
    {
        private static readonly HandlerList handlers = new HandlerList();
        private bool cancelled;
        private readonly Dictionary<LivingEntity, Double> affectedEntities;

        public PotionSplashEvent(ThrownPotion potion, Dictionary<LivingEntity, Double> affectedEntities) :
            base(potion)
        {
            this.affectedEntities = affectedEntities;
        }

        /**
         * Gets the potion which caused this event
         *
         * @return The thrown potion entity
         */
        public ThrownPotion getPotion()
        {
            return getEntity();
        }

        /**
         * Retrieves a list of all effected entities
         *
         * @return A fresh copy of the affected entity list
         */
        public List<LivingEntity> getAffectedEntities()
        {
            return new List<LivingEntity>(affectedEntities.Keys);
        }

        /**
         * Gets the intensity of the potion's effects for given entity; This
         * depends on the distance to the impact center
         *
         * @param entity Which entity to get intensity for
         * @return intensity relative to maximum effect; 0.0: not affected; 1.0:
         *     fully hit by potion effects
         */
        public double getIntensity(LivingEntity entity)
        {
            if (affectedEntities.ContainsKey(entity))
                return affectedEntities[entity];
            else
                return 0;
        }

        /**
         * Overwrites the intensity for a given entity
         *
         * @param entity For which entity to define a new intensity
         * @param intensity relative to maximum effect
         */
        public void setIntensity(LivingEntity entity, double intensity)
        {
            if (entity == null) throw new ArgumentNullException("You must specify a valid entity.");
            if (intensity <= 0.0)
            {
                affectedEntities.Remove(entity);
            }
            else
            {
                affectedEntities.Add(entity, Math.Min(intensity, 1.0));
            }
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            cancelled = cancel;
        }

        public override HandlerList getHandlers()
        {
            return handlers;
        }

        public new static HandlerList getHandlerList()
        {
            return handlers;
        }
    }
}
