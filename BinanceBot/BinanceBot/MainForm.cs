using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceBot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            mIPlaceOrder.PerformClick();
        }

        private void mIPlaceOrder_Click(object sender, EventArgs e)
        {
            PlaceOrders mainScreen = new PlaceOrders();
            mainScreen.MdiParent = this;
            mainScreen.MaximizeBox = true;
            mainScreen.Show();
        }
    }
}
