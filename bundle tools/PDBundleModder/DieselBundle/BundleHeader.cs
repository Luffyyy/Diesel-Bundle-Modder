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
    using System.Linq;
    using System.IO;


    

    /// <summary>
    ///     The bundle header.
    /// </summary>
    /// 
    public class BundleHeader
    {
        #region Fields

        /// <summary>
        ///     The has length field.
        /// </summary>
        public bool HasLengthField = false;

        /// <summary>
        ///     Does the header contain information about multiple bundles?
        /// </summary>
        public bool multiBundle;

        /// <summary>
        ///     Is the header from a 64 bit diesel game(such as Raid WW2)?
        /// </summary>
        public bool x64;

        /// <summary>
        ///     The header.
        /// </summary>
        public List<ulong> Header = new List<ulong>();

        /// <summary>
        /// The _entries.
        /// </summary>
        private List<BundleEntry> entries = new List<BundleEntry>();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets list of bundle file entries
        /// </summary>
        public List<BundleEntry> Entries {  get { return this.entries; } }

        /// <summary>
        ///     Gets or sets
        /// </summary>
        public byte[] Footer { get; set; }

        #endregion

        #region Public Methods and Operators

        public bool ReadHeader(BinaryReader br) {

            br.BaseStream.Position = 8;
            if (br.ReadUInt32() == 0 && br.ReadUInt32() != 0)
                x64 = true;
            br.BaseStream.Position = 0;

            if (x64)
            {
                Header = new List<ulong>{
                    br.ReadUInt32(),
				    br.ReadUInt64(),
				    br.ReadUInt64(),
 				    br.ReadUInt64()
			    };
            }
            else
            {
                Header = new List<ulong>{
                    br.ReadUInt32(),//ref offset
                    br.ReadUInt32(),//tag / count
                    br.ReadUInt32(),//linux:tag / count
                    br.ReadUInt32() // offset / count
                };
            }

            if (Header[1] != Header[2])
                Header.Add(br.ReadUInt32());

            ulong itemCount = 0, offset = 0;

            for (int i = 1; i < (Header.Count - 1); i++)
            {
                if (Header[i] == Header[i + 1])
                {
                    itemCount = Header[i];
                    if (Header.Count <= i + 2)
                        Header.Add(br.ReadUInt32());

                    offset = Header[i + 2];
                    if (i != 1)
                        HasLengthField = true;

                    break;
                }
            }

            if (br.BaseStream.Position < (long)offset)
            {
                ulong amount = ((offset - (ulong)br.BaseStream.Position) / 4);
                for (uint i = 0; i < amount; i++)
                    Header.Add(br.ReadUInt32());
            }

            br.BaseStream.Position = (long)offset;

            Header.Add(br.ReadUInt32());

            for (int i = 0; i < (int)itemCount; ++i)
                ReadEntry(br, i);

            if (itemCount > 0 && !this.HasLengthField)
                this.entries[this.entries.Count - 1].Length = -1;

            //Footer breakdown
            /*
             * uint32 - tag
             * uint32 - section size
             * uint32 - count
             * uint32 - unknown
             * uint32 - unknown
             * uint32 - tag?
             * foreach (count):
             *  uint64 - hash (extension)
             *  uint64 - hash (path)
             * uint32 - end?
             * uint32 (0) - end
            */
            this.Footer = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));

            return true;
        }

        public bool ReadMultiBundleHeader(BinaryReader br, long bundleNum)
        {
            //Based of https://steamcommunity.com/app/218620/discussions/15/1474221865204158003/
            HasLengthField = true;
            multiBundle = true;

            Header = new List<ulong>()
            {
                br.ReadUInt32(), //eof
                br.ReadUInt32(), //bundle count
                br.ReadUInt32(), //unknown
                br.ReadUInt32(), //unknown
                br.ReadUInt32(), //unknown
            };

            for (long i = 0; i < (long)Header[1]; i++)
            {
                long Index = br.ReadInt64();
                uint entryCount1 = br.ReadUInt32();

                uint entryCount2 = br.ReadUInt32();
                ulong Offset = br.ReadUInt64();
                uint One = br.ReadUInt32();

                if (One == 1 && entryCount1 == entryCount2)
                {
                    if (Index == bundleNum)
                    {
                        br.BaseStream.Position = (long)Offset+4;
                        for (int x = 0; x < entryCount1; x++)
                            ReadEntry(br, x);

                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void ReadEntry(BinaryReader br, int i)
        {
            var be = new BundleEntry(br, HasLengthField);

            //Console.WriteLine("Entry " + be.Id + " Address " + be.Address + " Length " + be.Length);

            this.entries.Add(be);
            if (HasLengthField || i <= 0)
                return;

            BundleEntry pbe = this.entries[i - 1];
            pbe.Length = (int)be.Address - (int)pbe.Address;
        }

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
            bool useAllHeader = false;
            if (!File.Exists(headerFile))
            {
                if (!bundleId.StartsWith("all_"))
                {
                    headerFile = Path.GetDirectoryName(bundleId) + "\\all_h.bundle";
                    useAllHeader = true;
                }
                else
                {
                    Console.WriteLine("Bundle header file does not exist");
                    return false;
                }
            }

            try
            {
                using (var fs = new FileStream(headerFile, FileMode.Open))
                {
                    using (var br = new BinaryReader(fs))
                    {
                        if (useAllHeader)
                            return ReadMultiBundleHeader(br, int.Parse(Path.GetFileNameWithoutExtension(bundleId.Replace("all_", ""))));
                        else
                            return ReadHeader(br);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
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
                writer.Write(this.Footer);
        }

        /// <summary>
        /// The write header.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public void WriteHeader(BinaryWriter writer)
        {
            if (x64)
                foreach (ulong headerInt in this.Header)
                    writer.Write(headerInt);
            else
                foreach (uint headerInt in this.Header)
                    writer.Write(headerInt);
        }

        /// <summary>
        ///     Sort list of bundle file entries by Id
        /// </summary>
        public void SortEntriesId()
        {
            int oldcount = entries.Count();
            entries = entries.OrderBy(o => o.Id).ToList();
            if (oldcount != entries.Count())
                Console.WriteLine();
        }

        /// <summary>
        ///     Sort list of bundle file entries by Address
        /// </summary>
        public void SortEntriesAddress()
        {
            int oldcount = entries.Count();
            entries = entries.OrderBy(o => o.Length).ToList(); // Order by length, so 0 is always first
            entries = entries.OrderBy(o => o.Address).ToList(); //Order by address
            if (oldcount != entries.Count())
                Console.WriteLine();
        }

#endregion
    }
}