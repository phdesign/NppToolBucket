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
    partial class IndentationSettingsForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.checkBoxUseTabs = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxTabSize = new System.Windows.Forms.TextBox();
            this.textBoxIndentSize = new System.Windows.Forms.TextBox();
            this.labelTabSize = new System.Windows.Forms.Label();
            this.labelIndentSize = new System.Windows.Forms.Label();
            this.labelUseTabs = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(148, 3);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(148, 32);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(148, 61);
            this.buttonConvert.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(75, 23);
            this.buttonConvert.TabIndex = 5;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // checkBoxUseTabs
            // 
            this.checkBoxUseTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUseTabs.AutoSize = true;
            this.checkBoxUseTabs.Location = new System.Drawing.Point(120, 61);
            this.checkBoxUseTabs.Name = "checkBoxUseTabs";
            this.checkBoxUseTabs.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseTabs.TabIndex = 2;
            this.checkBoxUseTabs.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.buttonOK, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxUseTabs, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonCancel, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonConvert, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxTabSize, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxIndentSize, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelTabSize, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelIndentSize, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelUseTabs, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(226, 87);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // textBoxTabSize
            // 
            this.textBoxTabSize.Location = new System.Drawing.Point(105, 3);
            this.textBoxTabSize.Name = "textBoxTabSize";
            this.textBoxTabSize.Size = new System.Drawing.Size(30, 20);
            this.textBoxTabSize.TabIndex = 0;
            // 
            // textBoxIndentSize
            // 
            this.textBoxIndentSize.Location = new System.Drawing.Point(105, 32);
            this.textBoxIndentSize.Name = "textBoxIndentSize";
            this.textBoxIndentSize.Size = new System.Drawing.Size(30, 20);
            this.textBoxIndentSize.TabIndex = 1;
            // 
            // labelTabSize
            // 
            this.labelTabSize.AutoSize = true;
            this.labelTabSize.Location = new System.Drawing.Point(3, 5);
            this.labelTabSize.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelTabSize.Name = "labelTabSize";
            this.labelTabSize.Size = new System.Drawing.Size(52, 13);
            this.labelTabSize.TabIndex = 6;
            this.labelTabSize.Text = "Tab Size:";
            // 
            // labelIndentSize
            // 
            this.labelIndentSize.AutoSize = true;
            this.labelIndentSize.Location = new System.Drawing.Point(3, 34);
            this.labelIndentSize.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelIndentSize.Name = "labelIndentSize";
            this.labelIndentSize.Size = new System.Drawing.Size(63, 13);
            this.labelIndentSize.TabIndex = 7;
            this.labelIndentSize.Text = "Indent Size:";
            // 
            // labelUseTabs
            // 
            this.labelUseTabs.AutoSize = true;
            this.labelUseTabs.Location = new System.Drawing.Point(3, 63);
            this.labelUseTabs.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelUseTabs.Name = "labelUseTabs";
            this.labelUseTabs.Size = new System.Drawing.Size(56, 13);
            this.labelUseTabs.TabIndex = 8;
            this.labelUseTabs.Text = "Use Tabs:";
            // 
            // IndentationSettingsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(234, 97);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IndentationSettingsForm";
            this.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Indentation Settings";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.CheckBox checkBoxUseTabs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxTabSize;
        private System.Windows.Forms.TextBox textBoxIndentSize;
        private System.Windows.Forms.Label labelTabSize;
        private System.Windows.Forms.Label labelIndentSize;
        private System.Windows.Forms.Label labelUseTabs;
    }
}