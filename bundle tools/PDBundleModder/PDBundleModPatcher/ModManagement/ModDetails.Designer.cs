namespace PDBundleModPatcher
{
    partial class ModDetails
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
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DescriptionText = new System.Windows.Forms.TextBox();
            this.ModNameText = new System.Windows.Forms.TextBox();
            this.AuthorText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ReplacementFilesGridView = new System.Windows.Forms.DataGridView();
            this.sourceFileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.replacementFileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bundleRewriteItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReplacementFilesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bundleRewriteItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 19);
            this.label5.TabIndex = 17;
            this.label5.Text = "Mod Name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 19);
            this.label4.TabIndex = 16;
            this.label4.Text = "Author:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DescriptionText
            // 
            this.DescriptionText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionText.Location = new System.Drawing.Point(88, 58);
            this.DescriptionText.Multiline = true;
            this.DescriptionText.Name = "DescriptionText";
            this.DescriptionText.ReadOnly = true;
            this.DescriptionText.Size = new System.Drawing.Size(412, 140);
            this.DescriptionText.TabIndex = 14;
            // 
            // ModNameText
            // 
            this.ModNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModNameText.Location = new System.Drawing.Point(88, 9);
            this.ModNameText.Name = "ModNameText";
            this.ModNameText.ReadOnly = true;
            this.ModNameText.Size = new System.Drawing.Size(412, 20);
            this.ModNameText.TabIndex = 12;
            // 
            // AuthorText
            // 
            this.AuthorText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AuthorText.Location = new System.Drawing.Point(88, 32);
            this.AuthorText.Name = "AuthorText";
            this.AuthorText.ReadOnly = true;
            this.AuthorText.Size = new System.Drawing.Size(412, 20);
            this.AuthorText.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 19);
            this.label1.TabIndex = 18;
            this.label1.Text = "Files:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReplacementFilesGridView
            // 
            this.ReplacementFilesGridView.AllowUserToAddRows = false;
            this.ReplacementFilesGridView.AllowUserToDeleteRows = false;
            this.ReplacementFilesGridView.AllowUserToResizeRows = false;
            this.ReplacementFilesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplacementFilesGridView.AutoGenerateColumns = false;
            this.ReplacementFilesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReplacementFilesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sourceFileDataGridViewTextBoxColumn,
            this.replacementFileDataGridViewTextBoxColumn});
            this.ReplacementFilesGridView.DataSource = this.bundleRewriteItemBindingSource;
            this.ReplacementFilesGridView.Location = new System.Drawing.Point(88, 206);
            this.ReplacementFilesGridView.Name = "ReplacementFilesGridView";
            this.ReplacementFilesGridView.ReadOnly = true;
            this.ReplacementFilesGridView.RowHeadersVisible = false;
            this.ReplacementFilesGridView.Size = new System.Drawing.Size(412, 274);
            this.ReplacementFilesGridView.TabIndex = 19;
            // 
            // sourceFileDataGridViewTextBoxColumn
            // 
            this.sourceFileDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sourceFileDataGridViewTextBoxColumn.DataPropertyName = "SourceFile";
            this.sourceFileDataGridViewTextBoxColumn.HeaderText = "Source";
            this.sourceFileDataGridViewTextBoxColumn.Name = "sourceFileDataGridViewTextBoxColumn";
            this.sourceFileDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // replacementFileDataGridViewTextBoxColumn
            // 
            this.replacementFileDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.replacementFileDataGridViewTextBoxColumn.DataPropertyName = "ReplacementFile";
            this.replacementFileDataGridViewTextBoxColumn.HeaderText = "Replacement";
            this.replacementFileDataGridViewTextBoxColumn.Name = "replacementFileDataGridViewTextBoxColumn";
            this.replacementFileDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bundleRewriteItemBindingSource
            // 
            this.bundleRewriteItemBindingSource.DataSource = typeof(PDBundleModPatcher.BundleRewriteItem);
            // 
            // ModDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 492);
            this.Controls.Add(this.ReplacementFilesGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DescriptionText);
            this.Controls.Add(this.ModNameText);
            this.Controls.Add(this.AuthorText);
            this.MinimumSize = new System.Drawing.Size(540, 530);
            this.Name = "ModDetails";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModDetails";
            this.Load += new System.EventHandler(this.ModDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReplacementFilesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bundleRewriteItemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DescriptionText;
        private System.Windows.Forms.TextBox ModNameText;
        private System.Windows.Forms.TextBox AuthorText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ReplacementFilesGridView;
        private System.Windows.Forms.BindingSource bundleRewriteItemBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceFileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn replacementFileDataGridViewTextBoxColumn;
    }
}