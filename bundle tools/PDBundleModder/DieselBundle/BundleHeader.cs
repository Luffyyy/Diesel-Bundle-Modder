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
        ///     The header.
        /// </summary>
        public List<uint> Header = new List<uint>();



        /// <summary>
        /// The _entries.
        /// </summary>
        private List<BundleEntry> entries = new List<BundleEntry>();

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
				using (var fs = new FileStream(headerFile, FileMode.Open))
				{
					using (var br = new BinaryReader(fs))
					{
						

						this.Header = new List<uint>{
							br.ReadUInt32(), //ref offset
							br.ReadUInt32(), //tag / count
							br.ReadUInt32(), //linux:tag / count
 							br.ReadUInt32(), // offset / count
						};
						if (Header[1] != Header[2]) this.Header.Add(br.ReadUInt32());

						uint refOffset = this.Header[0];

						uint itemCount = 0, offset = 0;

						for (int i = 1; i < (Header.Count - 1); i++)
						{
							if (Header[i] == Header[i + 1])
							{
								itemCount = Header[i];
                                if (Header.Count <= i + 2)
                                {
                                    Header.Add(br.ReadUInt32());
                                }
                                offset = Header[i + 2];
								if (i != 1)
								{
									/*if (Header[1] == 0)
									{
										offset += 4;
									}
									else*/
										this.HasLengthField = true;
								}

								break;
							}
						}

						if (br.BaseStream.Position < offset)
						{
							uint amount = ((offset - (uint)br.BaseStream.Position) / 4);
							for (uint i = 0; i < amount; i++)
								this.Header.Add(br.ReadUInt32());
						}

		                br.BaseStream.Position = offset;

		                this.Header.Add(br.ReadUInt32());

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
					}
				}
            }
            catch (Exception exc)
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