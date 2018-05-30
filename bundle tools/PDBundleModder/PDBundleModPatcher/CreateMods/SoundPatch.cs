using SoundBankParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PDBundleModPatcher
{
    public partial class SoundPatch : Form
    {
        private BNK soundbank = new BNK();
        private string title = "";

        /// <summary>
        ///     The progress timer.
        /// </summary>
        private System.Windows.Forms.Timer progressTimer = new System.Windows.Forms.Timer();

        /// <summary>
        ///     The rewriter thread.
        /// </summary>
        private Thread bnkloadingThread;


        public SoundPatch()
        {
            InitializeComponent();
        }

        public SoundPatch(string title, MemoryStream bankStream)
        {
            InitializeComponent();

            this.title = title;
            Text = "Loading soundbank: " + this.title;


            progressTimer = new System.Windows.Forms.Timer();
            progressTimer.Interval = 500;
            progressTimer.Tick += this.BNKLoadingTimerElapsed;
            progressTimer.Enabled = true;
            progressTimer.Start();
            bnkloadingThread = new Thread(() => soundbank.LoadBNK(bankStream));
            bnkloadingThread.IsBackground = true;
            bnkloadingThread.Start();
        }


        private void BNKLoadingTimerElapsed(object sender, EventArgs args)
        {
            if (this.soundbank == null)
            {
                this.progressTimer.Stop();
                return;
            }
            this.soundbank.ProgressMutex.WaitOne();

            Text = "Loading soundbank: " + title + " " + soundbank.loadPercent + "%";

            if (this.soundbank.isLoaded)
            {
                Text = "Sound Patch for " + this.title;
                this.soundList_checkListBox.Items.Clear();
                this.soundList_checkListBox.Items.AddRange(SoundBankParser.StaticStorage.soundfiles.Values.ToArray());

                this.soundbank.ProgressMutex.ReleaseMutex();
                this.progressTimer.Enabled = false;
                this.progressTimer.Stop();

                return;
            }

            this.soundbank.ProgressMutex.ReleaseMutex();
        }


        private void soundList_checkListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            return;
        }

        private void soundList_checkListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SoundFile selected = (this.soundList_checkListBox.SelectedItem as SoundFile);
            double[] loops = selected.GetLooppoints();

            this.soundID_label.Text = "SoundID: " + selected.id;
            this.soundStreamed_label.Text = "Is Sound Streamed: " + selected.streamed.ToString();
            this.soundplaybacklength_label.Text = "Sound Playback Length (in ms): " + selected.GetLength();
            this.soundlooppoint_label.Text = "Sound Loop Point (in ms): " + loops[0] + ", " + loops[1];
            this.soundEffects_textbox.Text = selected.effects;

        }
    }
}
