namespace SqlRex
{
    partial class ConnectionDialog
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
            this.tbServerName = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.cbDatabaseName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbSqlAuth = new System.Windows.Forms.RadioButton();
            this.rbWinAuth = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServerName
            // 
            this.tbServerName.Location = new System.Drawing.Point(15, 25);
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.Size = new System.Drawing.Size(385, 20);
            this.tbServerName.TabIndex = 0;
            this.tbServerName.TextChanged += new System.EventHandler(this.tbServerName_TextChanged);
            // 
            // tbUserName
            // 
            this.tbUserName.Enabled = false;
            this.tbUserName.Location = new System.Drawing.Point(116, 121);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(284, 20);
            this.tbUserName.TabIndex = 0;
            // 
            // tbPassword
            // 
            this.tbPassword.Enabled = false;
            this.tbPassword.Location = new System.Drawing.Point(116, 147);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(284, 20);
            this.tbPassword.TabIndex = 0;
            // 
            // cbDatabaseName
            // 
            this.cbDatabaseName.Enabled = false;
            this.cbDatabaseName.FormattingEnabled = true;
            this.cbDatabaseName.Location = new System.Drawing.Point(15, 206);
            this.cbDatabaseName.Name = "cbDatabaseName";
            this.cbDatabaseName.Size = new System.Drawing.Size(385, 21);
            this.cbDatabaseName.TabIndex = 1;
            this.cbDatabaseName.DropDown += new System.EventHandler(this.cbDatabaseName_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server name:";
            // 
            // rbSqlAuth
            // 
            this.rbSqlAuth.AutoSize = true;
            this.rbSqlAuth.Location = new System.Drawing.Point(15, 87);
            this.rbSqlAuth.Name = "rbSqlAuth";
            this.rbSqlAuth.Size = new System.Drawing.Size(173, 17);
            this.rbSqlAuth.TabIndex = 3;
            this.rbSqlAuth.Text = "Use SQL Server Authentication";
            this.rbSqlAuth.UseVisualStyleBackColor = true;
            this.rbSqlAuth.CheckedChanged += new System.EventHandler(this.rbSqlAuth_CheckedChanged);
            // 
            // rbWinAuth
            // 
            this.rbWinAuth.AutoSize = true;
            this.rbWinAuth.Checked = true;
            this.rbWinAuth.Location = new System.Drawing.Point(15, 64);
            this.rbWinAuth.Name = "rbWinAuth";
            this.rbWinAuth.Size = new System.Drawing.Size(162, 17);
            this.rbWinAuth.TabIndex = 3;
            this.rbWinAuth.TabStop = true;
            this.rbWinAuth.Text = "Use Windows Authentication";
            this.rbWinAuth.UseVisualStyleBackColor = true;
            this.rbWinAuth.CheckedChanged += new System.EventHandler(this.rbWinAuth_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Database name:";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(9, 284);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(127, 23);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "Test connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(341, 284);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(260, 284);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // ConnectionDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(428, 319);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.rbWinAuth);
            this.Controls.Add(this.rbSqlAuth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbServerName);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbDatabaseName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbServerName;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.ComboBox cbDatabaseName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbSqlAuth;
        private System.Windows.Forms.RadioButton rbWinAuth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}