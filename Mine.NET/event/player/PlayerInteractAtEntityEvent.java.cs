using Mine.NET.entity;
using Mine.NET.inventory;
using Mine.NET.util;

namespace Mine.NET.Event.player
{
    /**
     * Represents an event that is called when a player right clicks an entity
     * with a location on the entity the was clicked.
     */
    public class PlayerInteractAtEntityEventArgs : PlayerInteractEntityEventArgs
    {
        private readonly Vector position;

        public PlayerInteractAtEntityEventArgs(Player who, Entity clickedEntity, Vector position) :
            this(who, clickedEntity, position, EquipmentSlot.HAND)
        {
        }

        public PlayerInteractAtEntityEventArgs(Player who, Entity clickedEntity, Vector position, EquipmentSlot hand) :
            base(who, clickedEntity, hand)
        {
            this.position = position;
        }

        public Vector getClickedPosition()
        {
            return position.clone();
        }
    }
}
