
using System;
using System.Collections.Generic;
/**
* A list of effects that the server is able to send to players.
*/
public class Effect
{

    public enum Effects
    {
        /**
         * An alternate click sound.
         */
        CLICK2 = 1000,
        /**
         * A click sound.
         */
        CLICK1 = 1001,
        /**
         * Sound of a bow firing.
         */
        BOW_FIRE = 1002,
        /**
         * Sound of a door opening/closing.
         */
        DOOR_TOGGLE = 1003,
        /**
         * Sound of fire being extinguished.
         */
        EXTINGUISH = 1004,
        /**
         * A song from a record. Needs the record item ID as additional info
         */
        RECORD_PLAY = 1010,
        /**
         * Sound of ghast shrieking.
         */
        GHAST_SHRIEK = 1007,
        /**
         * Sound of ghast firing.
         */
        GHAST_SHOOT = 1008,
        /**
         * Sound of blaze firing.
         */
        BLAZE_SHOOT = 1009,
        /**
         * Sound of zombies chewing on wooden doors.
         */
        ZOMBIE_CHEW_WOODEN_DOOR = 1012,
        /**
         * Sound of zombies chewing on iron doors.
         */
        ZOMBIE_CHEW_IRON_DOOR = 1011,
        /**
         * Sound of zombies destroying a door.
         */
        ZOMBIE_DESTROY_DOOR = 1021,
        /**
         * A visual smoke effect. Needs direction as additional info.
         */
        SMOKE = 2000,
        /**
         * Sound of a block breaking. Needs block ID as additional info.
         */
        STEP_SOUND = 2001,
        /**
         * Visual effect of a splash potion breaking. Needs potion data value as
         * additional info.
         */
        POTION_BREAK = 2002,
        /**
         * An ender eye signal; a visual effect.
         */
        ENDER_SIGNAL = 2003,
        /**
         * The flames seen on a mobspawner; a visual effect.
         */
        MOBSPAWNER_FLAMES = 2004
    }

    public static Dictionary<Effects, Effect> AllEffects = new Dictionary<Effects, Effect>
    {
    /**
     * An alternate click sound.
     */
    { Effects.CLICK2, new Effect(1000, EffectType.SOUND) },
        /**
         * A click sound.
         */
        { Effects.CLICK1, new Effect(1001, EffectType.SOUND) },
        /**
         * Sound of a bow firing.
         */
        { Effects.BOW_FIRE, new Effect(1002, EffectType.SOUND) },
        /**
         * Sound of a door opening/closing.
         */
        { Effects.DOOR_TOGGLE, new Effect(1003, EffectType.SOUND) },
        /**
         * Sound of fire being extinguished.
         */
        { Effects.EXTINGUISH, new Effect(1004, EffectType.SOUND) },
        /**
         * A song from a record. Needs the record item ID as additional info
         */
        { Effects.RECORD_PLAY, new Effect(1010, EffectType.SOUND, typeof(Material)) },
    /**
     * Sound of ghast shrieking.
     */
    { Effects.GHAST_SHRIEK, new Effect(1007, EffectType.SOUND) },
    /**
     * Sound of ghast firing.
     */
    { Effects.GHAST_SHOOT, new Effect(1008, EffectType.SOUND) },
    /**
     * Sound of blaze firing.
     */
    { Effects.BLAZE_SHOOT, new Effect(1009, EffectType.SOUND) },
    /**
     * Sound of zombies chewing on wooden doors.
     */
    { Effects.ZOMBIE_CHEW_WOODEN_DOOR, new Effect(1012, EffectType.SOUND) },
    /**
     * Sound of zombies chewing on iron doors.
     */
    { Effects.ZOMBIE_CHEW_IRON_DOOR, new Effect(1011, EffectType.SOUND) },
    /**
     * Sound of zombies destroying a door.
     */
    { Effects.ZOMBIE_DESTROY_DOOR, new Effect(1021, EffectType.SOUND) },
    /**
     * A visual smoke effect. Needs direction as additional info.
     */
    { Effects.SMOKE, new Effect(2000, EffectType.VISUAL, typeof(BlockFace)) },
    /**
     * Sound of a block breaking. Needs block ID as additional info.
     */
    { Effects.STEP_SOUND, new Effect(2001, EffectType.SOUND, typeof(Material)) },
    /**
     * Visual effect of a splash potion breaking. Needs potion data value as
     * additional info.
     */
    { Effects.POTION_BREAK, new Effect(2002, EffectType.VISUAL, typeof(Potion)) },
    /**
     * An ender eye signal; a visual effect.
     */
    { Effects.ENDER_SIGNAL, new Effect(2003, EffectType.VISUAL) },
    /**
     * The flames seen on a mobspawner; a visual effect.
     */
    { Effects.MOBSPAWNER_FLAMES, new Effect(2004, EffectType.VISUAL) }
    };

    private readonly int id;
    private readonly EffectType Type;
    private readonly Type data;

    Effect(int id, EffectType EffectType)
    {
        new Effect(id, EffectType, null);
    }

    Effect(int id, EffectType type, Type data)
    {
        this.id = id;
        this.Type = type;
        this.data = data;
    }

    /**
     * Gets the ID for this effect.
     *
     * @return ID of this effect
     * [Obsolete] Magic value
     */
    [Obsolete]
    public int getId()
    {
        return this.id;
    }

    /**
     * @return The EffectType of the effect.
     */
    public EffectType getType()
    {
        return this.Type;
    }

    /**
     * @return The class which represents data for this effect, or null if
     *     none
     */
    public Type getData()
    {
        return this.data;
    }

    /**
     * Gets the Effect associated with the given ID.
     *
     * @param id ID of the Effect to return
     * @return Effect with the given ID
     * [Obsolete] Magic value
     */
    [Obsolete]
    public static Effect getById(int id)
    {
        return AllEffects[(Effects)id];
    }

    /**
     * Represents the EffectType of an effect.
     */
    public enum EffectType { SOUND, VISUAL }
}
