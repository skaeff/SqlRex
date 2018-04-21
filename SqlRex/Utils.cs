using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public class Utils
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        public static bool ReplaceAll(FastColoredTextBox tb, Dictionary<string, string> variables)
        {
            if (variables.Count == 0)
                return false;
            
            var txt = tb.Text;
            var regex = new Regex(RegexValues.SqlCmdObjectsShort, RegexOptions.IgnoreCase);
            List<string> objects = new List<string>();
            foreach (Match item in regex.Matches(txt))
            {
                if (!objects.Contains(item.Value))
                    objects.Add(item.Value);
            }

            foreach (var item in objects)
            {
                if (variables.ContainsKey(item))
                {
                    txt = txt.Replace(item, variables[item]);
                }
            }

            tb.Text = txt;
            tb.Selection.Start = new Place(0, 0);


            return false;

            
        }

        public static bool ReplaceAll_(FastColoredTextBox tb, Dictionary<string, string> variables)
        {
            if (variables.Count == 0)
                return false;


            //************************
            Range range = tb.Selection.Clone();
            range.Normalize();

            //
            range.Start = new Place(0, 0);
            range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);

            var ranges = range.GetRangesByLines(RegexValues.SqlCmdObjectsShort, RegexOptions.IgnoreCase);
            
            var lsRanges = new List<Range>();
            foreach (var r in ranges)
            {
                lsRanges.Add(r);
            }

            //******
            var style = new TextStyle(System.Drawing.Brushes.Black, System.Drawing.Brushes.LightGreen, System.Drawing.FontStyle.Bold);

            foreach (var rng in lsRanges)
            {
                tb.Selection = rng;
                //tb.Selection.SetStyle(style);
                var st = rng.Text;
                if (variables.ContainsKey(st))
                {
                    tb.InsertText(variables[st], style);
                }
            }

            //check next
            range = tb.Selection.Clone();
            range.Normalize();

            //
            range.Start = new Place(0, 0);
            range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);

            ranges = range.GetRangesByLines(RegexValues.SqlCmdObjectsShort, RegexOptions.IgnoreCase);
            if (ranges.Count() == 0)
                return false;
            else
                return true;
            //************************

            var txt = tb.Text;
            var regex = new Regex(RegexValues.SqlCmdObjectsShort, RegexOptions.IgnoreCase);
            List<string> objects = new List<string>();
            foreach (Match item in regex.Matches(txt))
            {
                if (!objects.Contains(item.Value))
                    objects.Add(item.Value);
            }
            
            foreach (var item in objects)
            {
                if (variables.ContainsKey(item))
                {
                    txt = txt.Replace(item, variables[item]);
                }
            }

            tb.Text = txt;
            tb.Selection.Start = new Place(0, 0);

            
            return false;

            
        }

      
    }
}
