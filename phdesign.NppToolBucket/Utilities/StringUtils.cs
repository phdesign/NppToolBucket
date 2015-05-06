using System;
using System.Text;

namespace phdesign.NppToolBucket.Utilities
{
    public static class StringUtils
    {
        /// <summary>
        /// Converts and ANSI string to Unicode.
        /// </summary>
        public static string AnsiToUnicode(string str)
        {
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(str));
        }

        /// <summary>
        /// Converts a Unicode string to ANSI
        /// </summary>
        public static string UnicodeToAnsi(string str)
        {
            return Encoding.Default.GetString(Encoding.UTF8.GetBytes(str));
        }
    }
}