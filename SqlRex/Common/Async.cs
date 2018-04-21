using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace SqlRex.Common
{
    public class Async
    {
        public static void ExecAsync(Form form, Action<BackgroundWorker> action, Action<TimeSpan> onComplete, bool supportCancel = false)
        {
            var backgroundWorker = new BackgroundWorker();
            
            var txt = form.Text;

            var dic = new Dictionary<string, bool>();

            var focus = FindFocused(form);

            foreach (Control item in form.Controls)
            {
                dic.Add(item.Name, item.Enabled);
                item.Enabled = false;
            }

            var lc = new LongOperationControl(supportCancel);
            lc.Name = "LongOperationControl";
            lc.Location =
                new System.Drawing.Point(
                    form.Size.Width / 2 - lc.Size.Width / 2,
                    form.Size.Height / 2 - lc.Size.Height / 2
                    );
            

            form.Resize += form_Resize;

            form.Controls.Add(lc);
            lc.BringToFront();

            if (supportCancel)
            {
                backgroundWorker.WorkerSupportsCancellation = true;

                lc.Interrupt += (s, e) => backgroundWorker.CancelAsync();
            }

            backgroundWorker.DoWork += (sender, e) => action(backgroundWorker);
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += (sender, args) =>
                                                    {
                                                        var msg = args.UserState.ToString();
                                                        lc.ProgressMessage = msg;
                                                    };
            
            backgroundWorker.RunWorkerCompleted += (sender, e) =>
            {
                form.Text = txt;
                form.Resize -= form_Resize;

                lc.Stop();

                form.Controls.RemoveByKey("LongOperationControl");

                var elapsed = lc.Elapsed;
                lc = null;

                foreach (Control item in form.Controls)
                {
                    item.Enabled = dic[item.Name];
                }

                if (e.Error != null)
                {
                    throw e.Error;
                }

                if (onComplete != null)
                    onComplete(elapsed);

                if (focus != null)
                    focus.Focus();
            };

            

            form.Text = txt + "~";
            backgroundWorker.RunWorkerAsync();
        }

        static void form_Resize(object sender, EventArgs e)
        {
            var form = ((Form)sender);
            var lc = form.Controls["LongOperationControl"];
            lc.Location =
                new System.Drawing.Point(
                    form.Size.Width / 2 - lc.Size.Width / 2,
                    form.Size.Height / 2 - lc.Size.Height / 2
                    );
            
        }

        static Control FindFocused(Control form)
        {
            Control res = null;
            foreach (Control control in form.Controls)
            {
                if (control.Focused)
                    res = control;
                else
                    res = FindFocused(control);

                if (res != null)
                    break;
            }

            return res;
        }
    }
}
