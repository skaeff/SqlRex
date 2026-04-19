using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRex.Legacy;

namespace SqlRex
{
    interface IChildForm
    {
        event EventHandler<StringEventArgs> OnLastQuery;
        event EventHandler<StringEventArgs> OnTextModified;
        event EventHandler<TimeSpanEventArgs> OnAsyncCompleted;

        event EventHandler<StringEventArgs> OnCaptionChanged;

        string Status2 { get; }
        string FileName { get;  }
        bool TextModified { get;  }

        void SaveFile();
        void SaveFile(string fileName, Encoding enc);
        void Syncronized(Action action);
        T Syncronized<T>(Func<T> action);
        void NextItem();
        void PrevItem();

        void NotifyReadonlySql();
        void NotifyAutocomplete();

        void NotifyReloadConnections();
    }
}
