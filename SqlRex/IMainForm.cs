using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRex
{
    interface IMainForm
    {
        void LargeFileModeChanged();
        void RegexOnLoadChanged();

        void EncodingChanged();
        void ReadonlySqlChanged();
        void AutocompleteChanged();
        
        void ConnectionsChanged();
    }
}
