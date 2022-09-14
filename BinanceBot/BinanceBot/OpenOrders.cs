using BinanceBot.Models;
using BinanceBot.Models.CustomEnums;
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
    public partial class OpenOrders : Form
    {
        private BinanceCustomClient _binanceCustomClient;

        public OpenOrders()
        {
            InitializeComponent();

            cbOrderPairs.SelectedIndex = 0;

            _binanceCustomClient = new BinanceCustomClient();
        }

        private void btnFetchOpenOrders_Click(object sender, EventArgs e)
        {
            EnableDisableFields(false);

            lblRecordsCount.Text = "0";

            bool isCbPriceRangeChecked = cbPriceRange.Checked;

            decimal prFrom = string.IsNullOrEmpty(nuPRFrom.Text) ? 0 : decimal.Parse(nuPRFrom.Text);
            decimal prTo = string.IsNullOrEmpty(nuPRTo.Text) ? 0 : decimal.Parse(nuPRTo.Text);

            string tradePair = cbOrderPairs.SelectedItem.ToString();

            _ = Task.Run(async () =>
            {
                List<OpenOrderCustomModel> orders = await _binanceCustomClient.FetchOpenOrders(tradePair);

                if (isCbPriceRangeChecked)
                {
                    orders = orders.Where(x => x.Price >= prFrom && x.Price <= prTo).ToList();
                }

                if (!isCbPriceRangeChecked && orders.Count > 0)
                {
                    this.Invoke(() =>
                    {
                        nuPRFrom.Text = orders.First().Price.ToString("0.##");
                        nuPRTo.Text = orders.Last().Price.ToString("0.##");
                    });
                }

                lblRecordsCount.Invoke(() =>
                {
                    lblRecordsCount.Text = orders.Count.ToString();
                });

                openOrderGV.Invoke((MethodInvoker)delegate
                {
                    openOrderGV.DataSource = orders;
                });

                EnableDisableFields(true);

            });
        }

        private void EnableDisableFields(bool enableFlag)
        {
            this.Invoke((MethodInvoker)delegate
            {
                cbOrderPairs.Enabled = enableFlag;
                btnFetchOpenOrders.Enabled = enableFlag;
                cbPriceRange.Enabled = enableFlag;

                openOrderGV.Enabled = enableFlag;

                if (cbPriceRange.Checked)
                {
                    nuPRFrom.Enabled = enableFlag;
                    nuPRTo.Enabled = enableFlag;
                }
                else
                {
                    nuPRFrom.Enabled = false;
                    nuPRTo.Enabled = false;
                }

            });
        }

        private void cbPriceRange_CheckedChanged(object sender, EventArgs e)
        {
            nuPRFrom.Enabled = cbPriceRange.Checked;
            nuPRTo.Enabled = cbPriceRange.Checked;
        }
    }
}
