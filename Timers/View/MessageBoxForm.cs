using System;
using System.Windows.Forms;

namespace CustomTimers.View
{
    public partial class MessageBoxForm : Form
    {
        public MessageBoxForm ()
        {
            InitializeComponent ();
        }

        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }

        private void BtnOkClick (object sender, EventArgs e)
        {
            Close ();
        }
    }
}