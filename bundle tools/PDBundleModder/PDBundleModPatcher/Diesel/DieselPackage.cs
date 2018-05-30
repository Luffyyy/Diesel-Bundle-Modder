using DieselBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDBundleModPatcher.Diesel
{
    class DieselPackage
    {
        List<BundleEntry> entries = new List<BundleEntry>();


        public DieselPackage()
        {

        }

        public DieselPackage(List<BundleEntry> entries)
        {
            this.entries = entries;
        }

        public void writePackage(String name)
        {
            UInt64 hashed_name = DieselBundle.Utils.Hash64.HashString(name);



        }
    }
}
