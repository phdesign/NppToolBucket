using phdesign.NppToolBucket.Infrastructure;

namespace phdesign.NppToolBucket
{
    internal class GuidGeneratorSettings
    {
        #region Fields

        private readonly Settings _settings;

        #endregion

        #region Properties

        // Forces string to uppercase.
        public bool UseUppercase;
        // Include braces in front and end of each GUID.
        public bool IncludeBraces;
        // Inserts hyphens between each set.
        public bool IncludeHyphens;
        // How many GUIDs to generate, separated by a new line.
        public int HowMany;

        #endregion

        #region Constructor

        public GuidGeneratorSettings(Settings settings)
        {
            _settings = settings;
            Reload();
        }

        #endregion

        #region Public Methods

        public void Reload()
        {
            UseUppercase = _settings.GetBool(SettingsSection.Guids, "UseUppercase", false);
            IncludeBraces = _settings.GetBool(SettingsSection.Guids, "IncludeBraces", false);
            IncludeHyphens = _settings.GetBool(SettingsSection.Guids, "IncludeHyphens", true);
            HowMany = _settings.GetInt(SettingsSection.Guids, "HowMany", 1);
        }

        public void Save()
        {
            _settings.Set(SettingsSection.Guids, "UseUppercase", UseUppercase);
            _settings.Set(SettingsSection.Guids, "IncludeBraces", IncludeBraces);
            _settings.Set(SettingsSection.Guids, "IncludeHyphens", IncludeHyphens);
            _settings.Set(SettingsSection.Guids, "HowMany", HowMany);
            _settings.Save();
        }

        #endregion
    }
}