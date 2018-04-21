using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRex
{
    public class MyEncodingInfo
    {
        public EncodingInfo Enc { get; set; }
        public override string ToString()
        {
            if (Enc == null)
                return "Auto";
            return Enc.DisplayName;
        }

        public Encoding Current
        {
            get
            {
                if (Enc == null)
                    return Encoding.Default;
                return Enc.GetEncoding();
            }
        }
    }
}
