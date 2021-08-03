using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Core.Models.Discounts
{
	public abstract class ProductDiscount : IDiscount
	{
		public List<ProductDTO> Products { get; }

		public ProductDiscount(List<ProductDTO> products)
		{
			Products = products;
		}

		public virtual void ApplyDiscount()
		{
			Products.Sum(x => x.Price * x.Quantity);
		}
	}
}
