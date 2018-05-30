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
    public partial class ManageStrings : Form
    {

        private Dictionary<string, string> _original = new Dictionary<string, string>();
        private Dictionary<string, string> _originaleditable = new Dictionary<string, string>();
        private Dictionary<string, string> _newuserstrings = new Dictionary<string, string>();

        private List<DieselStringEntry> _stringsView = new List<DieselStringEntry>();
        
        public ManageStrings()
        {
            InitializeComponent();
        }

        public ManageStrings(DieselStrings strings)
        {
            InitializeComponent();

            this._original = strings.strings;
        }

        private void ManageStrings_Load(object sender, EventArgs e)
        {

            _stringsView.Clear();
            foreach (KeyValuePair<string, string> kvp in this._original)
            {
                DieselStringEntry bse = new DieselStringEntry(kvp.Key, kvp.Value);
                _stringsView.Add(bse);
            }

            this.stringsCollection_dataGridView.DataSource = this._stringsView;
            this.stringsCollection_dataGridView.Refresh();
        }

        private void stringsCollection_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.stringsCollection_dataGridView.SelectedRows.Count <= 0)
                return;

            foreach (DataGridViewRow row in this.stringsCollection_dataGridView.SelectedRows)
            {
                DieselStringEntry temp = (DieselStringEntry)row.DataBoundItem;

                this.stringIDTextBox.Text = temp.Key;
                this.stringTextBox.Text = temp.Value;
            }


        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void commitButton_Click(object sender, EventArgs e)
        {

        }

    }
}
