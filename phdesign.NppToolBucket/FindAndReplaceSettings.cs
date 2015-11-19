using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Jayrock.Json.Conversion;
using phdesign.NppToolBucket.Infrastructure;
using phdesign.NppToolBucket.Utilities.Enum;

namespace phdesign.NppToolBucket
{
    public enum SearchInOptions
    {
        [Description("Selected Text")]
        SelectedText = 0,
        [Description("Current Document")]
        CurrentDocument,
        [Description("All Open Documents")]
        OpenDocuments
    }

    internal class FindAndReplaceSettings
    {
        #region Fields

        private readonly Settings _settings;

        #endregion

        #region Properties

        public List<string> FindHistory;
        public List<string> ReplaceHistory;
        public bool MatchCase;
        public bool MatchWholeWord;
        public bool UseRegularExpression;
        public bool SearchFromBegining;
        public bool SearchBackwards;
        public SearchInOptions SearchIn;
        public Size WindowSize;

        #endregion

        #region Constructor

        public FindAndReplaceSettings(Settings settings)
        {
            _settings = settings;
            Reload();
        }

        #endregion

        #region Public Methods

        public void Reload()
        {
            MatchCase = _settings.GetBool(SettingsSection.FindAndReplace, "MatchCase", false);
            MatchWholeWord = _settings.GetBool(SettingsSection.FindAndReplace, "MatchWholeWord", false);
            SearchBackwards = _settings.GetBool(SettingsSection.FindAndReplace, "SearchBackwards", false);
            SearchFromBegining = _settings.GetBool(SettingsSection.FindAndReplace, "SearchFromBegining", false);
            UseRegularExpression = _settings.GetBool(SettingsSection.FindAndReplace, "UseRegularExpression", false);
            var searchIn = _settings.Get(SettingsSection.FindAndReplace, "SearchIn", null);
            SearchIn = EnumUtils.ParseEnum(searchIn, true, SearchInOptions.CurrentDocument);

            var findHistoryString = _settings.Get(SettingsSection.FindAndReplace, "FindHistory", null);
            FindHistory = DeserialiseList(findHistoryString);
            var replaceHistoryString = _settings.Get(SettingsSection.FindAndReplace, "ReplaceHistory", null);
            ReplaceHistory = DeserialiseList(replaceHistoryString);

            var windowSizeString = _settings.Get(SettingsSection.FindAndReplace, "WindowSize", null);
            WindowSize = DeserialiseSize(windowSizeString);
        }

        public void Save()
        {
            _settings.Set(SettingsSection.FindAndReplace, "MatchCase", MatchCase);
            _settings.Set(SettingsSection.FindAndReplace, "MatchWholeWord", MatchWholeWord);
            _settings.Set(SettingsSection.FindAndReplace, "SearchBackwards", SearchBackwards);
            _settings.Set(SettingsSection.FindAndReplace, "SearchFromBegining", SearchFromBegining);
            _settings.Set(SettingsSection.FindAndReplace, "UseRegularExpression", UseRegularExpression);
            _settings.Set(SettingsSection.FindAndReplace, "SearchIn", SearchIn.ToString());

            var findHistoryString = SerialiseList(FindHistory);
            _settings.Set(SettingsSection.FindAndReplace, "FindHistory", findHistoryString);
            var replaceHistoryString = SerialiseList(ReplaceHistory);
            _settings.Set(SettingsSection.FindAndReplace, "ReplaceHistory", replaceHistoryString);

            var windowSizeString = SerialiseSize(WindowSize);
            _settings.Set(SettingsSection.FindAndReplace, "WindowSize", windowSizeString);

            _settings.Save();
        }

        #endregion

        #region Private Methods

        private static string SerialiseSize(Size size)
        {
            return size.IsEmpty ? "" : string.Format("{0},{1}", size.Width, size.Height);
        }

        private static Size DeserialiseSize(string s)
        {
            try
            {
                var parts = s.Split(new[] { ',' });
                if (parts.Length == 2)
                {
                    int width;
                    int height;
                    if (int.TryParse(parts[0], out width) && int.TryParse(parts[1], out height))
                        return new Size(width, height);
                }
            }
            catch (Exception)
            {
            }
            return new Size(490, 204);
        }

        private static string SerialiseList(List<string> list)
        {
            if (list == null)
                return "";

            try
            {
                return JsonConvert.ExportToString(list.ToArray());
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static List<string> DeserialiseList(string s)
        {
            if (string.IsNullOrEmpty(s))
                return new List<string>();

            try
            {
                var items = JsonConvert.Import<string[]>(s);
                return new List<string>(items);
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        #endregion
    }
}
