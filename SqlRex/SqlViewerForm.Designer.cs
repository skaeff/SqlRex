namespace SqlRex
{
    partial class SqlViewerForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlViewerForm));
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.findUsagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findUsageswholeWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenerateSqlFile = new System.Windows.Forms.Button();
            this.cbSearchInText = new System.Windows.Forms.CheckBox();
            this.lbSqlDatabases = new System.Windows.Forms.ListBox();
            this.btnGetSqlObjects = new System.Windows.Forms.Button();
            this.lblRegexCntFound = new System.Windows.Forms.Label();
            this.tbSearchNode = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.diffWithFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getCREATETABLEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.documentMap1 = new FastColoredTextBoxNS.DocumentMap();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnGenerateSqlFileNoTables = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBox1.AutoIndent = false;
            this.fastColoredTextBox1.AutoIndentCharsPatterns = "";
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.fastColoredTextBox1.BackBrush = null;
            this.fastColoredTextBox1.BookmarkColor = System.Drawing.Color.Crimson;
            this.fastColoredTextBox1.CharHeight = 14;
            this.fastColoredTextBox1.CharWidth = 8;
            this.fastColoredTextBox1.CommentPrefix = "--";
            this.fastColoredTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Hotkeys = resources.GetString("fastColoredTextBox1.Hotkeys");
            this.fastColoredTextBox1.IsReplaceMode = false;
            this.fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.SQL;
            this.fastColoredTextBox1.LeftBracket = '(';
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox1.RightBracket = ')';
            this.fastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox1.ServiceColors")));
            this.fastColoredTextBox1.Size = new System.Drawing.Size(916, 371);
            this.fastColoredTextBox1.TabIndex = 0;
            this.fastColoredTextBox1.Text = "fastColoredTextBox1";
            this.fastColoredTextBox1.Zoom = 100;
            this.fastColoredTextBox1.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChanged);
            this.fastColoredTextBox1.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChangedDelayed);
            this.fastColoredTextBox1.VisibleRangeChangedDelayed += new System.EventHandler(this.fastColoredTextBox1_VisibleRangeChangedDelayed);
            this.fastColoredTextBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fastColoredTextBox1_MouseClick);
            this.fastColoredTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fastColoredTextBox1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findUsagesToolStripMenuItem,
            this.findUsageswholeWordToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(219, 48);
            // 
            // findUsagesToolStripMenuItem
            // 
            this.findUsagesToolStripMenuItem.Name = "findUsagesToolStripMenuItem";
            this.findUsagesToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.findUsagesToolStripMenuItem.Text = "Find usages...";
            this.findUsagesToolStripMenuItem.Click += new System.EventHandler(this.findUsagesToolStripMenuItem_Click);
            // 
            // findUsageswholeWordToolStripMenuItem
            // 
            this.findUsageswholeWordToolStripMenuItem.Name = "findUsageswholeWordToolStripMenuItem";
            this.findUsageswholeWordToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.findUsageswholeWordToolStripMenuItem.Text = "Find usages (whole word)...";
            this.findUsageswholeWordToolStripMenuItem.Click += new System.EventHandler(this.findUsageswholeWordToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGenerateSqlFileNoTables);
            this.panel1.Controls.Add(this.btnGenerateSqlFile);
            this.panel1.Controls.Add(this.cbSearchInText);
            this.panel1.Controls.Add(this.lbSqlDatabases);
            this.panel1.Controls.Add(this.btnGetSqlObjects);
            this.panel1.Controls.Add(this.lblRegexCntFound);
            this.panel1.Controls.Add(this.tbSearchNode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1428, 154);
            this.panel1.TabIndex = 1;
            // 
            // btnGenerateSqlFile
            // 
            this.btnGenerateSqlFile.Location = new System.Drawing.Point(15, 68);
            this.btnGenerateSqlFile.Name = "btnGenerateSqlFile";
            this.btnGenerateSqlFile.Size = new System.Drawing.Size(187, 23);
            this.btnGenerateSqlFile.TabIndex = 35;
            this.btnGenerateSqlFile.Text = "generate SQL file";
            this.btnGenerateSqlFile.UseVisualStyleBackColor = true;
            this.btnGenerateSqlFile.Click += new System.EventHandler(this.btnGenerateSqlFile_Click);
            // 
            // cbSearchInText
            // 
            this.cbSearchInText.AutoSize = true;
            this.cbSearchInText.Location = new System.Drawing.Point(456, 129);
            this.cbSearchInText.Name = "cbSearchInText";
            this.cbSearchInText.Size = new System.Drawing.Size(89, 17);
            this.cbSearchInText.TabIndex = 34;
            this.cbSearchInText.Text = "search in text";
            this.cbSearchInText.UseVisualStyleBackColor = true;
            // 
            // lbSqlDatabases
            // 
            this.lbSqlDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSqlDatabases.FormattingEnabled = true;
            this.lbSqlDatabases.Location = new System.Drawing.Point(223, 13);
            this.lbSqlDatabases.Name = "lbSqlDatabases";
            this.lbSqlDatabases.Size = new System.Drawing.Size(1180, 108);
            this.lbSqlDatabases.TabIndex = 33;
            this.lbSqlDatabases.SelectedIndexChanged += new System.EventHandler(this.lbSqlDatabases_SelectedIndexChanged);
            this.lbSqlDatabases.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSqlDatabases_MouseDoubleClick);
            // 
            // btnGetSqlObjects
            // 
            this.btnGetSqlObjects.Location = new System.Drawing.Point(14, 13);
            this.btnGetSqlObjects.Name = "btnGetSqlObjects";
            this.btnGetSqlObjects.Size = new System.Drawing.Size(188, 23);
            this.btnGetSqlObjects.TabIndex = 32;
            this.btnGetSqlObjects.Text = "get sql objects";
            this.btnGetSqlObjects.UseVisualStyleBackColor = true;
            this.btnGetSqlObjects.Click += new System.EventHandler(this.btnGetSqlObjects_Click);
            // 
            // lblRegexCntFound
            // 
            this.lblRegexCntFound.AutoSize = true;
            this.lblRegexCntFound.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRegexCntFound.Location = new System.Drawing.Point(12, 39);
            this.lblRegexCntFound.Name = "lblRegexCntFound";
            this.lblRegexCntFound.Size = new System.Drawing.Size(50, 18);
            this.lblRegexCntFound.TabIndex = 15;
            this.lblRegexCntFound.Text = "label1";
            // 
            // tbSearchNode
            // 
            this.tbSearchNode.Location = new System.Drawing.Point(12, 126);
            this.tbSearchNode.Name = "tbSearchNode";
            this.tbSearchNode.Size = new System.Drawing.Size(438, 20);
            this.tbSearchNode.TabIndex = 6;
            this.tbSearchNode.TextChanged += new System.EventHandler(this.tbSearchNode_TextChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(306, 154);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 371);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "SQL files|*.sql";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diffWithFileToolStripMenuItem,
            this.getCREATETABLEToolStripMenuItem,
            this.refreshObjectToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(175, 70);
            // 
            // diffWithFileToolStripMenuItem
            // 
            this.diffWithFileToolStripMenuItem.Name = "diffWithFileToolStripMenuItem";
            this.diffWithFileToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.diffWithFileToolStripMenuItem.Text = "Diff with file...";
            this.diffWithFileToolStripMenuItem.Click += new System.EventHandler(this.diffWithFileToolStripMenuItem_Click);
            // 
            // getCREATETABLEToolStripMenuItem
            // 
            this.getCREATETABLEToolStripMenuItem.Name = "getCREATETABLEToolStripMenuItem";
            this.getCREATETABLEToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.getCREATETABLEToolStripMenuItem.Text = "Get CREATE TABLE";
            this.getCREATETABLEToolStripMenuItem.Click += new System.EventHandler(this.getCREATETABLEToolStripMenuItem_Click);
            // 
            // refreshObjectToolStripMenuItem
            // 
            this.refreshObjectToolStripMenuItem.Name = "refreshObjectToolStripMenuItem";
            this.refreshObjectToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.refreshObjectToolStripMenuItem.Text = "Refresh object";
            this.refreshObjectToolStripMenuItem.Click += new System.EventHandler(this.refreshObjectToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.result});
            this.listView1.ContextMenuStrip = this.contextMenuStrip2;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 154);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(306, 371);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.VirtualMode = true;
            this.listView1.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.listView1_CacheVirtualItems);
            this.listView1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView1_RetrieveVirtualItem);
            this.listView1.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.listView1_SearchForVirtualItem);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // result
            // 
            this.result.Width = 300;
            // 
            // documentMap1
            // 
            this.documentMap1.BackColor = System.Drawing.Color.White;
            this.documentMap1.Dock = System.Windows.Forms.DockStyle.Right;
            this.documentMap1.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap1.Location = new System.Drawing.Point(919, 0);
            this.documentMap1.Name = "documentMap1";
            this.documentMap1.Size = new System.Drawing.Size(200, 371);
            this.documentMap1.TabIndex = 7;
            this.documentMap1.Target = this.fastColoredTextBox1;
            this.documentMap1.Text = "documentMap1";
            this.documentMap1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.documentMap1_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fastColoredTextBox1);
            this.panel2.Controls.Add(this.splitter2);
            this.panel2.Controls.Add(this.documentMap1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(309, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1119, 371);
            this.panel2.TabIndex = 8;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(916, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 371);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "SQL files|*.sql";
            // 
            // btnGenerateSqlFileNoTables
            // 
            this.btnGenerateSqlFileNoTables.Location = new System.Drawing.Point(15, 97);
            this.btnGenerateSqlFileNoTables.Name = "btnGenerateSqlFileNoTables";
            this.btnGenerateSqlFileNoTables.Size = new System.Drawing.Size(187, 23);
            this.btnGenerateSqlFileNoTables.TabIndex = 36;
            this.btnGenerateSqlFileNoTables.Text = "generate SQL file (no tables)";
            this.btnGenerateSqlFileNoTables.UseVisualStyleBackColor = true;
            this.btnGenerateSqlFileNoTables.Click += new System.EventHandler(this.btnGenerateSqlFileNoTables_Click);
            // 
            // SqlViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 525);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SqlViewerForm";
            this.Text = "Sql Objects";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tbSearchNode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblRegexCntFound;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader result;
        private System.Windows.Forms.ToolStripMenuItem findUsagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findUsageswholeWordToolStripMenuItem;
        private FastColoredTextBoxNS.DocumentMap documentMap1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button btnGetSqlObjects;
        private System.Windows.Forms.ToolStripMenuItem diffWithFileToolStripMenuItem;
        private System.Windows.Forms.ListBox lbSqlDatabases;
        private System.Windows.Forms.ToolStripMenuItem getCREATETABLEToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbSearchInText;
        private System.Windows.Forms.ToolStripMenuItem refreshObjectToolStripMenuItem;
        private System.Windows.Forms.Button btnGenerateSqlFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnGenerateSqlFileNoTables;
    }
}

