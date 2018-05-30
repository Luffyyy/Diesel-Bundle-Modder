// -----------------------------------------------------------------------
// <copyright file="ModScriptParser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace pdmodscript
{
    using System;
    using System.IO;

    using DieselBundle;
    using DieselBundle.Utils;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ModScriptParser
    {
        private BundleMod mod;

        private readonly NameIndex index;

        private readonly string scriptPath;

        private string currentLine;

        private int currentLineNumber;

        public ModScriptParser(string path, ref BundleMod mod, NameIndex fileIndex)
        {
            scriptPath = path;
            this.mod = mod;
            this.index = fileIndex;
            currentLineNumber = 1;
        }

        public bool Parse()
        {
            var reader = new StreamReader(new FileStream(scriptPath, FileMode.Open, FileAccess.Read));
            currentLine = reader.ReadLine();
            while (currentLine != null)
            {
                this.ParseLine();
                ++currentLineNumber;
                currentLine = reader.ReadLine();
            }
            reader.Close();
            return true;
        }

        private void Error(string message)
        {
            throw new Exception(String.Format("{0} Line: {1}", message, currentLineNumber));
        }

        private void CheckFile(string file, out ulong path, out ulong language, out ulong extension)
        {
            language = 0;
            path = 0;
            extension = 0;
            string[] pieces = file.Split('.');
            if (pieces.Length == 3)
            {
                path = Hash64.HashString(pieces[0]);
                extension = Hash64.HashString(pieces[2]);
                language = ulong.Parse(pieces[1]);
            }
            else if (pieces.Length == 2)
            {
                path = Hash64.HashString(pieces[0]);
                extension = Hash64.HashString(pieces[1]);
            }
            else
            {
                this.Error(
                    "Filename did not appear to be valid. It must be either path/to/file.ext or path/to/file.language.ext in format.");
            }
            if(index.Entry2Id(path, extension, 0).Count == 0)
                this.Error("Could not find the file in the file index. Please check the spelling and that you used / and not \\.");
        }

        private void ParseLine()
        {
            if (currentLine.Length <= 0) return;
            switch (currentLine[0])
            {
                case ';':
                    return;
                case '@':
                    this.ParseMetaLine();
                    break;
                default:
                    this.ParseFileLine();
                    break;
            }
        }

        private void ParseFileLine()
        {
            ulong path, extension, language = 0;
            string[] pieces = currentLine.Split(':');
            if(pieces.Length < 2)
                this.Error("File line didn't contain two pieces.");
            this.CheckFile(pieces[0].Trim(), out path, out language, out extension);
            if(!File.Exists(pieces[1].Trim()))
                this.Error("Replacement file does not exist.");
            var item = new BundleRewriteItem
                                         {
                                             BundleExtension = extension,
                                             BundleLanguage = (uint)language,
                                             BundlePath = path,
                                             IsLanguageSpecific = language != 0 ? true : false,
                                             ReplacementFile = pieces[1].Trim()
                                         };
            mod.ItemQueue.Add(item);
        }

        private void ParseMetaLine()
        {
            string[] pieces = currentLine.Split(' ');
            if(pieces.Length < 2)
                this.Error("Metadata line does not contain at least two pieces of information.");
            string remaining = currentLine.Substring(currentLine.IndexOf(' ')).Trim();
            switch (pieces[0])
            {
                case "@Author":
                    mod.Author = remaining;
                    break;
                case "@Name":
                    mod.Name = remaining;
                    break;
                case "@Description":
                    mod.Description = remaining.Replace("\\r\\n", System.Environment.NewLine).Replace("\\n", System.Environment.NewLine).Replace("\\r", System.Environment.NewLine);
                    break;
                default:
                    this.Error("Invalid metadata type.");
                    break;
            }
        }

        //Get mod name to use as filename
        public string getModName()
        {
            return mod.Name;
        }
    }
}
