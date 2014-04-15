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
