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
            PlaceOrders placeOrders = new PlaceOrders();
            placeOrders.MdiParent = this;
            placeOrders.MaximizeBox = true;
            placeOrders.Show();
        }

        private void openOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenOrders openOrders = new OpenOrders();
            openOrders.MdiParent = this;
            openOrders.Show();
        }

        private void ordersToExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdersToBeExecuted ordersToBeExecuted = new OrdersToBeExecuted();
            ordersToBeExecuted.MdiParent = this;
            ordersToBeExecuted.Show();
        }
    }
}
