using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRex
{
    interface IChildForm
    {

        event EventHandler<string> OnTextModified;
        event EventHandler<TimeSpan> OnAsyncCompleted;
        string FileName { get;  }
        bool TextModified { get;  }

        void SaveFile();
        void SaveFile(string fileName, Encoding enc);
        void Syncronized(Action action);
        T Syncronized<T>(Func<T> action);
        void NextItem();
        void PrevItem();
    }
}
