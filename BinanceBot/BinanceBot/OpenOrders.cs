using BinanceBot.Db;
using BinanceBot.Models;
using BinanceBot.Models.CustomEnums;
using CryptoExchange.Net.CommonObjects;
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

        private async void btnFetchOpenOrders_Click(object sender, EventArgs e)
        {
            _ = Task.Run(async () =>
            {
                await fetchOrders();
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

                btnCancelOpenOrders.Enabled = enableFlag;

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

        private async void btnCancelOpenOrders_Click(object sender, EventArgs e)
        {
            EnableDisableFields(false);

            foreach (DataGridViewRow sRow in openOrderGV.Rows)
            {
                decimal quantityFilled = decimal.Parse(sRow.Cells[8].Value.ToString());
                long orderId = long.Parse(sRow.Cells[1].Value.ToString());
                string symbol = sRow.Cells[0].Value.ToString();

                if (quantityFilled == 0)
                {
                    await _binanceCustomClient.CancelOpenOrder(symbol,orderId);
                }
            }

            await fetchOrders();
        }

        private async Task fetchOrders()
        {
            EnableDisableFields(false);

            bool isCbPriceRangeChecked = false;
            decimal prFrom = 0;
            decimal prTo = 0;
            string tradePair = String.Empty;

            this.Invoke(() =>
            {
                lblRecordsCount.Text = "0";

                isCbPriceRangeChecked = cbPriceRange.Checked;
                prFrom = string.IsNullOrEmpty(nuPRFrom.Text) ? 0 : decimal.Parse(nuPRFrom.Text);
                prTo = string.IsNullOrEmpty(nuPRTo.Text) ? 0 : decimal.Parse(nuPRTo.Text);
                tradePair = cbOrderPairs.SelectedItem.ToString();
            });

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

            btnCancelOpenOrders.Invoke(() =>
            {
                if (orders.Count > 0)
                    btnCancelOpenOrders.Visible = true;
                else
                    btnCancelOpenOrders.Visible = false;
            });

            EnableDisableFields(true);

        }
    }
}
