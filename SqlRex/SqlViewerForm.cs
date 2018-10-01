using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SqlRex
{
    public partial class SqlViewerForm : Form, IChildForm
    {
        AutoCompleteStringCollection _autoComplete = new AutoCompleteStringCollection();
        SynchronizationContext _sync;
        Encoding _encoding = Encoding.GetEncoding("windows-1251");

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
        Dictionary<string, List<string>> _serverTabs;
        string _selectedConnection = "";
        public SqlViewerForm()
        {
            InitializeComponent();
            timer1.Enabled = Config.Autocomplete;
            tbSearchNode.AutoCompleteCustomSource = _autoComplete;


            RebuildServerTabs();

           
            _sync = SynchronizationContext.Current;
            TextModified = false;
            
        }

        void RebuildServerTabs()
        {

            tabSource.TabPages.Clear();

            var conns = File.ReadAllLines(Application.StartupPath + @"\connections.txt");

            _serverTabs = new Dictionary<string, List<string>>();
            foreach (var item in conns)
            {
                var db = Utils.DecryptedConnectionString(item);
                var csb = new SqlConnectionStringBuilder(db);
                if (!csb.IntegratedSecurity)
                {
                    csb.Password = Utils.Decrypt(csb.Password);
                }
                if (!_serverTabs.ContainsKey(csb.DataSource))
                {
                    _serverTabs.Add(csb.DataSource, new List<string>());
                }
                _serverTabs[csb.DataSource].Add(item);
            }

            foreach (var item in _serverTabs)
            {
                var tp = new TabPage(item.Key);
                var lb = new ListBox();

                lb.DrawItem += lbSqlDatabases_DrawItem;
                lb.MeasureItem += lbSqlDatabases_MeasureItem;
                lb.SelectedIndexChanged += lbSqlDatabases_SelectedIndexChanged;
                lb.MouseDoubleClick += lbSqlDatabases_MouseDoubleClick;
                lb.KeyDown += lbSqlDatabases_KeyDown;
                lb.Dock = DockStyle.Fill;
                lb.DrawMode = DrawMode.OwnerDrawVariable;
                lb.Font = new Font(lb.Font.FontFamily, 10);
                lb.Items.AddRange(item.Value.ToArray());
                lb.ContextMenuStrip = contextMenuStrip1;

                tp.Controls.Add(lb);
                tabSource.TabPages.Add(tp);
            }
        }
        public event EventHandler<string> OnTextModified;
        public event EventHandler<TimeSpan> OnAsyncCompleted;
        
        private void ReportTime(TimeSpan tm)
        {
            if (OnAsyncCompleted != null)
                OnAsyncCompleted(null, tm);
        }


        public string FileName { get; private set; }
        

        void ClearFoundRanges()
        {
            var hl = fastColoredTextBox1.SyntaxHighlighter;

            foreach (var item in _foundRanges)
            {
                var rng = fastColoredTextBox1.GetLine(item.Start.iLine);

                rng.ClearAllStyle();
            }
            
            _foundRanges.Clear();
            
        }

        private List<Range> BuildTree3(string regexStr, BackgroundWorker worker)
        {
            var resultOut = new List<Range>();

            var tb = Syncronized(() => fastColoredTextBox1);


            ClearFoundRanges();
            var range = Syncronized(() => fastColoredTextBox1.Selection.Clone());
            range.Normalize();

            range.Start = new Place(0, 0);
            range.End = Syncronized(() => new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1));
            var result = range.GetRangesByLines(regexStr, RegexOptions.IgnoreCase);

            int i = 0;
            
            foreach (var item in result)
            {
                if (worker != null && worker.CancellationPending)
                    break;

                Syncronized(() => resultOut.Add(item));
                i++;
            }

            return resultOut;
        }

       

       
        string _sqlGo = @"(?i-msnx:\b(?<!-{2,}.*)go[^a-zA-Z])";
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        List<Range> _foundRanges = new List<Range>();
       
        List<SqlDdlObject> _listItems = new List<SqlDdlObject>();
        List<SqlDdlObject> _listItemsCache = new List<SqlDdlObject>();


        Stopwatch _sw = new Stopwatch();
        private void tbSearchNode_TextChanged(object sender, EventArgs e)
        {
            _sw.Restart();
            if (tbSearchNode.Text == "")
            {
                _listItems.Clear();
                _listItems.AddRange(_listItemsCache);

                _needRebuild = true;
                listView1.VirtualListSize = _listItems.Count;
                listView1.SelectedIndices.Clear();
            }
            else
            {
                _listItems.Clear();
                SqlDdlObject[] ranges;

                if(cbSearchInText.Checked)
                    ranges = _listItemsCache.Where(r => r.Text.ToLower().Contains(tbSearchNode.Text.ToLower())).ToArray();
                else
                    ranges = _listItemsCache.Where(r => r.Name.ToLower().Contains(tbSearchNode.Text.ToLower())).ToArray();

                _listItems.AddRange(ranges);
                _needRebuild = true;
                listView1.VirtualListSize = _listItems.Count;
                listView1.SelectedIndices.Clear();
            }
        }
        Dictionary<string, string> _variables = new Dictionary<string, string>();

        public string SqlText {
            get
            {
                return fastColoredTextBox1.Text;
            }
        }

        

        public void SaveFile()
        {
            MessageBox.Show("Not implemented");

            //File.WriteAllText(FileName, SqlText, _encoding);
            
            //var fi = new FileInfo(FileName);
            //Text = fi.Name;
            //TextModified = false;
            //OnTextModified(this, fi.Name);

            
        }

       
        
        public bool TextModified { get; private set; }
        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextModified = true;
            //if (!string.IsNullOrEmpty(FileName))
            //{
            //    var fi = new FileInfo(FileName);
            //    Text = "*" + fi.Name;
            //    if (OnTextModified != null)
            //        OnTextModified(this, Text);
            //}
        }

        private void copyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var str = "";
            //foreach (var item in _listItems)
            //{
            //    str += item.Text + Environment.NewLine;
            //}

            //Clipboard.SetText(str);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        

        private void OpenNotepad(string str)
        {
            var file = Path.GetTempFileName();
            File.WriteAllText(file, str);
            Process.Start("notepad.exe", file);
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (_cache.Count > e.ItemIndex && e.ItemIndex >= 0)
                e.Item = _cache[e.ItemIndex];
            else
                e.Item = new ListViewItem();
        }

        private void listView1_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            for (int i = 0; i < _listItems.Count; i++)
            {
                var item = _listItems[i];
                if(item.Text.Contains(e.Text))
                {
                    e.Index = i;
                }
            }
        }

        List<ListViewItem> _cache = new List<ListViewItem>();
        bool _needRebuild;
        private void listView1_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            if(_needRebuild)
            {
                _cache.Clear();
                foreach (var item in _listItems)
                {
                    _cache.Add(new ListViewItem(item.Name + "   [" + item.TypeDesc + "]"));
                }
                _needRebuild = false;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedIndices.Count > 0)
            {
                var idx = listView1.SelectedIndices[0];
                var range = _listItems[idx];

                _foundRanges.Clear();
                if (_ls.ContainsKey(range.ObjectId))
                    _foundRanges.AddRange(_ls[range.ObjectId]);

                fastColoredTextBox1.Text = range.Text;
                fastColoredTextBox1.DoSelectionVisible();
                
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
        
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
        }

        private void tbRegExp_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                
            }
        }

        public void NextItem()
        {
            if(listView1.SelectedIndices.Count > 0)
            {
                var idx = listView1.SelectedIndices[0];
                if (idx == _listItems.Count - 1)
                    idx = 0;
                else
                    idx = idx + 1;

                listView1.SelectedIndices.Clear();
                listView1.SelectedIndices.Add(idx);
                listView1.EnsureVisible(idx);
            }
        }

        public void PrevItem()
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var idx = listView1.SelectedIndices[0];
                if (idx == 0)
                    idx = _listItems.Count - 1;
                else
                    idx = idx - 1;

                listView1.SelectedIndices.Clear();
                listView1.SelectedIndices.Add(idx);
                listView1.EnsureVisible(idx);
            }
        }
        
        

        private void showObjectDependenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        

        private void findUsagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        
        private void findUsageswholeWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void fastColoredTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var fctb = fastColoredTextBox1;
            if (e.X < fctb.LeftIndent)
            {
                var place = fctb.PointToPlace(e.Location);
                if (fctb.Bookmarks.Contains(place.iLine))
                    fctb.Bookmarks.Remove(place.iLine);
                else
                    fctb.Bookmarks.Add(place.iLine);
            }
        }

        private void fastColoredTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        
        
        private void fastColoredTextBox1_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            
        }

        const int _maxLines = 50;
        private void fastColoredTextBox1_VisibleRangeChangedDelayed(object sender, EventArgs e)
        {
            //if (Config.UseLargeFiles)
            {
                var args = new TextChangedEventArgs(fastColoredTextBox1.VisibleRange);

                fastColoredTextBox1.OnSyntaxHighlight(args);

                var diff = fastColoredTextBox1.VisibleRange.End.iLine - fastColoredTextBox1.VisibleRange.Start.iLine;
                int minLine = fastColoredTextBox1.VisibleRange.Start.iLine - diff * 4;
                int maxLine = fastColoredTextBox1.VisibleRange.End.iLine + diff * 4;

                foreach (var item2 in _foundRanges)
                {
                    if (item2.Start.iLine > minLine && item2.End.iLine < maxLine)
                    {
                        var rng = fastColoredTextBox1.GetLine(item2.Start.iLine);

                        var hl = fastColoredTextBox1.SyntaxHighlighter;

                        rng.ClearAllStyle();
                        rng.SetStyle(new TextStyle(Brushes.Black, Brushes.Yellow, FontStyle.Regular));

                        var rng2 = new Range(fastColoredTextBox1, item2.Start, item2.End);
                        rng2.ClearAllStyle();
                        rng2.SetStyle(new TextStyle(Brushes.Black, Brushes.Orange, FontStyle.Bold));
                    }
                }
            }
        }

        private void documentMap1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        public void SaveFile(string fileName, Encoding enc)
        {
            fastColoredTextBox1.SaveToFile(fileName, enc);
        }

        private void btnGetSqlObjects_Click(object sender, EventArgs e)
        {
            var db = Utils.DecryptedConnectionString(_selectedConnection);
            var csb = new SqlConnectionStringBuilder(db);
            if (!csb.IntegratedSecurity)
            {
                csb.Password = Utils.Decrypt(csb.Password);
            }
            Text = csb.DataSource + "." + csb.InitialCatalog;
            (MdiParent as IMainForm).RefreshTab();
            Common.Async.ExecAsync(this, (b) => BuildSqlObjects(db, b), (tm) => ReportTime(tm), true);
        }

        private void BuildSqlObjects(string database, BackgroundWorker worker)
        {
            var result = Generate(database);

            Syncronized(() => _listItems.Clear());
            Syncronized(() => _listItemsCache.Clear());

            Syncronized(() => _listItems2.Clear());
            Syncronized(() => _listItemsCache2.Clear());

            Syncronized(() => _cache.Clear());
            Syncronized(() => _cache2.Clear());

            Syncronized(() => _ls.Clear());
            Syncronized(() => _lsDic.Clear());
            Syncronized(() => flowLayoutPanel1.Controls.Clear());


            Syncronized(() => _listItemsDic.Clear());
            Syncronized(() => _foundRanges.Clear());

            Syncronized(() => listView1.VirtualListSize = _listItems.Count);
            Syncronized(() => listView1.SelectedIndices.Clear());

            Syncronized(() => listView2.VirtualListSize = _listItems2.Count);
            Syncronized(() => listView2.SelectedIndices.Clear());


            Syncronized(() => findUsagesInFoundToolStripMenuItem.Text = "Find Usages in found");
            Syncronized(() => findUsagesInFoundToolStripMenuItem.Enabled = false);

            Syncronized(() => findUsagesInFoundanyTextToolStripMenuItem.Text = "Find Usages in found");
            Syncronized(() => findUsagesInFoundanyTextToolStripMenuItem.Enabled = false);
            _lastSearch = "";

            int i = 0;

            foreach (var item in result)
            {
                if (worker != null && worker.CancellationPending)
                    break;

                Syncronized(() => _listItems.Add(item));
                Syncronized(() => _listItemsCache.Add(item));

                i++;
            }

            _needRebuild = true;
            Syncronized(() => listView1.VirtualListSize = _listItems.Count);
            Syncronized(() => listView1.SelectedIndices.Clear());
            Syncronized(() => lblRegexCntFound.Text = "Found " + i.ToString() + " objects");
        }

        private List<SqlDdlObject> Generate(string database)
        {
            var result = new List<SqlDdlObject>();

            var tables = new List<string>();
            using (var conn = new SqlConnection(database))
            {
                conn.Open();
                var cmd = new SqlCommand(File.ReadAllText(Application.StartupPath + @"\query.sql"), conn);
                cmd.CommandTimeout = 0;
                //var rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var obj = new SqlDdlObject();
                    obj.Name = rdr.GetSqlString(2).ToString();
                    obj.Type = rdr.GetSqlString(1).ToString().Trim();
                    obj.Text = rdr.GetSqlString(0).ToString();
                    obj.TypeDesc = rdr.GetSqlString(3).ToString();
                    obj.ObjectId = rdr.GetInt32(4);
                    result.Add(obj);
                }
                rdr.Close();

                //foreach (var item in tables)
                //{
                //    var cmdTable = new SqlCommand(File.ReadAllText(Application.StartupPath + @"\get_create_table.sql"), conn);
                //    cmdTable.CommandTimeout = 0;
                //    cmdTable.Parameters.AddWithValue("table_name_ext", item);
                //    var tableRes = cmdTable.ExecuteScalar();
                //    if (tableRes != null)
                //    {
                //        var str = tableRes.ToString();

                //        writer.Write(str);
                //        writer.WriteLine();
                //        writer.Write("GO");
                //        writer.WriteLine();
                //    }
                //}


                conn.Close();
            }

            return result;
        }

        private void diffWithFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0 && openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var idx = listView1.SelectedIndices[0];
                var range = _listItems[idx];
                

                var file1 = Path.GetTempFileName();
                File.WriteAllText(file1, range.Text);

                var file2 = openFileDialog1.FileName;

                var ps2 = new Process();
                ps2.StartInfo = new ProcessStartInfo("TortoiseGitMerge", " /base:\"" + file1 + "\" /mine:\"" + file2 + "\"");
                ps2.Start();
            }
        }

        private void lbSqlDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((sender as ListBox).SelectedItem != null)
            //{
            //    _selectedConnection = (sender as ListBox).SelectedItem.ToString();
            //}
        }

        private void lbSqlDatabases_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                _selectedConnection = (sender as ListBox).SelectedItem.ToString();
                var db = Utils.DecryptedConnectionString(_selectedConnection);
                var csb = new SqlConnectionStringBuilder(db);
                if(!csb.IntegratedSecurity)
                {
                    csb.Password = Utils.Decrypt(csb.Password);
                }
                Text = csb.DataSource + "." + csb.InitialCatalog;
                (MdiParent as IMainForm).RefreshTab();
                Common.Async.ExecAsync(this, (b) => BuildSqlObjects(db, b), (tm) => ReportTime(tm), true);

                EnableButtons();
            }
        }

        void EnableButtons()
        {
            btnGenerateSqlFile.Enabled = true;
            btnGetSqlObjects.Enabled = true;
            btnGenerateSqlFileNoTables.Enabled = true;
            btnAssemblyExporter.Enabled = true;
        }

        private void getCREATETABLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var idx = listView1.SelectedIndices[0];
                var range = _listItems[idx];

                var database = Utils.DecryptedConnectionString(_selectedConnection);
                if(range.Type == "U")
                {
                    using (var conn = new SqlConnection(database))
                    {
                        conn.Open();
                        var cmdTable = new SqlCommand(File.ReadAllText(Application.StartupPath + @"\get_create_table.sql"), conn);
                        cmdTable.CommandTimeout = 0;
                        cmdTable.Parameters.AddWithValue("table_name_ext", range.Name);
                        var tableRes = cmdTable.ExecuteScalar();
                        if (tableRes != null)
                        {
                            var str = tableRes.ToString();
                            range.Text = str;
                            fastColoredTextBox1.Text = str;
                        }


                        conn.Close();
                    }
                }
            }
        }

        private void refreshObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var idx = listView1.SelectedIndices[0];
                var range = _listItems[idx];

                var database = Utils.DecryptedConnectionString(_selectedConnection);
                using (var conn = new SqlConnection(database))
                {
                    conn.Open();
                    var cmdTable = new SqlCommand(File.ReadAllText(Application.StartupPath + @"\get_object.sql"), conn);
                    cmdTable.CommandTimeout = 0;
                    cmdTable.Parameters.AddWithValue("object_id", range.ObjectId);
                    var tableRes = cmdTable.ExecuteScalar();
                    if (tableRes != null)
                    {
                        var str = tableRes.ToString();
                        range.Text = str;
                        fastColoredTextBox1.Text = str;
                    }


                    conn.Close();
                }
            }
        }

        private void btnGenerateSqlFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var db = Utils.DecryptedConnectionString(_selectedConnection);
                var fileName = saveFileDialog1.FileName;
                Common.Async.ExecAsync(this, (b) => Generate(db, fileName, true), (tm) => ReportTime(tm), true);
            }
        }

        private void Generate(string database, string fileName, bool getTables)
        {
            var tables = new List<string>();
            using (var conn = new SqlConnection(database))
            {
                using (var stream = new FileStream(fileName, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {

                    conn.Open();
                    var cmd = new SqlCommand(File.ReadAllText(Application.StartupPath + @"\query.sql"), conn);
                    cmd.CommandTimeout = 0;
                    //var rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var tip = rdr.GetSqlString(1);
                        if (tip == "U")
                        {
                            tables.Add(rdr.GetSqlString(2).ToString());
                        }
                        else
                        {
                            var str = rdr.GetSqlString(0);
                            writer.Write(str);
                            writer.WriteLine();
                            writer.Write("GO");
                            writer.WriteLine();
                        }
                    }
                    rdr.Close();

                    foreach (var item in tables)
                    {
                        if (getTables)
                        {
                            var cmdTable = new SqlCommand(File.ReadAllText(Application.StartupPath + @"\get_create_table.sql"), conn);
                            cmdTable.CommandTimeout = 0;
                            cmdTable.Parameters.AddWithValue("table_name_ext", item);
                            var tableRes = cmdTable.ExecuteScalar();
                            if (tableRes != null)
                            {
                                var str = tableRes.ToString();

                                writer.Write(str);
                                writer.WriteLine();
                                writer.Write("GO");
                                writer.WriteLine();
                            }
                        }
                        else
                        {
                            var str = "CREATE TABLE " + item;

                            writer.Write(str);
                            writer.WriteLine();
                            writer.Write("GO");
                            writer.WriteLine();
                        }
                    }


                    conn.Close();
                }
            }
        }

        private void btnGenerateSqlFileNoTables_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var db = Utils.DecryptedConnectionString(_selectedConnection);
                var fileName = saveFileDialog1.FileName;
                Common.Async.ExecAsync(this, (b) => Generate(db, fileName, false), (tm) => ReportTime(tm), true);
            }
        }

        public void NotifyReadonlySql()
        {
            fastColoredTextBox1.ReadOnly = Config.ReadOnlySql;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(_sw.Elapsed >= TimeSpan.FromSeconds(5))
            {
                if(!string.IsNullOrEmpty(tbSearchNode.Text))
                {
                    var txt = tbSearchNode.Text;
                    if (!_autoComplete.Cast<string>().Contains(txt))
                    {
                        _autoComplete.Add(txt);
                        var tip = "";
                        foreach (var item in _autoComplete.Cast<string>())
                        {
                            tip += item + Environment.NewLine;
                        }
                        toolTip1.SetToolTip(tbSearchNode, tip);
                    }
                }
            }
        }

        
        public void NotifyAutocomplete()
        {
            timer1.Enabled = Config.Autocomplete;
        }

        public void NotifyReloadConnections()
        {
            RebuildServerTabs();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl != null)
            {
                var ctl = ActiveControl;
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

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl != null)
            {
                var ctl = ActiveControl;
                if (ctl is TextBox)
                {

                }
                if (ctl is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (ctl as FastColoredTextBoxNS.FastColoredTextBox).Redo();
                }
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl != null)
            {
                var ctl = ActiveControl;
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl != null)
            {
                var ctl = ActiveControl;
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

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl != null)
            {
                var ctl = ActiveControl;
                if (ctl is TextBox)
                {
                    (ctl as TextBox).Paste();
                }
                if (ctl is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (ctl as FastColoredTextBoxNS.FastColoredTextBox).Paste();
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl != null)
            {
                var ctl = ActiveControl;
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

        private void addConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lb = ((sender as ToolStripItem).Owner as ContextMenuStrip).SourceControl as ListBox;
            var f = new ConnectionDialog();
            if(f.ShowDialog() == DialogResult.OK)
            {
                var connStr = f.ConnectionString;
                
                var conns = new List<string>();
                foreach (var item in _serverTabs)
                {
                    foreach (var conn in item.Value)
                    {
                        conns.Add(conn);
                    }
                }
                conns.Add(connStr);

                File.WriteAllLines(Application.StartupPath + @"\connections.txt", conns.ToArray());

                var main = MdiParent as IMainForm;
                if (main != null)
                {
                    main.ConnectionsChanged();
                }

            }
        }

        private void testConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lb = ((sender as ToolStripItem).Owner as ContextMenuStrip).SourceControl as ListBox;
            if (lb.SelectedItem != null)
            {
                var f = new ConnectionDialog();
                f.ConnectionString = lb.SelectedItem.ToString();
                if(f.ShowDialog() == DialogResult.OK)
                {
                    var connStr = f.ConnectionString;

                    var conns = new List<string>();
                    foreach (var item in _serverTabs)
                    {
                        item.Value.Remove(lb.SelectedItem.ToString());
                        foreach (var conn in item.Value)
                        {
                            conns.Add(conn);
                        }
                    }
                    conns.Add(connStr);

                    File.WriteAllLines(Application.StartupPath + @"\connections.txt", conns.ToArray());

                    var main = MdiParent as IMainForm;
                    if (main != null)
                    {
                        main.ConnectionsChanged();
                    }
                }
            }
        }

        private void deleteConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lb = ((sender as ToolStripItem).Owner as ContextMenuStrip).SourceControl as ListBox;
            if (lb.SelectedItem != null &&
                MessageBox.Show(string.Format("Are you sure to delete [{0}] from connections list?", lb.SelectedItem.ToString()), "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3)
                == DialogResult.Yes)
            {

                var conns = new List<string>();
                foreach (var item in _serverTabs)
                {
                    item.Value.Remove(lb.SelectedItem.ToString());
                    foreach (var conn in item.Value)
                    {
                        conns.Add(conn);
                    }
                }
                
                File.WriteAllLines(Application.StartupPath + @"\connections.txt", conns.ToArray());

                var main = MdiParent as IMainForm;
                if (main != null)
                {
                    main.ConnectionsChanged();
                }
            }
        }

        private void lbSqlDatabases_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;


            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.LightSkyBlue);//Choose the color

            var item = (sender as ListBox).Items[e.Index].ToString();

            var conStr = new SqlConnectionStringBuilder(item);

            e.DrawBackground();
            
            e.Graphics.DrawString(conStr.DataSource + "." + conStr.InitialCatalog + (conStr.IntegratedSecurity ? " [win]" : " [sql]"), e.Font, Brushes.Black, e.Bounds);

            e.DrawFocusRectangle();

        }

        private void lbSqlDatabases_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (sender as ListBox).Font.Height;
        }

        private void findUsagesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fastColoredTextBox1.SelectedText))
                FindUsage(EscapeRegex(fastColoredTextBox1.SelectedText));
        }

        private string EscapeRegex(string txt)
        {
            if(Config.FindUsageRegex)
            {
                return txt;
            }
            else
            {
                return Regex.Escape(txt);
            }
        }

        private void FindUsageInCurrentSearch(string regex)
        {
            var objects = new List<SqlDdlObject>(_listItemsCache2);
            FindUsage(regex, objects);
        }

        string _lastSearch = "";
        Dictionary<int, List<Range>> _ls = new Dictionary<int, List<Range>>();
        //TODO
        private void FindUsage(string regex, List<SqlDdlObject> objectsToSearch = null)
        {
            bool findInFound = objectsToSearch != null;
            if(objectsToSearch == null)
            {
                objectsToSearch = _listItemsCache;
            }

            var result = new List<Range>();
            var res = new Dictionary<string, Range>();
            Common.Async.ExecAsync(this, (b) =>
            {
                try
                {
                    fastColoredTextBox1.VisibleRangeChangedDelayed -= fastColoredTextBox1_VisibleRangeChangedDelayed;

                    

                    var rx = new Regex(regex, RegexOptions.IgnoreCase);

                    _ls.Clear();
                    
                    foreach (var item in objectsToSearch)
                    {
                        var resultOut = new List<Range>();

                        var matches = rx.Matches(item.Text);
                        foreach (Match match in matches)
                        {
                            Group g = match.Groups[0];
                            var lineNumber = item.Text.Take(g.Index).Count(c => c == '\n')/* + 1*/;//https://stackoverflow.com/questions/7255743/what-is-simpliest-way-to-get-line-number-from-char-position-in-string

                            var item1 = Syncronized(() => new Range(fastColoredTextBox1, g.Index, lineNumber, g.Index + g.Length, lineNumber));
                            Syncronized(() => resultOut.Add(item1));

                        }
                        if(resultOut.Count > 0)
                            _ls.Add(item.ObjectId, resultOut);

                        /*
                        
                        Syncronized(() => fastColoredTextBox1.Text = item.Text);

                        var tb = Syncronized(() => fastColoredTextBox1);


                        ClearFoundRanges();
                        var range = Syncronized(() => fastColoredTextBox1.Selection.Clone());
                        range.Normalize();

                        range.Start = new Place(0, 0);
                        range.End = Syncronized(() => new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1));
                        var result1 = range.GetRangesByLines(regex, RegexOptions.IgnoreCase);

                        int i = 0;

                        foreach (var item1 in result1)
                        {
                            //if (worker != null && worker.CancellationPending)
                            //    break;

                            Syncronized(() => resultOut.Add(item1));
                            i++;
                        }

                        ls.Add(item.ObjectId, resultOut);
                        //*/
                        //return resultOut;
                    }

                    _listItems2.Clear();
                    _listItemsCache2.Clear();
                    foreach (var item in _ls)
                    {
                        _listItems2.Add(objectsToSearch.First((o) => o.ObjectId == item.Key));
                        _listItemsCache2.Add(objectsToSearch.First((o) => o.ObjectId == item.Key));
                    }
                    //============================================================
                    var dicKey = "";
                    if (findInFound)
                    {
                        dicKey = $"{regex} in [{ _lastSearch }]";
                    }
                    else
                    {
                        dicKey = regex;
                    }
                    _lastSearch = regex;

                    Syncronized(() =>
                    {
                        var rb = new RadioButton();
                        rb.Text = dicKey;    
                        rb.AutoSize = true;
                        //rb.Checked = true;
                        rb.Click += (s, arg) =>
                        {
                            _listItems2.Clear();
                            _listItemsCache2.Clear();

                            //ClearFoundRanges();
                            //_needRebuild2 = true;

                            findUsagesInFoundToolStripMenuItem.Text = $"Find Usages in [{((Control)s).Text}] search";
                            findUsagesInFoundanyTextToolStripMenuItem.Text = $"Find Usages in [{((Control)s).Text}] search (any text)...";

                            _listItems2.AddRange(_listItemsDic[((Control)s).Text]);
                            _listItemsCache2.AddRange(_listItemsDic[((Control)s).Text]);

                            _ls.Clear();
                            foreach (var item in _lsDic[((Control)s).Text])
                            {
                                _ls.Add(item.Key, item.Value);
                            }

                            listView2.VirtualListSize = _listItems2.Count;
                            listView2.SelectedIndices.Clear();
                        };

                        flowLayoutPanel1.Controls.Add(rb);

                        findUsagesInFoundToolStripMenuItem.Text = $"Find Usages in [{dicKey}] search";
                        findUsagesInFoundToolStripMenuItem.Enabled = true;

                        findUsagesInFoundanyTextToolStripMenuItem.Text = $"Find Usages in [{dicKey}] search (any text)...";
                        findUsagesInFoundanyTextToolStripMenuItem.Enabled = true;

                        if (_listItemsDic.ContainsKey(dicKey))
                        {
                            _listItemsDic[dicKey].Clear();
                            _listItemsDic[dicKey].AddRange(new List<SqlDdlObject>(_listItems2));
                        }
                        else
                        {
                            _listItemsDic.Add(dicKey, new List<SqlDdlObject>(_listItems2));
                        }

                        if (_lsDic.ContainsKey(dicKey))
                        {
                            _lsDic[dicKey].Clear();
                            foreach (var item in _ls)
                            {
                                _lsDic[dicKey].Add(item.Key, item.Value);
                            }
                        }
                        else
                        {
                            _lsDic.Add(dicKey, new Dictionary<int, List<Range>>());
                            foreach (var item in _ls)
                            {
                                _lsDic[dicKey].Add(item.Key, item.Value);
                            }
                        }

                    });
                    //============================================================

                    Syncronized(() => listView2.VirtualListSize = _listItems2.Count);
                    Syncronized(() => listView2.SelectedIndices.Clear());
                    
                }
                catch { throw; }
                finally
                {
                    fastColoredTextBox1.VisibleRangeChangedDelayed += fastColoredTextBox1_VisibleRangeChangedDelayed;
                }

            }, (tm) => ReportTime(tm), true);
        }

        List<ListViewItem> _cache2 = new List<ListViewItem>();
        List<SqlDdlObject> _listItems2 = new List<SqlDdlObject>();
        List<SqlDdlObject> _listItemsCache2 = new List<SqlDdlObject>();
        private void listView2_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            //if (_needRebuild)
            {
                _cache2.Clear();
                foreach (var item in _listItems2)
                {
                    _cache2.Add(new ListViewItem(item.Name + "   [" + item.TypeDesc + "]"));
                }
                //_needRebuild = false;
            }
        }

        private void listView2_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (_cache2.Count > e.ItemIndex && e.ItemIndex >= 0)
                e.Item = _cache2[e.ItemIndex];
            else
                e.Item = new ListViewItem();
        }

        private void listView2_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            for (int i = 0; i < _listItems2.Count; i++)
            {
                var item = _listItems2[i];
                if (item.Text.Contains(e.Text))
                {
                    e.Index = i;
                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedIndices.Count > 0)
            {
                var idx = listView2.SelectedIndices[0];
                var range = _listItems2[idx];

                _foundRanges.Clear();
                if (_ls.ContainsKey(range.ObjectId))
                    _foundRanges.AddRange(_ls[range.ObjectId]);

                fastColoredTextBox1.Text = range.Text;
                fastColoredTextBox1.DoSelectionVisible();

            }
        }

        private void fastColoredTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Modifiers == Keys.Control && e.KeyCode == Keys.Down && _foundRanges != null && _foundRanges.Count > 0)
            {
                var range = fastColoredTextBox1.Selection;
                foreach (var item in _foundRanges)
                {
                    if(item.Start.iLine > range.Start.iLine)
                    {
                        fastColoredTextBox1.Selection = new Range(fastColoredTextBox1, item.Start.iLine);
                        fastColoredTextBox1.DoCaretVisible();
                        break;
                    }
                }

                e.Handled = true;
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Up && _foundRanges != null && _foundRanges.Count > 0)
            {
                var range = fastColoredTextBox1.Selection;
                for (int i = _foundRanges.Count - 1; i >= 0; i--)
                {
                    var item = _foundRanges[i];
                    if (item.Start.iLine < range.Start.iLine)
                    {
                        fastColoredTextBox1.Selection = new Range(fastColoredTextBox1, item.Start.iLine);
                        fastColoredTextBox1.DoCaretVisible();
                        break;
                    }
                }

                e.Handled = true;
            }
        }

        Dictionary<string, List<SqlDdlObject>> _listItemsDic = new Dictionary<string, List<SqlDdlObject>>();
        Dictionary<string, Dictionary<int, List<Range>>> _lsDic = new Dictionary<string, Dictionary<int, List<Range>>>();

        private void btnClearSearches_Click(object sender, EventArgs e)
        {
            _lsDic.Clear();
            _listItemsDic.Clear();

            flowLayoutPanel1.Controls.Clear();
        }

        private void findUsagesInFoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fastColoredTextBox1.SelectedText))
                FindUsageInCurrentSearch(EscapeRegex(fastColoredTextBox1.SelectedText));
        }

        private void findUsagesanyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var str = Microsoft.VisualBasic.Interaction.InputBox("Enter some text to search for", "Search string", "");
            if(!string.IsNullOrEmpty(str))
            {
                FindUsage(EscapeRegex(str));
            }
        }

        private void findUsagesInFoundanyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var str = Microsoft.VisualBasic.Interaction.InputBox("Enter some text to search for", "Search string", "");
            if (!string.IsNullOrEmpty(str))
            {
                FindUsageInCurrentSearch(EscapeRegex(str));
            }
        }

        private void btnAssemblyExporter_Click(object sender, EventArgs e)
        {
            var f = new AssemblyExporterForm(Utils.DecryptedConnectionString(_selectedConnection));
            f.ShowDialog();
        }


        private void lbSqlDatabases_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
