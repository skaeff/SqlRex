using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRex
{
    public class SqlDdlObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string TypeDesc { get; set; }
        public string Text { get; set; }
        public int ObjectId { get; set; }
    }
}
