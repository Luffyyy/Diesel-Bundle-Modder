using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser.Sections
{
    class BKHD
    {
        private uint BKHD_tag = 0x44484B42;

        public long offset;
        public UInt32 length;
        public UInt32 soundbank_version;
        public UInt32 soundbank_id;
        public UInt32 unknown1;
        public UInt32 unknown2;

        public byte[] remaining_data = null;

        public BKHD(BinaryReader instream)
        {
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.soundbank_version = instream.ReadUInt32();
            this.soundbank_id = instream.ReadUInt32();
            this.unknown1 = instream.ReadUInt32();
            this.unknown2 = instream.ReadUInt32();

            if (instream.BaseStream.Position - this.offset < this.length)
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - this.offset)));
            else if (instream.BaseStream.Position - this.offset > this.length)
                Console.WriteLine("BKHD - YOU READ TOO MUCH!!!");

        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.BKHD_tag);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.soundbank_version);
            outstream.Write(this.soundbank_id);
            outstream.Write(this.unknown1);
            outstream.Write(this.unknown2);

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
            return "[BKHD] offset: " + this.offset + " length: " + this.length + " soundbank_version: " + this.soundbank_version + " soundbank_id: " + this.soundbank_id + " unknown1: " + this.unknown1 + " unknown2: " + this.unknown2 + (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "");
        }

    }
}
