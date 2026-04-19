using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlRex.Legacy
{
    public static class nf
    {
        public static string n(this string val)
        {
            return val == null ? null : val;
        }
    }
}
