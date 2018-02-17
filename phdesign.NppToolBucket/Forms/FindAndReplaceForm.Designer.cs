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

namespace phdesign.NppToolBucket.Forms
{
    partial class FindAndReplaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonFindNext = new System.Windows.Forms.Button();
            this.buttonFindAll = new System.Windows.Forms.Button();
            this.buttonCount = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonReplaceAll = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonReplaceHistory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFindHistory = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.checkBoxSearchFromBegining = new System.Windows.Forms.CheckBox();
            this.checkBoxSearchBackwards = new System.Windows.Forms.CheckBox();
            this.checkBoxUseRegularExpression = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSearchIn = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 194);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.buttonFindNext);
            this.flowLayoutPanel1.Controls.Add(this.buttonFindAll);
            this.flowLayoutPanel1.Controls.Add(this.buttonCount);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.buttonReplace);
            this.flowLayoutPanel1.Controls.Add(this.buttonReplaceAll);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.buttonClose);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(398, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(81, 186);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // buttonFindNext
            // 
            this.buttonFindNext.Location = new System.Drawing.Point(3, 3);
            this.buttonFindNext.Name = "buttonFindNext";
            this.buttonFindNext.Size = new System.Drawing.Size(75, 22);
            this.buttonFindNext.TabIndex = 9;
            this.buttonFindNext.Text = "&Find Next";
            this.buttonFindNext.UseVisualStyleBackColor = true;
            this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
            // 
            // buttonFindAll
            // 
            this.buttonFindAll.Location = new System.Drawing.Point(3, 31);
            this.buttonFindAll.Name = "buttonFindAll";
            this.buttonFindAll.Size = new System.Drawing.Size(75, 22);
            this.buttonFindAll.TabIndex = 10;
            this.buttonFindAll.Text = "Find A&ll";
            this.buttonFindAll.UseVisualStyleBackColor = true;
            this.buttonFindAll.Click += new System.EventHandler(this.buttonFindAll_Click);
            // 
            // buttonCount
            // 
            this.buttonCount.Location = new System.Drawing.Point(3, 59);
            this.buttonCount.Name = "buttonCount";
            this.buttonCount.Size = new System.Drawing.Size(75, 22);
            this.buttonCount.TabIndex = 11;
            this.buttonCount.Text = "C&ount";
            this.buttonCount.UseVisualStyleBackColor = true;
            this.buttonCount.Click += new System.EventHandler(this.buttonCount_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(75, 3);
            this.panel1.TabIndex = 7;
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(3, 96);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(75, 22);
            this.buttonReplace.TabIndex = 12;
            this.buttonReplace.Text = "&Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // buttonReplaceAll
            // 
            this.buttonReplaceAll.Location = new System.Drawing.Point(3, 124);
            this.buttonReplaceAll.Name = "buttonReplaceAll";
            this.buttonReplaceAll.Size = new System.Drawing.Size(75, 22);
            this.buttonReplaceAll.TabIndex = 13;
            this.buttonReplaceAll.Text = "Replace &All";
            this.buttonReplaceAll.UseVisualStyleBackColor = true;
            this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 152);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(75, 3);
            this.panel2.TabIndex = 8;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(3, 161);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 22);
            this.buttonClose.TabIndex = 14;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.buttonReplaceHistory, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonFindHistory, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxReplace, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxFind, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxSearchIn, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(389, 188);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // buttonReplaceHistory
            // 
            this.buttonReplaceHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReplaceHistory.Image = global::phdesign.NppToolBucket.Properties.Resources.calendar_small_month;
            this.buttonReplaceHistory.Location = new System.Drawing.Point(372, 70);
            this.buttonReplaceHistory.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonReplaceHistory.Name = "buttonReplaceHistory";
            this.buttonReplaceHistory.Size = new System.Drawing.Size(17, 19);
            this.buttonReplaceHistory.TabIndex = 3;
            this.buttonReplaceHistory.UseVisualStyleBackColor = true;
            this.buttonReplaceHistory.Click += new System.EventHandler(this.buttonReplaceHistory_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fin&d:";
            // 
            // buttonFindHistory
            // 
            this.buttonFindHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindHistory.Image = global::phdesign.NppToolBucket.Properties.Resources.calendar_small_month;
            this.buttonFindHistory.Location = new System.Drawing.Point(372, 30);
            this.buttonFindHistory.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonFindHistory.Name = "buttonFindHistory";
            this.buttonFindHistory.Size = new System.Drawing.Size(17, 19);
            this.buttonFindHistory.TabIndex = 1;
            this.buttonFindHistory.UseVisualStyleBackColor = true;
            this.buttonFindHistory.Click += new System.EventHandler(this.buttonFindHistory_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 112);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Options:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Re&place:";
            // 
            // textBoxReplace
            // 
            this.textBoxReplace.AcceptsReturn = true;
            this.textBoxReplace.AcceptsTab = true;
            this.textBoxReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxReplace.Location = new System.Drawing.Point(67, 70);
            this.textBoxReplace.Multiline = true;
            this.textBoxReplace.Name = "textBoxReplace";
            this.textBoxReplace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReplace.Size = new System.Drawing.Size(302, 34);
            this.textBoxReplace.TabIndex = 2;
            // 
            // textBoxFind
            // 
            this.textBoxFind.AcceptsReturn = true;
            this.textBoxFind.AcceptsTab = true;
            this.textBoxFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFind.Location = new System.Drawing.Point(67, 30);
            this.textBoxFind.Multiline = true;
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFind.Size = new System.Drawing.Size(302, 34);
            this.textBoxFind.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.checkBoxMatchCase, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxMatchWholeWord, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxSearchFromBegining, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxSearchBackwards, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxUseRegularExpression, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(67, 110);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(302, 75);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(3, 3);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(82, 17);
            this.checkBoxMatchCase.TabIndex = 4;
            this.checkBoxMatchCase.Text = "Match ca&se";
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchWholeWord
            // 
            this.checkBoxMatchWholeWord.AutoSize = true;
            this.checkBoxMatchWholeWord.Location = new System.Drawing.Point(3, 26);
            this.checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
            this.checkBoxMatchWholeWord.Size = new System.Drawing.Size(113, 17);
            this.checkBoxMatchWholeWord.TabIndex = 5;
            this.checkBoxMatchWholeWord.Text = "Match &whole word";
            this.checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // checkBoxSearchFromBegining
            // 
            this.checkBoxSearchFromBegining.AutoSize = true;
            this.checkBoxSearchFromBegining.Location = new System.Drawing.Point(154, 3);
            this.checkBoxSearchFromBegining.Name = "checkBoxSearchFromBegining";
            this.checkBoxSearchFromBegining.Size = new System.Drawing.Size(126, 17);
            this.checkBoxSearchFromBegining.TabIndex = 6;
            this.checkBoxSearchFromBegining.Text = "Search from &begining";
            this.checkBoxSearchFromBegining.UseVisualStyleBackColor = true;
            // 
            // checkBoxSearchBackwards
            // 
            this.checkBoxSearchBackwards.AutoSize = true;
            this.checkBoxSearchBackwards.Location = new System.Drawing.Point(154, 26);
            this.checkBoxSearchBackwards.Name = "checkBoxSearchBackwards";
            this.checkBoxSearchBackwards.Size = new System.Drawing.Size(115, 17);
            this.checkBoxSearchBackwards.TabIndex = 7;
            this.checkBoxSearchBackwards.Text = "Search bac&kwards";
            this.checkBoxSearchBackwards.UseVisualStyleBackColor = true;
            this.checkBoxSearchBackwards.CheckedChanged += new System.EventHandler(this.checkBoxSearchBackwards_CheckedChanged);
            // 
            // checkBoxUseRegularExpression
            // 
            this.checkBoxUseRegularExpression.AutoSize = true;
            this.checkBoxUseRegularExpression.Location = new System.Drawing.Point(3, 49);
            this.checkBoxUseRegularExpression.Name = "checkBoxUseRegularExpression";
            this.checkBoxUseRegularExpression.Size = new System.Drawing.Size(133, 17);
            this.checkBoxUseRegularExpression.TabIndex = 5;
            this.checkBoxUseRegularExpression.Text = "Use regular e&xpression";
            this.checkBoxUseRegularExpression.UseVisualStyleBackColor = true;
            this.checkBoxUseRegularExpression.CheckedChanged += new System.EventHandler(this.checkBoxUseRegularExpression_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Search in:";
            // 
            // comboBoxSearchIn
            // 
            this.comboBoxSearchIn.FormattingEnabled = true;
            this.comboBoxSearchIn.Location = new System.Drawing.Point(67, 3);
            this.comboBoxSearchIn.Name = "comboBoxSearchIn";
            this.comboBoxSearchIn.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSearchIn.TabIndex = 8;
            // 
            // FindAndReplaceForm
            // 
            this.AcceptButton = this.buttonFindNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(490, 204);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "FindAndReplaceForm";
            this.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multiline Find and Replace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindAndReplaceForm_FormClosing);
            this.Shown += new System.EventHandler(this.FindAndReplaceForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonFindNext;
        private System.Windows.Forms.Button buttonFindAll;
        private System.Windows.Forms.Button buttonCount;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonReplaceAll;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxReplace;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox checkBoxMatchCase;
        private System.Windows.Forms.CheckBox checkBoxMatchWholeWord;
        private System.Windows.Forms.CheckBox checkBoxSearchFromBegining;
        private System.Windows.Forms.CheckBox checkBoxSearchBackwards;
        private System.Windows.Forms.CheckBox checkBoxUseRegularExpression;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSearchIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReplaceHistory;
        private System.Windows.Forms.Button buttonFindHistory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}