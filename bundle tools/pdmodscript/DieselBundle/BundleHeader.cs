// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleHeader.cs" company="Zwagoth">
//   This code is released into the public domain by Zwagoth.
// </copyright>
// <summary>
//   The bundle entry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DieselBundle
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///     The bundle header.
    /// </summary>
    public class BundleHeader
    {
        #region Fields

        /// <summary>
        ///     The has length field.
        /// </summary>
        public bool HasLengthField = false;

        /// <summary>
        ///     The header.
        /// </summary>
        public List<uint> Header = new List<uint>();

        /// <summary>
        /// The _entries.
        /// </summary>
        private readonly List<BundleEntry> entries = new List<BundleEntry>();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets list of bundle file entries
        /// </summary>
        public List<BundleEntry> Entries
        {
            get
            {
                return this.entries;
            }
        }

        /// <summary>
        ///     Gets or sets
        /// </summary>
        public byte[] Footer { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="bundleId">
        /// The bundle id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Load(string bundleId)
        {
            string headerFile = bundleId + "_h.bundle";
            if (!File.Exists(headerFile))
            {
                Console.WriteLine("Bundle header file does not exist.");
                return false;
            }

            try
            {
                var fs = new FileStream(headerFile, FileMode.Open);
                var br = new BinaryReader(fs);
                this.Header.Add(br.ReadUInt32());
                this.Header.Add(br.ReadUInt32());
                uint itemCount = br.ReadUInt32();
                this.Header.Add(itemCount);
                this.Header.Add(br.ReadUInt32());
                this.Header.Add(br.ReadUInt32());
                this.HasLengthField = this.Header[4] == 24;
                if (this.HasLengthField)
                {
                    this.Header.Add(br.ReadUInt32());
                    this.Header.Add(br.ReadUInt32());
                }

                for (int i = 0; i < itemCount; ++i)
                {
                    var be = new BundleEntry { Id = br.ReadUInt32(), Address = br.ReadUInt32() };
                    if (this.HasLengthField)
                    {
                        be.Length = br.ReadInt32();
                    }

                    this.entries.Add(be);
                    if (this.HasLengthField || i <= 0)
                    {
                        continue;
                    }

                    BundleEntry pbe = this.entries[i - 1];
                    pbe.Length = (int)be.Address - (int)pbe.Address;
                }

                if (itemCount > 0 && !this.HasLengthField)
                {
                    this.entries[this.entries.Count - 1].Length = -1;
                }

                this.Footer = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
                br.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The write footer.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public void WriteFooter(BinaryWriter writer)
        {
            if (this.Footer != null)
            {
                writer.Write(this.Footer);
            }
        }

        /// <summary>
        /// The write header.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public void WriteHeader(BinaryWriter writer)
        {
            foreach (uint headerInt in this.Header)
            {
                writer.Write(headerInt);
            }
        }

        #endregion
    }
}