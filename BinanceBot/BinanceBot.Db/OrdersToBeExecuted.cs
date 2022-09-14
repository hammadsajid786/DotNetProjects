using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot.Db
{
    public class OrdersToBeExecuted
    {
        [Key]
        public long Id { get; set; }
        
        [Required]
        public string Symbol { get; set; }

        [Precision(18, 8)]
        public decimal Price { get; set; }

        [Required]
        [Precision(18,8)]
        public decimal Quantity { get; set; }
        
        [Required]
        public string OrderType { get; set; }
        
        [Required]
        public string OrderSide { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        [Precision(18, 8)]
        public decimal QuantityFilled { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
