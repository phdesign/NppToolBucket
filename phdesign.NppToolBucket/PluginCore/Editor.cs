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

namespace phdesign.NppToolBucket.PluginCore
{
    internal class Editor : PluginBase
    {
        #region Constants

        private const int IndicatorMatch = 31;
        private const int BookmarkMarker = 24;

        #endregion

        #region Fields
        
        private readonly IntPtr _activeScintilla;

        #endregion

        #region Constructor

        private Editor(IntPtr activeScintilla)
        {
            _activeScintilla = activeScintilla;
        }

        #endregion

        #region Public Static Accessor Method

        public static Editor GetActive()
        {
            return new Editor(GetCurrentScintilla());
        }

        #endregion

        #region Public Methods

        #region  Call Methods

        public int Call(SciMsg msg, int wParam, IntPtr lParam)
        {
            return (int)Win32.SendMessage(_activeScintilla, msg, wParam, lParam);
        }

        public int Call(SciMsg msg, int wParam, string lParam)
        {
            return (int)Win32.SendMessage(_activeScintilla, msg, wParam, lParam);
        }

        public int Call(SciMsg msg, int wParam, StringBuilder lParam)
        {
            return (int)Win32.SendMessage(_activeScintilla, msg, wParam, lParam);
        }

        public int Call(SciMsg msg, int wParam, int lParam)
        {
            return (int)Win32.SendMessage(_activeScintilla, msg, wParam, lParam);
        }

        public int Call(SciMsg msg, int wParam)
        {
            return Call(msg, wParam, 0);
        }

        public int Call(SciMsg msg)
        {
            return Call(msg, 0, 0);
        }

        #endregion

        public string GetRange(int selStart, int selEnd, int bufCapacity)
        {
            using (var textRange = new Sci_TextRange(selStart, selEnd, bufCapacity))
            {
                Call(SciMsg.SCI_GETTEXTRANGE, 0, textRange.NativePointer);
                return textRange.lpstrText;
            }
        }

        /// <summary>
        /// Returns the text currently selected (highlighted).
        /// </summary>
        /// <returns>Currently selected text.</returns>
        public string GetSelectedText()
        {
            var selLength = Call(SciMsg.SCI_GETSELTEXT);
            var selectedText = new StringBuilder(selLength);
            if (selLength > 0)
                Call(SciMsg.SCI_GETSELTEXT, 0, selectedText);
            return selectedText.ToString();
        }

        /// <summary>
        /// Gets the selected text or if nothing is selected, gets whole document text.
        /// </summary>
        /// <returns>Selected or whole document text.</returns>
        public string GetSelectedOrAllText()
        {
            var selectedText = GetSelectedText();
            return string.IsNullOrEmpty(selectedText) ? GetDocumentText() : selectedText;
        }

        /// <summary>
        /// The currently selected text is replaced with text. If no text is selected the 
        /// text is inserted at current cursor postion.
        /// </summary>
        /// <param name="text">The document text to set.</param>
        public void SetSelectedText(string text)
        {
            Call(SciMsg.SCI_REPLACESEL, 0, text);
        }

        /// <summary>
        /// Sets the text for the entire document (replacing any existing text).
        /// </summary>
        /// <param name="text">The document text to set.</param>
        public void SetDocumentText(string text)
        {
            Call(SciMsg.SCI_SETTEXT, 0, text);
        }

        // Gets the entire document text.
        public string GetDocumentText()
        {
            var length = GetDocumentLength();
            var text = new StringBuilder(length + 1);
            if (length > 0)
                Call(SciMsg.SCI_GETTEXT, length + 1, text);
            return text.ToString();
        }

        public void AddBookmark(int lineNumber)
        {
            if (lineNumber == -1)
                lineNumber = GetCurrentLineNumber();
            if (!IsBookmarkPresent(lineNumber))
                Call(SciMsg.SCI_MARKERADD, lineNumber, BookmarkMarker);
        }

        public void RemoveAllBookmarks()
        {
            Call(SciMsg.SCI_MARKERDELETEALL, BookmarkMarker);
        }

        public bool IsBookmarkPresent(int lineNumber)
        {
            if (lineNumber == -1)
                lineNumber = GetCurrentLineNumber();
            var state = Call(SciMsg.SCI_MARKERGET, lineNumber);
            return (state & (1 << BookmarkMarker)) != 0;
        }

        public int GetCurrentLineNumber()
        {
            var currentPos = Call(SciMsg.SCI_GETCURRENTPOS);
            return Call(SciMsg.SCI_LINEFROMPOSITION, currentPos, 0);
        }

        public void RemoveFindMarks()
        {
            Call(SciMsg.SCI_SETINDICATORCURRENT, IndicatorMatch);
            Call(SciMsg.SCI_INDICATORCLEARRANGE, 0, GetDocumentLength());
        }

        public Sci_CharacterRange GetSelection()
        {
            return new Sci_CharacterRange(
                Call(SciMsg.SCI_GETSELECTIONSTART),
                Call(SciMsg.SCI_GETSELECTIONEND));
        }

        public int GetDocumentLength()
        {
            return Call(SciMsg.SCI_GETLENGTH);
        }


        public void SetSelection(int start, int end)
        {
            Call(SciMsg.SCI_SETSEL, start, end);
        }

        public void EnsureRangeVisible(int start, int end)
        {
            var lineStart = Call(SciMsg.SCI_LINEFROMPOSITION, Math.Min(start, end));
            var lineEnd = Call(SciMsg.SCI_LINEFROMPOSITION, Math.Max(start, end));
            for (var line = lineStart; line <= lineEnd; line++)
            {
                Call(SciMsg.SCI_ENSUREVISIBLE, line);
            }
        }

        public int FindInTarget(string findText, int startPosition, int endPosition)
        {
            Call(SciMsg.SCI_SETTARGETSTART, startPosition);
            Call(SciMsg.SCI_SETTARGETEND, endPosition);
            return Call(SciMsg.SCI_SEARCHINTARGET, findText.Length, findText);
        }

        #endregion
    }
}