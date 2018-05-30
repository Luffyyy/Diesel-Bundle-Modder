using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PDBundleModPatcher
{
    class DieselStringEntry
    {

        #region Fields

        /// <summary>
        ///     The string key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     The string value.
        /// </summary>
        public string Value { get; set; }

        #endregion

        public DieselStringEntry(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

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
        public static DieselStringEntry Deserialize(Stream inStream)
        {
            var reader = new StreamReader(inStream);
            return JsonConvert.DeserializeObject<DieselStringEntry>(reader.ReadToEnd());
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="inString">
        /// The in_string.
        /// </param>
        /// <returns>
        /// The <see cref="DieselStringEntry"/>.
        /// </returns>
        public static DieselStringEntry Deserialize(string inString)
        {
            return JsonConvert.DeserializeObject<DieselStringEntry>(inString);
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
        public static string Serialize(DieselStringEntry stringEntry)
        {
            return JsonConvert.SerializeObject(stringEntry);
        }


        public static bool operator ==(DieselStringEntry host, DieselStringEntry entry2)
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

        public static bool operator !=(DieselStringEntry host, DieselStringEntry entry2)
        {
            if (host == entry2)
                return false;
            else
                return true;
        }

        #endregion

    }
}
