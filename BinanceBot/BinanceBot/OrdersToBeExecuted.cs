using BinanceBot.Db;
using BinanceBot.Models;
using System.Data;

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

        private async void btnFetchOrdersFromDb_Click(object sender, EventArgs e)
        {
            EnableDisableFields(false);

            await fetchOrders();

            EnableDisableFields(true);
        }

        private void EnableDisableFields(bool enableFlag)
        {
            this.Invoke((MethodInvoker)delegate
            {
                cbOrderPairs.Enabled = enableFlag;
                cbOrderSide.Enabled = enableFlag;
                btnFetchOrdersFromDb.Enabled = enableFlag;
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

            List<Db.OrdersToBeExecuted> orders = new List<Db.OrdersToBeExecuted>();

            using (var dbContext = new BinanceDbContext())
            {
                var ordersQuery = dbContext.OrdersToBeExecuteds.AsQueryable().Where(x => x.Symbol == tradePair);

                if (!orderSide.Equals("ALL"))
                {
                    ordersQuery = ordersQuery.Where(x => x.OrderSide == orderSide);
                }

                if (isCbPriceRangeChecked)
                {
                    ordersQuery = ordersQuery.Where(x => x.Price >= prFrom && x.Price <= prTo);
                }

                ordersQuery = ordersQuery.OrderBy(x => x.Price);

                orders = ordersQuery.ToList();
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
                bSOpenGV.DataSource = orders.ToDataTable();
                openOrderGV.DataSource = bSOpenGV;
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

        private async void btnCancelOpenOrders_Click(object sender, EventArgs e)
        {
            EnableDisableFields(false);

            foreach (DataGridViewRow sRow in openOrderGV.Rows)
            {
                long orderId = long.Parse(sRow.Cells[0].Value.ToString());
                decimal quantityFilled = decimal.Parse(sRow.Cells[8].Value.ToString());

                if (quantityFilled == 0) //TODO: It needs to be check if There is quantity filled and Order quantity was different, This condition is added for the moment to came to know abou that order.
                {
                    bool isSuccessfullyExecuted = await _binanceCustomClient.CreateOrdersFromBacklog(orderId);

                    if (!isSuccessfullyExecuted)
                    {
                        break;
                    }
                }
            }

            await fetchOrders();

            EnableDisableFields(true);
        }

        private void openOrderGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int rowsLeftInGridCount = bSOpenGV.Count;
            lblRecordsCount.Text = rowsLeftInGridCount.ToString();
            if (rowsLeftInGridCount == 0)
            {
                btnCancelOpenOrders.Visible = false;
            }
        }
    }
}
