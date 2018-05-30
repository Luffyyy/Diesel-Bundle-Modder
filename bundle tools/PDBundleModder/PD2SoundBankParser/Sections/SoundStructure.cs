using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser.Sections
{
    class SoundStructure_effect
    {
        public byte effectindex; //00 - 03
        public UInt32 id;
        public byte[] zeroes = null;

        public SoundStructure_effect(BinaryReader instream)
        {
            this.effectindex = instream.ReadByte();
            this.id = instream.ReadUInt32();
            this.zeroes = instream.ReadBytes(2);
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.effectindex);
            outstream.Write(this.id);
            outstream.Write(this.zeroes);
        }
    }

    class SoundStructure_stategroup
    {
        public UInt32 id;
        public byte changeAt;
        public UInt16 number_of_differingStates;
        public List<SoundStructure_stategroup_different> differingStates = new List<SoundStructure_stategroup_different>();

        public SoundStructure_stategroup(BinaryReader instream)
        {
            this.id = instream.ReadUInt32();
            this.changeAt = instream.ReadByte();
            this.number_of_differingStates = instream.ReadUInt16();
            for (int y = 0; y < this.number_of_differingStates; y++)
                this.differingStates.Add(new SoundStructure_stategroup_different(instream));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.id);
            outstream.Write(this.changeAt);
            outstream.Write(this.number_of_differingStates);
            foreach (SoundStructure_stategroup_different diffstate in this.differingStates)
                diffstate.StreamWrite(outstream);
        }
    }

    class SoundStructure_stategroup_different
    {
        public UInt32 id;
        public UInt32 settingsID;

        public SoundStructure_stategroup_different(BinaryReader instream)
        {
            this.id = instream.ReadUInt32();
            this.settingsID = instream.ReadUInt32();
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.id);
            outstream.Write(this.settingsID);
        }
    }

    class SoundStructure_rtpc
    {
        public UInt32 id;
        public UInt32 yaxisType;
        public UInt32 unknownID;
        public byte unknown1;
        public byte num_of_points;
        public byte unknown2;
        public List<SoundStructure_rtpc_point> points = new List<SoundStructure_rtpc_point>();

        public SoundStructure_rtpc(BinaryReader instream)
        {
            this.id = instream.ReadUInt32();
            this.yaxisType = instream.ReadUInt32();
            this.unknownID = instream.ReadUInt32();
            this.unknown1 = instream.ReadByte();
            this.num_of_points = instream.ReadByte();
            this.unknown2 = instream.ReadByte();
            for (int y = 0; y < this.num_of_points; y++)
                this.points.Add(new SoundStructure_rtpc_point(instream));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.id);
            outstream.Write(this.yaxisType);
            outstream.Write(this.unknownID);
            outstream.Write(this.unknown1);
            outstream.Write(this.num_of_points);
            outstream.Write(this.unknown2);
            foreach (SoundStructure_rtpc_point rtpc_point in this.points)
                rtpc_point.StreamWrite(outstream);
        }
    }

    class SoundStructure_rtpc_point
    {
        public float x;
        public float y;
        public UInt32 curveShape;

        public SoundStructure_rtpc_point(BinaryReader instream)
        {
            this.x = instream.ReadSingle();
            this.y = instream.ReadSingle();
            this.curveShape = instream.ReadUInt32();
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.x);
            outstream.Write(this.y);
            outstream.Write(this.curveShape);
        }
    }

    class SoundStructure
    {

        private long offset;
        public bool parentOverride;
        public byte effects_count;
        //if effects_count > 0
        public byte[] effect_bypassedeffects = null;
        public List<SoundStructure_effect> effects = new List<SoundStructure_effect>();
        //end if
        public UInt32 outputbusID;
        public UInt32 parentobjectID;
        public bool overrideparentsettings_priority;
        public bool offsetpriority;
        public byte additionalParameters_count;
        public List<byte> additionalParametersType = new List<byte>();
        public List<object> additionalParametersValue = new List<object>();
        public byte unknown1;
        public bool isPositioningIncluded;
        //if isPositioningIncluded
        public byte pos_type; //00 = 2D, 01 = 3D
        //if type == 00
        public bool pos_enablePanner;
        //else if type == 01
        public UInt32 pos_sourceType; //02 = User-defined. 03 = Game-defined
        public UInt32 pos_attenuationObjectID;
        public bool pos_isSpatializationEnabled;
        ////if sourceType == 02
        public UInt32 pos_playType;
        public bool pos_isLooped; //ignore if playType != 02 || 03
        public UInt32 pos_transitionTime; //in ms, ignore if playType != 02 || 03
        public bool pos_followListenerOrientation;
        ////else if sourceType == 03
        public bool pos_updateAtEachFrame;
        ////end if
        //end if
        //end if
        public bool overrideparentsettings_gdas; //Game-Defined Auxiliary Sends
        public bool usegdas;
        public bool overrrideparentsettings_udas; //User-Defined Auxiliary Sends
        public bool udas_exists;
        //if udas_exists
        public UInt32 auxiliaryBus0ID;
        public UInt32 auxiliaryBus1ID;
        public UInt32 auxiliaryBus2ID;
        public UInt32 auxiliaryBus3ID;
        //end if
        public bool unknown2;
        //if unknown2
        public byte priorityEqual; //00 = discard oldest instance, 01 = discard newest instance
        public byte limitReached; //00 = kill voice, 01 = user virtual voice settings
        public UInt32 limitInstancesTo;
        //end if
        public byte instanceLimitType; //00 = per game object, 01 = globally
        public byte virtualvoicebehavior; //00 = continue to play, 01 = kill voice, 02 = send to virtual voice
        public bool overrideparentsettings_playbacklimit;
        public bool overrideparentsettings_virtualvoice;
        public UInt32 stategroup_count;
        public List<SoundStructure_stategroup> stategroups = new List<SoundStructure_stategroup>();
        public UInt16 rtpc_count; //Real-time parameter controls
        public List<SoundStructure_rtpc> rtpcs = new List<SoundStructure_rtpc>();





        public SoundStructure(BinaryReader instream)
        {
            this.offset = instream.BaseStream.Position;
            this.parentOverride = instream.ReadBoolean();
            this.effects_count = instream.ReadByte();

            if (this.effects_count > 0)
            {
                this.effect_bypassedeffects = instream.ReadBytes(8);

                for (int x = 0; x < this.effects_count; x++)
                    this.effects.Add(new SoundStructure_effect(instream));
            }
            this.outputbusID = instream.ReadUInt32();
            this.parentobjectID = instream.ReadUInt32();
            this.overrideparentsettings_priority = instream.ReadBoolean();
            this.offsetpriority = instream.ReadBoolean();
            this.additionalParameters_count = instream.ReadByte();
            for (int x = 0; x < this.additionalParameters_count; x++)
                this.additionalParametersType.Add(instream.ReadByte());
            
            for (int x = 0; x < this.additionalParameters_count; x++)
                this.additionalParametersValue.Add((this.additionalParametersType[x] == 0x07 ? instream.ReadUInt32() : instream.ReadSingle()));

            //FOR RESEARCH, REMOVE
            for (int x = 0; x < this.additionalParameters_count; x++)
            {
                if (StaticStorage.parameters.ContainsKey(this.additionalParametersType[x]))
                    StaticStorage.parameters[this.additionalParametersType[x]].Add(this.additionalParametersValue[x]);
                else
                {
                    HashSet<object> newparamsvals = new HashSet<object>();
                    newparamsvals.Add(this.additionalParametersValue[x]);
                    StaticStorage.parameters.Add(this.additionalParametersType[x], newparamsvals);
                }
                        
            }

            /*
            if (!this.additionalParametersType.Contains((byte)00))
            {
                this.additionalParametersType.Add((byte)(00)); //REMOVE
                this.additionalParametersValue.Add(150.0f); //REMOVE
                this.additionalParameters_count++; //REMOVE
            }
            else
            {
                this.additionalParametersValue[this.additionalParametersType.FindIndex(e => e == (byte)00)] = 150.0f;
            }*/

            this.unknown1 = instream.ReadByte();
            this.isPositioningIncluded = instream.ReadBoolean();
            if (this.isPositioningIncluded)
            {
                this.pos_type = instream.ReadByte();
                if (this.pos_type == (byte)(00))
                {
                    this.pos_enablePanner = instream.ReadBoolean();
                }
                else if (this.pos_type == (byte)(01))
                {
                    this.pos_sourceType = instream.ReadUInt32();
                    this.pos_attenuationObjectID = instream.ReadUInt32();
                    this.pos_isSpatializationEnabled = instream.ReadBoolean();
                    if (this.pos_sourceType == 02)
                    {
                        this.pos_playType = instream.ReadUInt32();
                        this.pos_isLooped = instream.ReadBoolean();
                        this.pos_transitionTime = instream.ReadUInt32();
                        this.pos_followListenerOrientation = instream.ReadBoolean();
                    }
                    else if (this.pos_sourceType == 03)
                    {
                        this.pos_updateAtEachFrame = instream.ReadBoolean();
                    }
                }
            }
            this.overrideparentsettings_gdas = instream.ReadBoolean();
            this.usegdas = instream.ReadBoolean();
            this.overrrideparentsettings_udas = instream.ReadBoolean();
            this.udas_exists = instream.ReadBoolean();
            if (this.udas_exists)
            {
                this.auxiliaryBus0ID = instream.ReadUInt32();
                this.auxiliaryBus1ID = instream.ReadUInt32();
                this.auxiliaryBus2ID = instream.ReadUInt32();
                this.auxiliaryBus3ID = instream.ReadUInt32();
            }
            this.unknown2 = instream.ReadBoolean();
            if (this.unknown2)
            {
                this.priorityEqual = instream.ReadByte();
                this.limitReached = instream.ReadByte();
                this.limitInstancesTo = instream.ReadUInt32();
            }
            this.instanceLimitType = instream.ReadByte();
            this.virtualvoicebehavior = instream.ReadByte();
            this.overrideparentsettings_playbacklimit = instream.ReadBoolean();
            this.overrideparentsettings_virtualvoice = instream.ReadBoolean();
            this.stategroup_count = instream.ReadUInt32();
            for (int x = 0; x < this.stategroup_count; x++)
                this.stategroups.Add(new SoundStructure_stategroup(instream));
            this.rtpc_count = instream.ReadUInt16();
            for (int x = 0; x < this.rtpc_count; x++)
                this.rtpcs.Add(new SoundStructure_rtpc(instream));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(this.parentOverride);
            outstream.Write(this.effects_count);
            if (this.effects_count > 0)
            {
                outstream.Write(this.effect_bypassedeffects);
                foreach(SoundStructure_effect effect in this.effects)
                    effect.StreamWrite(outstream);
            }
            outstream.Write(this.outputbusID);
            outstream.Write(this.parentobjectID);
            outstream.Write(this.overrideparentsettings_priority);
            outstream.Write(this.offsetpriority);
            outstream.Write(this.additionalParameters_count);
            foreach (byte paramType in this.additionalParametersType)
                outstream.Write(paramType);
            foreach (object paramVal in this.additionalParametersValue)
            {
                if (paramVal is UInt32)
                    outstream.Write((uint)paramVal);
                else
                    outstream.Write((float)paramVal);
            }
            outstream.Write(this.unknown1);
            outstream.Write(this.isPositioningIncluded);
            if (this.isPositioningIncluded)
            {
                outstream.Write(this.pos_type);
                if (this.pos_type == (byte)(00))
                {
                    outstream.Write(this.pos_enablePanner);
                }
                else if (this.pos_type == (byte)(01))
                {
                    outstream.Write(this.pos_sourceType);
                    outstream.Write(this.pos_attenuationObjectID);
                    outstream.Write(this.pos_isSpatializationEnabled);
                    if (this.pos_sourceType == 02)
                    {
                        outstream.Write(this.pos_playType);
                        outstream.Write(this.pos_isLooped);
                        outstream.Write(this.pos_transitionTime);
                        outstream.Write(this.pos_followListenerOrientation);
                    }
                    else if (this.pos_sourceType == 03)
                    {
                        outstream.Write(this.pos_updateAtEachFrame);
                    }
                }
            }

            outstream.Write(this.overrideparentsettings_gdas);
            outstream.Write(this.usegdas);
            outstream.Write(this.overrrideparentsettings_udas);
            outstream.Write(this.udas_exists);
            if (this.udas_exists)
            {
                outstream.Write(this.auxiliaryBus0ID);
                outstream.Write(this.auxiliaryBus1ID);
                outstream.Write(this.auxiliaryBus2ID);
                outstream.Write(this.auxiliaryBus3ID);
            }
            outstream.Write(this.unknown2);
            if (this.unknown2)
            {
                outstream.Write(this.priorityEqual);
                outstream.Write(this.limitReached);
                outstream.Write(this.limitInstancesTo);
            }
            outstream.Write(this.instanceLimitType);
            outstream.Write(this.virtualvoicebehavior);
            outstream.Write(this.overrideparentsettings_playbacklimit);
            outstream.Write(this.overrideparentsettings_virtualvoice);
            outstream.Write(this.stategroup_count);
            foreach (SoundStructure_stategroup stateg in this.stategroups)
                stateg.StreamWrite(outstream);
            outstream.Write(this.rtpc_count);
            foreach (SoundStructure_rtpc rtpc in this.rtpcs)
                rtpc.StreamWrite(outstream);
        }
    }
}
