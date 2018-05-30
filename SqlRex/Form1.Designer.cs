namespace SqlRex
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.findUsagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findUsageswholeWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearSearches = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbSearchNode2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnBookMarks = new System.Windows.Forms.Button();
            this.cbFullProcText = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblRegexCntFound2 = new System.Windows.Forms.Label();
            this.lblRegexCntFound = new System.Windows.Forms.Label();
            this.tbSearchNode = new System.Windows.Forms.TextBox();
            this.tbRegExp2 = new System.Windows.Forms.TextBox();
            this.tbRegExp = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showObjectDependenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miBookmarks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.documentMap1 = new FastColoredTextBoxNS.DocumentMap();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter3 = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
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
            this.fastColoredTextBox1.ContextMenuStrip = this.contextMenuStrip1;
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
            this.fastColoredTextBox1.Size = new System.Drawing.Size(610, 310);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(219, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
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
            this.panel1.Controls.Add(this.btnClearSearches);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.tbSearchNode2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.btnBookMarks);
            this.panel1.Controls.Add(this.cbFullProcText);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblRegexCntFound2);
            this.panel1.Controls.Add(this.lblRegexCntFound);
            this.panel1.Controls.Add(this.tbSearchNode);
            this.panel1.Controls.Add(this.tbRegExp2);
            this.panel1.Controls.Add(this.tbRegExp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1428, 215);
            this.panel1.TabIndex = 1;
            // 
            // btnClearSearches
            // 
            this.btnClearSearches.Location = new System.Drawing.Point(508, 186);
            this.btnClearSearches.Name = "btnClearSearches";
            this.btnClearSearches.Size = new System.Drawing.Size(114, 23);
            this.btnClearSearches.TabIndex = 35;
            this.btnClearSearches.Text = "clear searches";
            this.btnClearSearches.UseVisualStyleBackColor = true;
            this.btnClearSearches.Click += new System.EventHandler(this.btnClearSearches_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(628, 151);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(797, 58);
            this.flowLayoutPanel1.TabIndex = 34;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(983, 34);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 32;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tbSearchNode2
            // 
            this.tbSearchNode2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchNode2.Location = new System.Drawing.Point(989, 126);
            this.tbSearchNode2.Name = "tbSearchNode2";
            this.tbSearchNode2.Size = new System.Drawing.Size(398, 20);
            this.tbSearchNode2.TabIndex = 31;
            this.tbSearchNode2.TextChanged += new System.EventHandler(this.tbSearchNode2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(423, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(635, 20);
            this.textBox1.TabIndex = 30;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(136, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(281, 82);
            this.listBox1.TabIndex = 29;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // btnBookMarks
            // 
            this.btnBookMarks.Location = new System.Drawing.Point(12, 151);
            this.btnBookMarks.Name = "btnBookMarks";
            this.btnBookMarks.Size = new System.Drawing.Size(119, 23);
            this.btnBookMarks.TabIndex = 28;
            this.btnBookMarks.Text = "bookmarks";
            this.btnBookMarks.UseVisualStyleBackColor = true;
            this.btnBookMarks.Click += new System.EventHandler(this.btnBookMarks_Click);
            // 
            // cbFullProcText
            // 
            this.cbFullProcText.AutoSize = true;
            this.cbFullProcText.Location = new System.Drawing.Point(426, 40);
            this.cbFullProcText.Name = "cbFullProcText";
            this.cbFullProcText.Size = new System.Drawing.Size(132, 17);
            this.cbFullProcText.TabIndex = 25;
            this.cbFullProcText.Text = "full proc (between GO)";
            this.cbFullProcText.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "get DDL with index";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblRegexCntFound2
            // 
            this.lblRegexCntFound2.AutoSize = true;
            this.lblRegexCntFound2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRegexCntFound2.Location = new System.Drawing.Point(986, 77);
            this.lblRegexCntFound2.Name = "lblRegexCntFound2";
            this.lblRegexCntFound2.Size = new System.Drawing.Size(50, 18);
            this.lblRegexCntFound2.TabIndex = 15;
            this.lblRegexCntFound2.Text = "label1";
            // 
            // lblRegexCntFound
            // 
            this.lblRegexCntFound.AutoSize = true;
            this.lblRegexCntFound.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRegexCntFound.Location = new System.Drawing.Point(423, 77);
            this.lblRegexCntFound.Name = "lblRegexCntFound";
            this.lblRegexCntFound.Size = new System.Drawing.Size(50, 18);
            this.lblRegexCntFound.TabIndex = 15;
            this.lblRegexCntFound.Text = "label1";
            // 
            // tbSearchNode
            // 
            this.tbSearchNode.Location = new System.Drawing.Point(14, 126);
            this.tbSearchNode.Name = "tbSearchNode";
            this.tbSearchNode.Size = new System.Drawing.Size(438, 20);
            this.tbSearchNode.TabIndex = 6;
            this.tbSearchNode.TextChanged += new System.EventHandler(this.tbSearchNode_TextChanged);
            // 
            // tbRegExp2
            // 
            this.tbRegExp2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegExp2.Location = new System.Drawing.Point(989, 100);
            this.tbRegExp2.Name = "tbRegExp2";
            this.tbRegExp2.Size = new System.Drawing.Size(398, 20);
            this.tbRegExp2.TabIndex = 1;
            this.tbRegExp2.Text = "([A-Za-z0-9$]+)";
            this.tbRegExp2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRegExp2_KeyDown);
            // 
            // tbRegExp
            // 
            this.tbRegExp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegExp.Location = new System.Drawing.Point(14, 100);
            this.tbRegExp.Name = "tbRegExp";
            this.tbRegExp.Size = new System.Drawing.Size(438, 20);
            this.tbRegExp.TabIndex = 1;
            this.tbRegExp.Text = "([A-Za-z0-9$]+)";
            this.tbRegExp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRegExp_KeyDown);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(306, 215);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 310);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyListToolStripMenuItem,
            this.showObjectDependenciesToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(216, 48);
            // 
            // copyListToolStripMenuItem
            // 
            this.copyListToolStripMenuItem.Name = "copyListToolStripMenuItem";
            this.copyListToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.copyListToolStripMenuItem.Text = "Copy list";
            this.copyListToolStripMenuItem.Click += new System.EventHandler(this.copyListToolStripMenuItem_Click);
            // 
            // showObjectDependenciesToolStripMenuItem
            // 
            this.showObjectDependenciesToolStripMenuItem.Name = "showObjectDependenciesToolStripMenuItem";
            this.showObjectDependenciesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.showObjectDependenciesToolStripMenuItem.Text = "Show object dependencies";
            this.showObjectDependenciesToolStripMenuItem.Click += new System.EventHandler(this.showObjectDependenciesToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.result});
            this.listView1.ContextMenuStrip = this.contextMenuStrip2;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 215);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(306, 310);
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
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView2.ContextMenuStrip = this.contextMenuStrip3;
            this.listView2.Dock = System.Windows.Forms.DockStyle.Right;
            this.listView2.FullRowSelect = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(1122, 215);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(306, 310);
            this.listView2.TabIndex = 6;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.VirtualMode = true;
            this.listView2.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.listView2_CacheVirtualItems);
            this.listView2.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView2_RetrieveVirtualItem);
            this.listView2.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.listView2_SearchForVirtualItem);
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 300;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip3.Name = "contextMenuStrip2";
            this.contextMenuStrip3.Size = new System.Drawing.Size(216, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(215, 22);
            this.toolStripMenuItem1.Text = "Copy list";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(215, 22);
            this.toolStripMenuItem2.Text = "Show object dependencies";
            // 
            // miBookmarks
            // 
            this.miBookmarks.Name = "miBookmarks";
            this.miBookmarks.Size = new System.Drawing.Size(61, 4);
            this.miBookmarks.Opening += new System.ComponentModel.CancelEventHandler(this.miBookmarks_Opening);
            // 
            // documentMap1
            // 
            this.documentMap1.BackColor = System.Drawing.Color.White;
            this.documentMap1.Dock = System.Windows.Forms.DockStyle.Right;
            this.documentMap1.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap1.Location = new System.Drawing.Point(613, 0);
            this.documentMap1.Name = "documentMap1";
            this.documentMap1.Size = new System.Drawing.Size(200, 310);
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
            this.panel2.Location = new System.Drawing.Point(309, 215);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 310);
            this.panel2.TabIndex = 8;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(610, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 310);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(1119, 215);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 310);
            this.splitter3.TabIndex = 9;
            this.splitter3.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 525);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tbRegExp;
        private System.Windows.Forms.TextBox tbSearchNode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblRegexCntFound;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copyListToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader result;
        private System.Windows.Forms.CheckBox cbFullProcText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem showObjectDependenciesToolStripMenuItem;
        private System.Windows.Forms.TextBox tbRegExp2;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripMenuItem findUsagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findUsageswholeWordToolStripMenuItem;
        private System.Windows.Forms.Button btnBookMarks;
        private System.Windows.Forms.ContextMenuStrip miBookmarks;
        private FastColoredTextBoxNS.DocumentMap documentMap1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbSearchNode2;
        private System.Windows.Forms.Label lblRegexCntFound2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnClearSearches;
    }
}

