using Project.Domain.DomainModels;
using Project.Domain.DTO;
using Project.Domain.Identity;
using Project.Repository.Interface;
using Project.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service.Implementation
{
	public class ProductService : IProductService
	{
		private readonly IRepository<Product> _productRepository;
		private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
		private readonly IUserRepository _userRepository;
		private readonly ILogger<ProductService> _logger;

		public ProductService(ILogger<ProductService> logger, IRepository<Product> productRepository, IUserRepository userRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository)
		{
			_productRepository = productRepository;
			_userRepository = userRepository;
			_productInShoppingCartRepository = productInShoppingCartRepository;
			_logger = logger;
		}
		public Product GetDetailsForProduct(Guid? id)
		{
			return this._productRepository.Get(id);
		}
		public List<Product> GetAllProducts()
		{
			return this._productRepository.GetAll().ToList();
		}
		public void CreateNewProduct(Product t)
		{
			this._productRepository.Insert(t);
		}

		public void DeleteProduct(Guid? id)
		{
			var product = this.GetDetailsForProduct(id);
			this._productRepository.Delete(product);
		}
		public void UpdateExistingProduct(Product t)
		{
			this._productRepository.Update(t);
		}
		public AddToShoppingCartDTO GetShoppingCartInfo(Guid? id)
		{
			var product = this._productRepository.Get(id);
			var model = new AddToShoppingCartDTO();
			model.SelectedProduct = product;
			model.ProductId = product.Id;
			model.Quantity = 0;
			return model;
		}
		public bool AddToSHoppingCart(AddToShoppingCartDTO item, string userID)
		{
			var user = this._userRepository.Get(userID);
			var userShoppingCart = user.UserCart;
			if (userShoppingCart != null)
			{
				var product = this.GetDetailsForProduct(item.ProductId);

				if (product != null)
				{
					ProductInShoppingCart itemToAdd = new ProductInShoppingCart
					{
						Id = Guid.NewGuid(),
						Product = product,
						ProductId = product.Id,
						ShoppingCart = userShoppingCart,
						Quantity = item.Quantity
					};

					this._productInShoppingCartRepository.Insert(itemToAdd);
					_logger.LogInformation("Ticket was successfully added into shopping cart");
					return true;
				}
				return false;
			}
			_logger.LogInformation("Something was wrong. Ticket id or User id missing");
			return false;
		}
	}
}
