namespace Mine.NET.material;

import org.bukkit.Materials;
import org.bukkit.NetherWartsState;

/**
 * Represents nether wart
 */
public class NetherWarts : MaterialData {
    public NetherWarts() : base(Materials.NETHER_WARTS) {
    }

    public NetherWarts(NetherWartsState state) {
        this();
        setState(state);
    }

    /**
     * @param type the raw type id
     * [Obsolete] Magic value
     */
    [Obsolete]
    public NetherWarts(int type) : base(type) {
    }

    public NetherWarts(Materials type) {
        base (type);
    }

    /**
     * @param type the raw type id
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public NetherWarts(int type, readonly byte data) : base(type, data) {
    }

    /**
     * @param type the type
     * @param data the raw data value
     * [Obsolete] Magic value
     */
    [Obsolete]
    public NetherWarts(Materials type, readonly byte data) : base(type, data) {
    }

    /**
     * Gets the current growth state of this nether wart
     *
     * @return NetherWartsState of this nether wart
     */
    public NetherWartsState getState() {
        switch (getData()) {
            case 0:
                return NetherWartsState.SEEDED;
            case 1:
                return NetherWartsState.STAGE_ONE;
            case 2:
                return NetherWartsState.STAGE_TWO;
            default:
                return NetherWartsState.RIPE;
        }
    }

    /**
     * Sets the growth state of this nether wart
     *
     * @param state New growth state of this nether wart
     */
    public void setState(NetherWartsState state) {
        switch (state) {
            case SEEDED:
                setData((byte) 0x0);
                return;
            case STAGE_ONE:
                setData((byte) 0x1);
                return;
            case STAGE_TWO:
                setData((byte) 0x2);
                return;
            case RIPE:
                setData((byte) 0x3);
                return;
        }
    }

    public override string ToString() {
        return getState() + " " + base.ToString();
    }

    public override NetherWarts clone() {
        return (NetherWarts) base.clone();
    }
}
