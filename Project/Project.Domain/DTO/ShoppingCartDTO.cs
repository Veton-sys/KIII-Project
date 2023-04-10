using Project.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<ProductInShoppingCart> ProductsInShoppingCart{ get; set; }
        public float TotalPrice { get; set; }
        
    }
}
