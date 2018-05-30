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

        /// <summary>
        ///     The defined languages.
        /// </summary>
        private readonly Dictionary<uint, LanguageEntry> languages = new Dictionary<uint, LanguageEntry>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add to languages.
        /// </summary>
        /// <param name="hash">
        /// The hash of the language.
        /// </param>
        /// <param name="representation">
        /// The representation of the language.
        /// </param>
        public void AddLang(ulong hash, uint representation, uint unk)
        {
            var b = new LanguageEntry { Hash = hash, Unknown = unk };

            this.languages[representation] = b;
        }

        /// <summary>
        ///     The get Language Entries.
        /// </summary>
        public Dictionary<uint, LanguageEntry> getLangEntries()
        {
            Dictionary<uint, LanguageEntry> entries = new Dictionary<uint, LanguageEntry>(languages);

            return entries;
        }

        /// <summary>
        /// The id 2 language.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="LanguageEntry"/>.
        /// </returns>
        public LanguageEntry Id2Lang(uint id)
        {
            if (this.languages.ContainsKey(id))
            {
                return this.languages[id];
            }

            return null;
        }

        /// <summary>
        /// The language hash 2 id.
        /// </summary>
        /// <param name="lang_hash">
        /// The hash.
        /// </param>
        /// <returns>
        /// The matched ID.
        /// </returns>
        public uint Lang2Id(ulong lang_hash)
        {
            foreach (var kvpair in this.languages)
            {
                LanguageEntry entry = kvpair.Value;
                if (entry.Hash == lang_hash)
                    return kvpair.Key;
            }

            return 0;
        }

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
            var b = new NameEntry { Extension = extension, Path = path, Language = language, ID = id };
            this.id2Name[id] = b;
        }

        /// <summary>
        ///     The clear.
        /// </summary>
        public void Clear()
        {
            this.id2Name.Clear();
            this.languages.Clear();
        }

        /// <summary>
        ///     The get id2Name Entries.
        /// </summary>
        public List<NameEntry> getid2NameEntries()
        {
            List<NameEntry> entries = new List<NameEntry>();
            entries.AddRange(id2Name.Values);
            return entries;
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
					bool linux = false;
					/*#if LINUX
						reader.BaseStream.Position += 8;
					#else*/
						reader.BaseStream.Position += 4;
					//#endif                
                    uint lang_count = reader.ReadUInt32();
					if (lang_count == 0) {
						linux = true;
						lang_count = reader.ReadUInt32 ();
					}

					reader.BaseStream.Position += 4;

                    uint lang_offset;
				
					if (linux) {
						lang_offset = (uint)reader.ReadUInt64 ();
						reader.BaseStream.Position += 24;
					} else {
						lang_offset = reader.ReadUInt32 ();
						reader.BaseStream.Position += 12;
					}
                    
                    uint file_entries_count = reader.ReadUInt32();
					reader.BaseStream.Position += 4;

					uint file_entries_offset;
					if (linux)
						file_entries_offset = (uint)reader.ReadUInt64();
					else
						file_entries_offset = reader.ReadUInt32();

                    //Languages
					fs.Position = (long)lang_offset;
                    try
                    {
						for (int i = 0; i < lang_count; ++i)
                        {
                            ulong language_hash = reader.ReadUInt64();
                            uint language_representation = reader.ReadUInt32();
                            uint language_unknown = reader.ReadUInt32();

                            this.AddLang(language_hash, language_representation, language_unknown);
                        }
                    }
                    catch (Exception exc)
                    {
                        return false;
                    }

                    //File entries
					fs.Position = (long)file_entries_offset;
                    try
                    {
                        for (int i = 0; i < file_entries_count; ++i)
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
                    catch (Exception exc)
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