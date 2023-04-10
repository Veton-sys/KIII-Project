using Project.Domain.DomainModels;
using Project.Domain.DTO;
using Project.Repository.Interface;
using Project.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service.Implementation
{
	public class ShoppingCartService : IShoppingCartService
	{
		private readonly IRepository<ShoppingCart> _shoppingCartRepository;
		private readonly IRepository<Order> _orderRepository;
		private readonly IRepository<ProductInOrder> _productsInOrderRepository;
		private readonly IUserRepository _userRepository;
		private readonly IOrderRepository _orderRepo;
		public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository,
									IUserRepository userRepository,
									IRepository<Order> orderRepository,
									IRepository<ProductInOrder> productsInOrderRepository,
									IOrderRepository orderRepo)
		{
			_shoppingCartRepository = shoppingCartRepository;
			_userRepository = userRepository;
			_orderRepository = orderRepository;
			_productsInOrderRepository = productsInOrderRepository;
			_orderRepo = orderRepo;
		}
		public bool DeleteTicketFromShoppingCart(Guid Id, string userId)
		{

			if (!string.IsNullOrEmpty(userId) && Id != null)
			{
				var user = this._userRepository.Get(userId);
				var usershoppingcart = user.UserCart;
				var product = usershoppingcart.ProductInShoppingCarts.Where(z => z.ProductId == Id).FirstOrDefault();
				usershoppingcart.ProductInShoppingCarts.Remove(product);
				this._shoppingCartRepository.Update(usershoppingcart);
				return true;
			}
			return false;
		}

		public bool OrderNow(string userId)
		{
			if (!string.IsNullOrEmpty(userId))
			{
				var loggedInUser = this._userRepository.Get(userId);
				var userShoppingCart = loggedInUser.UserCart;

				Order order = new Order
				{
					Id = Guid.NewGuid(),
					User = loggedInUser,
					UserId = userId
				};

				this._orderRepository.Insert(order);

				List<ProductInOrder> productInOrders = new List<ProductInOrder>();

				var result = userShoppingCart.ProductInShoppingCarts.Select(z => new ProductInOrder
				{
					Id = Guid.NewGuid(),
					ProductId = z.Product.Id,
					OrderedProduct = z.Product,
					OrderId = order.Id,
					UserOrder = order,
					Quantity = z.Quantity
				}).ToList();

				StringBuilder sb = new StringBuilder();

				var totalPrice = 0;

				sb.AppendLine("Your order is completed. The order conains: ");

				for (int i = 1; i <= result.Count(); i++)
				{
					var item = result[i - 1];

					totalPrice += item.Quantity * item.OrderedProduct.ProductPrice;

					sb.AppendLine(i.ToString() + ". " + item.OrderedProduct.ProductName + " with price of: " + item.OrderedProduct.ProductPrice + " and quantity of: " + item.Quantity);
				}

				sb.AppendLine("Total price: " + totalPrice.ToString());
				productInOrders.AddRange(result);

				foreach (var item in productInOrders)
				{
					this._productsInOrderRepository.Insert(item);
				}

				loggedInUser.UserCart.ProductInShoppingCarts.Clear();

				this._userRepository.Update(loggedInUser);


				return true;
			}
			return false;
		}

		public ShoppingCartDTO GetShoppingCartInfo(string userId)
		{
			var user = this._userRepository.Get(userId);

			var usershoppingcart = user.UserCart;

			var productList = usershoppingcart.ProductInShoppingCarts.Select(z => new
			{
				Quantity = z.Quantity,
				Price = z.Product.ProductPrice
			}).ToList();

			float totalPrice = 0;

			foreach (var item in productList)
			{
				totalPrice += item.Quantity * item.Price;
			}

			//If this user has more than 3 orders from this site then he will automatically get a 20% discount			
			var nrOfOrder = this.GetNumberOfOrderFromUser(userId);

			if (nrOfOrder.Count <= 3)
			{
				totalPrice *= 1;
			}
			else
			{
				totalPrice *= (float)0.8;
			}

			ShoppingCartDTO scDto = new ShoppingCartDTO
			{
				ProductsInShoppingCart = usershoppingcart.ProductInShoppingCarts.ToList(),
				TotalPrice = totalPrice
			};
			return scDto;
		}

		public List<Order> GetNumberOfOrderFromUser(string userId)
		{
			return this._orderRepo.GetAllOrders().Where(z => z.UserId == userId).ToList();
		}
	}
}
