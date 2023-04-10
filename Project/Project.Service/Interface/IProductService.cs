using Project.Domain.DomainModels;
using Project.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Interface
{
	public interface IProductService
	{
		List<Product> GetAllProducts();
		Product GetDetailsForProduct(Guid? id);
		AddToShoppingCartDTO GetShoppingCartInfo(Guid? id);
		void CreateNewProduct(Product t);
		void UpdateExistingProduct(Product t);
		void DeleteProduct(Guid? id);
		bool AddToSHoppingCart(AddToShoppingCartDTO item, string userID);
		
		
	}
}
