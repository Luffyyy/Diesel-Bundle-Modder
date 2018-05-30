using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PDBundleModPatcher
{
    
    public class DieselStrings
    {
        private Dictionary<string, string> _strings = new Dictionary<string, string>();
        public Dictionary<string, string> strings { get { return new Dictionary<string, string>(_strings); } }

        public DieselStrings(Stream data)
        {
            using (BinaryReader br = new BinaryReader(data))
            {
                br.ReadUInt32(); //ignore
                uint stringCount = br.ReadUInt32(); //count
                br.ReadUInt32(); //count
                uint startingpos = br.ReadUInt32();
                br.ReadUInt32(); //ignore
                br.ReadUInt32(); //unknown
                br.ReadUInt32(); //unknown
                br.ReadUInt32(); //unknown
                br.ReadUInt32(); //unknown
                br.ReadUInt32(); //unknown

                //Go to the good stuff
                br.BaseStream.Seek(startingpos, SeekOrigin.Begin);

                for (int count = 0; count < stringCount; count++)
                {
                    UInt64 stringHash = br.ReadUInt64();
                    br.ReadUInt32(); //ignore
                    uint stringPos = br.ReadUInt32();
                    br.ReadUInt32(); //unknown
                    br.ReadUInt32(); //unknown

                    StringBuilder str = new StringBuilder();
                    long prevPos = br.BaseStream.Position;
                    br.BaseStream.Position = (long)stringPos;

                    char c;
                    while((c = br.ReadChar()) != '\0')
                    {
                        str.Append(c);
                    }

                    br.BaseStream.Position = prevPos;

                    string Key = (StaticStorage.Known_Index.GetAny(stringHash) != null ? StaticStorage.Known_Index.GetAny(stringHash) : stringHash.ToString());

                    if(this._strings.ContainsKey(Key))
                        this._strings[Key] = str.ToString();
                    else
                        this._strings.Add( Key, str.ToString());
                }
            }
        }

        /*
        public void StreamWriteData(Stream outstream)
        {
            Byte zero = 0;

            using (BinaryWriter bw = new BinaryWriter(outstream))
            {
                bw.Write();
                
                outstream.Write(this.hashname);
                outstream.Write(this.email.ToCharArray());
                outstream.Write(zero);
                outstream.Write(this.source_file.ToCharArray());
                outstream.Write(zero);
                outstream.Write(this.unknown2);

                if (this.remaining_data != null)
                    outstream.Write(this.remaining_data);
            }
        }
        */
    }
}
