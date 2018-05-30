using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDBundleModder_Updater
{
    /// <summary>
    ///     The static storage.
    /// </summary>
    public static class StaticStorage
    {
        #region Static Fields

        /// <summary>
        ///     The index.
        /// </summary>
        public static UpdateManager manager;

        #endregion
    }
    
    static class Program
    {
        private static bool isSilent = false;
        private static string version = "";


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                if (String.IsNullOrWhiteSpace(arg))
                    continue;

                if (arg.Equals("-silent"))
                {
                    isSilent = true;
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(version))
                    {
                        version = arg;
                    }
                }
            }
           

            if (args.Length >= 1 && !String.IsNullOrWhiteSpace(version))
            {
                StaticStorage.manager = new UpdateManager(version);
                if (StaticStorage.manager.checkUpdate())
                {
                    DialogResult result = MessageBox.Show("A new version of Bundle Modder is available (" + StaticStorage.manager.getUpdateVersion() + ") with following changes:\r\n\r\n" + StaticStorage.manager.getUpdateChanges() + "\r\n\r\nWould you like to install this update?", "Bundle Modder Updater", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                    if (result == DialogResult.Yes)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm());
                    }
                }
                else
                {
                    if (!isSilent)
                        MessageBox.Show("You have latest version of Bundle Modder installed.", "Bundle Modder Updater", MessageBoxButtons.OK);
                }
            }
            else
            {
                StaticStorage.manager = new UpdateManager();

                if (!isSilent)
                    MessageBox.Show("Application launched incorrectly, no version was specified.\r\nPlease use Bundle Modder if you want to check for updates.\r\nOptions -> \"Check for Updates\"", "Bundle Modder Updater", MessageBoxButtons.OK);
            }
        }
    }
}
