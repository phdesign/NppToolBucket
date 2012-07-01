/*
 * Copyright 2011-2012 Paul Heasley
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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