namespace PDBundleModder_Updater
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.update_progress = new System.Windows.Forms.ProgressBar();
            this.update_log_richtext = new System.Windows.Forms.RichTextBox();
            this.spooci = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // update_progress
            // 
            this.update_progress.Location = new System.Drawing.Point(12, 282);
            this.update_progress.Name = "update_progress";
            this.update_progress.Size = new System.Drawing.Size(560, 22);
            this.update_progress.TabIndex = 0;
            // 
            // update_log_richtext
            // 
            this.update_log_richtext.Location = new System.Drawing.Point(12, 12);
            this.update_log_richtext.Name = "update_log_richtext";
            this.update_log_richtext.ReadOnly = true;
            this.update_log_richtext.Size = new System.Drawing.Size(560, 264);
            this.update_log_richtext.TabIndex = 1;
            this.update_log_richtext.Text = "";
            // 
            // spooci
            // 
            this.spooci.AutoSize = true;
            this.spooci.Enabled = false;
            this.spooci.Location = new System.Drawing.Point(470, 303);
            this.spooci.Name = "spooci";
            this.spooci.Size = new System.Drawing.Size(108, 13);
            this.spooci.TabIndex = 2;
            this.spooci.Text = "With love from Spy. :)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 316);
            this.Controls.Add(this.update_log_richtext);
            this.Controls.Add(this.update_progress);
            this.Controls.Add(this.spooci);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 350);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bundle Modder Updater";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar update_progress;
        private System.Windows.Forms.RichTextBox update_log_richtext;
        private System.Windows.Forms.Label spooci;
    }
}

