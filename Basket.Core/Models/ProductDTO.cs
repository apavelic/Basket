using Basket.Core.Infrastructure;
using Basket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Models
{
    public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get { return Price * Quantity; } }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public override string ToString()
        {
            return $"{Name}\t\t{Quantity}\t\t${Price}\t\t${TotalPrice}\t\t${Discount}\t\t${TotalPrice - Discount}";
        }
    }
}
