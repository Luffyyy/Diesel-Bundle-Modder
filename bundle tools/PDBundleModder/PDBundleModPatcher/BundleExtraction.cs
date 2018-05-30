using DieselBundle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDBundleModPatcher
{
    public class ListEntryOption
    {
        public string Title { get; set; }

        public Func<BundleExtraction, BundleEntry, string> StringFunc { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class ListBundleOption : ListEntryOption
    {
        public new Func<BundleExtraction, BundleHeader, string, string> StringFunc { get; set; }
    }

    public struct ListOptions
    {
        public List<ListEntryOption> EntryInfo { get; set; }

        public List<ListBundleOption> BundleInfo { get; set; }

        public ListOptions(CheckedListBox.CheckedItemCollection options)
        {
            EntryInfo = new List<ListEntryOption>();
            BundleInfo = new List<ListBundleOption>();
            foreach (object obj in options)
            {
                ListEntryOption opt = (ListEntryOption)obj;
                if (opt is ListBundleOption)
                    BundleInfo.Add((ListBundleOption)opt);
                else
                    EntryInfo.Add(opt);
            }
        }
    }

    public class BundleExtraction
    {
        public Queue<string> log = new Queue<string>();
        public uint total_bundle { get; set; }
        public uint current_bundle { get; set; }
        public string current_bundle_name { get; set; }
        public uint current_bundle_progress { get; set; }
        public int current_bundle_total_progress { get; set; }
        public bool Finished { get; set; }
        private bool list = false;
        private string single_bundle;
        private string extract_folder;
        public ListOutputter ListOutput;
        private Dictionary<uint, string> cached_paths = new Dictionary<uint, string>();
        private bool terminate = false;

        public BundleExtraction(string single_bundle, bool list, CheckedListBox.CheckedItemCollection list_info, string list_formatter)
        {
            this.list = list;
            this.single_bundle = single_bundle;

            if (String.IsNullOrWhiteSpace(StaticStorage.settings.CustomExtractPath))
            {
                if (!Directory.Exists(Path.Combine(StaticStorage.settings.AssetsFolder, "extract")))
                    Directory.CreateDirectory(Path.Combine(StaticStorage.settings.AssetsFolder, "extract"));

                extract_folder = Path.Combine(StaticStorage.settings.AssetsFolder, "extract");
            }
            else
            {
                extract_folder = StaticStorage.settings.CustomExtractPath;
            }

            if (list)
            {
                Type output_type;
                switch (list_formatter)
                {
                    case "CSV":
                        output_type = typeof(CSVListOutputter);
                        break;
                    default:
                        output_type = typeof(ListOutputter);
                        break;
                }

                ListOutput = (ListOutputter)Activator.CreateInstance(output_type, !String.IsNullOrWhiteSpace(StaticStorage.settings.ListLogFile) ? StaticStorage.settings.ListLogFile : "./listlog.log", new ListOptions(list_info), this);
            }
        }

        public void Start()
        {
            List<string> files;
            if (single_bundle != null)
                files = new List<string> { Path.Combine(StaticStorage.settings.AssetsFolder, single_bundle) };
            else
                files = Directory.EnumerateFiles(StaticStorage.settings.AssetsFolder, "*_h.bundle").ToList();

            total_bundle = (uint)files.Count();

            foreach (string file in files)
            {
                if (this.terminate)
                    break;

                BundleHeader bundle;
                string bundle_path = file.Replace("_h.bundle", "");
                string bundle_id = Path.GetFileName(bundle_path);

                bundle = new BundleHeader();
                TextWriteLine("Loading bundle header " + bundle_id);
                if (!bundle.Load(bundle_path))
                {
                    TextWriteLine("Failed to parse bundle header.");
                    continue;
                }
                if (bundle.Entries.Count == 0)
                    continue;

                current_bundle_name = bundle_id;
                current_bundle_progress = 0;
                current_bundle_total_progress = list && this.ListOutput.ListOptions.EntryInfo.Count == 0 ? 1 : bundle.Entries.Count;
                if (list)
                    ListBundle(bundle, bundle_id);
                else
                {
                    TextWriteLine("Extracting bundle: " + bundle_id);
                    ExtractBundle(bundle, bundle_id);
                }
                current_bundle++;
            }
            if (ListOutput != null)
            {
                TextWriteLine("Writing List information to file");
                ListOutput.Write();
                ListOutput = null;
            }
            Finished = true;
        }

        public string[] getLog()
        {
            return log.ToArray();
        }

        public void TextWriteLine(string line, params object[] extras)
        {
            log.Enqueue(StaticStorage.log.WriteLine(string.Format(line, extras), true));
        }

        public string GetFileName(BundleEntry be)
        {
            string path;
            if (!cached_paths.ContainsKey(be.Id))
            {
                path = String.Format("unknown_{0:x}.bin", be.Id);
                NameEntry ne = StaticStorage.Index.Id2Name(be.Id);
                if (ne != null)
                {
                    path = StaticStorage.Known_Index.GetPath(ne.Path) ?? String.Format("{0:x}", ne.Path);

                    if (ne.Language != 0)
                    {
                        if (StaticStorage.Index.Id2Lang(ne.Language) != null)
                        {
                            string lang_ext = StaticStorage.Known_Index.GetAny(StaticStorage.Index.Id2Lang(ne.Language).Hash);
                            path += String.Format(".{0}", (lang_ext != null ? lang_ext : ne.Language.ToString("x")));
                        }
                        else
                            path += String.Format(".{0:x}", ne.Language);
                    }

                    string extension = StaticStorage.Known_Index.GetExtension(ne.Extension) ?? String.Format("{0:x}", ne.Extension);

                    if (!list && StaticStorage.settings.ExtensionConversion.ContainsKey(extension))
                        extension = StaticStorage.settings.ExtensionConversion[extension];

                    path += "." + extension;
                }
                cached_paths[be.Id] = path;
            }
            else
                path = cached_paths[be.Id];

            return path;
        }

        public void ListBundle(BundleHeader bundle, string bundle_id)
        {
            this.ListOutput.WriteBundle(bundle, bundle_id);

            if (this.ListOutput.ListOptions.EntryInfo.Count > 0)
            {
                for (; current_bundle_progress < current_bundle_total_progress; current_bundle_progress++)
                {
                    BundleEntry be = bundle.Entries[(int)current_bundle_progress];
                    if (this.terminate)
                        break;

                    this.ListOutput.WriteEntry(be);
                }
            }
            else
                current_bundle_progress++;
        }

        public void ExtractBundle(BundleHeader bundle, string bundle_id)
        {
            string bundle_file = Path.Combine(StaticStorage.settings.AssetsFolder, bundle_id + ".bundle");
            if (!File.Exists(bundle_file))
            {
                string error_message = "Bundle file does not exist.";
                MessageBox.Show(error_message);
                TextWriteLine(error_message);
                return;
            }
            using (FileStream fs = new FileStream(bundle_file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    for (; current_bundle_progress < current_bundle_total_progress; current_bundle_progress++)
                    {
                        BundleEntry be = bundle.Entries[(int)current_bundle_progress];
                        if (this.terminate)
                            break;

                        string path = Path.Combine(extract_folder, this.GetFileName(be));

                        if (StaticStorage.settings.IgnoreExistingFiles && File.Exists(path))
                            continue;

                        string folder = Path.GetDirectoryName(path);

                        if (!String.IsNullOrWhiteSpace(folder) && !Directory.Exists(folder))
                            Directory.CreateDirectory(folder);

                        if (be.Length == 0 && File.Exists(path))
                            continue;

                        fs.Position = be.Address;
                        byte[] data;
                        if (be.Length == -1)
                            data = br.ReadBytes((int)(fs.Length - fs.Position));
                        else
                            data = br.ReadBytes((int)be.Length);

                        File.WriteAllBytes(path, data);
                    }
                }
            }
        }

        public void Terminate()
        {
            this.terminate = true;
        }

    }

    public class ListOutputter
    {
        protected StringBuilder Output = new StringBuilder();

        protected string OutputFile;

        public ListOptions ListOptions { get; set; }

        protected dynamic Parent;

        public ListOutputter(string output_file, ListOptions options, dynamic parent)
        {
            this.OutputFile = output_file;
            ListOptions = options;
            Parent = parent;
            this.WriteTitle();
        }

        public virtual void WriteTitle()
        {
            for (int i = 0; i < this.ListOptions.EntryInfo.Count; i++)
            {
                ListEntryOption opt = this.ListOptions.EntryInfo[i];
                this.Output.Append((i == 0 ? "" : " - ") + opt.Title);
            }
            this.Output.AppendLine();
        }

        public virtual void WriteEntry(BundleEntry entry)
        {
            if (ListOptions.EntryInfo.Count == 0)
                return;

            this.Output.Append("\t");
            for (int i = 0; i < this.ListOptions.EntryInfo.Count; i++)
            {
                ListEntryOption opt = this.ListOptions.EntryInfo[i];
                this.Output.Append((i == 0 ? "" : " - ") + opt.StringFunc(Parent, entry));
            }
            this.Output.AppendLine();
        }

        public virtual void WriteBundle(BundleHeader header, string bundle_id)
        {
            if (ListOptions.BundleInfo.Count == 0)
                return;

            this.Output.AppendLine();
            for (int i = 0; i < this.ListOptions.BundleInfo.Count; i++)
            {
                ListBundleOption opt = this.ListOptions.BundleInfo[i];
                this.Output.Append((i == 0 ? "" : " - ") + opt.StringFunc(Parent, header, bundle_id));
            }
            this.Output.AppendLine((this.ListOptions.EntryInfo.Count > 0 ? ":" : ""));
        }

        public void Write()
        {
            File.WriteAllText(this.OutputFile, Output.ToString());
        }
    }

    public class CSVListOutputter : ListOutputter
    {
        public CSVListOutputter(string output_file, ListOptions options, dynamic parent) : base(output_file, options, (BundleExtraction)parent) { }

        private bool WriteEmptyBundleColumns = false;

        public override void WriteTitle()
        {
            int i = 0;

            foreach (ListEntryOption opt in this.ListOptions.BundleInfo)
            {
                this.Output.Append((i == 0 ? "" : ",") + "\"" + opt.Title + "\"");
                i++;
            }

            foreach (ListEntryOption opt in this.ListOptions.EntryInfo)
            {
                this.Output.Append((i == 0 ? "" : ",") + "\"" + opt.Title + "\"");
                i++;
            }
            this.Output.AppendLine();
        }

        public override void WriteBundle(BundleHeader header, string bundle_id)
        {
            if (ListOptions.BundleInfo.Count == 0)
                return;

            this.Output.AppendLine();
            for (int i = 0; i < this.ListOptions.BundleInfo.Count; i++)
            {
                ListBundleOption opt = this.ListOptions.BundleInfo[i];
                this.Output.Append((i == 0 ? "" : ",") + "\"" + opt.StringFunc(Parent, header, bundle_id) + "\"");
            }
            this.WriteEmptyBundleColumns = false;
        }

        public override void WriteEntry(BundleEntry entry)
        {
            if (ListOptions.EntryInfo.Count == 0)
                return;

            if (this.WriteEmptyBundleColumns && this.ListOptions.BundleInfo.Count > 0)
            {
                for (int i = 0; i < this.ListOptions.BundleInfo.Count - 1; i++)
                {
                    this.Output.Append(",");
                }
            }

            for (int i = 0; i < this.ListOptions.EntryInfo.Count; i++)
            {
                ListEntryOption opt = this.ListOptions.EntryInfo[i];
                this.Output.Append((i == 0 && this.ListOptions.BundleInfo.Count == 0 ? "" : ",") + "\"" + opt.StringFunc(Parent, entry) + "\"");
            }
            this.Output.AppendLine();
            this.WriteEmptyBundleColumns = true;
        }
    }
}
