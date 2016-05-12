using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine.NET.configuration.file
{
    public class JsonConfiguration : FileConfiguration
    {
        public override void loadFromString(string contents)
        {
            throw new NotImplementedException(); //TODO
        }

        public override string saveToString()
        {
            throw new NotImplementedException();
        }

        protected override string buildHeader()
        {
            throw new NotImplementedException();
        }
    }
}
