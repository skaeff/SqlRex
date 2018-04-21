using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SqlRex
{
    public partial class SettingsForm : Form, IChildForm
    {
        public SettingsForm()
        {
            InitializeComponent();
            cbLargeFiles.CheckedChanged -= cbLargeFiles_CheckedChanged;
            cbLargeFiles.Checked = Config.UseLargeFiles;
            cbLargeFiles.CheckedChanged += cbLargeFiles_CheckedChanged;

            cbRegexOnLoad.CheckedChanged -= cbRegexOnLoad_CheckedChanged;
            cbRegexOnLoad.Checked = Config.RegexOnLoad;
            cbRegexOnLoad.CheckedChanged += cbRegexOnLoad_CheckedChanged;
        }

        public string FileName
        {
            get
            {
                return "Settings";
            }
        }

        public bool TextModified { get; private set; }

        public event EventHandler<string> OnTextModified;
        public event EventHandler<TimeSpan> OnAsyncCompleted;

        public void SaveFile()
        {
            
        }

        public void Syncronized(Action action)
        {
            throw new NotImplementedException();
        }

        public T Syncronized<T>(Func<T> action)
        {
            throw new NotImplementedException();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            
        }
        public void NextItem()
        {
        }

        public void PrevItem()
        {
        }

        private void cbLargeFiles_CheckedChanged(object sender, EventArgs e)
        {
            Config.UseLargeFiles = cbLargeFiles.Checked;
            var main = MdiParent as IMainForm;
            if(main != null)
            {
                main.LargeFileModeChanged();
            }
        }

        public void SaveFile(string fileName, Encoding enc)
        {
            throw new NotImplementedException();
        }

        private void cbRegexOnLoad_CheckedChanged(object sender, EventArgs e)
        {
            Config.RegexOnLoad = cbRegexOnLoad.Checked;
            var main = MdiParent as IMainForm;
            if (main != null)
            {
                main.RegexOnLoadChanged();
            }
        }
    }
}
