using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser
{
    public class SoundFile
    {
        public UInt32 id;
        public String name;
        public bool streamed;
        public UInt32 data_offset;
        public object length_object;
        public object loop_object;
        public string effects;
    
        public double GetLength()
        {
            if (this.length_object == null)
                return -1.0D;

            if(this.length_object is SoundBankParser.Sections.HIRC_SoundSFX)
                return (this.length_object as SoundBankParser.Sections.HIRC_SoundSFX).soundlength;
            else if(this.length_object is SoundBankParser.Sections.HIRC_MusicTrack)
                return (this.length_object as SoundBankParser.Sections.HIRC_MusicTrack).soundLength;

            return -1.0D;
        }

        public double[] GetLooppoints()
        {
            double[] points = new double[2];
            
            if (this.loop_object == null)
                return points;

            points[0] = (this.loop_object as SoundBankParser.Sections.HIRC_MusicSegment).looppoint1;
            points[1] = (this.loop_object as SoundBankParser.Sections.HIRC_MusicSegment).looppoint2;

            return points;
        }


        public override string ToString()
        {
            return (String.IsNullOrWhiteSpace(name) ? id.ToString() : name);
        }
    }
}
