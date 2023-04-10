using Project.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.DomainModels
{
	public class Order : BaseEntity
	{
		public string UserId { get; set; }
		public User User { get; set; }
		public IEnumerable<ProductInOrder> ProductInOrders { get; set; }
	}
}
