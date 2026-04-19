using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlRex.Legacy
{
    public class TimeSpanEventArgs: EventArgs
    {
        public TimeSpan Data { get; private set; }
        public TimeSpanEventArgs(TimeSpan data)
        {
            Data = data;
        }
    }
}
