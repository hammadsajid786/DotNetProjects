using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot.Db
{
    public class BinanceDbContext : DbContext
    {
        public DbSet<OrdersToBeExecuted> OrdersToBeExecuteds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-QI7TCIK;Initial Catalog=BinanceDb;Integrated Security=True;");
        }
    }
}
