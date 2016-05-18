namespace Mine.NET.material;

import org.bukkit.Materials;
import org.bukkit.entity.EntityType;
import org.bukkit.inventory.meta.ItemMeta;

/**
 * Represents a spawn egg that can be used to spawn mobs
 */
public class SpawnEgg : MaterialData {

    public SpawnEgg() {
        base(Materials.MONSTER_EGG);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SpawnEgg(int type, byte data){
        base(type, data);
    }

    /**
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public SpawnEgg(byte data) {
        base(Materials.MONSTER_EGG, data);
    }

    public SpawnEgg(EntityType type) {
        this();
        setSpawnedType(type);
    }

    /**
     * Get the type of entity this egg will spawn.
     *
     * @return The entity type.
     * [Obsolete] This is now stored in {@link ItemMeta}. See SPIGOT-1592.
     */
    [Obsolete]
    public EntityType getSpawnedType() {
        return EntityType.fromId(getData());
    }

    /**
     * Set the type of entity this egg will spawn.
     *
     * @param type The entity type.
     * [Obsolete] This is now stored in {@link ItemMeta}. See SPIGOT-1592.
     */
    [Obsolete]
    public void setSpawnedType(EntityType type) {
        setData((byte) type.getTypeId());
    }

    public override string ToString() {
        return "SPAWN EGG{" + getSpawnedType() + "}";
    }

    public override SpawnEgg clone() {
        return (SpawnEgg) base.clone();
    }
}
