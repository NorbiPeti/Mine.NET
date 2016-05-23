using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine.NET.configuration.file
{
    public class JsonConfiguration : FileConfiguration
    {
        private JsonConfiguration() { }

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

        public static JsonConfiguration loadConfiguration(StreamReader reader)
        {
            var jc = new JsonConfiguration();
            jc.loadFromString(reader.ReadToEnd());
            return jc;
        }

        public static JsonConfiguration loadConfiguration(FileInfo file)
        {
            using (var sr = new StreamReader(file.OpenRead()))
                return loadConfiguration(sr);
        }
    }
}
