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
using System.Windows.Forms;
using phdesign.NppToolBucket.Forms;
using phdesign.NppToolBucket.Infrastructure;
using phdesign.NppToolBucket.PluginInfrastructure;

namespace phdesign.NppToolBucket
{
    /// <summary>
    /// Todo: Check different types of selection (e.g. by column), how does this affect us?
    /// </summary>
    public class FindAndReplace
    {
        #region Constants

        private const int MaxHistoryItems = 10;
        /// <summary>
        /// If false, regular expression syntax uses the old Unix style where \( and \) mark capturing sections while ( and ) are themselves.
        /// If true, regular expression syntax uses the more common style where ( and ) mark capturing sections while \( and \) are plain parentheses.
        /// </summary>
        private const bool UsePosixRegularExpressions = true;
        private const int UseSelectionAsFindTextLength = 200;

        #endregion

        #region Fields

        private Editor _editor;
        private FindAndReplaceForm _window;
        private IWin32Window _owner;
        private Sci_CharacterRange? _searchScope;
        private Sci_CharacterRange? _lastMatch;

        internal static FindAndReplaceSettings Settings;

        private static FindAndReplace _instance;

        #endregion

        #region Constructors

        private FindAndReplace()
        {
            _editor = Editor.GetActive();

            _window = new FindAndReplaceForm
            {
                FindHistory = Settings.FindHistory.ToArray(),
                ReplaceHistory = Settings.ReplaceHistory.ToArray(),
                MatchCase = Settings.MatchCase,
                MatchWholeWord = Settings.MatchWholeWord,
                UseRegularExpression = Settings.UseRegularExpression,
                SearchFromBegining = Settings.SearchFromBegining,
                SearchBackwards = Settings.SearchBackwards,
                SearchIn = Settings.SearchIn
            };
            if (!Settings.WindowSize.IsEmpty)
            {
                _window.ClientSize = Settings.WindowSize;
            }
            if (!Settings.WindowLocation.IsEmpty)
            {
                _window.StartPosition = FormStartPosition.Manual;
                _window.Location = Settings.WindowLocation;
            }
            _window.DoAction += OnDoAction;
            _owner = new WindowWrapper(PluginBase.nppData._nppHandle);
        }

        #endregion

        #region Public Static Methods

        internal static void Show()
        {
            if (_instance == null)
                _instance = new FindAndReplace();
            _instance.ShowForm();
        }

        #endregion

        #region Private Methods

        private void ShowForm()
        {
            if (_window.Visible) return;

            // Reset the search in
            if (_window.SearchIn == SearchInOptions.SelectedText)
                _window.SearchIn = SearchInOptions.CurrentDocument;

            // If selection is small, use it as find text, otherwise search in selection.
            var selLength = _editor.GetSelectionLength();
            if (selLength > 1)
            {
                var selectedText = _editor.GetSelectedText();
                if (selLength < UseSelectionAsFindTextLength && !selectedText.Contains("\n"))
                    _window.FindText = selectedText;
                else
                    _window.SearchIn = SearchInOptions.SelectedText;
            }

            _window.Show(_owner);
        }

        private void OnDoAction(object sender, DoActionEventArgs doActionEventArgs)
        {
            var window = sender as FindAndReplaceForm;
            if (window == null) return;

            // Set options
            Settings.MatchCase = window.MatchCase;
            Settings.MatchWholeWord = window.MatchWholeWord;
            Settings.UseRegularExpression = window.UseRegularExpression;
            Settings.SearchBackwards = window.SearchBackwards;
            Settings.SearchFromBegining = window.SearchFromBegining;
            Settings.SearchIn = window.SearchIn;
            Settings.WindowSize = window.ClientSize;
            Settings.WindowLocation = window.Location;

            var findText = window.FindText;
            var replaceText = window.ReplaceText;
            // Check find text is not null or empty. Replace text can be.
            if (string.IsNullOrEmpty(findText)) return;
            // Update history
            if (SaveFindHistory(findText))
                window.FindHistory = Settings.FindHistory.ToArray();
            if (Settings.SearchIn == SearchInOptions.SelectedText)
            {
                var sel = _editor.GetSelectionRange();
                // Check if user has changed selection - if so reset search scope. Selection should either be initial selection or last found match.
                if (_searchScope.HasValue && 
                    !sel.Equals(_searchScope.Value) && 
                    _lastMatch.HasValue && 
                    !sel.Equals(_lastMatch.Value) &&
                    sel.cpMin != sel.cpMax)
                    _searchScope = null;

                if (!_searchScope.HasValue)
                {
                    _searchScope = sel;
                    // Check we have a selection
                    if (_searchScope.Value.cpMin == _searchScope.Value.cpMax)
                    {
                        MessageBox.Show(_owner, "No text selected", window.Text);
                        return;
                    }
                    Settings.SearchFromBegining = true;
                }
            }

            switch (doActionEventArgs.Action)
            {
                case Forms.Action.FindNext:
                    var posFound = FindNext(findText);
                    if (posFound == -1)
                    {
                        MessageBox.Show(
                            _owner,
                            string.Format(
                                "{0} of {1} reached. No match found",
                                Settings.SearchBackwards ? "Start" : "End",
                                Settings.SearchIn == SearchInOptions.SelectedText ? "scope" : "document(s)"),
                            window.Text);
                        // If we haven't started from beginning of document, turn that option on.
                        if (!Settings.SearchFromBegining)
                            window.SearchFromBegining = true;
                        // Reset search in selection
                        if (Settings.SearchIn == SearchInOptions.SelectedText)
                            _editor.SetSelection(_searchScope.Value.cpMin, _searchScope.Value.cpMax);
                        // Clear search scope
                        _searchScope = null;
                    }
                    else
                    {
                        // Turn off search from beining otherwise we'll only ever find the first match.
                        window.SearchFromBegining = false;
                    }
                    break;
                case Forms.Action.FindAll:
                    MessageBox.Show(
                        _owner,
                        string.Format("{0} matches found", MarkAll(findText, false)), 
                        window.Text);
                    _searchScope = null;
                    break;
                case Forms.Action.Count:
                    MessageBox.Show(
                        _owner,
                        string.Format("{0} matches found", MarkAll(findText, true)), 
                        window.Text);
                    _searchScope = null;
                    break;
                case Forms.Action.Replace:
                    // Todo: Check if readonly
                    // if ((*_ppEditView)->getCurrentBuffer()->isReadOnly()) return false;
                    if (SaveReplaceHistory(replaceText))
                        window.ReplaceHistory = Settings.ReplaceHistory.ToArray();
                    var posFoundReplace = Replace(findText, replaceText);
                    if (posFoundReplace == -1)
                    {
                        MessageBox.Show(
                            _owner,
                            string.Format(
                                "{0} of {1} reached. No match found",
                                Settings.SearchBackwards ? "Start" : "End",
                                Settings.SearchIn == SearchInOptions.SelectedText ? "scope" : "document"),
                            window.Text);
                        // If we haven't started from begining of document, turn that option on.
                        if (!Settings.SearchFromBegining)
                            window.SearchFromBegining = true;
                        // Reset search in selection
                        if (Settings.SearchIn == SearchInOptions.SelectedText)
                            _editor.SetSelection(_searchScope.Value.cpMin, _searchScope.Value.cpMax);
                        // Clear search scope
                        _searchScope = null;
                    }
                    else
                    {
                        // Turn off search from beining otherwise we'll only ever find the first match.
                        window.SearchFromBegining = false;
                    }
                    break;
                case Forms.Action.ReplaceAll:
                    // Todo: Check if readonly
                    // if ((*_ppEditView)->getCurrentBuffer()->isReadOnly()) return false;
                    if (SaveReplaceHistory(replaceText))
                        window.ReplaceHistory = Settings.ReplaceHistory.ToArray();
                    MessageBox.Show(
                        _owner,
                        string.Format("{0} matches replaced", ReplaceAll(findText, replaceText)), 
                        window.Text);
                    _searchScope = null;
                    break;
            }
        }

        private bool IsViewVisible(int targetView)
        {
            int currentView;
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTSCINTILLA, 0, out currentView);
            // If the view is active it must be visible
            if (currentView == targetView) return true;
            var currentDocIndex = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTDOCINDEX, 0, currentView);
            var currentDocIndexTargetView = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTDOCINDEX, 0, targetView);

            // Try switching to other view, if that fails it must be hidden.
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, targetView, currentDocIndexTargetView);
            int newView;
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTSCINTILLA, 0, out newView);

            // Restore active doc
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, currentView, currentDocIndex);

            return newView == targetView;
        }

        private int ReplaceAll(string findText, string replaceText)
        {
            if (Settings.SearchIn == SearchInOptions.OpenDocuments)
            {
                var currentDocument = 0;
                var currentView = IsViewVisible((int)NppMsg.MAIN_VIEW) ? (int)NppMsg.MAIN_VIEW : (int)NppMsg.SUB_VIEW;
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, currentView, currentDocument);
                _editor = Editor.GetActive();
                return ReplaceAll(findText, replaceText, currentDocument, currentView);
            }
            return ReplaceAll(findText, replaceText, -1, -1);
        }

        private int ReplaceAll(string findText, string replaceText, int currentDocument, int currentView)
        {
            if (Settings.SearchIn == SearchInOptions.SelectedText && 
                (!_searchScope.HasValue || _searchScope.Value.cpMin == _searchScope.Value.cpMax))
                throw new InvalidOperationException("Search scope has not been defined.");

            var replacements = 0;
            SetSearchFlags();
            var startPosition = Settings.SearchIn == SearchInOptions.SelectedText ? _searchScope.Value.cpMin : 0;
            var endPosition = Settings.SearchIn == SearchInOptions.SelectedText ? _searchScope.Value.cpMax :_editor.GetDocumentLength();
            var posFound = _editor.FindInTarget(findText, startPosition, endPosition);
            if (findText == "^" && Settings.UseRegularExpression) {
                // Special case for replace all start of line so it hits the first line
                posFound = startPosition;
                _editor.SetTargetRange(startPosition, endPosition);
            }
            if (posFound != -1 && posFound <= endPosition)
            {
                _editor.BeginUndoAction();
                while (posFound != -1)
                {
                    var matchEnd = _editor.GetTargetRange().cpMax;
                    var targetLength = matchEnd - posFound;
                    var movePastEOL = 0;
                    if (targetLength <= 0)
                    {
                        var nextChar = _editor.GetCharAt(matchEnd);
                        if (nextChar == '\r' || nextChar == '\n')
                            movePastEOL = 1;
                    }
                    var replacedLength = _editor.ReplaceText(replaceText, Settings.UseRegularExpression);
                    // Modify for change caused by replacement
                    endPosition += replacedLength - targetLength;
                    // For the special cases of start of line and end of line
                    // something better could be done but there are too many special cases
                    var lastMatch = posFound + replacedLength + movePastEOL;
                    if (targetLength == 0)
                        lastMatch = _editor.PositionAfter(lastMatch); 
                    if (lastMatch >= endPosition)
                        // Run off the end of the document/selection with an empty match
                        posFound = -1;
                    else
                        posFound = _editor.FindInTarget(findText, lastMatch, endPosition);
                    replacements++;
                }
                _editor.EndUndoAction();
            }
            if (Settings.SearchIn == SearchInOptions.OpenDocuments)
            {
                // Is there another document?

                var fileCount = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETNBOPENFILES, 0, currentView + 1);
                // If there's another document in the view, switch to it
                if (currentDocument < fileCount - 1)
                {
                    Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, currentView, ++currentDocument);
                    _editor = Editor.GetActive();
                    replacements += ReplaceAll(findText, replaceText, currentDocument, currentView);
                }
                else if (currentView == (int)NppMsg.MAIN_VIEW && IsViewVisible((int)NppMsg.SUB_VIEW))
                {
                    currentView = (int)NppMsg.SUB_VIEW;
                    currentDocument = 0;
                    Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, currentView, currentDocument);
                    _editor = Editor.GetActive();
                    replacements += ReplaceAll(findText, replaceText, currentDocument, currentView);
                }
            }
            return replacements;
        }

        private int Replace(string findText, string replaceText)
        {
            // Check that the highlighted text is a match, if so replace it.
            var sel = _editor.GetSelectionRange();
            SetSearchFlags();
            var posFound = _editor.FindInTarget(findText, sel.cpMin, sel.cpMax);
            if (posFound != -1)
            {
                var match = _editor.GetTargetRange();
                if (sel.cpMin == match.cpMin && sel.cpMax == match.cpMax)
                {
                    var replacedLength = _editor.ReplaceText(replaceText, Settings.UseRegularExpression);
                    _editor.SetSelection(sel.cpMin + replacedLength, sel.cpMin);
                    _lastMatch = null;
                }
            }
            return FindNext(findText);
        }

        private int MarkAll(string findText, bool countOnly)
        {
            if (Settings.SearchIn == SearchInOptions.SelectedText && 
                (!_searchScope.HasValue || _searchScope.Value.cpMin == _searchScope.Value.cpMax))
                throw new InvalidOperationException("Search scope has not been defined.");
            
            var marked = 0;
            SetSearchFlags();
            var startPosition = Settings.SearchIn == SearchInOptions.SelectedText ? _searchScope.Value.cpMin : 0;
            var endPosition = Settings.SearchIn == SearchInOptions.SelectedText ? _searchScope.Value.cpMax : _editor.GetDocumentLength();
            var posFirstFound = _editor.FindInTarget(findText, startPosition, endPosition);

            if (!countOnly)
            {
                _editor.RemoveFindMarks();
                _editor.RemoveAllBookmarks();
            }
            if (posFirstFound != -1)
            {
                var posFound = posFirstFound;
                do
                {
                    marked++;
                    var matchEnd = _editor.GetTargetRange().cpMax;
                    if (!countOnly)
                    {
                        var line = _editor.LineFromPosition(posFound);
                        _editor.AddBookmark(line);
                        _editor.AddFindMark(posFound, matchEnd - posFound);
                    }
                    posFound = _editor.FindInTarget(findText, matchEnd, endPosition);
                } while ((posFound != -1) && (posFound != posFirstFound));

                // Jump to first match
                if (!countOnly)
                {
                    var posNextFound = FindNext(findText);
                    // Wrap around if not found.
                    if (posNextFound == -1 && !Settings.SearchFromBegining)
                    {
                        Settings.SearchFromBegining = true;
                        FindNext(findText);
                        Settings.SearchFromBegining = false;
                    }
                }
            }
            return marked;
        }

        private void SetSearchFlags() 
        {
            _editor.SetSearchFlags(
                Settings.MatchWholeWord,
                Settings.MatchCase,
                Settings.UseRegularExpression,
                UsePosixRegularExpressions);
        }

        private bool SaveReplaceHistory(string replaceText)
        {
            if (string.IsNullOrEmpty(replaceText)) return false;

            Settings.ReplaceHistory.Remove(replaceText);
            Settings.ReplaceHistory.Insert(0, replaceText);
            // Limit list to MaxHistoryItems
            if (MaxHistoryItems > 0 && Settings.ReplaceHistory.Count > MaxHistoryItems)
                Settings.ReplaceHistory.RemoveRange(MaxHistoryItems - 1, Settings.ReplaceHistory.Count - MaxHistoryItems);
            return true;
        }

        private bool SaveFindHistory(string findText)
        {
            if (string.IsNullOrEmpty(findText)) return false;

            Settings.FindHistory.Remove(findText);
            Settings.FindHistory.Insert(0, findText);
            // Limit list to MaxHistoryItems
            if (MaxHistoryItems > 0 && Settings.FindHistory.Count > MaxHistoryItems)
                Settings.FindHistory.RemoveRange(MaxHistoryItems - 1, Settings.FindHistory.Count - MaxHistoryItems);
            return true;
        }

        private int FindNext(string findText)
        {
            if (Settings.SearchIn == SearchInOptions.SelectedText && 
                (!_searchScope.HasValue || _searchScope.Value.cpMin == _searchScope.Value.cpMax))
                throw new InvalidOperationException("Search scope has not been defined."); 

            if (Settings.SearchIn == SearchInOptions.OpenDocuments && Settings.SearchFromBegining)
            {
                // Assumes that both views ALWAYS have one document open (index 0). Notepad++ always shows a 'New 1' document if everthing else is closed.
                if (!Settings.SearchBackwards)
                {
                    Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, IsViewVisible((int)NppMsg.MAIN_VIEW) ? (int)NppMsg.MAIN_VIEW : (int)NppMsg.SUB_VIEW, 0);
                    _editor = Editor.GetActive();
                }
                else
                {
                    if (IsViewVisible((int)NppMsg.SUB_VIEW))
                    {
                        var secondViewFileCount = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETNBOPENFILES, 0, (int)NppMsg.SECOND_VIEW);
                        Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, (int)NppMsg.SUB_VIEW, secondViewFileCount > 0 ? secondViewFileCount - 1 : 0);
                    }
                    else
                    {
                        var primaryViewFileCount = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETNBOPENFILES, 0, (int)NppMsg.PRIMARY_VIEW);
                        Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, (int)NppMsg.MAIN_VIEW, primaryViewFileCount > 0 ? primaryViewFileCount - 1 : 0);
                    }
                    _editor = Editor.GetActive();
                }
            }
            
            var selection = _editor.GetSelectionRange();
            var documentLength = _editor.GetDocumentLength();

            var startPosition = Settings.SearchBackwards
                ? Settings.SearchFromBegining
                    ? Settings.SearchIn == SearchInOptions.SelectedText 
                        ? _searchScope.Value.cpMax
                        : documentLength 
                    : selection.cpMin
                : Settings.SearchFromBegining
                    ? Settings.SearchIn == SearchInOptions.SelectedText 
                        ? _searchScope.Value.cpMin
                        : 0 
                    : selection.cpMax;
            var endPosition = Settings.SearchBackwards
                ? Settings.SearchIn == SearchInOptions.SelectedText
                    ? _searchScope.Value.cpMin
                    : 0
                : Settings.SearchIn == SearchInOptions.SelectedText
                    ? _searchScope.Value.cpMax
                    : documentLength;

            SetSearchFlags();
            var posFind = _editor.FindInTarget(findText, startPosition, endPosition);
            if (posFind != -1)
            {
                // Highlight if found
                _lastMatch = _editor.GetTargetRange();
                _editor.EnsureRangeVisible(_lastMatch.Value.cpMin, _lastMatch.Value.cpMax);
                _editor.SetSelection(_lastMatch.Value.cpMin, _lastMatch.Value.cpMax);
            }
            else if (Settings.SearchIn == SearchInOptions.OpenDocuments)
            {
                // Check next document
                int currentView;
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTSCINTILLA, 0, out currentView);
                var currentFileIndex = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTDOCINDEX, 0, currentView);
                if (!Settings.SearchBackwards)
                {
                    // Searching forwards
                    var fileCount = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETNBOPENFILES, 0, currentView + 1);
                    // If there's another document in the view, switch to it
                    if (currentFileIndex < fileCount - 1)
                        posFind = FindInOtherDocument(findText, currentView, currentFileIndex + 1);
                    else if (currentView == (int)NppMsg.MAIN_VIEW && IsViewVisible((int)NppMsg.SUB_VIEW))
                        posFind = FindInOtherDocument(findText, (int)NppMsg.SUB_VIEW, 0);
                }
                else
                {
                    // Searching backwards
                    if (currentFileIndex > 0)
                        posFind = FindInOtherDocument(findText, currentView, currentFileIndex - 1);
                    else if (currentView == (int)NppMsg.SUB_VIEW && IsViewVisible((int)NppMsg.MAIN_VIEW))
                    {
                        var primaryViewFileCount = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETNBOPENFILES, 0, (int)NppMsg.PRIMARY_VIEW);
                        posFind = FindInOtherDocument(findText, (int)NppMsg.MAIN_VIEW, primaryViewFileCount > 0 ? primaryViewFileCount - 1 : 0);
                    }
                }
            }

            return posFind;
        }

        private int FindInOtherDocument(string findText, int view, int fileIndex)
        {
            // Activate next document
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, view, fileIndex);
            _editor = Editor.GetActive();
            // This statement avoids a loop if there's no match in the first document, it calls FindInOtherDocument but if SearchFromBegining is true it starts back at first document again.
            Settings.SearchFromBegining = false;
            // Reset cursor position to top of documnet
            _editor.SetSelection(0, 0);
            return FindNext(findText);
        }

        #endregion
    }
}