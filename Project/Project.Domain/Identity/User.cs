using Microsoft.AspNetCore.Identity;
using Project.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Identity
{
	public class User : IdentityUser
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public virtual ShoppingCart UserCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<DevicesOfUser> DevicesOfUser { get; set; }


    }
}
