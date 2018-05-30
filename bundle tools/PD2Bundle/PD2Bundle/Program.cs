using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DieselBundle;

namespace PD2Bundle
{
    class Program
    {
        private static NameIndex name_index = new NameIndex();
        private static KnownIndex known_index = new KnownIndex();
        private static bool list_only = false;
        private static bool extract_one = false;
        private static string extract_id = "";
        private static string extract_path = "";
        private static bool extract_all = false;
        private static bool custom_path = false;
        private const string HashlistFile = "./hashlist";

        static void Main(string[] args)
        {
            /**foreach (string arg in args)
            {
                switch (arg)
                {
                    case "-list":
                        list_only = true;
                        break;
                    case "-extract_all":
                        extract_all = true;
                        break;
                    default:
                        extract_one = true;
                        extract_id = arg;
                        break;
                }
            }**/

            for (int i = 0; i < args.Length; i++)
            {
                switch(args[i])
                {
                    case "-list":
                        list_only = true;
                        break;
                    case "-extract_one":
                        extract_one = true;
                        extract_id = args[i + 1];
                        break;
                    case "-extract_all":
                        extract_all = true;
                        break;
                    case "-extract_to":
                        extract_path = args[i + 1];
                        custom_path = true;
                        break;
                }
            }

            if (!LoadHashList())
            {
                Console.ReadKey();
                return;
            }

            if (extract_one && extract_id.Length > 0)
            {
                BundleHeader bundle = new BundleHeader();
                if (!bundle.Load(extract_id))
                {
                    Console.WriteLine("Failed to parse bundle header.");
                    return;
                }
                if (list_only)
                {
                    ListBundle(bundle, extract_id);
                }
                else
                {
                    ExtractBundle(bundle, extract_id);
                }
            }
            else
            {
                foreach (string file in Directory.EnumerateFiles(".", "*_h.bundle"))
                {
                    string bundle_id = file.Replace("_h.bundle", "");
                    bundle_id = bundle_id.Remove(0, 2);
                    BundleHeader bundle = new BundleHeader();
                    Console.WriteLine("Loading bundle header...");
                    if (!bundle.Load(bundle_id))
                    {
                        Console.WriteLine("Failed to parse bundle header.");
                        return;
                    }
                    Console.WriteLine("Extract bundle: {0}", bundle_id);

                    if (list_only)
                    {
                        ListBundle(bundle, bundle_id);
                    }
                    else
                    {
                        ExtractBundle(bundle, bundle_id);
                    }
                }
            }
        }

        private static void ListBundle(BundleHeader bundle, string bundle_id)
        {
            foreach (BundleEntry be in bundle.Entries)
            {
                string path = String.Format("unknown_{0:x}.bin", be.Id);
                NameEntry ne = name_index.Id2Name(be.Id);
                if (ne != null)
                {
                    string name = known_index.GetPath(ne.Path);
                    string extension = known_index.GetExtension(ne.Extension);
                    if (name != null)
                    {
                        path = name;
                    }
                    else
                    {
                        path = String.Format("{0:x}", ne.Path);
                    }
                    if (ne.Language != 0)
                    {
                        path += String.Format(".{0:x}", ne.Language);
                    }
                    if (extension != null)
                    {
                        path += String.Format(".{0}", extension);
                    }
                    else
                    {
                        path += String.Format(".{0:x}", ne.Extension);
                    }
                }
                Console.WriteLine("{0:x} - {1}", ne.Path, path);
            }
        }

        private static void ExtractBundle(BundleHeader bundle, string bundle_id)
        {
            string bundle_file = bundle_id + ".bundle";
            if (!File.Exists(bundle_file))
            {
                Console.WriteLine("Bundle file does not exist.");
                return;
            }
            using (FileStream fs = new FileStream(bundle_file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    if (!Directory.Exists(custom_path ? extract_path: "extract"))
                    {
                        Directory.CreateDirectory(custom_path ? extract_path : "extract");
                    }
                    string file_prefix = custom_path ? extract_path : "extract/";
                    byte[] data;
                    foreach (BundleEntry be in bundle.Entries)
                    {
                        string path = String.Format("unknown_{0:x}.bin", be.Id);
                        NameEntry ne = name_index.Id2Name(be.Id);
                        if (ne != null)
                        {
                            string name = known_index.GetPath(ne.Path);
                            string extension = known_index.GetExtension(ne.Extension);
                            if (!extract_all)
                            {
                                switch (extension)
                                {
                                    case "stream":
                                        continue;
                                    case "texture":
                                        continue;
                                    case "movie":
                                        continue;
                                }
                            }
                            if (name != null)
                            {
                                path = name;
                            }
                            else
                            {
                                path = String.Format("{0:x}", ne.Path);
                            }
                            if (ne.Language != 0)
                            {
                                path += String.Format(".{0:x}", ne.Language);
                            }
                            if (extension != null)
                            {
                                path += String.Format(".{0}", extension);
                            }
                            else
                            {
                                path += String.Format(".{0:x}", ne.Extension);
                            }
                        }
                        string folder = Path.GetDirectoryName(path);
                        if (folder != null && folder.Length != 0)
                        {
                            if (!Directory.Exists(Path.Combine(file_prefix + folder)))
                            {
                                Directory.CreateDirectory(Path.Combine(file_prefix + folder));
                            }
                        }
                        using (FileStream os = new FileStream(Path.Combine(file_prefix + path), FileMode.Create, FileAccess.Write))
                        {
                            using (BinaryWriter obr = new BinaryWriter(os))
                            {
                                fs.Position = be.Address;
                                if (be.Length == -1)
                                {
                                    data = br.ReadBytes((int)(fs.Length - fs.Position));
                                    obr.Write(data);
                                }
                                else
                                {
                                    data = br.ReadBytes((int)be.Length);
                                    obr.Write(data);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool LoadHashList()
        {
            name_index.Load("bundle_db.blb");
            if (!File.Exists(HashlistFile))
            {
                //Hashlist was not found, it will need to be retrieved.

                if (!RetrieveHashlist())
                {
                    Console.WriteLine("There was an error retrieving the hashlist");
                    return false;
                }

            }
            else
            {
                known_index.Load(HashlistFile);
            }

            SortedSet<String> paths = new SortedSet<String>();
            List<NameEntry> nameentrylist = name_index.getid2NameEntries();

            foreach (NameEntry ne in nameentrylist)
            {
                string path = known_index.GetPath(ne.Path);
                string extension = known_index.GetExtension(ne.Extension);
                string language = null;

                if (name_index.Id2Lang(ne.Language) != null)
                {
                    language = known_index.GetAny(name_index.Id2Lang(ne.Language).Hash);

                    if (String.IsNullOrWhiteSpace(language))
                        language = ne.Language.ToString("X");
                }


                if (path == null || extension == null)
                    continue;

                paths.Add(path + "." + (!String.IsNullOrWhiteSpace(language) ? language + "." : "") + extension);
            }
            return true;
        }

        public static bool RetrieveHashlist()
        {

            HashSet<string> new_paths = new HashSet<string>();
            HashSet<string> new_exts = new HashSet<string>();
            HashSet<string> new_other = new HashSet<string>();
            StringBuilder sb = new StringBuilder();
            string[] known_bundles = { "all_14" };

            string[] idstring_data;

            foreach (string bundle_id in known_bundles)
            {
                string bundle_id_path = bundle_id;
                if (!File.Exists(bundle_id + ".bundle") || !File.Exists(bundle_id + "_h.bundle"))
                {
                    continue;
                }

                BundleHeader bundle = new BundleHeader();
                if (!bundle.Load(bundle_id_path))
                {
                    Console.WriteLine(string.Format("[Update error] Failed to parse bundle header. ({0})", bundle_id));
                    return false;
                }

                string bundle_file = bundle_id_path + ".bundle";
                if (!File.Exists(bundle_file))
                {
                    Console.WriteLine(string.Format("[Update error] Bundle file does not exist. ({0})", bundle_file));
                    return false;
                }
                using (FileStream fs = new FileStream(bundle_file, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] data;
                        foreach (BundleEntry be in bundle.Entries)
                        {
                            NameEntry ne = name_index.Id2Name(be.Id);
                            if (ne == null)
                                continue;
                            
                            if (ne.Path == 0x9234DD22C60D71B8 && ne.Extension == 0x9234DD22C60D71B8)
                            {
                                fs.Position = be.Address;
                                if (be.Length == -1)
                                    data = br.ReadBytes((int)(fs.Length - fs.Position));
                                else
                                    data = br.ReadBytes((int)be.Length);

                                foreach (byte read in data)
                                    sb.Append((char)read);

                                idstring_data = sb.ToString().Split('\0');
                                sb.Clear();

                                foreach (string idstring in idstring_data)
                                {
                                    if (idstring.Contains("/"))
                                        new_paths.Add(idstring);
                                    else if (!idstring.Contains("/") && !idstring.Contains(".") && !idstring.Contains(":") && !idstring.Contains("\\"))
                                        new_exts.Add(idstring);
                                    else
                                        new_other.Add(idstring);
                                }

                                new_paths.Add("idstring_lookup");
                                new_paths.Add("existing_banks");

                                known_index.Clear();
                                known_index.Load(ref new_paths, ref new_exts, ref new_other);

                                known_index.GenerateHashList(HashlistFile);

                                new_paths.Clear();
                                new_exts.Clear();

                                return true;
                            }
                        }
                        br.Close();
                    }
                    fs.Close();
                }
            }
            return false;
        }

    }
}
