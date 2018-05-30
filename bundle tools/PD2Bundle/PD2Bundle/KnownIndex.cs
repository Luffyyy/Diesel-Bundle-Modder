using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PD2Bundle
{
    class Hash64
    {
        [DllImport("hash64.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong Hash(byte[] k, ulong length, ulong level);
        public static ulong HashString(string input, ulong level = 0)
        {
            return Hash(UTF8Encoding.UTF8.GetBytes(input), (ulong)UTF8Encoding.UTF8.GetByteCount(input), level);
        }
    }

    class KnownIndex
    {
        private Dictionary<ulong, string> exts = new Dictionary<ulong, string>();
        private Dictionary<ulong, string> paths = new Dictionary<ulong, string>();
        private HashSet<string> usedexts = new HashSet<string>();
        private HashSet<string> usedpaths = new HashSet<string>();

        public string GetExtension(ulong hash)
        {
            if (exts.ContainsKey(hash))
            {
                this.usedexts.Add(exts[hash]);
                return exts[hash];
            }
            return null;
        }

        public string GetPath(ulong hash)
        {
            if (paths.ContainsKey(hash))
            {
                this.usedpaths.Add(paths[hash]);
                return paths[hash];
            }
            return null;
        }

        private void CheckCollision(Dictionary<ulong, string> item, ulong hash, string value)
        {
            if ( item.ContainsKey(hash) && (item[hash] != value) )
            {
                Console.WriteLine("Hash collision: {0:x} : {1} == {2}", hash, item[hash], value);
            }
        }

        public HashSet<string> GetUsedPaths()
        {
            return this.usedpaths;
        }

        public HashSet<string> GetUsedExts()
        {
            return this.usedexts;
        }

        public void Clear()
        {
            this.exts.Clear();
            this.paths.Clear();
            this.usedexts.Clear();
            this.usedpaths.Clear();
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
    }
}
