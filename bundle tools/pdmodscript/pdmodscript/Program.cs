using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdmodscript
{
    using System.IO;

    using DieselBundle;

    using Ionic.Zip;

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("Usage: pdmodscript <input script> <output mod file>");
                return;
            }
            try
            {
                BundleMod newMod = new BundleMod();
                NameIndex fileIndex = new NameIndex();
                fileIndex.Load("bundle_db.blb");
                ModScriptParser parser = new ModScriptParser(args[0], ref newMod, fileIndex);
                parser.Parse();
                //Use the @name for output filename
                if (args.Length == 1)
                    Save( parser.getModName() , newMod);
                else
                    Save(args[1], newMod);
            }
            catch (Exception e)
            {
                Console.Write("[ERROR] {0}\nTrace: {1}", e.Message, e.StackTrace);
                return;
            }
        }

        private static void Save(string fileName, BundleMod mod)
        {
            //Add .pdmod at end if it's missing
            if (!fileName.Contains(".pdmod")) fileName += ".pdmod";

            fileName = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), ""));
            BundleMod modToZip = mod;
            Dictionary<string, string> filesToZip = new Dictionary<string, string>();
            int dupesCount = 1;
            foreach (BundleRewriteItem bundleItem in modToZip.ItemQueue)
            {
                if (bundleItem.ReplacementFile == null)
                {
                    continue;
                }
//                if (Path.IsPathRooted(bundleItem.ReplacementFile))
//                {

                if (filesToZip.Values.Contains(Path.GetFileName(bundleItem.ReplacementFile)))
                {
                    filesToZip.Add(bundleItem.ReplacementFile, Path.GetFileNameWithoutExtension(bundleItem.ReplacementFile) + "_" + dupesCount + Path.GetExtension(bundleItem.ReplacementFile));
                    bundleItem.ReplacementFile = Path.GetFileNameWithoutExtension(bundleItem.ReplacementFile) + "_" + dupesCount + Path.GetExtension(bundleItem.ReplacementFile);
                    Console.WriteLine(Path.GetFileName(bundleItem.ReplacementFile) + " - " + dupesCount);
                    dupesCount++;
                }
                else
                {
                    filesToZip.Add(bundleItem.ReplacementFile, Path.GetFileName(bundleItem.ReplacementFile));
                    bundleItem.ReplacementFile = Path.GetFileName(bundleItem.ReplacementFile);
                }
//                }
            }

            string modjson = BundleMod.Serialize(modToZip);
            using (var zip = new ZipFile(fileName))
            {
                zip.Password = "0$45'5))66S2ixF51a<6}L2UK";
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.RemoveSelectedEntries("*");

                foreach (KeyValuePair<string, string> kp in filesToZip)
                {
                    zip.AddEntry(Path.GetFileName(kp.Value), new FileStream(kp.Key, FileMode.Open, FileAccess.Read));
                }
                zip.AddEntry("pdmod.json", Encoding.UTF8.GetBytes(modjson));
                zip.Save();
            }
        }
    }
}
