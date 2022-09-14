using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot.Models.CustomEnums
{
    internal class Messages
    {
        public static readonly string InsufficientBalance = "Account has insufficient balance for requested action.";
        public static readonly string PurchaseOrderNotCreated = "Purchase order not created.";
        public static readonly string SellOrderNotCreated = "Sell order not created.";
        public static readonly string InvalidAPIKeyIPOrPermissions = "Invalid API-key, IP, or permissions for action.";
        public static readonly string InternetConnectionIssue = "HttpRequestException - No such host is known. (api.binance.com:443)";

        public static readonly string OpenOrderCancelReasonByBot = "Order cancelled from Binance Bot";
    }
}
