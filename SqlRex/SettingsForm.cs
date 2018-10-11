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
    public partial class SettingsForm : BaseForm
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

            cbReadonlySQL.CheckedChanged -= cbReadonlySQL_CheckedChanged;
            cbReadonlySQL.Checked = Config.ReadOnlySql;
            cbReadonlySQL.CheckedChanged += cbReadonlySQL_CheckedChanged;

            cbAutoComplete.CheckedChanged -= cbAutoComplete_CheckedChanged;
            cbAutoComplete.Checked = Config.Autocomplete;
            cbAutoComplete.CheckedChanged += cbAutoComplete_CheckedChanged;

            cbFindUsageRegex.CheckedChanged -= cbFindUsageRegex_CheckedChanged;
            cbFindUsageRegex.Checked = Config.FindUsageRegex;
            cbFindUsageRegex.CheckedChanged += cbFindUsageRegex_CheckedChanged;

            cbEncoding.SelectedIndexChanged -= cbEncoding_SelectedIndexChanged;
            var list = Encoding.GetEncodings();

            var ls = new List<MyEncodingInfo>();

            ls.Add(new MyEncodingInfo());
            ls.AddRange(list.Select<EncodingInfo, MyEncodingInfo>((a) => new MyEncodingInfo { Enc = a }));

            cbEncoding.Items.AddRange(ls.ToArray());

            var un = ls.FirstOrDefault((i) => i.Enc != null && i.Enc.DisplayName.Contains(Config.Encoding));

            if (un != default(MyEncodingInfo))
                cbEncoding.SelectedItem = un;
            else
                cbEncoding.SelectedIndex = 0;

            cbEncoding.SelectedIndexChanged += cbEncoding_SelectedIndexChanged;
        }

        public override string FileName
        {
            get
            {
                return "Settings";
            }
        }
        

        private void btnLoad_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
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
        
        private void cbRegexOnLoad_CheckedChanged(object sender, EventArgs e)
        {
            Config.RegexOnLoad = cbRegexOnLoad.Checked;
            var main = MdiParent as IMainForm;
            if (main != null)
            {
                main.RegexOnLoadChanged();
            }
        }

        private void cbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            var enc = cbEncoding.SelectedItem as MyEncodingInfo;
            
            Config.Encoding = enc.Enc?.DisplayName;
            var main = MdiParent as IMainForm;
            if (main != null)
            {
                main.EncodingChanged();
            }
        }

        private void cbReadonlySQL_CheckedChanged(object sender, EventArgs e)
        {
            Config.ReadOnlySql = cbReadonlySQL.Checked;
            var main = MdiParent as IMainForm;
            if (main != null)
            {
                main.ReadonlySqlChanged();
            }
        }
        

        private void cbAutoComplete_CheckedChanged(object sender, EventArgs e)
        {
            Config.Autocomplete = cbAutoComplete.Checked;
            var main = MdiParent as IMainForm;
            if (main != null)
            {
                main.AutocompleteChanged();
            }
        }


        private void cbFindUsageRegex_CheckedChanged(object sender, EventArgs e)
        {
            Config.FindUsageRegex = cbFindUsageRegex.Checked;
        }
    }
}
