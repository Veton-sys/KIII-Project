using Project.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.DomainModels
{
	public class DevicesOfUser : BaseEntity
	{
		public Guid DeviceId { get; set; }
		public ServiceDevice Device { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }


	}
}
