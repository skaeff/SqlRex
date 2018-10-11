
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public partial class GitLogForm : BaseForm
    {
        
       
        
        public GitLogForm()
        {
            InitializeComponent();
        }
        public GitLogForm(string fileName):this()
        {
            
            Text = fileName;
            FileName = fileName;
            listView1.Columns.Add("revision", 250);
            listView1.Columns.Add("author", 250);
            listView1.Columns.Add("time", 250);
            listView1.Columns.Add("message", 500);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            if (keyData == Keys.Enter)
            {
                Common.Async.ExecAsync(this,(b) => ShowDiff(FileName), null);
                
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ShowDiff(string file)
        {
            var cnt = Syncronized(()=> listView1.SelectedIndices.Count);
            if (cnt != 2)
                return;

            var item1 = Syncronized(() => listView1.SelectedItems[1].Tag) as string;
            var item2 = Syncronized(() => listView1.SelectedItems[0].Tag) as string;

            var fi = new FileInfo(file);


            var tempPath = Application.StartupPath + @"\temp";
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
            
            var file1 = GetGitFile(item1, file);
            var file2 = GetGitFile(item2, file);

            var ps = new Process();
            ps.StartInfo = new ProcessStartInfo("TortoiseMerge", " /base:\"" + file1 + "\" /mine:\"" + file2 + "\"");
            ps.Start();
        }

        public static string GetGitFile(string revision, string fileName)
        {
            var fi = new FileInfo(fileName);

            var tempPath = Application.StartupPath + @"\temp";
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            var tempFile = tempPath + @"\" + revision.Substring(0, 5) + "_" + fi.Name;
            var ps = new Process();

            var str = @"git -C """ + fi.DirectoryName + @""" show """ + revision + @":./" + fi.Name + @""">""" + tempFile + @"""";
           
            ps.StartInfo = new ProcessStartInfo("cmd", "/c " + str);
           
            ps.StartInfo.UseShellExecute = true;

            
            ps.Start();
            ps.WaitForExit();
            

            return tempFile;
        }

        public void SetLog(List<string> list)
        {
            listView1.Items.Clear();

            foreach (var item in list)
            {
                var data = item.Split('♦');

                var lvItem = new ListViewItem(data[0]);
                lvItem.SubItems.Add(data[1]);//author
                lvItem.SubItems.Add(data[3]);//data
                lvItem.SubItems.Add(data[2]);//msg
                lvItem.Tag = data[0];
                listView1.Items.Add(lvItem);
            }
        }

    }
}
