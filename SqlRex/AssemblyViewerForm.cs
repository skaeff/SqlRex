using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public partial class AssemblyViewerForm : Form
    {
        AssemblyDefinition _assemblyDefinition;
        
        public AssemblyViewerForm(Stream stream, List<SqlAssemblyObject> dlls)
        {
            InitializeComponent();

            var pars = new ReaderParameters();
            pars.AssemblyResolver = new DatabaseAssemblyResolver(dlls);
            _assemblyDefinition = Mono.Cecil.AssemblyDefinition.ReadAssembly(stream, pars);

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
