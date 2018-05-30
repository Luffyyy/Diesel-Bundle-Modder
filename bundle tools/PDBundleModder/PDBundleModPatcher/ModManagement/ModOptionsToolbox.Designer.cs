namespace PDBundleModPatcher
{
    partial class ModOptionsToolbox
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
            this.removeFromParent = new System.Windows.Forms.Button();
            this.addToParent = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.variables_ComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.properties_datagridview = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.properties_datagridview)).BeginInit();
            this.SuspendLayout();
            // 
            // removeFromParent
            // 
            this.removeFromParent.Enabled = false;
            this.removeFromParent.Location = new System.Drawing.Point(3, 63);
            this.removeFromParent.Name = "removeFromParent";
            this.removeFromParent.Size = new System.Drawing.Size(75, 23);
            this.removeFromParent.TabIndex = 5;
            this.removeFromParent.Text = "Remove";
            this.removeFromParent.UseVisualStyleBackColor = true;
            // 
            // addToParent
            // 
            this.addToParent.Location = new System.Drawing.Point(176, 63);
            this.addToParent.Name = "addToParent";
            this.addToParent.Size = new System.Drawing.Size(75, 23);
            this.addToParent.TabIndex = 4;
            this.addToParent.Text = "Add/Set";
            this.addToParent.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(82, 36);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(169, 21);
            this.comboBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Control Type: ";
            // 
            // variables_ComboBox
            // 
            this.variables_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.variables_ComboBox.FormattingEnabled = true;
            this.variables_ComboBox.Location = new System.Drawing.Point(60, 9);
            this.variables_ComboBox.Name = "variables_ComboBox";
            this.variables_ComboBox.Size = new System.Drawing.Size(191, 21);
            this.variables_ComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Variable: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Properties:";
            // 
            // properties_datagridview
            // 
            this.properties_datagridview.AllowUserToAddRows = false;
            this.properties_datagridview.AllowUserToDeleteRows = false;
            this.properties_datagridview.AllowUserToResizeColumns = false;
            this.properties_datagridview.AllowUserToResizeRows = false;
            this.properties_datagridview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.properties_datagridview.BackgroundColor = System.Drawing.SystemColors.Control;
            this.properties_datagridview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.properties_datagridview.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.properties_datagridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.properties_datagridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.properties_datagridview.Location = new System.Drawing.Point(3, 105);
            this.properties_datagridview.Name = "properties_datagridview";
            this.properties_datagridview.RowHeadersVisible = false;
            this.properties_datagridview.Size = new System.Drawing.Size(248, 349);
            this.properties_datagridview.TabIndex = 8;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Property";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ModOptionsToolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 466);
            this.Controls.Add(this.properties_datagridview);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.removeFromParent);
            this.Controls.Add(this.addToParent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.variables_ComboBox);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ModOptionsToolbox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Toolbox";
            this.Load += new System.EventHandler(this.ModOptionsToolbox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.properties_datagridview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox variables_ComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button removeFromParent;
        private System.Windows.Forms.Button addToParent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView properties_datagridview;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}