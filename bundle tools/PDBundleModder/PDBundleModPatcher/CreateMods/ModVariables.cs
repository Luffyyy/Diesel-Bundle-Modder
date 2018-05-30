using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PDBundleModPatcher
{
    public partial class ModVariables : Form
    {
        public HashSet<ModVariable> modVars { get; set; }


        public ModVariables(HashSet<ModVariable> passedModVars)
        {
            InitializeComponent();

            modVars = passedModVars;

            this.varType_combobox.SelectedIndex = 0;

            this.removeVarButton.Enabled = (modVars.Count > 0 ? true : false);

            this.modVars_dataGridView.DataSource = getModVariableViews(this.modVars.ToList());
            this.modVars_dataGridView.Refresh();
        }

        private List<ModVariableView> getModVariableViews(List<ModVariable> modVars)
        {
            List<ModVariableView> vars = new List<ModVariableView>();

            foreach(var var in modVars)
                vars.Add(new ModVariableView(var));

            return vars;
        }

        private void varType_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.varType_combobox.SelectedIndex == 0)
            {
                this.varDefaultValue_combobox.DropDownStyle = ComboBoxStyle.DropDownList;
                this.varDefaultValue_combobox.Items.Clear();
                this.varDefaultValue_combobox.Items.Add("True");
                this.varDefaultValue_combobox.Items.Add("False");
                this.varDefaultValue_combobox.SelectedIndex = 0;
            }
            else
            {
                this.varDefaultValue_combobox.DropDownStyle = ComboBoxStyle.Simple;
                this.varDefaultValue_combobox.Items.Clear();
                this.varDefaultValue_combobox.Text = "";
            }

            
        }

        private void addVarButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.varName_textbox.Text))
            {
                MessageBox.Show("You must enter a name for this mod variable.");
                return;
            }
            else if (String.IsNullOrWhiteSpace(this.varDefaultValue_combobox.Text))
            {
                MessageBox.Show("You must enter a default value for this mod variable.");
                return;
            }

            object newVar = new object();

            try
            {
                switch(this.varType_combobox.SelectedIndex)
                {
                    case 0:
                        newVar = Boolean.Parse(this.varDefaultValue_combobox.Text);
                        break;
                    case 1:
                        newVar = this.varDefaultValue_combobox.Text;
                        break;
                    case 2:
                        newVar = float.Parse(this.varDefaultValue_combobox.Text);
                        break;
                    case 3:
                        newVar = Double.Parse(this.varDefaultValue_combobox.Text);
                        break;
                    case 4:
                        newVar = Int16.Parse(this.varDefaultValue_combobox.Text);
                        break;
                    case 5:
                        newVar = UInt16.Parse(this.varDefaultValue_combobox.Text);
                        break;
                    case 6:
                        newVar = Int32.Parse(this.varDefaultValue_combobox.Text);
                        break;
                    case 7:
                        newVar = UInt32.Parse(this.varDefaultValue_combobox.Text);
                        break;
                    case 8:
                        newVar = Int64.Parse(this.varDefaultValue_combobox.Text);
                        break;
                    case 9:
                        newVar = UInt64.Parse(this.varDefaultValue_combobox.Text);
                        break;
                }

            }
            catch(Exception exc)
            {
                MessageBox.Show("Default Variable Value:\n" + this.varDefaultValue_combobox.Text + "\nIs not valid to the " + this.varType_combobox.Text + " data type." +
                    "\n\nException:\n" + exc.Message, "Invalid Default Variable");
                return;
            }

            ModVariable newmodvar = new ModVariable(newVar, this.varName_textbox.Text, this.varDisplayName_textBox.Text);
            if (this.modVars.Contains(newmodvar))
                this.modVars.Remove(newmodvar);
            this.modVars.Add(newmodvar);

            this.removeVarButton.Enabled = (modVars.Count > 0 ? true : false);

            this.modVars_dataGridView.DataSource = getModVariableViews(this.modVars.ToList());
            this.modVars_dataGridView.Refresh();
        }

        private void removeVarButton_Click(object sender, EventArgs e)
        {
            if (this.modVars_dataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show(
                "You did not select any mod variables to remove.",
                "No mod variable selected");
                return;
            }

            foreach (DataGridViewRow row in this.modVars_dataGridView.SelectedRows)
            {
                ModVariable temp = (ModVariable)row.DataBoundItem;

                this.modVars.Remove(temp);
            }

            this.removeVarButton.Enabled = (modVars.Count > 0 ? true : false);

            this.modVars_dataGridView.DataSource = getModVariableViews(this.modVars.ToList());
            this.modVars_dataGridView.Refresh();
        }

        private void modVars_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView gridview_sender = (DataGridView)sender;

            foreach (DataGridViewRow row in this.modVars_dataGridView.SelectedRows)
            {
                ModVariable temp = ((ModVariableView)row.DataBoundItem).tag;

                this.setVarData(temp.var);
                this.varName_textbox.Text = temp.name;
                this.varDisplayName_textBox.Text = temp.displayname;
                this.varInFileRepresentation_TextBox.Text = "{Var:" + temp.name + "}";
            }
        }

        private void setVarData(object variable)
        {
            if (variable is Boolean)
            {
                this.varType_combobox.SelectedIndex = 0;
                this.varDefaultValue_combobox.Text = ((bool)variable).ToString();
            }
            else if (variable is String)
            {
                this.varType_combobox.SelectedIndex = 1;
                this.varDefaultValue_combobox.Text = (variable as String);
            }
            else if (variable is float)
            {
                this.varType_combobox.SelectedIndex = 2;
                this.varDefaultValue_combobox.Text = ((float)variable).ToString();
            }
            else if (variable is double)
            {
                this.varType_combobox.SelectedIndex = 3;
                this.varDefaultValue_combobox.Text = ((double)variable).ToString();
            }
            else if (variable is Int16)
            {
                this.varType_combobox.SelectedIndex = 4;
                this.varDefaultValue_combobox.Text = ((Int16)variable).ToString();
            }
            else if (variable is UInt16)
            {
                this.varType_combobox.SelectedIndex = 5;
                this.varDefaultValue_combobox.Text = ((UInt16)variable).ToString();
            }
            else if (variable is Int32)
            {
                this.varType_combobox.SelectedIndex = 6;
                this.varDefaultValue_combobox.Text = ((Int32)variable).ToString();
            }
            else if (variable is UInt32)
            {
                this.varType_combobox.SelectedIndex = 7;
                this.varDefaultValue_combobox.Text = ((UInt32)variable).ToString();
            }
            else if (variable is Int64)
            {
                this.varType_combobox.SelectedIndex = 8;
                this.varDefaultValue_combobox.Text = ((Int64)variable).ToString();
            }
            else if (variable is UInt64)
            {
                this.varType_combobox.SelectedIndex = 9;
                this.varDefaultValue_combobox.Text = ((UInt64)variable).ToString();
            }
        }

        private void varName_textbox_TextChanged(object sender, EventArgs e)
        {
            varInFileRepresentation_TextBox.Text = "{Var:" + varName_textbox.Text + "}";
        }

        private void varName_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }


    public class ModVariableView
    {
        public string type { get; set; }
        public string name { get; set; }
        public string defVal { get; set; }
        
        public ModVariable tag;

        public ModVariableView(ModVariable modVar)
        {
            this.type = modVar.getVariableTypeString();
            this.name = modVar.name;
            this.defVal = modVar.getVariableValueString();

            this.tag = modVar;
        }
    }
}
