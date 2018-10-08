using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using Mono.Cecil;
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
    public partial class AssemblyExporterForm : BaseForm
    {

        public AssemblyExporterForm()
        {
            InitializeComponent();
            serverTabsControl1.RebuildServerTabs();
            serverTabsControl1.OnDatabaseSelected += ServerTabsControl1_OnDatabaseSelected;
            serverTabsControl1.OnServerTabsChanged += ServerTabsControl1_OnServerTabsChanged;
        }

        private void ServerTabsControl1_OnServerTabsChanged(object sender, EventArgs e)
        {
            var main = MdiParent as IMainForm;
            if (main != null)
            {
                main.ConnectionsChanged();
            }
        }

        public override void NotifyReloadConnections()
        {
            serverTabsControl1.RebuildServerTabs();
        }

        private void ServerTabsControl1_OnDatabaseSelected(object sender, string e)
        {
            Common.Async.ExecAsync(this, (b) => InitList(e), (tm) => ReportTime(tm), true);
        }

        string _connStr;
        void InitList(string connStr)
        {
            _connStr = connStr;

            var result = new List<SqlAssemblyObject>();

            using (var conn = new SqlConnection(_connStr))
            {
                Syncronized(() => Text =  conn.DataSource + "." + conn.Database + " CLR dlls");

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

            _listItems.Clear();
            _listItemsCache.Clear();

            _listItems.AddRange(result);
            _listItemsCache.AddRange(result);
            _needRebuild = true;
            Syncronized(()=> listView1.VirtualListSize = _listItems.Count);
            Syncronized(()=> listView1.SelectedIndices.Clear());
            Syncronized(() => (MdiParent as IMainForm).RefreshTab());
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenViewer();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void exploredoubleClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenViewer();
        }

        AssemblyDefinition _assemblyDefinition;

        void OpenViewer()
        {
            try
            {
                if (listView1.SelectedIndices.Count > 0)
                {
                    var idx = listView1.SelectedIndices[0];


                    var pars = new ReaderParameters();
                    pars.AssemblyResolver = new DatabaseAssemblyResolver(_listItems);
                    _assemblyDefinition = Mono.Cecil.AssemblyDefinition.ReadAssembly(new MemoryStream(_listItems[idx].Data.Value), pars);

                    treeView1.Nodes.Clear();

                    label1.Text = "Types from [" + _listItems[idx].AssemblyName + "]";
                    foreach (var typeInAssembly in _assemblyDefinition.MainModule.Types)
                    {
                        if (typeInAssembly.IsPublic)
                        {
                            var node = new TreeNode(typeInAssembly.FullName);
                            node.Tag = typeInAssembly;
                            node.ImageIndex = 0;
                            node.SelectedImageIndex = 0;
                            foreach (var item in typeInAssembly.Methods)
                            {
                                var mNode = new TreeNode(item.Name);
                                mNode.Tag = item;
                                mNode.ImageIndex = 2;
                                mNode.SelectedImageIndex = 2;
                                node.Nodes.Add(mNode);
                            }

                            foreach (var item in typeInAssembly.Properties)
                            {
                                var mNode = new TreeNode(item.Name);
                                mNode.Tag = item;
                                mNode.ImageIndex = 3;
                                mNode.SelectedImageIndex = 3;
                                node.Nodes.Add(mNode);
                            }

                            foreach (var item in typeInAssembly.Fields)
                            {
                                var mNode = new TreeNode(item.Name);
                                mNode.Tag = item;
                                mNode.ImageIndex = 1;
                                mNode.SelectedImageIndex = 1;
                                node.Nodes.Add(mNode);
                            }

                            treeView1.Nodes.Add(node);

                        }
                    }

                    //var f = new AssemblyViewerForm(idx, _listItems);
                    //f.MdiParent = this.MdiParent;
                    //f.Show();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var decompiler = new CSharpDecompiler(_assemblyDefinition.MainModule, new DecompilerSettings());
            //var name = new FullTypeName(e.Node.Text);

            var str = decompiler.DecompileAsString(e.Node.Tag as IMemberDefinition);
            //var str = decompiler.DecompileTypeAsString(name);

            fastColoredTextBox1.Text = str;
        }
    }
}
