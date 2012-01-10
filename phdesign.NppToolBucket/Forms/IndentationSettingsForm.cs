using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
