using Project.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.DomainModels
{
	public class ShoppingCart : BaseEntity
	{
		public string userId { get; set; }
		public User user { get; set; }
		public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
	}
}
