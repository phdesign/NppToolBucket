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
using phdesign.NppToolBucket.PluginCore;
using phdesign.NppToolBucket.Utilities.Security;

namespace phdesign.NppToolBucket
{
    internal class Helpers
    {
        private const string LoremIpsum =
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        internal static void ComputeSHA1Hash()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            if (string.IsNullOrEmpty(text)) return;

            var hash = SHA1.ComputeHash(text);
            editor.SetSelectedText(hash);
        }

        internal static void ComputeMD5Hash()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            if (string.IsNullOrEmpty(text)) return;

            var hash = MD5.ComputeHash(text);
            editor.SetSelectedText(hash);
        }

        internal static void GenerateLoremIpsum()
        {
            Editor.GetActive().SetSelectedText(LoremIpsum);
        }

        internal static void Base64Encode()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            if (string.IsNullOrEmpty(text)) return;

            var bytes = Encoding.UTF8.GetBytes(text);
            var result = Convert.ToBase64String(bytes);
            editor.SetSelectedText(result);
        }

        internal static void Base64Decode()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            if (string.IsNullOrEmpty(text)) return;

            var bytes = Convert.FromBase64String(text);
            var result = Encoding.UTF8.GetString(bytes);
            editor.SetSelectedText(result);
        }

        internal static void ClearFindAllInAllDocuments()
        {
            // Get current doc index
            int originalView;
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTSCINTILLA, 0, out originalView);

            // Do the other view first so we leave the user back on the original view they started
            var otherView = originalView == (int)NppMsg.MAIN_VIEW ? (int)NppMsg.SUB_VIEW : (int)NppMsg.MAIN_VIEW;
            ClearFindAllInView(otherView);
            ClearFindAllInView(originalView);
        }

        private static void ClearFindAllInView(int currentView)
        {
            var originalDocument = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTDOCINDEX, 0, currentView);
            // Get number of docs
            var docCount = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETNBOPENFILES, 0, currentView + 1);

            // Loop through all docs
            for (int i = 0; i < docCount; i++)
            {
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, currentView, i);
                ClearFindAll();
            }
            // Restore original doc
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, currentView, originalDocument);
        }

        private static void ClearFindAll()
        {
            var editor = Editor.GetActive();
            editor.RemoveFindMarks();
            editor.RemoveAllBookmarks();
        }
    }
}