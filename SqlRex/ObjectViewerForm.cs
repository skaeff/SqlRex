
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public partial class ObjectViewerForm : Form
    {
        Dictionary<string, string> _variables = new Dictionary<string, string>();
        public ObjectViewerForm()
        {
            InitializeComponent();
        }

        public void SetVariables(Dictionary<string, string> vars)
        {
            _variables.Clear();
            foreach (var item in vars)
            {
                _variables.Add(item.Key, item.Value);
            }
        }

        public Place Start { get; private set; }
        public Place End { get; private set; }
        public string SqlText { get; private set; }
        public bool IsSaved { get; private set; }
        internal void SetSqlText(string sqlText, Place start, Place end)
        {
            SqlText = sqlText;
            fastColoredTextBox1.Text = sqlText;
            Start = start;
            End = end;
        }

        public void SetBookMarks(Place start, BaseBookmarks marks)
        {
            foreach (var item in marks)
            {
                fastColoredTextBox1.Bookmarks.Add(item.LineIndex - start.iLine);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string _prevText="";
        private void btnReplace_Click(object sender, EventArgs e)
        {
            _prevText = fastColoredTextBox1.Text;
            while (Utils.ReplaceAll(fastColoredTextBox1, _variables)) { }
                       
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
            SqlText = fastColoredTextBox1.Text;
            IsSaved = true;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ActiveControl != null)
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

        private void btnDiff_Click(object sender, EventArgs e)
        {
            Diff();
        }

        private void Diff()
        {
            var tempPath = Application.StartupPath + @"\temp";
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            var file1 = tempPath + @"\1";
            var file2 = tempPath + @"\2";
            File.WriteAllText(file1, _prevText);
            File.WriteAllText(file2, fastColoredTextBox1.Text);

            var ps = new Process();
            ps.StartInfo = new ProcessStartInfo("TortoiseMerge", " /base:\"" + file1 + "\" /mine:\"" + file2 + "\"");
            ps.Start();
        }

        private void diffWithPrevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Diff();
        }

        List<Range> _foundRanges = new List<Range>();
        internal void SetHighlightRange(Place start, Place end, List<Range> foundRanges)
        {
            _foundRanges.Clear();
            
            foreach (var item in foundRanges)
            {
                if (item.Start.iLine > start.iLine && item.End.iLine < end.iLine)
                {
                    var rng = fastColoredTextBox1.GetLine(item.Start.iLine - start.iLine);
                    var hl = fastColoredTextBox1.SyntaxHighlighter;

                    rng.ClearAllStyle();
                    rng.SetStyle(new TextStyle(Brushes.Black, Brushes.Yellow, FontStyle.Regular));

                    item.ClearAllStyle();
                    item.SetStyle(new TextStyle(Brushes.Black, Brushes.Orange, FontStyle.Bold));

                    _foundRanges.Add(rng);
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
