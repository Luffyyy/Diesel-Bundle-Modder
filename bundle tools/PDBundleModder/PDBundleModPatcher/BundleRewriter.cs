// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleRewriter.cs" company="">
//   
// </copyright>
// <summary>
//   The bundle rewrite item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PDBundleModPatcher
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using DieselBundle;

    using Ionic.Zip;

    using Newtonsoft.Json;
    using System.Windows.Forms;


    /// <summary>
    /// The bundle rewrite script.
    /// </summary>
    public class BundleRewriteScriptAction
    {
        #region Fields

        /// <summary>
        /// Action to perform.
        /// </summary>
        public string action; //Find, Replace, MovePos, Write, Insert, Read, Goto, Writepos

        /// <summary>
        /// Type of action.
        /// </summary>
        public string type; //data, position

        /// <summary>
        /// Array of bytes to look for.
        /// </summary>
        public byte[] find;

        /// <summary>
        /// Array of bytes to write.
        /// </summary>
        public byte[] write;

        /// <summary>
        /// Position at which the array of bytes was found.
        /// </summary>
        public int position;

        /// <summary>
        /// Label to get.
        /// </summary>
        public int getLabel;

        /// <summary>
        /// Label to create.
        /// </summary>
        public int setLabel;

        /// <summary>
        /// Number of bytes to read.
        /// </summary>
        public int readBytes;

        /// <summary>
        /// Line number as it appears in the script file.
        /// </summary>
        public int lineNum;

        /// <summary>
        /// Script filename.
        /// </summary>
        public String fileName;


        #endregion
    }


    /// <summary>
    /// Script label.
    /// </summary>
    public class BundleRewriteScriptLabel
    {
        #region Fields

        /// <summary>
        /// Recorded position.
        /// </summary>
        public int position;

        /// <summary>
        /// Recorded data.
        /// </summary>
        public byte[] data;

        #endregion
    }


    /// <summary>
    /// The bundle rewrite item.
    /// </summary>
    public class BundleRewriteItem
    {
        #region Enums
        /*
        public enum ReplacementType
        {
            FileReplacement = "File"
        };
        */
        #endregion

        #region Fields

        /// <summary>
        /// The bundle extension.
        /// </summary>
        public ulong BundleExtension;

        /// <summary>
        /// The bundle language.
        /// </summary>
        public uint BundleLanguage;

        /// <summary>
        /// The bundle path.
        /// </summary>
        public ulong BundlePath;

        /// <summary>
        /// The file replacement type
        /// </summary>
        public int ReplacementType;

        /// <summary>
        /// The mod name.
        /// </summary>
        [JsonIgnore]
        public string ModName;

        /// <summary>
        /// The mod author.
        /// </summary>
        [JsonIgnore]
        public string ModAuthor;

        /// <summary>
        /// The mod description
        /// </summary>
        [JsonIgnore]
        public string ModDescription;

        /// <summary>
        /// The ids.
        /// </summary>
        [JsonIgnore]
        public HashSet<uint> Ids;

        /// <summary>
        /// The is language specific.
        /// </summary>
        public bool IsLanguageSpecific;

        /// <summary>
        /// The Source file.
        /// </summary>
        [JsonIgnore]
        public string SourceFile { get; set; }

        /// <summary>
        /// The replacement file.
        /// </summary>
        public string ReplacementFile { get; set; }

        /// <summary>
        /// Does this replacement use variables?
        /// </summary>
        [JsonIgnore]
        public bool hasVariables { get; set; }

        /// <summary>
        /// The toRemove.
        /// </summary>
        [JsonIgnore]
        public bool toRemove;

        /// <summary>
        /// The toReinstall.
        /// </summary>
        [JsonIgnore]
        public bool toReinstall;

        /// <summary>
        /// The toShared.
        /// </summary>
        [JsonIgnore]
        public bool toShared;

        /// <summary>
        /// The priority of this entry.
        /// </summary>
        [JsonIgnore]
        public bool priority;

        public BundleRewriteItem()
        {
        }

        public BundleRewriteItem(ulong path, uint language, ulong ext)
        {
            this.BundlePath = path;
            this.BundleLanguage = language;
            if (language != 0)
                this.IsLanguageSpecific = true;
            this.BundleExtension = ext;
        }

        public static bool operator ==(BundleRewriteItem item1, BundleRewriteItem item2)
        {
            if (((object)item1 == null && (object)item2 != null) ||
                ((object)item1 != null && (object)item2 == null)
                )
                return false;
            else if ((object)item1 == null && (object)item2 == null)
                return true;

            if (item1.BundleExtension == item2.BundleExtension &&
               item1.BundleLanguage == item2.BundleLanguage &&
               item1.BundlePath == item2.BundlePath &&
               item1.IsLanguageSpecific == item2.IsLanguageSpecific)
                return true;
            return false;
        }

        public static bool operator !=(BundleRewriteItem item1, BundleRewriteItem item2)
        {
            if (item1 == item2)
                return false;
            return true;
        }

        public static bool operator ==(BundleRewriteItem item1, BundleEntryPath item2)
        {
            if (((object)item1 == null && (object)item2 != null) ||
                ((object)item1 != null && (object)item2 == null)
                )
                return false;
            else if ((object)item1 == null && (object)item2 == null)
                return true;

            if (item1.BundleExtension == item2.EntryExtension &&
               item1.BundleLanguage == item2.EntryLanguage &&
               item1.BundlePath == item2.EntryPath
                )
                return true;
            return false;
        }

        public static bool operator !=(BundleRewriteItem item1, BundleEntryPath item2)
        {
            if (item1 == item2)
                return false;
            return true;
        }

        public BundleEntryPath getBundleEntryPath()
        {
            BundleEntryPath bep = new BundleEntryPath();
            bep.EntryPath = this.BundlePath;
            bep.EntryLanguage = this.BundleLanguage;
            bep.EntryExtension = this.BundleExtension;
            return bep;
        }

        public string getEscapedName()
        {
            return Path.GetInvalidFileNameChars().Aggregate(ModName, (current, c) => current.Replace(c.ToString(), "_"));
        }

        public bool isOverrideable()
        {
            if (this.BundleExtension == 0x5368E150B05A5B8C ||
                this.BundleExtension == 0x9CBCB6A20DBE209 ||
                this.BundleExtension == 0x2C980556285CB378 ||
                this.BundleExtension == 0xAF612BBC207E00BD ||
                this.BundleExtension == 0x296F10982B9995AE ||
                this.BundleExtension == 0x3EB5063801844EA2 ||
                this.BundleExtension == 0xA16640869D0A18D0 ||
                this.BundleExtension == 0x171AAB267339E89C ||
                this.BundleExtension == 0xCBA8EA4305A2FCB6)
                return true;

            return false;
        }

        #endregion
    }

    /// <summary>
    /// The bundle rewrite entry id.
    /// </summary>
    public class BundleRewriteEntryID
    {
        #region Fields

        /// <summary>
        /// The EntryID.
        /// </summary>
        public uint EntryID;

        /// <summary>
        /// Is this entry to be restored?
        /// </summary>
        public bool toRestore = false;

        /// <summary>
        /// The priority BundleRewriteItem.
        /// </summary>
        public BundleRewriteItem priorityBundleRewriteItem = new BundleRewriteItem();

        /// <summary>
        /// The item queue. For other items.
        /// </summary>
        public List<BundleRewriteItem> BundleRewriteItem_queue = new List<BundleRewriteItem>();

        #endregion
    }

    /// <summary>
    /// The bundle rewriter.
    /// </summary>
    internal class BundleRewriter
    {
        #region Fields

        /// <summary>
        /// The bundle progress message.
        /// </summary>
        public string BundleProgressMessage;

        /// <summary>
        /// The bundle progress percentage.
        /// </summary>
        public int BundleProgressPercentage;

        /// <summary>
        /// The done.
        /// </summary>
        public bool Done = false;

        /// <summary>
        /// The error.
        /// </summary>
        public string Error;

        /// <summary>
        /// The progress mutex.
        /// </summary>
        public Mutex ProgressMutex = new Mutex();

        /// <summary>
        /// The total progress message.
        /// </summary>
        public string TotalProgressMessage;

        /// <summary>
        /// The total progress percentage.
        /// </summary>
        public int TotalProgressPercentage;

        /// <summary>
        /// The total elapsed time.
        /// </summary>
        public System.Diagnostics.Stopwatch TotalElapsedTime = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// The elapsed time to calculate speed.
        /// </summary>
        public System.Diagnostics.Stopwatch SpeedElapsedTime = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// The current entry count.
        /// </summary>
        public long CurrentEntryCount = 0L;

        /// <summary>
        /// The entry count to calculate speed.
        /// </summary>
        public long SpeedEntryCount = 0L;

        /// <summary>
        /// The total entry count.
        /// </summary>
        public long TotalEntryCount = 0L;

        /// <summary>
        /// The total bundles count.
        /// </summary>
        public int TotalBundlesCount = 0;

        /// <summary>
        /// The current bundles count.
        /// </summary>
        public int CurrentBundlesCount = 0;

        /// <summary>
        /// The currently processing bundle.
        /// </summary>
        public string CurrentBundle = "";

        /// <summary>
        /// The report of bundles check
        /// </summary>
        private Dictionary<String, String> _checkBundlesReport = new Dictionary<String, String>();
        public Dictionary<String, String> checkBundlesReport { get { return _checkBundlesReport; } }

        /// <summary>
        /// The _asset folder.
        /// </summary>
        private readonly string _assetFolder;

        /// <summary>
        /// The type of backing up bundles.
        /// </summary>
        private readonly int _backupType;

        /// <summary>
        /// The type of applying mods.
        /// </summary>
        private readonly bool _overrideFolder;

        /// <summary>
        /// The type of marking up installed mods.
        /// </summary>
        private readonly bool _overrideFolderDummies;

        /// <summary>
        /// Solve confliction issues of override folder.
        /// </summary>
        private readonly bool _overrideFolderShared;

        /// <summary>
        /// The _instMod.
        /// </summary>
        private readonly BundleMod _mod;

        /// <summary>
        /// The _bufferSize.
        /// </summary>
        public readonly int bufferSize;

        /// <summary>
        /// The _patch all bundles.
        /// </summary>
        private readonly bool _patchAllBundles;

        /// <summary>
        /// The is multiple mods.
        /// </summary>
        private readonly bool _multiMods;

        /// <summary>
        /// The _zip path.
        /// </summary>
        private readonly string _zipPath;

        /// <summary>
        /// The sorted _rewrite items.
        /// </summary>
        private Dictionary<uint, BundleRewriteEntryID> _rewriteItems = new Dictionary<uint, BundleRewriteEntryID>();

        /// <summary>
        /// The zip.
        /// </summary>
        private ZipFile Zip;

        /// <summary>
        /// The Backup Entry.
        /// </summary>
        public BackupEntry backupEntry = new BackupEntry();

        /// <summary>
        /// The Backup Entries.
        /// </summary>
        public HashSet<BackupEntry> backupEntries = new HashSet<BackupEntry>();

        /// <summary>
        /// Is allowed to backup.
        /// </summary>
        public bool allowBackup = false;


        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BundleRewriter"/> class.
        /// </summary>
        /// <param name="assetsPath">
        /// The assets path.
        /// </param>
        public BundleRewriter(string assetsPath)
        {
            this._assetFolder = assetsPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BundleRewriter"/> class.
        /// </summary>
        /// <param name="zipPath">
        /// The zip path.
        /// </param>
        /// <param name="mod">
        /// The mod.
        /// </param>
        /// <param name="assetsPath">
        /// The assets path.
        /// </param>
        /// <param name="includeAllBundles">
        /// The include all bundles.
        /// </param>
        /// </param>
        /// <param name="multiMods">
        /// The multiple mod involvement.
        /// </param>
        public BundleRewriter(string zipPath, BundleMod mod, int bufferSize, string assetsPath, int backupType, bool overridefolder, bool overridefolderdummies, bool overridefoldershared, bool includeAllBundles, bool multiMods)
        {
            this._zipPath = zipPath;
            this._mod = mod;
            this.bufferSize = bufferSize;
            this._assetFolder = assetsPath;
            this._backupType = backupType;
            this._overrideFolder = overridefolder;
            this._overrideFolderDummies = overridefolderdummies;
            this._overrideFolderShared = overridefolderdummies;
            this._patchAllBundles = includeAllBundles;
            this._multiMods = multiMods;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The apply mod.
        /// </summary>
        public void ApplyMod()
        {
            this.Done = false;
            using (this.Zip = new ZipFile(this._zipPath))
            {
                bool wasSuccessful = false;
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0xAB2664BF82E646UL);
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0xAB2664BF82E6460C);
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0x29104BC4AC649965);


                if (!this.LocateChangeIds())
                    return;

                HashSet<string> bundles = this.LocateBundlesToRewrite(out wasSuccessful);
                if (!wasSuccessful)
                    return;

                if (!this._patchAllBundles)
                    bundles.RemoveWhere(i => i.StartsWith("all_"));

                if (bundles.Count <= 0)
                {
                    this.Error = "Unable to locate any bundles with the requested items in them.\n"
                                 + "If the changed file(s) only exists within an all_x.bundle file you will need to enable the option to patch all_x.bundle files.\n"
                                 + "Refer to the patch description for if this is required.";
                    return;
                }

                this.SetTotalProgress("Backing up Bundles", -1);
                int currentBundle = 1;
                int bundleCount = bundles.Count;
                foreach (var bundle in bundles)
                {

                    this.SetBundleProgress(
                        string.Format("{0}/{1}", currentBundle, bundleCount),
                        (int)((currentBundle / (float)bundleCount) * 100.0f));

                    if (this._backupType == 1)
                        this.BackupEntries(this._mod, Path.Combine(this._assetFolder, bundle));
                    else
                    {
                        this.BackupFile(Path.Combine(this._assetFolder, bundle + "_h.bundle"));
                        this.BackupFile(Path.Combine(this._assetFolder, bundle + ".bundle"));
                    }
                    currentBundle += 1;

                }

                backupEntry.Name = this._mod.Name;
                backupEntry.Author = this._mod.Author;
                backupEntry.Description = this._mod.Description;
                backupEntry.BackupType = (this._backupType == 0) ? "Bundles" : "Bundle Entries";
                backupEntry.InstallDate = DateTime.Now;
                backupEntry.RewriteAll = this._patchAllBundles;
                HashSet<BundleRewriteItem> backupQueue = new HashSet<BundleRewriteItem>();
                foreach (BundleRewriteItem bri in this._mod.ItemQueue)
                {
                    BundleRewriteItem newbri = new BundleRewriteItem();
                    newbri.BundleExtension = bri.BundleExtension;
                    newbri.BundleLanguage = bri.BundleLanguage;
                    newbri.BundlePath = bri.BundlePath;
                    newbri.Ids = bri.Ids;
                    newbri.IsLanguageSpecific = bri.IsLanguageSpecific;

                    backupQueue.Add(newbri);
                }

                backupEntry.ItemQueue = backupQueue;
                allowBackup = true;


                currentBundle = 1;
                TotalElapsedTime.Start();
                foreach (var bundle in bundles)
                {
                    this.SetTotalProgress(
                        string.Format("Patching Bundle {0}/{1}", currentBundle, bundleCount),
                        (int)((currentBundle / (float)bundleCount) * 100.0f));
                    if (!this.PatchBundle(bundle))
                        return;

                    currentBundle += 1;
                }
                TotalElapsedTime.Stop();
            }

            this.SetTotalProgress("Done", 100);
            this.Done = true;
        }

        /// <summary>
        /// The apply mods.
        /// </summary>
        public void ApplyMods(BundleMod[] toAddModsList)
        {
            this.Done = false;
            using (this.Zip = new ZipFile(this._zipPath))
            {
                bool wasSuccessful = false;
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0xAB2664BF82E646UL);
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0xAB2664BF82E6460C);
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0x29104BC4AC649965);

                if (!this.LocateChangeIds())
                    return;

                HashSet<string> bundles = this.LocateBundlesToRewrite(out wasSuccessful);
                if (!wasSuccessful)
                    return;

                if (!this._patchAllBundles)
                    bundles.RemoveWhere(i => i.StartsWith("all_"));

                if (bundles.Count <= 0)
                {
                    this.Error = "Unable to locate any bundles with the requested items in them.\n"
                                 + "If the changed file(s) only exists within an all_x.bundle file you will need to enable the option to patch all_x.bundle files.\n"
                                 + "Refer to the patch description for if this is required.";
                    return;
                }



                this.SetTotalProgress("Backing up Bundles", -1);
                this.CurrentBundlesCount = 1;
                this.TotalBundlesCount = bundles.Count;
                foreach (var bundle in bundles)
                {

                    this.SetBundleProgress(
                        string.Format("Backing up Bundle {0}/{1}", CurrentBundlesCount, TotalBundlesCount),
                        (int)((CurrentBundlesCount / (float)TotalBundlesCount) * 100.0f));

                    if (this._backupType == 1)
                    {
                        this.BackupEntries(this._mod, Path.Combine(this._assetFolder, bundle));
                    }
                    else
                    {
                        this.BackupFile(Path.Combine(this._assetFolder, bundle + "_h.bundle"));
                        this.BackupFile(Path.Combine(this._assetFolder, bundle + ".bundle"));
                    }
                    CurrentBundlesCount += 1;

                }

                foreach (BundleMod bm in toAddModsList)
                {
                    BackupEntry multibackupEntry = new BackupEntry();
                    multibackupEntry.Name = bm.Name;
                    multibackupEntry.Author = bm.Author;
                    multibackupEntry.Description = bm.Description;
                    multibackupEntry.BackupType = (this._backupType == 0) ? "Bundles" : "Bundle Entries";
                    multibackupEntry.InstallDate = DateTime.Now;
                    multibackupEntry.RewriteAll = this._patchAllBundles;
                    HashSet<BundleRewriteItem> backupQueue = new HashSet<BundleRewriteItem>();

                    foreach (BundleRewriteItem bri in bm.ItemQueue)
                    {
                        BundleRewriteItem newbri = new BundleRewriteItem();
                        newbri.BundleExtension = bri.BundleExtension;
                        newbri.BundleLanguage = bri.BundleLanguage;
                        newbri.BundlePath = bri.BundlePath;
                        newbri.Ids = bri.Ids;
                        newbri.IsLanguageSpecific = bri.IsLanguageSpecific;
                        if (this._backupType == 0)
                            newbri.ReplacementFile = bri.ReplacementFile;
                        else
                            newbri.ReplacementFile = bri.BundlePath.ToString("x") + '.' + bri.BundleLanguage.ToString("x") + '.' + bri.BundleExtension.ToString("x");
                        newbri.SourceFile = bri.SourceFile;

                        backupQueue.Add(newbri);
                    }
                    multibackupEntry.ItemQueue = backupQueue;
                    backupEntries.Add(multibackupEntry);
                }
                allowBackup = true;


                CurrentBundlesCount = 1;
                TotalElapsedTime.Start();
                foreach (var bundle in bundles)
                {
                    this.SetTotalProgress(
                        string.Format("Patching Bundle {0}/{1}", CurrentBundlesCount, TotalBundlesCount),
                        (int)((CurrentBundlesCount / (float)TotalBundlesCount) * 100.0f));
                    if (!this.PatchBundle(bundle))
                        return;

                    CurrentBundlesCount += 1;
                }
                TotalElapsedTime.Stop();
            }
            this.SetTotalProgress("Done", 100);
            this.Done = true;
        }

        /// <summary>
        /// The apply changes.
        /// </summary>
        public void ApplyChanges(BundleMod[] toAddModsList, bool method)
        {
            this.Done = false;
            using (this.Zip = new ZipFile(this._zipPath))
            {
                bool wasSuccessful = false;
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0xAB2664BF82E646UL);
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0xAB2664BF82E6460C);
                this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0x29104BC4AC649965);

                if (!this.LocateChangeIds())
                    return;

                var toRemove = this._mod.ItemQueue.Where(p => p.toRemove);
                //var toReinstall = this._mod.ItemQueue.Where(p => p.toReinstall);
                var toInstall = this._mod.ItemQueue.Where(p => !p.toRemove);


                if (this._overrideFolder)
                {
                    this.SetTotalProgress("Utilizing Override Folder", -1);

                    HashSet<BundleEntryPath> toSharedFolder = new HashSet<BundleEntryPath>();
                    HashSet<BundleEntryPath> overrideApplied = new HashSet<BundleEntryPath>();
                    Dictionary<BundleEntryPath, HashSet<BundleRewriteItem>> bep_sorted = new Dictionary<BundleEntryPath, HashSet<BundleRewriteItem>>();

                    foreach (BundleRewriteItem bri in this._mod.ItemQueue)
                    {
                        if (bri.toRemove)
                            continue;

                        if (bep_sorted.ContainsKey(bri.getBundleEntryPath()))
                        {
                            if (this._overrideFolderShared && !toSharedFolder.Contains(bri.getBundleEntryPath()))
                                toSharedFolder.Add(bri.getBundleEntryPath());
                            bep_sorted[bri.getBundleEntryPath()].Add(bri);
                        }
                        else
                        {
                            HashSet<BundleRewriteItem> newbrihs = new HashSet<BundleRewriteItem>();
                            newbrihs.Add(bri);
                            bep_sorted.Add(bri.getBundleEntryPath(), newbrihs);
                        }
                    }


                    HashSet<BundleRewriteItem> reducedItemQueue = new HashSet<BundleRewriteItem>();
                    int override_count = 1;
                    foreach (BundleRewriteItem bri in this._mod.ItemQueue)
                    {
                        this.SetBundleProgress(
                            string.Format("Processing {0}/{1}", override_count, this._mod.ItemQueue.Count),
                            (int)((override_count / (float)this._mod.ItemQueue.Count) * 100.0f));

                        if (bri.isOverrideable()
                            //&& !bri.ReplacementFile.EndsWith(".script")
                            )
                        {

                            if (!string.IsNullOrEmpty(StaticStorage.Known_Index.GetPath(bri.BundlePath)) &&
                                !string.IsNullOrEmpty(StaticStorage.Known_Index.GetExtension(bri.BundleExtension))
                                )
                            {
                                string extrname = "";
                                extrname += StaticStorage.Known_Index.GetPath(bri.BundlePath);
                                extrname += "." + StaticStorage.Known_Index.GetExtension(bri.BundleExtension);

                                string modName = bri.getEscapedName();

                                if (bri.toRemove)
                                {
                                    if (!toSharedFolder.Contains(bri.getBundleEntryPath()) && File.Exists(Path.Combine(this._assetFolder, "mod_overrides", "Bundle_Modder_Shared", extrname)))
                                        File.Delete(Path.Combine(this._assetFolder, "mod_overrides", "Bundle_Modder_Shared", extrname));
                                    else if (File.Exists(Path.Combine(this._assetFolder, "mod_overrides", modName, extrname)))
                                        File.Delete(Path.Combine(this._assetFolder, "mod_overrides", modName, extrname));

                                    DeleteEmptyDirs(Path.Combine(this._assetFolder, "mod_overrides", modName));
                                }
                                else
                                {
                                    if (bri.toReinstall)
                                    {
                                        if (File.Exists(Path.Combine(this._assetFolder, "mod_overrides", modName, extrname)))
                                            File.Delete(Path.Combine(this._assetFolder, "mod_overrides", modName, extrname));
                                    }

                                    String path;
                                    if (this._overrideFolderShared && toSharedFolder.Contains(bri.getBundleEntryPath()))
                                        path = Path.Combine(this._assetFolder, "mod_overrides", "Bundle_Modder_Shared", extrname);
                                    else
                                        path = Path.Combine(this._assetFolder, "mod_overrides", modName, extrname);

                                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                                        Directory.CreateDirectory(Path.GetDirectoryName(path));

                                    HashSet<BundleRewriteItem> bep_items = new HashSet<BundleRewriteItem>();
                                    if (bep_sorted.ContainsKey(bri.getBundleEntryPath()))
                                        bep_items = bep_sorted[bri.getBundleEntryPath()];

                                    var outOverride = new FileStream(
                                                    path,
                                                    FileMode.Create,
                                                    FileAccess.ReadWrite);

                                    int runningcount = 0;
                                    foreach (var item in bep_items)
                                    {
                                        if (item.SourceFile != null)
                                        {
                                            if(this.Zip == null)
                                                this.Zip = new ZipFile(item.SourceFile);

                                            if (!this.Zip.Name.Equals(item.SourceFile))
                                            {
                                                this.Zip.Dispose();
                                                this.Zip = new ZipFile(item.SourceFile);
                                            }
                                        }
                                        else
                                            continue;


                                        ZipEntry zip_entry = this.Zip[item.ReplacementFile];
                                        if (zip_entry != null)
                                        {
                                            if (zip_entry.UsesEncryption)
                                            {
                                                zip_entry.Password = "0$45'5))66S2ixF51a<6}L2UK";
                                                //zip_entry.Encryption = EncryptionAlgorithm.WinZipAes256;
                                            }
                                            
                                            MemoryStream zipEntryData = new MemoryStream();

                                            if (item.ReplacementFile.EndsWith(".script"))
                                            {
                                                MemoryStream scriptData = new MemoryStream();
                                                zip_entry.Extract(scriptData);
                                                scriptData.Seek(0, SeekOrigin.Begin);
                                                MemoryStream bundleData = new MemoryStream();

                                                if (RetrieveFile(item, out bundleData))
                                                {
                                                    if (runningcount > 0)
                                                    {
                                                        bundleData.SetLength(0);
                                                        outOverride.Seek(0, SeekOrigin.Begin);
                                                        outOverride.CopyTo(bundleData);
                                                    }

                                                    StreamReader scriptStream = new StreamReader(scriptData);

                                                    List<BundleRewriteScriptAction> scriptActions = ParseScriptActions(ref scriptStream, zip_entry.FileName);
                                                    scriptData.Close();
                                                    scriptStream.Close();

                                                    //apply script functions here
                                                    bundleData.Seek(0, SeekOrigin.Begin);

                                                    List<byte> entryBytes = bundleData.ToArray().ToList();

                                                    outOverride.Seek(0, SeekOrigin.Begin);
                                                    ApplyScriptActions(ref entryBytes, ref scriptActions);

                                                    foreach (byte b in entryBytes)
                                                        outOverride.WriteByte(b);
                                                    
                                                }
                                                else
                                                {
                                                    reducedItemQueue.Add(item);
                                                }
                                            }
                                            else
                                            {
                                                outOverride.Position = 0L;
                                                zipEntryData.Position = 0L;
                                                zip_entry.Extract(zipEntryData);
                                                zipEntryData.Position = 0L;
                                                zipEntryData.CopyTo(outOverride, this.bufferSize);
                                                //foreach (byte b in zipEntryData.ToArray().ToList())
                                                //    outOverride.WriteByte(b);
                                            }

                                            outOverride.Flush();
                                            
                                        }
                                        runningcount++;
                                    }

                                    outOverride.Close();
                                }
                            }
                            else
                            {
                                reducedItemQueue.Add(bri);
                            }

                        }
                        else
                        {
                            if (this._overrideFolderDummies)
                            {
                                string modName = bri.ModName;
                                modName = Path.GetInvalidFileNameChars().Aggregate(modName, (current, c) => current.Replace(c.ToString(), "_"));

                                if (bri.toRemove)
                                {
                                    if (Directory.Exists(Path.Combine(this._assetFolder, "mod_overrides", modName)))
                                    {
                                        //Directory.Delete(Path.Combine(this._assetFolder, "mod_overrides", modName), true);
                                        DeleteDirectory(Path.Combine(this._assetFolder, "mod_overrides", modName));
                                    }
                                }
                                else
                                {
                                    if (!Directory.Exists(Path.Combine(this._assetFolder, "mod_overrides", modName)))
                                        Directory.CreateDirectory(Path.Combine(this._assetFolder, "mod_overrides", modName));
                                }
                            }

                            reducedItemQueue.Add(bri);
                        }
                        override_count++;
                    }

                    this.SetBundleProgress("Done", 100);
                    this._mod.ItemQueue = reducedItemQueue;
                }

                HashSet<string> bundles = this.LocateBundlesToRewrite(out wasSuccessful);
                if (!wasSuccessful)
                    return;

                if (!this._patchAllBundles)
                    bundles.RemoveWhere(i => i.StartsWith("all_"));

                if (bundles.Count <= 0 && !this._overrideFolder)
                {
                    this.Error = "Unable to locate any bundles with the requested items in them.\n"
                                 + "If the changed file(s) only exists within an all_x.bundle file you will need to enable the option to patch all_x.bundle files.\n"
                                 + "Refer to the patch description for if this is required.";
                    return;
                }


                this.SetTotalProgress("Backing up Bundles", -1);
                this.CurrentBundlesCount = 1;
                this.TotalBundlesCount = bundles.Count;
                foreach (var bundle in bundles)
                {

                    this.SetBundleProgress(
                        string.Format("Backing up Bundle {0}/{1}", this.CurrentBundlesCount, this.TotalBundlesCount),
                        (int)((this.CurrentBundlesCount / (float)this.TotalBundlesCount) * 100.0f));

                    if (this._backupType == 1)
                    {
                        this.BackupEntries(this._mod, Path.Combine(this._assetFolder, bundle));
                    }
                    else
                    {
                        this.BackupFile(Path.Combine(this._assetFolder, bundle + "_h.bundle"));
                        this.BackupFile(Path.Combine(this._assetFolder, bundle + ".bundle"));
                    }
                    this.CurrentBundlesCount += 1;

                }

                foreach (BundleMod bm in toAddModsList)
                {
                    BackupEntry multibackupEntry = new BackupEntry();
                    multibackupEntry.Name = bm.Name;
                    multibackupEntry.Author = bm.Author;
                    multibackupEntry.Description = bm.Description;
                    multibackupEntry.BackupType = (this._backupType == 0) ? "Bundles" : "Bundle Entries";
                    multibackupEntry.InstallDate = DateTime.Now;
                    multibackupEntry.RewriteAll = this._patchAllBundles;
                    HashSet<BundleRewriteItem> backupQueue = new HashSet<BundleRewriteItem>();

                    foreach (BundleRewriteItem bri in bm.ItemQueue)
                    {
                        BundleRewriteItem newbri = new BundleRewriteItem();
                        newbri.BundleExtension = bri.BundleExtension;
                        newbri.BundleLanguage = bri.BundleLanguage;
                        newbri.BundlePath = bri.BundlePath;
                        newbri.Ids = bri.Ids;
                        newbri.IsLanguageSpecific = bri.IsLanguageSpecific;
                        if (this._backupType == 0)
                            newbri.ReplacementFile = bri.ReplacementFile;
                        else
                            newbri.ReplacementFile = bri.BundlePath.ToString("x") + '.' + bri.BundleLanguage.ToString("x") + '.' + bri.BundleExtension.ToString("x");
                        newbri.SourceFile = bri.SourceFile;

                        backupQueue.Add(newbri);
                    }
                    multibackupEntry.ItemQueue = backupQueue;
                    backupEntries.Add(multibackupEntry);
                }
                allowBackup = true;


                foreach (var rewriteItem in this._mod.ItemQueue)
                {
                    foreach (var id in rewriteItem.Ids)
                    {
                        if (this._rewriteItems.ContainsKey(id))
                        {
                            if (rewriteItem.toRemove)
                                this._rewriteItems[id].toRestore = true;

                            if (rewriteItem.ReplacementFile.EndsWith(".script"))
                            {
                                this._rewriteItems[id].BundleRewriteItem_queue.Add(rewriteItem);
                            }
                            else
                            {
                                if (rewriteItem.priority)
                                {
                                    this._rewriteItems[id].priorityBundleRewriteItem = rewriteItem;
                                }
                                else
                                {
                                    //Don't add... it's not part of priority
                                    this._rewriteItems[id].BundleRewriteItem_queue.Add(rewriteItem);
                                }
                            }
                        }
                        else
                        {
                            BundleRewriteEntryID breid = new BundleRewriteEntryID();

                            if (rewriteItem.toRemove)
                                breid.toRestore = true;

                            if (rewriteItem.ReplacementFile.EndsWith(".script"))
                            {
                                breid.BundleRewriteItem_queue.Add(rewriteItem);
                            }
                            else
                            {
                                if (rewriteItem.priority)
                                {
                                    breid.priorityBundleRewriteItem = rewriteItem;
                                }
                                else
                                {
                                    //Don't add, it's not part of priority
                                    breid.BundleRewriteItem_queue.Add(rewriteItem);
                                }
                            }

                            this._rewriteItems.Add(id, breid);
                        }
                    }
                }


                this.CurrentBundlesCount = 1;
                TotalElapsedTime.Start();
                SpeedElapsedTime.Start();

                foreach (var bundle in bundles)
                {
                    CurrentBundle = bundle;
                    this.SetTotalProgress(
                        string.Format("Patching Bundle {0}/{1}", this.CurrentBundlesCount, this.TotalBundlesCount),
                        (int)((this.CurrentBundlesCount / (float)this.TotalBundlesCount) * 100.0f));
                    if (!this.PatchBundle(bundle))
                        return;

                    this.CurrentBundlesCount += 1;
                }
                TotalElapsedTime.Stop();
                SpeedElapsedTime.Stop();
            }
            this.Zip.Dispose();
            this.SetTotalProgress("Done", 100);
            this.Done = true;
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }


        /// <summary>
        /// The apply mods.
        /// </summary>
        public void RemoveMods(bool method)
        {
            this.Done = false;
            bool wasSuccessful = false;
            this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0xAB2664BF82E646UL);
            this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0xAB2664BF82E6460C);
            this._mod.ItemQueue.RemoveWhere(i => i.BundleExtension == 0x29104BC4AC649965);

            if (!this.LocateChangeIds())
                return;

            HashSet<string> bundles = this.LocateBundlesToRewrite(out wasSuccessful);
            if (!wasSuccessful)
                return;

            if (!this._patchAllBundles)
                bundles.RemoveWhere(i => i.StartsWith("all_"));

            if (bundles.Count <= 0)
            {
                this.Error = "Unable to locate any bundles with the requested items in them.\n"
                             + "If the changed file(s) only exists within an all_x.bundle file you will need to enable the option to patch all_x.bundle files.\n"
                             + "Refer to the patch description for if this is required.";
                return;
            }



            int currentBundle = 1;
            int bundleCount = bundles.Count;

            foreach (var bundle in bundles)
            {
                this.SetTotalProgress(
                    string.Format("Patching Bundle {0}/{1}", currentBundle, bundleCount),
                    (int)((currentBundle / (float)bundleCount) * 100.0f));
                if (!this.PatchBundleRestore(this._mod, bundle, method))
                    return;

                currentBundle += 1;
            }

            this.SetTotalProgress("Done", 100);
            this.Done = true;
        }


        #endregion

        #region Methods

        /// <summary>
        /// The backup file.
        /// </summary>
        /// <param name="bundlePath">
        /// The bundle path.
        /// </param>
        private void BackupFile(string bundlePath)
        {
            string backupFolder = Path.Combine(this._assetFolder, "asset_backups");
            Directory.CreateDirectory(backupFolder);
			string backupPath = Path.Combine(backupFolder, Path.GetFileName(bundlePath));
            if (!File.Exists(backupPath))
                File.Copy(bundlePath, backupPath);
        }

        /// <summary>
        /// The backup bundle entry.
        /// </summary>
        /// <param name="bundlePath">
        /// The bundle path.
        /// </param>
        private void BackupEntries(BundleMod mod, string bundlePath)
        {
            string backupFolder = Path.Combine(this._assetFolder, "asset_backups");
            Directory.CreateDirectory(backupFolder);

            var header = new BundleHeader();
            if (!header.Load(bundlePath))
            {
                this.Error = "Failed to parse bundle header: " + bundlePath;
            }

            var bundleFS = new FileStream(bundlePath + ".bundle", FileMode.Open, FileAccess.Read);
            var bundleBR = new BinaryReader(bundleFS);

            foreach (BundleEntry entry in header.Entries)
            {
                BundleRewriteItem rewriteEntryItem;
                BundleRewriteItem backupRewriteItem = new BundleRewriteItem();
                if (mod.ItemQueue.Any(rewriteItem => rewriteItem.Ids.Contains(entry.Id)))
                {
                    NameEntry ne = StaticStorage.Index.Id2Name(entry.Id);
                    rewriteEntryItem = mod.ItemQueue.First(rewriteItem => rewriteItem.Ids.Contains(entry.Id));

					if (!File.Exists(Path.Combine(backupFolder, ne.ToString())))
                    {
						using (FileStream exportFS = new FileStream(Path.Combine(backupFolder, ne.ToString()), FileMode.CreateNew, FileAccess.Write))
                        {
                            using (BinaryWriter exportBW = new BinaryWriter(exportFS))
                            {
                                long entryLength = entry.Length == -1 ? bundleFS.Length - bundleFS.Position : entry.Length;
                                bundleFS.Seek(entry.Address, SeekOrigin.Begin);
                                exportBW.Write(bundleBR.ReadBytes((int)entryLength));
                            }
                        }
                    }
                }
            }

            bundleBR.Close();
            bundleFS.Close();


        }

        /// <summary>
        /// The locate bundles to rewrite.
        /// </summary>
        /// <param name="success">
        /// The success.
        /// </param>
        /// <returns>
        /// The <see cref="HashSet"/>.
        /// </returns>
        private HashSet<string> LocateBundlesToRewrite(out bool success)
        {
            success = false;
            var bundles = new HashSet<string>();
            this.SetTotalProgress("Determining Bundles To Modify", -1);
            List<string> files = Directory.EnumerateFiles(this._assetFolder, "*_h.bundle").ToList();

            for (int i = 0; i < files.Count; i++ )
            {
                string file = files[i];
                this.SetBundleProgress(
                        string.Format("Reviewing Bundle {0}/{1}", i, files.Count),
                        (int)((i / (float)files.Count) * 100.0f));

                string bundlePath = file.Replace("_h.bundle", string.Empty);
                var header = new BundleHeader();
                if (!header.Load(bundlePath))
                {
                    this.Error = "Failed to parse bundle header: " + bundlePath;
                    return bundles;
                }

                foreach (BundleEntry entry in header.Entries)
                {
                    bool found = false;
                    if (this._mod.ItemQueue.Any(rewriteItem => rewriteItem.Ids.Contains(entry.Id)))
                    {
                        bundles.Add(Path.GetFileNameWithoutExtension(bundlePath));
                        this.TotalEntryCount += header.Entries.Count;
                        found = true;
                    }

                    if (found)
                    {
                        break;
                    }
                }
            }

            this.SetBundleProgress("Done", 100);
            success = true;
            return bundles;
        }

        /// <summary>
        /// The locate bundles.
        /// </summary>
        /// <param name="success">
        /// The success.
        /// </param>
        /// <returns>
        /// The <see cref="HashSet"/>.
        /// </returns>
        public Dictionary<string, List<uint>> LocateBundles(out bool success, List<uint> ids)
        {
            success = false;
            var bundles = new Dictionary<string, List<uint>>();
            var files = Directory.EnumerateFiles(this._assetFolder, "*_h.bundle").ToList();

            for (int i = 0; i < files.Count; i++ )
            {
                string file = files[i];
                this.SetBundleProgress(
                        string.Format("Reviewing Bundle {0}/{1}", i, files.Count),
                        (int)((i / (float)files.Count) * 100.0f));

                List<uint> enties = new List<uint>();
                string bundlePath = file.Replace("_h.bundle", string.Empty);
                var header = new BundleHeader();
                if (!header.Load(bundlePath))
                {
                    this.Error = "Failed to parse bundle header: " + bundlePath;
                    return bundles;
                }

                foreach (BundleEntry entry in header.Entries)
                {
                    if (ids.Contains(entry.Id))
                    {
                        enties.Add(entry.Id);
                    }
                }

                if (enties.Count > 0)
                    bundles.Add(Path.GetFileNameWithoutExtension(bundlePath), enties);

            }

            this.SetBundleProgress("Done", 100);
            success = true;
            return bundles;
        }

        /// <summary>
        /// The locate change ids.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool LocateChangeIds()
        {
            this.SetTotalProgress("Determining File Ids", -1);

            for (int i = 0; i < this._mod.ItemQueue.Count; i++ )
            {
                BundleRewriteItem item = this._mod.ItemQueue.ElementAt(i);
                    this.SetBundleProgress(
                            string.Format("Reviewing Bundle {0}/{1}", i, this._mod.ItemQueue.Count),
                            (int)((i / (float)this._mod.ItemQueue.Count) * 100.0f));

                    item.Ids = StaticStorage.Index.Entry2Id(
                        item.BundlePath,
                        item.BundleExtension,
                        item.BundleLanguage,
                        item.IsLanguageSpecific);
                    if (item.Ids.Count <= 0)
                    {
                        this.Error = "Failed to find one or more items to replace in the global file index.";
                        return false;
                    }
                }

            return true;
        }

        /// <summary>
        /// The patch bundle.
        /// </summary>
        /// <param name="bundleId">
        /// The bundle id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool PatchBundle(string bundleId)
        {
            System.Diagnostics.Stopwatch st_total = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch st_entry = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch st_writing = new System.Diagnostics.Stopwatch();


            bool isAll = bundleId.Contains("all_");
            var inHeader = new BundleHeader();
            if (!inHeader.Load(Path.Combine(this._assetFolder, bundleId)))
            {
                return false;
            }

            var outHeader = new BundleHeader();
            outHeader.Footer = inHeader.Footer;
            outHeader.Header = inHeader.Header;
            outHeader.HasLengthField = inHeader.HasLengthField;
            var inBundle = new FileStream(Path.Combine(this._assetFolder, bundleId + ".bundle"), FileMode.Open, FileAccess.Read);
            var outBundle = new FileStream(
                Path.Combine(this._assetFolder, bundleId + ".bundle.new"),
                FileMode.Create,
                FileAccess.ReadWrite);
            //BufferedStream bsin = new BufferedStream(inBundle);
            //BufferedStream bsout = new BufferedStream(outBundle);
            var br = new BinaryReader(inBundle);
            var bw = new BinaryWriter(outBundle);
            long inFileLength = inBundle.Length;
            int currentAddress = 0;
            int currentEntry = 1;
            int entryCount = inHeader.Entries.Count;

            //stream buffers
            //int bufferSize = 4096; //1024^2
            byte[] buffer = new byte[this.bufferSize];

            if (isAll)
                inHeader.SortEntriesAddress();

            foreach (BundleEntry entry in inHeader.Entries)
            {
                st_total.Restart();
                st_entry.Restart();

                if (currentEntry % 100 == 0)
                {
                    this.SetBundleProgress(
                        string.Format("Writing entry {0}/{1}", currentEntry, entryCount),
                        (int)((currentEntry / (float)entryCount) * 100.0f));
                }

                var newEntry = new BundleEntry();
                newEntry.Id = entry.Id;
                newEntry.Length = entry.Length;
                newEntry.Address = (uint)currentAddress;
                bool replaced = false;
                bool firstpatched = false;
                bool restore = false;


                if (this._rewriteItems.ContainsKey(entry.Id))
                {
                    if (this._rewriteItems[entry.Id].toRestore)
                        restore = true;

                    MemoryStream newData = new MemoryStream();

                    if (restore)
                    {
                        if (this._backupType == 0)
                        {
                            var inRestoreBundle = new FileStream(Path.Combine(this._assetFolder, "asset_backups", bundleId + ".bundle"), FileMode.Open, FileAccess.Read);
                            var inRestoreHeader = new BundleHeader();
                            if (!inRestoreHeader.Load(Path.Combine(this._assetFolder, "asset_backups", bundleId)))
                                return false;

                            foreach (BundleEntry restoreEntry in inRestoreHeader.Entries)
                            {
                                if (restoreEntry.Id == entry.Id)
                                {
                                    newEntry.Length = this.RestoreEntry(restoreEntry, 0, inRestoreBundle, newData);
                                    replaced = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            NameEntry ne = StaticStorage.Index.Id2Name(entry.Id);
                            if (File.Exists(Path.Combine(this._assetFolder, "asset_backups", ne.ToString())))
                            {
                                using (FileStream importFS = new FileStream(Path.Combine(this._assetFolder, "asset_backups", ne.ToString()), FileMode.Open, FileAccess.Read))
                                {
                                    using (BinaryReader importBR = new BinaryReader(importFS))
                                    {
                                        newEntry.Length = (int)importFS.Length;

                                        newData.Seek(0, SeekOrigin.Begin);
                                        newData.Write(importBR.ReadBytes((int)importFS.Length), 0, (int)importFS.Length);
                                        replaced = true;
                                    }
                                }

                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    if (this._rewriteItems[entry.Id].priorityBundleRewriteItem != new BundleRewriteItem())
                    {
                        newEntry.Length = this.WriteZipEntry(entry, inBundle, newData, this._rewriteItems[entry.Id].priorityBundleRewriteItem);
                        firstpatched = true;
                        replaced = true;
                    }

                    foreach (var rewriteItem in this._rewriteItems[entry.Id].BundleRewriteItem_queue)
                    {
                        if (rewriteItem.toRemove)
                            continue;

                        if (!firstpatched)
                        {
                            newEntry.Length = this.WriteZipEntry(entry, inBundle, newData, rewriteItem);
                            firstpatched = true;
                        }
                        else
                        {
                            newEntry.Length = this.WriteZipEntry(newEntry, newData, newData, rewriteItem);
                        }

                        replaced = true;
                    }

                    if (newData.Length > 0L)
                    {

                        //buffer = new byte[megabyte];
                        newData.Position = 0L;
                        int bytesRead = newData.Read(buffer, 0, this.bufferSize);
                        while (bytesRead > 0)
                        {
                            outBundle.Write(buffer, 0, bytesRead);
                            bytesRead = newData.Read(buffer, 0, this.bufferSize);
                        }

                        //outBundle.Seek(currentAddress, SeekOrigin.Begin);
                        //newData.CopyTo(outBundle);
                    }

                }

                st_entry.Stop();
                st_writing.Restart();

                if (replaced)
                    currentAddress += newEntry.Length;

                outHeader.Entries.Add(newEntry);

                if (!replaced)
                {
                    /*
                        inBundle.Seek(entry.Address, SeekOrigin.Begin);
                        long entryLength = entry.Length == -1 ? inBundle.Length - inBundle.Position : entry.Length;
                        var br = new BinaryReader(inBundle);
                        var bw = new BinaryWriter(outBundle);
                        bw.Write(br.ReadBytes((int)entryLength));
                        currentAddress += (int)entryLength;
                     */

                    //inBundle.Seek(entry.Address, SeekOrigin.Begin);
                    inBundle.Seek((long)entry.Address, SeekOrigin.Begin);
                    long entryLength = (entry.Length == -1 ? inFileLength - (long)entry.Address : (long)entry.Length);
                    int remaining = (int)entryLength;
                    int totalread = 0;
                    //buffer = new byte[megabyte];
                    //bw.Write(br.ReadBytes((int)entryLength));

                    int bytesRead = inBundle.Read(buffer, 0, (((remaining - totalread) / this.bufferSize) > 0 ? this.bufferSize : remaining - totalread));
                    totalread += bytesRead;
                    while (bytesRead > 0 && totalread <= remaining)
                    {
                        bw.Write(buffer, 0, bytesRead);
                        bytesRead = inBundle.Read(buffer, 0, (((remaining - totalread) / this.bufferSize) > 0 ? this.bufferSize : remaining - totalread));
                        totalread += bytesRead;
                    }
                    //bw.Flush();

                    currentAddress += (int)entryLength;
                }

                currentEntry += 1;
                CurrentEntryCount++;
                SpeedEntryCount++;

                st_writing.Stop();
                st_total.Stop();
                if (st_total.ElapsedMilliseconds > 200)
                    Console.WriteLine(bundleId + " - T: " + st_total.ElapsedMilliseconds + " ms. [entry: " + st_entry.ElapsedMilliseconds + " ms. writing: " + st_writing.ElapsedMilliseconds + " ms.] with " + newEntry.Length + " for replaced " + replaced);
            }


            bw.Flush();
            inBundle.Close();
            outBundle.Close();

            if (isAll)
                outHeader.SortEntriesId();
            var outHeaderStream = new FileStream(
                Path.Combine(this._assetFolder, bundleId + "_h.bundle"),
                FileMode.OpenOrCreate,
                FileAccess.Write);
            var outHeaderBr = new BinaryWriter(outHeaderStream);
            outHeader.WriteHeader(outHeaderBr);
            foreach (BundleEntry entry in outHeader.Entries)
            {
                entry.WriteEntry(outHeaderBr, outHeader.HasLengthField);
            }

            outHeader.WriteFooter(outHeaderBr);
            outHeaderBr.Close();
            outHeaderStream.Close();
            File.Delete(Path.Combine(this._assetFolder, bundleId + ".bundle"));
            File.Move(Path.Combine(this._assetFolder, bundleId + ".bundle.new"), Path.Combine(this._assetFolder, bundleId + ".bundle"));
            this.SetBundleProgress("Done", 100);
            return true;
        }


        /// <summary>
        /// The retrieve file from bundle.
        /// </summary>
        /// <param name="bundleId">
        /// The bundle id.
        /// </param>
        /// <param name="data">
        /// The file data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool RetrieveFile(BundleRewriteItem item, out MemoryStream data)
        {
            data = new MemoryStream();

            if (item.Ids == null)
            {
                item.Ids = StaticStorage.Index.Entry2Id(
                        item.BundlePath,
                        item.BundleExtension,
                        item.BundleLanguage,
                        item.IsLanguageSpecific);
            }

            string bundleId = "";
            foreach (string file in Directory.EnumerateFiles(this._assetFolder, "*_h.bundle"))
            {
                string bundlePath = file.Replace("_h.bundle", string.Empty);
                var header = new BundleHeader();
                if (!header.Load(bundlePath))
                {
                    this.Error = "Failed to parse bundle header: " + bundlePath;
                    return false;
                }

                foreach (BundleEntry entry in header.Entries)
                {
                    if (item.Ids.Contains(entry.Id) && entry.Length > 0)
                    {
                        bundleId = Path.GetFileNameWithoutExtension(bundlePath);

                        var inBundle = new FileStream(Path.Combine(this._assetFolder, bundleId + ".bundle"), FileMode.Open, FileAccess.Read);
                        var inBundleBinary = new BinaryReader(inBundle);

                        inBundle.Seek(entry.Address, SeekOrigin.Begin);
                        long readLength = entry.Length == -1 ? inBundle.Length - inBundle.Position : entry.Length;
                        data.Write(inBundleBinary.ReadBytes((int)readLength), 0, (int)readLength);

                        data.Position = 0L;
                        inBundleBinary.Close();
                        inBundle.Close();

                        return true;
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// The retrieve file from bundle.
        /// </summary>
        /// <param name="bundleId">
        /// The bundle id.
        /// </param>
        /// <param name="data">
        /// The file data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool RetrieveFile(string bundleId, out MemoryStream data)
        {
            data = new MemoryStream();
            var inHeader = new BundleHeader();
            if (!inHeader.Load(Path.Combine(this._assetFolder, "asset_backups", bundleId)))
                return false;

            var inBundle = new FileStream(Path.Combine(this._assetFolder, bundleId + ".bundle"), FileMode.Open, FileAccess.Read);
            var inBundleBinary = new BinaryReader(inBundle);
            int currentEntry = 1;
            int entryCount = inHeader.Entries.Count;

            foreach (BundleEntry entry in inHeader.Entries)
            {
                if (currentEntry % 100 == 0)
                {
                    this.SetBundleProgress(
                        string.Format("Reading entry {0}/{1}", currentEntry, entryCount),
                        (int)((currentEntry / (float)entryCount) * 100.0f));
                }

                if (this._rewriteItems.ContainsKey(entry.Id))
                {
                    inBundle.Seek(entry.Address, SeekOrigin.Begin);
                    long readLength = entry.Length == -1 ? inBundle.Length - inBundle.Position : entry.Length;
                    data.Write(inBundleBinary.ReadBytes((int)readLength), 0, (int)readLength);
                    break;
                }
                currentEntry += 1;
            }

            inBundleBinary.Close();
            inBundle.Close();

            this.SetBundleProgress("Done", 100);
            return true;
        }


        /// <summary>
        /// The patch bundle restore.
        /// </summary>
        /// <param name="bundleId">
        /// The bundle id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool PatchBundleRestore(BundleMod mod, string bundleId, bool method)
        {

            if (method)
            {
                if (!File.Exists(Path.Combine(this._assetFolder, bundleId + ".bundle")) || !File.Exists(Path.Combine(this._assetFolder, bundleId + "_h.bundle")) || !File.Exists(Path.Combine(this._assetFolder, "asset_backups", bundleId + ".bundle")) || !File.Exists(Path.Combine(this._assetFolder, "asset_backups", bundleId + "_h.bundle")))
                    return false;

                File.Delete(Path.Combine(this._assetFolder, bundleId + ".bundle"));
                File.Delete(Path.Combine(this._assetFolder, bundleId + "_h.bundle"));
                File.Copy(Path.Combine(this._assetFolder, "asset_backups", bundleId + ".bundle"), Path.Combine(this._assetFolder, bundleId + ".bundle"), true);
                File.Copy(Path.Combine(this._assetFolder, "asset_backups", bundleId + "_h.bundle"), Path.Combine(this._assetFolder, bundleId + "_h.bundle"), true);
                this.SetBundleProgress("Done", 100);
                return true;
            }

            var inHeader = new BundleHeader();
            var inRestoreHeader = new BundleHeader();
            Dictionary<uint, BundleEntry> inRestoreDictionary = new Dictionary<uint, BundleEntry>();
            if (!inHeader.Load(Path.Combine(this._assetFolder, bundleId)))
            {
                return false;
            }

            if (!inRestoreHeader.Load(Path.Combine(this._assetFolder, "asset_backups", bundleId)))
                return false;

            foreach (BundleEntry entry in inRestoreHeader.Entries)
            {
                if (!inRestoreDictionary.ContainsKey(entry.Id))
                    inRestoreDictionary.Add(entry.Id, entry);
            }


            var outHeader = new BundleHeader();
            outHeader.Footer = inHeader.Footer;
            outHeader.Header = inHeader.Header;
            outHeader.HasLengthField = inHeader.HasLengthField;
            var inRestoreBundle = new FileStream(Path.Combine(this._assetFolder, "asset_backups", bundleId + ".bundle"), FileMode.Open, FileAccess.Read);
            var inBundle = new FileStream(Path.Combine(this._assetFolder, bundleId + ".bundle"), FileMode.Open, FileAccess.Read);
            var outBundle = new FileStream(
                Path.Combine(this._assetFolder, bundleId + ".bundle.new"),
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite);
            int currentAddress = 0;
            int currentEntry = 1;
            int entryCount = inHeader.Entries.Count;
            foreach (BundleEntry entry in inHeader.Entries)
            {
                if (currentEntry % 100 == 0)
                {
                    this.SetBundleProgress(
                        string.Format("Writing entry {0}/{1}", currentEntry, entryCount),
                        (int)((currentEntry / (float)entryCount) * 100.0f));
                }

                var newEntry = new BundleEntry();
                newEntry.Id = entry.Id;
                newEntry.Length = entry.Length;
                newEntry.Address = (uint)currentAddress;
                bool replaced = false;


                foreach (int length in from rewriteItem in mod.ItemQueue
                                       where rewriteItem.Ids.Contains(entry.Id)
                                       select this.RestoreEntry(inRestoreDictionary[entry.Id], (uint)currentAddress, inRestoreBundle, outBundle))
                {
                    currentAddress += length;
                    newEntry.Length = length;
                    replaced = true;
                    break;
                }

                outHeader.Entries.Add(newEntry);

                if (!replaced)
                {
                    inBundle.Seek(entry.Address, SeekOrigin.Begin);
                    long entryLength = entry.Length == -1 ? inBundle.Length - inBundle.Position : entry.Length;
                    var br = new BinaryReader(inBundle);
                    var bw = new BinaryWriter(outBundle);
                    bw.Write(br.ReadBytes((int)entryLength));
                    currentAddress += (int)entryLength;
                }

                currentEntry += 1;
            }

            inBundle.Close();
            inRestoreBundle.Close();
            outBundle.Close();
            var outHeaderStream = new FileStream(
                Path.Combine(this._assetFolder, bundleId + "_h.bundle"),
                FileMode.OpenOrCreate,
                FileAccess.Write);
            var outHeaderBr = new BinaryWriter(outHeaderStream);
            outHeader.WriteHeader(outHeaderBr);
            foreach (BundleEntry entry in outHeader.Entries)
            {
                entry.WriteEntry(outHeaderBr, outHeader.HasLengthField);
            }

            outHeader.WriteFooter(outHeaderBr);
            outHeaderBr.Close();
            outHeaderStream.Close();
            File.Delete(Path.Combine(this._assetFolder, bundleId + ".bundle"));
            File.Move(Path.Combine(this._assetFolder, bundleId + ".bundle.new"), Path.Combine(this._assetFolder, bundleId + ".bundle"));
            this.SetBundleProgress("Done", 100);
            return true;
        }


        /// <summary>
        /// The set bundle progress.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="percentage">
        /// The percentage.
        /// </param>
        private void SetBundleProgress(string message, int percentage)
        {
            this.ProgressMutex.WaitOne();
            this.BundleProgressMessage = "[" + message + "]";
            this.BundleProgressPercentage = percentage;
            this.ProgressMutex.ReleaseMutex();
        }

        /// <summary>
        /// The set total progress.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="percentage">
        /// The percentage.
        /// </param>
        private void SetTotalProgress(string message, int percentage)
        {
            this.ProgressMutex.WaitOne();
            this.TotalProgressMessage = message;
            this.TotalProgressPercentage = percentage;
            this.ProgressMutex.ReleaseMutex();
        }

        /// <summary>
        /// The write zip entry.
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <param name="rewriteItem">
        /// Replacement parameters in form of BundleRewriteItem
        /// </param>
        /// <returns>
        /// How many bytes were written to the stream.
        /// </returns>
        private int WriteZipEntry(BundleEntry bundleEntry, Stream input, Stream output, BundleRewriteItem rewriteItem)
        {
            if (rewriteItem.SourceFile != null)
            {
                if(this.Zip == null)
                    this.Zip = new ZipFile(rewriteItem.SourceFile);
                else if (!this.Zip.Name.Equals(rewriteItem.SourceFile))
                {
                    this.Zip.Dispose();
                    this.Zip = new ZipFile(rewriteItem.SourceFile);
                }
            }

            int extractedLength = 0;

            ZipEntry zip_entry = this.Zip[rewriteItem.ReplacementFile];
            if (zip_entry != null)
            {
                if (zip_entry.UsesEncryption)
                {
                    zip_entry.Password = "0$45'5))66S2ixF51a<6}L2UK";
                    //zip_entry.Encryption = EncryptionAlgorithm.WinZipAes256;
                }

                var br = new BinaryReader(input);
                var bw = new BinaryWriter(output);
                var data = new MemoryStream();
                zip_entry.Extract(data);
                data.Seek(0, SeekOrigin.Begin);

                if (zip_entry.FileName.EndsWith(".script"))
                {
                    StreamReader scriptStream = new StreamReader(data);

                    List<BundleRewriteScriptAction> scriptActions = ParseScriptActions(ref scriptStream, zip_entry.FileName);
                    data.Close();
                    scriptStream.Close();

                    //apply script functions here
                    if (input != output)
                        input.Seek(bundleEntry.Address, SeekOrigin.Begin);
                    else
                        input.Seek(0, SeekOrigin.Begin);
                    long entryLength = bundleEntry.Length == -1 ? input.Length - input.Position : bundleEntry.Length;


                    List<byte> entryBytes = br.ReadBytes((int)entryLength).ToList();

                    output.Seek(0, SeekOrigin.Begin);
                    if (!ApplyScriptActions(ref entryBytes, ref scriptActions))
                    {
                        bw.Write(br.ReadBytes((int)entryLength));
                        return (int)(entryLength);
                    }


                    bw.Write(entryBytes.ToArray());
                    return (int)entryBytes.ToArray().Length;
                }

                output.Seek(0, SeekOrigin.Begin);

                zip_entry.Extract(output);
                extractedLength = (int)zip_entry.UncompressedSize;

                data.Close();
            }

            return extractedLength;
        }


        /// <summary>
        /// The restore entry.
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <param name="rewriteItem">
        /// Replacement parameters in form of BundleRewriteItem
        /// </param>
        /// <returns>
        /// How many bytes were written to the stream.
        /// </returns>
        private int RestoreEntry(BundleEntry restoreBundleEntry, uint currentAddress, Stream input, Stream output)
        {

            var br = new BinaryReader(input);
            var bw = new BinaryWriter(output);

            input.Seek(restoreBundleEntry.Address, SeekOrigin.Begin);
            long entryLength = restoreBundleEntry.Length == -1 ? input.Length - input.Position : restoreBundleEntry.Length;

            output.Seek(currentAddress, SeekOrigin.Begin);
            bw.Write(br.ReadBytes((int)entryLength));

            return (int)entryLength;

        }


        #endregion

        /// <summary>
        /// Parse Script Actions
        /// </summary>
        /// <returns>
        /// The list of script actions to perform
        /// </returns>
        private List<BundleRewriteScriptAction> ParseScriptActions(ref StreamReader scriptStream, String scriptname)
        {
            List<BundleRewriteScriptAction> scriptActions = new List<BundleRewriteScriptAction>();
            String line;
            int lineNumber = 1;

            while ((line = scriptStream.ReadLine()) != null)
            {
                if (line.StartsWith(";") || line.Length <= 0)
                {
                    continue;
                }

                string[] lineParse = line.Split(new string[] { " : " }, StringSplitOptions.None);
                BundleRewriteScriptAction scriptAction = new BundleRewriteScriptAction();
                scriptAction.action = lineParse[0].ToLower();
                scriptAction.lineNum = lineNumber;
                scriptAction.fileName = "";

                switch (scriptAction.action)
                {
                    case "replace":
                        string[] replaceFind = lineParse[1].Split(' ');
                        byte[] replaceFindBytes = new byte[replaceFind.Length];
                        string[] replaceWrite = lineParse[2].Split(' ');
                        byte[] replaceWriteBytes = new byte[replaceWrite.Length];
                        for (int i = 0; i < replaceFind.Length; i++)
                        {
                            replaceFindBytes[i] = Convert.ToByte(replaceFind[i], 16);
                        }
                        scriptAction.find = replaceFindBytes;
                        for (int i = 0; i < replaceWrite.Length; i++)
                        {
                            replaceWriteBytes[i] = Convert.ToByte(replaceWrite[i], 16);
                        }
                        scriptAction.write = replaceWriteBytes;
                        break;
                    case "find":
                        if (lineParse.Length == 2)
                        {
                            string[] findFind = lineParse[1].Split(' ');
                            byte[] findBytes = new byte[findFind.Length];
                            for (int i = 0; i < findFind.Length; i++)
                            {
                                findBytes[i] = Convert.ToByte(findFind[i], 16);
                            }
                            scriptAction.find = findBytes;
                        }
                        else if (lineParse.Length == 3)
                        {
                            scriptAction.find = ScriptDataToBytes(lineParse[1], lineParse[2]);
                        }
                        break;
                    case "movepos":
                        scriptAction.position = Convert.ToInt32(lineParse[1]);
                        break;
                    case "write":
                        if (lineParse.Length == 2)
                        {
                            //Write bytes at current position
                            string[] writeSplit = lineParse[1].Split(' ');
                            byte[] writeWriteBytes = new byte[writeSplit.Length];
                            for (int i = 0; i < writeSplit.Length; i++)
                            {
                                writeWriteBytes[i] = Convert.ToByte(writeSplit[i], 16);
                            }
                            scriptAction.write = writeWriteBytes;


                        }
                        else if (lineParse.Length == 3)
                        {
                            //Write type of data at current position
                            if (lineParse[1].ToLower().Equals("data"))
                            {
                                scriptAction.type = "data";
                                scriptAction.getLabel = lineParse[2].GetHashCode();
                            }
                            else if (lineParse[1].ToLower().Equals("position"))
                            {
                                scriptAction.type = "position";
                                scriptAction.getLabel = lineParse[2].GetHashCode();
                            }
                            else
                            {
                                scriptAction.write = ScriptDataToBytes(lineParse[1], lineParse[2]);
                            }
                        }
                        break;
                    case "insert":
                        if (lineParse.Length == 2)
                        {
                            string[] insertSplitWrite = lineParse[1].Split(' ');
                            byte[] insertWrite = new byte[insertSplitWrite.Length];
                            for (int i = 0; i < insertSplitWrite.Length; i++)
                            {
                                insertWrite[i] = Convert.ToByte(insertSplitWrite[i], 16);
                            }
                            scriptAction.write = insertWrite;
                        }
                        else if (lineParse.Length == 3)
                        {
                            if (lineParse[1].ToLower().Equals("data"))
                            {
                                scriptAction.type = "data";
                                scriptAction.getLabel = lineParse[2].GetHashCode();
                            }
                            else if (lineParse[1].ToLower().Equals("position"))
                            {
                                scriptAction.type = "position";
                                scriptAction.getLabel = lineParse[2].GetHashCode();
                            }
                            else
                            {
                                scriptAction.write = ScriptDataToBytes(lineParse[1], lineParse[2]);
                            }
                        }
                        break;
                    case "goto":
                        if (lineParse.Length == 2)
                        {
                            if (lineParse[1].ToLower().Equals("end"))
                                scriptAction.position = -1;
                            else if (lineParse[1].ToLower().Equals("beginning"))
                                scriptAction.position = 0;
                            else
                                scriptAction.position = Convert.ToInt32(lineParse[1]);
                        }
                        else if (lineParse.Length == 3)
                        {
                            if (lineParse[1].ToLower().Equals("position"))
                            {
                                scriptAction.type = "position";
                                scriptAction.getLabel = lineParse[2].GetHashCode();
                            }
                            else
                            {
                                continue;
                            }
                        }
                        break;
                    case "savepos":
                        if (lineParse.Length == 2)
                        {
                            scriptAction.type = "position";
                            scriptAction.setLabel = lineParse[1].GetHashCode();
                        }
                        else
                        {
                            continue;
                        }
                        break;
                    case "read":
                        if (lineParse.Length == 3)
                        {
                            scriptAction.type = "data";
                            scriptAction.setLabel = lineParse[1].GetHashCode();
                            scriptAction.readBytes = Convert.ToInt32(lineParse[2]);
                        }
                        break;
                    case "delete":
                        if (lineParse.Length == 2)
                        {
                            scriptAction.type = lineParse[1].ToLower();
                        }
                        break;
                    default:
                        continue;
                }

                scriptActions.Add(scriptAction);
                lineNumber++;
            }

            return scriptActions;
        }


        /// <summary>
        /// Apply Script Actions
        /// </summary>
        /// <returns>
        /// Whether or not the process was successful.
        /// </returns>
        private bool ApplyScriptActions(ref List<byte> entryBytes, ref List<BundleRewriteScriptAction> scriptActions)
        {

            int currentPosition = 0;
            Dictionary<int, BundleRewriteScriptLabel> labels = new Dictionary<int, BundleRewriteScriptLabel>();


            foreach (BundleRewriteScriptAction scriptAction in scriptActions)
            {
                if (currentPosition > entryBytes.Count || currentPosition < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Attempt to perform actions outside array bounds. \nScript: " + scriptAction.fileName + " Action: " + scriptAction.action.ToLower() + " Line: " + scriptAction.lineNum, "Script Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }

                switch (scriptAction.action.ToLower())
                {
                    case "replace":
                        for (int i = 0; i < entryBytes.Count; i++)
                        {
                            if (i + scriptAction.find.Length > entryBytes.Count)
                                break;
                            if (entryBytes[i] == scriptAction.find[0])
                            {
                                bool found = false;
                                for (int j = 0; j < scriptAction.find.Length; j++)
                                {
                                    if (entryBytes[i + j] != scriptAction.find[j])
                                        break;
                                    if (j + 1 == scriptAction.find.Length)
                                        found = true;
                                }

                                if (found)
                                {
                                    scriptAction.position = i;
                                    for (int j = 0; j < scriptAction.find.Length; j++)
                                    {
                                        entryBytes.RemoveAt(i);
                                    }


                                    for (int j = 0; j < scriptAction.write.Length; j++)
                                    {
                                        if ((i + j) < entryBytes.Count)
                                        {
                                            entryBytes.Insert((i + j), scriptAction.write[j]);
                                        }
                                        else
                                        {
                                            entryBytes.Add(scriptAction.write[j]);
                                        }
                                    }
                                }
                            }

                        }
                        break;
                    case "find":
                        for (int i = 0; i < entryBytes.Count; i++)
                        {
                            if (i + scriptAction.find.Length > entryBytes.Count)
                                break;
                            if (entryBytes[i] == scriptAction.find[0])
                            {
                                bool found = false;
                                for (int j = 0; j < scriptAction.find.Length; j++)
                                {
                                    if (entryBytes[i + j] != scriptAction.find[j])
                                        break;
                                    if (j + 1 == scriptAction.find.Length)
                                        found = true;
                                }

                                if (found)
                                {
                                    scriptAction.position = i;
                                    currentPosition = i;
                                    break;
                                }
                            }

                        }
                        break;
                    case "movepos":
                        currentPosition += scriptAction.position;
                        break;
                    case "write":
                        scriptAction.position = currentPosition;
                        if (scriptAction.getLabel != 0)
                        {
                            if (labels.ContainsKey(scriptAction.getLabel))
                            {
                                if (scriptAction.type.Equals("data"))
                                    scriptAction.write = labels[scriptAction.getLabel].data;
                                else if (scriptAction.type.Equals("position"))
                                    scriptAction.write = BitConverter.GetBytes(labels[scriptAction.getLabel].position);
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Attempt to read an uninitialized label.\nScript: " + scriptAction.fileName + " Action: " + scriptAction.action.ToLower() + " Line: " + scriptAction.lineNum, "Script Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        for (int i = 0; i < scriptAction.write.Length; i++)
                        {
                            if (currentPosition > entryBytes.Count)
                                entryBytes.Add(scriptAction.write[i]);
                            else
                                entryBytes[currentPosition] = scriptAction.write[i];
                            currentPosition++;
                        }
                        break;
                    case "insert":
                        scriptAction.position = currentPosition;
                        if (scriptAction.getLabel != 0)
                        {
                            if (labels.ContainsKey(scriptAction.getLabel))
                            {
                                if (scriptAction.type.Equals("data"))
                                    scriptAction.write = labels[scriptAction.getLabel].data;
                                else if (scriptAction.type.Equals("position"))
                                    scriptAction.write = BitConverter.GetBytes(labels[scriptAction.getLabel].position);
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Attempt to read an uninitialized label.\nScript: " + scriptAction.fileName + " Action: " + scriptAction.action.ToLower() + " Line: " + scriptAction.lineNum, "Script Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        for (int i = 0; i < scriptAction.write.Length; i++)
                        {
                            if (currentPosition < entryBytes.Count)
                            {
                                entryBytes.Insert(currentPosition, scriptAction.write[i]);
                            }
                            else
                            {
                                entryBytes.Add(scriptAction.write[i]);
                            }
                            currentPosition++;
                        }
                        break;
                    case "goto":
                        if (scriptAction.getLabel != 0)
                        {
                            if (labels.ContainsKey(scriptAction.getLabel))
                                currentPosition = labels[scriptAction.getLabel].position;
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Attempt to read an uninitialized label.\nScript: " + scriptAction.fileName + " Action: " + scriptAction.action.ToLower() + " Line: " + scriptAction.lineNum, "Script Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        else
                        {
                            if (scriptAction.position == -1)
                                currentPosition = entryBytes.Count;
                            else
                                currentPosition = scriptAction.position;
                        }
                        break;
                    case "savepos":
                        BundleRewriteScriptLabel savePosNewLabel = new BundleRewriteScriptLabel();
                        savePosNewLabel.position = currentPosition;
                        if (labels.ContainsKey(scriptAction.setLabel))
                            labels[scriptAction.setLabel] = savePosNewLabel;
                        else
                            labels.Add(scriptAction.setLabel, savePosNewLabel);
                        break;
                    case "read":
                        List<byte> readBytes = new List<byte>();
                        for (int i = 0; i < scriptAction.readBytes; i++)
                        {
                            if ((currentPosition + i) < entryBytes.Count)
                            {
                                readBytes.Add(entryBytes[currentPosition + i]);
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Attempt to read past the end of file.\nScript: " + scriptAction.fileName + " Action: " + scriptAction.action.ToLower() + " Line: " + scriptAction.lineNum, "Script Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        BundleRewriteScriptLabel readNewLabel = new BundleRewriteScriptLabel();
                        readNewLabel.data = readBytes.ToArray();

                        if (labels.ContainsKey(scriptAction.setLabel))
                            labels[scriptAction.setLabel] = readNewLabel;
                        else
                            labels.Add(scriptAction.setLabel, readNewLabel);
                        break;
                    case "delete":
                        Int32 bytesToDelete = GetSize(scriptAction.type);
                        scriptAction.position = currentPosition;
                        if (bytesToDelete == -1)
                        {
                            while (entryBytes.Count > currentPosition && entryBytes[currentPosition] != 00)
                            {
                                entryBytes.RemoveAt(currentPosition);
                            }
                        }
                        else
                        {
                            if (currentPosition + bytesToDelete > entryBytes.Count)
                            {
                                System.Windows.Forms.MessageBox.Show("Attempt to delete past the end of file.\nScript: " + scriptAction.fileName + " Action: " + scriptAction.action.ToLower() + " Line: " + scriptAction.lineNum, "Script Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return false;
                            }
                            for (int x = 0; x < bytesToDelete; x++)
                            {
                                entryBytes.RemoveAt(currentPosition);
                            }
                        }

                        break;
                    default:
                        continue;
                }
            }

            return true;
        }


        /// <summary>
        /// Retrieve size of data type
        /// </summary>
        /// <returns>
        /// The size of data type.
        /// </returns>
        private Int32 GetSize(string type)
        {
            Int32 returnData;
            switch (type.ToLower())
            {
                case "text":
                case "string":
                    returnData = -1;
                    break;
                case "byte":
                case "ubyte":
                    returnData = sizeof(byte);
                    break;
                case "short":
                    returnData = sizeof(Int16);
                    break;
                case "ushort":
                    returnData = sizeof(UInt16);
                    break;
                case "int":
                    returnData = sizeof(Int32);
                    break;
                case "uint":
                    returnData = sizeof(UInt32);
                    break;
                case "int64":
                    returnData = sizeof(Int64);
                    break;
                case "uint64":
                    returnData = sizeof(UInt64);
                    break;
                case "float":
                    returnData = sizeof(float);
                    break;
                case "double":
                    returnData = sizeof(double);
                    break;
                default:
                    return Convert.ToInt32(type);
            }

            return returnData;
        }


        /// <summary>
        /// Data to proper byte array
        /// </summary>
        /// <returns>
        /// The byte array corresponding to data provided.
        /// </returns>
        private byte[] ScriptDataToBytes(string type, string data)
        {
            byte[] returnData;
            switch (type.ToLower())
            {
                case "hash":
                    returnData = BitConverter.GetBytes(DieselBundle.Utils.Hash64.HashString(data));
                    break;
                case "text":
                case "string":
                    data += '\0';
                    returnData = System.Text.Encoding.ASCII.GetBytes(data);
                    break;
                case "byte":
                case "ubyte":
                    returnData = BitConverter.GetBytes(Convert.ToByte(data));
                    break;
                case "short":
                    returnData = BitConverter.GetBytes(Convert.ToInt16(data));
                    break;
                case "ushort":
                    returnData = BitConverter.GetBytes(Convert.ToUInt16(data));
                    break;
                case "int":
                    returnData = BitConverter.GetBytes(Convert.ToInt32(data));
                    break;
                case "uint":
                    returnData = BitConverter.GetBytes(Convert.ToUInt32(data));
                    break;
                case "int64":
                    returnData = BitConverter.GetBytes(Convert.ToInt64(data));
                    break;
                case "uint64":
                    returnData = BitConverter.GetBytes(Convert.ToUInt64(data));
                    break;
                case "float":
                    returnData = BitConverter.GetBytes(Convert.ToSingle(data));
                    break;
                case "double":
                    returnData = BitConverter.GetBytes(Convert.ToDouble(data));
                    break;
                default:
                    return null;
            }

            return returnData;
        }

        /// <summary>
        /// Check all bundles for corruption
        /// </summary>
        public void CheckBundles()
        {

            IEnumerable<string> bundles = Directory.EnumerateFiles(this._assetFolder, "*_h.bundle");

            this.TotalBundlesCount = bundles.Count();
            this.CurrentBundlesCount = 0;

            foreach (var bundle in bundles)
            {
                String bundleId = bundle.Replace("_h.bundle", string.Empty);
                this.SetTotalProgress(
                    string.Format("{0}/{1} ({2})", this.CurrentBundlesCount, this.TotalBundlesCount, Path.GetFileName(bundleId)),
                    (int)((this.CurrentBundlesCount / (float)this.TotalBundlesCount) * 100.0f));

                try
                {
                    this.CheckBundle(bundleId);
                    this._checkBundlesReport.Add(bundleId, String.Empty);
                }
                catch (Exception e)
                {
                    //Do something here with the bundle
                    //MessageBox.Show("Corrupted bundle (" + bundleId + ")\n\n" + e.Message);
                    this._checkBundlesReport.Add(bundleId, e.Message);
                }

                this.CurrentBundlesCount++;
            }
            this.SetTotalProgress("Done", 100);
            this.Done = true;
        }

        private void CheckBundle(string bundleId)
        {
            var bundleSize = new FileInfo(bundleId + ".bundle").Length;

            //repair can be attempted, no guarantees
            //repair.. it's fucked.

            var bundle = new BundleHeader();
            if (!bundle.Load(bundleId))
            {
                throw new Exception("Corrupted BundleHeader");
            }

            //repair can be attempted, no guarantees
            //repair would consist of checking against a backup, given it exists

            foreach (BundleEntry be in bundle.Entries)
            {
                NameEntry ne = StaticStorage.Index.Id2Name(be.Id);
                if (ne == null)
                {
                    throw new Exception("Invalid NameEntry");
                }

                if (be.Address + be.Length > bundleSize)
                {
                    throw new Exception("Attempt to read past end of bundle");
                }
            }

            //can be repaired
            //repair would consist of resorting the entries, given they are all valid

            if (Path.GetFileNameWithoutExtension(bundleId).StartsWith("all_"))
            {
                //it's an all_ bundle... BundleHeader items should be sorted by Address (length 0 first)

                uint prevID = 0;

                foreach (BundleEntry entry in bundle.Entries)
                {
                    if (entry.Id < prevID)
                        throw new Exception("Invalid BundleEntry not sorted by ID");

                    prevID = entry.Id;
                }

            }
            else
            {
                //it's a regular bundle... BundleHeader items should be sorted by ID

                uint prevAddress = 0;

                foreach (BundleEntry entry in bundle.Entries)
                {
                    if (entry.Address < prevAddress)
                        throw new Exception("Invalid BundleEntry not sorted by Address");

                    prevAddress = entry.Address;
                }

            }
        }

        static void DeleteEmptyDirs(string dir)
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

        private Dictionary<string, string> RetrieveStrings(BundleRewriteItem filepath)
        {
            this.Done = false;

            this._mod.ItemQueue.Clear();
            this._mod.ItemQueue.Add(filepath);

            if (!this.LocateChangeIds())
                return null;

            bool wasSuccessful;
            HashSet<string> bundles = this.LocateBundlesToRewrite(out wasSuccessful);
            if (!wasSuccessful)
                return null;

            if (bundles.Count <= 0)
            {
                this.Error = "Unable to locate any bundles with the requested items in them.\n"
                                + "If the changed file(s) only exists within an all_x.bundle file you will need to enable the option to patch all_x.bundle files.\n"
                                + "Refer to the patch description for if this is required.";
                return null;
            }

            int currentBundle = 1;
            int bundleCount = bundles.Count;

            MemoryStream file_data = new MemoryStream();

            foreach (var bundle in bundles)
            {
                this.SetTotalProgress(
                    string.Format("Reading Bundle {0}/{1}", currentBundle, bundleCount),
                    (int)((currentBundle / (float)bundleCount) * 100.0f));
                if (!this.RetrieveFile(bundle, out file_data))
                    return null;

                if (file_data.Length > 0)
                    break;

                currentBundle += 1;
            }

            if (file_data.Length == 0)
                return null;

            //Extract strings here...
            Dictionary<string, string> return_dictionary = new Dictionary<string, string>();



            this.SetTotalProgress("Done", 100);
            this.Done = true;

            return return_dictionary;
        }
    }
}