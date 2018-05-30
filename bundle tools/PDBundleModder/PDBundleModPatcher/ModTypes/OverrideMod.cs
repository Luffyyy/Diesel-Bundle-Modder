namespace PDBundleModPatcher
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;

    public class OverrideMod
    {
        #region Fields

        /// <summary>
        ///     The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     The description.
        /// </summary>
        public string Description { get; set; }


        #endregion

        #region Public Methods and Operators

        public OverrideMod()
        {

        }

        public OverrideMod(string name, string author = "", string description = "")
        {
            this.Name = name;
            this.Author = author;
            this.Description = description;
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="inStream">
        /// The in_stream.
        /// </param>
        /// <returns>
        /// The <see cref="OverrideMod"/>.
        /// </returns>
        public static OverrideMod Deserialize(Stream inStream)
        {
            var reader = new StreamReader(inStream);
            return JsonConvert.DeserializeObject<OverrideMod>(reader.ReadToEnd());
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="inString">
        /// The in_string.
        /// </param>
        /// <returns>
        /// The <see cref="OverrideMod"/>.
        /// </returns>
        public static OverrideMod Deserialize(string inString)
        {
            return JsonConvert.DeserializeObject<OverrideMod>(inString);
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="mod">
        /// The mod.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Serialize(OverrideMod mod)
        {
            return JsonConvert.SerializeObject(mod, Formatting.Indented);
        }

        #endregion
    }
}