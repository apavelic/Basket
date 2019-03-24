using Basket.Data.Configurations;
using Basket.Data.Models;
using System.Data.Entity;

namespace Basket.Data.DatabaseInit
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("connection_string")
        {

        }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ProductConfig());
        }
    }
}
