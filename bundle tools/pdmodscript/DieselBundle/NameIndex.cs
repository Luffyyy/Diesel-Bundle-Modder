// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameIndex.cs" company="Zwagoth">
//   This code is released into the public domain by Zwagoth.
// </copyright>
// <summary>
//   The name entry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DieselBundle
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///     The name index.
    /// </summary>
    public class NameIndex
    {
        #region Fields

        /// <summary>
        ///     The id 2 name.
        /// </summary>
        private readonly Dictionary<uint, NameEntry> id2Name = new Dictionary<uint, NameEntry>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="extension">
        /// The extension.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        public void Add(ulong extension, ulong path, uint language, uint id)
        {
            var b = new NameEntry { Extension = extension, Path = path, Language = language };
            this.id2Name[id] = b;
        }

        /// <summary>
        ///     The clear.
        /// </summary>
        public void Clear()
        {
            this.id2Name.Clear();
        }

        /// <summary>
        /// The entry 2 id.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="extension">
        /// The extension.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <param name="checkLanguage">
        /// The check_language.
        /// </param>
        /// <returns>
        /// The <see cref="HashSet"/>.
        /// </returns>
        public HashSet<uint> Entry2Id(ulong path, ulong extension, uint language, bool checkLanguage = false)
        {
            var foundItems = new HashSet<uint>();
            foreach (var kvpair in this.id2Name)
            {
                NameEntry entry = kvpair.Value;
                if (entry.Path == path && entry.Extension == extension)
                {
                    if (checkLanguage)
                    {
                        if (entry.Language == language)
                        {
                            foundItems.Add(kvpair.Key);
                        }

                        continue;
                    }

                    foundItems.Add(kvpair.Key);
                }
            }

            return foundItems;
        }

        /// <summary>
        /// The id 2 name.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="NameEntry"/>.
        /// </returns>
        public NameEntry Id2Name(uint id)
        {
            if (this.id2Name.ContainsKey(id))
            {
                return this.id2Name[id];
            }

            return null;
        }

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Load(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(fs))
                {
                    for (int i = 0; i < 8; ++i)
                    {
                        reader.ReadBytes(4);
                    }

                    uint count = reader.ReadUInt32();
                    uint offset = reader.ReadUInt32();
                    fs.Position = offset;
                    try
                    {
                        for (int i = 0; i < count; ++i)
                        {
                            ulong ext = reader.ReadUInt64();
                            ulong fpath = reader.ReadUInt64();
                            uint language = reader.ReadUInt32();
                            reader.ReadBytes(4);
                            uint id = reader.ReadUInt32();
                            reader.ReadBytes(4);
                            this.Add(ext, fpath, language, id);
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}