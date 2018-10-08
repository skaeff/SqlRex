﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public partial class BaseForm : Form, IChildForm
    {
        SynchronizationContext _sync;
        public BaseForm()
        {
            InitializeComponent();
            _sync = SynchronizationContext.Current;
        }

        public virtual string FileName
        {
            get; protected set;
        }

        public virtual bool TextModified
        {
            get; protected set;
        }

        public event EventHandler<TimeSpan> OnAsyncCompleted;
        public event EventHandler<string> OnTextModified;

        protected void ReportTime(TimeSpan tm)
        {
            if (OnAsyncCompleted != null)
                OnAsyncCompleted(null, tm);
        }

        public virtual void NextItem()
        {
            //
        }

        public virtual void NotifyAutocomplete()
        {
            //
        }

        public virtual void NotifyReadonlySql()
        {
            //
        }

        public virtual void NotifyReloadConnections()
        {
            //
        }

        public virtual void PrevItem()
        {
            //
        }

        public virtual void SaveFile()
        {
            //
        }

        public virtual void SaveFile(string fileName, Encoding enc)
        {
            //
        }

        public virtual void Syncronized(Action action)
        {
            _sync.Send((o) => action(), null);
        }

        public virtual T Syncronized<T>(Func<T> action)
        {
            T result = default(T);
            _sync.Send((o) => result = action(), null);
            return result;
        }
    }
}
