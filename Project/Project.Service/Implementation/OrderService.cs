﻿using Project.Domain.DomainModels;
using Project.Repository.Interface;
using Project.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Implementation
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		public OrderService(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}
		public List<Order> GetAllOrders()
		{
			return this._orderRepository.GetAllOrders();
		}

		public Order GetOrderDetails(BaseEntity model)
		{
			return this._orderRepository.GetOrderDetails(model);
		}
	}
}
