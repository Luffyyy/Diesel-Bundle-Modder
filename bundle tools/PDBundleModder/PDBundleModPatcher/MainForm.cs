// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="">
//   
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PDBundleModPatcher
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    using DieselBundle;
    using DieselBundle.Utils;

    using Ionic.Zip;

    using Timer = System.Windows.Forms.Timer;
    using System.Runtime.Serialization.Formatters.Binary;
    using Newtonsoft.Json;
    using System.Drawing;
    using System.Reflection;
    using PDBundleModPatcher.Properties;
    using PDBundleModPatcher.ModManagement;
    using Microsoft.WindowsAPICodePack.Taskbar;
    using SoundBankParser;

    /// <summary>
    ///     The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants

        /// <summary>
        ///     The settings file.
        /// </summary>
        public const string SettingsFile = "./settings.xml";

        /// <summary>
        ///     The installed mods file.
        /// </summary>
        private const string ModsFile = "./mods.json";

        /// <summary>
        ///     The hashlist file.
        /// </summary>
        private const string HashlistFile = "./hashlist";

        /// <summary>
        ///     The local mods directory.
        /// </summary>
        private const string LocalModsDir = "./mods/";

        /// <summary>
        ///     The program title.
        /// </summary>
        private const string ProgramTitle = "PAYDAY Bundle Modder";

        /// <summary>
        ///     The current version full text.
        /// </summary>
        private static string FullVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        ///     The current version.
        /// </summary>
        private static double Version = double.Parse(
            FullVersion.Substring(0, 2) + FullVersion.Substring(2).Replace(".", ""), NumberStyles.Number, CultureInfo.InvariantCulture);


        /// <summary>
        ///     The compatible versions.
        /// </summary>
        public double[] Versions = { Version, 1.1630, 1.16296, 1.162951, 1.1629, 1.1628, 1.1627, 1.1626, 1.1625, 1.1623, 1.16, 1.15, 1.14 };

        #endregion

        #region Static Fields

        /// <summary>
        ///     Gets he instance.
        /// </summary>
        public static MainForm Instance;

        #endregion

        #region Fields

        public bool HashlistLoaded { get; set; }

        public bool ValidAssets { get; set; }

        /// <summary>
        ///     The new mod.
        /// </summary>
        private BundleMod newMod = new BundleMod();

        /// <summary>
        ///     The mods list to apply.
        /// </summary>
        private Dictionary<string, BundleMod> applyModsList = new Dictionary<string, BundleMod>();

        /// <summary>
        ///     The mods databse.
        /// </summary>
        private ModsDatabase mods_db = new ModsDatabase();

        /// <summary>
        ///     The marked for installation mods list.
        /// </summary>
        private Dictionary<string, BundleMod> markedInstallModsList = new Dictionary<string, BundleMod>();

        /// <summary>
        ///     The marked for removal mods list.
        /// </summary>
        private Dictionary<string, BundleMod> markedRemovalModsList = new Dictionary<string, BundleMod>();

        /// <summary>
        ///     The file control for modded files.
        /// </summary>
        private Dictionary<BundleEntryPath, List<BundleRewriteItem>> filecontrolDictionary = new Dictionary<BundleEntryPath, List<BundleRewriteItem>>();

        /// <summary>
        ///     The selected file control for modded files.
        /// </summary>
        private Dictionary<BundleEntryPath, string> filecontrolSelectedDictionary = new Dictionary<BundleEntryPath, string>();

        /// <summary>
        ///     The sharedfoldername.
        /// </summary>
        private String sharedfoldername = "Bundle_Modder_Shared";


        /// <summary>
        ///     The progress timer.
        /// </summary>
        private Timer progressTimer = new Timer();

        /// <summary>
        ///     The current mod.
        /// </summary>
        private BundleMod currentMod;

        /// <summary>
        ///     The last rewrite item.
        /// </summary>
        private BundleRewriteItem lastRewriteItem;

        /// <summary>
        ///     The mod zip file.
        /// </summary>
        private string modZipFile;

        /// <summary>
        ///     The mod script path.
        /// </summary>
        private string modScriptPath;

        /// <summary>
        ///     The rewriter.
        /// </summary>
        private BundleRewriter rewriter;

        /// <summary>
        ///     The rewriter thread.
        /// </summary>
        private Thread rewriterThread;

        /// <summary>
        ///     The saved state apply mod.
        /// </summary>
        private bool savedStateApplyMod;

        /// <summary>
        ///     The saved state open mod.
        /// </summary>
        private bool savedStateOpenMod;

        /// <summary>
        ///     The saved state refresh button.
        /// </summary>
        private bool savedStateRefreshButton;

        /// <summary>
        ///     The saved state file control button.
        /// </summary>
        private bool savedStateFileControlButton;

        /// <summary>
        ///     Tested Assets?
        /// </summary>
        private bool testedAssets = false;

        /// <summary>
        ///     Allow check of listBox checkboxes
        /// </summary>
        private bool allowCheck = false;

        /// <summary>
        ///     Toggles the usage of a watermark in search textbox
        /// </summary>
        private bool searchWatermark = true;

        /// <summary>
        ///     Last selected bundle mod
        /// </summary>
        private BundleMod _selectedMod;

        /// <summary>
        ///     Rightclicked bundle mod
        /// </summary>
        private BundleMod _rightclickedMod;

        /// <summary>
        ///     Icons to be used with listView
        /// </summary>
        private ImageList _utility_icons = new ImageList();

        /// <summary>
        ///     Report from the checkBundles
        /// </summary>
        private Dictionary<String, String> checkedBundlesReport = new Dictionary<string, string>();

        /// <summary>
        ///     Date when the report was performed.
        /// </summary>
        private DateTime checkedDate;


        /// <summary>
        ///     The packages list.
        /// </summary>
        private List<String> packagesList = new List<String>();

        /// <summary>
        ///     The file paths autocomplete collection.
        /// </summary>
        private AutoCompleteStringCollection pathsAutoCompleteCollection = new AutoCompleteStringCollection();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
        {
            Instance = this;
            this.InitializeComponent();

            this.AboutLabel.Text = String.Format(this.AboutLabel.Text, FullVersion);

            var watch = Stopwatch.StartNew();
            this.LoadSettings();

            watch.Stop();
            StaticStorage.log.WriteLine("LoadSettings - " + watch.ElapsedMilliseconds + " ms");

            watch.Restart();
            this.LoadHashList();

            watch.Stop();
            StaticStorage.log.WriteLine("LoadHashList - " + watch.ElapsedMilliseconds + " ms");

            watch.Restart();
            this.LoadPackageNames();

            watch.Stop();
            StaticStorage.log.WriteLine("LoadPackageNames - " + watch.ElapsedMilliseconds + " ms");

            watch.Restart();
            this.mods_db = new ModsDatabase(LocalModsDir, ModsFile, Version);
            this.mods_db.LoadModBackups();

            watch.Stop();
            StaticStorage.log.WriteLine("LoadModBackups - " + watch.ElapsedMilliseconds + " ms");

            watch.Restart();
            this.mods_db.LoadMods();
            this.updateModsListView();

            watch.Stop();
            StaticStorage.log.WriteLine("LoadLocalMods - " + watch.ElapsedMilliseconds + " ms");

            updateTitle();

            if (StaticStorage.settings.CheckForUpdatesOnLaunch)
                checkForUpdate();
        }

        #endregion

        #region Methods

        private void checkForUpdate(bool silent = true)
        {
            if (File.Exists("PDBundleModPatcher_Updater.exe"))
                try
                {
                    Process.Start("PDBundleModPatcher_Updater.exe", string.Format("\"{0}\" \"{1}\"", Version.ToString(CultureInfo.InvariantCulture), (silent ? "-silent" : "")));
                }
                catch (Exception exc)
                {
                    StaticStorage.log.WriteLine("ERROR: Could not open the Updater exe");
                    StaticStorage.log.WriteLine(exc.Message);
                }
            else
            {
                this.checkupdatesonstartup_checkbox.Enabled = false;
                this.checkForUpdates_Button.Enabled = false;
            }
        }

        private void updateTitle(string text = "")
        {
            this.Text = ProgramTitle + " " + FullVersion;

            if (StaticStorage.settings != null && StaticStorage.settings.Game != null && !String.IsNullOrWhiteSpace(StaticStorage.settings.Game))
                this.Text += " - " + StaticStorage.settings.Game;

            if (!String.IsNullOrWhiteSpace(text))
                this.Text += " " + text;
        }

        /// <summary>
        ///     The about label_ link clicked.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void AboutLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://bitbucket.org/zabb65/payday-2-modding-information/");
        }

        /// <summary>
        ///     The add bundle changes.
        /// </summary>
        /// <param name="ids">
        ///     The ids.
        /// </param>
        /// <param name="path">
        ///     The path.
        /// </param>
        /// <param name="extension">
        ///     The extension.
        /// </param>
        /// <param name="language">
        ///     The language.
        /// </param>
        /// <param name="languageSpecific">
        ///     The language_specific.
        /// </param>
        private void AddBundleChanges(
            HashSet<uint> ids,
            ulong path,
            ulong extension,
            uint language,
            bool languageSpecific,
            int replacementType)
        {
            var rewriteItem = new BundleRewriteItem
            {
                BundleExtension = extension,
                BundlePath = path,
                BundleLanguage = language,
                IsLanguageSpecific = languageSpecific,
                SourceFile = BundleFileName.Text.ToLower().Replace('\\', '/'),
                ReplacementFile = this.ReplacementFileName.Text,
                ReplacementType = replacementType
            };
            this.newMod.ItemQueue.Add(rewriteItem);
            this.lastRewriteItem = rewriteItem;
            this.UndoLast.Enabled = true;
        }

        /// <summary>
        ///     The add replacement button_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void AddReplacementButtonClick(object sender, EventArgs e)
        {
            if (this.BundleFileName.Text.Length <= 0)
            {
                MessageBox.Show("Please fill in a bundle file to replace.");
                return;
            }

            if (this.ReplacementFileName.Text.Length <= 0 || !File.Exists(this.ReplacementFileName.Text))
            {
                MessageBox.Show("Must select a replacement file, and it must exist.");
                return;
            }
            bool isLanguageSpecific = (Path.GetFileName(this.BundleFileName.Text).Split('.').Length == 3);

            ulong path, extension;
            uint language;
            if (!this.CheckBundleFileName(this.BundleFileName.Text.ToLower().Replace('\\', '/'), out path, out extension, out language))
            {
                MessageBox.Show(
                    "Could not locate file within the bundle system. Make sure that you entered the name correctly.\n"
                    + "If using text based extensions for JPMod style names, make sure you are using the engine correct endings. For example, use .texture instead of .dds");
                return;
            }

            HashSet<uint> bundleIds = StaticStorage.Index.Entry2Id(
                path,
                extension,
                language,
                isLanguageSpecific);
            if (bundleIds.Count < 1)
            {
                MessageBox.Show(
                    "Could not locate file within the bundle system. Make sure that you entered the name correctly.\n"
                    + "If using text based extensions for JPMod style names, make sure you are using the engine correct endings. For example, use .texture instead of .dds");
                return;
            }

            if (fileReplacementType_ComboBox.Text.Equals("Sound Bank Patch")) //a very stupid way to check this. kill me. can do
            {
                //call SoundPatch here... send and retrieve the objects
                //assign them to the mod
            }
            else if (fileReplacementType_ComboBox.Text.Equals("Strings Patch"))
            {
                //call StringsPatch here... send and retrieve objects
                //assign them to the mod
            }
            else
            {
                this.AddBundleChanges(bundleIds, path, extension, language, isLanguageSpecific, this.fileReplacementType_ComboBox.SelectedIndex);
            }

            this.AddedFilesView.DataSource = this.newMod.ItemQueue.ToList();
            this.AddedFilesView.Refresh();
        }

        /// <summary>
        ///     The apply button_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void ApplyButtonClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(StaticStorage.settings.GameProcess))
            {
                Process[] pname = Process.GetProcessesByName(StaticStorage.settings.GameProcess);
                if (pname.Length != 0)
                {
                    MessageBox.Show(StaticStorage.settings.Game + " is currently running, cannot patch.\nPlease close the game before applying mods.");
                    return;
                }
            }

            if (!this.ValidAssets)
            {
                MessageBox.Show("Your assets folder appears to be invalid. Please select a valid assets folder in Options.", "Invalid assets folder.");
                return;
            }
            if (this.rewriter != null)
                return;
            if (this.ExtractTimer != null)
            {
                if (this.ExtractTimer.Enabled)
                {
                    MessageBox.Show("Cannot perform 2 actions at once");
                    return;
                }
            }

            List<BundleMod> uninstall = new List<BundleMod>();
            List<BundleMod> reinstall = new List<BundleMod>();
            List<BundleMod> install = new List<BundleMod>();
            List<BundleMod> install_combo = new List<BundleMod>();
            Queue<BundleRewriteItem> mix_bri = new Queue<BundleRewriteItem>();
            BundleMod mix = new BundleMod();

            // 0 = Nothing, 1 = Marked for install (limegreen), 2 = Marked for removal (red), 3 = Marked for reinstallation (yellow), 4 = 
            Dictionary<string, BundleMod> modsList = this.mods_db.modsList;

            install = modsList.Values.Where(mod => mod.actionStatus == BundleMod.ModActionStatus.Install).ToList();
            reinstall = modsList.Values.Where(mod => mod.actionStatus == BundleMod.ModActionStatus.Reinstall || mod.actionStatus == BundleMod.ModActionStatus.ForcedReinstall).ToList();
            uninstall = modsList.Values.Where(mod => mod.actionStatus == BundleMod.ModActionStatus.Remove).ToList();

            //Perform other actions

            if (uninstall.FindIndex(mod => mod.type == BundleMod.ModType.lua) != -1)
            {
                DialogResult dlgres = MessageBox.Show("You have selected to uninstall some BLT mods, these will be completely deleted.\nDo you want to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                if (dlgres == DialogResult.No)
                    return;
            }

            this.TotalProgressLabel.Text = "Preparing to Apply Changes";

            this.savedStateApplyMod = this.ApplyButton.Enabled;
            this.savedStateOpenMod = this.OpenModButton.Enabled;
            this.savedStateRefreshButton = this.refreshButton.Enabled;
            this.savedStateFileControlButton = this.filecontrol_button.Enabled;

            this.availiableMods_listView.Enabled = false;
            this.availiableModsSearch_textbox.Enabled = false;
            this.ApplyButton.Enabled = false;
            this.OpenModButton.Enabled = false;
            this.refreshButton.Enabled = false;
            this.filecontrol_button.Enabled = false;
            this.ExtraOptions1.Enabled = false;

            //Generate install/reinstall/uninstall mods
            if (uninstall.Count > 0)
            {
                //Generate Bundle Mod
                foreach (BundleMod uninstMod in uninstall)
                {
                    if (uninstMod.type == BundleMod.ModType.lua)
                    {
                        Directory.Delete(uninstMod.file, true);
                    }
                    else
                    {
                        foreach (BundleRewriteItem bri in uninstMod.ItemQueue)
                        {
                            if (uninstMod.type == BundleMod.ModType.mod_override)
                            {
                                if (bri.isOverrideable()
                                //&& !bri.ReplacementFile.EndsWith(".script")
                                )
                                {

                                    if (!string.IsNullOrEmpty(StaticStorage.Known_Index.GetPath(bri.BundlePath)) && !string.IsNullOrEmpty(StaticStorage.Known_Index.GetExtension(bri.BundleExtension)))
                                    {
                                        string extrname = "";
                                        extrname += StaticStorage.Known_Index.GetPath(bri.BundlePath);
                                        extrname += "." + StaticStorage.Known_Index.GetExtension(bri.BundleExtension);


                                        string modName = bri.ModName;
                                        modName = Path.GetInvalidFileNameChars().Aggregate(modName, (current, c) => current.Replace(c.ToString(), "_"));


                                        if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", modName)))
                                        {
                                            if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", modName, extrname)))
                                            {
                                                File.Delete(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", modName, extrname));


                                                DeleteEmptyDirs(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", modName));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!mix_bri.Contains(bri))
                                {
                                    BundleRewriteItem newBri = bri;
                                    newBri.toRemove = true;
                                    mix_bri.Enqueue(newBri);
                                }
                            }
                        }
                        if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", uninstMod.getEscapedName(), "mod.txt")))
                        {
                            File.Delete(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", uninstMod.getEscapedName(), "mod.txt"));
                        }
                    }
                }
            }
            if (reinstall.Count > 0)
            {
                //Generate Bundle Mod
                foreach (BundleMod bm in reinstall)
                {
                    foreach (BundleRewriteItem bri in bm.ItemQueue)
                    {
                        if (!bri.ReplacementFile.EndsWith(".script") && filecontrolSelectedDictionary.ContainsKey(bri.getBundleEntryPath()))
                        {
                            if (filecontrolSelectedDictionary[bri.getBundleEntryPath()] != bm.Name)
                            {
                                BundleRewriteItem newBri = bri;
                                newBri.toRemove = true;
                                mix_bri.Enqueue(newBri);
                                continue;
                            }
                        }

                        if (bri.toReinstall && !mix_bri.Contains(bri))
                        {
                            BundleRewriteItem newBri = bri;
                            newBri.toRemove = false;
                            mix_bri.Enqueue(newBri);
                        }
                    }

                    if (bm.UtilizesOverride)
                    {
                        if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", bm.getEscapedName())) && File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", bm.getEscapedName(), "mod.txt")))
                        {
                            using (StreamWriter modsw = File.CreateText(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", bm.getEscapedName(), "mod.txt")))
                            {
                                modsw.Write(OverrideMod.Serialize(new OverrideMod(bm.Name, bm.Author, bm.Description)));
                            }
                        }
                    }
                }
            }
            if (install.Count > 0)
            {
                //Generate Bundle Mod
                foreach (BundleMod bm in install)
                {
                    foreach (BundleRewriteItem bri in bm.ItemQueue)
                    {

                        if (!bri.ReplacementFile.EndsWith(".script") && filecontrolSelectedDictionary.ContainsKey(bri.getBundleEntryPath()))
                        {
                            if (filecontrolSelectedDictionary[bri.getBundleEntryPath()] != bm.Name)
                                continue;
                        }

                        if (!mix_bri.Contains(bri))
                        {
                            BundleRewriteItem newBri = bri;
                            newBri.toRemove = false;
                            mix_bri.Enqueue(newBri);
                        }
                    }

                    if (bm.UtilizesOverride)
                    {
                        if (!Directory.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", bm.getEscapedName())))
                            Directory.CreateDirectory(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", bm.getEscapedName()));

                        using (StreamWriter modsw = File.CreateText(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides", bm.getEscapedName(), "mod.txt")))
                        {
                            modsw.Write(OverrideMod.Serialize(new OverrideMod(bm.Name, bm.Author, bm.Description)));
                        }
                    }
                }
            }
            install_combo.AddRange(install);
            install_combo.AddRange(reinstall);

            mix.ItemQueue = new HashSet<BundleRewriteItem>(mix_bri.ToList());

            int BufferSize;
            if (!Int32.TryParse(this.patchingBufferSize.Text, out BufferSize))
            {
                BufferSize = 4096;
            }

            this.rewriter = new BundleRewriter(
                "",
                mix,
                BufferSize,
                StaticStorage.settings.AssetsFolder,
                StaticStorage.settings.BackupType,
                StaticStorage.settings.OverrideFolder,
                StaticStorage.settings.OverrideFolderDummies,
                StaticStorage.settings.OverrideFolderShared,
                true,
                false);

            this.progressTimer = new Timer();
            this.progressTimer.Interval = 500;
            this.progressTimer.Tick += this.MultiProgressTimerElapsed;
            this.progressTimer.Enabled = true;
            this.progressTimer.Start();
            rewriterThread = new Thread(() => this.rewriter.ApplyChanges(install_combo.ToArray(), false));
            rewriterThread.IsBackground = true;
            rewriterThread.Start();

        }

        private void DeleteEmptyDirs(string dir)
        {
            if (String.IsNullOrEmpty(dir))
                throw new ArgumentException(
                    "Starting directory is a null reference or an empty string",
                    "dir");

            if (!Directory.Exists(dir))
                return;

            try
            {
                foreach (var d in Directory.EnumerateDirectories(dir))
                {
                    DeleteEmptyDirs(d);
                }

                var entries = Directory.EnumerateFileSystemEntries(dir);

                if (!entries.Any())
                {
                    try
                    {
                        Directory.Delete(dir);
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (DirectoryNotFoundException) { }
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        /// <summary>
        ///     The asset folder button_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void AssetFolderButtonClick(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();

            folderDialog.ShowNewFolderButton = false;
#if LINUX
#else
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
#endif
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                StaticStorage.settings.AssetsFolder = folderDialog.SelectedPath;
                if (this.TestAssetsFolder())
                {
                    this.SaveSettings();
                    this.LoadHashList();
                }
            }
        }

        /// <summary>
        ///     The browse for replacement_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void BrowseForReplacementClick(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.ReplacementFileName.Text = openDialog.FileName;
        }

        /// <summary>
        ///     The check bundle file name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="path">
        ///     The path.
        /// </param>
        /// <param name="extension">
        ///     The extension.
        /// </param>
        /// <param name="language">
        ///     The language.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        private bool CheckBundleFileName(string name, out ulong path, out ulong extension, out uint language)
        {
            path = 0;
            extension = 0;
            language = 0;
            string[] pieces = name.Split('.');
            if (this.JPModStyleName.Checked)
            {
                if (pieces.Length != 3)
                {
                    MessageBox.Show(
                        "JPMod style names must be in the format of pathhash.language.extension. Extensions may be text extensions, or the hash of the extension.");
                    return false;
                }

                try
                {
                    path = this.SwapEndianess(ulong.Parse(pieces[0], NumberStyles.HexNumber));
                    language = uint.Parse(pieces[1], NumberStyles.HexNumber);
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Filename was not in the correct format, check for typos and try adding the file again. Number conversion failed.");
                    return false;
                }

                try
                {
                    extension = this.SwapEndianess(ulong.Parse(pieces[2], NumberStyles.HexNumber));
                }
                catch (Exception)
                {
                    extension = Hash64.HashString(pieces[2]);
                }
            }
            else
            {
                if (pieces.Length < 2)
                {
                    MessageBox.Show("Bundle file name must contain at least a path and an extension.");
                    return false;
                }
                else if (pieces.Length > 3)
                {
                    MessageBox.Show(
                        "Language specific files must be in the format of path.language.extension.");
                    return false;
                }

                path = Hash64.HashString(pieces[0]);

                if (pieces.Length == 3)
                {
                    if ((language = StaticStorage.Index.Lang2Id(Hash64.HashString(pieces[1]))) == 0)
                    {
                        uint.TryParse(pieces[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out language);
                    }
                    if (language == 0)
                        return false;
                }
                extension = Hash64.HashString(pieces[pieces.Length - 1]);
            }

            return true;
        }

        public bool ParseModScript(string scriptPath)
        {
            int currentLineNumber = 1;
            StreamReader reader = new StreamReader(new FileStream(scriptPath, FileMode.Open, FileAccess.Read));
            string currentLine = reader.ReadLine();
            while (currentLine != null)
            {
                this.ParseLine(currentLine, currentLineNumber);
                ++currentLineNumber;
                currentLine = reader.ReadLine();
            }
            reader.Close();
            return true;
        }

        private void ParseLine(string currentLine, int currentLineNumber)
        {
            if (currentLine.Length <= 0) return;
            switch (currentLine[0])
            {
                case ';':
                    return;
                case '@':
                    this.ParseMetaLine(currentLine, currentLineNumber);
                    break;
                default:
                    this.ParseFileLine(currentLine, currentLineNumber);
                    break;
            }
        }

        private void ParseFileLine(string currentLine, int currentLineNumber)
        {
            ulong path, extension = 0;
            uint language = 0;
            string[] pieces = currentLine.Split(':');
            if (pieces.Length < 2)
                this.Error("File line didn't contain two pieces.", currentLineNumber);
            this.CheckFile(pieces[0].Trim(), out path, out language, out extension, currentLineNumber);
            if (!File.Exists(this.modScriptPath + "/" + pieces[1].Trim()))
                this.Error("Replacement file does not exist.", currentLineNumber);
            var item = new BundleRewriteItem
            {
                BundleExtension = extension,
                BundleLanguage = language,
                BundlePath = path,
                IsLanguageSpecific = language != 0 ? true : false,
                SourceFile = pieces[0].Trim(),
                ReplacementFile = this.modScriptPath + "/" + pieces[1].Trim()
            };
            this.newMod.ItemQueue.Add(item);
        }

        private void ParseMetaLine(string currentLine, int currentLineNumber)
        {
            string[] pieces = currentLine.Split(' ');
            if (pieces.Length < 2)
                this.Error("Metadata line does not contain at least two pieces of information.", currentLineNumber);
            string remaining = currentLine.Substring(currentLine.IndexOf(' ')).Trim();
            switch (pieces[0])
            {
                case "@Author":
                    this.ModAuthorEdit.Text = remaining;
                    break;
                case "@Name":
                    this.ModNameEdit.Text = remaining;
                    break;
                case "@Description":
                    this.ModDescriptionEdit.Text = remaining.Replace("\\r\\n", System.Environment.NewLine).Replace("\\n", System.Environment.NewLine).Replace("\\r", System.Environment.NewLine);
                    break;
                case "@Version":
                    if (Versions.Contains(Convert.ToDouble(remaining)))
                        this.SpecificVersion.SelectedText = remaining;
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show(String.Format("{0} Line: {1}", "Specified version was not found. Using latest.", currentLineNumber), "PDMod Script Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SpecificVersion.SelectedIndex = 0;
                    }
                    break;
                case "@var":
                    string varType = remaining.Substring(0, remaining.IndexOf(' ')).Trim();
                    remaining = remaining.Substring(remaining.IndexOf(' ')).Trim();

                    string varName = remaining.Substring(0, remaining.IndexOf(' ')).Trim();
                    remaining = remaining.Substring(remaining.IndexOf('=') + 1).Trim();

                    string varDefVal = "";
                    StringBuilder defvalsb = new StringBuilder();
                    int char_count = 0;
                    if (varType.ToLower().Equals("string"))
                    {
                        bool started = false;
                        bool nextIgnore = false;

                        foreach (char c in remaining)
                        {
                            char_count++;

                            if (!started)
                            {
                                if (c == '"')
                                    started = true;
                            }
                            else
                            {
                                if (c == '\\' && !nextIgnore)
                                {
                                    nextIgnore = true;
                                    continue;
                                }
                                else if (c == '\\' && nextIgnore)
                                {
                                    defvalsb.Append(c);
                                    defvalsb.Append(c);
                                }
                                else if (c == '"' && !nextIgnore)
                                {
                                    break;
                                }
                                else
                                {
                                    defvalsb.Append(c);
                                }
                            }

                            if (nextIgnore)
                                nextIgnore = false;
                        }

                    }
                    else
                    {
                        foreach (char c in remaining)
                        {
                            if (c == ' ')
                            {
                                break;
                            }
                            else
                            {
                                defvalsb.Append(c);
                            }

                            char_count++;
                        }
                    }

                    varDefVal = defvalsb.ToString();
                    remaining = remaining.Substring(char_count).Trim();

                    this.newMod.Variables.Add(new ModVariable(varType, varName, varDefVal));
                    break;
                default:
                    this.Error("Invalid metadata type.", currentLineNumber);
                    break;
            }
        }

        private void Error(string message, int currentLineNumber)
        {

            throw new Exception(String.Format("{0} Line: {1}", message, currentLineNumber));
        }

        private void CheckFile(string file, out ulong path, out uint language, out ulong extension, int currentLineNumber)
        {
            language = 0;
            path = 0;
            extension = 0;
            string[] pieces = file.Split('.');
            path = Hash64.HashString(pieces[0]);
            extension = Hash64.HashString(pieces[pieces.Length - 1]);
            if (pieces.Length == 3)
            {
                if ((language = StaticStorage.Index.Lang2Id(Hash64.HashString(pieces[1]))) == 0)
                {
                    uint.TryParse(pieces[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out language);
                }

                if (language == null || language == 0)
                    this.Error("Invalid language.", currentLineNumber);

            }
            else if (pieces.Length == 2)
            {

            }
            else
            {
                this.Error(
                    "Filename did not appear to be valid. It must be either path/to/file.ext or path/to/file.language.ext in format.", currentLineNumber);
            }
            if (StaticStorage.Index.Entry2Id(path, extension, 0).Count == 0)
                this.Error("Could not find the file in the file index. Please check the spelling and that you used / and not \\.", currentLineNumber);
        }

        /// <summary>
        ///     The create mod button_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void CreateModButtonClick(object sender, EventArgs e)
        {
            //MessageBox.Show("Mod creaton was disabled for this version of Bundle Modder.");
            //return;

            if (this.newMod.ItemQueue.Count < 1)
            {
                MessageBox.Show("You must add at least one replacement file before creating a modification package.");
                return;
            }

            if (this.ModNameEdit.TextLength <= 0 || this.ModAuthorEdit.TextLength <= 0)
            {
                MessageBox.Show("You must enter a name and author for this modification package.");
                return;
            }

            if (this.SpecificVersion.Text == null)
            {
                this.SpecificVersion.SelectedIndex = 0;
            }

            this.newMod.Author = this.ModAuthorEdit.Text;
            this.newMod.Description = this.ModDescriptionEdit.Text;
            this.newMod.Name = this.ModNameEdit.Text;
            this.newMod.Version = Convert.ToDouble(this.SpecificVersion.Text, CultureInfo.InvariantCulture);
            BundleMod modToZip = BundleMod.Deserialize(BundleMod.Serialize(this.newMod));
            Dictionary<string, string> filesToZip = new Dictionary<string, string>();
            string replacementPath = "";
            foreach (BundleRewriteItem bundleItem in modToZip.ItemQueue)
            {
                if (bundleItem.ReplacementFile == null)
                {
                    continue;
                }
                if (Path.IsPathRooted(bundleItem.ReplacementFile))
                {
                    replacementPath = bundleItem.getBundleEntryPath().ToString();

                    if (Path.GetExtension(bundleItem.ReplacementFile).Equals(".script"))
                    {
                        replacementPath = Path.ChangeExtension(replacementPath, "script");
                    }

                    int i = 0;
                    while (filesToZip.Values.Contains(replacementPath + (i == 0 ? "" : i.ToString())))
                    {
                        i++;
                    }
                    replacementPath = replacementPath + (i == 0 ? "" : i.ToString());

                    //filesToZip.Add(bundleItem.SourceFile, replacementPath);
                    if (filesToZip.ContainsKey(replacementPath))
                    {
                        DialogResult dialogResult = MessageBox.Show("The current replacement path has already been used '" + replacementPath + "' \n\n Would you like to continue and skip this entry? (Pressing no will stop mod creation)", "Entry already Present", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {

                        }
                        else if (dialogResult == DialogResult.No)
                        {

                            return;
                        }
                    }
                    else
                    {
                        filesToZip.Add(replacementPath, bundleItem.ReplacementFile);
                    }
                    bundleItem.ReplacementFile = replacementPath;
                    bundleItem.SourceFile = null;
                }
            }

            string modjson = BundleMod.Serialize(modToZip);
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PAYDAY Mods(*.pdmod)|*.pdmod";
            saveDialog.FileName = this.newMod.Name + ".pdmod";
            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var zip = new ZipFile(saveDialog.FileName))
            {
                if (StaticStorage.settings.AlwaysPasswordProtect)
                {
                    zip.Password = "0$45'5))66S2ixF51a<6}L2UK";
                    zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                }
                zip.RemoveSelectedEntries("*");
                zip.AddEntry("pdmod.json", Encoding.UTF8.GetBytes(modjson));

                foreach (KeyValuePair<string, string> kp in filesToZip)
                {
                    zip.AddEntry(kp.Key, new FileStream(kp.Value, FileMode.Open, FileAccess.Read));
                }

                zip.Save();
            }

            MessageBox.Show("Saved modification package.");
        }

        private void DeleteBackups()
        {
            MessageBox.Show(
                "The game version appears to have changed since you last ran this tool. The backups folder will now be removed so that the new copies may be backed up.\n"
                + "Please ensure that the game files have been validated and are in an unmodified state before applying any mods.\n\nThis may take a while to complete.");
            string backupFolder = Path.Combine(StaticStorage.settings.AssetsFolder, "asset_backups");
            if (Directory.Exists(backupFolder))
                Directory.Delete(backupFolder, true);
            if (File.Exists(ModsFile))
                File.Delete(ModsFile);
        }

        /// <summary>
        ///     The include all bundles_ checked changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void IncludeAllBundlesCheckedChanged(object sender, EventArgs e)
        {
            /*if (this.IncludeAllBundles.Checked)
            {
                MessageBox.Show(
                    "Rewriting all_x.bundle files is usually not required, and greatly increases time and disk space taken to mod the game.\n"
                    + "Unless a mod requires this option, leave it unchecked!",
                    "CAUTION!");
            }*/
        }

        /// <summary>
        ///     The load mod.
        /// </summary>
        private void LoadMod()
        {
            try
            {
                using (var zip = new ZipFile(this.modZipFile))
                {
                    foreach (ZipEntry entry in zip.Entries)
                    {
                        if (entry.UsesEncryption)
                        {
                            entry.Password = "0$45'5))66S2ixF51a<6}L2UK";
                            //entry.Encryption = EncryptionAlgorithm.WinZipAes256;
                        }

                        if (entry.FileName == "pdmod.json")
                        {
                            var ms = new MemoryStream();
                            entry.Extract(ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            this.currentMod = BundleMod.Deserialize(ms);

                            if (this.currentMod.Version > Version)
                                throw new Exception("This mod is not compatible with your Bundle Modder. Please update your Bundle Modder to the latest version. (" + this.currentMod.Version + ")");
                            this.ModNameText.Text = this.currentMod.Name;
                            this.AuthorText.Text = this.currentMod.Author;
                            this.DescriptionText.Text = this.currentMod.Description;
                            this.ApplyButton.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.ModNameText.Text = "Failed to load mod.";
                this.DescriptionText.Text = e.Message;
            }
        }

        /// <summary>
        ///     The check mod for validity
        /// </summary>
        private bool CheckMod(string zipfile)
        {
            BundleMod addMod;
            try
            {
                using (var zip = new ZipFile(zipfile))
                {
                    foreach (ZipEntry entry in zip.Entries)
                    {
                        if (entry.UsesEncryption)
                        {
                            entry.Password = "0$45'5))66S2ixF51a<6}L2UK";
                            //entry.Encryption = EncryptionAlgorithm.WinZipAes256;
                        }

                        if (entry.FileName == "pdmod.json")
                        {
                            var ms = new MemoryStream();
                            entry.Extract(ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            addMod = BundleMod.Deserialize(ms);

                            if (addMod.Version > Version)
                                throw new Exception("This mod is not compatible with your Bundle Modder.\nPlease update your Bundle Modder to the latest version. (" + addMod.Version + ")");
                            if (addMod.Game != null && !addMod.Game.Equals(StaticStorage.settings.Game))
                                throw new Exception("This mod was built for " + addMod.Game + " game, which is not compatible with your game.");

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed to load mod - " + Path.GetFileName(zipfile));
                return false;
            }

            return false;

        }

        /// <summary>
        ///     The load settings.
        /// </summary>
        private void LoadSettings()
        {
            if (!File.Exists(SettingsFile))
            {
                MessageBox.Show(
                    "It appears that this is the first time you are launching this tool.\n"
                    + "You will need to set the game asset folder before you will be able to create or apply any mods.\n\n"
                    + "WARNING! This tool modifies game files, and while it makes backups, you will need to validate the game cache after an update is applied by OVERKILL, and then apply your mods again!\n"
                    + "Failure to do so can result in undesirable side effects, such as game crashes and missing or corrupt game assets.",
                    "WARNING!");
                this.tabs.SelectedIndex = 2;
                StaticStorage.settings = new ProgramSettings();
                this.ValidAssets = false;
            }
            else
            {
                var serializer = new XmlSerializer(StaticStorage.settings.GetType());
                using (var fs = new FileStream(SettingsFile, FileMode.Open, FileAccess.Read))
                {
                    StaticStorage.settings = (ProgramSettings)serializer.Deserialize(fs);
                    //this.settings.BackupType = 0;
                }

                if (!this.TestAssetsFolder())
                {
                    MessageBox.Show("Your assets folder appears to be invalid. Please select a valid assets folder in Options.", "Invalid assets folder.");
                    this.tabs.SelectedIndex = 2;
                }
            }

            //apply settings
            this.backupMethodComboBox.SelectedIndex = StaticStorage.settings.BackupType; //this.settings.BackupType;
            this.ThemeSelection.SelectedIndex = StaticStorage.settings.ThemeSelection;
            this.patchingBufferSize.SelectedIndex = StaticStorage.settings.PatchBuffer;
            this.useOverrideFolderCheckbox.Checked = StaticStorage.settings.OverrideFolder;
            this.checkBoxOverrideShow.Checked = StaticStorage.settings.ShowOverrideMods;
            this.createOverrideFolderDummies.Checked = StaticStorage.settings.OverrideFolderDummies;
            this.useSharedPDMODTOOLfolder.Checked = StaticStorage.settings.OverrideFolderShared;
            this.bundleFilePathAutotompletion_checkbox.Checked = StaticStorage.settings.Autocompletion;
            this.playsoundoncompletion_checkbox.Checked = StaticStorage.settings.PlaySoundOnCompletion;
            this.rungameoncompletion_checkbox.Checked = StaticStorage.settings.RunGameOnCompletion;
            this.checkupdatesonstartup_checkbox.Checked = StaticStorage.settings.CheckForUpdatesOnLaunch;
            this.WriteConsole.Checked = StaticStorage.settings.WriteConsole;
            this.chkIgnoreExisting.Checked = StaticStorage.settings.IgnoreExistingFiles;
            this.cmbFormat.SelectedIndex = 0;
            this.clstSelectInformation.Items.AddRange(new object[] {
                new ListBundleOption { Title = "Package", StringFunc = (extract, bundle, bundle_id) => { return bundle_id; }},
                new ListBundleOption { Title = "Package (UnHashed)", StringFunc = (extract, bundle, bundle_id) => { return this.UnHashString(bundle_id, true); } },
                new ListEntryOption { Title = "ID", StringFunc = (extract, entry) => { return entry.Id.ToString(); } },
                new ListEntryOption { Title = "Length", StringFunc = (extract, entry) => { return entry.Length.ToString(); } },
                new ListEntryOption { Title = "Path", StringFunc = (extract, entry) => { return extract.GetFileName(entry); } },
            });
            this.clstSelectInformation.SetItemChecked(0, true);
            this.clstSelectInformation.SetItemChecked(4, true);

            this.Refresh_lstChangeExtensionView();
            this.createOverrideFolderDummies.Enabled = StaticStorage.settings.OverrideFolder;
            this.useSharedPDMODTOOLfolder.Enabled = StaticStorage.settings.OverrideFolder;
            this.txtExtractFolder.Text = StaticStorage.settings.CustomExtractPath;
            this.txtListFile.Text = StaticStorage.settings.ListLogFile;
            StaticStorage.log = new log();
        }

        /// <summary>
        ///     The load hashlist.
        /// </summary>
        private void LoadHashList()
        {
            if (!File.Exists(HashlistFile))
            {
                //Hashlist was not found, it will need to be retrieved.
                if (this.ValidAssets && !RetrieveHashlist())
                {
                    MessageBox.Show("The Hashlist could not be retrieved and no existing one was found in the Bundle Modder directory.\nMany features will not function!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.HashlistLoaded = false;

                }
                /*if (this.TestAssetsFolder() && !RetrieveHashlist())
                {
                    MessageBox.Show("There was an error retrieving the hashlist");
                }*/

            }
            else
            {

                var watch = Stopwatch.StartNew();

                StaticStorage.Known_Index.Load(HashlistFile);
                this.HashlistLoaded = true;
                watch.Stop();
                StaticStorage.log.WriteLine("Known_Index.Load - " + watch.ElapsedMilliseconds + " ms");
            }

            SortedSet<String> paths = new SortedSet<String>();
            List<NameEntry> nameentrylist = StaticStorage.Index.getid2NameEntries();

            foreach (NameEntry ne in nameentrylist)
            {
                string path = StaticStorage.Known_Index.GetPath(ne.Path);
                string extension = StaticStorage.Known_Index.GetExtension(ne.Extension);
                string language = null;

                if (StaticStorage.Index.Id2Lang(ne.Language) != null)
                {
                    language = StaticStorage.Known_Index.GetAny(StaticStorage.Index.Id2Lang(ne.Language).Hash);

                    if (String.IsNullOrWhiteSpace(language))
                        language = ne.Language.ToString("X");
                }


                if (path == null || extension == null)
                    continue;

                paths.Add(path + "." + (!String.IsNullOrWhiteSpace(language) ? language + "." : "") + extension);
            }

            pathsAutoCompleteCollection.AddRange(paths.ToArray());
        }


        public bool RetrieveHashlist()
        {
            HashSet<string> new_paths = new HashSet<string>();
            HashSet<string> new_exts = new HashSet<string>();
            HashSet<string> new_other = new HashSet<string>();
            StringBuilder sb = new StringBuilder();
            int all_count = Directory.GetFiles(StaticStorage.settings.AssetsFolder, "all_*_h.bundle").Length;
            string[] known_bundles = { "all_7", "all_0", "all_" + (all_count - 1) }; //Payday: The Heist, Payday 2 Demo, Payday 2

            string[] idstring_data;

            foreach (string bundle_id in known_bundles)
            {
                string bundle_id_path = Path.Combine(StaticStorage.settings.AssetsFolder, bundle_id);
                if (!File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, bundle_id + ".bundle")) || !File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, bundle_id + "_h.bundle")))
                    continue;

                BundleHeader bundle = new BundleHeader();
                if (!bundle.Load(bundle_id_path))
                {
                    StaticStorage.log.WriteLine(string.Format("[Update error] Failed to parse bundle header. ({0})", bundle_id));
                    return false;
                }

                string bundle_file = bundle_id_path + ".bundle";
                if (!File.Exists(bundle_file))
                {
                    StaticStorage.log.WriteLine(string.Format("[Update error] Bundle file does not exist. ({0})", bundle_file));
                    return false;
                }
                using (FileStream fs = new FileStream(bundle_file, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] data;
                        foreach (BundleEntry be in bundle.Entries)
                        {
                            NameEntry ne = StaticStorage.Index.Id2Name(be.Id);
                            if (ne == null)
                                continue;

                            if (ne.Path == 0x9234DD22C60D71B8 && ne.Extension == 0x9234DD22C60D71B8)
                            {
                                fs.Position = be.Address;
                                if (be.Length == -1)
                                    data = br.ReadBytes((int)(fs.Length - fs.Position));
                                else
                                    data = br.ReadBytes((int)be.Length);

                                foreach (byte read in data)
                                    sb.Append((char)read);

                                idstring_data = sb.ToString().Split('\0');
                                sb.Clear();

                                foreach (string idstring in idstring_data)
                                {
                                    if (idstring.Contains("/"))
                                        new_paths.Add(idstring);
                                    else if (!idstring.Contains("/") && !idstring.Contains(".") && !idstring.Contains(":") && !idstring.Contains("\\"))
                                        new_exts.Add(idstring);
                                    else
                                        new_other.Add(idstring);
                                }

                                new_paths.Add("idstring_lookup");
                                new_paths.Add("existing_banks");

                                //StaticStorage.Known_Index.Clear();
                                StaticStorage.Known_Index.Load(ref new_paths, ref new_exts, ref new_other);
                                this.HashlistLoaded = true;

                                StaticStorage.Known_Index.GenerateHashList(HashlistFile);

                                new_paths.Clear();
                                new_exts.Clear();

                                return true;
                            }
                        }
                        br.Close();
                    }
                    fs.Close();
                }
            }
            return false;
        }

        /// <summary>
        ///     The load package names.
        /// </summary>
        private void LoadPackageNames()
        {
            if (!this.ValidAssets)
            {
                //MessageBox.Show("There was an error loading package names. (Assets folder failed)");
                return;
            }

            List<String> packages = Directory.EnumerateFiles(StaticStorage.settings.AssetsFolder, "*_h.bundle").ToList();
            SortedSet<String> sortedpackages = new SortedSet<String>();
            var watch = Stopwatch.StartNew();

            foreach (String pkg in packages)
            {
                String pkgname = Path.GetFileNameWithoutExtension(pkg).Replace("_h", "");
                if (pkgname.StartsWith("all_"))
                    continue;

                string packagename = UnHashString(pkgname, true);

                if (!String.IsNullOrWhiteSpace(packagename))
                    sortedpackages.Add(packagename);
            }

            packagesList.AddRange(sortedpackages.ToArray());

            //StaticStorage.Known_Index.Load(HashlistFile);
            watch.Stop();
            StaticStorage.log.WriteLine("LoadPackageNames.EnumerateFiles - " + watch.ElapsedMilliseconds + " ms");
        }

        /// <summary>
        ///     The unmark mod to original state.
        /// </summary>
        private bool unmarkMod(ListViewItem lviMod)
        {
            BundleMod taggedMod = (BundleMod)(lviMod.Tag);
            if ((object)(taggedMod) == null)
            {
                //Msg "ListViewItem error - No mod was tagged"
                return false; //Couldn't work without a bound mod.
            }

            foreach (var item in taggedMod.ItemQueue)
                item.toReinstall = false;

            if (taggedMod.status == BundleMod.ModStatus.NotInstalled)
            {
                taggedMod.actionStatus = BundleMod.ModActionStatus.None;
                lviMod.Checked = false;
            }
            else if (taggedMod.status == BundleMod.ModStatus.Installed)
            {
                taggedMod.actionStatus = BundleMod.ModActionStatus.None;
                lviMod.Checked = true;
            }
            else if (taggedMod.status == BundleMod.ModStatus.ParticallyInstalled)
            {
                taggedMod.actionStatus = BundleMod.ModActionStatus.ForcedReinstall;
                lviMod.Checked = true;
            }
            else if (taggedMod.status == BundleMod.ModStatus.Unrecognized)
            {
                taggedMod.actionStatus = BundleMod.ModActionStatus.Missing;
                lviMod.Checked = true;
            }

            this.markupModCollision(taggedMod, true);

            return true;
        }

        /// <summary>
        ///     The mark mod for installation.
        /// </summary>
        private bool markModInstall(ListViewItem lviMod)
        {
            BundleMod taggedMod = (BundleMod)(lviMod.Tag);
            if ((object)(taggedMod) == null)
            {
                MessageBox.Show("ListViewItem error - No mod was tagged", "Error!");
                lviMod.Checked = false;
                return false; //Couldn't work without a bound mod.
            }

            if (!taggedMod.canInstall)
            {
                MessageBox.Show("This mod is not allowed to be installed", "Error!");
                lviMod.Checked = false;
                return false; //This mod was restricted from being installed.
            }

            if (taggedMod.status == BundleMod.ModStatus.Installed)
            {
                MessageBox.Show("This mod is already installed, you cannot install it again like this", "Error!");
                lviMod.Checked = false;
                return false; //This mod was restricted from being installed.
            }

            if (taggedMod.actionStatus != BundleMod.ModActionStatus.Install)
            {
                taggedMod.actionStatus = BundleMod.ModActionStatus.Install;
                lviMod.Checked = true;
            }
            this.markupModCollision(taggedMod);

            return true;
        }

        /// <summary>
        ///     The mark mod for uninstallation.
        /// </summary>
        private bool markModUnInstall(ListViewItem lviMod)
        {
            BundleMod taggedMod = (BundleMod)(lviMod.Tag);
            if ((object)(taggedMod) == null)
            {
                MessageBox.Show("ListViewItem error - No mod was tagged", "Error!");
                return false; //Couldn't work without a bound mod.
            }

            if (!taggedMod.canUninstall)
            {
                MessageBox.Show("This mod is not allowed to be uninstalled", "Error!");
                return false; //This mod was restricted from being uninstalled.
            }

            if (taggedMod.status == BundleMod.ModStatus.NotInstalled)
            {
                MessageBox.Show("This mod is not installed, you cannot uninstall it.", "Error!");
                return false; //This mod was restricted from being uninstalled.
            }

            if (taggedMod.actionStatus != BundleMod.ModActionStatus.Remove)
            {
                taggedMod.actionStatus = BundleMod.ModActionStatus.Remove;
                lviMod.Checked = false;
                this.markupModCollision(taggedMod);
            }


            return true;
        }

        /// <summary>
        ///     The mark mod for reinstallation.
        /// </summary>
        private bool markModReInstall(ListViewItem lviMod)
        {
            BundleMod taggedMod = (BundleMod)(lviMod.Tag);
            if ((object)(taggedMod) == null)
            {
                MessageBox.Show("ListViewItem error - No mod was tagged", "Error!");
                return false; //Couldn't work without a bound mod.
            }

            if (!taggedMod.canInstall)
            {
                MessageBox.Show("This mod is not allowed to be reinstalled", "Error!");
                return false; //This mod was restricted from being reinstalled.
            }

            if (taggedMod.status == BundleMod.ModStatus.NotInstalled)
            {
                MessageBox.Show("This mod is not installed, you cannot reinstall it.", "Error!");
                return false; //This mod was restricted from being reinstalled.
            }

            if (taggedMod.actionStatus != BundleMod.ModActionStatus.Reinstall)
            {
                taggedMod.actionStatus = BundleMod.ModActionStatus.Reinstall;
                lviMod.Checked = true;
                this.markupModCollision(taggedMod);
            }

            return true;
        }

        /// <summary>
        ///     The mark mod for forced reinstallation.
        /// </summary>
        private bool markModForceReInstall(ListViewItem lviMod)
        {
            BundleMod taggedMod = (BundleMod)(lviMod.Tag);
            if ((object)(taggedMod) == null)
            {
                MessageBox.Show("ListViewItem error - No mod was tagged", "Error!");
                return false; //Couldn't work without a bound mod.
            }

            if (!taggedMod.canInstall)
            {
                MessageBox.Show("This mod is not allowed to be reinstalled", "Error!");
                return false; //This mod was restricted from being reinstalled.
            }

            if (taggedMod.status == BundleMod.ModStatus.NotInstalled)
            {
                MessageBox.Show("This mod is not installed, you cannot reinstall it.", "Error!");
                return false; //This mod was restricted from being reinstalled.
            }

            if (taggedMod.actionStatus != BundleMod.ModActionStatus.ForcedReinstall)
            {
                taggedMod.actionStatus = BundleMod.ModActionStatus.ForcedReinstall;
                lviMod.Checked = true;
                this.markupModCollision(taggedMod);
            }

            return true;
        }




        /// <summary>
        ///     The update local mods.
        /// </summary>
        private void updateModsListView()
        {
            //Regen the missingfile list
            Dictionary<string, BundleMod> modsList = this.mods_db.modsList;
            List<BackupEntry> missingfile_list = this.mods_db.installedModsList_missing;

            this.allowCheck = false;
            //Clear items
            //this.availiableMods_listView.Items.Clear();
            List<ListViewItem> new_availiableMods = new List<ListViewItem>();

            //Populate with local mods
            foreach (BundleMod localMod in modsList.Values.ToList())
            {
                if (localMod.type == BundleMod.ModType.mod_override && !StaticStorage.settings.ShowOverrideMods)
                    continue;

                ListViewItem lvdi = new ListViewItem();

                if (this.mods_db.InstalledModsListContains(localMod) >= 0 || localMod.status == BundleMod.ModStatus.Installed)
                {
                    localMod.status = BundleMod.ModStatus.Installed;
                    lvdi.Checked = true;
                }
                else
                    lvdi.Checked = false;

                if (localMod.IncludesPatchScriptWithinOverride)
                {
                    lvdi.Text = "[*]" + localMod.Name;
                    lvdi.ToolTipText = "This mod uses the override folder with scripts, which may need to be reinstalled with game updates.";
                }
                else if (localMod.UtilizesBundles)
                {
                    lvdi.Text = "[#]" + localMod.Name;
                    lvdi.ToolTipText = "This mod will need to be reinstalled with each game update.";
                }
                else
                {
                    lvdi.Text = localMod.Name;
                }

                lvdi.Tag = localMod;
                lvdi.Group = this.availiableMods_listView.Groups[0];
                if (localMod.type == BundleMod.ModType.mod_override)
                    lvdi.Group = this.availiableMods_listView.Groups[1];
                else if (localMod.type == BundleMod.ModType.lua)
                {
                    lvdi.Group = this.availiableMods_listView.Groups[2];
                    lvdi.Checked = localMod.enabled;
                }

                if (localMod.actionStatus == BundleMod.ModActionStatus.Install)
                {
                    lvdi.BackColor = Color.LimeGreen;
                    lvdi.ForeColor = Color.Black;
                    lvdi.Checked = true;
                }
                else if (localMod.actionStatus == BundleMod.ModActionStatus.Remove)
                {
                    lvdi.BackColor = Color.Red;
                    lvdi.ForeColor = Color.White;
                    lvdi.Checked = false;
                }
                else if (localMod.actionStatus == BundleMod.ModActionStatus.Reinstall)
                {
                    lvdi.BackColor = Color.Yellow;
                    lvdi.ForeColor = Color.Black;
                    lvdi.Checked = true;
                }
                else if (localMod.actionStatus == BundleMod.ModActionStatus.ForcedReinstall)
                {
                    lvdi.BackColor = Color.Orange;
                    lvdi.ForeColor = Color.Black;
                    lvdi.Checked = true;
                }
                else if (localMod.actionStatus == BundleMod.ModActionStatus.Missing)
                {
                    localMod.canInstall = false;
                    localMod.canUninstall = true;
                    lvdi.BackColor = Color.LightBlue;
                    lvdi.ForeColor = Color.Black;
                    lvdi.Checked = true;
                }
                else
                {
                    lvdi.BackColor = Color.White;
                    lvdi.ForeColor = Color.Black;
                }

                if (searchMod(localMod, this.availiableModsSearch_textbox.Text))
                    new_availiableMods.Add(lvdi);
                //this.availiableMods_listView.Items.Add( lvdi );
            }

            //Populate with other mods
            //population here

            if (missingfile_list.Count > 0)
            {
                foreach (var missing in missingfile_list)
                {
                    ListViewItem lvdi = new ListViewItem();
                    BundleMod missingMod = new BundleMod();
                    missingMod.Name = missing.Name;
                    missingMod.Author = missing.Author;
                    missingMod.Description = missing.Description;
                    missingMod.ItemQueue = missing.ItemQueue;


                    missingMod.canInstall = false;
                    missingMod.canUninstall = true;
                    missingMod.actionStatus = BundleMod.ModActionStatus.Missing;
                    lvdi.BackColor = Color.LightBlue;
                    lvdi.ForeColor = Color.Black;
                    lvdi.Checked = true;

                    lvdi.Text = missingMod.Name;
                    lvdi.Tag = missingMod;
                    lvdi.Group = this.availiableMods_listView.Groups[0];

                    if (searchMod(missingMod, this.availiableModsSearch_textbox.Text))
                        new_availiableMods.Add(lvdi);
                    //this.availiableMods_listView.Items.Add(lvdi);
                }
            }

            //this.availiableMods_listView.Items = (this.availiableMods_listView.Items.).OrderByDescending(o => o.Checked).ToList();

            this.availiableMods_listView.Items.Clear();
            this.availiableMods_listView.Items.AddRange(new_availiableMods.ToArray());

            this.availiableMods_listView.Update();
            this.refreshModsListView();
            this.allowCheck = true;
        }

        /// <summary>
        ///     The refresh local mods.
        /// </summary>
        private void refreshModsListView()
        {
            Dictionary<string, BundleMod> modsList = this.mods_db.modsList;

            int markedInstall = modsList.Values.Count(e => e.actionStatus == BundleMod.ModActionStatus.Install);
            int markedReinstall = modsList.Values.Count(e => e.actionStatus == BundleMod.ModActionStatus.Reinstall || e.actionStatus == BundleMod.ModActionStatus.ForcedReinstall);
            int markedRemoval = modsList.Values.Count(e => e.actionStatus == BundleMod.ModActionStatus.Remove);
            filecontrolDictionary.Clear();

            foreach (ListViewItem lvi in this.availiableMods_listView.Items)
            {
                if (lvi == null || lvi.Tag == null)
                    continue;

                // 0 = Nothing, 1 = Marked for install (limegreen), 2 = Marked for removal (red), 3 = Marked for reinstallation (yellow), 4 = Marked for forced reinstallation (orange)
                if (((BundleMod)lvi.Tag).actionStatus == BundleMod.ModActionStatus.Install)
                {
                    lvi.BackColor = Color.LimeGreen;
                    lvi.ForeColor = Color.Black;
                    lvi.ImageIndex = 0;
                }
                else if (((BundleMod)lvi.Tag).actionStatus == BundleMod.ModActionStatus.Remove)
                {
                    lvi.BackColor = Color.Red;
                    lvi.ForeColor = Color.White;
                    lvi.ImageIndex = 1;
                }
                else if (((BundleMod)lvi.Tag).actionStatus == BundleMod.ModActionStatus.Reinstall)
                {
                    lvi.BackColor = Color.Yellow;
                    lvi.ForeColor = Color.Black;
                    lvi.ImageIndex = 2;
                }
                else if (((BundleMod)lvi.Tag).actionStatus == BundleMod.ModActionStatus.ForcedReinstall)
                {
                    lvi.BackColor = Color.Orange;
                    lvi.ForeColor = Color.Black;
                    lvi.ImageIndex = 2;
                }
                else if (((BundleMod)lvi.Tag).actionStatus == BundleMod.ModActionStatus.Missing)
                {
                    lvi.BackColor = Color.LightBlue;
                    lvi.ForeColor = Color.Black;
                    lvi.ImageIndex = 3;
                }
                else
                {
                    lvi.ImageIndex = -1;
                    lvi.BackColor = Color.White;
                    lvi.ForeColor = Color.Black;
                }
            }

            foreach (var mod in modsList.Values)
            {
                if (mod.actionStatus == BundleMod.ModActionStatus.None || mod.actionStatus == BundleMod.ModActionStatus.Remove)
                    continue;

                foreach (BundleRewriteItem bri in mod.ItemQueue)
                {
                    BundleEntryPath tempbep = bri.getBundleEntryPath();
                    if (filecontrolDictionary.ContainsKey(tempbep))
                    {
                        if (!filecontrolDictionary[tempbep].Contains(bri))
                            filecontrolDictionary[tempbep].Add(bri);
                    }
                    else
                    {
                        List<BundleRewriteItem> newmodslist = new List<BundleRewriteItem>();
                        newmodslist.Add(bri);
                        filecontrolDictionary.Add(tempbep, newmodslist);
                    }
                }
            }

            Dictionary<BundleEntryPath, List<BundleRewriteItem>> tempfilecontrol = new Dictionary<BundleEntryPath, List<BundleRewriteItem>>(filecontrolDictionary);
            foreach (KeyValuePair<BundleEntryPath, List<BundleRewriteItem>> kvp in tempfilecontrol)
            {
                if (kvp.Value.Count <= 1)
                {
                    filecontrolDictionary.Remove(kvp.Key);
                    if (filecontrolSelectedDictionary.ContainsKey(kvp.Key))
                        filecontrolSelectedDictionary.Remove(kvp.Key);
                }
                else
                {
                    if (kvp.Value.Count(p => !p.ReplacementFile.EndsWith(".script")) <= 1)
                        filecontrolDictionary.Remove(kvp.Key);
                    else
                    {
                        if (!filecontrolSelectedDictionary.ContainsKey(kvp.Key))
                            filecontrolSelectedDictionary.Add(kvp.Key, kvp.Value[0].ModName);
                    }
                }
            }

            this.filecontrol_button.Enabled = (filecontrolDictionary.Count > 0);
            this.filecontrol_button.Text = (filecontrolDictionary.Count > 0 ? "(" + filecontrolDictionary.Count + ") " : "") + "File Ctrl";

            if (markedInstall > 0 || markedReinstall > 0 || markedRemoval > 0)
            {
                string makeLabel = "Available Mods (";
                if (markedInstall > 0)
                    makeLabel += "Install: " + markedInstall;
                if (markedInstall > 0 && (markedReinstall > 0 || markedRemoval > 0))
                    makeLabel += " ";

                if (markedReinstall > 0)
                    makeLabel += "Reinstall: " + markedReinstall;
                if ((markedInstall > 0 || markedReinstall > 0) && markedRemoval > 0)
                    makeLabel += " ";

                if (markedRemoval > 0)
                    makeLabel += "Removal: " + markedRemoval;

                makeLabel += ")";

                this.AvailableModsLabel.Text = makeLabel;
                this.AvailableModsLabel.Refresh();

                this.ApplyButton.Enabled = true;
            }
            else
            {
                this.AvailableModsLabel.Text = "Available Mods";
                this.AvailableModsLabel.Refresh();
                this.ApplyButton.Enabled = false;
            }

            this.availiableMods_listView.Refresh();
        }

        /// <summary>
        ///     The markup mods for reinstall
        /// </summary>
        private Dictionary<string, List<BundleMod>> markupModCollision(BundleMod newMod, bool reverse = false)
        {
            Dictionary<string, BundleMod> modsList = this.mods_db.modsList;
            Dictionary<string, List<BundleMod>> return_list = new Dictionary<string, List<BundleMod>>();
            BundleMod return_item;

            foreach (BundleMod localmod in modsList.Values)
            {
                if (newMod == localmod)
                    continue;

                if (this.mods_db.InstalledModsListContains(localmod) != -1 && (localmod.actionStatus != BundleMod.ModActionStatus.Remove && localmod.actionStatus != BundleMod.ModActionStatus.ForcedReinstall))
                {
                    return_item = new BundleMod();

                    foreach (BundleRewriteItem bri in newMod.ItemQueue)
                    {
                        foreach (BundleRewriteItem briLocal in localmod.ItemQueue)
                        {
                            if (bri == briLocal)
                            {
                                if (reverse)
                                {
                                    bri.toReinstall = false;
                                    briLocal.toReinstall = false;
                                    bri.toShared = false;
                                    briLocal.toShared = false;
                                }
                                else
                                {
                                    bri.toReinstall = true;
                                    briLocal.toReinstall = true;
                                    bri.toShared = true;
                                    briLocal.toShared = true;
                                }
                                return_item.ItemQueue.Add(bri);
                            }
                        }
                    }

                    if (localmod.ItemQueue.Any(items => items.toReinstall == true))
                        localmod.actionStatus = BundleMod.ModActionStatus.Reinstall;
                    else
                        localmod.actionStatus = BundleMod.ModActionStatus.None;

                    if (localmod.actionStatus == BundleMod.ModActionStatus.Reinstall)
                    {
                        return_item.Author = localmod.Author;
                        return_item.Description = localmod.Description;
                        return_item.Name = localmod.Name;

                        foreach (var item in return_item.ItemQueue)
                        {
                            string file_name = "";
                            file_name += StaticStorage.Known_Index.GetPath(item.BundlePath);
                            if (item.IsLanguageSpecific)
                                file_name += "." + item.BundleLanguage;
                            file_name += "." + StaticStorage.Known_Index.GetExtension(item.BundleExtension);


                            if (return_list.ContainsKey(file_name))
                                return_list[file_name].Add(return_item);
                            else
                            {
                                List<BundleMod> newModList = new List<BundleMod>();
                                newModList.Add(newMod);
                                newModList.Add(return_item);
                                return_list.Add(file_name, newModList);
                            }
                        }
                    }
                }
            }

            return return_list;
        }

        /// <summary>
        ///     The markup (to be installed) mods for reinstall
        /// </summary>
        private void markupinstallModsCollision(bool reverse = false)
        {
            Dictionary<string, BundleMod> modsList = this.mods_db.modsList;

            // 0 = Nothing, 1 = Marked for install (limegreen), 2 = Marked for removal (red), 3 = Marked for reinstallation (yellow), 4 = 
            foreach (BundleMod localmod in modsList.Values)
            {
                if (localmod.actionStatus == BundleMod.ModActionStatus.Install || localmod.actionStatus == BundleMod.ModActionStatus.Remove || localmod.actionStatus == BundleMod.ModActionStatus.ForcedReinstall)
                {
                    markupModCollision(localmod, reverse);
                }
            }
        }

        /// <summary>
        ///     The progress timer elapsed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void ProgressTimerElapsed(object sender, EventArgs args)
        {
            if (this.rewriter == null)
            {
                this.progressTimer.Stop();
                return;
            }
            this.rewriter.ProgressMutex.WaitOne();

            this.TotalProgressLabel.Text = this.rewriter.TotalProgressMessage + " " + this.rewriter.BundleProgressMessage;
            this.BundleProgress.Value = this.rewriter.BundleProgressPercentage;
            int totalPercentage = this.rewriter.TotalProgressPercentage;
            if (totalPercentage == -1)
            {
                this.TotalProgress.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                this.TotalProgress.Style = ProgressBarStyle.Blocks;
                this.TotalProgress.Value = totalPercentage;
            }

            if (this.rewriter.allowBackup)
            {
                this.mods_db.RemoveInstalledMod(this.rewriter.backupEntry);

                this.mods_db.AddInstalledMod(this.rewriter.backupEntry);
                this.mods_db.SaveModBackups();

                this.rewriter.allowBackup = false;
            }

            if (this.rewriter.Done)
            {
                if (this.rewriter.Error != null)
                    MessageBox.Show("Error occured during modding of bundle files: " + this.rewriter.Error, "Error!");

                this.rewriter.ProgressMutex.ReleaseMutex();
                this.progressTimer.Enabled = false;
                this.progressTimer.Stop();
                this.rewriter = null;

                this.ApplyButton.Enabled = this.savedStateApplyMod;
                this.OpenModButton.Enabled = this.savedStateOpenMod;

                return;
            }

            this.rewriter.ProgressMutex.ReleaseMutex();
        }

        /// <summary>
        ///     The progress timer elapsed for multiple mods.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void MultiProgressTimerElapsed(object sender, EventArgs args)
        {
            if (this.rewriter == null)
            {
                this.progressTimer.Stop();
                return;
            }
            this.rewriter.ProgressMutex.WaitOne();
            double SpeedTimeTaken = (((this.rewriter.SpeedElapsedTime.ElapsedMilliseconds / 1000L) < 1L ? 1.0d : (this.rewriter.SpeedElapsedTime.ElapsedMilliseconds / 1000L)) * 1.0d);
            double PatchSpeed = (this.rewriter.SpeedEntryCount * 1.0d) / SpeedTimeTaken;

            double TimeTaken = (((this.rewriter.TotalElapsedTime.ElapsedMilliseconds / 1000L) < 1L ? 1.0d : (this.rewriter.TotalElapsedTime.ElapsedMilliseconds / 1000L)) * 1.0d);
            double AvgPatchSpeed = (this.rewriter.CurrentEntryCount * 1.0d) / TimeTaken;

            lock ((object)this.rewriter.SpeedEntryCount)
            {
                this.rewriter.SpeedEntryCount = 0L;
            }
            this.rewriter.SpeedElapsedTime.Restart();

            this.patchingTimeDetails.Text = "Current Bundle: " + this.rewriter.CurrentBundle + " Buffer: " + this.rewriter.bufferSize + "\r\n" +
                                            "Speed: " + PatchSpeed.ToString("0.##") + " Entries/Sec \r\n" +
                                            "Avg. Speed: " + AvgPatchSpeed.ToString("0.##") + " Entries/Sec || Est. Time left: " + (((TimeTaken / this.rewriter.CurrentEntryCount) * (this.rewriter.TotalEntryCount - this.rewriter.CurrentEntryCount)) < 60.0 ? ((TimeTaken / this.rewriter.CurrentEntryCount) * (this.rewriter.TotalEntryCount - this.rewriter.CurrentEntryCount)).ToString("0.##") + " seconds" : ((TimeTaken / this.rewriter.CurrentEntryCount) * (this.rewriter.TotalEntryCount - this.rewriter.CurrentEntryCount) / 60).ToString("0") + " minute" + ((int)((TimeTaken / this.rewriter.CurrentEntryCount) * (this.rewriter.TotalEntryCount - this.rewriter.CurrentEntryCount) / 60) > 1 ? "s" : "") + ((int)((TimeTaken / this.rewriter.CurrentEntryCount) * (this.rewriter.TotalEntryCount - this.rewriter.CurrentEntryCount) % 60.0) > 0 ? " " + ((TimeTaken / this.rewriter.CurrentEntryCount) * (this.rewriter.TotalEntryCount - this.rewriter.CurrentEntryCount) % 60).ToString("0") + " seconds" : ""));

            this.TotalProgressLabel.Text = this.rewriter.TotalProgressMessage + " " + this.rewriter.BundleProgressMessage;
            this.BundleProgress.Value = this.rewriter.BundleProgressPercentage;

            int totalPercentage = this.rewriter.TotalProgressPercentage;
            if (totalPercentage == -1)
            {
                this.TotalProgress.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                this.TotalProgress.Style = ProgressBarStyle.Blocks;
                this.TotalProgress.Value = totalPercentage;
            }

            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                TaskbarManager.Instance.SetProgressValue(this.rewriter.CurrentBundlesCount, this.rewriter.TotalBundlesCount);
            }

            if (this.rewriter.allowBackup)
            {
                foreach (BackupEntry be in this.rewriter.backupEntries)
                {
                    this.mods_db.RemoveInstalledMod(be);
                    this.mods_db.AddInstalledMod(be);
                }
                this.mods_db.SaveModBackups();

                this.rewriter.allowBackup = false;
            }

            if (this.rewriter.Error != null)
            {
                if (TaskbarManager.IsPlatformSupported)
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
                MessageBox.Show("Error occured during modding of bundle files: " + this.rewriter.Error, "Error!");
                this.rewriter.Done = true;
            }

            if (this.rewriter.Done)
            {
                //if (this.rewriter.Error != null)
                //    MessageBox.Show("Error occured during modding of bundle files: " + this.rewriter.Error, "Error!");

                if (TaskbarManager.IsPlatformSupported)
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

                if (StaticStorage.settings.PlaySoundOnCompletion)
                    System.Media.SystemSounds.Beep.Play();

                this.availiableMods_listView.Enabled = true;
                this.availiableModsSearch_textbox.Enabled = true;
                this.ApplyButton.Enabled = this.savedStateApplyMod;
                this.OpenModButton.Enabled = this.savedStateOpenMod;
                this.refreshButton.Enabled = this.savedStateRefreshButton;
                this.filecontrol_button.Enabled = this.savedStateFileControlButton;
                this.ExtraOptions1.Enabled = true;

                if (this.rewriter.Error == null)
                {
                    foreach (ListViewItem lvi in this.availiableMods_listView.Items)
                    {
                        if (((BundleMod)lvi.Tag).actionStatus == BundleMod.ModActionStatus.Remove)
                        {
                            this.mods_db.RemoveInstalledMod((BundleMod)lvi.Tag);
                        }
                    }

                    if (StaticStorage.settings.RunGameOnCompletion)
                    {
                        System.Diagnostics.Process.Start("steam://run/" + StaticStorage.settings.GameSteamID);
                    }
                }

                this.mods_db.SaveModBackups();
                this.mods_db.LoadMods(true);
                this.updateModsListView();

                this.rewriter.ProgressMutex.ReleaseMutex();
                this.progressTimer.Enabled = false;
                this.progressTimer.Stop();
                this.rewriter = null;

                return;
            }

            this.rewriter.ProgressMutex.ReleaseMutex();
        }


        /// <summary>
        ///     The progress timer elapsed for uninstalling mods.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void UninstallProgressTimerElapsed(object sender, EventArgs args)
        {
            if (this.rewriter == null)
            {
                this.progressTimer.Stop();
                return;
            }
            this.rewriter.ProgressMutex.WaitOne();

            this.TotalProgressLabel.Text = this.rewriter.TotalProgressMessage + " " + this.rewriter.BundleProgressMessage;
            this.BundleProgress.Value = this.rewriter.BundleProgressPercentage;

            int totalPercentage = this.rewriter.TotalProgressPercentage;
            if (totalPercentage == -1)
            {
                this.TotalProgress.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                this.TotalProgress.Style = ProgressBarStyle.Blocks;
                this.TotalProgress.Value = totalPercentage;
            }


            if (this.rewriter.Done)
            {
                if (this.rewriter.Error != null)
                {
                    MessageBox.Show("Error occured during modding of bundle files: " + this.rewriter.Error, "Error!");
                }

                this.rewriter.ProgressMutex.ReleaseMutex();
                this.progressTimer.Enabled = false;
                this.progressTimer.Stop();
                this.rewriter = null;

                this.ApplyButton.Enabled = this.savedStateApplyMod;
                this.OpenModButton.Enabled = this.savedStateOpenMod;

                foreach (ListViewItem lvi in this.availiableMods_listView.Items)
                {
                    if (((BundleMod)lvi.Tag).actionStatus == BundleMod.ModActionStatus.Remove)
                    {
                        this.mods_db.RemoveInstalledMod((BundleMod)lvi.Tag);
                    }
                }

                this.mods_db.SaveModBackups();

                return;
            }

            this.rewriter.ProgressMutex.ReleaseMutex();
        }


        /// <summary>
        ///     The progress timer elapsed for checking bundles.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void BundleCheckProgressTimerElapsed(object sender, EventArgs args)
        {
            if (this.rewriter == null)
            {
                this.progressTimer.Stop();
                return;
            }
            this.rewriter.ProgressMutex.WaitOne();
            this.progressTextCorruptedBundlesCheck.Text = "Current progress: " + this.rewriter.TotalProgressMessage + " Corrupted: " + this.rewriter.checkBundlesReport.Count(e => e.Value != String.Empty);
            this.progressCorruptedBundlesCheck.Value = this.rewriter.TotalProgressPercentage;

            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                TaskbarManager.Instance.SetProgressValue(this.rewriter.CurrentBundlesCount, this.rewriter.TotalBundlesCount);
            }

            if (this.rewriter.Done)
            {
                if (this.rewriter.Error != null)
                {
                    if (TaskbarManager.IsPlatformSupported)
                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
                    MessageBox.Show("Error occured during modding of bundle files: " + this.rewriter.Error, "Error!");
                }

                if (TaskbarManager.IsPlatformSupported)
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

                this.checkedBundlesReport = new Dictionary<string, string>(this.rewriter.checkBundlesReport);
                this.generateCorruptedReport();

                this.rewriter.ProgressMutex.ReleaseMutex();
                this.progressTimer.Enabled = false;
                this.progressTimer.Stop();
                this.rewriter = null;

                this.ApplyButton.Enabled = this.savedStateApplyMod;
                this.OpenModButton.Enabled = this.savedStateOpenMod;

                if (this.verifyCorruptedBundlesCheckCheckBox.Checked)
                {
                    System.Diagnostics.Process.Start("steam://validate/" + StaticStorage.settings.GameSteamID);
                }

                this.verifyCorruptedBundlesCheckCheckBox.Enabled = true;
                this.corruptedShowOnlyCorrupted_checkbox.Enabled = true;
                this.attemptRepair_button.Enabled = true;

                return;
            }

            this.rewriter.ProgressMutex.ReleaseMutex();
        }

        /// <summary>
        ///     The save settings.
        /// </summary>
        private void SaveSettings()
        {
            var serializer = new XmlSerializer(StaticStorage.settings.GetType());
            if (!File.Exists(SettingsFile))
                File.Create(SettingsFile).Close();
            using (var fs = new FileStream(SettingsFile, FileMode.Truncate, FileAccess.Write))
            {
                serializer.Serialize(fs, StaticStorage.settings);
            }
        }

        /// <summary>
        ///     The swap endianess.
        /// </summary>
        /// <param name="uvalue">
        ///     The uvalue.
        /// </param>
        /// <returns>
        ///     The <see cref="ulong" />.
        /// </returns>
        private ulong SwapEndianess(ulong uvalue)
        {
            ulong swapped = 0x00000000000000FF & (uvalue >> 56) | 0x000000000000FF00 & (uvalue >> 40)
                            | 0x0000000000FF0000 & (uvalue >> 24) | 0x00000000FF000000 & (uvalue >> 8)
                            | 0x000000FF00000000 & (uvalue << 8) | 0x0000FF0000000000 & (uvalue << 24)
                            | 0x00FF000000000000 & (uvalue << 40) | 0xFF00000000000000 & (uvalue << 56);
            return swapped;
        }

        /// <summary>
        ///     The test assets folder.
        /// </summary>
        /// <returns>
        ///     Success <see cref="bool" />.
        /// </returns>
        public bool TestAssetsFolder()
        {

            if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "bundle_db.blb")))
            {
                if (!StaticStorage.Index.Load(Path.Combine(StaticStorage.settings.AssetsFolder, "bundle_db.blb")))
                {
                    MessageBox.Show("Failed to load the bundle_db.blb file.");
                    return (ValidAssets = false);
                }
            }
            else if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "all.blb")))
            {
                if (!StaticStorage.Index.Load(Path.Combine(StaticStorage.settings.AssetsFolder, "all.blb")))
                {
                    MessageBox.Show("Failed to load the all.blb file.");
                    return (ValidAssets = false);
                }
            }
            else
            {
                return (ValidAssets = false);
            }

            if (!this.testedAssets)
            {
                //int all_count = Directory.GetFiles (StaticStorage.settings.AssetsFolder, "all_*_h.bundle").Length;
                //TestBundleForIDStrings("all_" + (all_count - 1).ToString())
                bool linux = false;
                if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "bundle_db.blb")) && (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "payday2_win32_release.exe")) || (linux = File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "payday2_release"))))) //Payday 2
                {
                    StaticStorage.settings.Game = "PAYDAY 2" + (linux ? " [Linux]" : "");
                    StaticStorage.settings.GameShortName = "PAYDAY 2" + (linux ? " [Linux]" : "");
                    if (linux)
                        StaticStorage.settings.GameProcess = "payday2_release";
                    else
                        StaticStorage.settings.GameProcess = "payday2_win32_release";

                    StaticStorage.settings.GameSteamID = 218620;
                    StaticStorage.settings.GameSupportsOverride = !linux;
                }
                else if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "all.blb")) && File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "payday2_win32_release.exe"))) //Payday 2 Demo
                {
                    StaticStorage.settings.Game = "PAYDAY 2 Demo";
                    StaticStorage.settings.GameShortName = "PAYDAY 2";
                    StaticStorage.settings.GameProcess = "payday2_win32_release";
                    StaticStorage.settings.GameSteamID = 251040;
                    StaticStorage.settings.GameSupportsOverride = false;
                }
                else if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "all.blb")) && File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "payday_win32_release.exe"))) //Payday: The Heist
                {
                    StaticStorage.settings.Game = "PAYDAY: The Heist";
                    StaticStorage.settings.GameShortName = "PAYDAY";
                    StaticStorage.settings.GameProcess = "payday_win32_release";
                    StaticStorage.settings.GameSteamID = 24240;
                    StaticStorage.settings.GameSupportsOverride = false;
                }
                else
                {
                    MessageBox.Show(
                        "The folder you selected does not appear to be a valid assets folder. Please select a valid assets folder.",
                        "Invalid assets folder.");
                    this.tabs.SelectedIndex = 2;
                    return (ValidAssets = false);
                }


                updateTitle();
                this.AssetFolderEdit.Text = StaticStorage.settings.AssetsFolder;
            }
            this.testedAssets = true;

            if (StaticStorage.settings.GameSupportsOverride)
            {
                this.useOverrideFolderCheckbox.Enabled = true;
                this.createOverrideFolderDummies.Enabled = true;
                this.useSharedPDMODTOOLfolder.Enabled = true;
            }
            else
            {
                StaticStorage.settings.OverrideFolder = false;
                this.useOverrideFolderCheckbox.Checked = false;
                this.useOverrideFolderCheckbox.Enabled = false;
                this.createOverrideFolderDummies.Enabled = false;
                this.useSharedPDMODTOOLfolder.Enabled = false;
            }

            this.OpenModButton.Enabled = true;
            this.AddReplacementButton.Enabled = true;
            this.CreateModButton.Enabled = true;
            this.TestGameVersion();
            return (ValidAssets = true);

        }


        /*private bool TestBundleForIDStrings(string bundle_id)
        {
            string bundle_id_path = Path.Combine(StaticStorage.settings.AssetsFolder, bundle_id);
            if (!File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, bundle_id + ".bundle")) || !File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, bundle_id + "_h.bundle")))
                return false;

            BundleHeader bundle = new BundleHeader();
            if (!bundle.Load(bundle_id_path))
            {
                StaticStorage.log.WriteLine(string.Format("[Update error] Failed to parse bundle header. ({0})", bundle_id));
                return false;
            }

            string bundle_file = bundle_id_path + ".bundle";
            if (!File.Exists(bundle_file))
            {
                StaticStorage.log.WriteLine(String.Format("[Update error] Bundle file does not exist. ({0})", bundle_file));
                return false;
            }

            foreach (BundleEntry be in bundle.Entries)
            {
                NameEntry ne = StaticStorage.Index.Id2Name(be.Id);
                if (ne != null && ne.Path == 0x9234DD22C60D71B8 && ne.Extension == 0x9234DD22C60D71B8)
                {
                    return true;
                }
            }
            return false;
        }*/


        private void TestGameVersion()
        {
            if (File.Exists(Path.GetFullPath(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "game.ver"))))
            {
                using (
                    var fs = new FileStream(Path.GetFullPath(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "game.ver")), FileMode.Open, FileAccess.Read)
                    )
                {
                    using (var sr = new StreamReader(fs))
                    {
                        string gameVersion = sr.ReadToEnd();
                        if (StaticStorage.settings.CurrentVersion != gameVersion)
                        {
                            this.DeleteBackups();
                            StaticStorage.settings.CurrentVersion = gameVersion;
                            this.SaveSettings();
                            this.RetrieveHashlist();
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     The undo last_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void UndoLastClick(object sender, EventArgs e)
        {

            if (this.AddedFilesView.SelectedRows.Count <= 0)
            {
                MessageBox.Show(
                "You did not select any replacement files to remove.",
                "No replacement entry selected");
                return;
            }

            foreach (DataGridViewRow row in this.AddedFilesView.SelectedRows)
            {
                BundleRewriteItem temp = (BundleRewriteItem)row.DataBoundItem;

                this.newMod.ItemQueue.Remove(temp);
            }

            if (this.newMod.ItemQueue.Count == 0)
                this.UndoLast.Enabled = false;

            this.AddedFilesView.DataSource = this.newMod.ItemQueue.ToList();
            this.AddedFilesView.Refresh();

        }

        #endregion

        private void AddMod_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "PAYDAY Mods(*.pdmod, *.zip)|*.pdmod;*.zip";
            openDialog.CheckFileExists = true;
            openDialog.Multiselect = true;
            if (openDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (string file in openDialog.FileNames)
            {
                if (!this.CheckMod(file))
                {
                    MessageBox.Show("The following mod could not be loaded:\n" + file, "Failed to load mod - " + Path.GetFileName(file));
                }
                else
                {
                    if (File.Exists(Path.Combine(LocalModsDir, Path.GetFileName(file))))
                    {
                        DialogResult decision = MessageBox.Show(
                            "One of your pdmod files already exists in your local library.\n" +
                            Path.GetFileName(file) + "\n" +
                            "Would you like to replace this file?",
                            "Attention",
                            MessageBoxButtons.YesNo);

                        if (decision == DialogResult.No)
                        {
                            continue;
                        }
                        else
                        {
                            File.Delete(Path.Combine(LocalModsDir, Path.GetFileName(file)));
                            File.Copy(file, Path.Combine(LocalModsDir, Path.GetFileName(file)));
                        }
                    }
                    else
                    {
                        File.Copy(file, Path.Combine(LocalModsDir, Path.GetFileName(file)));
                    }
                }

                this.mods_db.LoadSingleMod(Path.Combine(LocalModsDir, Path.GetFileName(file)));
            }
            this.mods_db.LoadMods();
            this.updateModsListView();
        }

        private void createModLoad_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "PAYDAY Mod Project|*.*";
            openDialog.CheckFileExists = true;
            if (openDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            this.newMod = new BundleMod();
            this.modScriptPath = Path.GetDirectoryName(openDialog.FileName);
            try
            {
                ParseModScript(openDialog.FileName);
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Mod Script Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            this.AddedFilesView.DataSource = this.newMod.ItemQueue.ToList();
            this.AddedFilesView.Refresh();
        }

        private void backupMethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.mods_db.installedModsList.Count > 0 && this.backupMethodComboBox.SelectedIndex != StaticStorage.settings.BackupType)
            {
                MessageBox.Show("Installed mods must be removed before changing this setting.", "Error!");
                this.backupMethodComboBox.SelectedIndex = StaticStorage.settings.BackupType;
            }
            else
            {
                StaticStorage.settings.BackupType = this.backupMethodComboBox.SelectedIndex;
                this.SaveSettings();
            }
        }

        private void savePDMod_Click(object sender, EventArgs e)
        {

            if (this.newMod.ItemQueue.Count < 1)
            {
                MessageBox.Show("You must add at least one replacement file before creating a modification package.");
                return;
            }

            if (this.ModNameEdit.TextLength <= 0 || this.ModAuthorEdit.TextLength <= 0)
            {
                MessageBox.Show("You must enter a name and author for this modification package.");
                return;
            }

            if (this.SpecificVersion.Text == null)
            {
                this.SpecificVersion.SelectedIndex = 0;
                //MessageBox.Show("You must select a enter a name and author for this modification package.");
                //return;
            }


            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PAYDAY Mod Project|*.*";
            saveDialog.FileName = this.ModNameEdit.Text + ".txt";
            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Dictionary<string, string> projectFiles = new Dictionary<string, string>();

            if (!Directory.Exists(Path.Combine(Path.GetTempPath(), "mod")))
                Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "mod"));

            foreach (var item in this.newMod.ItemQueue)
            {
                if (File.Exists(item.ReplacementFile))
                {
                    File.Copy(item.ReplacementFile, Path.Combine(Path.GetTempPath(), "mod", Path.GetFileName(item.ReplacementFile)), true);
                    projectFiles.Add(item.ReplacementFile, Path.Combine(Path.GetTempPath(), "mod", Path.GetFileName(item.ReplacementFile)));
                }
                else
                {
                    MessageBox.Show(
                    "File: " + item.ReplacementFile + "\nDoes not exist.",
                    "Missing File");
                    return;
                }
            }


            using (FileStream fs = new FileStream(Path.Combine(Path.GetTempPath(), Path.GetFileName(saveDialog.FileName)), FileMode.CreateNew))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("@Name " + this.ModNameEdit.Text);
                    sw.WriteLine("@Author " + this.ModAuthorEdit.Text);
                    sw.WriteLine("@Description " + this.ModDescriptionEdit.Text.Replace(System.Environment.NewLine, "\\r\\n"));
                    sw.WriteLine("@Version " + this.SpecificVersion.Text);

                    if (this.newMod.Variables.Count > 0)
                    {
                        sw.WriteLine();
                        sw.WriteLine(";Mod Variables included in this project");

                        foreach (var item in this.newMod.Variables)
                        {
                            sw.WriteLine("@var " + item.ToFullString());
                        }
                    }

                    sw.WriteLine();
                    sw.WriteLine(";Files included in this project");

                    foreach (var item in this.newMod.ItemQueue)
                    {
                        sw.WriteLine(item.SourceFile + " : " + Path.Combine("mod", Path.GetFileName(item.ReplacementFile)));
                    }
                }
            }


            //Save
            File.Copy(Path.Combine(Path.GetTempPath(), Path.GetFileName(saveDialog.FileName)), saveDialog.FileName, true);

            if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(saveDialog.FileName), "mod")))
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(saveDialog.FileName), "mod"));

            foreach (var pair in projectFiles)
            {
                File.Copy(pair.Value, Path.Combine(Path.GetDirectoryName(saveDialog.FileName), "mod", Path.GetFileName(pair.Key)), true);
            }

            //Clean up
            File.Delete(Path.Combine(Path.GetTempPath(), Path.GetFileName(saveDialog.FileName)));

            foreach (var pair in projectFiles)
            {
                File.Delete(pair.Value);
            }


            MessageBox.Show(
                    "Project successfully saved.",
                    "Project Saved");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.allowCheck = false;

            this.SpecificVersion.SelectedIndex = 0;
            this.fileReplacementType_ComboBox.SelectedIndex = 0;
            if (this.backupMethodComboBox.SelectedIndex < 0)
                this.backupMethodComboBox.SelectedIndex = 1;
            //this.ThemeSelection.SelectedIndex = 0;
            this.reports_FormattingComboBox.SelectedIndex = 0;

            this.availiableMods_listView.CheckBoxes = true;

            this.BundleFileName.AutoCompleteCustomSource = pathsAutoCompleteCollection;

            this.selectedPackage.Items.Clear();
            this.selectedPackage.Items.Add("Default");
            this.selectedPackage.SelectedIndex = 0;
            this.selectedPackage.Items.AddRange(packagesList.ToArray());
            this.selectedPackage.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            this.selectedPackage.AutoCompleteCustomSource.AddRange(packagesList.ToArray());

            this._utility_icons.Images.Add(Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("PDBundleModPatcher.Resources.accept-icon.png")));
            this._utility_icons.Images.Add(Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("PDBundleModPatcher.Resources.remove-icon.png")));
            this._utility_icons.Images.Add(Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("PDBundleModPatcher.Resources.warning-icon.png")));
            this._utility_icons.Images.Add(Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("PDBundleModPatcher.Resources.help-icon.png")));

            this.availiableMods_listView.SmallImageList = this._utility_icons;

            this.availiableModsSearch_textbox.ForeColor = Color.Gray;
            this.availiableModsSearch_textbox.Text = "Search availiable mods...";
            this.availiableModsSearch_textbox.GotFocus += (source, eventarg) =>
            {
                if (this.searchWatermark)
                {
                    this.searchWatermark = false;
                    this.availiableModsSearch_textbox.Text = "";
                    this.availiableModsSearch_textbox.ForeColor = Color.Black;
                }
            };

            this.availiableModsSearch_textbox.LostFocus += (source, eventarg) =>
            {
                if (!this.searchWatermark && string.IsNullOrEmpty(this.availiableModsSearch_textbox.Text))
                {
                    this.searchWatermark = true;
                    this.availiableModsSearch_textbox.Text = "Search availiable mods...";
                    this.availiableModsSearch_textbox.ForeColor = Color.Gray;
                }
            };

            this.allowCheck = true;
        }

        private void hashButton_Click(object sender, EventArgs e)
        {
            ulong hash = Hash64.HashString(this.hashTextBox.Text);

            if (this.hashUseHexcheckBox.Checked)
            {
                if (this.hashSwapHexEndiannessCheckbox.Checked)
                {
                    hash =
                        ((0x00000000000000FF) & (hash >> 56)
                        | (0x000000000000FF00) & (hash >> 40)
                        | (0x0000000000FF0000) & (hash >> 24)
                        | (0x00000000FF000000) & (hash >> 8)
                        | (0x000000FF00000000) & (hash << 8)
                        | (0x0000FF0000000000) & (hash << 24)
                        | (0x00FF000000000000) & (hash << 40)
                        | (0xFF00000000000000) & (hash << 56));
                }

                this.hashHashBox.Text = string.Format("{0:x}", hash);
            }
            else
                this.hashHashBox.Text = hash.ToString();
        }

        private void unHashButton_Click(object sender, EventArgs e)
        {
            string hashVal = this.hashHashBox.Text;
            ulong hash;

            try
            {
                string unhashed = "";
                if (this.hashUseHexcheckBox.Checked)
                {
                    unhashed = this.UnHashString(hashVal, this.hashSwapHexEndiannessCheckbox.Checked);
                    if (unhashed == null)
                        MessageBox.Show("Error while converting from hex to ulong", "Could not reverse the hash");
                    else if (unhashed == "")
                        MessageBox.Show("Entered hash is not part of this game.", "Could not reverse the hash");
                }
                else
                {
                    hash = Convert.ToUInt64(this.hashHashBox.Text);
                    if (StaticStorage.Known_Index.GetAny(hash) != null)
                    {
                        unhashed = StaticStorage.Known_Index.GetAny(hash);
                    }
                    else
                        MessageBox.Show("Entered hash is not part of this game.", "Could not reverse the hash");

                }

                if (!String.IsNullOrWhiteSpace(unhashed))
                    this.hashTextBox.Text = unhashed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Exception while converting");
            }
        }

        public string UnHashString(string hashVal, bool swap_edianness)
        {
            ulong hash;

            if (hashVal.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase))
                hashVal = hashVal.Substring(2);

            if (!ulong.TryParse(hashVal, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hash))
                return null;

            if (swap_edianness)
            {
                hash =
                    ((0x00000000000000FF) & (hash >> 56)
                    | (0x000000000000FF00) & (hash >> 40)
                    | (0x0000000000FF0000) & (hash >> 24)
                    | (0x00000000FF000000) & (hash >> 8)
                    | (0x000000FF00000000) & (hash << 8)
                    | (0x0000FF0000000000) & (hash << 24)
                    | (0x00FF000000000000) & (hash << 40)
                    | (0xFF00000000000000) & (hash << 56));
            }

            return StaticStorage.Known_Index.GetAny(hash) ?? "";
        }

        private void BundleFileName_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                BundleFileName.Select(BundleFileName.Text.Length, 0);
                BundleFileName.Focus();
            }
        }

        private void generateCorruptedReport()
        {
            if (this.checkedBundlesReport.Count == 0 || this.checkedDate == null)
                return;

            this.corruptedBundlesReport_richTextBox.ResetText();

            this.corruptedBundlesReport_richTextBox.AppendText("Bundles Corruption Check started on " + this.checkedDate + "\n\n");

            if (this.checkedBundlesReport.Count(e => !e.Value.Equals(String.Empty)) == 0 && this.corruptedShowOnlyCorrupted_checkbox.Checked)
            {
                this.corruptedBundlesReport_richTextBox.AppendText("No corrupted bundles\n");
            }
            else
            {
                foreach (KeyValuePair<String, String> keyvp in this.checkedBundlesReport)
                {
                    if (this.corruptedShowOnlyCorrupted_checkbox.Checked && keyvp.Value.Equals(String.Empty))
                        continue;

                    int startSelection = this.corruptedBundlesReport_richTextBox.TextLength;
                    this.corruptedBundlesReport_richTextBox.AppendText(keyvp.Key + ".bundle ... " + (keyvp.Value.Equals(String.Empty) ? "OK" : keyvp.Value) + "\n");
                    int endSelection = this.corruptedBundlesReport_richTextBox.TextLength;

                    if (!keyvp.Value.Equals(String.Empty))
                    {
                        this.corruptedBundlesReport_richTextBox.Select(startSelection, endSelection);
                        this.corruptedBundlesReport_richTextBox.SelectionColor = Color.Red;
                    }
                }
            }
        }

        private void runCorruptedCheckButton_Click(object sender, EventArgs e)
        {
            if (!this.ValidAssets)
            {
                MessageBox.Show("Your assets folder appears to be invalid. Please select a valid assets folder in Options.", "Invalid assets folder.");
                return;
            }
            if (this.rewriter != null)
                return;

            this.checkedDate = DateTime.Now;
            this.checkedBundlesReport.Clear();

            this.verifyCorruptedBundlesCheckCheckBox.Enabled = false;
            this.corruptedShowOnlyCorrupted_checkbox.Enabled = false;
            this.attemptRepair_button.Enabled = false;

            this.savedStateApplyMod = this.ApplyButton.Enabled;
            this.savedStateOpenMod = this.OpenModButton.Enabled;

            this.ApplyButton.Enabled = false;
            this.OpenModButton.Enabled = false;

            int BufferSize;
            if (!Int32.TryParse(this.patchingBufferSize.Text, out BufferSize))
            {
                BufferSize = 4096;
            }

            this.rewriter = new BundleRewriter(
                "",
                new BundleMod(),
                BufferSize,
                StaticStorage.settings.AssetsFolder,
                StaticStorage.settings.BackupType,
                StaticStorage.settings.OverrideFolder,
                StaticStorage.settings.OverrideFolderDummies,
                StaticStorage.settings.OverrideFolderShared,
                true,
                false);

            this.progressTimer = new Timer();
            this.progressTimer.Interval = 500;
            this.progressTimer.Tick += this.BundleCheckProgressTimerElapsed;
            this.progressTimer.Enabled = true;
            this.progressTimer.Start();
            rewriterThread = new Thread(() => this.rewriter.CheckBundles());
            rewriterThread.IsBackground = true;
            rewriterThread.Start();
        }

        private void availableMods_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availiableMods_listView.SelectedItems.Count == 1)
            {
                this._selectedMod = (BundleMod)availiableMods_listView.SelectedItems[0].Tag;
                this.ModNameText.Text = this._selectedMod.Name;
                this.AuthorText.Text = this._selectedMod.Author;
                this.DescriptionText.Text = this._selectedMod.Description;
                this.moreModDetails_button.Enabled = true;
                this.availiableMods_listView.SelectedItems[0].Focused = false;
                this.availiableMods_listView.SelectedItems[0].Selected = false;
                this.availiableMods_listView.Update();
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.mods_db.LoadMods();
            this.updateModsListView();
        }

        private void availableMods_listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (allowCheck)
            {
                BundleMod eMod = (BundleMod)e.Item.Tag;

                if (eMod.type == BundleMod.ModType.lua)
                {
                    eMod.enabled = e.Item.Checked;
                    this.mods_db.SaveBLTModManagement();
                }
                else
                {
                    if (this.mods_db.InstalledModsListContains(eMod) == -1 && eMod.status == BundleMod.ModStatus.NotInstalled) // Mod is NOT installed
                    {
                        if (e.Item.Checked)
                        {
                            //Marked for install
                            this.markModInstall(e.Item);
                        }
                        else
                        {
                            //Unmarked for install
                            this.unmarkMod(e.Item);

                            this.markupinstallModsCollision();
                        }
                    }
                    else
                    {
                        if (e.Item.Checked)
                        {
                            //Unmarked for removal
                            this.unmarkMod(e.Item);

                            this.markupinstallModsCollision();
                        }
                        else
                        {
                            //Marked for removal
                            this.markModUnInstall(e.Item);
                        }
                    }
                }
                this.refreshModsListView();
            }
        }

        private void availiableMods_listView_MouseClick(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListViewItem item = listView.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    this._rightclickedMod = (BundleMod)item.Tag;
                    item.Selected = true;
                    modContextMenuStrip.Show(listView, e.Location);
                }
            }
        }

        private void moreModDetails_button_Click(object sender, EventArgs e)
        {
            Form moreModDetails = new ModDetails(_selectedMod);
            moreModDetails.ShowDialog(this);
        }

        private void markForReinstallationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mods_db.InstalledModsListContains(this._rightclickedMod) == -1 && this._rightclickedMod.status != BundleMod.ModStatus.Installed)
            {
                MessageBox.Show("You cannot reinstall a mod that isn't installed.", "Error!");
                return;
            }

            if (this._rightclickedMod.actionStatus == BundleMod.ModActionStatus.ForcedReinstall)
            {
                foreach (var item in this._rightclickedMod.ItemQueue)
                {
                    item.toReinstall = false;
                }
                this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.None;
                this.markupModCollision(this._rightclickedMod, true);
            }
            else if (this._rightclickedMod.actionStatus != BundleMod.ModActionStatus.Install && this._rightclickedMod.actionStatus != BundleMod.ModActionStatus.Remove)
            {
                foreach (var item in this._rightclickedMod.ItemQueue)
                {
                    item.toReinstall = true;
                }
                this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.ForcedReinstall;
                this.markupModCollision(this._rightclickedMod);
            }
            this.markupinstallModsCollision();
            this.refreshModsListView();
        }

        private void removeFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mods_db.InstalledModsListContains(this._rightclickedMod) > -1)
            {
                MessageBox.Show("Installed mods must be uninstalled first", "Error!");
            }
            else
            {
                if (this._rightclickedMod.canRemove)
                {
                    this.mods_db.RemoveModsList(this._rightclickedMod);
                    this.updateModsListView();

                    this.markupinstallModsCollision();
                    this.refreshModsListView();
                }
                else
                {
                    MessageBox.Show("This mod is not allowed to be removed.", "Error!");
                }
            }

        }

        private void useOverrideFolderCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.OverrideFolder = this.useOverrideFolderCheckbox.Checked;

            if (StaticStorage.settings.OverrideFolder)
            {
                this.createOverrideFolderDummies.Enabled = true;
                this.useSharedPDMODTOOLfolder.Enabled = true;
            }
            else
            {
                this.createOverrideFolderDummies.Enabled = false;
                this.useSharedPDMODTOOLfolder.Enabled = false;
            }

            this.SaveSettings();
        }


        private void showOverrideFolderCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.OverrideFolder = this.useOverrideFolderCheckbox.Checked;

            if (StaticStorage.settings.OverrideFolder)
            {
                this.createOverrideFolderDummies.Enabled = true;
                this.useSharedPDMODTOOLfolder.Enabled = true;
            }
            else
            {
                this.createOverrideFolderDummies.Enabled = false;
                this.useSharedPDMODTOOLfolder.Enabled = false;
            }

            this.SaveSettings();
        }

        private void createOverrideFolderDummies_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.OverrideFolderDummies = this.createOverrideFolderDummies.Checked;
            this.SaveSettings();
        }
        private void ShowOverrideMods_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.ShowOverrideMods = this.checkBoxOverrideShow.Checked;
            /*if (StaticStorage.settings.ShowOverrideMods && !this.availiableMods_listView.Groups.Contains(this.listViewGroup2))
				this.availiableMods_listView.Groups.Insert (1, this.listViewGroup2);
			else
				this.availiableMods_listView.Groups.RemoveAt (1);*/

            this.SaveSettings();
            //this.mods_db.LoadMods();
            this.updateModsListView();
        }

        private void useSharedPDMODTOOLfolder_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.OverrideFolderShared = this.useSharedPDMODTOOLfolder.Checked;
            this.SaveSettings();
        }

        private void markForInstallationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mods_db.InstalledModsListContains(this._rightclickedMod) == -1 && this._rightclickedMod.status == BundleMod.ModStatus.Installed)
            {
                MessageBox.Show("You cannot install an installed mod.\r\nPlease use \"Toggle reinstallation\" to reinstall this mod.", "Error!");
                return;
            }

            if (this._rightclickedMod.actionStatus == BundleMod.ModActionStatus.Install)
            {
                foreach (var item in this._rightclickedMod.ItemQueue)
                    item.toReinstall = false;

                if (this._rightclickedMod.status == BundleMod.ModStatus.NotInstalled)
                {
                    this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.None;
                }
                else if (this._rightclickedMod.status == BundleMod.ModStatus.Installed)
                {
                    this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.None;
                }
                else if (this._rightclickedMod.status == BundleMod.ModStatus.ParticallyInstalled)
                {
                    this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.ForcedReinstall;
                }
                else if (this._rightclickedMod.status == BundleMod.ModStatus.Unrecognized)
                {
                    this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.Missing;
                }

                this.markupModCollision(this._rightclickedMod, true);
            }
            else if (this._rightclickedMod.actionStatus == BundleMod.ModActionStatus.None)
            {
                this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.Install;
                this.markupModCollision(this._rightclickedMod);
            }
            this.markupinstallModsCollision();
            this.refreshModsListView();
        }

        private void markForUninstallationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mods_db.InstalledModsListContains(this._rightclickedMod) == -1 && this._rightclickedMod.status == BundleMod.ModStatus.NotInstalled)
            {
                MessageBox.Show("You cannot uninstall a mod that isn't installed.", "Error!");
                return;
            }

            if (this._rightclickedMod.actionStatus == BundleMod.ModActionStatus.Remove)
            {
                foreach (var item in this._rightclickedMod.ItemQueue)
                    item.toReinstall = false;

                if (this._rightclickedMod.status == BundleMod.ModStatus.NotInstalled)
                {
                    this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.None;
                }
                else if (this._rightclickedMod.status == BundleMod.ModStatus.Installed)
                {
                    this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.None;
                }
                else if (this._rightclickedMod.status == BundleMod.ModStatus.ParticallyInstalled)
                {
                    this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.ForcedReinstall;
                }
                else if (this._rightclickedMod.status == BundleMod.ModStatus.Unrecognized)
                {
                    this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.Missing;
                }

                this.markupModCollision(this._rightclickedMod, true);
            }
            else if (this._rightclickedMod.actionStatus != BundleMod.ModActionStatus.Remove)
            {
                this._rightclickedMod.actionStatus = BundleMod.ModActionStatus.Remove;
                this.markupModCollision(this._rightclickedMod);
            }
            this.markupinstallModsCollision();
            this.refreshModsListView();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.modContextMenuStrip.Items[0].Visible = true;
            this.modContextMenuStrip.Items[1].Visible = true;
            this.modContextMenuStrip.Items[3].Visible = true;

            if (this._rightclickedMod.canInstall)
            {
                if (this._rightclickedMod.status == BundleMod.ModStatus.Installed)
                    this.modContextMenuStrip.Items[0].Enabled = false;
                else
                    this.modContextMenuStrip.Items[0].Enabled = true;
                this.modContextMenuStrip.Items[1].Enabled = true;
            }
            else
            {
                this.modContextMenuStrip.Items[0].Enabled = false;
                this.modContextMenuStrip.Items[1].Enabled = false;
            }

            if (this._rightclickedMod.canUninstall)
            {
                this.modContextMenuStrip.Items[2].Enabled = true;
            }
            else
            {
                this.modContextMenuStrip.Items[2].Enabled = false;
            }

            if (this._rightclickedMod.canRemove)
            {
                this.modContextMenuStrip.Items[3].Enabled = true;
            }
            else
            {
                this.modContextMenuStrip.Items[3].Enabled = false;
            }

            this.modContextMenuStrip.Items[5].Visible = false;
            this.modContextMenuStrip.Items[6].Visible = false;
            this.modContextMenuStrip.Items[7].Visible = false;

            if (this._rightclickedMod.type == BundleMod.ModType.PDMod)
            {
                this.modContextMenuStrip.Items[5].Visible = true;

                if (this._rightclickedMod.UtilizesOverride)
                {
                    this.modContextMenuStrip.Items[6].Visible = true;
                }
            }
            else if (this._rightclickedMod.type == BundleMod.ModType.mod_override)
            {
                this.modContextMenuStrip.Items[6].Visible = true;
            }
            else if (this._rightclickedMod.type == BundleMod.ModType.lua)
            {
                this.modContextMenuStrip.Items[7].Visible = true;

                //Incompatible options
                this.modContextMenuStrip.Items[0].Visible = false;
                this.modContextMenuStrip.Items[1].Visible = false;
                this.modContextMenuStrip.Items[3].Visible = false;
            }
        }

        private void filecontrol_button_Click(object sender, EventArgs e)
        {
            FileControl filecontrolForm = new FileControl();
            filecontrolForm.filecontrolDictionary = filecontrolDictionary;
            filecontrolForm.filecontrolSelectedDictionary = filecontrolSelectedDictionary;
            filecontrolForm.ShowDialog(this);

            filecontrolSelectedDictionary = filecontrolForm.filecontrolSelectedDictionary;
        }

        private void availiableModsSearch_textbox_TextChanged(object sender, EventArgs e)
        {
            if (this.availiableModsSearch_textbox.Text.StartsWith("status"))
            {
                this.availiableModsSearch_textbox.AutoCompleteMode = AutoCompleteMode.Suggest;
                AutoCompleteStringCollection status_collection = new AutoCompleteStringCollection();
                status_collection.Add("status:installed");
                status_collection.Add("status:not installed");
                status_collection.Add("status:partially installed");
                status_collection.Add("status:unknown");

                this.availiableModsSearch_textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.availiableModsSearch_textbox.AutoCompleteCustomSource = status_collection;

            }
            else if (this.availiableModsSearch_textbox.Text.StartsWith("type"))
            {
                this.availiableModsSearch_textbox.AutoCompleteMode = AutoCompleteMode.Suggest;
                AutoCompleteStringCollection type_collection = new AutoCompleteStringCollection();
                type_collection.Add("type:pdmod");
                type_collection.Add("type:override");
                type_collection.Add("type:lua");

                this.availiableModsSearch_textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.availiableModsSearch_textbox.AutoCompleteCustomSource = type_collection;
            }
            else if (this.availiableModsSearch_textbox.Text.StartsWith("marked"))
            {
                this.availiableModsSearch_textbox.AutoCompleteMode = AutoCompleteMode.Suggest;
                AutoCompleteStringCollection marked_collection = new AutoCompleteStringCollection();
                marked_collection.Add("marked:installation");
                marked_collection.Add("marked:uninstall");
                marked_collection.Add("marked:force reinstall");
                marked_collection.Add("marked:missing");

                this.availiableModsSearch_textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.availiableModsSearch_textbox.AutoCompleteCustomSource = marked_collection;
            }
            else
            {
                this.availiableModsSearch_textbox.AutoCompleteMode = AutoCompleteMode.None;
            }

            //if (!String.IsNullOrWhiteSpace(this.availiableModsSearch_textbox.Text) && !this.searchWatermark)
            updateModsListView();
        }

        private bool searchMod(BundleMod mod, String text)
        {
            if (String.IsNullOrWhiteSpace(this.availiableModsSearch_textbox.Text) || this.searchWatermark)
                return true;

            text = text.ToLower(CultureInfo.InvariantCulture);

            //special logic
            if (text.StartsWith("status:"))
            {
                if (text.TrimStart("status:".ToCharArray()).Equals("installed") &&
                    mod.status == BundleMod.ModStatus.Installed)
                    return true;
                else if ((text.TrimStart("status:".ToCharArray()).Equals("not installed") ||
                    text.TrimStart("status:".ToCharArray()).Equals("!installed")) &&
                    mod.status == BundleMod.ModStatus.NotInstalled)
                    return true;
                else if ((text.TrimStart("status:".ToCharArray()).Equals("partially installed") ||
                    text.TrimStart("status:".ToCharArray()).Equals("partly installed") ||
                    text.TrimStart("status:".ToCharArray()).Equals("part installed")) &&
                    mod.status == BundleMod.ModStatus.ParticallyInstalled)
                    return true;
                else if ((text.TrimStart("status:".ToCharArray()).Equals("unknown") ||
                    text.TrimStart("status:".ToCharArray()).Equals("?")) &&
                    mod.status == BundleMod.ModStatus.Unrecognized)
                    return true;
            }
            else if (text.StartsWith("type:"))
            {
                if (text.TrimStart("type:".ToCharArray()).Equals("pdmod") &&
                    mod.type == BundleMod.ModType.PDMod)
                    return true;
                else if (text.TrimStart("type:".ToCharArray()).Equals("override") &&
                    mod.type == BundleMod.ModType.mod_override)
                    return true;
                else if (text.TrimStart("type:".ToCharArray()).Equals("lua") &&
                    mod.type == BundleMod.ModType.lua)
                    return true;
            }
            else if (text.StartsWith("marked:"))
            {
                if ((text.TrimStart("marked:".ToCharArray()).Equals("install") ||
                    text.TrimStart("marked:".ToCharArray()).Equals("installation")) &&
                    mod.actionStatus == BundleMod.ModActionStatus.Install)
                    return true;
                else if ((text.TrimStart("marked:".ToCharArray()).Equals("remove") ||
                    text.TrimStart("marked:".ToCharArray()).Equals("removal") ||
                    text.TrimStart("marked:".ToCharArray()).Equals("uninstall") ||
                    text.TrimStart("marked:".ToCharArray()).Equals("delete")) &&
                    mod.actionStatus == BundleMod.ModActionStatus.Remove)
                    return true;
                else if ((text.TrimStart("marked:".ToCharArray()).Equals("reinstall") ||
                    text.TrimStart("marked:".ToCharArray()).Equals("reinstallation")) &&
                    mod.actionStatus == BundleMod.ModActionStatus.Reinstall)
                    return true;
                else if ((text.TrimStart("marked:".ToCharArray()).Equals("force reinstall") ||
                    text.TrimStart("marked:".ToCharArray()).Equals("force reinstallation") ||
                    text.TrimStart("marked:".ToCharArray()).Equals("freinstall")) &&
                    mod.actionStatus == BundleMod.ModActionStatus.ForcedReinstall)
                    return true;
                else if ((text.TrimStart("marked:".ToCharArray()).Equals("missing")) &&
                    mod.actionStatus == BundleMod.ModActionStatus.Missing)
                    return true;
            }

            if (mod.Name.ToLower(CultureInfo.InvariantCulture).Contains(this.availiableModsSearch_textbox.Text.ToLower(CultureInfo.InvariantCulture)) ||
                    mod.Author.ToLower(CultureInfo.InvariantCulture).Contains(this.availiableModsSearch_textbox.Text.ToLower(CultureInfo.InvariantCulture)) ||
                    mod.Description.ToLower(CultureInfo.InvariantCulture).Contains(this.availiableModsSearch_textbox.Text.ToLower(CultureInfo.InvariantCulture))
                    )
                return true;

            return false;
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool newMods = false;
            foreach (string file in files)
            {
                if (file.EndsWith(".zip") || file.EndsWith(".pdmod"))
                {
                    if (!this.CheckMod(file))
                    {
                        MessageBox.Show("The following mod could not be loaded:\n" + file, "Failed to load mod - " + Path.GetFileName(file));
                    }
                    else
                    {
                        if (File.Exists(Path.Combine(LocalModsDir, Path.GetFileName(file))))
                        {
                            DialogResult decision = MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                                "One of your pdmod files already exists in your local library.\n" +
                                Path.GetFileName(file) + "\n" +
                                "Would you like to overwrite this file?",
                                "Attention",
                                MessageBoxButtons.YesNo);

                            if (decision == DialogResult.No)
                            {
                                continue;
                            }
                            else
                            {
                                File.Delete(Path.Combine(LocalModsDir, Path.GetFileName(file)));
                                File.Copy(file, Path.Combine(LocalModsDir, Path.GetFileName(file)));
                            }
                        }
                        else
                        {
                            File.Copy(file, Path.Combine(LocalModsDir, Path.GetFileName(file)));
                        }

                        newMods = true;

                        this.mods_db.LoadSingleMod(Path.Combine(LocalModsDir, Path.GetFileName(file)));
                    }
                }
                StaticStorage.log.WriteLine(file);
            }

            if (newMods)
                this.updateModsListView();
        }

        private void BundleFileName_TextChanged(object sender, EventArgs e)
        {
            fileReplacementType_update();
        }

        private void ReplacementFileName_TextChanged(object sender, EventArgs e)
        {
            fileReplacementType_update();
        }

        private void fileReplacementType_update()
        {
            fileReplacementType_ComboBox.Items.Clear();
            /*
             * File Replacement
             * Patch Script
             * Strings Patch
             * Sound Bank Patch
             */
            fileReplacementType_ComboBox.Items.Add("File Replacement");

            if (ReplacementFileName.Text.EndsWith(".script"))
                fileReplacementType_ComboBox.Items.Add("Patch Script");

            if (BundleFileName.Text.EndsWith(".strings"))
                fileReplacementType_ComboBox.Items.Add("Strings Patch");
            else if (/*BundleFileName.Text.EndsWith(".stream") ||*/ BundleFileName.Text.EndsWith(".bnk"))
                fileReplacementType_ComboBox.Items.Add("Sound Bank Patch");

            fileReplacementType_ComboBox.SelectedIndex = 0;

        }

        private void bundleFilePathAutotompletion_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.Autocompletion = this.bundleFilePathAutotompletion_checkbox.Checked;
            this.SaveSettings();

            if (StaticStorage.settings.Autocompletion)
                this.BundleFileName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            else
                this.BundleFileName.AutoCompleteMode = AutoCompleteMode.None;
        }

        private void availiableMods_listView_Resize(object sender, EventArgs e)
        {
            this.availiableMods_listView.Columns[0].Width = (int)(availiableMods_listView.Width * 0.92);
        }

        private void playsoundoncompletion_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.PlaySoundOnCompletion = this.playsoundoncompletion_checkbox.Checked;
            this.SaveSettings();
        }

        private void modVariables_button_Click(object sender, EventArgs e)
        {
            ModVariables viewModVars = new ModVariables(this.newMod.Variables);
            viewModVars.ShowDialog(this);
        }

        private void corruptedShowOnlyCorrupted_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            this.generateCorruptedReport();
        }

        private void rungameoncompletion_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.RunGameOnCompletion = this.rungameoncompletion_checkbox.Checked;
            this.SaveSettings();
        }

        private void fileReplacementType_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.fileReplacementType_ComboBox.Text.Equals("File Replacement"))
            {
                this.BrowseForReplacement.Enabled = false;
                this.BrowseForReplacement.Visible = false;

                this.ConfigureReplacementButton.Enabled = true;
                this.ConfigureReplacementButton.Visible = true;
            }
            else
            {
                this.BrowseForReplacement.Enabled = true;
                this.BrowseForReplacement.Visible = true;

                this.ConfigureReplacementButton.Enabled = false;
                this.ConfigureReplacementButton.Visible = false;
            }
        }

        private void ConfigureReplacementButton_Click(object sender, EventArgs e)
        {
            /*
             * File Replacement
             * Patch Script
             * Strings Patch
             * Sound Bank Patch
             */

            ulong path, extension;
            uint language;

            switch (this.fileReplacementType_ComboBox.Text)
            {
                case ("File Replacement"):
                    return;
                case ("Strings Patch"):
                    if (!this.CheckBundleFileName(this.BundleFileName.Text.ToLower().Replace('\\', '/'), out path, out extension, out language))
                    {
                        return;
                    }
                    BundleRewriteItem tempBRI = new BundleRewriteItem(path, language, extension);
                    tempBRI.IsLanguageSpecific = (Path.GetFileName(this.BundleFileName.Text).Split('.').Length == 3);

                    BundleRewriter br = new BundleRewriter(StaticStorage.settings.AssetsFolder);
                    MemoryStream stringsStream = new MemoryStream();
                    br.RetrieveFile(tempBRI, out stringsStream);
                    DieselStrings ds = new DieselStrings(stringsStream);

                    ManageStrings msForm = new ManageStrings(ds);
                    msForm.ShowDialog(this);
                    /*
                    FileControl filecontrolForm = new FileControl();
                    filecontrolForm.filecontrolDictionary = filecontrolDictionary;
                    filecontrolForm.filecontrolSelectedDictionary = filecontrolSelectedDictionary;
                    filecontrolForm.ShowDialog(this);

                    filecontrolSelectedDictionary = filecontrolForm.filecontrolSelectedDictionary;
                    */
                    break;
                case ("Sound Bank Patch"):

                    if (!this.CheckBundleFileName(this.BundleFileName.Text.ToLower().Replace('\\', '/'), out path, out extension, out language))
                    {
                        return;
                    }
                    BundleRewriteItem soundtempBRI = new BundleRewriteItem(path, language, extension);
                    soundtempBRI.IsLanguageSpecific = (Path.GetFileName(this.BundleFileName.Text).Split('.').Length == 3);

                    BundleRewriter soundbr = new BundleRewriter(StaticStorage.settings.AssetsFolder);
                    MemoryStream soundStream = new MemoryStream();

                    soundbr.RetrieveFile(soundtempBRI, out soundStream);

                    SoundPatch spForm = new SoundPatch(Path.GetFileNameWithoutExtension(this.BundleFileName.Text.ToLower()), soundStream);
                    spForm.ShowDialog(this);

                    break;
                default:
                    break;
            }

        }

        private void patchingBufferSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.PatchBuffer = this.patchingBufferSize.SelectedIndex;
            this.SaveSettings();
        }

        private void viewModConfig_Click(object sender, EventArgs e)
        {

        }

        private void reports_GenerateReportButton_Click(object sender, EventArgs e)
        {
            string localappdatapath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#if LINUX
				string pd2crashPath = Path.Combine(localappdatapath, "Starbreeze", StaticStorage.settings.GameShortName.Replace(" ", ""), "crash.txt");
#else
            string pd2crashPath = Path.Combine(localappdatapath, StaticStorage.settings.GameShortName, "crash.txt");
#endif

            this.reports_ReportRichTextBox.Clear();

            if (!File.Exists(pd2crashPath))
            {
                this.reports_ReportRichTextBox.AppendText("Crash file not found.\r\nPath: " + pd2crashPath);
                return;
            }
#if LINUX
				bool luaHookPresent = File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "libblt_loader.so"));
#else
            bool luaHookPresent = File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "IPHLPAPI.dll"));
#endif
            bool luaHookBLTDataPresent = File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods", "base", "mod.txt"));
            //bool luaHookHoxHudDataPresent = File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods", "base", "mod.txt"));

            bool formatting_isReddit = false;
            string formatting_Header = "";
            string formatting_Header_Close = "";
            string formatting_Code = "";
            string formatting_Code_Close = "";
            bool formatting_Code_PrefixHeader = false;
            string formatting_Highlight = "";
            string formatting_Highlight_Close = "";
            string formatting_List = "";
            string formatting_List_Item = "   ";
            string formatting_List_Close = "";

            switch (this.reports_FormattingComboBox.SelectedIndex)
            {
                case (1):
                    formatting_isReddit = false;
                    formatting_Header = "[h1]";
                    formatting_Header_Close = "[/h1]";
                    formatting_Code = "[code]";
                    formatting_Code_Close = "[/code]";
                    formatting_Code_PrefixHeader = false;
                    formatting_Highlight = "[b]";
                    formatting_Highlight_Close = "[/b]";
                    formatting_List = "[list]";
                    formatting_List_Item = "[*]";
                    formatting_List_Close = "[/list]";
                    break;
                case (2):
                    formatting_isReddit = true;
                    formatting_Header = "**";
                    formatting_Header_Close = "**";
                    formatting_Code = "    ";
                    formatting_Code_PrefixHeader = true;
                    formatting_Highlight = "*";
                    formatting_Highlight_Close = "*";
                    formatting_List = "";
                    formatting_List_Item = "* ";
                    formatting_List_Close = "";
                    break;
            }


            FileInfo pd2crashInfo = new FileInfo(pd2crashPath);

            //Report begins
            this.reports_ReportRichTextBox.AppendText("Crash date: " + pd2crashInfo.LastWriteTime.ToString() + "\r\n\r\n" + (formatting_isReddit ? "\r\n" : ""));
            FileStream pd2crashfs = new FileStream(pd2crashPath, FileMode.Open);

            this.reports_ReportRichTextBox.AppendText(formatting_Header + "PAYDAY 2 Crash log" + formatting_Header_Close + "\r\n" + (formatting_isReddit ? "\r\n" : ""));

            //PAYDAY Crashlog
            if (!formatting_Code_PrefixHeader && !String.IsNullOrWhiteSpace(formatting_Code) && !String.IsNullOrWhiteSpace(formatting_Code_Close))
                this.reports_ReportRichTextBox.AppendText(formatting_Code);
            using (StreamReader pd2crashsr = new StreamReader(pd2crashfs))
            {
                string line = "";
                for (; (line = pd2crashsr.ReadLine()) != null;)
                {
                    if (String.IsNullOrWhiteSpace(line)) continue;
                    this.reports_ReportRichTextBox.AppendText((formatting_Code_PrefixHeader ? formatting_Code : "") + line + "\r\n");
                }
            }
            if (!formatting_Code_PrefixHeader && !String.IsNullOrWhiteSpace(formatting_Code) && !String.IsNullOrWhiteSpace(formatting_Code_Close))
                this.reports_ReportRichTextBox.AppendText(formatting_Code_Close);

            //Lua Info
            this.reports_ReportRichTextBox.AppendText("\r\n\r\n");
            if (this.reports_IncludeLuaInformationCheckBox.Checked)
            {
                this.reports_ReportRichTextBox.AppendText(formatting_Header + "Lua information" + formatting_Header_Close + "\r\n" + (formatting_isReddit ? "\r\n" : ""));

                this.reports_ReportRichTextBox.AppendText("Lua Hook: " + (luaHookPresent ? formatting_Highlight + "present" + formatting_Highlight_Close : "not present") + "\r\n" + (formatting_isReddit ? "\r\n" : ""));

                this.reports_ReportRichTextBox.AppendText("BLT Hook data: " + (luaHookBLTDataPresent ? formatting_Highlight + "present" + formatting_Highlight_Close : "not present") + "\r\n" + (formatting_isReddit ? "\r\n" : ""));

                //this.reports_ReportRichTextBox.AppendText("HoxHud data: " + (luaHookHoxHudDataPresent ? formatting_Highlight + "present" + (this.reports_FormatForSteamCommunityUseCheckBox.Checked ? "[/b]" : "") : "not present") + "\r\n");

                this.reports_ReportRichTextBox.AppendText("\r\n\r\n");
            }

            //BLT Logs
            if (this.reports_IncludeLuaInformationCheckBox.Checked && this.reports_LuaBLTIncludeLog.Checked)
            {
                this.reports_ReportRichTextBox.AppendText(formatting_Header + "Lua BLT Log (+/- 1 minute)" + formatting_Header_Close + "\r\n" + (formatting_isReddit ? "\r\n" : ""));

                string bltlogpath = Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods", "logs", pd2crashInfo.LastWriteTime.Year.ToString("0000") + "_" + pd2crashInfo.LastWriteTime.Month.ToString("00") + "_" + pd2crashInfo.LastWriteTime.Day.ToString("00") + "_log.txt");


                DateTime baseCrashTime = new DateTime(pd2crashInfo.LastWriteTime.Ticks);
                DateTime futureCrashTime = new DateTime(baseCrashTime.Ticks);
                futureCrashTime.AddMinutes(1.0d);
                DateTime pastCrashTime = new DateTime(baseCrashTime.Ticks);
                pastCrashTime.AddMinutes(-1.0d);

                StringBuilder logsb = new StringBuilder();

                if (File.Exists(bltlogpath))
                {
                    string[] logLines = File.ReadAllLines(bltlogpath);

                    foreach (string logline in logLines)
                    {
                        if (logline.StartsWith(baseCrashTime.ToString("hh:mm")) ||
                            logline.StartsWith(futureCrashTime.ToString("hh:mm")) ||
                            logline.StartsWith(pastCrashTime.ToString("hh:mm")))
                        {
                            if (String.IsNullOrWhiteSpace(logline)) continue;
                            logsb.AppendLine((formatting_Code_PrefixHeader ? formatting_Code : "") + logline);
                        }
                    }
                }
                else
                {
                    logsb.AppendLine((formatting_Code_PrefixHeader ? formatting_Code : "") + "BLT Log file was not found.");
                }

                if (!formatting_Code_PrefixHeader && !String.IsNullOrWhiteSpace(formatting_Code) && !String.IsNullOrWhiteSpace(formatting_Code_Close))
                    this.reports_ReportRichTextBox.AppendText(formatting_Code);
                if (logsb.Length == 0)
                {
                    this.reports_ReportRichTextBox.AppendText((formatting_Code_PrefixHeader ? formatting_Code : "") + "No logs were found\r\n");
                }
                else
                {
                    this.reports_ReportRichTextBox.AppendText(logsb.ToString() + "\r\n");
                }
                if (!formatting_Code_PrefixHeader && !String.IsNullOrWhiteSpace(formatting_Code) && !String.IsNullOrWhiteSpace(formatting_Code_Close))
                    this.reports_ReportRichTextBox.AppendText(formatting_Code_Close);

                this.reports_ReportRichTextBox.AppendText("\r\n\r\n");
            }

            //BLT Mods
            if (this.reports_IncludeLuaInformationCheckBox.Checked && this.reports_IncludeLuaModsList.Checked)
            {
                this.reports_ReportRichTextBox.AppendText(formatting_Header + "Lua BLT Mods list" + formatting_Header_Close + "\r\n" + (formatting_isReddit ? "\r\n" : ""));

                if (Directory.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods")))
                {
                    List<string> bltmods = Directory.EnumerateDirectories(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods")).ToList();

                    if (!String.IsNullOrWhiteSpace(formatting_List) && !String.IsNullOrWhiteSpace(formatting_List_Close))
                        this.reports_ReportRichTextBox.AppendText(formatting_List + "\r\n");

                    foreach (string bltmod in bltmods)
                    {
                        if (!Directory.Exists(bltmod))
                            continue;

                        if (Path.GetFileNameWithoutExtension(bltmod).Equals("log"))
                            continue;

                        if (!File.Exists(Path.Combine(bltmod, "mod.txt")))
                            continue;

                        FileStream bltModfs = new FileStream(Path.Combine(bltmod, "mod.txt"), FileMode.Open);

                        using (StreamReader bltModsr = new StreamReader(bltModfs))
                        {
                            try
                            {
                                dynamic jsonDe = JsonConvert.DeserializeObject(bltModsr.ReadToEnd());

                                if (jsonDe != null && jsonDe.name != null && jsonDe.author != null && jsonDe.version != null)
                                {
                                    this.reports_ReportRichTextBox.AppendText(formatting_List_Item + "Name: " + jsonDe.name + ", Version: " + jsonDe.version + ", Author: " + jsonDe.author + "\r\n" + (formatting_isReddit ? "\r\n" : ""));
                                }
                            }
                            catch (Exception exc)
                            {
                                int startSelection = this.reports_ReportRichTextBox.TextLength;
                                this.reports_ReportRichTextBox.AppendText(formatting_List_Item + "Failed parsing mods.txt of " + Path.GetFileNameWithoutExtension(bltmod) + ", Message: " + exc.Message + "\r\n" + (formatting_isReddit ? "\r\n" : ""));
                                int endSelection = this.reports_ReportRichTextBox.TextLength;

                                this.reports_ReportRichTextBox.Select(startSelection, endSelection);
                                this.reports_ReportRichTextBox.SelectionColor = Color.Red;
                            }
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(formatting_List) && !String.IsNullOrWhiteSpace(formatting_List_Close))
                        this.reports_ReportRichTextBox.AppendText(formatting_List_Close + "\r\n");
                }
                else
                {
                    int startSelection = this.reports_ReportRichTextBox.TextLength;
                    this.reports_ReportRichTextBox.AppendText("Lua BLT Mods directory wasn't found. Corrupt install?\r\n" + (formatting_isReddit ? "\r\n" : ""));
                    int endSelection = this.reports_ReportRichTextBox.TextLength;

                    this.reports_ReportRichTextBox.Select(startSelection, endSelection);
                    this.reports_ReportRichTextBox.SelectionColor = Color.Red;
                }

                this.reports_ReportRichTextBox.AppendText("\r\n\r\n");
            }

            if (this.reports_IncludePdmodModsList.Checked)
            {
                Dictionary<string, BundleMod> pdmods = this.mods_db.modsList;

                this.reports_ReportRichTextBox.AppendText(formatting_Header + "Installed PDMods/Override list" + formatting_Header_Close + "\r\n" + (formatting_isReddit ? "\r\n" : ""));

                if (!String.IsNullOrWhiteSpace(formatting_List) && !String.IsNullOrWhiteSpace(formatting_List_Close))
                    this.reports_ReportRichTextBox.AppendText(formatting_List + "\r\n");

                foreach (KeyValuePair<string, BundleMod> kvpmod in pdmods)
                {

                    if (kvpmod.Value.status == BundleMod.ModStatus.NotInstalled)
                        continue;
                    if (kvpmod.Value.type == BundleMod.ModType.lua)
                        continue;

                    try
                    {
                        this.reports_ReportRichTextBox.AppendText(formatting_List_Item + "Name: " + kvpmod.Value.Name + ", Type: " + kvpmod.Value.type.ToString() + ", State: " + kvpmod.Value.status.ToString() + ", Author: " + kvpmod.Value.Author + "\r\n" + (formatting_isReddit ? "\r\n" : ""));
                    }
                    catch (Exception exc)
                    {
                        int startSelection = this.reports_ReportRichTextBox.TextLength;
                        this.reports_ReportRichTextBox.AppendText(formatting_List_Item + "Failed parsing mods.txt of " + Path.GetFileNameWithoutExtension(kvpmod.Key) + ", Message: " + exc.Message + "\r\n" + (formatting_isReddit ? "\r\n" : ""));
                        int endSelection = this.reports_ReportRichTextBox.TextLength;

                        this.reports_ReportRichTextBox.Select(startSelection, endSelection);
                        this.reports_ReportRichTextBox.SelectionColor = Color.Red;
                    }
                }

                if (!String.IsNullOrWhiteSpace(formatting_List) && !String.IsNullOrWhiteSpace(formatting_List_Close))
                    this.reports_ReportRichTextBox.AppendText(formatting_List_Close + "\r\n");
                this.reports_ReportRichTextBox.AppendText("\r\n\r\n");
            }

        }

        private void reports_IncludeLuaInformationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.reports_LuaBLTIncludeLog.Enabled = this.reports_IncludeLuaInformationCheckBox.Checked;
            this.reports_IncludeLuaModsList.Enabled = this.reports_IncludeLuaInformationCheckBox.Checked;
        }

        private void modConfiguration_button_Click(object sender, EventArgs e)
        {
            ModOptions viewModOptions = new ModOptions(true);
            viewModOptions.modVariables = this.newMod.Variables.ToList();
            viewModOptions.Location = new Point(this.Location.X + (this.Width - viewModOptions.Width) / 2, this.Location.Y + (this.Height - viewModOptions.Height) / 2);
            viewModOptions.Show(this);

            /*
            HashSet<string> replacementFiles = new HashSet<string>();
            foreach (BundleRewriteItem item in this.newMod.ItemQueue)
            {
                if (!replacementFiles.Contains(Path.GetFileName(item.ReplacementFile)))
                    replacementFiles.Add(Path.GetFileName(item.ReplacementFile));
            }

            ModVariables viewModVars = new ModVariables(this.newMod.Variables, replacementFiles);
            viewModVars.ShowDialog(this);
            */
        }

        private void ShowFileFolder(object sender, EventArgs e)
        {
#if LINUX
				Process.Start("/usr/bin/xdg-open", String.Format("\"{0}\"", Path.GetDirectoryName(this._rightclickedMod.file) + "/"));
#else
            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", this._rightclickedMod.file));
#endif
        }

        private void showInModoverrideFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowFileFolder(sender, e);
        }

        private void showInBLTModsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowFileFolder(sender, e);
        }

        private void showPDModFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowFileFolder(sender, e);
        }

        private void checkupdatesonstartup_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.CheckForUpdatesOnLaunch = this.checkupdatesonstartup_checkbox.Checked;
            this.SaveSettings();
        }

        private void checkForUpdates_Button_Click(object sender, EventArgs e)
        {
            this.checkForUpdate(false);
        }

        private void ExtraOptions_OnClick(object sender, EventArgs e)
        {
            ExtraOptForm ExtraOpt = new ExtraOptForm();
            ExtraOpt.ShowDialog(this);
        }

        public void mark_all(ExtraOptForm extraOptForms, String type)
        {
            if (type == "install")
            {
                foreach (ListViewItem item in this.availiableMods_listView.Items)
                {
                    BundleMod mod = (BundleMod)item.Tag;
                    if (this.mods_db.InstalledModsListContains(mod) == -1 && (mod.status == BundleMod.ModStatus.NotInstalled))
                    {
                        item.Checked = true;
                        //foreach (var queueitem in mod.ItemQueue)
                        //{
                        //queueitem.toReinstall = true;
                        //}
                        mod.actionStatus = BundleMod.ModActionStatus.Install;
                        this.markupModCollision(mod);
                    }

                }
            }
            else if (type == "reinstall")
            {
                foreach (ListViewItem item in this.availiableMods_listView.Items)
                {
                    BundleMod mod = (BundleMod)item.Tag;
                    if (this.mods_db.InstalledModsListContains(mod) != -1 && mod.status == BundleMod.ModStatus.Installed)
                    {
                        foreach (var queueitem in mod.ItemQueue)
                        {
                            queueitem.toReinstall = true;
                        }
                        mod.actionStatus = BundleMod.ModActionStatus.ForcedReinstall;
                        this.markupModCollision(mod);
                    }

                }
            }
            else if (type == "uninstall")
            {
                foreach (ListViewItem item in this.availiableMods_listView.Items)
                {
                    BundleMod mod = (BundleMod)item.Tag;
                    if (mod.status != BundleMod.ModStatus.NotInstalled)
                    {
                        if (mod.type != BundleMod.ModType.lua)
                            item.Checked = false;

                        mod.actionStatus = BundleMod.ModActionStatus.Remove;
                        this.markupModCollision(mod);
                    }

                }
            }
            this.markupinstallModsCollision();
            this.refreshModsListView();
        }


        private void ThemSelectionChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.ThemeSelection = this.ThemeSelection.SelectedIndex;
            this.SaveSettings();
        }

        #region Bundle Extraction

        static private NameIndex NameIndex = StaticStorage.Index;
        static private KnownIndex KnownIndex = StaticStorage.Known_Index;
        static private BundleHeader bundle = new BundleHeader();
        int logtracker = 0;
        string StartLabel = "Start";
        string StopLabel = "Stop";

        void TextWriteLine(string line, params object[] extra)
        {
            this.txtExtractLog.AppendText(StaticStorage.log.WriteLine(string.Format(line, extra), true));
            this.txtExtractLog.ScrollToCaret();
        }

        public Thread bundleThread;
        Timer ExtractTimer;
        private void StartExtractingClick(object sender, EventArgs e)
        {
            if (!this.btnStartExtracting.Text.Equals(StopLabel))
            {
                if (this.progressTimer != null && this.progressTimer.Enabled)
                {
                    MessageBox.Show("Cannot perform 2 actions at once", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.tabExtractOptions.SelectedIndex == 1 && this.clstSelectInformation.CheckedItems.Count == 0)
                {
                    MessageBox.Show("You must select atleast one piece of information to be included in the list!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!String.IsNullOrWhiteSpace(StaticStorage.settings.CustomExtractPath) && !Directory.Exists(StaticStorage.settings.CustomExtractPath))
                {
                    MessageBox.Show("Selected custom extract directory does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StaticStorage.settings.CustomExtractPath = "";
                    this.txtExtractFolder.Text = "";
                    this.SaveSettings();
                    return;
                }

                if (!this.chkExtractAll.Checked && !File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, this.txtSingleBundle.Text + ".bundle")))
                {
                    MessageBox.Show("Entered single bundle does not exist in your game directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!this.ValidAssets)
                {
                    MessageBox.Show("Your selected assets folder is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (StaticStorage.settings.ShowExtractProblem && this.tabExtractOptions.SelectedIndex == 0 && String.IsNullOrWhiteSpace(StaticStorage.settings.CustomExtractPath))
                {
                    DialogResult dialogResult = MessageBox.Show("No custom extract folder chosen. The files will be extracted to " + Path.Combine(StaticStorage.settings.AssetsFolder, "extract") + "\n\nWould you like to continue? (If yes, then this message will not be displayed anymore)", "Extract Folder problem", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        StaticStorage.settings.ShowExtractProblem = false;
                        this.SaveSettings();
                    }
                    else if (dialogResult == DialogResult.No)
                        return;
                }
                this.btnStartExtracting.Text = StopLabel;

                this.SetExtractOptionsEnabled(false);
                ExtractTimer = new Timer();
                ExtractTimer.Interval = 100;
                ExtractTimer.Tick += this.UpdateProgressTick;
                ExtractTimer.Enabled = true;
                StaticStorage.extract = new BundleExtraction(this.chkExtractAll.Checked ? null : this.txtSingleBundle.Text, this.tabExtractOptions.SelectedIndex == 1, this.clstSelectInformation.CheckedItems, (string)this.cmbFormat.SelectedItem);
                bundleThread = new Thread(() => StaticStorage.extract.Start());
                bundleThread.IsBackground = true;
                bundleThread.Start();
                ExtractTimer.Start();
            }
            else
            {
                StaticStorage.extract.Terminate();
                this.prgExtractMain.Style = ProgressBarStyle.Marquee;
                this.lblExtractProgress.Text = "Canceling...";
                this.btnStartExtracting.Enabled = false;
            }
        }

        private void CustomExtractClick(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            //folderDialog.ShowNewFolderButton = false;
            #if !LINUX
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            #endif
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                StaticStorage.settings.CustomExtractPath = folderDialog.SelectedPath;
                this.SaveSettings();
                this.txtExtractFolder.Text = StaticStorage.settings.CustomExtractPath;
            }
        }

        private void WriteConsoleCheckChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.WriteConsole = this.WriteConsole.Checked;
            this.SaveSettings();
        }

        private void SetExtractOptionsEnabled(bool enabled)
        {
            this.tabExtractOptions.Enabled = enabled;
            this.flpGeneralExtractOptions.Enabled = enabled;
        }

        private void chkExtractAll_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSingleBundle.Enabled = !this.chkExtractAll.Checked;
        }

        private void FinishExtract()
        {
            this.ExtractTimer.Enabled = false;
            this.ExtractTimer.Stop();
            bundleThread.Abort();
            bundleThread = null;

            logtracker = 0;
            this.btnStartExtracting.Text = StartLabel;
            this.lblExtractProgress.Text = "- Entries: 0/0, Bundle: 0/0";
            this.prgExtractMain.Style = ProgressBarStyle.Continuous;
            this.prgExtractMain.Value = 0;
            this.prgExtractSubProgress.Value = 0;
            this.btnStartExtracting.Enabled = true;
            StaticStorage.extract.log.Clear();

            this.SetExtractOptionsEnabled(true);

            this.btnExtractFolder.Enabled = true;
            TextWriteLine("Finished!");

            StaticStorage.extract = null;
            MessageBox.Show("Extract/List operation completed!", "Finished!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateProgressTick(object sender, EventArgs args)
        {
            string[] logs = StaticStorage.extract.getLog();
            this.prgExtractMain.Value = (int)(((float)StaticStorage.extract.current_bundle / StaticStorage.extract.total_bundle) * 100);
            this.prgExtractSubProgress.Value = (int)(((float)StaticStorage.extract.current_bundle_progress / StaticStorage.extract.current_bundle_total_progress) * 100);
            if (this.prgExtractMain.Style != ProgressBarStyle.Marquee)
            {
                this.lblExtractProgress.Text = String.Format("{0} Entries: {1}/{2}, Bundle: {3}/{4}", StaticStorage.extract.current_bundle_name, StaticStorage.extract.current_bundle_progress, StaticStorage.extract.current_bundle_total_progress, StaticStorage.extract.current_bundle, StaticStorage.extract.total_bundle);
            }
            if (logs.Length > logtracker)
            {
                for (; logtracker < logs.Length; logtracker++)
                {
                    this.txtExtractLog.SelectionStart = this.txtExtractLog.TextLength;
                    this.txtExtractLog.SelectionLength = 0;
                    this.txtExtractLog.AppendText(logs[logtracker].ToString());
                }

                this.txtExtractLog.ScrollToCaret();

            }
            if (StaticStorage.extract.Finished && logs.Length == logtracker)
                FinishExtract();
        }

        private void BrowseForLogClick(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() != DialogResult.OK)
                return;

            this.txtListFile.Text = openDialog.FileName;
            StaticStorage.settings.ListLogFile = openDialog.FileName;
            this.SaveSettings();
        }

        public void Refresh_lstChangeExtensionView()
        {
            this.lstChangeExtension.Items.Clear();
            foreach (KeyValuePair<string, string> ext_change in StaticStorage.settings.ExtensionConversion)
            {
                this.lstChangeExtension.Items.Add(new ListViewItem(new string[] { ext_change.Key, ext_change.Value }) { Name = ext_change.Key });
            }
        }

        private void lstChangeExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item;
            ListView lst;
            if ((lst = sender as ListView)?.SelectedItems?.Count > 0 && (item = lst?.SelectedItems[0]) != null)
            {
                this.txtExtensionMod.Text = item.Name;
                this.txtExtensionReplacement.Text = item.SubItems[1].Text;
            }
        }

        private void btnApplyExtensionChange_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.txtExtensionReplacement.Text))
            {
                MessageBox.Show("Extension Replacement must not be empty or white space!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (StaticStorage.settings.ExtensionConversion.ContainsKey(this.txtExtensionMod.Text))
                StaticStorage.settings.ExtensionConversion[this.txtExtensionMod.Text] = this.txtExtensionReplacement.Text;
            else
                StaticStorage.settings.ExtensionConversion.Add(this.txtExtensionMod.Text, this.txtExtensionReplacement.Text);

            this.txtExtensionMod.Text = "";
            this.txtExtensionReplacement.Text = "";
            this.SaveSettings();
            this.Refresh_lstChangeExtensionView();
        }

        private void btnDeleteExtMod_Click(object sender, EventArgs e)
        {
            ListViewItem item;
            if (this.lstChangeExtension.SelectedItems.Count > 0 && (item = this.lstChangeExtension.SelectedItems[0]) != null)
            {
                StaticStorage.settings.ExtensionConversion.Remove(item.Name);
                this.lstChangeExtension.Items.Remove(item);
                this.txtExtensionMod.Text = "";
                this.txtExtensionReplacement.Text = "";
            }
            else
                MessageBox.Show("No item has been selected for deletion!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        private void chkIgnoreExisting_CheckedChanged(object sender, EventArgs e)
        {
            StaticStorage.settings.IgnoreExistingFiles = this.chkIgnoreExisting.Checked;
            this.SaveSettings();
        }
    }

    /// <summary>
    ///     The static storage.
    /// </summary>
    /// 


    public static class StaticStorage
    {
        #region Static Fields

        /// <summary>
        ///     The index.
        /// </summary>
        public static NameIndex Index = new NameIndex();

        /// <summary>
        ///     The known index.
        /// </summary>
        public static KnownIndex Known_Index = new KnownIndex();

        /// <summary>
        ///     The known index.
        /// </summary>
        public static ProgramSettings settings = new ProgramSettings();

        /// <summary>
        /// Logging Class
        /// </summary>
        public static log log;

        /// <summary>
        /// Bundle Extractor class
        /// </summary>
        public static BundleExtraction extract;

        #endregion
    }

    /// <summary>
    ///     The added file.
    /// </summary>
    public class AddedFile
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the new file.
        /// </summary>
        public string NewFile { get; set; }

        /// <summary>
        ///     Gets or sets the old file.
        /// </summary>
        public string OldFile { get; set; }

        #endregion
    }
}