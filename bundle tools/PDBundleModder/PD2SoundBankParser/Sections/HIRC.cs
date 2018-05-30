using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser.Sections
{
    class HIRC_object
    {
        private long offset;
        public byte type;
        public UInt32 length;
        public UInt32 id;
        public byte[] remaining_data = null;

        public HIRC_object(BinaryReader instream)
        {
            instream.BaseStream.Position -= 1;

            this.type = instream.ReadByte();
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.id = instream.ReadUInt32();

            if (instream.BaseStream.Position - offset < this.length)
            {
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - offset)));
            }
            else if (instream.BaseStream.Position - offset > this.length)
            {
                Console.WriteLine("HIRC_object - YOU READ TOO MUCH!!!");
            }
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.type);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.id);

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
            return "HIRC Object [ " +
                "type: " + this.type + " " +
                "length: " + this.length + " " +
                "offset: " + this.offset + " " +
                "id: " + this.id + " " +
                (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "") +
                "]";
        }
    }

    class HIRC_Settings
    {
        private long offset;
        public byte type; //1
        public UInt32 length;
        public UInt32 id;
        public byte settings_count;
        public List<byte> settings_voice = new List<byte>();
        public List<byte> settings_voice_value = new List<byte>();

        public byte[] remaining_data = null;

        public HIRC_Settings(BinaryReader instream)
        {
            instream.BaseStream.Position -= 1;

            this.type = instream.ReadByte();
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.id = instream.ReadUInt32();
            this.settings_count = instream.ReadByte();
            for (int x = 0; x < this.settings_count; x++)
                this.settings_voice.Add(instream.ReadByte());
            for (int x = 0; x < this.settings_count; x++)
                this.settings_voice_value.Add(instream.ReadByte());


            if (instream.BaseStream.Position - offset < this.length)
            {
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - offset)));
            }
            else if (instream.BaseStream.Position - offset > this.length)
            {
                Console.WriteLine("HIRC_Settings - YOU READ TOO MUCH!!!");
            }
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.type);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.id);
            outstream.Write(this.settings_count);
            foreach (byte set_voice in this.settings_voice)
                outstream.Write(set_voice);
            foreach (byte set_voice_val in this.settings_voice_value)
                outstream.Write(set_voice_val);

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
            return "HIRC Settings [ " +
                "type: " + this.type + " " +
                "length: " + this.length + " " +
                "offset: " + this.offset + " " +
                "id: " + this.id + " " +
                "settings_count: " + this.settings_count + " " +
                "settings_voice: " + this.settings_voice.Count + " " +
                "settings_voice_value: " + this.settings_voice_value.Count + " " +
                (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "") +
                "]";
        }
    }

    class HIRC_SoundSFX
    {
        private long offset;
        public byte type; //2
        public UInt32 length;
        public UInt32 id;
        public UInt32 unknownInt;
        public UInt32 soundincluded; //0 = included, 1 = streamed, 2= streamed with prefetch
        public UInt32 soundid;
        public UInt32 soundsourceid;
        public UInt32 soundoffset; //if included
        public UInt32 soundlength = UInt32.MaxValue; //if included //NOTE: MaxValue for debug, remove for release
        public byte soundtype; //00 = Sound SFX, 01 = Sound Voice
        public SoundStructure structure;

        public byte[] remaining_data = null;

        public HIRC_SoundSFX(BinaryReader instream)
        {
            instream.BaseStream.Position -= 1;

            this.type = instream.ReadByte();
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.id = instream.ReadUInt32();
            this.unknownInt = instream.ReadUInt32();
            this.soundincluded = instream.ReadUInt32();
            this.soundid = instream.ReadUInt32();
            this.soundsourceid = instream.ReadUInt32();
            if (this.soundincluded == 0)
            {
                this.soundoffset = instream.ReadUInt32();
                this.soundlength = instream.ReadUInt32();
            }
            this.soundtype = instream.ReadByte();
            //this.structure = new SoundStructure(instream);

            if (instream.BaseStream.Position - offset < this.length)
            {
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - offset)));
            }
            else if (instream.BaseStream.Position - offset > this.length)
            {
                Console.WriteLine("HIRC_SoundSFX - YOU READ TOO MUCH!!!");
            }
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.type);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.id);
            outstream.Write(this.unknownInt);
            outstream.Write(this.soundincluded);
            outstream.Write(this.soundid);
            outstream.Write(this.soundsourceid);
            if (this.soundincluded == 0)
            {
                outstream.Write(this.soundoffset);
                outstream.Write(this.soundlength);
            }
            outstream.Write(this.soundtype);
            //outstream.Write(this.structure);

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
            return "HIRC SoundSFX [ " +
                "type: " + this.type + " " +
                "length: " + this.length + " " +
                "offset: " + this.offset + " " +
                "id: " + this.id + " " +
                "unknownInt: " + this.unknownInt + " " +
                "soundincluded: " + this.soundincluded + " " +
                "soundid: " + this.soundid + " " +
                "soundsourceid: " + this.soundsourceid + " " +
                "soundoffset: " + this.soundoffset + " " +
                "soundlength: " + this.soundlength + " " +
                "soundtype: " + this.soundtype + " " +
                //"SoundStructure: " + this.structure + " " +
                (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "") +
                "]";
        }
    }

    class HIRC_EventAction
    {
        private long offset;
        public byte type; //3
        public UInt32 length;
        public UInt32 id;
        public byte scope;
        public byte actionType;
        public UInt32 gameObjectID;
        public byte unknown1; //always 0
        public byte additionalParameters;
        public List<byte> parameterTypes = new List<byte>();
        public List<byte> parameterValues = new List<byte>();
        public byte unknown2; //always 0
        public UInt32 stateGroupId; // if actionType == 0x12 || 0x19
        public UInt32 stateId; // if actionType == 0x12 || 0x19

        public byte[] remaining_data = null;

        public HIRC_EventAction(BinaryReader instream)
        {
            instream.BaseStream.Position -= 1;

            this.type = instream.ReadByte();
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.id = instream.ReadUInt32();
            this.scope = instream.ReadByte();
            this.actionType = instream.ReadByte();
            this.gameObjectID = instream.ReadUInt32();
            this.unknown1 = instream.ReadByte();
            this.additionalParameters = instream.ReadByte();
            for (int x = 0; x < this.additionalParameters; x++)
                this.parameterTypes.Add(instream.ReadByte());
            for (int x = 0; x < this.additionalParameters; x++)
                this.parameterValues.Add(instream.ReadByte());
            this.unknown2 = instream.ReadByte();
            if(this.actionType == 0x12 || this.actionType == 0x19)
            {
                this.stateGroupId = instream.ReadUInt32();
                this.stateId = instream.ReadUInt32();
            }

            if (instream.BaseStream.Position - offset < this.length)
            {
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - offset)));
            }
            else if (instream.BaseStream.Position - offset > this.length)
            {
                Console.WriteLine("HIRC_EventAction - YOU READ TOO MUCH!!!");
            }
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.type);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.id);
            outstream.Write(this.scope);
            outstream.Write(this.actionType);
            outstream.Write(this.gameObjectID);
            outstream.Write(this.unknown1);
            outstream.Write(this.additionalParameters);
            foreach (byte paramtype in this.parameterTypes)
                outstream.Write(paramtype);
            foreach (byte paramval in this.parameterValues)
                outstream.Write(paramval);
            outstream.Write(this.unknown2);
            if (this.actionType == 0x12 || this.actionType == 0x19)
            {
                outstream.Write(this.stateGroupId);
                outstream.Write(this.stateId);
            }

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
            return "HIRC EventAction [ " +
                "type: " + this.type + " " +
                "length: " + this.length + " " +
                "offset: " + this.offset + " " +
                "id: " + this.id + " " +
                "scope: " + this.scope + " " +
                "actionType: " + this.actionType + " " +
                "gameObjectID: " + this.gameObjectID + " " +
                "unknown1: " + this.unknown1 + " " +
                "additionalParameters: " + this.additionalParameters + " " +
                "parameterTypes: " + this.parameterTypes.Count + " " +
                "parameterValues: " + this.parameterValues.Count + " " +
                "unknown2: " + this.unknown2 + " " +
                "stateGroupId: " + this.stateGroupId + " " +
                "stateId: " + this.stateId + " " +
                (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "") +
                "]";
        }
    }

    class HIRC_MusicTrack
    {
        private long offset;
        public byte type; //11
        public UInt32 length;
        public UInt32 id;

        public byte[] unknown1; //8 unknown bytes
        public UInt32 streamed; //? Seems to determine following positions
        public UInt32 soundID;
        public UInt32 soundID2; //repeated.?
        public byte[] unknown2; //9 bytes if streamed == 1 AND 17 bytes if streamed == 2
        public UInt32 soundID3; //repeated.? for the third time? I get it. It's this ID. Stop repeating it.
        public byte[] unknown3; //24 unknown bytes
        public Double soundLength; //sound length in ms (1000 ms = 1 sec)

        public byte[] remaining_data = null;

        public HIRC_MusicTrack(BinaryReader instream)
        {
            instream.BaseStream.Position -= 1;

            this.type = instream.ReadByte();
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.id = instream.ReadUInt32();
            this.unknown1 = instream.ReadBytes(8);
            this.streamed = instream.ReadUInt32();
            this.soundID = instream.ReadUInt32();
            this.soundID2 = instream.ReadUInt32();
            if(this.streamed == 1)
                this.unknown2 = instream.ReadBytes(9);
            else if(this.streamed == 2)
                this.unknown2 = instream.ReadBytes(17);
            else
                this.unknown2 = instream.ReadBytes(8); //break here. I wanna see.
            this.soundID3 = instream.ReadUInt32();
            this.unknown3 = instream.ReadBytes(24);
            this.soundLength = instream.ReadDouble();

            if (instream.BaseStream.Position - offset < this.length)
            {
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - offset)));
            }
            else if (instream.BaseStream.Position - offset > this.length)
            {
                Console.WriteLine("HIRC_MusicTrack - YOU READ TOO MUCH!!!");
            }
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.type);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.id);
            outstream.Write(this.unknown1);
            outstream.Write(this.streamed);
            outstream.Write(this.soundID);
            outstream.Write(this.soundID2);
            outstream.Write(this.unknown2);
            outstream.Write(this.soundID3);
            outstream.Write(this.unknown3);
            outstream.Write(this.soundLength);

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
            return "HIRC MusicTrack [ " +
                "type: " + this.type + " " +
                "length: " + this.length + " " +
                "offset: " + this.offset + " " +
                "id: " + this.id + " " +
                "unknown1: " + this.unknown1.Length + " " +
                "streamed: " + this.streamed + " " +
                "soundID: " + this.soundID + " " +
                "soundID2: " + this.soundID2 + " " +
                "unknown2: " + this.unknown2.Length + " " +
                "soundID3: " + this.soundID3 + " " +
                "unknown3: " + this.unknown3.Length + " " +
                "soundLength: " + this.soundLength + " " +
                (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "") +
                "]";
        }
    }

    class HIRC_MusicSegment
    {
        private long offset;
        public byte type; //10
        public UInt32 length;
        public UInt32 id;

        public SoundStructure soundstructure;
        public byte[] unknown1 = null;
        public Double looppoint1;
        public byte[] unknown2 = null;
        public Double looppoint2;

        public byte[] remaining_data = null;

        public HIRC_MusicSegment(BinaryReader instream)
        {
            instream.BaseStream.Position -= 1;

            this.type = instream.ReadByte();
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.id = instream.ReadUInt32();
            this.soundstructure = new SoundStructure(instream);

            this.unknown1 = instream.ReadBytes(37);
            this.looppoint1 = instream.ReadDouble();
            this.unknown2 = instream.ReadBytes(24);
            this.looppoint2 = instream.ReadDouble();

            /*
            this.childObjectsCount = instream.ReadUInt32();
            for (int x = 0; x < this.childObjectsCount; x++)
                this.childObjects.Add(instream.ReadUInt32());
            */
            if (instream.BaseStream.Position - offset < this.length)
            {
                this.remaining_data = instream.ReadBytes((int)(this.length - (uint)(instream.BaseStream.Position - offset)));
            }
            else if (instream.BaseStream.Position - offset > this.length)
            {
                Console.WriteLine("HIRC_MusicSegment - YOU READ TOO MUCH!!!");
            }
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.type);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.id);
            this.soundstructure.StreamWrite(outstream);

            outstream.Write(this.unknown1);
            outstream.Write(this.looppoint1);
            outstream.Write(this.unknown2);
            outstream.Write(this.looppoint2);

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
            return "HIRC MusicSegment [ " +
                "type: " + this.type + " " +
                "length: " + this.length + " " +
                "offset: " + this.offset + " " +
                "id: " + this.id + " " +
                "unknown1: " + this.unknown1.Length + " " +
                "looppoint1: " + this.looppoint1 + " " +
                "unknown2: " + this.unknown2.Length + " " +
                "looppoint2: " + this.looppoint2 + " " +
                "soundstructure: " + this.soundstructure + " " +
                (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "") +
                "]";
        }
    }

    class HIRC
    {
        private uint HIRC_tag = 0x43524948;
        //public List<Tuple<HIRC_MusicTrack, HIRC_MusicSegment>>

        public long offset;

        public UInt32 length;
        public UInt32 objects_count;
        public List<object> objects = new List<object>();

        public byte[] remaining_data = null;

        public HIRC(BinaryReader instream)
        {
            this.length = instream.ReadUInt32();
            this.offset = instream.BaseStream.Position;
            this.objects_count = instream.ReadUInt32();

            for (int x = 0; x < this.objects_count; x++)
            {
                object newobject = new object();

                byte idbyte = instream.ReadByte();

                if (idbyte == 1)
                {
                    newobject = new HIRC_Settings(instream);
                }
                else if (idbyte == 2)
                {
                    newobject = new HIRC_SoundSFX(instream);
                }
                else if (idbyte == 3)
                {
                    newobject = new HIRC_EventAction(instream);
                }
                else if (idbyte == 10)
                {
                    newobject = new HIRC_MusicSegment(instream);
                }
                else if (idbyte == 11)
                {
                    newobject = new HIRC_MusicTrack(instream);
                }
                else
                {
                    newobject = new HIRC_object(instream);
                }
                this.objects.Add(newobject);
            }
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.HIRC_tag);
            long newsizestart = outstream.BaseStream.Position;
            outstream.Write(this.length);
            outstream.Write(this.objects_count);
            foreach (object hirc_obj in this.objects)
            {
                if (hirc_obj is HIRC_Settings)
                    (hirc_obj as HIRC_Settings).StreamWrite(outstream);
                else if (hirc_obj is HIRC_SoundSFX)
                    (hirc_obj as HIRC_SoundSFX).StreamWrite(outstream);
                else if (hirc_obj is HIRC_EventAction)
                    (hirc_obj as HIRC_EventAction).StreamWrite(outstream);
                else if (hirc_obj is HIRC_MusicTrack)
                    (hirc_obj as HIRC_MusicTrack).StreamWrite(outstream);
                else if (hirc_obj is HIRC_MusicSegment)
                    (hirc_obj as HIRC_MusicSegment).StreamWrite(outstream);
                else
                    (hirc_obj as HIRC_object).StreamWrite(outstream);
            }

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
            string ret_string = "[HIRC] offset: " + this.offset + " length: " + this.length + " objects_count: " + this.objects_count + " objects.count: " + this.objects.Count + (this.remaining_data != null ? " REMAINING DATA! " + this.remaining_data.Length + " bytes" : "");

            if (this.objects.Count > 0)
            {
                foreach (object hirc_obj in this.objects)
                {
                    ret_string += "\r\n\t" + hirc_obj;
                }
            }

            return ret_string;
        }
    }
}
