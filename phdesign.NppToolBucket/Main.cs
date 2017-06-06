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

using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using phdesign.NppToolBucket.Infrastructure;
using phdesign.NppToolBucket.PluginInfrastructure;
using phdesign.NppToolBucket.Utilities;

namespace phdesign.NppToolBucket
{
    class Main
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
                    Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, iniFilePathBuilder);
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
            FindAndReplace.Settings = new FindAndReplaceSettings(_settings);

            PluginBase.SetCommand((int)CmdIndex.IndentationSettings, "Change indentation settings", IndentationSettings.Show, new ShortcutKey(false, true, true, Keys.I));
            PluginBase.SetCommand((int)CmdIndex.FindAndReplace, "Multiline find and replace", FindAndReplace.Show, new ShortcutKey(false, true, true, Keys.F));
            PluginBase.SetCommand((int)CmdIndex.Seperator1, "---", null);
            PluginBase.SetCommand((int)CmdIndex.GenerateGuid, "Generate GUID", GuidGenerator.Show, new ShortcutKey(false, true, true, Keys.G));
            PluginBase.SetCommand((int)CmdIndex.GenerateLoremIpsum, "Generate Lorem Ipsum", Helpers.GenerateLoremIpsum);
            PluginBase.SetCommand((int)CmdIndex.ComputeMD5Hash, "Compute MD5 hash", Helpers.ComputeMD5Hash);
            PluginBase.SetCommand((int)CmdIndex.ComputeSHA1Hash, "Compute SHA1 hash", Helpers.ComputeSHA1Hash);
            PluginBase.SetCommand((int)CmdIndex.Base64Encode, "Base 64 encode", Helpers.Base64Encode);
            PluginBase.SetCommand((int)CmdIndex.Base64Decode, "Base 64 decode", Helpers.Base64Decode);
            PluginBase.SetCommand((int)CmdIndex.Seperator2, "---", null);
            PluginBase.SetCommand((int)CmdIndex.OpenConfigFile, "Open config file", OpenConfigFile);
            PluginBase.SetCommand((int)CmdIndex.About, "About", About);
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
                PluginBase.nppData._nppHandle,
                (uint)NppMsg.NPPM_ADDTOOLBARICON,
                PluginBase._funcItems.Items[(int)CmdIndex.IndentationSettings]._cmdID,
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
                PluginBase.nppData._nppHandle,
                (uint)NppMsg.NPPM_ADDTOOLBARICON,
                PluginBase._funcItems.Items[(int)CmdIndex.FindAndReplace]._cmdID,
                pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }

        internal static void PluginCleanUp()
        {
            _settings.Set(SettingsSection.Global, "Version", AssemblyUtils.Version);
            //_settings.Set(SettingsSection.Global, "ShowTabBarIcons", _showTabBarIcons);
            FindAndReplace.Settings.Save();
        }

        #endregion

        #region Menu Functions

        internal static void OpenConfigFile()
        {
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_DOOPEN, 0, IniFilePath);
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
    }
}