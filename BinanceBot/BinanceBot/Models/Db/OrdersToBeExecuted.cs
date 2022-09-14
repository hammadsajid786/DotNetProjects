using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot.Models.Db
{
    public class OrdersToBeExecuted
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        [Required]
        public string OrderType { get; set; }
        [Required]
        public string OrderSide { get; set; }
        public DateTime CreatedTime { get; set; }
        public long ClientOrderId { get; set; }
        public decimal QuantityFilled { get; set; }
    }
}
