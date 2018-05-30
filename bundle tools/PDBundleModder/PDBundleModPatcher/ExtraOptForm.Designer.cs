namespace PDBundleModPatcher
{
    partial class ExtraOptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtraOptForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExtraOptonsGroup = new System.Windows.Forms.GroupBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.InstallAll = new System.Windows.Forms.Button();
            this.ReinstallAllButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ReinstallAllBox = new System.Windows.Forms.PictureBox();
            this.UninstallButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.ExtraOptonsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReinstallAllBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.ExtraOptonsGroup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 166);
            this.panel1.TabIndex = 0;
            // 
            // ExtraOptonsGroup
            // 
            this.ExtraOptonsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtraOptonsGroup.Controls.Add(this.CloseButton);
            this.ExtraOptonsGroup.Controls.Add(this.pictureBox2);
            this.ExtraOptonsGroup.Controls.Add(this.InstallAll);
            this.ExtraOptonsGroup.Controls.Add(this.ReinstallAllButton);
            this.ExtraOptonsGroup.Controls.Add(this.pictureBox1);
            this.ExtraOptonsGroup.Controls.Add(this.ReinstallAllBox);
            this.ExtraOptonsGroup.Controls.Add(this.UninstallButton);
            this.ExtraOptonsGroup.Location = new System.Drawing.Point(3, 3);
            this.ExtraOptonsGroup.Name = "ExtraOptonsGroup";
            this.ExtraOptonsGroup.Size = new System.Drawing.Size(229, 160);
            this.ExtraOptonsGroup.TabIndex = 7;
            this.ExtraOptonsGroup.TabStop = false;
            this.ExtraOptonsGroup.Text = "Extra Options";
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(151, 134);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PDBundleModPatcher.Properties.Resources.accept_icon;
            this.pictureBox2.Location = new System.Drawing.Point(7, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(23, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // InstallAll
            // 
            this.InstallAll.Location = new System.Drawing.Point(32, 19);
            this.InstallAll.Name = "InstallAll";
            this.InstallAll.Size = new System.Drawing.Size(75, 23);
            this.InstallAll.TabIndex = 5;
            this.InstallAll.Text = "Install All";
            this.InstallAll.UseVisualStyleBackColor = true;
            this.InstallAll.Click += new System.EventHandler(this.InstallAllClick);
            // 
            // ReinstallAllButton
            // 
            this.ReinstallAllButton.Location = new System.Drawing.Point(32, 48);
            this.ReinstallAllButton.Name = "ReinstallAllButton";
            this.ReinstallAllButton.Size = new System.Drawing.Size(75, 23);
            this.ReinstallAllButton.TabIndex = 0;
            this.ReinstallAllButton.Text = "Reinstall All";
            this.ReinstallAllButton.UseVisualStyleBackColor = true;
            this.ReinstallAllButton.Click += new System.EventHandler(this.ReinstallAllClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PDBundleModPatcher.Properties.Resources.remove_icon;
            this.pictureBox1.Location = new System.Drawing.Point(7, 77);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // ReinstallAllBox
            // 
            this.ReinstallAllBox.Image = global::PDBundleModPatcher.Properties.Resources.warning_icon;
            this.ReinstallAllBox.Location = new System.Drawing.Point(7, 48);
            this.ReinstallAllBox.Name = "ReinstallAllBox";
            this.ReinstallAllBox.Size = new System.Drawing.Size(23, 23);
            this.ReinstallAllBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ReinstallAllBox.TabIndex = 1;
            this.ReinstallAllBox.TabStop = false;
            // 
            // UninstallButton
            // 
            this.UninstallButton.Location = new System.Drawing.Point(32, 77);
            this.UninstallButton.Name = "UninstallButton";
            this.UninstallButton.Size = new System.Drawing.Size(75, 23);
            this.UninstallButton.TabIndex = 3;
            this.UninstallButton.Text = "Uninstall All";
            this.UninstallButton.UseVisualStyleBackColor = true;
            this.UninstallButton.Click += new System.EventHandler(this.UninstallAllClick);
            // 
            // ExtraOptForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 166);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(550, 364);
            this.MinimumSize = new System.Drawing.Size(251, 205);
            this.Name = "ExtraOptForms";
            this.Text = "Extra Options";
            this.panel1.ResumeLayout(false);
            this.ExtraOptonsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReinstallAllBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ReinstallAllBox;
        private System.Windows.Forms.Button ReinstallAllButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button InstallAll;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button UninstallButton;
        private System.Windows.Forms.GroupBox ExtraOptonsGroup;
    }
}