using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindingNationalityGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            pictureBox1.BringToFront();
            this.AllowDrop = true;

            imagesDropInitialPoint = new Point(337, 0);

            imagesMoveXPoint = 0;

        }

        private Point MouseDownLocation;

        private Point imagesDropInitialPoint;
        private Point imageMovePoints;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1.Left = e.X + pictureBox1.Left - MouseDownLocation.X;
                pictureBox1.Top = e.Y + pictureBox1.Top - MouseDownLocation.Y;
                panel1.BackColor = Color.LightBlue;
            }
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            MessageBox.Show("something just leave me");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Top = pictureBox1.Top + 10;
        }
    }
}
