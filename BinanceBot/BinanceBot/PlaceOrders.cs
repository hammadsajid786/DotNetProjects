
using Binance.Net.Clients;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Enums;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Authentication;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects;
using BinanceBot.Models;
using BinanceBot.Models.CustomEnums;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BinanceBot
{
    public partial class PlaceOrders : Form
    {
        private BinanceCustomClient _binanceCustomClient;
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        private decimal totalBUSDBuyBMSL = 0;

        public PlaceOrders()
        {
            InitializeComponent();

            cbOrderPairs.SelectedIndex = 0;

            totalBUSDBuyBMSL = decimal.Parse(txtBUSDBuyBMSL.Text);

            _binanceCustomClient = new BinanceCustomClient();
        }

        private void btnPlaceMarketOrderMSLB_Click(object sender, EventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            labelOrdersExecutedSMBL.Text = "0";

            labelU2OSMBL.Text = "0";
            labelU2OSMBL.ForeColor = Color.Black;

            EnableDisableFields(false, OrderType.MarketSellLimitBuy);

            string tradePair = cbOrderPairs.SelectedItem.ToString();
            decimal sellPriceBUSD = 50; // Override in ValidateSellPrice method
            decimal purchaseMargin = 10; // Override in ValidateSellPrice method

            int maxOrderCount = int.Parse(nUpDownControlSMBL.Value.ToString());
            int threadSleepValue = Convert.ToInt32(nUDSleepSMBL.Value);

            if (!ValidateSMBL(out sellPriceBUSD, out purchaseMargin))
            {
                EnableDisableFields(true, OrderType.MarketSellLimitBuy);
                return;
            }

            _ = Task.Run(async () =>
            {
                int ordersExecuted = 0;
                int secondOrderNotExecuted = 0;

                var tasksList = new List<Task>();

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                for (int i = 1; i <= maxOrderCount; i++)
                {
                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    tasksList.Add(Task.Run(async () =>
                    {
                        if (cancellationTokenSource.IsCancellationRequested)
                        {
                            return;
                        }

                        Tuple<bool, string> tupleResults = await _binanceCustomClient.SellMarketThenBuyLimitOrder(tradePair, sellPriceBUSD, purchaseMargin);

                        if (!tupleResults.Item1)
                        {
                            if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.PurchaseOrderNotCreated))
                            {
                                Interlocked.Increment(ref secondOrderNotExecuted);

                                labelU2OSMBL.Invoke((MethodInvoker)delegate
                                {
                                    labelU2OSMBL.Text = secondOrderNotExecuted.ToString();
                                    labelU2OSMBL.ForeColor = Color.Red;
                                });

                                return;
                            }

                            cancellationTokenSource.Cancel();

                            if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.InsufficientBalance)
                                ||
                                tupleResults.Item2.Contains(Models.CustomEnums.Messages.InternetConnectionIssue)
                                )
                            {
                            }
                            else if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.InvalidAPIKeyIPOrPermissions))
                            {
                                MessageBox.Show(Models.CustomEnums.Messages.InvalidAPIKeyIPOrPermissions);
                            }
                        }
                        else
                        {
                            Interlocked.Increment(ref ordersExecuted);

                            labelOrdersExecutedSMBL.Invoke((MethodInvoker)delegate
                            {
                                labelOrdersExecutedSMBL.Text = (ordersExecuted).ToString();
                            });
                        }
                    }));

                    if (i % 5 == 0 && i != maxOrderCount)
                    {
                        cancellationToken.WaitHandle.WaitOne(threadSleepValue); // Wait for * seconds after every 5 orders.
                    }

                    if (i % 25 == 0)
                    {
                        Task.WaitAll(tasksList.ToArray());
                        stopWatch.Stop();

                        int waitFor50RequestsToCompleteMilliSeconds = (10000 - stopWatch.Elapsed.Milliseconds);
                        if (waitFor50RequestsToCompleteMilliSeconds > 0)
                        {
                            cancellationToken.WaitHandle.WaitOne(waitFor50RequestsToCompleteMilliSeconds);
                        }

                        stopWatch.Reset();
                        stopWatch.Start();
                    }
                }

                try
                {
                    Task.WaitAll(tasksList.ToArray());
                }
                catch (AggregateException agExc)
                {
                    string exceptionMessage = agExc.Message;
                    for (int j = 0; j < agExc.InnerExceptions.Count; j++)
                        exceptionMessage += Environment.NewLine + agExc.InnerExceptions[j].ToString();

                    MessageBox.Show("Exceptions occured in waiting: " + exceptionMessage);
                }

                EnableDisableFields(true, OrderType.MarketSellLimitBuy);
            });
        }

        private void EnableDisableFields(bool enableFlag, OrderType orderType)
        {
            this.Invoke((MethodInvoker)delegate
            {
                btnPlaceMarketOrderMSLB.Enabled = enableFlag;
                cbOrderPairs.Enabled = enableFlag;
                txtBUSDSellMSLB.Enabled = enableFlag;
                txtPurchaseMarginMSLB.Enabled = enableFlag;
                nUpDownControlSMBL.Enabled = enableFlag;

                btnMarketBuyLimitSell.Enabled = enableFlag;
                txtBUSDBuyBMSL.Enabled = enableFlag;
                txtSellMarginBMSL.Enabled = enableFlag;
                nUpDownControlBMSL.Enabled = enableFlag;

                nUDSleepBMSL.Enabled = enableFlag;
                nUDSleepSMBL.Enabled = enableFlag;

                if (orderType == OrderType.MarketSellLimitBuy)
                {
                    btnStopMBLS.Enabled = enableFlag;
                    btnStopMSLB.Enabled = !enableFlag;
                }
                else
                {
                    btnStopMBLS.Enabled = !enableFlag;
                    btnStopMSLB.Enabled = enableFlag;
                }
            });
        }

        private void btnMarketBuyLimitSell_Click(object sender, EventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();

            labelOrdersExecutedBMSL.Text = "0";

            labelU2OBMSL.Text = "0";
            labelU2OBMSL.ForeColor = Color.Black;

            EnableDisableFields(false, OrderType.MarketBuyLimitSell);

            string tradePair = cbOrderPairs.SelectedItem.ToString();
            decimal buyPriceBUSD = 50; // Override in ValidateSellPrice method
            decimal purchaseMargin = 5; // Override in ValidateSellPrice method

            int maxOrderCount = int.Parse(nUpDownControlBMSL.Value.ToString());

            int threadSleepValue = Convert.ToInt32(nUDSleepBMSL.Value);

            if (!ValidateBMSL(out buyPriceBUSD, out purchaseMargin))
            {
                EnableDisableFields(true, OrderType.MarketBuyLimitSell);
                return;
            }

            _ = Task.Run(() =>
            {
                int ordersExecuted = 0;
                int secondOrderNotExecuted = 0;

                var tasksList = new List<Task>();

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                for (int i = 1; i <= maxOrderCount; i++)
                {
                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    tasksList.Add(Task.Run(async () =>
                    {
                        if (cancellationTokenSource.IsCancellationRequested)
                        {
                            return;
                        }

                        Tuple<bool, string> tupleResults = await _binanceCustomClient.BuyMarketThenSellLimitOrder(tradePair, buyPriceBUSD, purchaseMargin);

                        if (!tupleResults.Item1)
                        {
                            if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.SellOrderNotCreated))
                            {
                                Interlocked.Increment(ref secondOrderNotExecuted);

                                labelU2OBMSL.Invoke((MethodInvoker)delegate
                                {
                                    labelU2OBMSL.Text = secondOrderNotExecuted.ToString();
                                    labelU2OBMSL.ForeColor = Color.Red;
                                });

                                return;
                            }

                            cancellationTokenSource.Cancel();

                            if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.InsufficientBalance)
                                ||
                                tupleResults.Item2.Contains(Models.CustomEnums.Messages.InternetConnectionIssue)
                                )
                            {
                            }
                            else if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.InvalidAPIKeyIPOrPermissions))
                            {
                                MessageBox.Show(Models.CustomEnums.Messages.InvalidAPIKeyIPOrPermissions);
                            }
                        }
                        else
                        {
                            Interlocked.Increment(ref ordersExecuted);

                            labelOrdersExecutedBMSL.Invoke((MethodInvoker)delegate
                            {
                                labelOrdersExecutedBMSL.Text = (ordersExecuted).ToString();
                            });
                        }
                    }));

                    if (i % 5 == 0 && i != maxOrderCount)
                    {
                        cancellationToken.WaitHandle.WaitOne(threadSleepValue); // Wait for * seconds after every 5 orders.
                    }

                    if (i % 25 == 0)
                    {
                        Task.WaitAll(tasksList.ToArray());
                        stopWatch.Stop();

                        int waitFor50RequestsToCompleteMilliSeconds = (10000 - stopWatch.Elapsed.Milliseconds);
                        if (waitFor50RequestsToCompleteMilliSeconds > 0)
                        {
                            cancellationToken.WaitHandle.WaitOne(waitFor50RequestsToCompleteMilliSeconds);
                        }

                        stopWatch.Reset();
                        stopWatch.Start();
                    }
                }

                try
                {
                    Task.WaitAll(tasksList.ToArray());
                }
                catch (AggregateException agExc)
                {
                    string exceptionMessage = agExc.Message;
                    for (int j = 0; j < agExc.InnerExceptions.Count; j++)
                        exceptionMessage += Environment.NewLine + agExc.InnerExceptions[j].ToString();

                    MessageBox.Show("Exceptions occured in waiting: " + exceptionMessage);
                }

                EnableDisableFields(true, OrderType.MarketBuyLimitSell);
            });
        }

        private bool ValidateSMBL(
           out decimal parsedSellPrice
         , out decimal purchasePriceMargin)
        {
            parsedSellPrice = 0m;
            purchasePriceMargin = 0m;

            // Sell Validations
            if (string.IsNullOrEmpty(txtBUSDSellMSLB.Text))
            {
                MessageBox.Show("Sell Qty required.");
                return false;
            }

            if (!decimal.TryParse(txtBUSDSellMSLB.Text, out parsedSellPrice))
            {
                MessageBox.Show("Sell Qty Invalid.");
                return false;
            }

            if (parsedSellPrice < 1 || parsedSellPrice > 500)
            {
                MessageBox.Show("Sell Qty Must be in between 1-500 for safety.");
                return false;
            }

            if (string.IsNullOrEmpty(txtPurchaseMarginMSLB.Text))
            {
                MessageBox.Show("Purchase Margin required.");
                return false;
            }

            // Purchase Validations
            if (!decimal.TryParse(txtPurchaseMarginMSLB.Text, out purchasePriceMargin))
            {
                MessageBox.Show("Purchase Margin Invalid.");
                return false;
            }

            if (purchasePriceMargin < 1 || purchasePriceMargin > 50)
            {
                MessageBox.Show("Purchase price Margin Must be in between 1-50 for safety.");
                return false;
            }

            return true;
        }

        private bool ValidateBMSL(
              out decimal buyBUSDPrice
            , out decimal sellPriceMargin)
        {
            buyBUSDPrice = 0m;
            sellPriceMargin = 0m;

            // Sell Validations
            if (string.IsNullOrEmpty(txtBUSDBuyBMSL.Text))
            {
                MessageBox.Show("Buy Qty required.");
                return false;
            }

            if (!decimal.TryParse(txtBUSDBuyBMSL.Text, out buyBUSDPrice))
            {
                MessageBox.Show("Buy Qty Invalid.");
                return false;
            }

            if (string.IsNullOrEmpty(txtSellMarginBMSL.Text))
            {
                MessageBox.Show("Sell Margin required.");
                return false;
            }

            // Purchase Validations
            if (!decimal.TryParse(txtSellMarginBMSL.Text, out sellPriceMargin))
            {
                MessageBox.Show("Sell Margin Invalid.");
                return false;
            }

            if (sellPriceMargin < 1 || sellPriceMargin > 50)
            {
                MessageBox.Show("Purchase price Margin Must be in between 1-30 for safety.");
                return false;
            }

            return true;
        }

        private void txtBUSDBuyBMSL_Leave(object sender, EventArgs e)
        {
            decimal totalBUSDBuyBMSLNew = decimal.Parse(txtBUSDBuyBMSL.Text);

            if (totalBUSDBuyBMSL != totalBUSDBuyBMSLNew)
            {
                var dialogResult = MessageBox.Show("Are you sure you want to make Buy value :" + txtBUSDBuyBMSL.Text, "Are you sure?", MessageBoxButtons.YesNo);

                if (dialogResult != DialogResult.Yes)
                {
                    txtBUSDBuyBMSL.Focus();
                }
                else
                {
                    totalBUSDBuyBMSL = totalBUSDBuyBMSLNew; // Updating Cache value
                }
            }
        }

        private void btnStopMarketOrderMSLB_Click(object sender, EventArgs e)
        {
            btnStopMSLB.Enabled = false;
            cancellationTokenSource.Cancel();
        }

        private void btnStopMBLS_Click(object sender, EventArgs e)
        {
            btnStopMBLS.Enabled = false;
            cancellationTokenSource.Cancel();
        }

    }
}