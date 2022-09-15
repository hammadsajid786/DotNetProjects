using BinanceBot.Models;
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
    public partial class OrdersToBeExecuted : Form
    {
        private BinanceCustomClient _binanceCustomClient;

        public OrdersToBeExecuted()
        {
            InitializeComponent();

            cbOrderPairs.SelectedIndex = 0;

            cbOrderSide.SelectedIndex = 0;

            _binanceCustomClient = new BinanceCustomClient();
        }


    }
}
