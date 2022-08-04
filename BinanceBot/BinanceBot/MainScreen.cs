
using Binance.Net.Clients;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Enums;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Authentication;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects;
using BinanceBot.Models;

namespace BinanceBot
{
    public partial class MainScreen : Form
    {
        private BinanceCustomClient _binanceCustomClient;

        private decimal totalBUSDBuyBMSL = 0;

        public MainScreen()
        {
            InitializeComponent();

            cbPairsMSLB.SelectedIndex = 0;
            cbPairsBMSL.SelectedIndex = 0;

            totalBUSDBuyBMSL = decimal.Parse(txtBUSDBuyBMSL.Text);

            _binanceCustomClient = new BinanceCustomClient();
        }

        private CancellationTokenSource cts = new CancellationTokenSource();

        private async void btnPlaceMarketOrderMSLB_Click(object sender, EventArgs e)
        {
            txtOrdersExecutedSMBL.Text = "0";

            EnableDisableFields(false);

            string tradePair = cbPairsMSLB.SelectedItem.ToString();
            decimal sellPriceBUSD = 50; // Override in ValidateSellPrice method
            decimal purchaseMargin = 10; // Override in ValidateSellPrice method

            int maxOrderCount = int.Parse(nUpDownControlSMBL.Value.ToString());

            if (!ValidateSMBL(out sellPriceBUSD, out purchaseMargin))
            {
                EnableDisableFields(true);
                return;
            }

            int ordersExecuted = 0;

            for (int i = 1; i <= maxOrderCount; i++)
            {
                Tuple<bool, string> tupleResults = await _binanceCustomClient.SellMarketThenBuyLimitOrder(tradePair, sellPriceBUSD, purchaseMargin);

                if (!tupleResults.Item1)
                {
                    if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.InsufficientBalance))
                    {
                    }
                    else if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.PurchaseOrderNotCreated))
                    {
                        MessageBox.Show(Models.CustomEnums.Messages.PurchaseOrderNotCreated);
                    }
                    else if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.InvalidAPIKeyIPOrPermissions))
                    {
                        MessageBox.Show(Models.CustomEnums.Messages.InvalidAPIKeyIPOrPermissions);
                    }

                    break;
                }
                else
                    ordersExecuted++;


                //Thread.Sleep(2000); // Wait for 2 seconds.
            }

            txtOrdersExecutedSMBL.Text = ordersExecuted.ToString();

            EnableDisableFields(true);
        }

        private void EnableDisableFields(bool enableFlag)
        {
            btnPlaceMarketOrderMSLB.Enabled = enableFlag;
            cbPairsMSLB.Enabled = enableFlag;
            txtBUSDSellMSLB.Enabled = enableFlag;
            txtPurchaseMarginMSLB.Enabled = enableFlag;
            nUpDownControlSMBL.Enabled = enableFlag;

            btnMarketBuyLimitSell.Enabled = enableFlag;
            cbPairsBMSL.Enabled = enableFlag;
            txtBUSDBuyBMSL.Enabled = enableFlag;
            txtSellMarginBMSL.Enabled = enableFlag;
            nUpDownControlBMSL.Enabled = enableFlag;
        }

        private async void btnMarketBuyLimitSell_Click(object sender, EventArgs e)
        {
            txtOrdersExecutedBMSL.Text = "0";

            EnableDisableFields(false);

            string tradePair = cbPairsBMSL.SelectedItem.ToString();
            decimal buyPriceBUSD = 50; // Override in ValidateSellPrice method
            decimal purchaseMargin = 5; // Override in ValidateSellPrice method

            int maxOrderCount = int.Parse(nUpDownControlBMSL.Value.ToString());

            if (!ValidateBMSL(out buyPriceBUSD, out purchaseMargin))
            {
                EnableDisableFields(true);
                return;
            }

            int ordersExecuted = 0;

            for (int i = 1; i <= maxOrderCount; i++)
            {
                Tuple<bool, string> tupleResults = await _binanceCustomClient.BuyMarketThenSellLimitOrder(tradePair, buyPriceBUSD, purchaseMargin);

                if (!tupleResults.Item1)
                {
                    if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.InsufficientBalance))
                    {
                    }
                    else if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.SellOrderNotCreated))
                    {
                        MessageBox.Show(Models.CustomEnums.Messages.SellOrderNotCreated);
                    }
                    else if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.InvalidAPIKeyIPOrPermissions))
                    {
                        MessageBox.Show(Models.CustomEnums.Messages.InvalidAPIKeyIPOrPermissions);
                    }

                    break;
                }
                else
                    ordersExecuted++;

                //Thread.Sleep(500); // Wait for 2 seconds.
            }

            txtOrdersExecutedBMSL.Text = ordersExecuted.ToString();

            EnableDisableFields(true);
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
    }
}