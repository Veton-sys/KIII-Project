using Project.Domain.DomainModels;
using Project.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Interface
{
	public interface IShoppingCartService
	{
		ShoppingCartDTO GetShoppingCartInfo(string userId);
		bool DeleteTicketFromShoppingCart(Guid ticketId, string userId);
		bool OrderNow(string userId);
		List<Order> GetNumberOfOrderFromUser(string userId);
	}
}
