using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDBundleModPatcher
{
    public partial class ModDetails : Form
    {

        private BundleMod _mod;

        private List<BundleRewriteItem> _items = new List<BundleRewriteItem>();

        public ModDetails()
        {
            InitializeComponent();
        }

        public ModDetails(BundleMod mod) : this()
        {
            this._mod = mod;
        }

        private void ModDetails_Load(object sender, EventArgs e)
        {
            this.Text = "Mod Details - " + this._mod.Name;

            this.ModNameText.Text = this._mod.Name;
            this.AuthorText.Text = this._mod.Author;
            this.DescriptionText.Text = this._mod.Description;

            foreach (BundleRewriteItem item in this._mod.ItemQueue)
            {
                BundleRewriteItem newBri = new BundleRewriteItem();

                newBri.ReplacementFile = Path.GetFileName(this._mod.file)+"/"+item.ReplacementFile;

                String sourcefile = "";
                String path = StaticStorage.Known_Index.GetAny(item.BundlePath);
                String extension = StaticStorage.Known_Index.GetExtension(item.BundleExtension);

                if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(extension))
                {
                    if (item.IsLanguageSpecific)
                        sourcefile = path + "." + item.BundleLanguage + "." + extension;
                    else
                        sourcefile = path + "." + extension;


                    newBri.SourceFile = sourcefile;

                    _items.Add(newBri);
                }
            }

            this.ReplacementFilesGridView.DataSource = _items;
            this.ReplacementFilesGridView.Update();
        }
    }
}
