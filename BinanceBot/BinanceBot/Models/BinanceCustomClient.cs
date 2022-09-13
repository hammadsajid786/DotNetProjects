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
using Microsoft.Win32;
using BinanceBot.Models.CustomEnums;

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
            _binanceApiKey = Program.binanceAPIKey;
            _binanceAPISecret = Program.binanceAPISecret;

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
               (tradePair, OrderSide.Sell, SpotOrderType.Market, null, sellPriceBUSD, null, null, null, null, null, null, null, 10000);

            if (orderMarketSellDetails.Success) // 1st Order
            {
                decimal quantiyFilled = orderMarketSellDetails.Data.QuantityFilled;
                decimal priceSell = orderMarketSellDetails.Data.AverageFillPrice.Value;

                decimal pricePurchased = Math.Round(priceSell - purchaseMargin);

                WebCallResult<BinancePlacedOrder> orderLimitBuyDetails = await _client.SpotApi.Trading.PlaceOrderAsync
                (tradePair, OrderSide.Buy, SpotOrderType.Limit, quantiyFilled, null, null, pricePurchased, TimeInForce.GoodTillCanceled, null, null, null, null, 10000);

                if (orderLimitBuyDetails.Success) // 2nd Order
                {
                    isSuccess = true;
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

                if (message != Models.CustomEnums.Messages.InsufficientBalance
                    &&
                    !message.Contains(Models.CustomEnums.Messages.InternetConnectionIssue)
                    )
                {
                    string textToWrite = DateTime.Now + Environment.NewLine
                                             + "Order Type           : " + SpotOrderType.Market.ToString() + Environment.NewLine
                                             + "Order Side           : " + OrderSide.Sell.ToString() + Environment.NewLine
                                             + "Not fulfilled Reason : " + orderMarketSellDetails.Error.Message + Environment.NewLine
                                             ;

                    fileLoggingNonSuccessOrders.WriteToFile(textToWrite);
                }
            }

            return new Tuple<bool, string>(isSuccess, message);
        }

        public async Task<Tuple<bool, string>> BuyMarketThenSellLimitOrder(string tradePair, decimal buyPriceBUSD, decimal purchaseMargin)
        {
            bool isSuccess = false;
            string message = string.Empty;

            WebCallResult<BinancePlacedOrder> orderMarketBuyDetails = await _client.SpotApi.Trading.PlaceOrderAsync
               (tradePair, OrderSide.Buy, SpotOrderType.Market, null, buyPriceBUSD, null, null, null, null, null, null, null, 10000);

            if (orderMarketBuyDetails.Success) // 1st Order
            {
                decimal quantiyFilled = orderMarketBuyDetails.Data.QuantityFilled;
                decimal priceSell = orderMarketBuyDetails.Data.AverageFillPrice.Value;

                decimal pricePurchased = Math.Round(priceSell + purchaseMargin);

                WebCallResult<BinancePlacedOrder> orderLimitSellDetails = await _client.SpotApi.Trading.PlaceOrderAsync
                (tradePair, OrderSide.Sell, SpotOrderType.Limit, quantiyFilled, null, null, pricePurchased, TimeInForce.GoodTillCanceled, null, null, null, null, 10000);

                if (orderLimitSellDetails.Success) // 2nd Order
                {
                    isSuccess = true;
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

                if (message != Models.CustomEnums.Messages.InsufficientBalance
                    &&
                    !message.Contains(Models.CustomEnums.Messages.InternetConnectionIssue)
                    )
                {

                    string textToWrite = DateTime.Now + Environment.NewLine
                                         + "Order Type           : " + SpotOrderType.Market.ToString() + Environment.NewLine
                                         + "Order Order Side     : " + OrderSide.Buy.ToString() + Environment.NewLine
                                         + "Not fulfilled Reason : " + orderMarketBuyDetails.Error.Message + Environment.NewLine
                                         ;

                    fileLoggingNonSuccessOrders.WriteToFile(textToWrite);
                }
            }

            return new Tuple<bool, string>(isSuccess, message);
        }

        public async Task<List<OpenOrderCustomModel>> FetchOpenOrders(string tradePair)
        {
            WebCallResult<IEnumerable<BinanceOrder>> openOrders = await _client.SpotApi.Trading.GetOpenOrdersAsync(tradePair, 60000);

            List<OpenOrderCustomModel> ordersList = new List<OpenOrderCustomModel>();

            if (openOrders.Success)
            {
                foreach (var openOrder in openOrders.Data)
                {
                    ordersList.Add(new OpenOrderCustomModel()
                    {
                        Symbol = openOrder.Symbol,
                        OrderId = openOrder.Id,
                        ClientOrderId = openOrder.Id,
                        Price = openOrder.Price,
                        Quantity = openOrder.Quantity,
                        OrderType = openOrder.Type.ToString() == "Limit" ? SpotOrderType.Limit : SpotOrderType.Market,
                        OrderSide = openOrder.Side == OrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                        CreatedTime = openOrder.CreateTime,
                        UpdatedTime = openOrder.UpdateTime
                    });
                }
            }

            return ordersList.OrderBy(x=>x.Price).ToList();
        }
    }
}
