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
    public partial class WinExcerciseForm : Form
    {
        private WinTracker winTrackerForm;
        public WinExcerciseForm()
        {
            winTrackerForm = new WinTracker(this);
            InitializeComponent();
        }

        private void btnShowTracker_Click(object sender, EventArgs e)
        {
            winTrackerForm.ShowDialog(this);
        }

        public void setProgressValue(int progessValue)
        {
            progressBar.Invoke(new Action(() =>
            {
                progressBar.Value = progessValue;
            }));
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                Tuple<int, int> tuple = this.Compute(int.Parse(txtValue.Text));
                txtValSquared.Text = tuple.Item1.ToString();
                txtValCubed.Text = tuple.Item2.ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {
            this.lblInterpolatedName.Text = $"My name is {txtFName.Text} {txtLName.Text}";
        }
    }
}