using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlRex.Legacy
{
    public class StringEventArgs: EventArgs
    {
        public string Data { get; private set; }
        public StringEventArgs(string data)
        {
            Data = data;
        }
    }
}
