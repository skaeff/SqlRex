using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SqlRex
{
    public partial class ServerTabsControl : UserControl
    {
        public event EventHandler<string> OnDatabaseSelected;
        public event EventHandler OnServerTabsChanged;

        public ServerTabsControl()
        {
            InitializeComponent();
            
        }

        Dictionary<string, List<string>> _serverTabs;
        string _selectedConnection = "";

        public string SelectedConnection
        {
            get { return _selectedConnection; }
        }

        public void RebuildServerTabs()
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

                tp.Tag = item.Key;
                var lb = new ListView();

                //lb.DrawItem += lbSqlDatabases_DrawItem;
                //lb.MeasureItem += lbSqlDatabases_MeasureItem;
                lb.SelectedIndexChanged += lbSqlDatabases_SelectedIndexChanged;
                lb.MouseDoubleClick += lbSqlDatabases_MouseDoubleClick;
                lb.KeyDown += lbSqlDatabases_KeyDown;
                lb.Dock = DockStyle.Fill;
                lb.SmallImageList = imageList1;
                lb.View = View.SmallIcon;
                lb.HideSelection = false;
                //lb.DrawMode = DrawMode.OwnerDrawVariable;
                //lb.Font = new Font(lb.Font.FontFamily, 10);
                lb.Items.AddRange(item.Value.ConvertAll<ListViewItem>((s) =>
                {
                    var csb = new SqlConnectionStringBuilder(s);
                    var auth = "[sql]";
                    if (csb.IntegratedSecurity)
                    {
                        auth = "[win]";
                    }
                    return new ListViewItem(csb.InitialCatalog + auth) { Tag = s, ImageIndex = 0 };

                }
                    ).ToArray());
                lb.ContextMenuStrip = contextMenuStrip1;

                tp.Controls.Add(lb);
                tabSource.TabPages.Add(tp);
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

            var item = (sender as ListView).Items[e.Index].ToString();

            var conStr = new SqlConnectionStringBuilder(item);

            e.DrawBackground();

            e.Graphics.DrawString(conStr.DataSource + "." + conStr.InitialCatalog + (conStr.IntegratedSecurity ? " [win]" : " [sql]"), e.Font, Brushes.Black, e.Bounds);

            e.DrawFocusRectangle();

        }

        private void lbSqlDatabases_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (sender as ListView).Font.Height;
        }

        private void lbSqlDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbSqlDatabases_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((sender as ListView).SelectedItems.Count > 0)
            {

                foreach (TabPage page in tabSource.TabPages)
                {
                    page.Text = page.Tag.ToString();
                    foreach (Control control in page.Controls)
                    {
                        if (control is ListView)
                        {
                            foreach (ListViewItem item in (control as ListView).Items)
                            {
                                item.ImageIndex = 0;
                            }
                        }
                    }
                }

                var selectedPage = ((sender as ListView).Parent as TabPage);
                selectedPage.Text = "*" + selectedPage.Text;
                (sender as ListView).SelectedItems[0].ImageIndex = 1;
                _selectedConnection = (sender as ListView).SelectedItems[0].Tag.ToString();
                var db = Utils.DecryptedConnectionString(_selectedConnection);
                var csb = new SqlConnectionStringBuilder(db);
                if (!csb.IntegratedSecurity)
                {
                    csb.Password = Utils.Decrypt(csb.Password);
                }
                Text = csb.DataSource + "." + csb.InitialCatalog;

                if(OnDatabaseSelected != null)
                {
                    OnDatabaseSelected(null, db);
                }
                //(MdiParent as IMainForm).RefreshTab();
                //Common.Async.ExecAsync(this, (b) => BuildSqlObjects(db, b), (tm) => ReportTime(tm), true);
                //EnableButtons();
            }
        }

        private void lbSqlDatabases_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lbSqlDatabases_MouseDoubleClick(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
            }
            //e.SuppressKeyPress = true;
        }

        private void addConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lb = ((sender as ToolStripItem).Owner as ContextMenuStrip).SourceControl as ListView;
            var f = new ConnectionDialog();
            if (f.ShowDialog() == DialogResult.OK)
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

                if(OnServerTabsChanged != null)
                {
                    OnServerTabsChanged(null, EventArgs.Empty);
                }
                //var main = MdiParent as IMainForm;
                //if (main != null)
                //{
                //    main.ConnectionsChanged();
                //}

            }
        }

        private void testConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lb = ((sender as ToolStripItem).Owner as ContextMenuStrip).SourceControl as ListView;
            if (lb.SelectedItems.Count > 0)
            {
                var f = new ConnectionDialog();
                f.ConnectionString = lb.SelectedItems[0].Tag.ToString();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    var connStr = f.ConnectionString;

                    var conns = new List<string>();
                    foreach (var item in _serverTabs)
                    {
                        item.Value.Remove(lb.SelectedItems[0].Tag.ToString());
                        foreach (var conn in item.Value)
                        {
                            conns.Add(conn);
                        }
                    }
                    conns.Add(connStr);

                    File.WriteAllLines(Application.StartupPath + @"\connections.txt", conns.ToArray());

                    if (OnServerTabsChanged != null)
                    {
                        OnServerTabsChanged(null, EventArgs.Empty);
                    }
                    //var main = MdiParent as IMainForm;
                    //if (main != null)
                    //{
                    //    main.ConnectionsChanged();
                    //}
                }
            }
        }

        private void deleteConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lb = ((sender as ToolStripItem).Owner as ContextMenuStrip).SourceControl as ListView;
            if (lb.SelectedItems.Count > 0 &&
                MessageBox.Show(string.Format("Are you sure to delete [{0}] from connections list?", lb.SelectedItems[0].Tag.ToString()), "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3)
                == DialogResult.Yes)
            {

                var conns = new List<string>();
                foreach (var item in _serverTabs)
                {
                    item.Value.Remove(lb.SelectedItems[0].Tag.ToString());
                    foreach (var conn in item.Value)
                    {
                        conns.Add(conn);
                    }
                }

                File.WriteAllLines(Application.StartupPath + @"\connections.txt", conns.ToArray());

                if (OnServerTabsChanged != null)
                {
                    OnServerTabsChanged(null, EventArgs.Empty);
                }
                //var main = MdiParent as IMainForm;
                //if (main != null)
                //{
                //    main.ConnectionsChanged();
                //}
            }
        }
    }
}
