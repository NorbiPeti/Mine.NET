
using System;
using System.Collections.Generic;
/**
* Represents various types of worlds that may exist
*/
public static class WorldType {
    public enum WorldTypes
    {
        NORMAL,
        FLAT,
        VERSION_1_1,
        LARGE_BIOMES,
        AMPLIFIED,
        CUSTOMIZED
    }

    private readonly static Dictionary<WorldTypes, string> BY_NAME = new Dictionary<WorldTypes, string>()
    {
        { WorldTypes.NORMAL, "DEFAULT" },
        { WorldTypes.FLAT, "FLAT" },
        { WorldTypes.VERSION_1_1, "DEFAULT_1_1" },
        { WorldTypes.LARGE_BIOMES, "LARGEBIOMES" },
        { WorldTypes.AMPLIFIED, "AMPLIFIED" },
        { WorldTypes.CUSTOMIZED, "CUSTOMIZED" }
    };
}
