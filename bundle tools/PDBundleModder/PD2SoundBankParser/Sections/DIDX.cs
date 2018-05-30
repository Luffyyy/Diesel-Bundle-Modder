using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser.Sections
{
    class DIDX_object
    {
        public UInt32 id;
        public UInt32 data_offset;
        public UInt32 data_length;

        public DIDX_object(BinaryReader instream)
        {
            this.id = instream.ReadUInt32();
            this.data_offset = instream.ReadUInt32();
            this.data_length = instream.ReadUInt32();
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.id);
            outstream.Write(this.data_offset);
            outstream.Write(this.data_length);
        }
    }

    class DIDX
    {
        private uint DIDX_tag = 0x58444944;

        public long offset;
        public UInt32 length;
        public List<DIDX_object> objects = new List<DIDX_object>();

        public byte[] remaining_data = null;

        public DIDX(BinaryReader instream)
        {
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;

            while (instream.BaseStream.Position - offset < this.length)
                this.objects.Add(new DIDX_object(instream));

            if (instream.BaseStream.Position - this.offset < this.length)
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - this.offset)));
            else if (instream.BaseStream.Position - this.offset > this.length)
                Console.WriteLine("DIDX - YOU READ TOO MUCH!!!");
        }

        public void UpdateObjects(DATA data_section)
        {
            if (data_section.files.Count == 0)
                return;

            foreach (var obj in this.objects)
            {
                if (data_section.files.ContainsKey(obj.id))
                {

                }
            }

        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.DIDX_tag);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            foreach (DIDX_object didx_obj in this.objects)
                didx_obj.StreamWrite(outstream);

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
            string ret_string = "[DIDX] offset: " + this.offset + " length: " + this.length + " objects count: " + this.objects.Count + (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "");

            if (this.objects.Count > 0)
            {
                foreach (DIDX_object didx_obj in this.objects)
                {
                    ret_string += "\r\n\t DIDX Object [ " +
                    "id: " + didx_obj.id + " " +
                    "data_offset: " + didx_obj.data_offset + " " +
                    "data_length: " + didx_obj.data_length + " " +
                    "]";
                }
            }

            return ret_string;
        }

    }
}

