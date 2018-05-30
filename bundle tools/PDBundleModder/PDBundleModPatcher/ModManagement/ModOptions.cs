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
    public partial class ModOptions : Form
    {
        private bool isEditMode = false;
        ModOptionsToolbox toolbox = new ModOptionsToolbox();

        public List<ModVariable> modVariables = new List<ModVariable>();


        public ModOptions(bool edit = false)
        {
            InitializeComponent();

            isEditMode = edit;
        }

        private void ModOptions_Load(object sender, EventArgs e)
        {

            if (isEditMode)
            {
                this.Text = "[EDIT] Mod Options";
                this.editDragHint_label.Visible = true;
                toolbox.modVariables = this.modVariables;
                toolbox.Location = new Point(this.Left + this.Width, this.Top);
                toolbox.Show(this);
            }

            refreshControls();
        }

        private void refreshControls()
        {
            this.modFormFlowPanel.Controls.Clear();

            Random rand = new Random();

            foreach (var modVar in this.modVariables/*.Where(v => v.displayposition != -1 && v.displaytype != ModVariableDisplayType.None)*/)
            {
                FlowLayoutPanel modVarPanel = new FlowLayoutPanel();
                modVarPanel.AutoSize = true;

                Label modVarLabel = new Label();
                modVarLabel.AutoSize = true;
                modVarLabel.Text = modVar.ToString();

                if (this.isEditMode)
                {
                    modVarPanel.BackColor = SystemColors.ControlDark;
                    modVarPanel.MouseDown += modVarPanel_MouseDown;
                }

                //Add controls to the panel
                modVarPanel.Controls.Add(modVarLabel);

                //Add panel to controls
                this.modFormFlowPanel.Controls.Add(modVarPanel);
            }
        }

        private void modVarPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Determines which item was selected.
            FlowLayoutPanel lb = ((FlowLayoutPanel)sender);
            Point pt = new Point(e.X, e.Y);

            lb.DoDragDrop(lb, DragDropEffects.Move);
        }

        private void modFormFlowPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void modFormFlowPanel_DragDrop(object sender, DragEventArgs e)
        {
            FlowLayoutPanel data = (FlowLayoutPanel)e.Data.GetData(typeof(FlowLayoutPanel));
            FlowLayoutPanel _destination = (FlowLayoutPanel)sender;
            FlowLayoutPanel _source = (FlowLayoutPanel)data.Parent;

            if (_source != _destination)
            {
                // Add control to panel
                _destination.Controls.Add(data);
                data.Size = new Size(_destination.Width, 50);

                // Reorder
                Point p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p);
                int index = _destination.Controls.GetChildIndex(item, false);
                _destination.Controls.SetChildIndex(data, index);

                // Invalidate to paint!
                _destination.Invalidate();
                _source.Invalidate();
            }
            else
            {
                // Just add the control to the new panel.
                // No need to remove from the other panel,
                // this changes the Control.Parent property.
                Point p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p);
                int index = _destination.Controls.GetChildIndex(item, false);
                _destination.Controls.SetChildIndex(data, index);
                _destination.Invalidate();
            }
        }
    }
}
