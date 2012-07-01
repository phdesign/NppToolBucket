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
using phdesign.NppToolBucket.PluginCore;

namespace phdesign.NppToolBucket
{
    internal class IndentationSettings
    {
        private readonly Editor _editor;

        private IndentationSettings()
        {
            _editor = Editor.GetActive();
        }

        internal static void Show()
        {
            new IndentationSettings().ShowDialog();
        }

        // C:\Working\Sandbox\NppPlugin.NET\scintilla\scintilla\scite\src\SciTEBase.cxx@2795
        // C:\Working\Sandbox\NppPlugin.NET\scintilla\scintilla\scite\gtk\SciTEGTK.cxx@1874
        private void ShowDialog()
        {
            var currentTabSize = _editor.Call(SciMsg.SCI_GETTABWIDTH);
            var currentIndentSize = _editor.Call(SciMsg.SCI_GETINDENT);
            var currentUseTabs = (_editor.Call(SciMsg.SCI_GETUSETABS) != 0);

            var dialog = new IndentationSettingsForm
            {
                TabSize = currentTabSize,
                IndentSize = currentIndentSize,
                UseTabs = currentUseTabs
            };
            dialog.ShowDialog();

            SetTabSize(dialog.TabSize, dialog.IndentSize, dialog.UseTabs);
            if (dialog.ConvertIndent)
                ConvertIndentation(dialog.TabSize, dialog.UseTabs);
        }

        private void SetTabSize(int tabSize, int indentSize, bool useTabs)
        {
            if (tabSize > 0)
                _editor.Call(SciMsg.SCI_SETTABWIDTH, tabSize);
            if (indentSize >= 0)
                _editor.Call(SciMsg.SCI_SETINDENT, indentSize);
            _editor.Call(SciMsg.SCI_SETUSETABS, useTabs ? 1 : 0);
        }

        private void ConvertIndentation(int tabSize, bool useTabs)
        {
            _editor.Call(SciMsg.SCI_BEGINUNDOACTION);
            var maxLine = _editor.Call(SciMsg.SCI_GETLINECOUNT);
            for (var line = 0; line < maxLine; line++)
            {
                var lineStart = _editor.Call(SciMsg.SCI_POSITIONFROMLINE, line);
                var indent = _editor.Call(SciMsg.SCI_GETLINEINDENTATION, line);
                var indentPos = _editor.Call(SciMsg.SCI_GETLINEINDENTPOSITION, line);
                const int maxIndentation = 1000;
                if (indent < maxIndentation)
                {
                    var indentationNow = _editor.GetRange(lineStart, indentPos, maxIndentation);
                    var indentationWanted = CreateIndentation(indent, tabSize, !useTabs);
                    if (indentationNow != indentationWanted)
                    {
                        _editor.Call(SciMsg.SCI_SETTARGETSTART, lineStart);
                        _editor.Call(SciMsg.SCI_SETTARGETEND, indentPos);
                        _editor.Call(SciMsg.SCI_REPLACETARGET, indentationWanted.Length, indentationWanted);
                    }
                }
            }
            _editor.Call(SciMsg.SCI_ENDUNDOACTION);
        }

        private static string CreateIndentation(int indent, int tabSize, bool insertSpaces)
        {
            var indentation = new StringBuilder();
            if (!insertSpaces)
            {
                while (indent >= tabSize)
                {
                    indentation.Append("\t");
                    indent -= tabSize;
                }
            }
            while (indent > 0)
            {
                indentation.Append(" ");
                indent--;
            }
            return indentation.ToString();
        }
    }
}