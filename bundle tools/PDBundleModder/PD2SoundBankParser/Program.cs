using SoundBankParser.Sections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBankParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = @"weapon_mg42.bnk"; //music

            //string[] bnkfiles = Directory.GetFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "*.bnk", SearchOption.TopDirectoryOnly);

            BNK soundbank;

            //foreach (string bnk in bnkfiles)
            //{
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    MemoryStream ms = new MemoryStream();
                    fs.CopyTo(ms);
                    soundbank = new BNK();
                    soundbank.LoadBNK(ms);
                    ms.Close();
                }

                //generate the same bnk
            using (FileStream fs = new FileStream(filepath + "_gen", FileMode.Create, FileAccess.Write))
                {
                    MemoryStream ms = new MemoryStream();
                    soundbank.GenerateBNK(ms);
                    ms.WriteTo(fs);
                    ms.Close();
                }
            //}
        }
    }
}
