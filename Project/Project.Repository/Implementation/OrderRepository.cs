using Project.Domain.DomainModels;
using Project.Repository;
using Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Project.Repository.Implementation
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext context;
		private DbSet<Order> entities;
		string errorMessage = string.Empty;

		public OrderRepository(ApplicationDbContext context)
		{
			this.context = context;
			entities = context.Set<Order>();
		}
		public List<Order> GetAllOrders()
		{
			return entities
					.Include(z => z.ProductInOrders)
					.Include(z => z.User)
					.Include("ProductInOrders.OrderedProduct")
					.ToListAsync().Result;
		}

		public Order GetOrderDetails(BaseEntity model)
		{
			return entities
					.Include(z => z.ProductInOrders)
					.Include(z => z.User)
					.Include("ProductInOrders.OrderedProduct")
					.SingleOrDefaultAsync(z => z.Id == model.Id).Result;
		}
	}
}
