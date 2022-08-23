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
        private MultiThreadFileWriter fileLogging;
        private MultiThreadFileWriter fileLoggingNonSuccessOrders;

        public BinanceCustomClient()
        {
            _binanceApiKey = System.Configuration.ConfigurationManager.AppSettings["binanceApiKey"];
            _binanceAPISecret = System.Configuration.ConfigurationManager.AppSettings["biancneAPISecret"];

            _client = new BinanceClient();
            _client.SetApiCredentials(new ApiCredentials(_binanceApiKey, _binanceAPISecret));

            fileLogging = new MultiThreadFileWriter();
            fileLoggingNonSuccessOrders = new MultiThreadFileWriter("FileLoggingPathNotSuccessOrders");
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
                    string textToWrite = DateTime.Now + Environment.NewLine
                                         + "1st Order Created Time    : " + orderMarketSellDetails.Data.CreateTime + Environment.NewLine
                                         + "1st Order Price Sell      : " + priceSell + Environment.NewLine
                                         + "1st Order Quantity Filled : " + quantiyFilled + Environment.NewLine
                                         + "2nd Order Type            : " + SpotOrderType.Limit.ToString() + Environment.NewLine
                                         + "2nd Order Order Side      : " + OrderSide.Buy.ToString() + Environment.NewLine
                                         + "2nd Order Trade Pair      : " + tradePair + Environment.NewLine
                                         + "2nd Price to be Purchased : " + pricePurchased + Environment.NewLine
                                         + "Not fulfilled Reason      : " + orderLimitBuyDetails.Error.Message + Environment.NewLine
                                         ;

                    fileLogging.WriteToFile(textToWrite);

                    message = CustomEnums.Messages.PurchaseOrderNotCreated;
                }
            }
            else
            {
                message = orderMarketSellDetails.Error.Message;

                string textToWrite = DateTime.Now + Environment.NewLine
                                         + "Order Type           : " + SpotOrderType.Market.ToString() + Environment.NewLine
                                         + "2nd Order Order Side : " + OrderSide.Sell.ToString() + Environment.NewLine
                                         + "Not fulfilled Reason : " + orderMarketSellDetails.Error.Message + Environment.NewLine
                                         ;

                fileLoggingNonSuccessOrders.WriteToFile(textToWrite);
            }

            return new Tuple<bool, string>(isSuccess, message);
        }

        public async Task<Tuple<bool, string>> BuyMarketThenSellLimitOrder(string tradePair, decimal buyPriceBUSD, decimal purchaseMargin)
        {
            bool isSuccess = false;
            string message = string.Empty;

            WebCallResult<BinancePlacedOrder> orderMarketBuyDetails = await _client.SpotApi.Trading.PlaceOrderAsync
               (tradePair, OrderSide.Buy, SpotOrderType.Market, null, buyPriceBUSD);

            if (orderMarketBuyDetails.Success)
            {
                decimal quantiyFilled = orderMarketBuyDetails.Data.QuantityFilled;
                decimal priceSell = orderMarketBuyDetails.Data.AverageFillPrice.Value;

                decimal pricePurchased = Math.Round(priceSell + purchaseMargin);

                WebCallResult<BinancePlacedOrder> orderLimitSellDetails = await _client.SpotApi.Trading.PlaceOrderAsync
                (tradePair, OrderSide.Sell, SpotOrderType.Limit, quantiyFilled, null, null, pricePurchased, TimeInForce.GoodTillCanceled);

                if (orderLimitSellDetails.Success)
                {
                    isSuccess = true;

                    //MessageBox.Show("Order filled of Quanity: " + quantiyFilled + "BTC"
                    //    + Environment.NewLine + "BUSD: " + Math.Round(priceSell,2)
                    //    + Environment.NewLine + "Bought BTC at: " + pricePurchased);
                }
                else
                {
                    string textToWrite = DateTime.Now + Environment.NewLine
                                         + "1st Order Created Time    : " + orderMarketBuyDetails.Data.CreateTime + Environment.NewLine
                                         + "1st Order Price Sell      : " + priceSell + Environment.NewLine
                                         + "1st Order Quantity Filled : " + quantiyFilled + Environment.NewLine
                                         + "2nd Order Type            : " + SpotOrderType.Limit.ToString() + Environment.NewLine
                                         + "2nd Order Order Side      : " + OrderSide.Sell.ToString() + Environment.NewLine
                                         + "2nd Order Trade Pair      : " + tradePair + Environment.NewLine
                                         + "2nd Price to be Purchased : " + pricePurchased + Environment.NewLine
                                         + "Not fulfilled Reason      : " + orderLimitSellDetails.Error.Message + Environment.NewLine
                                         ;

                    fileLogging.WriteToFile(textToWrite);

                    message = CustomEnums.Messages.SellOrderNotCreated;
                }
            }
            else
            {
                message = orderMarketBuyDetails.Error.Message;

                string textToWrite = DateTime.Now + Environment.NewLine
                                         + "Order Type           : " + SpotOrderType.Market.ToString() + Environment.NewLine
                                         + "Order Order Side     : " + OrderSide.Buy.ToString() + Environment.NewLine
                                         + "Not fulfilled Reason : " + orderMarketBuyDetails.Error.Message + Environment.NewLine
                                         ;

                fileLoggingNonSuccessOrders.WriteToFile(textToWrite);

            }

            return new Tuple<bool, string>(isSuccess, message);
        }

    }
}
