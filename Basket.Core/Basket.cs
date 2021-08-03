using Basket.Core.Factories;
using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Core
{
	public class Basket : IBasket
	{
		public bool HasDiscount { get; set; }

		private List<ProductDTO> _shoppingCart;
		private readonly ILogger _logger;
		private List<DiscountType> _discountTypes
			=> Enum.GetValues(typeof(DiscountType)).Cast<DiscountType>().ToList();
		public Basket()
		{
			// TODO add dependecy injection
			_logger = new Logger();
			_shoppingCart = new List<ProductDTO>();
		}
		public bool Add(ProductDTO productToAdd)
		{
			try
			{
				var product = _shoppingCart.FirstOrDefault(x => x.ProductId == productToAdd.ProductId);

				if (product == null)
					_shoppingCart.Add(productToAdd);
				else
					product.Quantity += productToAdd.Quantity;

				if (HasDiscount)
					ApplyDiscount();

				return true;
			}
			catch (Exception e)
			{
				_logger.Log($"Class: Basket.cs, Method: Add, Message: {e.Message}", LogType.Error);
				return false;
			}
		}
		public decimal GetTotalPrice()
		{
			try
			{
				return _shoppingCart.Sum(x => x.TotalPrice - x.Discount);
			}
			catch (Exception e)
			{
				_logger.Log($"Class: Basket.cs, Method: GetTotalPrice, Message: {e.Message}", LogType.Error);
				throw e;
			}
		}

		public void ApplyDiscount()
		{
			try
			{
				_discountTypes.ForEach(type => DiscountFactory
					.GetDiscountModel(type, _shoppingCart)
					.ApplyDiscount());
			}
			catch (Exception e)
			{
				_logger.Log($"Class: Basket.cs, Method: ApplyDiscount, Message: {e.Message}", LogType.Error);
				throw e;
			}
		}
		public bool EmptyCart()
		{
			try
			{
				_shoppingCart.Clear();
				return true;
			}
			catch (Exception e)
			{
				_logger.Log($"Class: Basket.cs, Method: EmptyCart, Message: {e.Message}", LogType.Error);
				return false;
			}
		}
	}
}
