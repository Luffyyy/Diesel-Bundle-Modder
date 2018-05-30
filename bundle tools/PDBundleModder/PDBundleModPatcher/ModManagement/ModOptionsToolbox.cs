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
    public partial class ModOptionsToolbox : Form
    {

        public List<ModVariable> modVariables = new List<ModVariable>();

        public ModOptionsToolbox()
        {
            InitializeComponent();
        }

        private void ModOptionsToolbox_Load(object sender, EventArgs e)
        {
            Console.WriteLine();

            foreach (var modVar in modVariables)
            {
                this.variables_ComboBox.Items.Add(modVar);
            }
        }
    }
}
