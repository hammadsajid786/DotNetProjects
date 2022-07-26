
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
        public MainScreen()
        {
            InitializeComponent();
            cbPairsMSLB.SelectedIndex = 0;
            cbMaxOrderCountSMBL.SelectedIndex = 0;
        }

        private readonly string binanceApiKey = "laUY2nCE1LfjkqEemjdHzxPre09NoA1FfCfszbDC6fDd6QMoA87bEEEidiWb11UN";
        private readonly string biancneAPISecret = "7OYzJUEuJwEZIyZGLZfA8XVt4I1GYBTuTSWBLhjMEY6b9rA4GpTURd4kFLRWrABm";

        private CancellationTokenSource cts = new CancellationTokenSource();

        private async void btnPlaceMarketOrderMSLB_Click(object sender, EventArgs e)
        {
            txtOrdersExecuted.Text = "0";

            EnableDisableFields(false);

            string tradePair = cbPairsMSLB.SelectedItem.ToString();
            decimal sellPriceBUSD = 50; // Override in ValidateSellPrice method
            decimal purchaseMargin = 10; // Override in ValidateSellPrice method

            int maxOrderCount = int.Parse(cbMaxOrderCountSMBL.SelectedItem.ToString());

            if (!ValidateSellPrice(out sellPriceBUSD, out purchaseMargin))
            {
                EnableDisableFields(false);
                return;
            }

            BinanceCustomClient binanceCustomClient = new BinanceCustomClient();
            int ordersExecuted = maxOrderCount;

            for (int i = 1; i <= maxOrderCount; i++)
            {
                Tuple<bool, string> tupleResults = await binanceCustomClient.SellMarketThenBuyLimitOrder(tradePair, sellPriceBUSD, purchaseMargin);

                if (!tupleResults.Item1)
                {
                    if (tupleResults.Item2.Equals("Account has insufficient balance for requested action."))
                    {
                        ordersExecuted = i - 1;
                        //Thread.Sleep(10000); // Wait 10 seconds
                        break;
                    }

                    if (tupleResults.Item2.Equals(Models.CustomEnums.Messages.PurchaseOrderNotCreated))
                    {
                        ordersExecuted = i - 1;
                        MessageBox.Show(Models.CustomEnums.Messages.PurchaseOrderNotCreated);
                        break;
                    }
                }

                Thread.Sleep(2000); // Wait for 2 seconds.
            }

            txtOrdersExecuted.Text = ordersExecuted.ToString();

            EnableDisableFields(true);
        }

        private bool ValidateSellPrice(
              out decimal parsedSellPrice
            , out decimal purchasePriceMargin)
        {
            parsedSellPrice = 0m;
            purchasePriceMargin = 0m;

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

        private void EnableDisableFields(bool enable)
        {
            btnPlaceMarketOrderMSLB.Enabled = enable;
            cbPairsMSLB.Enabled = enable;
            txtBUSDSellMSLB.Enabled = enable;
            txtPurchaseMarginMSLB.Enabled = enable;
            cbMaxOrderCountSMBL.Enabled = enable;

            btnMarketBuyLimitSell.Enabled = enable;
        }
    }
}