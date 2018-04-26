using Microsoft.WindowsAPICodePack.Taskbar;
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
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public partial class MainForm : Form, IMainForm
    {
        private int childFormNumber = 0;

        SynchronizationContext _sync;
        public void Syncronized(Action action)
        {
            _sync.Send((o) => action(), null);
        }

        public T Syncronized<T>(Func<T> action)
        {
            T result = default(T);
            _sync.Send((o) => result = action(), null);
            return result;
        }
        public MainForm()
        {
            _sync = SynchronizationContext.Current;
            TaskbarManager.Instance.ApplicationId = Assembly.GetEntryAssembly().Location;

            InitializeComponent();
            LargeFileModeChanged();
            RegexOnLoadChanged();
            nextFindToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.PageDown;
            prevFindToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.PageUp;

            RebuildRecent();
            Utils.SetDoubleBuffered(tabForms);

            var list  = Encoding.GetEncodings();

            var ls = new List<MyEncodingInfo>();

            ls.Add(new MyEncodingInfo());
            ls.AddRange(list.Select<EncodingInfo, MyEncodingInfo>((a) => new MyEncodingInfo { Enc = a }));

            cbEncoding.Items.AddRange(ls.ToArray());

            cbEncoding.SelectedIndex = 0;
        }

        

        string _recentFiles = Application.StartupPath + @"\recent.txt";
        private JumpList _jumpList;

        

        private void OpenFile(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                AppendRecent(FileName);
                RebuildRecent();

                OpenScript(FileName);
            }
        }


        public void RebuildRecent()
        {
            if (!File.Exists(_recentFiles))
                return;

            var files = File.ReadAllLines(_recentFiles);

            int i = 0;
            while (i < fileMenu.DropDownItems.Count)
            {
                if(fileMenu.DropDownItems[i].Tag != null && fileMenu.DropDownItems[i].Tag.ToString() == "1")
                {
                    i++;
                }
                else
                {
                    fileMenu.DropDownItems.RemoveAt(i);
                }
            }

            
            foreach (var item in files)
            {
                fileMenu.DropDownItems.Add(new ToolStripMenuItem(item, null, OnRecentFileClick));
            }
            
        }

        void RebuildRecentJumpList()
        {
            if (!File.Exists(_recentFiles))
                return;

            var files = File.ReadAllLines(_recentFiles);

            
            _jumpList = JumpList.CreateJumpList();
            List<JumpListTask> tasks = new List<JumpListTask>();

            List<JumpListCustomCategory> cats = new List<JumpListCustomCategory>();
            foreach (var item in files)
            {
                var fi = new FileInfo(item);
                
                var task = new JumpListLink(Assembly.GetEntryAssembly().Location, fi.Name + "    (" + fi.DirectoryName + ")");
                task.WorkingDirectory = fi.DirectoryName;
                task.Arguments = item;
                
                
                tasks.Add(task);   
                
            }

            _jumpList.AddUserTasks(tasks.ToArray());
            _jumpList.Refresh();
        }

        private JumpListCustomCategory CreateCategory(string fileName, string categoryName, string searchPattern)
        {
            var fi = new FileInfo(fileName);

            var category = new JumpListCustomCategory(fi.DirectoryName);

            var vsmPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "VSM");
            var items = from f in Directory.GetFiles(vsmPath, searchPattern)
                        select new JumpListItem(Path.Combine(vsmPath, f));
            category.AddJumpListItems(items.ToArray());

            return category;
        }

        private void OnRecentFileClick(object sender, EventArgs e)
        {
            var fileName = (sender as ToolStripMenuItem).Text;
            AppendRecent(fileName);
            RebuildRecent();
            OpenScript(fileName);
        }

        public void OpenScript(string file, bool isFolder = false)
        {
            
            var f = new Form1();

            var encoding = (cbEncoding.SelectedItem as MyEncodingInfo).Enc;
            Common.Async.ExecAsync(this, (bgw) =>
            {
                if(isFolder)
                {
                    var fi = new FileInfo(file);
                    var tempFile = Path.GetTempFileName();
                    foreach (var item in fi.Directory.GetFiles("*.sql", SearchOption.AllDirectories))
                    {
                        File.AppendAllText(tempFile, File.ReadAllText(item.FullName));
                        File.AppendAllText(tempFile, Environment.NewLine);
                    }
                    file = tempFile;
                }
                f.LoadFile(file, true, encoding);
            }, (elapsed) =>
            {
                toolStripStatusLabel.Text = elapsed.ToString();
                f.MdiParent = this;
                f.Show();
                
            });
            
        }

        public void AppendRecent(string fileName)
        {
            if (File.Exists(_recentFiles))
            {
                bool fileAdded = false;
                var files = File.ReadAllLines(_recentFiles);
                foreach (var item in files)
                {
                    if(item == fileName)
                    {
                        fileAdded = true;
                    }
                }

                if(!fileAdded)
                {
                    var newLines = new List<string>(files);
                    newLines.Insert(0, fileName);
                    File.WriteAllLines(_recentFiles, newLines);
                    
                }
                else
                {
                    var newLines = new List<string>(files);
                    var idx = newLines.IndexOf(fileName);
                    newLines.RemoveAt(idx);
                    newLines.Insert(0, fileName);
                    File.WriteAllLines(_recentFiles, newLines);
                }
            }
            else
            {
                File.AppendAllLines(_recentFiles, new[] { fileName });
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "SQL Files (*.sql)|*.sql|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;

                if (this.ActiveMdiChild.Tag != null)
                {
                    var form = (this.ActiveMdiChild.Tag as TabPage).Tag as IChildForm;

                    var encoding = (cbEncoding.SelectedItem as MyEncodingInfo).Current;
                    form.SaveFile(FileName, encoding);
                }
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabForms.SelectedTab != null)
            {
                var ctl = (tabForms.SelectedTab.Tag as Form).ActiveControl;
                if (ctl is TextBox)
                {
                    (ctl as TextBox).Cut();
                }
                if (ctl is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (ctl as FastColoredTextBoxNS.FastColoredTextBox).Cut();
                }
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabForms.SelectedTab != null)
            {
                var ctl = (tabForms.SelectedTab.Tag as Form).ActiveControl;
                if (ctl is TextBox)
                {
                    (ctl as TextBox).Copy();
                }
                if (ctl is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (ctl as FastColoredTextBoxNS.FastColoredTextBox).Copy();
                }
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabForms.SelectedTab != null)
            {
                var ctl = (tabForms.SelectedTab.Tag as Form).ActiveControl;
                if (ctl is TextBox)
                {
                    (ctl as TextBox).Paste();
                }
                if(ctl is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (ctl as FastColoredTextBoxNS.FastColoredTextBox).Paste();
                }
            }
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                tabForms.Visible = false;
            
            else
            {
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
                
                if (this.ActiveMdiChild.Tag == null)
                {
                    
                    TabPage tp = new TabPage(this.ActiveMdiChild.Text);

                    

                    tp.Tag = this.ActiveMdiChild;
                    tp.Parent = tabForms;
                    tabForms.SelectedTab = tp;

                    this.ActiveMdiChild.Tag = tp;
                    ActiveMdiChild.FormClosing += ActiveMdiChild_FormClosing;
                    this.ActiveMdiChild.FormClosed += new FormClosedEventHandler(ActiveMdiChild_FormClosed);
                    (ActiveMdiChild as IChildForm).OnTextModified += MainForm_OnTextModified;
                    (ActiveMdiChild as IChildForm).OnAsyncCompleted += MainForm_OnAsyncCompleted;

                    tp.ToolTipText = (ActiveMdiChild as IChildForm).FileName;

                    Utils.SetDoubleBuffered(tp);
                }

                if (!tabForms.Visible) tabForms.Visible = true;
                tabForms.SelectedTab = ActiveMdiChild.Tag as TabPage;

            }
        }

        private void MainForm_OnAsyncCompleted(object sender, TimeSpan e)
        {
            toolStripStatusLabel.Text = e.ToString();
        }

        private void MainForm_OnTextModified(object sender, string e)
        {
            ((sender as Form).Tag as TabPage).Text = e;
        }

        private void ActiveMdiChild_FormClosing(object sender, FormClosingEventArgs e)
        {
            var form = sender as IChildForm;
            if(form.TextModified)
            {
                if(MessageBox.Show("Script " + form.FileName + " modified, Close?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Tp_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Middle)
            {
                var tabControl = sender as TabControl;
                var tabs = tabControl.TabPages;

                if (e.Button == MouseButtons.Middle)
                {
                    var page = tabs.Cast<TabPage>().Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location)).First();
                    (page.Tag as Form).Close();
                    
                }
                
            }
        }

        private void ActiveMdiChild_FormClosed(object sender,
                                    FormClosedEventArgs e)
        {
            ((sender as Form).Tag as TabPage).Dispose();
        }

        private void tabForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((tabForms.SelectedTab != null) &&
       (tabForms.SelectedTab.Tag != null))
                (tabForms.SelectedTab.Tag as Form).Select();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveScript();
        }

        private void SaveScript()
        {
            if (this.ActiveMdiChild.Tag != null)
            {
                var form = (this.ActiveMdiChild.Tag as TabPage).Tag as IChildForm;
                form.SaveFile();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScript();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabForms.SelectedTab != null)
            {
                var ctl = (tabForms.SelectedTab.Tag as Form).ActiveControl;
                if (ctl is TextBox)
                {
                    (ctl as TextBox).SelectAll();
                }
                if (ctl is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (ctl as FastColoredTextBoxNS.FastColoredTextBox).SelectAll();
                }
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabForms.SelectedTab != null)
            {
                var ctl = (tabForms.SelectedTab.Tag as Form).ActiveControl;
                if (ctl is TextBox)
                {
                    
                }
                if (ctl is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (ctl as FastColoredTextBoxNS.FastColoredTextBox).Redo();
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabForms.SelectedTab != null)
            {
                var ctl = (tabForms.SelectedTab.Tag as Form).ActiveControl;
                if (ctl is TextBox)
                {
                    (ctl as TextBox).Undo();
                }
                if (ctl is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (ctl as FastColoredTextBoxNS.FastColoredTextBox).Undo();
                }
            }
        }

        private void svnDiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        

        public void ShowGitDiff(string file, params int[] revs)
        {
            try
            {
                var fi = new FileInfo(file);

                var tempPath = Application.StartupPath + @"\temp";
                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);

                var list = new List<string>();

                Common.Async.ExecAsync(this, (b) =>
                {

                    var ps = new Process();

                    ps.StartInfo = new ProcessStartInfo("git", @"-C """ + fi.DirectoryName + @""" log --pretty=format:""%H"" """ + fi.Name + @"""");

                    ps.StartInfo.CreateNoWindow = true;
                    ps.StartInfo.RedirectStandardOutput = true;
                    ps.StartInfo.RedirectStandardError = true;
                    ps.StartInfo.UseShellExecute = false;

                    ps.Start();
                    ps.WaitForExit();


                    while (!ps.StandardOutput.EndOfStream)
                    {
                        string line = ps.StandardOutput.ReadLine();
                        list.Add(line);
                    }

                    if(list.Count == 0)
                    {
                        Syncronized(() => MessageBox.Show("No Git repo found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1));
                        return;
                    }
                    var file1 = GitLogForm.GetGitFile(list[0], file);
                    var file2 = file;

                    var ps2 = new Process();
                    ps2.StartInfo = new ProcessStartInfo("TortoiseMerge", " /base:\"" + file1 + "\" /mine:\"" + file2 + "\"");
                    ps2.Start();

                }, (tm) => toolStripStatusLabel.Text = tm.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            RebuildRecentJumpList();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new SettingsForm();
            f.MdiParent = this;
            f.Show();
        }

        

       

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new AboutBox();
            f.ShowDialog();
        }

        private void showGitHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new GitLogForm();
            f.MdiParent = this;
            f.Show();
        }

        private void gitLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabForms.TabCount > 0 && tabForms.SelectedTab != null)
            {
                var form = (tabForms.SelectedTab.Tag as Form);
                var file = (form as IChildForm).FileName;

                var list = new List<string>();

                var tempPath = Application.StartupPath + @"\temp";
                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);

                Common.Async.ExecAsync(form, (b) => {
                    
                    var ps = new Process();

                    var fi = new FileInfo(file);

                    var tempFile = tempPath + @"\" + Guid.NewGuid().ToString("N");
                    var str = @"git -C """ + fi.DirectoryName + @""" log --pretty=format:""%H♦%cn♦%s♦%aI"" """ + fi.Name + @""" > """ + tempFile + @"""";
                    ps.StartInfo = new ProcessStartInfo("cmd", "/c " + str);
                    ps.StartInfo.UseShellExecute = true;
                    ps.Start();
                    ps.WaitForExit();

                    list.AddRange(File.ReadAllLines(tempFile));

                  

                }, (elapsed) => {
                    toolStripStatusLabel.Text = elapsed.ToString();
                    var f = new GitLogForm(file);
                    f.SetLog(list);
                    f.ShowDialog();
                });
            }
        }

        private void gitDiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabForms.TabCount > 0 && tabForms.SelectedTab != null)
            {
                var form = (tabForms.SelectedTab.Tag as Form);
                var file = (form as IChildForm).FileName;
                ShowGitDiff(file);
            }
        }

        private void showGraphViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void nextFindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabForms.TabCount > 0 && tabForms.SelectedTab != null)
            {
                var form = (tabForms.SelectedTab.Tag as Form);
                (form as IChildForm).NextItem();
            }
        }

        private void prevFindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabForms.TabCount > 0 && tabForms.SelectedTab != null)
            {
                var form = (tabForms.SelectedTab.Tag as Form);
                (form as IChildForm).PrevItem();
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                
                OpenScript(FileName, true);
            }
        }

        public void LargeFileModeChanged()
        {
            lblLargeFilesMode.Text = "LargeFiles mode : " + (Config.UseLargeFiles ? "ON" : "OFF");
            
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void RegexOnLoadChanged()
        {
            lblRegexOnLoad.Text = "RegexOnLoad mode: " + (Config.RegexOnLoad ? "ON" : "OFF");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var f = new SqlViewerForm();
            f.MdiParent = this;
            f.Show();
        }
    }
}
