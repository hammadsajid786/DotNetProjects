using Binance.Net.Enums;
using BinanceBot.Models;
using CryptoExchange.Net.CommonObjects;
using System.Data;

namespace BinanceBot
{
    public partial class OpenOrders : Form
    {
        private BinanceCustomClient _binanceCustomClient;

        public OpenOrders()
        {
            InitializeComponent();

            cbOrderPairs.SelectedIndex = 0;

            cbOrderSide.SelectedIndex = 0;

            _binanceCustomClient = new BinanceCustomClient();
        }

        private async void btnFetchOpenOrders_Click(object sender, EventArgs e)
        {
            EnableDisableFields(false);

            _ = Task.Run(async () =>
            {
                await fetchOrders();

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
                cbOrderSide.Enabled = enableFlag;

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

                if (quantityFilled == 0) //TODO: It needs to be check if There is quantity filled and Order quantity was different, This condition is added for the moment to came to know abou that order.
                {
                    if (!await _binanceCustomClient.CancelOpenOrder(symbol, orderId))
                    {
                        break; // Break the loop if an order not executed successfully.
                    }
                }
            }

            await fetchOrders();

            EnableDisableFields(true);
        }

        private async Task fetchOrders()
        {
            bool isCbPriceRangeChecked = false;
            decimal prFrom = 0;
            decimal prTo = 0;
            string tradePair = string.Empty;
            string orderSide = string.Empty;

            this.Invoke(() =>
            {
                lblRecordsCount.Text = "0";

                isCbPriceRangeChecked = cbPriceRange.Checked;
                prFrom = string.IsNullOrEmpty(nuPRFrom.Text) ? 0 : decimal.Parse(nuPRFrom.Text);
                prTo = string.IsNullOrEmpty(nuPRTo.Text) ? 0 : decimal.Parse(nuPRTo.Text);
                tradePair = cbOrderPairs.SelectedItem.ToString();
                orderSide = cbOrderSide.SelectedItem.ToString();
            });

            List<OpenOrderCustomModel> orders = await _binanceCustomClient.FetchOpenOrders(tradePair);

            if (!orderSide.Equals("ALL"))
            {
                orders = orders.Where(x => x.OrderSide == Enum.Parse<OrderSide>(orderSide)).ToList();
            }

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

            openOrderGV.Invoke((MethodInvoker)delegate
            {
                bsOpenOrderGV.DataSource = orders.ToDataTable();
                openOrderGV.DataSource = bsOpenOrderGV;
                openOrderGV.Refresh();

            });

            lblRecordsCount.Invoke(() =>
            {
                lblRecordsCount.Text = orders.Count.ToString();
            });

            btnCancelOpenOrders.Invoke(() =>
            {
                if (orders.Count > 0)
                    btnCancelOpenOrders.Visible = true;
                else
                    btnCancelOpenOrders.Visible = false;
            });

        }

        private void openOrderGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int rowsLeftInGridCount = bsOpenOrderGV.Count;
            lblRecordsCount.Text = rowsLeftInGridCount.ToString();
            if (rowsLeftInGridCount == 0)
            {
                btnCancelOpenOrders.Visible = false;
            }
        }
    }
}
