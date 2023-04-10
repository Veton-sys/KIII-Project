using Project.Domain.Identity;
using Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Repository.Implementation
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext context;
		private DbSet<User> entities;
		string errorMessage = string.Empty;

		public UserRepository(ApplicationDbContext context)
		{
			this.context = context;
			entities = context.Set<User>();
		}
		public User Get(string id)
		{
			return entities.Include(z => z.UserCart)
						   .Include("UserCart.ProductInShoppingCarts")
						   .Include("UserCart.ProductInShoppingCarts.Product")
						   .SingleOrDefault(s => s.Id == id);
		}

		public IEnumerable<User> GetAll()
		{
			return entities.AsEnumerable();
		}

		public void Insert(User entity)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Add(entity);
			context.SaveChanges();
		}

		public void Update(User entity)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Update(entity);
			context.SaveChanges();
		}
		public void Delete(User entity)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Remove(entity);
			context.SaveChanges();
		}
	}
}
