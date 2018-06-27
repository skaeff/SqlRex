using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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

        /// <summary>
        /// Encrypts a given password and returns the encrypted data
        /// as a base64 string.
        /// </summary>
        /// <param name="plainText">An unencrypted string that needs
        /// to be secured.</param>
        /// <returns>A base64 encoded string that represents the encrypted
        /// binary data.
        /// </returns>
        /// <remarks>This solution is not really secure as we are
        /// keeping strings in memory. If runtime protection is essential,
        /// <see cref="SecureString"/> should be used.</remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="plainText"/>
        /// is a null reference.</exception>
        public static string Encrypt(string plainText)
        {
            try
            {
                if (plainText == null) throw new ArgumentNullException("plainText");

                //encrypt data
                var data = Encoding.Unicode.GetBytes(plainText);
                byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

                //return as base64 string
                return Convert.ToBase64String(encrypted);
            }
            catch
            {
                return plainText;
            }
        }

        /// <summary>
        /// Decrypts a given string.
        /// </summary>
        /// <param name="cipher">A base64 encoded string that was created
        /// through the <see cref="Encrypt(string)"/> or
        /// <see cref="Encrypt(SecureString)"/> extension methods.</param>
        /// <returns>The decrypted string.</returns>
        /// <remarks>Keep in mind that the decrypted string remains in memory
        /// and makes your application vulnerable per se. If runtime protection
        /// is essential, <see cref="SecureString"/> should be used.</remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="cipher"/>
        /// is a null reference.</exception>
        public static string Decrypt(string cipher)
        {
            try
            {
                if (cipher == null) throw new ArgumentNullException("cipher");

                //parse base64 string
                byte[] data = Convert.FromBase64String(cipher);

                //decrypt data
                byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(decrypted);
            }
            catch
            {
                return cipher;
            }
        }

        public static string DecryptedConnectionString(string connStr)
        {
            try
            {
                var csb = new SqlConnectionStringBuilder(connStr);
                if (!csb.IntegratedSecurity)
                {
                    csb.Password = Utils.Decrypt(csb.Password);
                }

                return csb.ConnectionString;
            }
            catch
            {
                return connStr;
            }
        }

    }
}
