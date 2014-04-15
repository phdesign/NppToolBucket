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

        #region Constructor

        private GuidGenerator()
        {
            _editor = Editor.GetActive();
            _dialog = new GuidGeneratorForm();
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

            var guids = GetGuids(_dialog.IncludeBraces, _dialog.UseUppercase, _dialog.IncludeHyphens, _dialog.HowMany);
            _editor.SetSelectedText(guids);
        }

        /// <summary>
        /// Generates a string of GUID(s).
        /// </summary>
        /// <param name="includeBraces">Include braces in front and end of each GUID.</param>
        /// <param name="useUppercase">Forces string to uppercase.</param>
        /// <param name="includeHyphens">Inserts hyphens between each set.</param>
        /// <param name="howMany">How many GUIDs to generate, seperated my a new line.</param>
        /// <returns>A string of GUID(s).</returns>
        private string GetGuids(bool includeBraces, bool useUppercase, bool includeHyphens, int howMany)
        {
            var result = new StringBuilder();
            for (var i = 0; i < howMany; i++)
            {
                if (includeBraces)
                    result.Append("{");
                var guid = Guid.NewGuid().ToString();
                if (useUppercase)
                    guid = guid.ToUpper();
                result.Append(guid);
                if (includeBraces)
                    result.Append("}");
                result.AppendLine();
            }
            if (!includeHyphens)
                result.Replace("-", "");

            return result.ToString();
        }

        #endregion
    }
}