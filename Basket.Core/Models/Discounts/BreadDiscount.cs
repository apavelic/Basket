using Basket.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Core.Models.Discounts
{
	public class BreadDiscount : ProductDiscount
	{
		public BreadDiscount(List<ProductDTO> products) :
						base(products)
		{

		}

		public override void ApplyDiscount()
		{
			var butter = Products.FirstOrDefault(x => x.Name == ProductEnum.Butter.ToString());
			var bread = Products.FirstOrDefault(x => x.Name == ProductEnum.Bread.ToString());

			if (bread != null)
			{
				var numberOfBreadsToDiscount = butter?.Quantity / 2;

				if (bread.Quantity < numberOfBreadsToDiscount)
					numberOfBreadsToDiscount = bread.Quantity;

				bread.Discount = numberOfBreadsToDiscount.Value * bread.Price / 2;
			}
		}
	}
}
