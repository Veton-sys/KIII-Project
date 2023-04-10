using Project.Domain.DomainModels;
using Project.Repository.Interface;
using Project.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service.Implementation
{
	public class ServiceDeviceService : IServiceDeviceService
	{
		private readonly IUserRepository _userRepository;
		private readonly IRepository<ServiceDevice> _serviceDeviceRepository;
		private readonly IRepository<DevicesOfUser> _deviceOfUserRepository;

		public ServiceDeviceService(IUserRepository userRepository, IRepository<ServiceDevice> serivceDeviceRepository, IRepository<DevicesOfUser> deviceOfUserRepository)
		{
			this._userRepository = userRepository;
			this._serviceDeviceRepository = serivceDeviceRepository;
			this._deviceOfUserRepository = deviceOfUserRepository;
		}
		public List<ServiceDevice> GetAllDevices()
		{
			return this._serviceDeviceRepository.GetAll().ToList();
		}
		public List<ServiceDevice> GetAllDevicesFromUser(string userId)
		{
			var devicesOfUser = this._deviceOfUserRepository.GetAll().Where(z => z.UserId == userId).ToList();
			var devices = new List<ServiceDevice>();
			foreach(var item in devicesOfUser)
			{
				var d = this._serviceDeviceRepository.Get(item.DeviceId);
				devices.Add(d);
			}
			return devices;
		}
		public ServiceDevice GetDetailsForDevice(Guid? id)
		{
			return this._serviceDeviceRepository.Get(id);
		}
		public void CreateNewDevice(ServiceDevice t)
		{
			this._serviceDeviceRepository.Insert(t);
		}
		public void UpdateExistingDevice(ServiceDevice t)
		{
			this._serviceDeviceRepository.Update(t);
		}
		public void DeleteProduct(Guid? id)
		{
			var device = this.GetDetailsForDevice(id);
			this._serviceDeviceRepository.Delete(device);
		}

		public bool AddDeviceToUser(ServiceDevice t, string userId)
		{
			var user = this._userRepository.Get(userId);
			if(user != null)
			{
				DevicesOfUser deviceOfUser = new DevicesOfUser
				{
					Id = Guid.NewGuid(),
					Device = t,
					DeviceId = t.Id,
					User = user,
					UserId = user.Id
				};

				this._deviceOfUserRepository.Insert(deviceOfUser);
				return true;
			}
			return false;
		}


	}
}
