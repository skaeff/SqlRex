using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    public partial class ConnectionDialog : Form
    {
        public ConnectionDialog()
        {
            InitializeComponent();
        }

        public string ConnectionString
        {
            get
            {
                var csb = new SqlConnectionStringBuilder();
                csb.DataSource = tbServerName.Text;

                csb.IntegratedSecurity = rbWinAuth.Checked;
                if (csb.IntegratedSecurity)
                {
                    csb.UserID = tbUserName.Text;
                    csb.Password = tbPassword.Text;
                }
                csb.InitialCatalog = cbDatabaseName.Text;
                return csb.ConnectionString;
            }
            set
            {
                var csb = new SqlConnectionStringBuilder(value);
                tbServerName.Text = csb.DataSource;
                tbUserName.Text = csb.UserID;
                tbPassword.Text = csb.Password;
                rbWinAuth.Checked = csb.IntegratedSecurity;
                rbSqlAuth.Checked = !csb.IntegratedSecurity;
                cbDatabaseName.Text = csb.InitialCatalog;
            }
        }

        private void SetUserPassEnabled()
        {
            tbPassword.Enabled = rbSqlAuth.Checked;
            tbUserName.Enabled = rbSqlAuth.Checked;
        }

        private void rbWinAuth_CheckedChanged(object sender, EventArgs e)
        {
            SetUserPassEnabled();
        }

        private void rbSqlAuth_CheckedChanged(object sender, EventArgs e)
        {
            SetUserPassEnabled();
        }


        private void cbDatabaseName_DropDown(object sender, EventArgs e)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Open();

                    DataTable databases = con.GetSchema("Databases");

                    cbDatabaseName.Items.Clear();

                    foreach (DataRow database in databases.Rows)
                    {
                        string databaseName = database.Field<String>("database_name");
                        short dbID = database.Field<short>("dbid");
                        DateTime creationDate = database.Field<DateTime>("create_date");

                        cbDatabaseName.Items.Add(databaseName);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionWindowForm.ShowError(ex);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbServerName.Text))
            {
                MessageBox.Show("You have to specify Server Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                }
                MessageBox.Show("Ok", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed: {0}", ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //ExceptionWindowForm.ShowError(ex);
            }
        }

        private void tbServerName_TextChanged(object sender, EventArgs e)
        {
            cbDatabaseName.Enabled = !string.IsNullOrEmpty(tbServerName.Text);
        }
    }
}
