using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinExercise
{
    public partial class WinTracker : Form
    {
        private WinExcerciseForm parentForm;
        public WinTracker(WinExcerciseForm winExcerciseForm)
        {
            //winExcerciseForm.TopLevel = false;
            this.parentForm = winExcerciseForm;
            InitializeComponent();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            this.parentForm.setProgressValue(this.trackBar.Value);
        }

        private void WinTracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason==CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
