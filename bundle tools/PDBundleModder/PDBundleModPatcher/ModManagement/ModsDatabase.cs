using DieselBundle.Utils;
using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDBundleModPatcher.ModManagement
{
    class ModsDatabase
    {

        private String modsDirectory = "";

        private String modsFile = "";

        private double version;

        /// <summary>
        ///     The local mods list.
        /// </summary>
        private Dictionary<string, BundleMod> _modsList = new Dictionary<string, BundleMod>();
        public Dictionary<string, BundleMod> modsList { get { return new Dictionary<string, BundleMod>(_modsList); } }

        /// <summary>
        ///     The missing file, installed mods list.
        /// </summary>
        private List<BackupEntry> _missingfile_installedModsList = new List<BackupEntry>();
        public List<BackupEntry> installedModsList_missing { get { return new List<BackupEntry>(_missingfile_installedModsList); } }

        /// <summary>
        ///     The installed mods list.
        /// </summary>
        private List<BackupEntry> _installedModsList = new List<BackupEntry>();
        public List<BackupEntry> installedModsList { get { return new List<BackupEntry>(_installedModsList); } }

        public ModsDatabase(String modsDir, String modsFile, double ver)
        {
            this.modsDirectory = modsDir;
            this.modsFile = modsFile;
            this.version = ver;
            this._modsList = new Dictionary<string, BundleMod>();
            this._missingfile_installedModsList = new List<BackupEntry>();
            this._installedModsList = new List<BackupEntry>();
        }

        public ModsDatabase()
        {
            this._modsList = new Dictionary<string, BundleMod>();
            this._missingfile_installedModsList = new List<BackupEntry>();
            this._installedModsList = new List<BackupEntry>();
        }
        

        /// <summary>
        ///     The load a single local mod.
        /// </summary>
        public bool LoadSingleMod(string path)
        {

            if (!Directory.Exists(modsDirectory))
            {
                return false;
            }

            if (!File.Exists(path))
            {
                path = Path.Combine(modsDirectory, Path.GetFileName(path));
                if (!File.Exists(path))
                    return false;
            }

            BundleMod localMod = new BundleMod();
            try
            {
                using (ZipFile zip = new ZipFile(path))
                {
                    MemoryStream ms = new MemoryStream();
                    ZipEntry pdmod_json = zip["pdmod.json"];

                    if (pdmod_json.UsesEncryption)
                    {
                        pdmod_json.Password = "0$45'5))66S2ixF51a<6}L2UK";
                        //pdmod_json.Encryption = EncryptionAlgorithm.WinZipAes256;
                    }

                    
                    pdmod_json.Extract(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    localMod = BundleMod.Deserialize(ms);

                    if (localMod.Version > this.version)
                        throw new Exception("This mod is not compatible with your Bundle Modder.\nPlease update your Bundle Modder to the latest version. (" + localMod.Version + ")");
                    if (localMod.Game != null && !localMod.Game.Equals(StaticStorage.settings.Game))
                        throw new Exception("This mod was built for " + localMod.Game + " game, which is not compatible with your game.");

                    if (localMod.ItemQueue != null)
                    {
                        foreach (BundleRewriteItem bri in localMod.ItemQueue)
                        {
                            bri.ModName = localMod.Name;
                            bri.ModAuthor = localMod.Author;
                            bri.ModDescription = localMod.Description;
                            bri.SourceFile = path;
                            if (bri.isOverrideable())
                            {
                                localMod.UtilizesOverride = true;
                                if (bri.ReplacementFile != null && bri.ReplacementFile.EndsWith(".script"))
                                    localMod.IncludesPatchScriptWithinOverride = true;
                            }
                            else
                                localMod.UtilizesBundles = true;

                            if (bri.ReplacementFile != null && bri.ReplacementFile.EndsWith(".script"))
                                localMod.IncludesPatchScript = true;
                        }
                    }
                    localMod.type = BundleMod.ModType.PDMod;
                    localMod.file = Path.GetFullPath(path);

                    AddModsList(path, localMod, true);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed to load mod - " + Path.GetFileName(path));
                StaticStorage.log.WriteLine(e.ToString());
            }

            return true;
        }

        public bool CanCreateDirectory(string Directory)
        {
            if (string.IsNullOrWhiteSpace(Directory))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     The load local mods.
        /// </summary>
        /// 
        public void LoadMods(bool overrride = false)
        {
            var watch = Stopwatch.StartNew();

            if (Directory.Exists(modsDirectory))
            {
                watch.Restart();

                if (overrride)
                    this._modsList.Clear();

                List<string> leftovers = this.modsList.Keys.ToList();

                string[] pdmods = Directory.GetFiles(modsDirectory, "*.pdmod");

                //System.Threading.Tasks.Parallel.ForEach(pdmods, file =>
                foreach (string file in pdmods)
                {
                    if (!File.Exists(file))
                        return;//continue;

                    if (LoadSingleMod(file))
                    {
                        leftovers.Remove(file);
                    }
                }//);

                watch.Stop();
                Console.WriteLine("LoadLocalMods.pdmods - " + watch.ElapsedMilliseconds + " ms");

                watch.Restart();
                foreach (string left in leftovers)
                    RemoveModsList(left);

                watch.Stop();
                Console.WriteLine("LoadLocalMods.pdmods.leftovers - " + watch.ElapsedMilliseconds + " ms");

            }
            else
            {
                if (this.CanCreateDirectory(modsDirectory))
                {
                    Directory.CreateDirectory(modsDirectory);
                }
            }

            watch.Restart();

            //load in override folder
            if (Directory.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides")))
            { 
                List<BundleMod> mod_overrides_mods = new List<BundleMod>();
                string[] mod_overrides = Directory.EnumerateDirectories(Path.Combine(StaticStorage.settings.AssetsFolder, "mod_overrides")).ToArray();

                foreach (string mo in mod_overrides)
                {
                    if (!Directory.Exists(mo))
                        continue;//continue;
                    if (new DirectoryInfo(mo).Name == "Bundle_Modder_Shared")
                        continue;//continue;

                    string[] allfiles = System.IO.Directory.GetFiles(mo, "*.*", System.IO.SearchOption.AllDirectories);
                    BundleMod mo_mod = new BundleMod();
                    mo_mod.Name = new DirectoryInfo(mo).Name;
                    mo_mod.Author = "<UNKNOWN>";
                    mo_mod.Description = "This mod is installed in \"mod_overrides\" folder. No description for this mod is availiable. This mod was not matched with any local mods. You can only uninstall this mod.";
                    mo_mod.file = mo;
                    mo_mod.status = BundleMod.ModStatus.Unrecognized;
                    mo_mod.type = BundleMod.ModType.mod_override;
                    mo_mod.actionStatus = BundleMod.ModActionStatus.Missing;
                    mo_mod.UtilizesOverride = true;

                    if (File.Exists(Path.Combine(mo, "mod.txt")))
                    {
                        try
                        {
                            OverrideMod overrideModInformation = OverrideMod.Deserialize(File.ReadAllText(Path.Combine(mo, "mod.txt")));

                            if(!String.IsNullOrWhiteSpace(overrideModInformation.Name))
                                mo_mod.Name = overrideModInformation.Name;
                            
                            if(!String.IsNullOrWhiteSpace(overrideModInformation.Author))
                                mo_mod.Author = overrideModInformation.Author;

                            if (!String.IsNullOrWhiteSpace(overrideModInformation.Description))
                                mo_mod.Description = overrideModInformation.Description;
                            
                            mo_mod.status = BundleMod.ModStatus.Installed;
                            mo_mod.actionStatus = BundleMod.ModActionStatus.None;
                        }
                        catch(Exception exc)
                        {
                        }
                    }

                    foreach (string mo_entry in allfiles)
                    {
                        if (mo_entry.EndsWith("mod.txt"))
                            continue;
                        
                        BundleRewriteItem mo_bri = new BundleRewriteItem();
                        string filepath = mo_entry.Substring(mo.Length + 1).Replace('\\', '/');
                        string[] pathelements = filepath.Split('.');
                        if (pathelements.Length > 3)
                            continue;

                        string entrypath = pathelements[0];
                        if (pathelements.Length == 2)
                        {
                            mo_bri.BundlePath = Hash64.HashString(pathelements[0]);
                            mo_bri.BundleExtension = Hash64.HashString(pathelements[1]);
                        }
                        else if (pathelements.Length == 3)
                        {
                            mo_bri.BundlePath = Hash64.HashString(pathelements[0]);
                            UInt32 lang = 0;
                            if (UInt32.TryParse(pathelements[1], out lang))
                                mo_bri.BundleLanguage = lang;
                            mo_bri.BundleExtension = Hash64.HashString(pathelements[2]);
                            mo_bri.IsLanguageSpecific = true;
                        }
                        else
                            continue;
                        mo_bri.ModName = mo_mod.Name;
                        mo_bri.ModAuthor = mo_mod.Author;
                        mo_bri.ModDescription = mo_mod.Description;
                        mo_bri.ReplacementFile = "";
                        if (mo_bri.isOverrideable()
                            //&& !bri.ReplacementFile.EndsWith(".script")
                            )
                        {

                            if (string.IsNullOrEmpty(StaticStorage.Known_Index.GetPath(mo_bri.BundlePath)) ||
                                string.IsNullOrEmpty(StaticStorage.Known_Index.GetExtension(mo_bri.BundleExtension))
                                )
                            {
                                continue;
                            }
                        }
                        mo_mod.ItemQueue.Add(mo_bri);
                    }
                    mod_overrides_mods.Add(mo_mod);
                }

                //check vs others
                Dictionary<string, BundleMod> temporarylocalModsList_master = this.modsList;

                foreach (BundleMod mo_bm in mod_overrides_mods)
                {
                    bool modMatch = false;

                    List<BundleMod> matched_mods = temporarylocalModsList_master.Values.Where(mod => mod.getEscapedName().Equals(mo_bm.Name) || mod.Name.Equals(mo_bm.Name)).ToList();

                    foreach (BundleMod bm in matched_mods)
                    {
                        modMatch = true;
                        if (mo_bm.ItemQueue.Count > bm.ItemQueue.Count) //the override mod contains too many files, not equal
                        {
                            mo_bm.canInstall = false;
                            mo_bm.canUninstall = true;
                            mo_bm.actionStatus = BundleMod.ModActionStatus.Missing;
                            mo_bm.status = BundleMod.ModStatus.Unrecognized;

                            AddModsList(mo_bm.file, mo_bm);
                        }
                        else
                        {
                            bool[] mo_checklist = new bool[mo_bm.ItemQueue.Count];

                            int checklist_i = 0;
                            bool mo_onlyfolder = !(bm.ItemQueue.Any(x => !x.isOverrideable())); //isOverradable
                            foreach (BundleRewriteItem mo_bri in mo_bm.ItemQueue)
                            {
                                if (bm.ItemQueue.Any(x => x.BundlePath == mo_bri.BundlePath && x.BundleExtension == mo_bri.BundleExtension))
                                    mo_checklist[checklist_i] = true;
                                checklist_i++;
                            }

                            bool mo_equal = !mo_checklist.Any(x => !x);
                            /*
                            for (checklist_i = 0; mo_equal && checklist_i < mo_bm.ItemQueue.Count; checklist_i++)
                                if (!mo_checklist[checklist_i])
                                    mo_equal = false;
                            */
                            if (!mo_equal)
                            {
                                mo_bm.canInstall = false;
                                mo_bm.canUninstall = true;
                                mo_bm.actionStatus = BundleMod.ModActionStatus.Missing;
                                mo_bm.status = BundleMod.ModStatus.Unrecognized;

                                AddModsList(mo_bm.file, mo_bm);
                            }
                            else
                            {
                                if (mo_onlyfolder || InstalledModsListContains(bm) > -1)
                                {
                                    bm.status = BundleMod.ModStatus.Installed;//installed
                                }
                                else
                                {
                                    bm.actionStatus = BundleMod.ModActionStatus.ForcedReinstall;
                                    bm.status = BundleMod.ModStatus.ParticallyInstalled;//installed

                                    foreach (BundleRewriteItem bri in bm.ItemQueue)
                                        bri.toReinstall = true;
                                }
                            }
                        }
                    }

                    if (!modMatch)
                    {
                        mo_bm.canInstall = false;
                        mo_bm.canUninstall = true;

                        AddModsList(mo_bm.file, mo_bm);
                    }

                }
            }

            watch.Stop();
            Console.WriteLine("LoadLocalMods.overrides - " + watch.ElapsedMilliseconds + " ms");

            watch.Restart();

            //BLT Mods
            if ( Directory.Exists( Path.Combine( StaticStorage.settings.AssetsFolder, "..", "mods") ) )
            {
                if( Directory.Exists( Path.Combine( StaticStorage.settings.AssetsFolder, "..", "mods", "base") ) )
                {
                    List<string> bltmods = Directory.EnumerateDirectories(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods")).ToList();

                    foreach (string bltmod in bltmods)
                    {
                        if (!Directory.Exists(bltmod))
                            continue;

                        if (Path.GetFileNameWithoutExtension(bltmod).Equals("log") || Path.GetFileNameWithoutExtension(bltmod).Equals("base"))
                            continue;

                        if (!File.Exists(Path.Combine(bltmod, "mod.txt")))
                            continue;


                        BundleMod blt_mod = new BundleMod();
                        blt_mod.Name = new DirectoryInfo(bltmod).Name;
                        blt_mod.Author = "<UNKNOWN>";
                        blt_mod.Description = "This is a BLT Hook mod. No description for this mod is availiable. This mod doesn't have a proper description. You can enable/disable this mod as well as uninstall it.";
                        blt_mod.file = bltmod;
                        blt_mod.status = BundleMod.ModStatus.Installed;
                        blt_mod.type = BundleMod.ModType.lua;
                        blt_mod.actionStatus = BundleMod.ModActionStatus.None;
                        blt_mod.UtilizesOverride = false;
                        blt_mod.UtilizesBundles = false;
                        blt_mod.enabled = true;

                        try
                        {
                            FileStream bltModfs = new FileStream(Path.Combine(bltmod, "mod.txt"), FileMode.Open);
                            using (StreamReader bltModsr = new StreamReader(bltModfs))
                            {
                                try
                                {
                                    //JsonConvert
                                    dynamic jsonDe = JsonConvert.DeserializeObject(bltModsr.ReadToEnd());
                                    //dynamic jsonDe = null;
                                    if (jsonDe != null)
                                    {
                                        if (jsonDe.name != null)
                                        {
                                            blt_mod.Name = jsonDe.name;
                                        }
                                        if (jsonDe.author != null)
                                        {
                                            blt_mod.Author = jsonDe.author;
                                        }
                                        if (jsonDe.description != null)
                                        {
                                            blt_mod.Description = jsonDe.description;
                                        }
                                    }
                                }
                                catch (Exception exc)
                                {
                                    blt_mod.Description += " Failed parsing mods.txt of " + Path.GetFileNameWithoutExtension(bltmod) + ", Message: " + exc.Message;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            blt_mod.Description += " Failed parsing mods.txt of " + Path.GetFileNameWithoutExtension(bltmod) + ", Message: " + e.Message;
                        }
                        AddModsList(bltmod, blt_mod, true);
                    }
                }

                LoadBLTModManagement();
            }

            watch.Stop();
            Console.WriteLine("LoadLocalMods.blt_mods - " + watch.ElapsedMilliseconds + " ms");
        }

        /// <summary>
        ///     The load mod backups.
        /// </summary>
        public void LoadModBackups()
        {
            if (File.Exists(this.modsFile))
            {
                using (Stream stream = File.Open(this.modsFile, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        this._installedModsList = JsonConvert.DeserializeObject<List<BackupEntry>>(reader.ReadToEnd());
                    }
                }
                if (this._installedModsList == null)
                    this._installedModsList = new List<BackupEntry>();
            }
        }

        /// <summary>
        ///     The save mod backups.
        /// </summary>
        public void SaveModBackups()
        {
            using (FileStream stream = File.Open(this.modsFile, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(JsonConvert.SerializeObject(this._installedModsList, Formatting.Indented));
                }
            }
        }


        /// <summary>
        ///     The load BLT mod management.
        /// </summary>
        public void LoadBLTModManagement()
        {
            //Read mods_status
            if (File.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods", "saves", "mod_manager.txt")))
            {
                Dictionary<string, bool> statuses = new Dictionary<string, bool>();

                FileStream bltMod_managerfs = new FileStream(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods", "saves", "mod_manager.txt"), FileMode.Open);
                using (StreamReader bltMod_managersr = new StreamReader(bltMod_managerfs))
                {
                    try
                    {
                        statuses = JsonConvert.DeserializeObject<Dictionary<string, bool>>(bltMod_managersr.ReadToEnd());
                    }
                    catch (Exception exc)
                    {
                        //blt_mod.Description += " Failed parsing mods.txt of " + Path.GetFileNameWithoutExtension(bltmod) + ", Message: " + exc.Message;
                        StaticStorage.log.WriteLine("ERROR: Failed parsing mod_manager.txt, message: " + exc.Message);
                    }
                }

                foreach (BundleMod bltMod in this._modsList.Values.Where(m => m.type != null && m.type == BundleMod.ModType.lua))
                {
                    if (statuses.ContainsKey("mods/" + Path.GetFileNameWithoutExtension(bltMod.file) + "/"))
                    {
                        bltMod.enabled = statuses["mods/" + Path.GetFileNameWithoutExtension(bltMod.file) + "/"];
                    }
                }
            }
        }

        /// <summary>
        ///     The save BLT mod management.
        /// </summary>
        public void SaveBLTModManagement()
        {
            Dictionary<string, bool> statuses = new Dictionary<string, bool>();

            foreach (BundleMod bltMod in this._modsList.Values.Where(m => m.type == BundleMod.ModType.lua))
            {
                statuses.Add("mods/" + Path.GetFileNameWithoutExtension(bltMod.file) + "/", bltMod.enabled);
            }

            using (FileStream stream = File.Open(Path.Combine(StaticStorage.settings.AssetsFolder, "..", "mods", "saves", "mod_manager.txt"), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(JsonConvert.SerializeObject(statuses, Formatting.Indented));
                }
            }
        }


        /// <summary>
        ///     The check installedModsList for mod.
        /// </summary>
        public int InstalledModsListContains(BundleMod mod)
        {

            for (int pos = 0; pos < this._installedModsList.Count; pos++)
            {
                if (mod == this._installedModsList[pos])
                {
                    if (_missingfile_installedModsList.Contains(this._installedModsList[pos]))
                        _missingfile_installedModsList.Remove(this._installedModsList[pos]);
                    return pos;
                }
            }

            return -1;
        }


        /// <summary>
        ///     Add a value to the modsList, replace if specified
        /// </summary>
        public bool AddModsList(string key, BundleMod value, bool replace = false)
        {
            if (_modsList.ContainsKey(key))
            {
                if (replace)
                    _modsList[key] = value;
                else
                    return false;
            }
            else
            {
                _modsList.Add(key, value);
            }
            return true;
        }

        /// <summary>
        ///     The remove local mod.
        /// </summary>
        public bool RemoveModsList(BundleMod mod)
        {
            if ((object)mod == null)
                return false;
            
            try
            {
                if (_modsList.ContainsKey(mod.file))
                    _modsList.Remove(mod.file);

                if (File.Exists(mod.file))
                    File.Delete(mod.file);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed to remove mod");
                return false;
            }
        }

        public bool RemoveModsList(string key, bool deleteFile = false)
        {
            if (_modsList.ContainsKey(key))
            {
                _modsList.Remove(key);

                if (deleteFile)
                {
                    try
                    {
                        if (File.Exists(key))
                            File.Delete(key);
                        return true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Failed to remove mod");
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddInstalledMod(BackupEntry entry)
        {
            this._installedModsList.Add(entry);
        }

        public void RemoveInstalledMod(BackupEntry entry)
        {
            foreach (BackupEntry listEntry in this._installedModsList)
            {
                if (entry == listEntry)
                {
                    this._installedModsList.Remove(listEntry);
                    break;
                }
            }
        }

        public void RemoveInstalledMod(BundleMod entry)
        {
            foreach (BackupEntry listEntry in this._installedModsList)
            {
                if (entry == listEntry)
                {
                    this._installedModsList.Remove(listEntry);
                    break;
                }
            }
        }

    }
}
