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
    public partial class ExceptionWindowForm : Form
    {
        public ExceptionWindowForm()
        {
            InitializeComponent();
        }

        public static void ShowError(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
