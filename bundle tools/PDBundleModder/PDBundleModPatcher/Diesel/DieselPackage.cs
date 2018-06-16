using DieselEngineFormats.Bundle;
using DieselEngineFormats.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDBundleModPatcher.Diesel
{
    class DieselPackage
    {
        List<PackageFileEntry> entries = new List<PackageFileEntry>();


        public DieselPackage()
        {

        }

        public DieselPackage(List<PackageFileEntry> entries)
        {
            this.entries = entries;
        }

        public void writePackage(String name)
        {
            UInt64 hashed_name = Hash64.HashString(name);
        }
    }
}
