// -----------------------------------------------------------------------
// <copyright file="BundleMod.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace pdmodscript
{
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BundleMod
    {
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
        ///     The name.
        /// </summary>
        public string Name { get; set; }

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
            return JsonConvert.SerializeObject(mod);
        }
        #endregion
    }
}
