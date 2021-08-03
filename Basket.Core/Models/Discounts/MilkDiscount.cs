using System.Collections.Generic;
using System.Linq;
using Basket.Core.Infrastructure;

namespace Basket.Core.Models.Discounts
{
	public class MilkDiscount : ProductDiscount
	{
		private const int _discountQuantity = 4; // buy 3 milks and get the 4th for free 

		public MilkDiscount(List<ProductDTO> products) :
				base(products)
		{

		}

		public override void ApplyDiscount()
		{
			var filteredProduct = Products.FirstOrDefault(x => x.Name == ProductEnum.Milk.ToString());
			var discountTarget = filteredProduct?.Quantity / _discountQuantity;

			if (discountTarget > 0)
				filteredProduct.Discount = filteredProduct.Price * discountTarget.Value;
		}
	}
}
