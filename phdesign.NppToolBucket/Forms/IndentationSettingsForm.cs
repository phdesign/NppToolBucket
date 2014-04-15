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
using System.Windows.Forms;

namespace phdesign.NppToolBucket.Forms
{
    public partial class IndentationSettingsForm : Form
    {
        public bool ConvertIndent { get; set; }

        public bool UseTabs
        {
            get { return checkBoxUseTabs.Checked; }
            set { checkBoxUseTabs.Checked = value; }
        }

        public int TabSize
        {
            get
            {
                int value;
                return Int32.TryParse(textBoxTabSize.Text, out value) ? value : -1;
            }
            set { textBoxTabSize.Text = value.ToString(); }
        }

        public int IndentSize
        {
            get
            {
                int value;
                return Int32.TryParse(textBoxIndentSize.Text, out value) ? value : -1;
            }
            set { textBoxIndentSize.Text = value.ToString(); }
        }

        public IndentationSettingsForm()
        {
            InitializeComponent();
            ConvertIndent = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            ConvertIndent = true;
            DialogResult = DialogResult.OK;
        }
    }
}
