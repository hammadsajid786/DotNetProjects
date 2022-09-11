
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

        private int _binanceRequestOrdersLimit;

        public PlaceOrders()
        {
            InitializeComponent();

            cbOrderPairs.SelectedIndex = 0;

            totalBUSDBuyBMSL = decimal.Parse(txtBUSDBuyBMSL.Text);

            _binanceCustomClient = new BinanceCustomClient();
            _binanceRequestOrdersLimit = int.Parse(System.Configuration.ConfigurationManager.AppSettings["BinanceRequestOrdersLimit"]);
        }

        private void btnPlaceSellMarketBuyLimit_SMBL_Click(object sender, EventArgs e)
        {
            InitializeCancellationToken();

            labelOrdersExecutedSMBL.Text = "0";

            labelU2OSMBL.Text = "0";
            labelU2OSMBL.ForeColor = Color.Black;

            EnableDisableFields(false, OrderType.SellMarketBuyLimit);

            string tradePair = cbOrderPairs.SelectedItem.ToString();
            decimal sellPriceBUSD = 50; // Override in ValidateSellPrice method
            decimal purchaseMargin = 10; // Override in ValidateSellPrice method

            int maxOrderCount = int.Parse(nUpDownControlSMBL.Value.ToString());

            int threadSleepValue = cbSMBL.Checked ? Convert.ToInt32(ndlSleepSMBL.Value) : 0;

            if (!ValidateSMBL(out sellPriceBUSD, out purchaseMargin))
            {
                EnableDisableFields(true, OrderType.SellMarketBuyLimit);
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

                    if (threadSleepValue > 0 && i % 5 == 0 && i != maxOrderCount)
                    {
                        cancellationToken.WaitHandle.WaitOne(threadSleepValue); // Wait for * seconds after every 5 orders.
                    }

                    if (i % _binanceRequestOrdersLimit == 0)
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

                EnableDisableFields(true, OrderType.SellMarketBuyLimit);
            });
        }

        private void btnPlaceBuyMarketSellLimit_BMSL_Click(object sender, EventArgs e)
        {
            InitializeCancellationToken();

            labelOrdersExecutedBMSL.Text = "0";

            labelU2OBMSL.Text = "0";
            labelU2OBMSL.ForeColor = Color.Black;

            EnableDisableFields(false, OrderType.BuyMarketSellLimit);

            string tradePair = cbOrderPairs.SelectedItem.ToString();
            decimal buyPriceBUSD = 50; // Override in ValidateSellPrice method
            decimal purchaseMargin = 5; // Override in ValidateSellPrice method

            int maxOrderCount = int.Parse(nUpDownControlBMSL.Value.ToString());

            int threadSleepValue = cbBMSL.Checked ? Convert.ToInt32(nudSleepBMSL.Value) : 0;

            if (!ValidateBMSL(out buyPriceBUSD, out purchaseMargin))
            {
                EnableDisableFields(true, OrderType.BuyMarketSellLimit);
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

                    if (threadSleepValue > 0 && i % 5 == 0 && i != maxOrderCount)
                    {
                        cancellationToken.WaitHandle.WaitOne(threadSleepValue); // Wait for * seconds after every 5 orders.
                    }

                    if (i % _binanceRequestOrdersLimit == 0)
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

                EnableDisableFields(true, OrderType.BuyMarketSellLimit);
            });
        }

        private void EnableDisableFields(bool enableFlag, OrderType orderType)
        {
            this.Invoke((MethodInvoker)delegate
            {
                btnPlaceOrderSMBL.Enabled = enableFlag;
                cbOrderPairs.Enabled = enableFlag;
                txtBUSDSellSMBL.Enabled = enableFlag;
                txtPurchaseMarginSMBL.Enabled = enableFlag;
                nUpDownControlSMBL.Enabled = enableFlag;

                btnPlaceOrderBMSL.Enabled = enableFlag;
                txtBUSDBuyBMSL.Enabled = enableFlag;
                txtSellMarginBMSL.Enabled = enableFlag;
                nUpDownControlBMSL.Enabled = enableFlag;

                cbBMSL.Enabled = enableFlag;
                cbSMBL.Enabled = enableFlag;

                if (!enableFlag) // Disable fields flag
                {
                    if (orderType == OrderType.SellMarketBuyLimit)
                    {
                        btnStopBMSL.Enabled = enableFlag;
                        btnStopSMBL.Enabled = !enableFlag;

                        pbMSBL.Visible = true;
                    }
                    else
                    {
                        btnStopBMSL.Enabled = !enableFlag;
                        btnStopSMBL.Enabled = enableFlag;

                        pbBMSL.Visible = true;
                    }

                    nudSleepBMSL.Enabled = false;
                    ndlSleepSMBL.Enabled = false;

                }
                else if (enableFlag) // Enable fields flag
                {
                    btnStopBMSL.Enabled = false;
                    btnStopSMBL.Enabled = false;

                    pbMSBL.Visible = false;
                    pbBMSL.Visible = false;

                    nudSleepBMSL.Enabled = false;
                    ndlSleepSMBL.Enabled = false;

                    nudSleepBMSL.Enabled = cbBMSL.Checked;
                    ndlSleepSMBL.Enabled = cbSMBL.Checked;
                }
            });
        }

        private bool ValidateSMBL(
           out decimal parsedSellPrice
         , out decimal purchasePriceMargin)
        {
            parsedSellPrice = 0m;
            purchasePriceMargin = 0m;

            // Sell Validations
            if (string.IsNullOrEmpty(txtBUSDSellSMBL.Text))
            {
                MessageBox.Show("Sell Qty required.");
                return false;
            }

            if (!decimal.TryParse(txtBUSDSellSMBL.Text, out parsedSellPrice))
            {
                MessageBox.Show("Sell Qty Invalid.");
                return false;
            }

            if (parsedSellPrice < 1 || parsedSellPrice > 500)
            {
                MessageBox.Show("Sell Qty Must be in between 1-500 for safety.");
                return false;
            }

            if (string.IsNullOrEmpty(txtPurchaseMarginSMBL.Text))
            {
                MessageBox.Show("Purchase Margin required.");
                return false;
            }

            // Purchase Validations
            if (!decimal.TryParse(txtPurchaseMarginSMBL.Text, out purchasePriceMargin))
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

        private void btnStopOrder_SMBL_Click(object sender, EventArgs e)
        {
            btnStopSMBL.Enabled = false;
            cancellationTokenSource.Cancel();
        }

        private void btnStopOrder_BMSL_Click(object sender, EventArgs e)
        {
            btnStopBMSL.Enabled = false;
            cancellationTokenSource.Cancel();
        }

        private void InitializeCancellationToken()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
        }

        private void cbSMBL_CheckedChanged(object sender, EventArgs e)
        {
            ndlSleepSMBL.Enabled = cbSMBL.Checked;
        }

        private void cbBMSL_CheckedChanged(object sender, EventArgs e)
        {
            nudSleepBMSL.Enabled = cbBMSL.Checked;
        }

    }
}