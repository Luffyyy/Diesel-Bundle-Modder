using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser.Sections
{
    class DATA
    {

        private uint DATA_tag = 0x41544144;
        public long offset;

        public UInt32 length;
        public Dictionary<uint, byte[]> files = new Dictionary<uint, byte[]>();

        public byte[] remaining_data = null;


        public DATA(BinaryReader instream)
        {
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;

            if (instream.BaseStream.Position - offset < this.length)
            {
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - offset)));
            }
            else if (instream.BaseStream.Position - offset > this.length)
            {
                Console.WriteLine("DATA - YOU READ TOO MUCH!!!");
            }

        }

        public Dictionary<uint, UInt32> GenerateFileData()
        {
            if (files.Count == 0)
                return null;

            Dictionary<uint, UInt32> offsets = new Dictionary<uint, UInt32>();
            List<byte> newData = new List<byte>();

            uint currentOffset = 0;
            foreach (KeyValuePair<uint, byte[]> kvp in this.files)
            {
                int padding = (int)newData.Count % 16;
                if (padding > 0)
                {
                    for (int x = 16 - padding; x > 0; x--)
                        newData.Add((byte)(0));
                }

                offsets.Add(kvp.Key, currentOffset);
                newData.AddRange(kvp.Value);
                currentOffset += (uint)kvp.Value.Length;
            }

            this.remaining_data = newData.ToArray();

            return offsets;
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.DATA_tag);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);

            if (this.remaining_data != null)
                outstream.Write(this.remaining_data);
            //update section size
            long newsizeend = outstream.BaseStream.Position;
            outstream.BaseStream.Position = newsizestart;
            outstream.Write((uint)(newsizeend - (newsizestart + 4)));

            outstream.BaseStream.Position = newsizeend;
        }

        public override string ToString()
        {
            return "[DATA] offset: " + this.offset + " length: " + this.length + " files count: " + files.Count + (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "");
        }
    }
}
