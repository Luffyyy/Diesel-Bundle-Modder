// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleMod.cs" company="Zwagoth">
//   This code is released into the public domain by Zwagoth.
// </copyright>
// <summary>
//   The bundle mod.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PDBundleModPatcher
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    ///     The bundle mod.
    /// </summary>
    public class BundleMod
    {
        #region Enums

        //The mod type. 0 = Unknown, 1 = PDMod, 2 = Override folder, 3 = lua
        public enum ModType
        {
            Unknown,
            PDMod,
            mod_override,
            lua
        };

        //     The mod marked status.
        //     0 = Nothing, 1 = Marked for install (limegreen), 2 = Marked for removal (red), 3 = Marked for reinstallation (yellow), 4 = Marked for forced reinstallation (orange), 5 = Missing (Blue), 6 =
        public enum ModActionStatus
        {
            None,
            Install,
            Remove,
            Reinstall,
            ForcedReinstall,
            Missing
        };

        //     The mod status.
        //     0 = Not Installed, 1 = Installed, 2 = Partially installed, 3 = Unrecognized mod
        public enum ModStatus
        {
            NotInstalled,
            Installed,
            ParticallyInstalled,
            Unrecognized
        };

        #endregion

        #region Fields

        /// <summary>
        ///     The author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     The description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The item queue.
        /// </summary>
        public HashSet<BundleRewriteItem> ItemQueue = new HashSet<BundleRewriteItem>();

        /// <summary>
        ///     The object queue.
        /// </summary>
        public HashSet<object> ObjectQueue = new HashSet<object>();

        /// <summary>
        ///     The variables.
        /// </summary>
        public HashSet<ModVariable> Variables = new HashSet<ModVariable>();

        /// <summary>
        ///     The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The version compatibility.
        /// </summary>
        public double Version { get; set; }

        /// <summary>
        ///     The game compatibility.
        /// </summary>
        public string Game { get; set; }

        /// <summary>
        ///     The game compatibility.
        /// </summary>
        [JsonIgnore]
        public string file { get; set; }

        /// <summary>
        ///     Is this mod enabled?
        /// </summary>
        [JsonIgnore]
        public bool enabled { get; set; }

        //[Actions - START]//
        /// <summary>
        ///     Is this mod to be removed?
        /// </summary>
        [JsonIgnore]
        public bool toRemove { get; set; }
        //[Actions - END]//

        //[Permissions - START]//
        /// <summary>
        ///     Is this mod allowed to be installed?
        /// </summary>
        [JsonIgnore]
        public bool canInstall = true;

        /// <summary>
        ///     Is this mod allowed to be uninstalled?
        /// </summary>
        [JsonIgnore]
        public bool canUninstall = true;

        /// <summary>
        ///     Is this mod allowed to be removed from mods list?
        /// </summary>
        [JsonIgnore]
        public bool canRemove = true;
        //[Permissions - END]//

        /// <summary>
        ///     Does this mod utilize override folder.
        /// </summary>
        [JsonIgnore]
        public bool UtilizesOverride = false;

        /// <summary>
        ///     Does this mod require bundle patching.
        /// </summary>
        [JsonIgnore]
        public bool UtilizesBundles = false;

        /// <summary>
        ///     Does this mod include a patch script.
        /// </summary>
        [JsonIgnore]
        public bool IncludesPatchScript = false;

        /// <summary>
        ///     Does this mod include a patch script for override.
        /// </summary>
        [JsonIgnore]
        public bool IncludesPatchScriptWithinOverride = false;

        /// <summary>
        ///     The mod type.
        /// </summary>
        [JsonIgnore]
        public ModType type { get; set; }

        //[Status - START]//
        /// <summary>
        ///     The mod marked status.
        ///     0 = Nothing, 1 = Marked for install (limegreen), 2 = Marked for removal (red), 3 = Marked for reinstallation (yellow), 4 = Marked for forced reinstallation (orange), 5 = Missing (Blue), 6 =
        /// </summary>
        [JsonIgnore]
        public ModActionStatus actionStatus { get; set; }

        /// <summary>
        ///     The mod status.
        ///     0 = Not Installed, 1 = Installed, 2 = Partially installed, 3 = Unrecognized mod
        /// </summary>
        [JsonIgnore]
        public ModStatus status { get; set; }
        //[Status - END]//

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="inStream">
        /// The in_stream.
        /// </param>
        /// <returns>
        /// The <see cref="BundleMod"/>.
        /// </returns>
        public string getEscapedName()
        {
            return Path.GetInvalidFileNameChars().Aggregate(Name, (current, c) => current.Replace(c.ToString(), "_"));
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="inStream">
        /// The in_stream.
        /// </param>
        /// <returns>
        /// The <see cref="BundleMod"/>.
        /// </returns>
        public static BundleMod Deserialize(Stream inStream)
        {
            var reader = new StreamReader(inStream);
            return JsonConvert.DeserializeObject<BundleMod>(reader.ReadToEnd());
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="inString">
        /// The in_string.
        /// </param>
        /// <returns>
        /// The <see cref="BundleMod"/>.
        /// </returns>
        public static BundleMod Deserialize(string inString)
        {
            return JsonConvert.DeserializeObject<BundleMod>(inString);
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="mod">
        /// The mod.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Serialize(BundleMod mod)
        {
            return JsonConvert.SerializeObject(mod, Formatting.Indented);
        }

        public static bool operator==(BundleMod mod, BundleMod mod2)
        {
            if (
                    mod.Name.Equals(mod2.Name) &&
                    mod.Author.Equals(mod2.Author) &&
                    mod.Description.Equals(mod2.Description) &&
                    mod.ItemQueue.Count == mod2.ItemQueue.Count
                    )
            {
                BundleRewriteItem[] modentryQueue = mod.ItemQueue.ToArray();
                BundleRewriteItem[] mod2entryQueue = mod2.ItemQueue.ToArray();


                for (int i = 0; i < mod.ItemQueue.Count; i++)
                {
                    if (!(mod2entryQueue[i].BundlePath == modentryQueue[i].BundlePath) ||
                        !(mod2entryQueue[i].BundleLanguage == modentryQueue[i].BundleLanguage) ||
                        !(mod2entryQueue[i].BundleExtension == modentryQueue[i].BundleExtension) ||
                        !(mod2entryQueue[i].IsLanguageSpecific == modentryQueue[i].IsLanguageSpecific))
                    {
                        return false;
                    }
                }

                return true;

            }

            return false;
        }

        public static bool operator !=(BundleMod mod, BundleMod mod2)
        {
            if (mod == mod2)
                return false;
            else
                return true;
        }

        public static bool operator ==(BundleMod mod, BackupEntry backup)
        {
            if (
                    mod.Name.Equals(backup.Name) &&
                    mod.Author.Equals(backup.Author) &&
                    mod.Description.Equals(backup.Description) &&
                    mod.ItemQueue.Count == backup.ItemQueue.Count
                    )
            {
                BundleRewriteItem[] modentryQueue = mod.ItemQueue.ToArray();
                BundleRewriteItem[] backupentryQueue = backup.ItemQueue.ToArray();


                for (int i = 0; i < mod.ItemQueue.Count; i++)
                {
                    if (!(backupentryQueue[i].BundlePath == modentryQueue[i].BundlePath) ||
                        !(backupentryQueue[i].BundleLanguage == modentryQueue[i].BundleLanguage) ||
                        !(backupentryQueue[i].BundleExtension == modentryQueue[i].BundleExtension) ||
                        !(backupentryQueue[i].IsLanguageSpecific == modentryQueue[i].IsLanguageSpecific))
                    {
                        return false;
                    }
                }

                return true;

            }

            return false;
        }

        public static bool operator !=(BundleMod mod, BackupEntry backup)
        {
            if (mod == backup)
                return false;
            else
                return true;
        }

        public bool Equals(BundleMod guest)
        {
            if (ReferenceEquals(guest, null))
                return false;

            if (ReferenceEquals(this, guest))
                return true;

            return this == guest;
        }

        public override bool Equals(object guest)
        {
            if (ReferenceEquals(guest, null))
                return false;

            if (ReferenceEquals(this, guest))
                return true;

            if (guest.GetType() != this.GetType()) return false;

            return Equals((BundleMod)guest);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Author.GetHashCode();
            hash = (hash * 7) + this.Description.GetHashCode();
            hash = (hash * 7) + this.ItemQueue.Count.GetHashCode();

            return hash;
        }

        #endregion
    }
}