using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SqlRex.Common
{
    public partial class LongOperationControl : UserControl
    {
        private readonly GraphicsPath roundedRectanglePath;
        private readonly Timer operationProgressTimer;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        public EventHandler Interrupt;

        public TimeSpan Elapsed
        {
            get { return elapsedTime; }
        }
        public string ProgressMessage
        {
            get { return lblProgressMessage.Text; }
            set { lblProgressMessage.Text = value; }
        }
        public LongOperationControl(bool supportCancel)
        {
            InitializeComponent();
            Localize();
            //*
            int radius = 10;
            int width = this.Width - radius * 4;
            int height = this.Height - radius * 4;

            lblProgressMessage.Text = "";

            roundedRectanglePath = new GraphicsPath();

          
            roundedRectanglePath.AddLine(radius, 0, width, 0);
            roundedRectanglePath.AddArc(width, 0, 2 * radius, 2 * radius, 270, 90);
            roundedRectanglePath.AddLine(width + 2 * radius, radius, width + 2 * radius, radius + height);
            roundedRectanglePath.AddArc(width, height, 2 * radius, 2 * radius, 0, 90);
            roundedRectanglePath.AddLine(radius, height + 2 * radius, width, height + 2 * radius);
            roundedRectanglePath.AddArc(0, height, 2 * radius, 2 * radius, 90, 90);
            roundedRectanglePath.AddLine(0, radius, 0, radius + height);
            roundedRectanglePath.AddArc(0, 0, 2 * radius, 2 * radius, 180, 90);
           
            this.Region = new Region(roundedRectanglePath);
            //*/
            label1.Text = "Please wait..." + Environment.NewLine + elapsedTime;

            operationProgressTimer = new Timer();
            operationProgressTimer.Interval = 1000;
            operationProgressTimer.Tick += (sender, e) =>
            {
                elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
                label1.Text = "Please wait..." + Environment.NewLine + elapsedTime;
            };
            operationProgressTimer.Start();

            if (supportCancel)
                btnCancel.Visible = true;
            else
                btnCancel.Visible = false;
        }

        internal void Stop()
        {
            if (operationProgressTimer != null)
                operationProgressTimer.Stop();
        }

        void Localize()
        {
            this.label1.Text = "Please wait...";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will cancel operation. Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (Interrupt != null)
                    Interrupt(null, EventArgs.Empty);

                btnCancel.Text = "Cancelling...";
                btnCancel.Enabled = false;
            }
        }
    }
}
