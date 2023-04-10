using Project.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Interface
{
	public interface IServiceDeviceService
	{
		List<ServiceDevice> GetAllDevices();
		ServiceDevice GetDetailsForDevice(Guid? id);
		void CreateNewDevice(ServiceDevice t);
		void UpdateExistingDevice(ServiceDevice t);
		void DeleteProduct(Guid? id);
		bool AddDeviceToUser(ServiceDevice t, string userId);
		List<ServiceDevice> GetAllDevicesFromUser(string userId);
	}
}
