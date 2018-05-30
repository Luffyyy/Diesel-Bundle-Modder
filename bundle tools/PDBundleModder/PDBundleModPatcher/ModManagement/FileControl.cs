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
    public partial class FileControl : Form
    {

        public Dictionary<BundleEntryPath, List<BundleRewriteItem>> filecontrolDictionary { get; set; }
        public Dictionary<BundleEntryPath, string> filecontrolSelectedDictionary { get; set; }
        
        public FileControl()
        {
            InitializeComponent();
        }

        private void FileControl_Load(object sender, EventArgs e)
        {
            this.Text = "File Control (" + this.filecontrolDictionary.Count + ")";
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;

            foreach(KeyValuePair<BundleEntryPath, List<BundleRewriteItem>> kvp in filecontrolDictionary)
            {
                BundleEntryPath key = kvp.Key;
                List<BundleRewriteItem> mods = kvp.Value;

                if (mods.Count(p => !p.ReplacementFile.EndsWith(".script")) == 0)
                    continue;

                String filepath = "";
                String path = StaticStorage.Known_Index.GetAny(key.EntryPath);
                String extension = StaticStorage.Known_Index.GetExtension(key.EntryExtension);

                if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(extension))
                {
                    if (key.IsLanguageSpecific)
                        filepath = path + "." + key.EntryLanguage + "." + extension;
                    else
                        filepath = path + "." + extension;
                }

                List<string> modoptions = new List<string>();

                foreach (BundleRewriteItem bm in mods)
                {
                    modoptions.Add(bm.ModName);
                }

                DataGridViewRow dgvRow = new DataGridViewRow();

                dgvRow.Cells.Add(new DataGridViewTextBoxCell());
                dgvRow.Cells.Add(new DataGridViewComboBoxCell());

                dgvRow.Cells[0].Value = filepath;

                ((DataGridViewComboBoxCell)dgvRow.Cells[1]).DataSource = modoptions;
                ((DataGridViewComboBoxCell)dgvRow.Cells[1]).Tag = key;

                if (filecontrolSelectedDictionary.ContainsKey(key))
                    ((DataGridViewComboBoxCell)dgvRow.Cells[1]).Value = filecontrolSelectedDictionary[key];
                else
                    ((DataGridViewComboBoxCell)dgvRow.Cells[1]).Value = modoptions[0];

                dataGridView1.Rows.Add(dgvRow);

            }

        }

        void dataGridView1_CurrentCellDirtyStateChanged(object sender,
            EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[1];
            if (cb.Value != null)
            {
                // do stuff
                if (filecontrolSelectedDictionary.ContainsKey((BundleEntryPath)cb.Tag))
                    filecontrolSelectedDictionary[(BundleEntryPath)cb.Tag] = (string)cb.Value;
                else
                    filecontrolSelectedDictionary.Add((BundleEntryPath)cb.Tag, (string)cb.Value);

                //dataGridView1.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex != 1)
                return;

            dataGridView1.BeginEdit(true);
            ComboBox comboBox = (ComboBox)dataGridView1.EditingControl;
            comboBox.DroppedDown = true;
        }
    }
}
