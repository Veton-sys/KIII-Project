using Project.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository.Interface
{
	public interface IUserRepository
	{
		IEnumerable<User> GetAll();
		User Get(string id);
		void Insert(User entity);
		void Delete(User entity);
		void Update(User entity);
	}
}
