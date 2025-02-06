using System;
using System.Windows.Forms;

namespace R36_Linux_Partition_Sizer
{
    public partial class frmHelpAndInfo : Form
    {
        public frmHelpAndInfo()
        {
            InitializeComponent();
        }

        private void frmHelpAndInfo_Load(object sender, EventArgs e)
        {
            // Just simply loading a RichTextFormat (.rtf) file directly from the Embedded Resources into the RichTextBox
            richTextBox1.Rtf = Properties.Resources.help;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
