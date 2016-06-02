using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine.NET.entity
{
    public interface INamedEntity
    {
        /**
         * This is the name of the specified AnimalTamer.
         *
         * @return The name to reference on tamed animals or null if a name cannot be obtained
         */
        String getName();
    }
}
