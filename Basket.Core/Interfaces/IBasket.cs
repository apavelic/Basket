using Basket.Core.Models;
using System.Collections.Generic;

namespace Basket.Core.Interfaces
{
	public interface IBasket
	{
		bool Add(ProductDTO product);
		void ApplyDiscount();
		bool HasDiscount { get; }
		decimal GetTotalPrice();
		bool EmptyCart();
	}
}
