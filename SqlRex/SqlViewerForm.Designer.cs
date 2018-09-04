﻿namespace SqlRex
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
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.findUsagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findUsagesInFoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findUsagesanyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findUsagesInFoundanyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearSearches = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGenerateSqlFileNoTables = new System.Windows.Forms.Button();
            this.btnGenerateSqlFile = new System.Windows.Forms.Button();
            this.cbSearchInText = new System.Windows.Forms.CheckBox();
            this.lbSqlDatabases = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAssemblyExporter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(158, 15);
            this.fastColoredTextBox1.BackBrush = null;
            this.fastColoredTextBox1.BookmarkColor = System.Drawing.Color.Crimson;
            this.fastColoredTextBox1.CharHeight = 15;
            this.fastColoredTextBox1.CharWidth = 7;
            this.fastColoredTextBox1.CommentPrefix = "--";
            this.fastColoredTextBox1.ContextMenuStrip = this.contextMenuStrip3;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Font = new System.Drawing.Font("Consolas", 9.75F);
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
            this.fastColoredTextBox1.Size = new System.Drawing.Size(610, 344);
            this.fastColoredTextBox1.TabIndex = 0;
            this.fastColoredTextBox1.Text = "fastColoredTextBox1";
            this.fastColoredTextBox1.Zoom = 100;
            this.fastColoredTextBox1.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChanged);
            this.fastColoredTextBox1.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChangedDelayed);
            this.fastColoredTextBox1.VisibleRangeChangedDelayed += new System.EventHandler(this.fastColoredTextBox1_VisibleRangeChangedDelayed);
            this.fastColoredTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fastColoredTextBox1_KeyDown);
            this.fastColoredTextBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fastColoredTextBox1_MouseClick);
            this.fastColoredTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fastColoredTextBox1_MouseDoubleClick);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findUsagesToolStripMenuItem,
            this.findUsagesInFoundToolStripMenuItem,
            this.findUsagesanyTextToolStripMenuItem,
            this.findUsagesInFoundanyTextToolStripMenuItem,
            this.toolStripMenuItem1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator6,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator7,
            this.selectAllToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip2";
            this.contextMenuStrip3.Size = new System.Drawing.Size(247, 242);
            // 
            // findUsagesToolStripMenuItem
            // 
            this.findUsagesToolStripMenuItem.Name = "findUsagesToolStripMenuItem";
            this.findUsagesToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.findUsagesToolStripMenuItem.Text = "Find Usages";
            this.findUsagesToolStripMenuItem.Click += new System.EventHandler(this.findUsagesToolStripMenuItem_Click_1);
            // 
            // findUsagesInFoundToolStripMenuItem
            // 
            this.findUsagesInFoundToolStripMenuItem.Enabled = false;
            this.findUsagesInFoundToolStripMenuItem.Name = "findUsagesInFoundToolStripMenuItem";
            this.findUsagesInFoundToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.findUsagesInFoundToolStripMenuItem.Text = "Find Usages in found";
            this.findUsagesInFoundToolStripMenuItem.Click += new System.EventHandler(this.findUsagesInFoundToolStripMenuItem_Click);
            // 
            // findUsagesanyTextToolStripMenuItem
            // 
            this.findUsagesanyTextToolStripMenuItem.Name = "findUsagesanyTextToolStripMenuItem";
            this.findUsagesanyTextToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.findUsagesanyTextToolStripMenuItem.Text = "Find Usages (any text)...";
            this.findUsagesanyTextToolStripMenuItem.Click += new System.EventHandler(this.findUsagesanyTextToolStripMenuItem_Click);
            // 
            // findUsagesInFoundanyTextToolStripMenuItem
            // 
            this.findUsagesInFoundanyTextToolStripMenuItem.Enabled = false;
            this.findUsagesInFoundanyTextToolStripMenuItem.Name = "findUsagesInFoundanyTextToolStripMenuItem";
            this.findUsagesInFoundanyTextToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.findUsagesInFoundanyTextToolStripMenuItem.Text = "Find Usages in found (any text)...";
            this.findUsagesInFoundanyTextToolStripMenuItem.Click += new System.EventHandler(this.findUsagesInFoundanyTextToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(243, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.undoToolStripMenuItem.Text = "&Отменить";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.redoToolStripMenuItem.Text = "&Вернуть";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(243, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.cutToolStripMenuItem.Text = "&Вырезать";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.copyToolStripMenuItem.Text = "&Копировать";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.pasteToolStripMenuItem.Text = "&Вставить";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(243, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.selectAllToolStripMenuItem.Text = "Выделить &все";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAssemblyExporter);
            this.panel1.Controls.Add(this.btnClearSearches);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
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
            this.panel1.Size = new System.Drawing.Size(1428, 181);
            this.panel1.TabIndex = 1;
            // 
            // btnClearSearches
            // 
            this.btnClearSearches.Location = new System.Drawing.Point(508, 155);
            this.btnClearSearches.Name = "btnClearSearches";
            this.btnClearSearches.Size = new System.Drawing.Size(114, 23);
            this.btnClearSearches.TabIndex = 38;
            this.btnClearSearches.Text = "clear searches";
            this.btnClearSearches.UseVisualStyleBackColor = true;
            this.btnClearSearches.Click += new System.EventHandler(this.btnClearSearches_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(628, 123);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(797, 58);
            this.flowLayoutPanel1.TabIndex = 37;
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
            this.lbSqlDatabases.ContextMenuStrip = this.contextMenuStrip1;
            this.lbSqlDatabases.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbSqlDatabases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbSqlDatabases.FormattingEnabled = true;
            this.lbSqlDatabases.Location = new System.Drawing.Point(223, 13);
            this.lbSqlDatabases.Name = "lbSqlDatabases";
            this.lbSqlDatabases.Size = new System.Drawing.Size(1180, 108);
            this.lbSqlDatabases.TabIndex = 33;
            this.lbSqlDatabases.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbSqlDatabases_DrawItem);
            this.lbSqlDatabases.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbSqlDatabases_MeasureItem);
            this.lbSqlDatabases.SelectedIndexChanged += new System.EventHandler(this.lbSqlDatabases_SelectedIndexChanged);
            this.lbSqlDatabases.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSqlDatabases_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addConnectionToolStripMenuItem,
            this.testConnectionToolStripMenuItem,
            this.deleteConnectionToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 70);
            // 
            // addConnectionToolStripMenuItem
            // 
            this.addConnectionToolStripMenuItem.Name = "addConnectionToolStripMenuItem";
            this.addConnectionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addConnectionToolStripMenuItem.Text = "add connection...";
            this.addConnectionToolStripMenuItem.Click += new System.EventHandler(this.addConnectionToolStripMenuItem_Click);
            // 
            // testConnectionToolStripMenuItem
            // 
            this.testConnectionToolStripMenuItem.Name = "testConnectionToolStripMenuItem";
            this.testConnectionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.testConnectionToolStripMenuItem.Text = "edit connection...";
            this.testConnectionToolStripMenuItem.Click += new System.EventHandler(this.testConnectionToolStripMenuItem_Click);
            // 
            // deleteConnectionToolStripMenuItem
            // 
            this.deleteConnectionToolStripMenuItem.Name = "deleteConnectionToolStripMenuItem";
            this.deleteConnectionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.deleteConnectionToolStripMenuItem.Text = "delete connection";
            this.deleteConnectionToolStripMenuItem.Click += new System.EventHandler(this.deleteConnectionToolStripMenuItem_Click);
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
            this.tbSearchNode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbSearchNode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbSearchNode.Location = new System.Drawing.Point(12, 126);
            this.tbSearchNode.Name = "tbSearchNode";
            this.tbSearchNode.Size = new System.Drawing.Size(438, 20);
            this.tbSearchNode.TabIndex = 6;
            this.tbSearchNode.TextChanged += new System.EventHandler(this.tbSearchNode_TextChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(306, 181);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 344);
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
            this.listView1.Location = new System.Drawing.Point(0, 181);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(306, 344);
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
            this.documentMap1.Location = new System.Drawing.Point(613, 0);
            this.documentMap1.Name = "documentMap1";
            this.documentMap1.Size = new System.Drawing.Size(200, 344);
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
            this.panel2.Location = new System.Drawing.Point(309, 181);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 344);
            this.panel2.TabIndex = 8;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(610, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 344);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "SQL files|*.sql";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Right;
            this.listView2.FullRowSelect = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(1122, 181);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(306, 344);
            this.listView2.TabIndex = 9;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.VirtualMode = true;
            this.listView2.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.listView2_CacheVirtualItems);
            this.listView2.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView2_RetrieveVirtualItem);
            this.listView2.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.listView2_SearchForVirtualItem);
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 300;
            // 
            // btnAssemblyExporter
            // 
            this.btnAssemblyExporter.Location = new System.Drawing.Point(15, 152);
            this.btnAssemblyExporter.Name = "btnAssemblyExporter";
            this.btnAssemblyExporter.Size = new System.Drawing.Size(187, 23);
            this.btnAssemblyExporter.TabIndex = 39;
            this.btnAssemblyExporter.Text = "assembly exporter";
            this.btnAssemblyExporter.UseVisualStyleBackColor = true;
            this.btnAssemblyExporter.Click += new System.EventHandler(this.btnAssemblyExporter_Click);
            // 
            // SqlViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 525);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SqlViewerForm";
            this.Text = "Sql Objects";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblRegexCntFound;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader result;
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem findUsagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnClearSearches;
        private System.Windows.Forms.ToolStripMenuItem findUsagesInFoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findUsagesanyTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findUsagesInFoundanyTextToolStripMenuItem;
        private System.Windows.Forms.Button btnAssemblyExporter;
    }
}

