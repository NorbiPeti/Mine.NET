using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine.NET
{
    public class DataMine : DataContext
    {
        public DataMine(string connection) : base(connection)
        {
        }
    }
}
