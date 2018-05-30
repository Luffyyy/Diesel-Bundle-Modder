namespace PDBundleModPatcher
{
    partial class ModVariables
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
            this.modVars_dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defValDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modVariableViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.varInFileRepresentation_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.varDefaultValue_combobox = new System.Windows.Forms.ComboBox();
            this.removeVarButton = new System.Windows.Forms.Button();
            this.addVarButton = new System.Windows.Forms.Button();
            this.varType_combobox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.varName_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.varDisplayName_textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.modVars_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modVariableViewBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // modVars_dataGridView
            // 
            this.modVars_dataGridView.AllowUserToAddRows = false;
            this.modVars_dataGridView.AllowUserToDeleteRows = false;
            this.modVars_dataGridView.AllowUserToResizeRows = false;
            this.modVars_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modVars_dataGridView.AutoGenerateColumns = false;
            this.modVars_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.modVars_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn,
            this.defValDataGridViewTextBoxColumn});
            this.modVars_dataGridView.DataSource = this.modVariableViewBindingSource;
            this.modVars_dataGridView.Location = new System.Drawing.Point(3, 4);
            this.modVars_dataGridView.MultiSelect = false;
            this.modVars_dataGridView.Name = "modVars_dataGridView";
            this.modVars_dataGridView.ReadOnly = true;
            this.modVars_dataGridView.RowHeadersVisible = false;
            this.modVars_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.modVars_dataGridView.Size = new System.Drawing.Size(578, 349);
            this.modVars_dataGridView.TabIndex = 0;
            this.modVars_dataGridView.SelectionChanged += new System.EventHandler(this.modVars_dataGridView_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "type";
            this.dataGridViewTextBoxColumn1.HeaderText = "Type";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Variable Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // defValDataGridViewTextBoxColumn
            // 
            this.defValDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.defValDataGridViewTextBoxColumn.DataPropertyName = "defVal";
            this.defValDataGridViewTextBoxColumn.HeaderText = "Default Value";
            this.defValDataGridViewTextBoxColumn.Name = "defValDataGridViewTextBoxColumn";
            this.defValDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modVariableViewBindingSource
            // 
            this.modVariableViewBindingSource.DataSource = typeof(PDBundleModPatcher.ModVariableView);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.varDisplayName_textBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.varInFileRepresentation_TextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.varDefaultValue_combobox);
            this.groupBox1.Controls.Add(this.removeVarButton);
            this.groupBox1.Controls.Add(this.addVarButton);
            this.groupBox1.Controls.Add(this.varType_combobox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.varName_textbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 101);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // varInFileRepresentation_TextBox
            // 
            this.varInFileRepresentation_TextBox.Location = new System.Drawing.Point(120, 72);
            this.varInFileRepresentation_TextBox.Name = "varInFileRepresentation_TextBox";
            this.varInFileRepresentation_TextBox.ReadOnly = true;
            this.varInFileRepresentation_TextBox.Size = new System.Drawing.Size(291, 20);
            this.varInFileRepresentation_TextBox.TabIndex = 15;
            this.varInFileRepresentation_TextBox.Text = "{Var:*NAME*}";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "In-file representation:";
            // 
            // varDefaultValue_combobox
            // 
            this.varDefaultValue_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.varDefaultValue_combobox.FormattingEnabled = true;
            this.varDefaultValue_combobox.Location = new System.Drawing.Point(88, 44);
            this.varDefaultValue_combobox.Name = "varDefaultValue_combobox";
            this.varDefaultValue_combobox.Size = new System.Drawing.Size(147, 22);
            this.varDefaultValue_combobox.TabIndex = 13;
            // 
            // removeVarButton
            // 
            this.removeVarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeVarButton.Location = new System.Drawing.Point(499, 70);
            this.removeVarButton.Name = "removeVarButton";
            this.removeVarButton.Size = new System.Drawing.Size(73, 25);
            this.removeVarButton.TabIndex = 11;
            this.removeVarButton.Text = "Remove";
            this.removeVarButton.UseVisualStyleBackColor = true;
            this.removeVarButton.Click += new System.EventHandler(this.removeVarButton_Click);
            // 
            // addVarButton
            // 
            this.addVarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addVarButton.Location = new System.Drawing.Point(420, 70);
            this.addVarButton.Name = "addVarButton";
            this.addVarButton.Size = new System.Drawing.Size(73, 25);
            this.addVarButton.TabIndex = 10;
            this.addVarButton.Text = "Add/Set";
            this.addVarButton.UseVisualStyleBackColor = true;
            this.addVarButton.Click += new System.EventHandler(this.addVarButton_Click);
            // 
            // varType_combobox
            // 
            this.varType_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.varType_combobox.FormattingEnabled = true;
            this.varType_combobox.Items.AddRange(new object[] {
            "Boolean",
            "String",
            "Float",
            "Double",
            "Short",
            "Unsigned Short",
            "Integer",
            "Unsigned Integer",
            "Long",
            "Unsigned Long"});
            this.varType_combobox.Location = new System.Drawing.Point(50, 17);
            this.varType_combobox.Name = "varType_combobox";
            this.varType_combobox.Size = new System.Drawing.Size(185, 21);
            this.varType_combobox.TabIndex = 5;
            this.varType_combobox.SelectedIndexChanged += new System.EventHandler(this.varType_combobox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Default Value:";
            // 
            // varName_textbox
            // 
            this.varName_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.varName_textbox.Location = new System.Drawing.Point(281, 17);
            this.varName_textbox.Name = "varName_textbox";
            this.varName_textbox.Size = new System.Drawing.Size(291, 20);
            this.varName_textbox.TabIndex = 1;
            this.varName_textbox.TextChanged += new System.EventHandler(this.varName_textbox_TextChanged);
            this.varName_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.varName_textbox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(237, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Display Name:";
            // 
            // varDisplayName_textBox
            // 
            this.varDisplayName_textBox.Location = new System.Drawing.Point(318, 44);
            this.varDisplayName_textBox.Name = "varDisplayName_textBox";
            this.varDisplayName_textBox.Size = new System.Drawing.Size(254, 20);
            this.varDisplayName_textBox.TabIndex = 17;
            // 
            // ModVariables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.modVars_dataGridView);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "ModVariables";
            this.Text = "Mod Variables";
            ((System.ComponentModel.ISupportInitialize)(this.modVars_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modVariableViewBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView modVars_dataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox varType_combobox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox varName_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button removeVarButton;
        private System.Windows.Forms.Button addVarButton;
        private System.Windows.Forms.ComboBox varDefaultValue_combobox;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultvalDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource modVariableViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defValDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox varInFileRepresentation_TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox varDisplayName_textBox;
        private System.Windows.Forms.Label label5;
    }
}