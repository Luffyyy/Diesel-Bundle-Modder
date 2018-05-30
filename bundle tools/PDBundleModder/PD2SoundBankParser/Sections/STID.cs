using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser.Sections
{
    class SoundBank
    {
        public UInt32 id;
        public String soundbank_name;

        public SoundBank(BinaryReader instream)
        {
            this.id = instream.ReadUInt32();
            this.soundbank_name = instream.ReadString();
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.id);
            outstream.Write(this.soundbank_name);
        }
    }

    class STID
    {
        private uint STID_tag = 0x44495453;
        public long offset;

        public UInt32 length;
        public UInt32 unknownInt1;
        public UInt32 soundbank_count;
        public List<SoundBank> soundbanks = new List<SoundBank>();

        public byte[] remaining_data = null;

        public STID(BinaryReader instream)
        {
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.unknownInt1 = instream.ReadUInt32();
            this.soundbank_count = instream.ReadUInt32();

            for (int x = 0; x < this.soundbank_count; x++)
                this.soundbanks.Add(new SoundBank(instream));

            if (instream.BaseStream.Position - offset < this.length)
            {
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - offset)));
            }
            else if (instream.BaseStream.Position - offset > this.length)
            {
                Console.WriteLine("STID - YOU READ TOO MUCH!!!");
            }
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.STID_tag);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.unknownInt1);
            outstream.Write(this.soundbank_count);
            foreach (SoundBank soundbank_obj in this.soundbanks)
                soundbank_obj.StreamWrite(outstream);

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
            string ret_string = "[STID] offset: " + this.offset + " length: " + this.length + " unknownInt1: " + this.unknownInt1 + " soundbank_count: " + this.soundbank_count + (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "");

            if (this.soundbanks.Count > 0)
            {
                foreach (SoundBank soundbank_obj in this.soundbanks)
                {
                    ret_string += "\r\n\t SoundBank Object [ " +
                "id: " + soundbank_obj.id + " " +
                "name: " + soundbank_obj.soundbank_name + " " +
                "]";
                }
            }

            return ret_string;
        }

    }
}
