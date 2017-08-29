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
using System.Text;
using System.Windows.Forms;
using phdesign.NppToolBucket.Forms;
using phdesign.NppToolBucket.Infrastructure;
using phdesign.NppToolBucket.PluginCore;

namespace phdesign.NppToolBucket
{
    internal class GuidGenerator
    {
        #region Fields

        private readonly Editor _editor;
        /// <summary>
        /// The single reused instance of the dialog.
        /// </summary>
        private readonly GuidGeneratorForm _dialog;
        /// <summary>
        /// By attaching to the correct Notepad++ window, if user switching applications, the dialog hides with Notepad++.
        /// </summary>
        private readonly IWin32Window _owner;
        /// <summary>
        /// This class is a singleton.
        /// </summary>
        private static GuidGenerator _instance;

        #endregion

        #region Properties

        internal static GuidGeneratorSettings Settings;

        #endregion

        #region Constructor

        private GuidGenerator()
        {
            _editor = Editor.GetActive();
            _dialog = new GuidGeneratorForm
            {
                IncludeBraces = Settings.IncludeBraces,
                UseUppercase = Settings.UseUppercase,
                IncludeHyphens = Settings.IncludeHyphens,
                HowMany = Settings.HowMany
            };
            _owner = new WindowWrapper(PluginBase.nppData._nppHandle);
        }

        #endregion

        #region Public Static Methods

        internal static void Show()
        {
            if (_instance == null)
                _instance = new GuidGenerator();
            _instance.ShowDialog();
        }

        #endregion

        #region Private Methods

        private void ShowDialog()
        {
            if (_dialog.Visible) return;

            var result = _dialog.ShowDialog(_owner);
            if (result != DialogResult.OK) return;

            Settings.IncludeBraces = _dialog.IncludeBraces;
            Settings.UseUppercase = _dialog.UseUppercase;
            Settings.IncludeHyphens = _dialog.IncludeHyphens;
            Settings.HowMany = _dialog.HowMany;
            var guids = GetGuids();
            _editor.SetSelectedText(guids);
        }

        /// <summary>
        /// Generates a string of GUID(s).
        /// </summary>
        /// <returns>A string of GUID(s).</returns>
        private string GetGuids()
        {
            var result = new StringBuilder();
            for (var i = 0; i < Settings.HowMany; i++)
            {
                if (i > 0)
                    result.AppendLine();
                if (Settings.IncludeBraces)
                    result.Append("{");
                var guid = Guid.NewGuid().ToString();
                if (Settings.UseUppercase)
                    guid = guid.ToUpper();
                result.Append(guid);
                if (Settings.IncludeBraces)
                    result.Append("}");
            }
            if (!Settings.IncludeHyphens)
                result.Replace("-", "");

            return result.ToString();
        }

        #endregion
    }
}