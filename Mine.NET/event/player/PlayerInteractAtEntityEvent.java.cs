using Mine.NET.entity;
using Mine.NET.inventory;
using Mine.NET.util;

namespace Mine.NET.Event.player
{
    /**
     * Represents an event that is called when a player right clicks an entity
     * with a location on the entity the was clicked.
     */
    public class PlayerInteractAtEntityEvent : PlayerInteractEntityEvent
    {
        private static readonly HandlerList handlers = new HandlerList();
        private readonly Vector position;

        public PlayerInteractAtEntityEvent(Player who, Entity clickedEntity, Vector position) :
            this(who, clickedEntity, position, EquipmentSlot.HAND)
        {
        }

        public PlayerInteractAtEntityEvent(Player who, Entity clickedEntity, Vector position, EquipmentSlot hand) :
            base(who, clickedEntity, hand)
        {
            this.position = position;
        }

        public Vector getClickedPosition()
        {
            return position.clone();
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
