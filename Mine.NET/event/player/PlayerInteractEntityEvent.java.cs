using Mine.NET.entity;
using Mine.NET.inventory;

namespace Mine.NET.Event.player
{
    /**
     * Represents an event that is called when a player right clicks an entity.
     */
    public class PlayerInteractEntityEventArgs<T> : PlayerEventArgs, Cancellable
    {
        protected Entity clickedEntity;
        bool cancelled = false;
        private EquipmentSlot hand;

        public PlayerInteractEntityEventArgs(Player who, Entity clickedEntity) :
            this(who, clickedEntity, EquipmentSlot.HAND)
        {
        }

        public PlayerInteractEntityEventArgs(Player who, Entity clickedEntity, EquipmentSlot hand) :
            base(who)
        {
            this.clickedEntity = clickedEntity;
            this.hand = hand;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setCancelled(bool cancel)
        {
            this.cancelled = cancel;
        }

        /**
         * Gets the entity that was rightclicked by the player.
         *
         * @return entity right clicked by player
         */
        public Entity getRightClicked()
        {
            return this.clickedEntity;
        }

        /**
         * The hand used to perform this interaction.
         *
         * @return the hand used to interact
         */
        public EquipmentSlot getHand()
        {
            return hand;
        }
    }

    public class PlayerInteractEntityEventArgs : PlayerInteractEntityEventArgs<Entity>
    {

        public PlayerInteractEntityEventArgs(Player who, Entity clickedEntity) :
            base(who, clickedEntity, EquipmentSlot.HAND)
        {
        }

        public PlayerInteractEntityEventArgs(Player who, Entity clickedEntity, EquipmentSlot hand) :
            base(who, clickedEntity, hand)
        {
        }
    }
}
