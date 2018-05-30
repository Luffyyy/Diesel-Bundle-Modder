namespace PDBundleModPatcher
{
    partial class ModOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.closeButton = new System.Windows.Forms.Button();
            this.modFormFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.editDragHint_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(297, 327);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // modFormFlowPanel
            // 
            this.modFormFlowPanel.AllowDrop = true;
            this.modFormFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modFormFlowPanel.Location = new System.Drawing.Point(0, -1);
            this.modFormFlowPanel.Name = "modFormFlowPanel";
            this.modFormFlowPanel.Size = new System.Drawing.Size(385, 322);
            this.modFormFlowPanel.TabIndex = 1;
            this.modFormFlowPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.modFormFlowPanel_DragDrop);
            this.modFormFlowPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.modFormFlowPanel_DragEnter);
            // 
            // editDragHint_label
            // 
            this.editDragHint_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDragHint_label.Location = new System.Drawing.Point(12, 327);
            this.editDragHint_label.Name = "editDragHint_label";
            this.editDragHint_label.Size = new System.Drawing.Size(279, 31);
            this.editDragHint_label.TabIndex = 2;
            this.editDragHint_label.Text = "Note: In edit mode, you can rearrange the order of options by dragging the option" +
    "s around.";
            this.editDragHint_label.Visible = false;
            // 
            // ModOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.editDragHint_label);
            this.Controls.Add(this.modFormFlowPanel);
            this.Controls.Add(this.closeButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 600);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "ModOptions";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Mod Options";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ModOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.FlowLayoutPanel modFormFlowPanel;
        private System.Windows.Forms.Label editDragHint_label;
    }
}