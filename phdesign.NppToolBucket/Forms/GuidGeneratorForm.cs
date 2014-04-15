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
    public partial class GuidGeneratorForm : Form
    {
        public bool UseUppercase
        {
            get { return checkBoxUppercase.Checked; }
            set { checkBoxUppercase.Checked = value; }
        }

        public bool IncludeBraces
        {
            get { return checkBoxBraces.Checked; }
            set { checkBoxBraces.Checked = value; }
        }

        public bool IncludeHyphens
        {
            get { return checkBoxHyphens.Checked; }
            set { checkBoxHyphens.Checked = value; }
        }

        public int HowMany
        {
            get
            {
                int value;
                return Int32.TryParse(numericHowMany.Text, out value) ? value : 1;
            }
            set { numericHowMany.Text = value.ToString(); }
        }

        public GuidGeneratorForm()
        {
            InitializeComponent();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
