using Microsoft.EntityFrameworkCore;
using CodingTask.Domain.Customers;

using CodingTask.Domain.Products;
using CodingTask.Infrastructure.Processing.InternalCommands;
using CodingTask.Infrastructure.Processing.Outbox;

namespace CodingTask.Infrastructure.Database
{
    public class OrdersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

       

        public OrdersContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
        }
    }
}
