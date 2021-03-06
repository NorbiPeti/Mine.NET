using Mine.NET.inventory;
using System;
using System.Collections.Generic;

namespace Mine.NET
{
    /**
    * This interface provides value conversions that may be specific to a
    * runtime, or have arbitrary meaning (read: magic values).
    * <p>
    * Their existence and behavior is not guaranteed across future versions. They
    * may be poorly named, throw exceptions, have misleading parameters, or any
    * other bad programming practice.
    * <p>
    * This interface is unsupported and only for internal use.
    *
    * [Obsolete] Unsupported {@literal &} internal use only
*/
    [Obsolete]
    public interface UnsafeValues
    {

        Materials getMaterialFromInternalName(String name);

        List<String> tabCompleteInternalMaterialName(String token, List<String> completions);

        ItemStack modifyItemStack(ItemStack stack, String arguments);

        Statistic getStatisticFromInternalName(String name);

        Achievement getAchievementFromInternalName(String name);

        List<String> tabCompleteInternalStatisticOrAchievementName(String token, List<String> completions);
    }
}
