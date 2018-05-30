namespace PDBundleModPatcher
{
    partial class SoundPatch
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.soundssearchbox_textbox = new System.Windows.Forms.TextBox();
            this.soundList_checkListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.soundreplacementinformation_groupbox = new System.Windows.Forms.GroupBox();
            this.soundreplacementfile_label = new System.Windows.Forms.Label();
            this.soundreplacementpath_combobox = new System.Windows.Forms.ComboBox();
            this.soundreplacementbrowse_button = new System.Windows.Forms.Button();
            this.soundInformation_groupbox = new System.Windows.Forms.GroupBox();
            this.soundID_label = new System.Windows.Forms.Label();
            this.soundStreamed_label = new System.Windows.Forms.Label();
            this.soundplaybacklength_label = new System.Windows.Forms.Label();
            this.soundlooppoint_label = new System.Windows.Forms.Label();
            this.soundEffects_textbox = new System.Windows.Forms.TextBox();
            this.soundReplacement_needsConvert_checkbox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.soundreplacementinformation_groupbox.SuspendLayout();
            this.soundInformation_groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.soundssearchbox_textbox);
            this.splitContainer1.Panel1.Controls.Add(this.soundList_checkListBox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.soundreplacementinformation_groupbox);
            this.splitContainer1.Panel2.Controls.Add(this.soundInformation_groupbox);
            this.splitContainer1.Size = new System.Drawing.Size(685, 413);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 0;
            // 
            // soundssearchbox_textbox
            // 
            this.soundssearchbox_textbox.Location = new System.Drawing.Point(6, 25);
            this.soundssearchbox_textbox.Name = "soundssearchbox_textbox";
            this.soundssearchbox_textbox.Size = new System.Drawing.Size(219, 20);
            this.soundssearchbox_textbox.TabIndex = 2;
            // 
            // soundList_checkListBox
            // 
            this.soundList_checkListBox.FormattingEnabled = true;
            this.soundList_checkListBox.Location = new System.Drawing.Point(6, 47);
            this.soundList_checkListBox.Name = "soundList_checkListBox";
            this.soundList_checkListBox.Size = new System.Drawing.Size(219, 364);
            this.soundList_checkListBox.TabIndex = 1;
            this.soundList_checkListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.soundList_checkListBox_ItemCheck);
            this.soundList_checkListBox.SelectedIndexChanged += new System.EventHandler(this.soundList_checkListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sounds within this BNK";
            // 
            // soundreplacementinformation_groupbox
            // 
            this.soundreplacementinformation_groupbox.Controls.Add(this.textBox3);
            this.soundreplacementinformation_groupbox.Controls.Add(this.label4);
            this.soundreplacementinformation_groupbox.Controls.Add(this.textBox2);
            this.soundreplacementinformation_groupbox.Controls.Add(this.label3);
            this.soundreplacementinformation_groupbox.Controls.Add(this.textBox1);
            this.soundreplacementinformation_groupbox.Controls.Add(this.label2);
            this.soundreplacementinformation_groupbox.Controls.Add(this.soundReplacement_needsConvert_checkbox);
            this.soundreplacementinformation_groupbox.Controls.Add(this.soundreplacementfile_label);
            this.soundreplacementinformation_groupbox.Controls.Add(this.soundreplacementpath_combobox);
            this.soundreplacementinformation_groupbox.Controls.Add(this.soundreplacementbrowse_button);
            this.soundreplacementinformation_groupbox.Enabled = false;
            this.soundreplacementinformation_groupbox.Location = new System.Drawing.Point(3, 218);
            this.soundreplacementinformation_groupbox.Name = "soundreplacementinformation_groupbox";
            this.soundreplacementinformation_groupbox.Size = new System.Drawing.Size(444, 183);
            this.soundreplacementinformation_groupbox.TabIndex = 11;
            this.soundreplacementinformation_groupbox.TabStop = false;
            this.soundreplacementinformation_groupbox.Text = "Replacement Sound Information";
            // 
            // soundreplacementfile_label
            // 
            this.soundreplacementfile_label.AutoSize = true;
            this.soundreplacementfile_label.Location = new System.Drawing.Point(6, 16);
            this.soundreplacementfile_label.Name = "soundreplacementfile_label";
            this.soundreplacementfile_label.Size = new System.Drawing.Size(104, 13);
            this.soundreplacementfile_label.TabIndex = 7;
            this.soundreplacementfile_label.Text = "Sound Replacement";
            // 
            // soundreplacementpath_combobox
            // 
            this.soundreplacementpath_combobox.FormattingEnabled = true;
            this.soundreplacementpath_combobox.Location = new System.Drawing.Point(116, 13);
            this.soundreplacementpath_combobox.Name = "soundreplacementpath_combobox";
            this.soundreplacementpath_combobox.Size = new System.Drawing.Size(241, 21);
            this.soundreplacementpath_combobox.TabIndex = 8;
            // 
            // soundreplacementbrowse_button
            // 
            this.soundreplacementbrowse_button.Location = new System.Drawing.Point(363, 11);
            this.soundreplacementbrowse_button.Name = "soundreplacementbrowse_button";
            this.soundreplacementbrowse_button.Size = new System.Drawing.Size(75, 23);
            this.soundreplacementbrowse_button.TabIndex = 9;
            this.soundreplacementbrowse_button.Text = "Browse...";
            this.soundreplacementbrowse_button.UseVisualStyleBackColor = true;
            // 
            // soundInformation_groupbox
            // 
            this.soundInformation_groupbox.Controls.Add(this.soundID_label);
            this.soundInformation_groupbox.Controls.Add(this.soundStreamed_label);
            this.soundInformation_groupbox.Controls.Add(this.soundplaybacklength_label);
            this.soundInformation_groupbox.Controls.Add(this.soundlooppoint_label);
            this.soundInformation_groupbox.Controls.Add(this.soundEffects_textbox);
            this.soundInformation_groupbox.Location = new System.Drawing.Point(3, 9);
            this.soundInformation_groupbox.Name = "soundInformation_groupbox";
            this.soundInformation_groupbox.Size = new System.Drawing.Size(444, 201);
            this.soundInformation_groupbox.TabIndex = 10;
            this.soundInformation_groupbox.TabStop = false;
            this.soundInformation_groupbox.Text = "Sound Information";
            // 
            // soundID_label
            // 
            this.soundID_label.AutoSize = true;
            this.soundID_label.Location = new System.Drawing.Point(6, 16);
            this.soundID_label.Name = "soundID_label";
            this.soundID_label.Size = new System.Drawing.Size(80, 13);
            this.soundID_label.TabIndex = 1;
            this.soundID_label.Text = "SoundID: #ID#";
            // 
            // soundStreamed_label
            // 
            this.soundStreamed_label.AutoSize = true;
            this.soundStreamed_label.Location = new System.Drawing.Point(6, 29);
            this.soundStreamed_label.Name = "soundStreamed_label";
            this.soundStreamed_label.Size = new System.Drawing.Size(160, 13);
            this.soundStreamed_label.TabIndex = 2;
            this.soundStreamed_label.Text = "Is Sound Streamed: #streamed#";
            // 
            // soundplaybacklength_label
            // 
            this.soundplaybacklength_label.AutoSize = true;
            this.soundplaybacklength_label.Location = new System.Drawing.Point(6, 42);
            this.soundplaybacklength_label.Name = "soundplaybacklength_label";
            this.soundplaybacklength_label.Size = new System.Drawing.Size(270, 13);
            this.soundplaybacklength_label.TabIndex = 3;
            this.soundplaybacklength_label.Text = "Sound Playback Length (in ms): #length_object.length#";
            // 
            // soundlooppoint_label
            // 
            this.soundlooppoint_label.AutoSize = true;
            this.soundlooppoint_label.Location = new System.Drawing.Point(6, 55);
            this.soundlooppoint_label.Name = "soundlooppoint_label";
            this.soundlooppoint_label.Size = new System.Drawing.Size(246, 13);
            this.soundlooppoint_label.TabIndex = 4;
            this.soundlooppoint_label.Text = "Sound Loop Point (in ms): #loop_object.looppoint#";
            // 
            // soundEffects_textbox
            // 
            this.soundEffects_textbox.Enabled = false;
            this.soundEffects_textbox.Location = new System.Drawing.Point(9, 71);
            this.soundEffects_textbox.Multiline = true;
            this.soundEffects_textbox.Name = "soundEffects_textbox";
            this.soundEffects_textbox.Size = new System.Drawing.Size(429, 123);
            this.soundEffects_textbox.TabIndex = 5;
            // 
            // soundReplacement_needsConvert_checkbox
            // 
            this.soundReplacement_needsConvert_checkbox.AutoSize = true;
            this.soundReplacement_needsConvert_checkbox.Location = new System.Drawing.Point(9, 42);
            this.soundReplacement_needsConvert_checkbox.Name = "soundReplacement_needsConvert_checkbox";
            this.soundReplacement_needsConvert_checkbox.Size = new System.Drawing.Size(95, 17);
            this.soundReplacement_needsConvert_checkbox.TabIndex = 10;
            this.soundReplacement_needsConvert_checkbox.Text = "Convert sound";
            this.soundReplacement_needsConvert_checkbox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Sound Playback Length: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(140, 105);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(111, 20);
            this.textBox1.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Sound Loop Point A:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(116, 131);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(111, 20);
            this.textBox2.TabIndex = 15;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(116, 157);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(111, 20);
            this.textBox3.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Sound Loop Point B:";
            // 
            // SoundPatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 413);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SoundPatch";
            this.ShowIcon = false;
            this.Text = "Sound Patch for #BNK#";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.soundreplacementinformation_groupbox.ResumeLayout(false);
            this.soundreplacementinformation_groupbox.PerformLayout();
            this.soundInformation_groupbox.ResumeLayout(false);
            this.soundInformation_groupbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox soundList_checkListBox;
        private System.Windows.Forms.Label soundID_label;
        private System.Windows.Forms.TextBox soundssearchbox_textbox;
        private System.Windows.Forms.TextBox soundEffects_textbox;
        private System.Windows.Forms.Label soundlooppoint_label;
        private System.Windows.Forms.Label soundplaybacklength_label;
        private System.Windows.Forms.Label soundStreamed_label;
        private System.Windows.Forms.GroupBox soundreplacementinformation_groupbox;
        private System.Windows.Forms.Label soundreplacementfile_label;
        private System.Windows.Forms.ComboBox soundreplacementpath_combobox;
        private System.Windows.Forms.Button soundreplacementbrowse_button;
        private System.Windows.Forms.GroupBox soundInformation_groupbox;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox soundReplacement_needsConvert_checkbox;

    }
}