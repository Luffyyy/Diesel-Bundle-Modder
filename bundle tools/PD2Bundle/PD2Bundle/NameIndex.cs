using System;
using System.Collections.Generic;
using System.IO;

namespace PD2Bundle
{

    class NameEntry
    {
        public ulong path;
        public ulong extension;
        public uint language;
        public override string ToString()
        {
            return path.ToString("x") + '.' + language.ToString("x") + '.' + extension.ToString("x");
        }
    };

    class NameIndex
    {
        private Dictionary<uint, NameEntry> id2name = new Dictionary<uint, NameEntry>();

        public void Add(ulong extension, ulong path, uint language, uint id)
        {
            NameEntry b = new NameEntry { extension = extension, path = path, language = language };
            id2name[id] = b;
        }

        public NameEntry Id2Name(uint id)
        {
            if (id2name.ContainsKey(id))
            {
                return id2name[id];
            }
            return null;
        }

        public uint Name2Id(string name)
        {
            return 0;
        }

        public void Clear()
        {
            id2name.Clear();
        }

        public bool Load(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    for (int i = 0; i < 8; ++i)
                    {
                        reader.ReadBytes(4);
                    }
                    uint count = reader.ReadUInt32();
                    uint offset = reader.ReadUInt32();
                    fs.Position = (long)offset;
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
    }
}
