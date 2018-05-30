using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser.Sections
{
    class Unknown
    {
        public UInt32 tag;
        public long offset;
        public UInt32 length;
        public byte[] data = null;

        public Unknown(BinaryReader instream)
        {
            instream.BaseStream.Position -= 4; //adjust position to read the tag
            this.tag = instream.ReadUInt32();
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;

            this.data = instream.ReadBytes((int)this.length);
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.tag);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.data);

            //update section size
            long newsizeend = outstream.BaseStream.Position;
            outstream.BaseStream.Position = newsizestart;
            outstream.Write((uint)(newsizeend - (newsizestart + 4)));

            outstream.BaseStream.Position = newsizeend;
        }

        public override string ToString()
        {
            return "[Unknown] tag: " + this.tag + " offset: " + this.offset + " length: " + this.length + " data count: " + (this.data != null ? " REMAINING DATA! " + this.data.Length + " bytes" : "");
        
        }
    }
}
