using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace phdesign.NppToolBucket.Infrastructure
{
    internal enum SettingsSection
    {
        Global,
        FindAndReplace
    }

    internal class Settings
    {
        [DllImport("kernel32")]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        private readonly string _iniFilePath;
        private Dictionary<string, Dictionary<string, Setting>> _allSettings;

        public Settings(string filePath)
        {
            _iniFilePath = filePath;
            if (!File.Exists(filePath))
            {
                var iniFileDirectory = Path.GetDirectoryName(filePath);
                if (iniFileDirectory != null && !Directory.Exists(iniFileDirectory))
                    Directory.CreateDirectory(iniFileDirectory);
                using (var writer = File.CreateText(filePath))
                    writer.Write(
                        @"; {0} plugin configuration file.
; Please restart Notepad++ after modifying this file for changes to take effect.
; ShowTabBarIcons: If true, displays icons in the tab bar for some of the main plugin operations
[Global]
ShowTabBarIcons=True",
                        Main.PluginName);
            }
            Load();
        }

        public string Get(SettingsSection section, string keyName, string defaultValue)
        {
            var setting = GetSetting(section.ToString(), keyName);
            return setting == null ? defaultValue : setting.Value ?? defaultValue;
        }

        public int GetInt(SettingsSection section, string keyName, int defaultValue)
        {
            var setting = GetSetting(section.ToString(), keyName);
            if (setting == null) return defaultValue;
            int value;
            return int.TryParse(setting.Value, out value) ? value : defaultValue;
        }

        public bool GetBool(SettingsSection section, string keyName, bool defaultValue)
        {
            var setting = GetSetting(section.ToString(), keyName);
            if (setting == null || string.IsNullOrEmpty(setting.Value)) 
                return defaultValue;
            if (setting.Value.Equals("true", StringComparison.CurrentCultureIgnoreCase) || setting.Value == "1")
                return true;
            if (setting.Value.Equals("false", StringComparison.CurrentCultureIgnoreCase) || setting.Value == "0")
                return false;

            return defaultValue;
        }

        public void Set(SettingsSection section, string keyName, string value)
        {
            Dictionary<string, Setting> sectionSettings;
            if (!_allSettings.TryGetValue(section.ToString(), out sectionSettings))
            {
                sectionSettings = new Dictionary<string, Setting>();
                _allSettings.Add(section.ToString(), sectionSettings);
            }
            Setting setting;
            if (!sectionSettings.TryGetValue(keyName, out setting))
            {
                sectionSettings.Add(
                    keyName, new Setting(value) {IsDirty = true});
            }
            else if (setting.Value != value)
            {
                setting.Value = value;
                setting.IsDirty = true;
            }
        }

        public void Set(SettingsSection section, string keyName, int value)
        {
            Set(section, keyName, value.ToString());
        }

        public void Set(SettingsSection section, string keyName, bool value)
        {
            Set(section, keyName, value ? "True" : "False");
        }

        public void Save()
        {
            foreach (var section in _allSettings)
            {
                foreach (var setting in section.Value)
                {
                    if (setting.Value.IsDirty)
                        WritePrivateProfileString(section.Key, setting.Key, setting.Value.Value, _iniFilePath);
                }
            }
        }

        private void Load()
        {
            _allSettings = new Dictionary<string, Dictionary<string, Setting>>();
            foreach (var sectionName in GetSections())
            {
                var section = new Dictionary<string, Setting>();
                foreach (var keyName in GetKeys(sectionName))
                {
                    section.Add(keyName, new Setting(GetValue(sectionName, keyName)));
                }
                _allSettings.Add(sectionName, section);
            }
        }

        private List<string> GetSections()
        {
            return new List<string>(GetValue(null, null).Split('\0'));
        }   

        private List<string> GetKeys(string sectionName)
        {
            return new List<string>(GetValue(sectionName, null).Split('\0'));
        }

        private string GetValue(string sectionName, string keyName)
        {
            const uint bufSize = 32767;
            var truncateLastByte = (sectionName == null || keyName == null) ? 1 : 0;
            var buffer = new char[bufSize];
            uint bytesReturned = GetPrivateProfileString(sectionName, keyName, null, buffer, bufSize, _iniFilePath);
            if (bytesReturned == 0)
                return string.Empty;

            return new string(buffer, 0, (int)bytesReturned - truncateLastByte);
        }

        //private string GetValue(string sectionName, string keyName)
        //{
        //    const int minBufSize = 255;
        //    int bufSize, retVal;
        //    var attempts = 0;
        //    var bufSizeTooSmall = (sectionName == null || keyName == null) ? 2 : 1;
        //    var value = new StringBuilder(minBufSize);

        //    do
        //    {
        //        bufSize = minBufSize * ++attempts;
        //        value.Capacity = bufSize;
        //        // http://msdn.microsoft.com/en-us/library/ms724353(v=vs.85).aspx
        //        // If return value is buffer size - 1 then buffer is too small.
        //        retVal = Win32.GetPrivateProfileString(sectionName, keyName, null, value, bufSize, _iniFilePath);

        //    } while (retVal == bufSize - bufSizeTooSmall && bufSize + minBufSize < value.MaxCapacity);

        //    return value.ToString();
        //}

        private Setting GetSetting(string sectionName, string keyName)
        {
            Dictionary<string, Setting> sectionSettings;
            if (!_allSettings.TryGetValue(sectionName, out sectionSettings))
                return null;
            Setting setting;
            return !sectionSettings.TryGetValue(keyName, out setting) ? null : setting;
        }

        private class Setting
        {
            public string Value;
            public bool IsDirty;

            public Setting(string value)
            {
                Value = value;
            }
        }
    }
}