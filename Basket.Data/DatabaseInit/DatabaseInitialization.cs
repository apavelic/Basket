using Basket.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Data.DatabaseInit
{
    public class DatabaseInitialization : CreateDatabaseIfNotExists<ApplicationContext>
    {

        private ApplicationContext context;

        protected override void Seed(ApplicationContext context)
        {

            this.context = context;
            InitializeProducts();

            base.Seed(context);
        }

        private void InitializeProducts()
        {
            context.Products.Add(new Product(Guid.Parse("25546792-9A4C-E911-93A5-144F8A014C66"), "Bread", 1));
            context.Products.Add(new Product(Guid.Parse("26546792-9A4C-E911-93A5-144F8A014C66"), "Butter", 0.80m));
            context.Products.Add(new Product(Guid.Parse("27546792-9A4C-E911-93A5-144F8A014C66"), "Milk", 1.15m));
        }
    }
}
