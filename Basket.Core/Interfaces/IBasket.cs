using Basket.Core.Models;
using System.Collections.Generic;

namespace Basket.Core.Interfaces
{
	public interface IBasket
	{
		IEnumerable<ProductDTO> GetCartContent();
		bool Add(ProductDTO product);
		void ApplyDiscount();
		bool HasDiscount { get; set; }
		decimal GetTotalPrice();
		bool EmptyCart();
	}
}
