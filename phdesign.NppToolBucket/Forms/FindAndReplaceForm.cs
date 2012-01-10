using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using phdesign.NppToolBucket.Utilities.Enum;

namespace phdesign.NppToolBucket.Forms
{
    public partial class FindAndReplaceForm : Form
    {
        #region Fields

        private ContextMenuStrip contextMenuStripFindHistory;
        private ContextMenuStrip contextMenuStripReplaceHistory;
        private string[] _findHistory;
        private string[] _replaceHistory;

        #endregion

        #region Constants

        private const string ClearHistoryMenuItem = "Clear history";
        private const string SearchFromEndText = "Search from end";
        private const string SearchFromBeginingText = "Search from begining";

        #endregion

        #region Properties

        public string FindText 
        { 
            get { return textBoxFind.Text; }
            set { textBoxFind.Text = value; }
        }

        public string ReplaceText
        {
            get { return textBoxReplace.Text; }
            set { textBoxReplace.Text = value; }
        }

        public SearchInOptions SearchIn
        {
            get { return (SearchInOptions)Enum.Parse(typeof(SearchInOptions), comboBoxSearchIn.SelectedIndex.ToString()); }
            set { comboBoxSearchIn.SelectedIndex = (int)value; }
        }

        public string[] FindHistory
        {
            get { return _findHistory; }
            set
            {
                _findHistory = value;
                contextMenuStripFindHistory.Items.Clear();
                if (_findHistory != null && _findHistory.Length > 0)
                {
                    // Update history button state
                    buttonFindHistory.Enabled = true;
                    // Update history menu
                    foreach (var historyItem in _findHistory)
                    {
                        contextMenuStripFindHistory.Items.Add(
                            new ToolStripButton(Shorten(historyItem)) {Tag = historyItem});
                    }
                }
                else
                {
                    buttonFindHistory.Enabled = false;
                }
            }
        }

        public string[] ReplaceHistory
        {
            get { return _replaceHistory; }
            set
            {
                _replaceHistory = value;
                contextMenuStripReplaceHistory.Items.Clear();
                if (_replaceHistory != null && _replaceHistory.Length > 0)
                {
                    // Update history button state
                    buttonReplaceHistory.Enabled = true;
                    // Update history menu
                    foreach (var historyItem in _replaceHistory)
                    {
                        contextMenuStripReplaceHistory.Items.Add(
                            new ToolStripButton(Shorten(historyItem)) { Tag = historyItem });
                    }
                }
                else
                {
                    buttonReplaceHistory.Enabled = false;
                }
            }
        }

        public bool MatchCase
        {
            get { return checkBoxMatchCase.Checked; }
            set { checkBoxMatchCase.Checked = value; }
        }

        public bool MatchWholeWord
        {
            get { return checkBoxMatchWholeWord.Checked; }
            set { checkBoxMatchWholeWord.Checked = value; }
        }

        public bool UseRegularExpression
        {
            get { return checkBoxUseRegularExpression.Checked; }
            set { checkBoxUseRegularExpression.Checked = value; }
        }

        public bool SearchFromBegining
        {
            get { return checkBoxSearchFromBegining.Checked; }
            set { checkBoxSearchFromBegining.Checked = value; }
        }

        public bool SearchBackwards
        {
            get { return checkBoxSearchBackwards.Checked; }
            set { checkBoxSearchBackwards.Checked = value; }
        }

        #endregion

        #region Events

        public event EventHandler<DoActionEventArgs> DoAction;

        #endregion

        #region Constructors

        public FindAndReplaceForm()
        {
            InitializeComponent();
            contextMenuStripFindHistory = new ContextMenuStrip
            {
                ShowImageMargin = false,
                AutoSize = true
            };
            contextMenuStripFindHistory.ItemClicked += contextMenuStripFindHistory_ItemClicked;
            contextMenuStripReplaceHistory = new ContextMenuStrip
            {
                ShowImageMargin = false,
                AutoSize = true
            };
            contextMenuStripReplaceHistory.ItemClicked += contextMenuStripReplaceHistory_ItemClicked;
            buttonFindHistory.Enabled = false;
            buttonReplaceHistory.Enabled = false;
            checkBoxSearchFromBegining.Text = SearchFromBeginingText;

            var searchInOptions = new List<string>(EnumUtils.GetEnumDescriptions<SearchInOptions>().Values);
            comboBoxSearchIn.DataSource = searchInOptions;
        }

        #endregion

        #region Private Helper Methods

        private void OnDoAction(Action action)
        {
            if (DoAction != null)
                DoAction(this, new DoActionEventArgs(action));
        }

        /// <summary>
        /// If longer than 20 chars, show first 15, elipsis and last 5.
        /// </summary>
        private string Shorten(string value)
        {
            const int maxLength = 30;
            const int showStartLength = 20;
            const int showEndLength = 7;

            if (value.Length <= maxLength)
                return value;

            return string.Format("{0}...{1}",
                value.Substring(0, showStartLength),
                value.Substring(value.Length - showEndLength));
        }

        #endregion

        #region Event Handlers

        private void contextMenuStripReplaceHistory_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            textBoxReplace.Text = (string)e.ClickedItem.Tag;
        }

        private void contextMenuStripFindHistory_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            textBoxFind.Text = (string)e.ClickedItem.Tag;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonCount_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.Count);
        }

        private void checkBoxSearchBackwards_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSearchFromBegining.Text = checkBoxSearchBackwards.Checked 
                ? SearchFromEndText
                : SearchFromBeginingText;
        }

        private void buttonFindNext_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.FindNext);
        }

        private void buttonFindAll_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.FindAll);
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.Replace);
        }

        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.ReplaceAll);
        }

        private void buttonFindHistory_Click(object sender, EventArgs e)
        {
            contextMenuStripFindHistory.Show(buttonFindHistory, buttonFindHistory.Width, 0);
        }

        private void buttonReplaceHistory_Click(object sender, EventArgs e)
        {
            contextMenuStripReplaceHistory.Show(buttonReplaceHistory, buttonReplaceHistory.Width, 0);
        }

        private void FindAndReplaceForm_Shown(object sender, EventArgs e)
        {
            textBoxFind.Focus();
        }

        private void FindAndReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide the form rather than closing it.
            Hide();
            e.Cancel = true;
        }

        #endregion
    }

    public enum Action
    {
        FindNext,
        FindAll,
        Replace,
        ReplaceAll,
        Count
    }

    public class DoActionEventArgs : EventArgs
    {
        public Action Action;

        public DoActionEventArgs(Action action)
        {
            Action = action;
        }
    }
}
