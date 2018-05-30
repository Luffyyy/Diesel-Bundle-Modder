using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PDBundleModPatcher
{
    class DieselString
    {
        #region Fields

        /// <summary>
        ///     The affected bundlepath.
        /// </summary>
        public string bundlepath { get; set; }

        /// <summary>
        ///     The strings table.
        /// </summary>
        public HashSet<DieselStringEntry> table = new HashSet<DieselStringEntry>();


        #endregion


        #region Public Methods and Operators

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="inStream">
        /// The in_stream.
        /// </param>
        /// <returns>
        /// The <see cref="DieselStringEntry"/>.
        /// </returns>
        public static DieselString Deserialize(Stream inStream)
        {
            var reader = new StreamReader(inStream);
            return JsonConvert.DeserializeObject<DieselString>(reader.ReadToEnd());
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="inString">
        /// The in_string.
        /// </param>
        /// <returns>
        /// The <see cref="DieselString"/>.
        /// </returns>
        public static DieselString Deserialize(string inString)
        {
            return JsonConvert.DeserializeObject<DieselString>(inString);
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="stringEntry">
        /// The mod.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Serialize(DieselString stringEntry)
        {
            return JsonConvert.SerializeObject(stringEntry);
        }

        /*
        public static bool operator ==(BundleString host, BundleString entry2)
        {
            if (
                    host.Key.Equals(entry2.Key) &&
                    host.Value.Equals(entry2.Value)
                )
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(BundleString host, BundleString entry2)
        {
            if (host == entry2)
                return false;
            else
                return true;
        }
        */
        #endregion

    }
}
