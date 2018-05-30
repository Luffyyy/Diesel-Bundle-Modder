namespace DieselBundle
{
    /// <summary>
    ///     The Language entry.
    /// </summary>
    public class LanguageEntry
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the language entry hash.
        /// </summary>
        public ulong Hash { get; set; }

        /// <summary>
        ///     Gets or sets the language entry Unknown value.
        /// </summary>
        public ulong Unknown { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Return a string representation of a LanguageEntry.
        /// </summary>
        /// <returns>
        ///     The string representation of a Language entry.
        /// </returns>
        public override string ToString()
        {
            return this.Hash.ToString("x") + '\t' + this.Unknown.ToString();
        }

        #endregion
    }
}