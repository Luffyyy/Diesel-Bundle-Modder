using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using DieselBundle.Utils;

namespace DieselBundle
{
    public class KnownIndex
    {
        public Dictionary<ulong, string> exts = new Dictionary<ulong, string>();
		public Dictionary<ulong, string> paths = new Dictionary<ulong, string>();
		public Dictionary<ulong, string> others = new Dictionary<ulong, string>();
        private HashSet<string> usedexts = new HashSet<string>();
        private HashSet<string> usedpaths = new HashSet<string>();
        private HashSet<string> usedothers = new HashSet<string>();

        public string version;

        public string GetExtension(ulong hash)
        {
            string ret_obj;
            if (exts.TryGetValue(hash, out ret_obj))
                this.usedexts.Add(ret_obj);
            return ret_obj;
        }

        public string GetPath(ulong hash)
        {
            string ret_obj;
            if(paths.TryGetValue(hash, out ret_obj))
                this.usedpaths.Add(ret_obj);
            return ret_obj;
        }

        public string GetOther(ulong hash)
        {
            string ret_obj;
            if (others.TryGetValue(hash, out ret_obj))
                this.usedothers.Add(ret_obj);
            return ret_obj;
        }

        public string GetAny(ulong hash)
        {
            if (paths.ContainsKey(hash))
            {
                this.usedpaths.Add(paths[hash]);
                return paths[hash];
            }
            else if (exts.ContainsKey(hash))
            {
                this.usedexts.Add(exts[hash]);
                return exts[hash];
            }
            else if (others.ContainsKey(hash))
            {
                this.usedothers.Add(others[hash]);
                return others[hash];
            }
            return null;
        }

        public void Load(ref HashSet<string> new_paths, ref HashSet<string> new_exts)
        {
            foreach (string path in new_paths)
            {
                ulong hash = Hash64.HashString(path);
                this.CheckCollision(this.paths, hash, path);
                this.paths[hash] = path;
            }

            foreach (string ext in new_exts)
            {
                ulong hash = Hash64.HashString(ext);
                this.CheckCollision(this.exts, hash, ext);
                this.exts[hash] = ext;
            }

        }

        public bool Load()
        {
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream("./paths.txt", FileMode.Open)))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        line = line.ToLower();
                        ulong hash = Hash64.HashString(line);
                        this.CheckCollision(this.paths, hash, line);
                        this.paths[hash] = line;
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
                using (StreamReader sr2 = new StreamReader(new FileStream("./extensions.txt", FileMode.Open)))
                {
                    string line = sr2.ReadLine();
                    while (line != null)
                    {
                        line = line.ToLower();
                        ulong hash = Hash64.HashString(line);
                        this.CheckCollision(this.exts, hash, line);
                        this.exts[hash] = line;
                        line = sr2.ReadLine();
                    }
                    sr2.Close();
                }
            }

            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public void GenerateUsedExts()
        {
            StreamWriter fs = new StreamWriter("extensions.txt", false);
            foreach (string ext in this.usedexts)
            {
                fs.WriteLine(ext);
            }
            fs.Close();
        }

        public void GenerateUsedPaths()
        {
            StreamWriter fs = new StreamWriter("paths.txt", false);
            foreach (string path in this.usedpaths)
            {
                fs.WriteLine(path);
            }
            fs.Close();
        }

        private void CheckCollision(Dictionary<ulong, string> item, ulong hash, string value)
        {
            if ( item.ContainsKey(hash) && (item[hash] != value) )
            {
                Console.WriteLine("Hash collision: {0:x} : {1} == {2}", hash, item[hash], value);
            }
        }

        public void Clear()
        {
            this.exts.Clear();
            this.paths.Clear();
            this.usedexts.Clear();
            this.usedpaths.Clear();
        }

        public void GenerateHashList(string HashlistFile)
        {
            StreamWriter fs = new StreamWriter(HashlistFile, false);

            if (this.version != null)
                fs.WriteLine("//" + this.version);

            foreach (KeyValuePair<ulong, string> pair in this.exts)
            {
                fs.WriteLine(pair.Value);
            }

            foreach (KeyValuePair<ulong, string> pair in this.paths)
            {
                fs.WriteLine(pair.Value);
            }

            foreach (KeyValuePair<ulong, string> pair in this.others)
            {
                fs.WriteLine(pair.Value);
            }

            fs.Close();
        }


        public void Load(ref HashSet<string> new_paths, ref HashSet<string> new_exts, ref HashSet<string> new_other)
        {
            foreach (string path in new_paths)
            {
                ulong hash = Hash64.HashString(path);
                this.CheckCollision(this.paths, hash, path);
                this.paths[hash] = path;
            }

            foreach (string ext in new_exts)
            {
                ulong hash = Hash64.HashString(ext);
                this.CheckCollision(this.exts, hash, ext);
                this.exts[hash] = ext;
            }

            foreach (string other in new_other)
            {
                ulong hash = Hash64.HashString(other);
                this.CheckCollision(this.exts, hash, other);
                this.others[hash] = other;
            }
        }

        public bool Load(string HashlistFile)
        {
            try
            {
                foreach (string hash in File.ReadLines(HashlistFile))
                {
                    if (String.IsNullOrEmpty(hash) || hash.StartsWith("//")) //Check for empty or comment
                        continue;

                    ulong hashed = Hash64.HashString(hash);

                    if ((hash.Contains("/") || hash.Equals("idstring_lookup") || hash.Equals("existing_banks")) && !paths.ContainsKey(hashed))
                        paths.Add(hashed, hash);
                    else if (!hash.Contains("/") && !hash.Contains(".") && !hash.Contains(":") && !hash.Contains("\\") && !exts.ContainsKey(hashed))
                        exts.Add(hashed, hash);
                    else if (!others.ContainsKey(hashed))
                        others.Add(hashed, hash);
                }
            }
            catch (Exception exc)
            {
                return false;
            }
            return true;
        }
    }
}
