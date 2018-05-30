namespace PDBundleModPatcher
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;


    public class BackupEntry
    {

        #region Fields

        /// <summary>
        ///     The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     The description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The rewrite all.
        /// </summary>
        public bool RewriteAll { get; set; }

        /// <summary>
        ///     The status.
        /// </summary>
        [JsonIgnore]
        public bool Corrupted { get; set; }

        /// <summary>
        ///     The replaced files.
        /// </summary>
        public HashSet<BundleRewriteItem> ItemQueue = new HashSet<BundleRewriteItem>();

        /// <summary>
        ///     The list of affected bundles.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, List<uint>> bundles = new Dictionary<string, List<uint>>();

        /// <summary>
        ///     The backup type.
        /// </summary>
        public string BackupType { get; set; }


        /// <summary>
        ///     The install date.
        /// </summary>
        public System.DateTime InstallDate { get; set; }

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
        public static BackupEntry Deserialize(Stream inStream)
        {
            var reader = new StreamReader(inStream);
            return JsonConvert.DeserializeObject<BackupEntry>(reader.ReadToEnd());
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
        public static BackupEntry Deserialize(string inString)
        {
            return JsonConvert.DeserializeObject<BackupEntry>(inString);
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
        public static string Serialize(BackupEntry entry)
        {
            return JsonConvert.SerializeObject(entry);
        }


        public static bool operator ==(BackupEntry host, BackupEntry entry2)
        {
            if (
                    host.Name.Equals(entry2.Name) &&
                    host.Author.Equals(entry2.Author) &&
                    host.Description.Equals(entry2.Description) &&
                    host.ItemQueue.Count == entry2.ItemQueue.Count
                    )
            {
                BundleRewriteItem[] modentryQueue = host.ItemQueue.ToArray();
                BundleRewriteItem[] backupentryQueue = entry2.ItemQueue.ToArray();


                for (int i = 0; i < entry2.ItemQueue.Count; i++)
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

        public static bool operator !=(BackupEntry host, BackupEntry entry2)
        {
            if (host == entry2)
                return false;
            else
                return true;
        }

        #endregion

    }
}
