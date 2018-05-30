using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PDBundleModPatcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main(string[] args)
        {
            if (!AppDomain.CurrentDomain.FriendlyName.EndsWith("vshost.exe"))
            {
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            MainForm frm = new MainForm();

            Application.Run(frm);
        }

        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Please report this error and contents of exceptions.log to the author:\n\n" + e.Exception.Message, "Unhandled Thread Exception");
            // here you can log the exception ...

            try
            {
                using (StreamWriter sw_log = new StreamWriter("./exceptions.log", true))
                {
                    sw_log.WriteLine("====");
                    sw_log.WriteLine(DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " Unhandled Thread Exception");
                    sw_log.WriteLine("====");
                    sw_log.WriteLine(e.Exception.ToString());

                }
            }
            catch
            {
                Application.Exit();
            }
        }

        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Please report this error and contents of exceptions.log to the author:\n\n" + (e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
            // here you can log the exception ...

            try
            {
                using (StreamWriter sw_log = new StreamWriter("./exceptions.log", true))
                {
                    sw_log.WriteLine("====");
                    sw_log.WriteLine(DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " Unhandled UI Exception");
                    sw_log.WriteLine("====");
                    sw_log.WriteLine(e.ExceptionObject.ToString());

                }
            }
            catch
            {
                Application.Exit();
            }
        }

    }
}
