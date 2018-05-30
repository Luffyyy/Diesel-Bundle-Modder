// -----------------------------------------------------------------------
// <copyright file="BundleRewriteItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace pdmodscript
{
    using System.Collections.Generic;

    /// <summary>
    /// The bundle rewrite item.
    /// </summary>
    public class BundleRewriteItem
    {
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
        /// The is language specific.
        /// </summary>
        public bool IsLanguageSpecific;

        /// <summary>
        /// The replacement file.
        /// </summary>
        public string ReplacementFile;

        #endregion
    }
}
