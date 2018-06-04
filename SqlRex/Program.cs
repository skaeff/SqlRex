using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlRex
{
    static class Program
    {
       
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new MainForm());
        //}

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var tempPath = Application.StartupPath + @"\temp";
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
                Directory.CreateDirectory(tempPath);
            }

#if DEBUG
            Application.Run(new MainForm());
#else
            string[] args = Environment.GetCommandLineArgs();
            SingleInstanceController controller = new SingleInstanceController();
            controller.Run(args);
#endif
        }

        public class SingleInstanceController : WindowsFormsApplicationBase
        {
            public SingleInstanceController()
            {
                IsSingleInstance = true;

                StartupNextInstance += this_StartupNextInstance;
            }

            void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
            {
                var form = MainForm as MainForm; //My derived form type
                form.AppendRecent(e.CommandLine[1]);
                form.RebuildRecent();
                form.OpenScript(e.CommandLine[1]);
            }

            protected override void OnCreateMainForm()
            {
                MainForm = new MainForm();
                string[] args = Environment.GetCommandLineArgs();
                if(args.Length > 1)
                {
                    (MainForm as MainForm).OpenScript(args[1]);
                }
            }
        }
    }
}
