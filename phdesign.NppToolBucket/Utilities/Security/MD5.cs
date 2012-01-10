using System;
using System.IO;
using System.Text;

namespace phdesign.NppToolBucket.Utilities.Security
{
    public class MD5
    {
        /// <summary>
        /// Generate MD5 checksum for a stream. Rewinds stream.
        /// </summary>
        /// <param name="inputStream">The stream to create checksum of.</param>
        /// <returns>An MD5 checksum of stream.</returns>
        public static string ComputeHash(Stream inputStream)
        {
            var hash = BitConverter.ToString(
                System.Security.Cryptography.MD5.Create().ComputeHash(inputStream)).Replace("-", "");
            if (inputStream.CanSeek)
                inputStream.Seek(0, SeekOrigin.Begin);
            return hash;
        }

        /// <summary>
        /// Generate MD5 checksum for a string.
        /// </summary>
        /// <param name="input">The string to create checksum of.</param>
        /// <returns>An MD5 checksum of the string.</returns>
        public static string ComputeHash(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException("input");
            var bytes = Encoding.Default.GetBytes(input);
            return BitConverter.ToString(System.Security.Cryptography.MD5.Create().ComputeHash(bytes)).Replace("-", "");
        } 
    }
}