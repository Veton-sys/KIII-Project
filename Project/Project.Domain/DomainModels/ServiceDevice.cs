using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Domain.DomainModels
{
	public class ServiceDevice : BaseEntity
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Model { get; set; }
		[Required]
		public string UserMessage { get; set; }
		public string AdminMessage { get; set; }
		public string Status { get; set; }
		public bool PayedFor{ get; set; }
		public Nullable<DateTime> DateAdded { get; set; }
		public Nullable<double> PriceForRepair { get; set; }
		public virtual ICollection<DevicesOfUser> DevicesOfUser { get; set; }



	}
}
