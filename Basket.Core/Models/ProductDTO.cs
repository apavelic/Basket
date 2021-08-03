using System;

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
