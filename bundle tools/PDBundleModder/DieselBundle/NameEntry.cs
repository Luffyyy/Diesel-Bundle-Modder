// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameEntry.cs" company="Zwagoth">
//   This code is released into the public domain by Zwagoth.
// </copyright>
// <summary>
//   The name entry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DieselBundle
{
    /// <summary>
    ///     The name entry.
    /// </summary>
    public class NameEntry
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets name entry extension hash.
        /// </summary>
        public ulong Extension { get; set; }

        /// <summary>
        ///     Gets or sets the name entry language ids.
        /// </summary>
        public uint Language { get; set; }

        /// <summary>
        ///     Gets or sets the name entry path hash.
        /// </summary>
        public ulong Path { get; set; }

        /// <summary>
        ///     Gets or sets the ID of the entry
        /// </summary>
        public uint ID { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Return a string representation of a NameEntry.
        /// </summary>
        /// <returns>
        ///     The string representation of a name entry.
        /// </returns>
        public override string ToString()
        {
            return this.Path.ToString("x") + '.' + this.Language.ToString("x") + '.' + this.Extension.ToString("x");
        }

        #endregion
    }
}