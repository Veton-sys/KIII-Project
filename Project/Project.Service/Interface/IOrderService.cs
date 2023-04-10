using Project.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Interface
{
	public interface IOrderService
	{
		List<Order> GetAllOrders();
		Order GetOrderDetails(BaseEntity model);
	}
}
