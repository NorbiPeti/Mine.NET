using Mine.NET.entity;
using Mine.NET.inventory;
using System;
using System.Collections.Generic;

namespace Mine.NET.Event.entity
{
    /**
     * Thrown whenever a {@link Player} dies
     */
    public class PlayerDeathEventArgs : EntityDeathEventArgs<Player>
    {
        private int newExp = 0;
        private String deathMessage = "";
        private int newLevel = 0;
        private int newTotalExp = 0;
        private bool keepLevel = false;
        private bool keepInventory = false;

        public PlayerDeathEventArgs(Player player, List<ItemStack> drops, int droppedExp, String deathMessage) :
            this(player, drops, droppedExp, 0, deathMessage)
        {
        }

        public PlayerDeathEventArgs(Player player, List<ItemStack> drops, int droppedExp, int newExp, String deathMessage) :
            this(player, drops, droppedExp, newExp, 0, 0, deathMessage)
        {
        }

        public PlayerDeathEventArgs(Player player, List<ItemStack> drops, int droppedExp, int newExp, int newTotalExp, int newLevel, String deathMessage) :
            base(player, drops, droppedExp)
        {
            this.newExp = newExp;
            this.newTotalExp = newTotalExp;
            this.newLevel = newLevel;
            this.deathMessage = deathMessage;
        }

        /**
         * Set the death message that will appear to everyone on the server.
         *
         * @param deathMessage Message to appear to other players on the server.
         */
        public void setDeathMessage(String deathMessage)
        {
            this.deathMessage = deathMessage;
        }

        /**
         * Get the death message that will appear to everyone on the server.
         *
         * @return Message to appear to other players on the server.
         */
        public String getDeathMessage()
        {
            return deathMessage;
        }

        /**
         * Gets how much EXP the Player should have at respawn.
         * <p>
         * This does not indicate how much EXP should be dropped, please see
         * {@link #getDroppedExp()} for that.
         *
         * @return New EXP of the respawned player
         */
        public int getNewExp()
        {
            return newExp;
        }

        /**
         * Sets how much EXP the Player should have at respawn.
         * <p>
         * This does not indicate how much EXP should be dropped, please see
         * {@link #setDroppedExp(int)} for that.
         *
         * @param exp New EXP of the respawned player
         */
        public void setNewExp(int exp)
        {
            newExp = exp;
        }

        /**
         * Gets the Level the Player should have at respawn.
         *
         * @return New Level of the respawned player
         */
        public int getNewLevel()
        {
            return newLevel;
        }

        /**
         * Sets the Level the Player should have at respawn.
         *
         * @param level New Level of the respawned player
         */
        public void setNewLevel(int level)
        {
            newLevel = level;
        }

        /**
         * Gets the Total EXP the Player should have at respawn.
         *
         * @return New Total EXP of the respawned player
         */
        public int getNewTotalExp()
        {
            return newTotalExp;
        }

        /**
         * Sets the Total EXP the Player should have at respawn.
         *
         * @param totalExp New Total EXP of the respawned player
         */
        public void setNewTotalExp(int totalExp)
        {
            newTotalExp = totalExp;
        }

        /**
         * Gets if the Player should keep all EXP at respawn.
         * <p>
         * This flag overrides other EXP settings
         *
         * @return True if Player should keep all pre-death exp
         */
        public bool getKeepLevel()
        {
            return keepLevel;
        }

        /**
         * Sets if the Player should keep all EXP at respawn.
         * <p>
         * This overrides all other EXP settings
         * <p>
         * This doesn't prevent prevent the EXP from dropping.
         * {@link #setDroppedExp(int)} should be used stop the
         * EXP from dropping.
         *
         * @param keepLevel True to keep all current value levels
         */
        public void setKeepLevel(bool keepLevel)
        {
            this.keepLevel = keepLevel;
        }

        /**
         * Sets if the Player keeps inventory on death.
         *
         * @param keepInventory True to keep the inventory
         */
        public void setKeepInventory(bool keepInventory)
        {
            this.keepInventory = keepInventory;
        }

        /**
         * Gets if the Player keeps inventory on death.
         *
         * @return True if the player keeps inventory on death
         */
        public bool getKeepInventory()
        {
            return keepInventory;
        }
    }
}
