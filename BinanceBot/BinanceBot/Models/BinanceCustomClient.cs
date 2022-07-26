using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot.Models
{
    internal class BinanceCustomClient
    {
        private readonly string _binanceApiKey;
        private readonly string _binanceAPISecret;

        private BinanceClient _client;
        public BinanceCustomClient()
        {
            _binanceApiKey = System.Configuration.ConfigurationManager.AppSettings["binanceApiKey"];
            _binanceAPISecret = System.Configuration.ConfigurationManager.AppSettings["biancneAPISecret"];

            _client = new BinanceClient();
            _client.SetApiCredentials(new ApiCredentials(_binanceApiKey, _binanceAPISecret));
        }

        public async Task<Tuple<bool, string>> SellMarketThenBuyLimitOrder(string tradePair, decimal sellPriceBUSD, decimal purchaseMargin)
        {
            bool isSuccess = false;
            string message = string.Empty;

            WebCallResult<BinancePlacedOrder> orderMarketSellDetails = await _client.SpotApi.Trading.PlaceOrderAsync
               (tradePair, OrderSide.Sell, SpotOrderType.Market, null, sellPriceBUSD);

            if (orderMarketSellDetails.Success)
            {
                decimal quantiyFilled = orderMarketSellDetails.Data.QuantityFilled;
                decimal priceSell = orderMarketSellDetails.Data.AverageFillPrice.Value;

                decimal pricePurchased = Math.Round(priceSell - purchaseMargin);

                WebCallResult<BinancePlacedOrder> orderLimitBuyDetails = await _client.SpotApi.Trading.PlaceOrderAsync
                (tradePair, OrderSide.Buy, SpotOrderType.Limit, quantiyFilled, null, null, pricePurchased, TimeInForce.GoodTillCanceled);

                if (orderLimitBuyDetails.Success)
                {
                    isSuccess = true;

                    //MessageBox.Show("Order filled of Quanity: " + quantiyFilled + "BTC"
                    //    + Environment.NewLine + "BUSD: " + Math.Round(priceSell,2)
                    //    + Environment.NewLine + "Bought BTC at: " + pricePurchased);
                }
                else
                {
                    message = CustomEnums.Messages.PurchaseOrderNotCreated;
                }
            }
            else
            {
                message = orderMarketSellDetails.Error.Message;
            }

            return new Tuple<bool, string>(isSuccess, message);
        }
    }
}
