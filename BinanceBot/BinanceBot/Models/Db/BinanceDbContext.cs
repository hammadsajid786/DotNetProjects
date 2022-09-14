using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot.Models.Db
{
    public  class BinanceDbContext : DbContext
    {
        public DbSet<OrdersToBeExecuted> ordersToBeExecuteds { get; set; }
    }
}
