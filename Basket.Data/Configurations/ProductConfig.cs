using Basket.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Basket.Data.Configurations
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            ToTable("Products");

            HasKey(p => p.ProductId);
            Property(p => p.ProductId).IsRequired();
        }
    }
}
