using System;
using System.Collections.Generic;

namespace Mine.NET
{
    public abstract class BukkitCommand : Command
    {
        protected BukkitCommand(String name) : base(name)
        {
        }

        protected BukkitCommand(String name, String description, String usageMessage, List<String> aliases) : base(name, description, usageMessage, aliases)
        {
        }
    }
}
