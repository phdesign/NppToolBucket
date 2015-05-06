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
using System.Collections.Generic;
using System.Text;
using phdesign.NppToolBucket.Utilities;

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

        #region Private Call Methods

        private int Call(SciMsg msg, int wParam, IntPtr lParam)
        {
            return (int)Win32.SendMessage(_activeScintilla, msg, wParam, lParam);
        }

        private int Call(SciMsg msg, int wParam, string lParam)
        {
            return (int)Win32.SendMessage(_activeScintilla, msg, wParam, lParam);
        }

        private int Call(SciMsg msg, int wParam, StringBuilder lParam)
        {
            return (int)Win32.SendMessage(_activeScintilla, msg, wParam, lParam);
        }

        private int Call(SciMsg msg, int wParam, int lParam)
        {
            return (int)Win32.SendMessage(_activeScintilla, msg, wParam, lParam);
        }

        private int Call(SciMsg msg, int wParam)
        {
            return Call(msg, wParam, 0);
        }

        private int Call(SciMsg msg)
        {
            return Call(msg, 0, 0);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the text between the positions start and end. If end is -1, text is returned to the end of the document. 
        /// The text is 0 terminated, so you must supply a buffer that is at least 1 character longer than the number of characters 
        /// you wish to read. The return value is the length of the returned text not including the terminating 0.
        /// </summary>
        public string GetTextByRange(int start, int end)
        {
            return GetTextByRange(start, end, end - start);
        }

        /// <summary>
        /// Returns the text between the positions start and end. If end is -1, text is returned to the end of the document. 
        /// The text is 0 terminated, so you must supply a buffer that is at least 1 character longer than the number of characters 
        /// you wish to read. The return value is the length of the returned text not including the terminating 0.
        /// </summary>
        public string GetTextByRange(int start, int end, int bufCapacity)
        {
            using (var textRange = new Sci_TextRange(start, end, bufCapacity))
            {
                Call(SciMsg.SCI_GETTEXTRANGE, 0, textRange.NativePointer);
                //return textRange.lpstrText;
                return IsUnicode() ? StringUtils.AnsiToUnicode(textRange.lpstrText) : textRange.lpstrText;
            }
        }

        /// <summary>
        /// Replaces the current selected target range of text.
        /// </summary>
        /// <param name="text">The replacement text.</param>
        /// <param name="useRegularExpression">If true, uses a regular expressions replacement.</param>
        /// <returns> The length of the replacement string.</returns>
        public int ReplaceText(string text, bool useRegularExpression)
        {
            if (IsUnicode())
                text = StringUtils.UnicodeToAnsi(text);
            return Call(useRegularExpression ? SciMsg.SCI_REPLACETARGETRE : SciMsg.SCI_REPLACETARGET, text.Length, text);
        }

        /// <summary>
        /// Replaces a range of text with new text.
        /// Note that the recommended way to delete text in the document is to set the target to the text to be removed, and to 
        /// perform a replace target with an empty string.
        /// </summary>
        /// <returns> The length of the replacement string.</returns>
        public int ReplaceText(int start, int end, string text)
        {
            SetTargetRange(start, end);
            if (IsUnicode())
                text = StringUtils.UnicodeToAnsi(text);
            // If length is -1, text is a zero terminated string, otherwise length sets the number of character to replace 
            // the target with. After replacement, the target range refers to the replacement text. The return value is the 
            // length of the replacement string.
            return Call(SciMsg.SCI_REPLACETARGET, text.Length, text);
        }

        /// <summary>
        /// Returns true if the current document is displaying in unicode format or false for ANSI.
        /// Note that all strings marshaled to and from Scintilla come in ANSI format so need to 
        /// be converted if using Unicode.
        /// </summary>
        public bool IsUnicode()
        {
            var result = Call(SciMsg.SCI_GETCODEPAGE);
            return result == (int)SciMsg.SC_CP_UTF8;
        }

        /// <summary>
        /// Size in bytes of the selection.
        /// </summary>
        public int GetSelectionLength()
        {
            return Call(SciMsg.SCI_GETSELTEXT);
        }

        /// <summary>
        /// Returns the text currently selected (highlighted).
        /// </summary>
        /// <returns>Currently selected text.</returns>
        public string GetSelectedText()
        {
            var selLength = GetSelectionLength();
            // Todo: Use a string / char array as stringbuilder can't handle null characters?
            var selectedText = new StringBuilder(selLength);
            if (selLength > 0)
                Call(SciMsg.SCI_GETSELTEXT, 0, selectedText);
            var ret = selectedText.ToString();
            return IsUnicode() ? StringUtils.AnsiToUnicode(ret) : ret;
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
            if (IsUnicode())
                text = StringUtils.UnicodeToAnsi(text);
            Call(SciMsg.SCI_REPLACESEL, 0, text);
        }

        /// <summary>
        /// Sets the text for the entire document (replacing any existing text).
        /// </summary>
        /// <param name="text">The document text to set.</param>
        public void SetDocumentText(string text)
        {
            if (IsUnicode())
                text = StringUtils.UnicodeToAnsi(text);
            Call(SciMsg.SCI_SETTEXT, 0, text);
        }

        /// <summary>
        /// Gets the entire document text.
        /// </summary>
        public string GetDocumentText()
        {
            var length = GetDocumentLength();
            var text = new StringBuilder(length + 1);
            if (length > 0)
                Call(SciMsg.SCI_GETTEXT, length + 1, text);
            var ret = text.ToString();
            return IsUnicode() ? StringUtils.AnsiToUnicode(ret) : ret;
        }

        /// <summary>
        /// Add a bookmark at a specific line.
        /// </summary>
        /// <param name="lineNumber">The line number to add a bookmark to.</param>
        public void AddBookmark(int lineNumber)
        {
            if (lineNumber == -1)
                lineNumber = GetCurrentLineNumber();
            if (!IsBookmarkPresent(lineNumber))
                Call(SciMsg.SCI_MARKERADD, lineNumber, BookmarkMarker);
        }

        /// <summary>
        /// Remove all bookmarks from the document.
        /// </summary>
        public void RemoveAllBookmarks()
        {
            Call(SciMsg.SCI_MARKERDELETEALL, BookmarkMarker);
        }

        /// <summary>
        /// Is there a bookmark set on a line.
        /// </summary>
        /// <param name="lineNumber">The line number to check.</param>
        /// <returns>True if a bookmark is set.</returns>
        public bool IsBookmarkPresent(int lineNumber)
        {
            if (lineNumber == -1)
                lineNumber = GetCurrentLineNumber();
            var state = Call(SciMsg.SCI_MARKERGET, lineNumber);
            return (state & (1 << BookmarkMarker)) != 0;
        }

        /// <summary>
        /// Get the line number that the cursor is on.
        /// </summary>
        public int GetCurrentLineNumber()
        {
            var currentPos = Call(SciMsg.SCI_GETCURRENTPOS);
            return Call(SciMsg.SCI_LINEFROMPOSITION, currentPos, 0);
        }

        /// <summary>
        /// Remove all 'find' marks.
        /// </summary>
        public void RemoveFindMarks()
        {
            Call(SciMsg.SCI_SETINDICATORCURRENT, IndicatorMatch);
            Call(SciMsg.SCI_INDICATORCLEARRANGE, 0, GetDocumentLength());
        }

        /// <summary>
        /// Marks a range of text.
        /// </summary>
        public void AddFindMark(int pos, int length)
        {
            Call(SciMsg.SCI_INDICATORFILLRANGE, pos, length);
        }

        /// <summary>
        /// Returns the start and end of the selection without regard to which end is the current position and which is the anchor. 
        /// SCI_GETSELECTIONSTART returns the smaller of the current position or the anchor position.
        /// </summary>
        /// <returns>A character range.</returns>
        public Sci_CharacterRange GetSelectionRange()
        {
            return new Sci_CharacterRange(
                Call(SciMsg.SCI_GETSELECTIONSTART),
                Call(SciMsg.SCI_GETSELECTIONEND));
        }

        /// <summary>
        /// Returns the current target start and end positions from a previous operation.
        /// </summary>
        public Sci_CharacterRange GetTargetRange()
        {
            return new Sci_CharacterRange(
                Call(SciMsg.SCI_GETTARGETSTART),
                Call(SciMsg.SCI_GETTARGETEND));
        }

        /// <summary>
        /// Sets the start and end positions for an upcoming operation.
        /// </summary>
        public void SetTargetRange(int start, int end)
        {
            Call(SciMsg.SCI_SETTARGETSTART, start);
            Call(SciMsg.SCI_SETTARGETEND, end);
        }

        /// <summary>
        /// Returns the length of the document in bytes.
        /// </summary>
        public int GetDocumentLength()
        {
            return Call(SciMsg.SCI_GETLENGTH);
        }

        /// <summary>
        /// Sets both the anchor and the current position. If end is negative, it means the end of the document. 
        /// If start is negative, it means remove any selection (i.e. set the start to the same position as end). 
        /// The caret is scrolled into view after this operation.
        /// </summary>
        /// <param name="start">The selection start (anchor) position.</param>
        /// <param name="end">The selection end (current) position.</param>
        public void SetSelection(int start, int end)
        {
            Call(SciMsg.SCI_SETSEL, start, end);
        }

        /// <summary>
        /// Make a range visible by scrolling to the last line of the range.
        /// A line may be hidden because more than one of its parent lines is contracted. Both these message travels up the 
        /// fold hierarchy, expanding any contracted folds until they reach the top level. The line will then be visible. 
        /// </summary>
        public void EnsureRangeVisible(int start, int end)
        {
            var lineStart = Call(SciMsg.SCI_LINEFROMPOSITION, Math.Min(start, end));
            var lineEnd = Call(SciMsg.SCI_LINEFROMPOSITION, Math.Max(start, end));
            for (var line = lineStart; line <= lineEnd; line++)
            {
                Call(SciMsg.SCI_ENSUREVISIBLE, line);
            }
        }

        /// <summary>
        /// This searches for the first occurrence of a text string in the target defined by startPosition and endPosition. 
        /// The text string is not zero terminated; the size is set by length. 
        /// The search is modified by the search flags set by SCI_SETSEARCHFLAGS. 
        /// If the search succeeds, the target is set to the found text and the return value is the position of the start 
        /// of the matching text. If the search fails, the result is -1.
        /// </summary>
        /// <param name="findText">String to search for.</param>
        /// <param name="startPosition">Where to start searching from.</param>
        /// <param name="endPosition">Where to stop searching.</param>
        /// <returns>-1 if no match is found, otherwise the position (relative to start) of the first match.</returns>
        public int FindInTarget(string findText, int startPosition, int endPosition)
        {
            SetTargetRange(startPosition, endPosition);
            if (IsUnicode())
                findText = StringUtils.UnicodeToAnsi(findText);
            return Call(SciMsg.SCI_SEARCHINTARGET, findText.Length, findText);
        }

        /// <summary>
        /// Gets the size of a tab as a multiple of the size of a space character in STYLE_DEFAULT. The default tab width is 8 characters. 
        /// There are no limits on tab sizes, but values less than 1 or large values may have undesirable effects.
        /// </summary>
        public int GetTabWidth()
        {
            return Call(SciMsg.SCI_GETTABWIDTH);
        }

        /// <summary>
        /// Gets the size of indentation in terms of the width of a space in STYLE_DEFAULT. If you set a width of 0, 
        /// the indent size is the same as the tab size. There are no limits on indent sizes, but values less than 0 or 
        /// large values may have undesirable effects.
        /// </summary>
        public int GetIndent()
        {
            return Call(SciMsg.SCI_GETINDENT);
        }

        /// <summary>
        /// Determines whether indentation should be created out of a mixture of tabs and spaces or be based purely on spaces. 
        /// Set useTabs to false (0) to create all tabs and indents out of spaces. The default is true. 
        /// You can use SCI_GETCOLUMN to get the column of a position taking the width of a tab into account.
        /// </summary>
        public int GetUseTabs()
        {
            return Call(SciMsg.SCI_GETUSETABS);
        }

        /// <summary>
        /// Sets the size of a tab as a multiple of the size of a space character in STYLE_DEFAULT. The default tab width is 8 characters. 
        /// There are no limits on tab sizes, but values less than 1 or large values may have undesirable effects.
        /// </summary>
        /// <param name="tabSize"></param>
        public void SetTabWidth(int tabSize)
        {
            Call(SciMsg.SCI_SETTABWIDTH, tabSize);
        }

        /// <summary>
        /// Sets the size of indentation in terms of the width of a space in STYLE_DEFAULT. If you set a width of 0, 
        /// the indent size is the same as the tab size. There are no limits on indent sizes, but values less than 0 or 
        /// large values may have undesirable effects.
        /// </summary>
        public void SetIndent(int indentSize)
        {
            Call(SciMsg.SCI_SETINDENT, indentSize);
        }

        /// <summary>
        /// Determines whether indentation should be created out of a mixture of tabs and spaces or be based purely on spaces. 
        /// Set useTabs to false (0) to create all tabs and indents out of spaces. The default is true. 
        /// You can use SCI_GETCOLUMN to get the column of a position taking the width of a tab into account.
        /// </summary>
        public void SetUseTabs(bool useTabs)
        {
            Call(SciMsg.SCI_SETUSETABS, useTabs ? 1 : 0);
        }

        /// <summary>
        /// Mark the beginning of a set of operations that you want to undo all as one operation but that you have to generate 
        /// as several operations. Alternatively, you can use these to mark a set of operations that you do not want to have 
        /// combined with the preceding or following operations if they are undone.
        /// </summary>
        public void BeginUndoAction()
        {
            Call(SciMsg.SCI_BEGINUNDOACTION);
        }

        /// <summary>
        /// Mark the end of a set of operations that you want to undo all as one operation but that you have to generate 
        /// as several operations. Alternatively, you can use these to mark a set of operations that you do not want to have 
        /// combined with the preceding or following operations if they are undone.
        /// </summary>
        public void EndUndoAction()
        {
            Call(SciMsg.SCI_ENDUNDOACTION);
        }

        /// <summary>
        /// This returns the number of lines in the document. An empty document contains 1 line. A document holding only an 
        /// end of line sequence has 2 lines.
        /// </summary>
        public int GetLineCount()
        {
            return Call(SciMsg.SCI_GETLINECOUNT);
        }

        /// <summary>
        /// This returns the document position that corresponds with the start of the line. If line is negative, 
        /// the position of the line holding the start of the selection is returned. If line is greater than the 
        /// lines in the document, the return value is -1. If line is equal to the number of lines in the document 
        /// (i.e. 1 line past the last line), the return value is the end of the document.
        /// </summary>
        public int PositionFromLine(int line)
        {
            return Call(SciMsg.SCI_POSITIONFROMLINE, line);
        }

        /// <summary>
        /// Returns the line that contains the position pos in the document. The return value is 0 if pos &lt;= 0. 
        /// The return value is the last line if pos is beyond the end of the document.
        /// </summary>
        public int LineFromPosition(int pos)
        {
            return Call(SciMsg.SCI_LINEFROMPOSITION, pos);
        }

        /// <summary>
        /// Returns the amount of indentation on a line. The indentation is measured in character columns, which correspond to 
        /// the width of space characters.
        /// </summary>
        public int GetLineIndentation(int line)
        {
            return Call(SciMsg.SCI_GETLINEINDENTATION, line);
        }

        /// <summary>
        /// This returns the position at the end of indentation of a line.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int GetLineIndentPosition(int line)
        {
            return Call(SciMsg.SCI_GETLINEINDENTPOSITION, line);
        }

        public void SetSearchFlags(bool matchWholeWord, bool matchCase, bool useRegularExpression, bool usePosixRegularExpressions)
        {
            var searchFlags = (matchWholeWord ? (int)SciMsg.SCFIND_WHOLEWORD : 0) |
                (matchCase ? (int)SciMsg.SCFIND_MATCHCASE : 0) |
                (useRegularExpression ? (int)SciMsg.SCFIND_REGEXP : 0) |
                (usePosixRegularExpressions ? (int)SciMsg.SCFIND_POSIX : 0);
            Call(SciMsg.SCI_SETSEARCHFLAGS, searchFlags);
        }

        /// <summary>
        /// This returns the character at pos in the document or 0 if pos is negative or past the end of the document.
        /// </summary>
        public char GetCharAt(int pos)
        {
            var bytes = new List<byte>();
            // PositionAfter helps detect high Unicode characters, get up to 2 more bytes
            var end = Math.Min(PositionAfter(pos), pos + 2);
            for (var i = pos; i < end; i++)
            {
                bytes.Add((byte)Call(SciMsg.SCI_GETCHARAT, i));
            }
            return IsUnicode()
                ? Encoding.UTF8.GetChars(bytes.ToArray())[0]
                : Encoding.Default.GetChars(bytes.ToArray())[0];
        }

        /// <summary>
        /// return the position after another position in the document taking into account the current code page. 
        /// The maximum is the last position in the document. If called with a position within a multi byte character will 
        /// return the position of the end of that character.
        /// </summary>
        public int PositionAfter(int pos)
        {
            return Call(SciMsg.SCI_POSITIONAFTER, pos);
        }

        #endregion
    }
}