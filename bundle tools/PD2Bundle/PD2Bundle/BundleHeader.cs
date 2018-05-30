using System;
using System.Collections.Generic;
using System.IO;

namespace PD2Bundle
{
    class BundleEntry
    {
        public uint id;
        public uint address;
        public int length;
        public override string ToString()
        {
            return String.Format("BundleEntry(id: {0} address: {1} length: {2})", id, address, length);
        }
    }

    class BundleHeader
    {
        public List<BundleEntry> Entries = new List<BundleEntry>();
        public bool Load(string bundle_id)
        {
            string header = bundle_id + "_h.bundle";
            if(!File.Exists(header))
            {
                Console.WriteLine("Bundle header file does not exist.");
                return false;
            }
            try
            {
                uint item_count;
                bool has_length;
                using (FileStream fs = new FileStream(header, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        br.ReadUInt32();
                        br.ReadUInt32();
                        item_count = br.ReadUInt32();
                        br.ReadUInt32();
                        has_length = br.ReadUInt32() == 24;
                        if (has_length)
                        {
                            fs.Position += 2 * 4;
                        }
                        for (int i = 0; i < item_count; ++i)
                        {
                            BundleEntry be = new BundleEntry();
                            be.id = br.ReadUInt32();
                            be.address = br.ReadUInt32();
                            if (has_length)
                            {
                                be.length = br.ReadInt32();
                            }
                            this.Entries.Add(be);
                            if (!has_length && i > 0)
                            {
                                BundleEntry pbe = this.Entries[i - 1];
                                pbe.length = (int)be.address - (int)pbe.address;
                            }
                        }
                    }
                }
                if (item_count > 0 && !has_length)
                {
                    Entries[Entries.Count - 1].length = -1;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
