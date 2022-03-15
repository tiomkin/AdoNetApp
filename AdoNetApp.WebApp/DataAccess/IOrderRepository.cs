using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoNetApp.WebApp.Models;

namespace AdoNetApp.WebApp.DataAccess
{
	public interface IOrderRepository
	{
		void AddOrder(CreateOrder order);
		void UpdateOrder(int id, Order order);
		Order GetOrderById(int id);
		IEnumerable<Order> GetAllOrders();
		void DeleteOrderById(int id);
	}
}
