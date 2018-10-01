using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public partial class AssemblyExporterForm : Form
    {
        string _connStr;
        public AssemblyExporterForm(string connStr)
        {
            InitializeComponent();
            _connStr = connStr;

            var result = new List<SqlAssemblyObject>();

            using (var conn = new SqlConnection(_connStr))
            {
                Text = conn.DataSource + "." + conn.Database;

                conn.Open();
                var cmd = new SqlCommand(File.ReadAllText(Application.StartupPath + @"\get_assemblies.sql"), conn);
                cmd.CommandTimeout = 0;
                //var rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var obj = new SqlAssemblyObject();
                    obj.AssemblyName = rdr.GetSqlString(0).ToString();
                    obj.Data = rdr.GetSqlBytes(1);
                    result.Add(obj);
                }
                rdr.Close();
                
                conn.Close();
            }

            _listItems.AddRange(result);
            _listItemsCache.AddRange(result);
            _needRebuild = true;
            listView1.VirtualListSize = _listItems.Count;
            listView1.SelectedIndices.Clear();
        }
        List<SqlAssemblyObject> _listItems = new List<SqlAssemblyObject>();
        List<SqlAssemblyObject> _listItemsCache = new List<SqlAssemblyObject>();
        bool _needRebuild;

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
                SqlAssemblyObject[] ranges;

                ranges = _listItemsCache.Where(r => r.AssemblyName.ToLower().Contains(tbSearchNode.Text.ToLower())).ToArray();

                _listItems.AddRange(ranges);
                _needRebuild = true;
                listView1.VirtualListSize = _listItems.Count;
                listView1.SelectedIndices.Clear();
            }
        }

        List<ListViewItem> _cache = new List<ListViewItem>();
        private void listView1_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            if (_needRebuild)
            {
                _cache.Clear();
                foreach (var item in _listItems)
                {
                    _cache.Add(new ListViewItem(item.AssemblyName));
                }
                _needRebuild = false;
            }
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
                if (item.AssemblyName.Contains(e.Text))
                {
                    e.Index = i;
                }
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(listView1.SelectedIndices.Count > 0) 
            {
                var idx = listView1.SelectedIndices[0];
                var asm = _listItems[idx];

                saveFileDialog1.FileName = asm.AssemblyName + ".dll";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream bytestream = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                    {
                        bytestream.Write(asm.Data.Value, 0, (int)asm.Data.Length);
                        bytestream.Close();
                    }
                }
            }
        }
    }
}
