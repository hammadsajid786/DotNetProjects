using Binance.Net.Enums;
using BinanceBot.Models.CustomEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot.Models
{
    internal class OpenOrderCustomModel
    {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public Binance.Net.Enums.SpotOrderType OrderType { get; set; }
        public Binance.Net.Enums.OrderSide OrderSide { get; set; }
        public DateTime CreatedTime { get; set; }
        public long ClientOrderId { get; set; }
        public decimal QuantityFilled { get; set; }
    }
}
