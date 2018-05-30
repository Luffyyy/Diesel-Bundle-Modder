using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDBundleModPatcher
{
    public class BundleEntryPath
    {
        /// <summary>
        /// The bundle extension.
        /// </summary>
        public ulong EntryExtension;

        /// <summary>
        /// The bundle language.
        /// </summary>
        public uint EntryLanguage;

        /// <summary>
        /// The bundle path.
        /// </summary>
        public ulong EntryPath;

        /// <summary>
        /// The is language specific.
        /// </summary>
        public bool IsLanguageSpecific;


        public static bool operator==(BundleEntryPath host, BundleEntryPath guest)
        {
            if (host.EntryExtension == guest.EntryExtension &&
                host.EntryLanguage == guest.EntryLanguage &&
                host.EntryPath == guest.EntryPath &&
                host.IsLanguageSpecific == guest.IsLanguageSpecific
                )
                return true;
            return false;
        }

        public static bool operator !=(BundleEntryPath host, BundleEntryPath guest)
        {
            return !(host == guest);
        }

        public bool Equals(BundleEntryPath guest)
        {
            if (ReferenceEquals(guest, null))
                return false;

            if(ReferenceEquals(this, guest))
                return true;

            return this == guest;
        }

        public override bool Equals(object guest)
        {
            if (ReferenceEquals(guest, null))
                return false;

            if (ReferenceEquals(this, guest))
                return true;

            if (guest.GetType() != this.GetType()) return false;

            return Equals((BundleEntryPath)guest);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash = (hash * 7) + EntryExtension.GetHashCode();
            hash = (hash * 7) + EntryLanguage.GetHashCode();
            hash = (hash * 7) + EntryPath.GetHashCode();
            hash = (hash * 7) + IsLanguageSpecific.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            if (this.EntryPath == null || this.EntryLanguage == null || this.EntryExtension == null)
                return "";

            StringBuilder path = new StringBuilder();

            string path_string = StaticStorage.Known_Index.GetPath(this.EntryPath);
            string language_string = (this.EntryLanguage != 0 ? this.EntryLanguage.ToString() : "");
            string extension_string = StaticStorage.Known_Index.GetExtension(this.EntryExtension);

            if (path_string == null || language_string == null || extension_string == null)
                return "";

            if (!String.IsNullOrWhiteSpace(path_string))
                path.Append(path_string);

            if (!String.IsNullOrWhiteSpace(language_string))
                path.Append("." + path_string);

            if (!String.IsNullOrWhiteSpace(extension_string))
                path.Append("." + extension_string);

            return path.ToString();
        }
    }
}
