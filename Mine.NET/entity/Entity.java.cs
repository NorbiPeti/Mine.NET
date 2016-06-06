using Mine.NET.command;
using Mine.NET.Event.entity;
using Mine.NET.Event.player;
using Mine.NET.metadata;
using Mine.NET.util;
using System;
using System.Collections.Generic;

namespace Mine.NET.entity
{
    /**
     * Represents a base entity in the world
     */
    public interface Entity : Metadatable, CommandSender
    {

        /**
         * Gets the entity's current position
         *
         * @return a new copy of Location containing the position of this entity
         */
        Location getLocation();

        /**
         * Stores the entity's current position in the provided Location object.
         * <p>
         * If the provided Location is null this method does nothing and returns
         * null.
         *
         * @param loc the location to copy into
         * @return The Location object provided or null
         */
        Location getLocation(Location loc);

        /**
         * Sets this entity's velocity
         *
         * @param velocity New velocity to travel with
         */
        void setVelocity(Vector velocity);

        /**
         * Gets this entity's current velocity
         *
         * @return Current travelling velocity of this entity
         */
        Vector getVelocity();

        /**
         * Returns true if the entity is supported by a block. This value is a
         * state updated by the server and is not recalculated unless the entity
         * moves.
         *
         * @return True if entity is on ground.
         */
        bool isOnGround();

        /**
         * Gets the current world this entity resides in
         *
         * @return World
         */
        World getWorld();

        /**
         * Teleports this entity to the given location. If this entity is riding a
         * vehicle, it will be dismounted prior to teleportation.
         *
         * @param location New location to teleport this entity to
         * @return <code>true</code> if the teleport was successful
         */
        bool teleport(Location location);

        /**
         * Teleports this entity to the given location. If this entity is riding a
         * vehicle, it will be dismounted prior to teleportation.
         *
         * @param location New location to teleport this entity to
         * @param cause The cause of this teleportation
         * @return <code>true</code> if the teleport was successful
         */
        bool teleport(Location location, TeleportCause cause);

        /**
         * Teleports this entity to the target Entity. If this entity is riding a
         * vehicle, it will be dismounted prior to teleportation.
         *
         * @param destination Entity to teleport this entity to
         * @return <code>true</code> if the teleport was successful
         */
        bool teleport(Entity destination);

        /**
         * Teleports this entity to the target Entity. If this entity is riding a
         * vehicle, it will be dismounted prior to teleportation.
         *
         * @param destination Entity to teleport this entity to
         * @param cause The cause of this teleportation
         * @return <code>true</code> if the teleport was successful
         */
        bool teleport(Entity destination, TeleportCause cause);

        /**
         * Returns a list of entities within a bounding box centered around this
         * entity
         *
         * @param x 1/2 the size of the box along x axis
         * @param y 1/2 the size of the box along y axis
         * @param z 1/2 the size of the box along z axis
         * @return {@code List<Entity>} List of entities nearby
         */
        List<Entity> getNearbyEntities(double x, double y, double z);

        /**
         * Returns a unique id for this entity
         *
         * @return Entity id
         */
        int getEntityId();

        /**
         * Returns the entity's current fire ticks (ticks before the entity stops
         * being on fire).
         *
         * @return int fireTicks
         */
        int getFireTicks();

        /**
         * Returns the entity's maximum fire ticks.
         *
         * @return int maxFireTicks
         */
        int getMaxFireTicks();

        /**
         * Sets the entity's current fire ticks (ticks before the entity stops
         * being on fire).
         *
         * @param ticks Current ticks remaining
         */
        void setFireTicks(int ticks);

        /**
         * Mark the entity's removal.
         */
        void remove();

        /**
         * Returns true if this entity has been marked for removal.
         *
         * @return True if it is dead.
         */
        bool isDead();

        /**
         * Returns false if the entity has died or been despawned for some other
         * reason.
         *
         * @return True if valid.
         */
        bool isValid();

        /**
         * Gets the {@link Server} that contains this Entity
         *
         * @return Server instance running this Entity
         */

        /**
         * Gets the primary passenger of a vehicle. For vehicles that could have
         * multiple passengers, this will only return the primary passenger.
         *
         * @return an entity
         */
        Entity getPassenger();

        /**
         * Set the passenger of a vehicle.
         *
         * @param passenger The new passenger.
         * @return false if it could not be done for whatever reason
         */
        bool setPassenger(Entity passenger);

        /**
         * Check if a vehicle has passengers.
         *
         * @return True if the vehicle has no passengers.
         */
        bool isEmpty();

        /**
         * Eject any passenger.
         *
         * @return True if there was a passenger.
         */
        bool eject();

        /**
         * Returns the distance this entity has fallen
         *
         * @return The distance.
         */
        float getFallDistance();

        /**
         * Sets the fall distance for this entity
         *
         * @param distance The new distance.
         */
        void setFallDistance(float distance);

        /**
         * Record the last {@link EntityDamageEvent} inflicted on this entity
         *
         * @param event a {@link EntityDamageEvent}
         */
        void setLastDamageCause(EntityDamageEventArgs event_);

        /**
         * Retrieve the last {@link EntityDamageEvent} inflicted on this entity.
         * This event may have been cancelled.
         *
         * @return the last known {@link EntityDamageEvent} or null if hitherto
         *     unharmed
         */
        EntityDamageEventArgs getLastDamageCause();

        /**
         * Returns a unique and persistent id for this entity
         *
         * @return unique id
         */
        Guid getUniqueId();

        /**
         * Gets the amount of ticks this entity has lived for.
         * <p>
         * This is the equivalent to "age" in entities.
         *
         * @return Age of entity
         */
        int getTicksLived();

        /**
         * Sets the amount of ticks this entity has lived for.
         * <p>
         * This is the equivalent to "age" in entities. May not be less than one
         * tick.
         *
         * @param value Age of entity
         */
        void setTicksLived(int value);

        /**
         * Performs the specified {@link EntityEffect} for this entity.
         * <p>
         * This will be viewable to all players near the entity.
         *
         * @param type Effect to play.
         */
        void playEffect(EntityEffect type);

        /**
         * Get the type of the entity.
         *
         * @return The entity type.
         */
        EntityType getType();

        /**
         * Returns whether this entity is inside a vehicle.
         *
         * @return True if the entity is in a vehicle.
         */
        bool isInsideVehicle();

        /**
         * Leave the current vehicle. If the entity is currently in a vehicle (and
         * is removed from it), true will be returned, otherwise false will be
         * returned.
         *
         * @return True if the entity was in a vehicle.
         */
        bool leaveVehicle();

        /**
         * Get the vehicle that this player is inside. If there is no vehicle,
         * null will be returned.
         *
         * @return The current vehicle.
         */
        Entity getVehicle();

        /**
         * Sets a custom name on a mob. This name will be used in death messages
         * and can be sent to the client as a nameplate over the mob.
         * <p>
         * Setting the name to null or an empty string will clear it.
         * <p>
         * This value has no effect on players, they will always use their real
         * name.
         *
         * @param name the name to set
         */
        void setCustomName(String name);

        /**
         * Gets the custom name on a mob. If there is no name this method will
         * return null.
         * <p>
         * This value has no effect on players, they will always use their real
         * name.
         *
         * @return name of the mob or null
         */
        String getCustomName();

        /**
         * Sets whether or not to display the mob's custom name client side. The
         * name will be displayed above the mob similarly to a player.
         * <p>
         * This value has no effect on players, they will always display their
         * name.
         *
         * @param flag custom name or not
         */
        void setCustomNameVisible(bool flag);

        /**
         * Gets whether or not the mob's custom name is displayed client side.
         * <p>
         * This value has no effect on players, they will always display their
         * name.
         *
         * @return if the custom name is displayed
         */
        bool isCustomNameVisible();

        /**
         * Sets whether the entity has a team colored (default: white) glow.
         *
         * @param flag if the entity is glowing
         */
        void setGlowing(bool flag);

        /**
         * Gets whether the entity is glowing or not.
         *
         * @return whether the entity is glowing
         */
        bool isGlowing();

        /**
         * Sets whether the entity is invulnerable or not.
         * <p>
         * When an entity is invulnerable it can only be damaged by players in
         * creative mode.
         *
         * @param flag if the entity is invulnerable
         */
        void setInvulnerable(bool flag);

        /**
         * Gets whether the entity is invulnerable or not.
         *
         * @return whether the entity is
         */
        bool isInvulnerable();
    }
}
