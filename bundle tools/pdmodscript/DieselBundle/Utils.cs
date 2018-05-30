// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utils.cs" company="">
//   
// </copyright>
// <summary>
//   Utils.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DieselBundle.Utils
{
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// The hash 64.
    /// </summary>
    public class Hash64
    {
        #region Public Methods and Operators

        /// <summary>
        /// The hash.
        /// </summary>
        /// <param name="k">
        /// The k.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <returns>
        /// The <see cref="ulong"/>.
        /// </returns>
        [DllImport("hash64.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong Hash(byte[] k, ulong length, ulong level);

        /// <summary>
        /// The hash string.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <returns>
        /// The <see cref="ulong"/>.
        /// </returns>
        public static ulong HashString(string input, ulong level = 0)
        {
            return Hash(Encoding.UTF8.GetBytes(input), (ulong)Encoding.UTF8.GetByteCount(input), level);
        }

        #endregion
    }
}