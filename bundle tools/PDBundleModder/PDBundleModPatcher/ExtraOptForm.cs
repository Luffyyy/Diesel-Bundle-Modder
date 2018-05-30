using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDBundleModPatcher
{
    public partial class ExtraOptForm : Form
    {
        public ExtraOptForm()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReinstallAllClick(object sender, EventArgs e)
        {
            MainForm.Instance.mark_all(this, "reinstall");
        }

        private void InstallAllClick(object sender, EventArgs e)
        {
            MainForm.Instance.mark_all(this, "install");
        }

        private void UninstallAllClick(object sender, EventArgs e)
        {
            MainForm.Instance.mark_all(this, "uninstall");
        }
    }
}
