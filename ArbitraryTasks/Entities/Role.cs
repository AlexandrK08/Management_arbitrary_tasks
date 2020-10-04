using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LinqToDB.Mapping;

namespace ArbitraryTasks.Entities
{
    public class Role
    {
        public UInt64 ID { get; set; }

        public UInt16 Value { get; set; }

        public String Caption { get; set; }

        public String Description { get; set; }
    }
}
