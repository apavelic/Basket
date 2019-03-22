using Basket.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Data.Configurations
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            ToTable("Products");

            HasKey(p => p.ProductId);
            Property(p => p.ProductId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
