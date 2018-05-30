using SoundBankParser.Sections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoundBankParser
{
    public class StaticStorage
    {
        public static Dictionary<byte, HashSet<object>> parameters = new Dictionary<byte, HashSet<object>>();
        public static Dictionary<uint, SoundFile> soundfiles = new Dictionary<uint, SoundFile>();
    }
    
    public class BNK
    {
        private uint BKHD_tag = 0x44484B42;
        private uint DIDX_tag = 0x58444944;
        private uint DATA_tag = 0x41544144;
        private uint HIRC_tag = 0x43524948;
        private uint STID_tag = 0x44495453;

        private List<object> sections = new List<object>();

        public Mutex ProgressMutex = new Mutex();

        public bool isLoaded = false;
        public float loadPercent = 0.0f;

        public BNK()
        {
            isLoaded = false;
            loadPercent = 0.0f;
        }

        public void LoadBNK(Stream instream)
        {
            instream.Position = 0;
            using (BinaryReader br = new BinaryReader(instream))
            {
                while (instream.Position < instream.Length)
                {
                    this.ProgressMutex.WaitOne();
                    loadPercent = (float)((100 * instream.Position) / instream.Length);
                    this.ProgressMutex.ReleaseMutex();

                    //Find all sections
                    uint section_tag = br.ReadUInt32();
                    //long offset = fs.Position+4;
                    object section = new object();

                    if (section_tag == BKHD_tag)
                    {
                        section = new BKHD(br);
                    }
                    else if (section_tag == DIDX_tag)
                    {
                        section = new DIDX(br);
                    }
                    else if (section_tag == DATA_tag)
                    {
                        section = new DATA(br);
                    }
                    else if (section_tag == STID_tag)
                    {
                        section = new STID(br);
                    }
                    else if (section_tag == HIRC_tag)
                    {
                        section = new HIRC(br);
                    }
                    else
                    {
                        section = new Unknown(br);
                    }

                    //Console.WriteLine(section);
                    sections.Add(section);
                }
            }

            /*
            foreach(KeyValuePair<byte, HashSet<object>> kvp in StaticStorage.parameters)
            {
                Console.WriteLine(kvp.Key);
                foreach(object val in kvp.Value)
                    Console.WriteLine("\t"+val);
            }
            */

            //organize DATA section and compile a list of sound files
            DIDX didx_section = sections.FirstOrDefault(e => e is DIDX) as DIDX;
            DATA data_section = sections.FirstOrDefault(e => e is DATA) as DATA;
            HIRC hirc_section = sections.FirstOrDefault(e => e is HIRC) as HIRC;
            Dictionary<uint, UInt32> fileOffsets = new Dictionary<uint, uint>();

            if (didx_section != null && data_section != null)
            {
                foreach (var obj in didx_section.objects)
                    data_section.files.Add(obj.id, data_section.remaining_data.Skip((int)obj.data_offset).Take((int)obj.data_length).ToArray());
                fileOffsets = data_section.GenerateFileData();
            }

            if (hirc_section != null)
            {
                for (int x = 0; x < hirc_section.objects.Count; x++)
                {
                    object section = hirc_section.objects[x];
                    SoundFile sf = new SoundFile();
                    if (section is HIRC_SoundSFX)
                    {
                        sf.id = (section as HIRC_SoundSFX).soundid;
                        sf.streamed = ((section as HIRC_SoundSFX).soundincluded == 0 ? false : true);
                        sf.length_object = section;
                    }
                    else if (section is HIRC_MusicTrack)
                    {
                        sf.id = (section as HIRC_MusicTrack).soundID;
                        sf.streamed = ((section as HIRC_MusicTrack).streamed == 0 ? false : true);
                        sf.length_object = section;
                        sf.loop_object = hirc_section.objects[x + 1];

                        if ((sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParameters_count > 0)
                        {
                            sf.effects = "Additional parameters:\r\n";
                            for (int y = 0; y < (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParameters_count; y++)
                            {
                                byte type = (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersType[y];

                                if (type == 0x00)
                                    sf.effects += "General Settings: Voice: Volume - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x02)
                                    sf.effects += "General Settings: Voice: Pitch - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x03)
                                    sf.effects += "General Settings: Voice: Low-pass filter - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x05)
                                    sf.effects += "Advanced Settings: Playback Priority: Priority - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x06)
                                    sf.effects += "Advanced Settings: Playback Priority: Offset priority - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x07)
                                    sf.effects += "Loop count (0 = infinite) - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x08)
                                    sf.effects += " Motion: Audio to Motion Settings: Motion Volume Offset - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x0B)
                                    sf.effects += "Positioning: 2D: Panner X-coordinate - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x0C)
                                    sf.effects += "Positioning: 2D: Panner Y-coordinate - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x0D)
                                    sf.effects += "Positioning: Center % - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x12)
                                    sf.effects += "General Settings: User-Defined Auxiliary Sends: Bus #0 Volume - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x13)
                                    sf.effects += "General Settings: User-Defined Auxiliary Sends: Bus #1 Volume - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x14)
                                    sf.effects += "General Settings: User-Defined Auxiliary Sends: Bus #2 Volume - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x15)
                                    sf.effects += "General Settings: User-Defined Auxiliary Sends: Bus #3 Volume - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x16)
                                    sf.effects += "General Settings: Game-Defined Auxiliary Sends: Volume - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x17)
                                    sf.effects += "General Settings: Output Bus: Volume - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else if (type == 0x18)
                                    sf.effects += "General Settings: Output Bus: Low-pass filter - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];
                                else
                                    sf.effects += "Unknown - " + (sf.loop_object as HIRC_MusicSegment).soundstructure.additionalParametersValue[y];

                                sf.effects += "\r\n";
                            }
                        }
                        else
                            sf.effects = "This sound does not have any additional parameters.";

                        x++;
                    }
                    else
                        continue;

                    if (!StaticStorage.soundfiles.ContainsKey(sf.id))
                        StaticStorage.soundfiles.Add(sf.id, sf);
                }
            }

            foreach (var sf in StaticStorage.soundfiles.Values)
            {
                if (fileOffsets.ContainsKey(sf.id))
                    sf.data_offset = fileOffsets[sf.id];
                else
                    sf.data_offset = 0;
            }

            isLoaded = true;
        }

        public void GenerateBNK(MemoryStream outstream)
        {
            BinaryWriter bw = new BinaryWriter(outstream);

            foreach (object obj in sections)
            {
                if (obj is BKHD)
                    (obj as BKHD).StreamWrite(bw);
                else if (obj is DIDX)
                    (obj as DIDX).StreamWrite(bw);
                else if (obj is DATA)
                    (obj as DATA).StreamWrite(bw);
                else if (obj is STID)
                    (obj as STID).StreamWrite(bw);
                else if (obj is HIRC)
                    (obj as HIRC).StreamWrite(bw);
                else if (obj is Unknown)
                    (obj as Unknown).StreamWrite(bw);
            }
        }

    }
}
