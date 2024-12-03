using Microsoft.EntityFrameworkCore;

namespace BinanceBot.Db
{
    public class BinanceDbContext : DbContext
    {
        public DbSet<OrdersToBeExecuted> OrdersToBeExecuteds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ALPHA;Initial Catalog=BinanceDb;Integrated Security=True;");
        }
    }
}
