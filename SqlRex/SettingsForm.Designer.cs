namespace SqlRex
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.cbLargeFiles = new System.Windows.Forms.CheckBox();
            this.cbRegexOnLoad = new System.Windows.Forms.CheckBox();
            this.cbEncoding = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbReadonlySQL = new System.Windows.Forms.CheckBox();
            this.cbAutoComplete = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbLargeFiles
            // 
            this.cbLargeFiles.AutoSize = true;
            this.cbLargeFiles.Location = new System.Drawing.Point(12, 12);
            this.cbLargeFiles.Name = "cbLargeFiles";
            this.cbLargeFiles.Size = new System.Drawing.Size(77, 17);
            this.cbLargeFiles.TabIndex = 3;
            this.cbLargeFiles.Text = "Large Files";
            this.cbLargeFiles.UseVisualStyleBackColor = true;
            this.cbLargeFiles.CheckedChanged += new System.EventHandler(this.cbLargeFiles_CheckedChanged);
            // 
            // cbRegexOnLoad
            // 
            this.cbRegexOnLoad.AutoSize = true;
            this.cbRegexOnLoad.Location = new System.Drawing.Point(12, 53);
            this.cbRegexOnLoad.Name = "cbRegexOnLoad";
            this.cbRegexOnLoad.Size = new System.Drawing.Size(270, 17);
            this.cbRegexOnLoad.TabIndex = 4;
            this.cbRegexOnLoad.Text = "Apply DdlObjectsPreparedWithIndex on file opening";
            this.cbRegexOnLoad.UseVisualStyleBackColor = true;
            this.cbRegexOnLoad.CheckedChanged += new System.EventHandler(this.cbRegexOnLoad_CheckedChanged);
            // 
            // cbEncoding
            // 
            this.cbEncoding.FormattingEnabled = true;
            this.cbEncoding.Location = new System.Drawing.Point(105, 91);
            this.cbEncoding.Name = "cbEncoding";
            this.cbEncoding.Size = new System.Drawing.Size(211, 21);
            this.cbEncoding.TabIndex = 5;
            this.cbEncoding.SelectedIndexChanged += new System.EventHandler(this.cbEncoding_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "default encoding";
            // 
            // cbReadonlySQL
            // 
            this.cbReadonlySQL.AutoSize = true;
            this.cbReadonlySQL.Location = new System.Drawing.Point(12, 132);
            this.cbReadonlySQL.Name = "cbReadonlySQL";
            this.cbReadonlySQL.Size = new System.Drawing.Size(90, 17);
            this.cbReadonlySQL.TabIndex = 7;
            this.cbReadonlySQL.Text = "readonly SQL";
            this.cbReadonlySQL.UseVisualStyleBackColor = true;
            this.cbReadonlySQL.CheckedChanged += new System.EventHandler(this.cbReadonlySQL_CheckedChanged);
            // 
            // cbAutoComplete
            // 
            this.cbAutoComplete.AutoSize = true;
            this.cbAutoComplete.Location = new System.Drawing.Point(12, 168);
            this.cbAutoComplete.Name = "cbAutoComplete";
            this.cbAutoComplete.Size = new System.Drawing.Size(139, 17);
            this.cbAutoComplete.TabIndex = 8;
            this.cbAutoComplete.Text = "Autocomplete in textbox";
            this.cbAutoComplete.UseVisualStyleBackColor = true;
            this.cbAutoComplete.CheckedChanged += new System.EventHandler(this.cbAutoComplete_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 402);
            this.Controls.Add(this.cbAutoComplete);
            this.Controls.Add(this.cbReadonlySQL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbEncoding);
            this.Controls.Add(this.cbRegexOnLoad);
            this.Controls.Add(this.cbLargeFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbLargeFiles;
        private System.Windows.Forms.CheckBox cbRegexOnLoad;
        private System.Windows.Forms.ComboBox cbEncoding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbReadonlySQL;
        private System.Windows.Forms.CheckBox cbAutoComplete;
    }
}