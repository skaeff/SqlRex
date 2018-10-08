using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public partial class AssemblyViewerForm : Form
    {
        string _fileName;
        public AssemblyViewerForm(string fileName)
        {
            InitializeComponent();
            _fileName = fileName;
            LoadTree();
        }

        private void LoadTree()
        {



            string pathToAssembly = _fileName;
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(pathToAssembly);
            Mono.Cecil.AssemblyDefinition assemblyDefinition = Mono.Cecil.AssemblyDefinition.ReadAssembly(pathToAssembly);
            //AstBuilder astBuilder = null;

            foreach (var typeInAssembly in assemblyDefinition.MainModule.Types)
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
                    /*
                    Console.WriteLine("T:{0}", typeInAssembly.Name);
                    //just reset the builder to include only code for a single type
                    //astBuilder = new AstBuilder(new ICSharpCode.Decompiler.DecompilerContext(assemblyDefinition.MainModule));
                    //astBuilder.AddType(typeInAssembly);
                    StringWriter output = new StringWriter();
                    //astBuilder.GenerateCode(new PlainTextOutput(output));
                    string result = output.ToString();
                    output.Dispose();
                    */
                }
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var decompiler = new CSharpDecompiler(_fileName, new DecompilerSettings());
            //var name = new FullTypeName(e.Node.Text);

            var str = decompiler.DecompileAsString(e.Node.Tag as IMemberDefinition);
            //var str = decompiler.DecompileTypeAsString(name);

            fastColoredTextBox1.Text = str;
        }
    }
}
