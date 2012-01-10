using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Jayrock.Json.Conversion;
using phdesign.NppToolBucket.Infrastructure;
using phdesign.NppToolBucket.PluginCore;
using phdesign.NppToolBucket.Utilities;

namespace phdesign.NppToolBucket
{
    class Main : PluginBase
    {
        private enum CmdIndex
        {
            IndentationSettings = 0,
            FindAndReplace,
            Seperator1,
            GenerateGuid,
            GenerateLoremIpsum,
            ComputeMD5Hash,
            ComputeSHA1Hash,
            Base64Encode,
            Base64Decode,
            Seperator2,
            OpenConfigFile,
            About,
        }

        #region Fields

        internal const string PluginName = "ToolBucket";
        internal const string PluginShortName = "NppToolBucket";

        private static string _iniFilePath;
        private static Settings _settings;
        private static bool _showTabBarIcons;

        #endregion

        #region Properties

        private static string IniFilePath
        {
            get
            {
                if (_iniFilePath == null)
                {
                    var iniFilePathBuilder = new StringBuilder(Win32.MAX_PATH);
                    Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, iniFilePathBuilder);
                    _iniFilePath = iniFilePathBuilder.ToString();
                    _iniFilePath = Path.Combine(_iniFilePath, PluginShortName + ".ini");
                }
                return _iniFilePath;
            }
        }

        #endregion

        #region StartUp/CleanUp

        internal static void CommandMenuInit()
        {
            _settings = new Settings(IniFilePath);
            _showTabBarIcons = _settings.GetBool(SettingsSection.Global, "ShowTabBarIcons", true);
            FindAndReplace.MatchCase = _settings.GetBool(SettingsSection.FindAndReplace, "MatchCase", false);
            FindAndReplace.MatchWholeWord = _settings.GetBool(SettingsSection.FindAndReplace, "MatchWholeWord", false);
            FindAndReplace.SearchBackwards = _settings.GetBool(SettingsSection.FindAndReplace, "SearchBackwards", false);
            FindAndReplace.SearchFromBegining = _settings.GetBool(SettingsSection.FindAndReplace, "SearchFromBegining", false);
            FindAndReplace.UseRegularExpression = _settings.GetBool(SettingsSection.FindAndReplace, "UseRegularExpression", false);
            var findHistory = _settings.Get(SettingsSection.FindAndReplace, "FindHistory", null);
            if (!string.IsNullOrEmpty(findHistory))
                FindAndReplace.FindHistory = new List<string>(Deserialise(findHistory));
            var replaceHistory = _settings.Get(SettingsSection.FindAndReplace, "ReplaceHistory", null);
            if (!string.IsNullOrEmpty(replaceHistory))
                FindAndReplace.ReplaceHistory = new List<string>(Deserialise(replaceHistory));

            SetCommand((int)CmdIndex.IndentationSettings, "Change indentation settings", IndentationSettings.Show);
            SetCommand((int)CmdIndex.FindAndReplace, "Multiline find and replace", FindAndReplace.Show, new ShortcutKey(false, true, true, Keys.F));
            SetCommand((int)CmdIndex.Seperator1, "---", null);
            SetCommand((int)CmdIndex.GenerateGuid, "Generate GUID", Helpers.GenerateGuid);
            SetCommand((int)CmdIndex.GenerateLoremIpsum, "Generate Lorem Ipsum", Helpers.GenerateLoremIpsum);
            SetCommand((int)CmdIndex.ComputeMD5Hash, "Compute MD5 hash", Helpers.ComputeMD5Hash);
            SetCommand((int)CmdIndex.ComputeSHA1Hash, "Compute SHA1 hash", Helpers.ComputeSHA1Hash);
            SetCommand((int)CmdIndex.Base64Encode, "Base 64 encode", Helpers.Base64Encode);
            SetCommand((int)CmdIndex.Base64Decode, "Base 64 decode", Helpers.Base64Decode);
            SetCommand((int)CmdIndex.Seperator2, "---", null);
            SetCommand((int)CmdIndex.OpenConfigFile, "Open config file", OpenConfigFile);
            SetCommand((int)CmdIndex.About, "About", About);
        }

        internal static void SetToolBarIcon()
        {
            if (!_showTabBarIcons) return;

            var toolbarIconEditIndent = Properties.Resources.edit_indent;
            var tbIcons = new toolbarIcons
            {
                hToolbarBmp = toolbarIconEditIndent.GetHbitmap()
            };
            var pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(
                nppData._nppHandle,
                NppMsg.NPPM_ADDTOOLBARICON,
                _funcItems.Items[(int)CmdIndex.IndentationSettings]._cmdID,
                pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);

            var toolbarIconBinocularPencil = Properties.Resources.binocular_pencil;
            tbIcons = new toolbarIcons
            {
                hToolbarBmp = toolbarIconBinocularPencil.GetHbitmap()
            };
            pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(
                nppData._nppHandle,
                NppMsg.NPPM_ADDTOOLBARICON,
                _funcItems.Items[(int)CmdIndex.FindAndReplace]._cmdID,
                pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }

        internal static void PluginCleanUp()
        {
            _settings.Set(SettingsSection.Global, "Version", AssemblyUtils.Version);
            //_settings.Set(SettingsSection.Global, "ShowTabBarIcons", _showTabBarIcons);
            _settings.Set(SettingsSection.FindAndReplace, "MatchCase", FindAndReplace.MatchCase);
            _settings.Set(SettingsSection.FindAndReplace, "MatchWholeWord", FindAndReplace.MatchWholeWord);
            _settings.Set(SettingsSection.FindAndReplace, "SearchBackwards", FindAndReplace.SearchBackwards);
            _settings.Set(SettingsSection.FindAndReplace, "SearchFromBegining", FindAndReplace.SearchFromBegining);
            _settings.Set(SettingsSection.FindAndReplace, "UseRegularExpression", FindAndReplace.UseRegularExpression);
            if (FindAndReplace.FindHistory != null && FindAndReplace.FindHistory.Count > 0)
                _settings.Set(SettingsSection.FindAndReplace, "FindHistory", Serialise(FindAndReplace.FindHistory.ToArray()));
            if (FindAndReplace.ReplaceHistory != null && FindAndReplace.ReplaceHistory.Count > 0)
                _settings.Set(SettingsSection.FindAndReplace, "ReplaceHistory", Serialise(FindAndReplace.ReplaceHistory.ToArray()));
            _settings.Save();
        }

        #endregion

        #region Menu Functions

        internal static void OpenConfigFile()
        {
            Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, IniFilePath);
        }

        internal static void About()
        {
            MessageBox.Show(
                string.Format("{0}\r\nv{1}\r\n\r\nBy Paul Heasley\r\nwww.phdesign.com.au", PluginName, AssemblyUtils.Version),
                string.Format("{0} Plugin", PluginName),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Serialises a string array to a string using Jayrock JSON parser.
        /// http://msdn.microsoft.com/en-us/library/bb299886.aspx#intro_to_json_topic5
        /// http://jayrock.berlios.de/
        /// </summary>
        /// <param name="array">The array to serialise.</param>
        /// <returns>JSON representation of array.</returns>
        private static string Serialise(string[] array)
        {
            return JsonConvert.ExportToString(array);
        }

        /// <summary>
        /// Deserialises a JSON string to a string array using Jayrock JSON parser.
        /// http://msdn.microsoft.com/en-us/library/bb299886.aspx#intro_to_json_topic5
        /// http://jayrock.berlios.de/
        /// </summary>
        /// <param name="s">The JSON representation of the array.</param>
        /// <returns>Array of string.</returns>
        private static string[] Deserialise(string s)
        {
            return JsonConvert.Import<string[]>(s);
        }

        #endregion
    }
}