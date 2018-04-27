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
        public SqlViewerForm()
        {
            InitializeComponent();

            var conns = File.ReadAllLines(Application.StartupPath + @"\connections.txt");
            lbSqlDatabases.Items.AddRange(conns);
            lbSqlDatabases.SelectedIndex = 0;

           
            _sync = SynchronizationContext.Current;
            TextModified = false;
            
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

     

        private void tbSearchNode_TextChanged(object sender, EventArgs e)
        {
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
           
            File.WriteAllText(FileName, SqlText, _encoding);
            
            var fi = new FileInfo(FileName);
            Text = fi.Name;
            TextModified = false;
            OnTextModified(this, fi.Name);

            
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
            if (Config.UseLargeFiles)
            {
                var args = new TextChangedEventArgs(fastColoredTextBox1.VisibleRange);
                
                fastColoredTextBox1.OnSyntaxHighlight(args);

                var diff = fastColoredTextBox1.VisibleRange.End.iLine - fastColoredTextBox1.VisibleRange.Start.iLine;
                int minLine = fastColoredTextBox1.VisibleRange.Start.iLine - diff * 4;
                int maxLine = fastColoredTextBox1.VisibleRange.End.iLine + diff * 4;
                
                
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
            var db = lbSqlDatabases.SelectedItem.ToString();
            Common.Async.ExecAsync(this, (b) => BuildSqlObjects(db, b), (tm) => ReportTime(tm), true);
        }

        private void BuildSqlObjects(string database, BackgroundWorker worker)
        {
            var result = Generate(database);

            Syncronized(() => _listItems.Clear());
            Syncronized(() => _listItemsCache.Clear());

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
            
        }

        private void lbSqlDatabases_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbSqlDatabases.SelectedItem != null)
            {
                var db = lbSqlDatabases.SelectedItem.ToString();
                Common.Async.ExecAsync(this, (b) => BuildSqlObjects(db, b), (tm) => ReportTime(tm), true);
            }
        }

        private void getCREATETABLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var idx = listView1.SelectedIndices[0];
                var range = _listItems[idx];

                var database = lbSqlDatabases.SelectedItem.ToString();
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

                var database = lbSqlDatabases.SelectedItem.ToString();
                
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
                var db = lbSqlDatabases.SelectedItem.ToString();
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
                var db = lbSqlDatabases.SelectedItem.ToString();
                var fileName = saveFileDialog1.FileName;
                Common.Async.ExecAsync(this, (b) => Generate(db, fileName, false), (tm) => ReportTime(tm), true);
            }
        }
    }
}
