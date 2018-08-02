using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class Form1 : Form, IChildForm
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
        public Form1()
        {
            InitializeComponent();

            timer1.Enabled = Config.Autocomplete;
            tbSearchNode.AutoCompleteCustomSource = _autoComplete;

            fastColoredTextBox1.ReadOnly = Config.ReadOnlySql;

            var fields = typeof(RegexValues).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            listBox1.Items.AddRange(fields);

            _sync = SynchronizationContext.Current;
            TextModified = false;

            
        }

        public event EventHandler<string> OnTextModified;
        public event EventHandler<TimeSpan> OnAsyncCompleted;

        void BuildTreeAsync(string regex)
        {
            Common.Async.ExecAsync(this, (b) => BuildTree(regex, b), (tm) => ReportTime(tm), true);
        }

        private void ReportTime(TimeSpan tm)
        {
            if (OnAsyncCompleted != null)
                OnAsyncCompleted(null, tm);
        }

        void BuildTreeAsync2(string regex)
        {
            Common.Async.ExecAsync(this, (b) => BuildTree2(regex, b), (tm) => ReportTime(tm), true);
        }

        public string FileName { get; private set; }
        internal void LoadFile(string fileName, bool createWatcher = false, EncodingInfo encoding = null)
        {
            var fi2 = new FileInfo(fileName);
            Text = fi2.Name;
            FileName = fileName;

            Syncronized(()=>fastColoredTextBox1.TextChanged -= fastColoredTextBox1_TextChanged);

            if (Config.UseLargeFiles)
            {
                Syncronized(() => fastColoredTextBox1.OpenBindingFile(fileName, encoding == null ? null : encoding.GetEncoding()));
            }
            else
            {
                if(encoding == null)
                {
                    Syncronized(() => fastColoredTextBox1.OpenFile(fileName));
                }
                else
                {
                    Syncronized(() => fastColoredTextBox1.OpenFile(fileName, encoding.GetEncoding()));
                }
                
            }
            
            Syncronized(() => fastColoredTextBox1.TextChanged += fastColoredTextBox1_TextChanged);

            
            Syncronized(() => tbRegExp.Text = @"(?i-msnx:\b(?<!-{2,}.*)go[^a-zA-Z])");
            Syncronized(() => tbRegExp.Text = RegexValues.DdlObjectsPreparedWithIndex);

            if (Config.RegexOnLoad)
            {
                BuildTree(RegexValues.DdlObjectsPreparedWithIndex, null);
            }
            
            var fi = new FileInfo(fileName);

            CreateFileWatcher(fi.DirectoryName);
            
        }
        FileSystemWatcher _watcher;
        public void CreateFileWatcher(string path)
        {
            var tr = new Thread(() =>
            {
                if (_watcher != null)
                {
                    _watcher.EnableRaisingEvents = false;
                    _watcher.Changed -= OnChanged;
                    _watcher.Dispose();
                    _watcher = null;
                }
                
                _watcher = new FileSystemWatcher();
                _watcher.Path = path;
                
                _watcher.NotifyFilter =
                    
                    NotifyFilters.LastWrite; 
                _watcher.Filter = "*.sql";

                

                _watcher.Changed += new FileSystemEventHandler(OnChanged);
                
                _watcher.EnableRaisingEvents = true;
            });

            tr.Name = "fileWatcher{" + Guid.NewGuid().ToString() + "}";
            tr.IsBackground = true;
            tr.Start();
        }

        List<string> _changedFiles = new List<string>();
        DateTime _lastFileChangedDate = DateTime.MinValue;
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (e.FullPath == this.FileName
                && e.ChangeType == WatcherChangeTypes.Changed
                && DateTime.Now - _lastFileChangedDate > TimeSpan.FromSeconds(5)
                )
            {
                
                _lastFileChangedDate = DateTime.Now;
                Syncronized(() =>{
                    if (MessageBox.Show("file " + e.Name + " changed, Reload?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                    {

                        LoadFile(FileName);
                    }
                });
                
            }
            
        }

        
        private void BuildTree(string regexStr, BackgroundWorker worker)
        {
            var tb = Syncronized(()=> fastColoredTextBox1);
            
            ClearFoundRanges();
            Syncronized(() => _listItems.Clear());
            Syncronized(() => _listItemsCache.Clear());


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

                Syncronized(() => _listItems.Add(item));
                Syncronized(() => _listItemsCache.Add(item));

                i++;
            }

            _needRebuild = true;
            Syncronized(()=> listView1.VirtualListSize = _listItems.Count);
            Syncronized(() => listView1.SelectedIndices.Clear());
            Syncronized(() => lblRegexCntFound.Text = "Found " + i.ToString() + " matches");
        }

        private void BuildTree2(string regexStr, BackgroundWorker worker)
        {
            var tb = Syncronized(() => fastColoredTextBox1);


            ClearFoundRanges();
            Syncronized(() => _listItems2.Clear());
            Syncronized(() => _listItemsCache2.Clear());


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

                Syncronized(() => _listItems2.Add(item));
                Syncronized(() => _listItemsCache2.Add(item));

                i++;
            }

            _needRebuild2 = true;
            Syncronized(() => listView2.VirtualListSize = _listItems2.Count);
            Syncronized(() => listView2.SelectedIndices.Clear());
            Syncronized(() => lblRegexCntFound2.Text = "Found " + i.ToString() + " matches");
        }

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
            OpenViewer();
        }

        private void OpenViewer()
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var range = _listItems[listView1.SelectedIndices[0]];
                var tb = fastColoredTextBox1;

                var placeEnd = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
                var rng = new Range(tb, range.Start, placeEnd);
                rng.Normalize();
                

                int lineEnd = placeEnd.iLine;
                for (int i = range.Start.iLine; i < tb.Lines.Count; i++)
                {
                    if (tb.Lines[i].Trim().ToLower() == "go")
                    {
                        lineEnd = i;
                        break;
                    }
                }

                var plEnd = new Place(0, lineEnd);


                int lineStart = range.Start.iLine;

                if (cbFullProcText.Checked)
                {
                    for (int i = range.Start.iLine; i > 0; i--)
                    {
                        if (tb.Lines[i].Trim().ToLower() == "go")
                        {
                            lineStart = i;
                            break;
                        }
                    }
                }
                var plStart = new Place(0, lineStart);

                var sqlText = new Range(tb, plStart, plEnd).Text;
                var f = new ObjectViewerForm();
                f.SetSqlText(sqlText, range.Start, plEnd);
                f.SetBookMarks(range.Start, tb.Bookmarks);
                f.SetHighlightRange(range.Start, plEnd, _foundRanges);
                f.SetVariables(_variables);
                f.ShowDialog();

                if(f.IsSaved)
                {
                    tb.Selection.Start = f.Start;
                    tb.Selection.End = f.End;
                    tb.InsertText(f.SqlText);
                    
                }

                

            }
            return;
        }

        private void OpenViewer2()
        {
            if (listView2.SelectedIndices.Count > 0)
            {
                var range = _listItems2[listView2.SelectedIndices[0]];
                var tb = fastColoredTextBox1;

                var placeEnd = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
                var rng = new Range(tb, range.Start, placeEnd);
                rng.Normalize();
                
                int lineEnd = placeEnd.iLine;
                for (int i = range.Start.iLine; i < tb.Lines.Count; i++)
                {
                    if (tb.Lines[i].Trim().ToLower() == "go")
                    {
                        lineEnd = i;
                        break;
                    }
                }

                var plEnd = new Place(0, lineEnd);


                int lineStart = range.Start.iLine;

                if (cbFullProcText.Checked)
                {
                    for (int i = range.Start.iLine; i > 0; i--)
                    {
                        if (tb.Lines[i].Trim().ToLower() == "go")
                        {
                            lineStart = i;
                            break;
                        }
                    }
                }
                var plStart = new Place(0, lineStart);

                var sqlText = new Range(tb, plStart, plEnd).Text;
                var f = new ObjectViewerForm();
                f.SetSqlText(sqlText, range.Start, plEnd);
                f.SetBookMarks(range.Start, tb.Bookmarks);
                f.SetHighlightRange(range.Start, plEnd, _foundRanges);
                f.SetVariables(_variables);
                f.ShowDialog();

                if (f.IsSaved)
                {
                    tb.Selection.Start = f.Start;
                    tb.Selection.End = f.End;
                    tb.InsertText(f.SqlText);
                    
                }

                
            }
            return;
        }

        List<Range> _listItems = new List<Range>();
        List<Range> _listItemsCache = new List<Range>();

        List<Range> _listItems2 = new List<Range>();
        List<Range> _listItemsCache2 = new List<Range>();

        Dictionary<string, List<Range>> _listItemsDic = new Dictionary<string, List<Range>>();
        Dictionary<string, List<Range>> _foundRangesDic = new Dictionary<string, List<Range>>();

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
                Range[] ranges = _listItemsCache.Where(r => r.Text.ToLower().Contains(tbSearchNode.Text.ToLower())).ToArray();

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

        

        void DisposeWatcher()
        {
            if(_watcher != null)
            {
                _watcher.Dispose();
                _watcher = null;
            }
        }
        public void SaveFile()
        {
            DisposeWatcher();
            
            File.WriteAllText(FileName, SqlText, _encoding);
            
            var fi = new FileInfo(FileName);
            Text = fi.Name;
            TextModified = false;
            OnTextModified(this, fi.Name);

            CreateFileWatcher(fi.DirectoryName);
        }

       
        
        public bool TextModified { get; private set; }
        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextModified = true;
            if (!string.IsNullOrEmpty(FileName))
            {
                var fi = new FileInfo(FileName);
                Text = "*" + fi.Name;
                if (OnTextModified != null)
                    OnTextModified(this, Text);
            }
        }

        private void copyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var str = "";
            foreach (var item in _listItems)
            {
                str += item.Text + Environment.NewLine;
            }

            Clipboard.SetText(str);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
                _watcher = null;
            }
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
                    _cache.Add(new ListViewItem(item.Text));
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

                
                
                fastColoredTextBox1.Selection = range;
                fastColoredTextBox1.DoSelectionVisible();
                
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenViewer();
        }
        
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenViewer();
            }
        }

        private void tbRegExp_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                BuildTreeAsync(tbRegExp.Text);
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
        

        private void button1_Click_1(object sender, EventArgs e)
        {
            tbRegExp.Text = RegexValues.DdlObjectsPreparedWithIndex;
            BuildTreeAsync(RegexValues.DdlObjectsPreparedWithIndex);
        }

        private void showObjectDependenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       

        private void tbRegExp2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuildTreeAsync2(tbRegExp2.Text);
            }
        }

        List<ListViewItem> _cache2 = new List<ListViewItem>();
        bool _needRebuild2;
        private void listView2_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            if(_needRebuild2)
            {
                _cache2.Clear();
                foreach (var item in _listItems2)
                {
                    _cache2.Add(new ListViewItem(item.Text));
                }
                _needRebuild2 = false;
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

                
                
                fastColoredTextBox1.Selection = range;
                fastColoredTextBox1.DoSelectionVisible();

            }
        }

        private string EscapeRegex(string txt)
        {
            if (Config.FindUsageRegex)
            {
                return txt;
            }
            else
            {
                return Regex.Escape(txt);
            }
        }

        private void findUsagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(fastColoredTextBox1.SelectedText))
            FindUsage(EscapeRegex(fastColoredTextBox1.SelectedText));
        }

        List<Range> _foundRanges = new List<Range>();
        private void FindUsage(string regex)
        {
            

            var result = new List<Range>();
            var res = new Dictionary<string, Range>();
            Common.Async.ExecAsync(this, (b) =>
            {
                try
                {
                    fastColoredTextBox1.VisibleRangeChangedDelayed -= fastColoredTextBox1_VisibleRangeChangedDelayed;

                    result = BuildTree3(regex, b);

                    var data = _listItemsCache;
                    if (data.Count > 0)
                    {
                        foreach (var item2 in result)
                        {
                            if (b != null && b.CancellationPending)
                                break;

                            _foundRanges.Add(item2);
                            if (Config.UseLargeFiles)
                            {
                                var rng = fastColoredTextBox1.GetLine(item2.Start.iLine);

                                var hl = fastColoredTextBox1.SyntaxHighlighter;

                                rng.ClearAllStyle();
                                rng.SetStyle(new TextStyle(Brushes.Black, Brushes.Yellow, FontStyle.Regular));

                                item2.ClearAllStyle();
                                item2.SetStyle(new TextStyle(Brushes.Black, Brushes.Orange, FontStyle.Bold));
                            }


                            for (int i = 0; i < data.Count - 1; i++)
                            {
                                var r1 = data[i];
                                var r2 = data[i + 1];

                                if (item2.Start >= r1.Start && item2.End <= r2.Start)
                                {
                                    //not include duplicate range
                                    if (!res.ContainsKey(data[i].Text + data[i].Start.iLine.ToString()))
                                        res.Add(data[i].Text + data[i].Start.iLine.ToString(), data[i]);
                                    //if (!res.Exists((a) => a.Text == data[i].Text && a.Start.iLine == data[i].Start.iLine))
                                    //    res.Add(data[i]);
                                }
                            }

                            if (item2.Start >= data[data.Count - 1].Start)
                            {
                                //not include duplicate range
                                if (!res.ContainsKey(data[data.Count - 1].Text + data[data.Count - 1].Start.iLine.ToString()))
                                    res.Add(data[data.Count - 1].Text + data[data.Count - 1].Start.iLine.ToString(), data[data.Count - 1]);
                                //if (!res.Exists((a) => a.Text == data[data.Count - 1].Text && a.Start.iLine == data[data.Count - 1].Start.iLine))
                                //    res.Add(data[data.Count - 1]);
                            }

                        }
                        
                        Syncronized(() => _listItems2.Clear());
                        Syncronized(() => _listItemsCache2.Clear());

                        foreach (var item in res)
                        {
                            Syncronized(() => _listItems2.Add(item.Value));
                            Syncronized(() => _listItemsCache2.Add(item.Value));
                        }

                        //---------------------------------------------------
                        Syncronized(() =>
                        {
                            var rb = new RadioButton();
                            rb.Text = regex;
                            rb.AutoSize = true;
                            rb.Click += (s, arg) =>
                            {
                                _listItems2.Clear();
                                _listItemsCache2.Clear();

                                ClearFoundRanges();
                                _needRebuild2 = true;

                                _listItems2.AddRange(_listItemsDic[((Control)s).Text]);
                                _listItemsCache2.AddRange(_listItemsDic[((Control)s).Text]);
                                _foundRanges.AddRange(_foundRangesDic[((Control)s).Text]);

                                listView2.VirtualListSize = _listItems2.Count;
                                listView2.SelectedIndices.Clear();
                            };

                            flowLayoutPanel1.Controls.Add(rb);

                            if (_listItemsDic.ContainsKey(regex))
                            {
                                _listItemsDic[regex].Clear();
                                _listItemsDic[regex].AddRange(new List<Range>(_listItems2));
                            }
                            else
                            {
                                _listItemsDic.Add(regex, new List<Range>(_listItems2));
                            }

                            if (_foundRangesDic.ContainsKey(regex))
                            {
                                _foundRangesDic[regex].Clear();
                                _foundRangesDic[regex].AddRange(new List<Range>(_foundRanges));
                            }
                            else
                            {
                                _foundRangesDic.Add(regex, new List<Range>(_foundRanges));
                            }

                        });
                        //---------------------------------------------------

                        _needRebuild2 = true;
                        Syncronized(() => listView2.VirtualListSize = _listItems2.Count);
                        Syncronized(() => listView2.SelectedIndices.Clear());
                        Syncronized(() => lblRegexCntFound2.Text = "Found " + _listItemsCache2.Count.ToString() + " usages of " + regex);
                    }
                }
                catch { throw; }
                finally
                {
                    fastColoredTextBox1.VisibleRangeChangedDelayed += fastColoredTextBox1_VisibleRangeChangedDelayed;
                }

            }, (tm) => ReportTime(tm), true);
        }

        private void findUsageswholeWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fastColoredTextBox1.SelectedText))
                FindUsage(@"\b" + EscapeRegex(fastColoredTextBox1.SelectedText) + @"\b");
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

        private void btnBookMarks_Click(object sender, EventArgs e)
        {
            Point screenPoint = btnBookMarks.PointToScreen(new Point(btnBookMarks.Left, btnBookMarks.Bottom));
            if (screenPoint.Y + miBookmarks.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                miBookmarks.Show(btnBookMarks, new Point(0, -miBookmarks.Size.Height));
            }
            else
            {
                miBookmarks.Show(btnBookMarks, new Point(0, btnBookMarks.Height));
            }
        }

        private void miBookmarks_Opening(object sender, CancelEventArgs e)
        {
            miBookmarks.Items.Clear();
            foreach (var bookmark in fastColoredTextBox1.Bookmarks)
            {
                var item = miBookmarks.Items.Add(bookmark.Name);
                item.Tag = bookmark;
                item.Click += (o, a) => ((Bookmark)(o as ToolStripItem).Tag).DoVisible();
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenViewer2();
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
                foreach (var item2 in _foundRanges)
                {
                    if (item2.Start.iLine > minLine && item2.End.iLine < maxLine)
                    {
                        var rng = fastColoredTextBox1.GetLine(item2.Start.iLine);

                        var hl = fastColoredTextBox1.SyntaxHighlighter;

                        rng.ClearAllStyle();
                        rng.SetStyle(new TextStyle(Brushes.Black, Brushes.Yellow, FontStyle.Regular));

                        item2.ClearAllStyle();
                        item2.SetStyle(new TextStyle(Brushes.Black, Brushes.Orange, FontStyle.Bold));
                    }
                }
                
            }
        }

        private void documentMap1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var fi = listBox1.SelectedItem as FieldInfo;

                var regexVal = fi.GetValue(null).ToString();
                textBox1.Text = regexVal;
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                var fi = listBox1.SelectedItem as FieldInfo;

                var regexVal = fi.GetValue(null).ToString();
                tbRegExp.Text = regexVal;
                BuildTreeAsync(regexVal);
            }
        }

        private void tbSearchNode2_TextChanged(object sender, EventArgs e)
        {
            if (tbSearchNode2.Text == "")
            {
                _listItems2.Clear();
                _listItems2.AddRange(_listItemsCache2);

                _needRebuild2 = true;
                listView2.VirtualListSize = _listItems2.Count;
                listView2.SelectedIndices.Clear();
            }
            else
            {
                _listItems2.Clear();
                Range[] ranges = _listItemsCache2.Where(r => r.Text.ToLower().Contains(tbSearchNode2.Text.ToLower())).ToArray();

                _listItems2.AddRange(ranges);
                _needRebuild2 = true;
                listView2.VirtualListSize = _listItems2.Count;
                listView2.SelectedIndices.Clear();
            }
        }

        public void SaveFile(string fileName, Encoding enc)
        {
            fastColoredTextBox1.SaveToFile(fileName, enc);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var str = "";
            foreach (var item in _listItems2)
            {
                str += item.Text + Environment.NewLine;
            }

            Clipboard.SetText(str);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var dt = DateTime.FromOADate(43279.000000000000);

            return;
            var rb = new RadioButton();
            rb.Text = "test";
            flowLayoutPanel1.Controls.Add(rb);
        }

        private void btnClearSearches_Click(object sender, EventArgs e)
        {
            _foundRangesDic.Clear();
            _listItemsDic.Clear();

            flowLayoutPanel1.Controls.Clear();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        public void NotifyReadonlySql()
        {
            fastColoredTextBox1.ReadOnly = Config.ReadOnlySql;
        }

        public void NotifyAutocomplete()
        {
            timer1.Enabled = Config.Autocomplete;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_sw.Elapsed >= TimeSpan.FromSeconds(5))
            {
                if (!string.IsNullOrEmpty(tbSearchNode.Text))
                {
                    var txt = tbSearchNode.Text;
                    if (!_autoComplete.Cast<string>().Contains(txt))
                    {
                        _autoComplete.Add(txt);
                        var tip = "";
                        foreach(var item in _autoComplete.Cast<string>())
                        {
                            tip += item + Environment.NewLine;
                        }
                        toolTip1.SetToolTip(tbSearchNode, tip);
                    }
                }
            }
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

        private void fastColoredTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Down && _foundRanges != null && _foundRanges.Count > 0)
            {
                var range = fastColoredTextBox1.Selection;
                foreach (var item in _foundRanges)
                {
                    if (item.Start.iLine > range.Start.iLine)
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
    }
}
