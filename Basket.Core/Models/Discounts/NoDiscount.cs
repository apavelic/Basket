using Basket.Core.Infrastructure;
using System.Collections.Generic;

namespace Basket.Core.Models.Discounts
{
	public class NoDiscount : ProductDiscount
	{
		public override DiscountType Type => DiscountType.NoDiscount;

		public NoDiscount(List<ProductDTO> products) :
						base(products)
		{

		}
	}
}
