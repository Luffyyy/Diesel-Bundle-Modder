namespace PDBundleModPatcher
{
    partial class ManageStrings
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
            this.components = new System.ComponentModel.Container();
            this.stringsCollection_dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dieselStringEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.stringIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stringTextBox = new System.Windows.Forms.TextBox();
            this.commitButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.stringsCollection_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dieselStringEntryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // stringsCollection_dataGridView
            // 
            this.stringsCollection_dataGridView.AllowUserToAddRows = false;
            this.stringsCollection_dataGridView.AllowUserToDeleteRows = false;
            this.stringsCollection_dataGridView.AllowUserToResizeRows = false;
            this.stringsCollection_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringsCollection_dataGridView.AutoGenerateColumns = false;
            this.stringsCollection_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.stringsCollection_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stringsCollection_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.stringsCollection_dataGridView.DataSource = this.dieselStringEntryBindingSource;
            this.stringsCollection_dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.stringsCollection_dataGridView.Location = new System.Drawing.Point(12, 12);
            this.stringsCollection_dataGridView.MultiSelect = false;
            this.stringsCollection_dataGridView.Name = "stringsCollection_dataGridView";
            this.stringsCollection_dataGridView.RowHeadersVisible = false;
            this.stringsCollection_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.stringsCollection_dataGridView.Size = new System.Drawing.Size(561, 350);
            this.stringsCollection_dataGridView.TabIndex = 0;
            this.stringsCollection_dataGridView.SelectionChanged += new System.EventHandler(this.stringsCollection_dataGridView_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn1.HeaderText = "String ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn2.HeaderText = "Text";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dieselStringEntryBindingSource
            // 
            this.dieselStringEntryBindingSource.DataSource = typeof(PDBundleModPatcher.DieselStringEntry);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 371);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "String ID:";
            // 
            // stringIDTextBox
            // 
            this.stringIDTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringIDTextBox.Location = new System.Drawing.Point(69, 368);
            this.stringIDTextBox.Name = "stringIDTextBox";
            this.stringIDTextBox.Size = new System.Drawing.Size(501, 20);
            this.stringIDTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 401);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "String:";
            // 
            // stringTextBox
            // 
            this.stringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringTextBox.Location = new System.Drawing.Point(69, 394);
            this.stringTextBox.Multiline = true;
            this.stringTextBox.Name = "stringTextBox";
            this.stringTextBox.Size = new System.Drawing.Size(501, 106);
            this.stringTextBox.TabIndex = 4;
            // 
            // commitButton
            // 
            this.commitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.commitButton.Location = new System.Drawing.Point(15, 506);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(75, 23);
            this.commitButton.TabIndex = 5;
            this.commitButton.Text = "Add/Set";
            this.commitButton.UseVisualStyleBackColor = true;
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(495, 506);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeButton.Location = new System.Drawing.Point(96, 506);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(153, 23);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "Remove Selected String";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // ManageStrings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 538);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.stringTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stringIDTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stringsCollection_dataGridView);
            this.MaximizeBox = false;
            this.Name = "ManageStrings";
            this.ShowIcon = false;
            this.Text = "Manage Strings";
            this.Load += new System.EventHandler(this.ManageStrings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stringsCollection_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dieselStringEntryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView stringsCollection_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox stringIDTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox stringTextBox;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource dieselStringEntryBindingSource;
    }
}