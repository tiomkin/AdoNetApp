using AdoNetApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace AdoNetApp.WebApp.DataAccess
{
	public class OrderRepository : IOrderRepository
	{
		private SqlConnection _connection;
		private readonly string _connectionString;
		private SqlDataAdapter _adapter;
		private DataSet _dataSet;

		public OrderRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Database");
			FillAdapter();
		}

		private SqlConnection Connection
		{
			get
			{
				if (_connection is null || _connection.State is ConnectionState.Closed or ConnectionState.Broken)
				{
					_connection = new SqlConnection(_connectionString);
					_connection.Open();
					return _connection;
				}

				return _connection;
			}
		}

		public void AddOrder(CreateOrder order)
		{
			var record = _dataSet.Tables[0].NewRow();
			record["CreatedDate"] = DateTime.Now;
			record["Status"] = order.Status;
			record["ProductId"] = order.ProductId;
			_dataSet.Tables[0].Rows.Add(record);
			_dataSet.Tables[0].AcceptChanges();

			using (Connection)
			{
				_adapter.Fill(_dataSet);
			}
		}

		public void UpdateOrder(int id, Order order)
		{
			var record = _dataSet.Tables[0].Rows.Find(id);

			_ = record ?? throw new InvalidOperationException($"Can not find record by id {id}");

			record["Status"] = order.Status;
			record["UpdatedDate"] = DateTime.Now;
			_dataSet.Tables[0].AcceptChanges();

			_adapter.Update(_dataSet);
		}

		public Order GetOrderById(int id)
		{
			var record = _dataSet.Tables[0].AsEnumerable().FirstOrDefault(x => x.Field<int>("Id") == id);

			if (record != null)
			{
				var order = new Order()
				{
					Id = (int) record["Id"],
					Status = record["Status"].ToString(),
					CreatedDate = (DateTime) record["CreatedDate"],
					UpdatedDate = (DateTime) record["UpdatedDate"],
					ProductId = (int) record["ProductId"]
				};

				return order;
			}

			return null;
		}

		public IEnumerable<Order> GetAllOrders()
		{
			var orderList = new List<Order>();
			var records = _dataSet.Tables[0].Rows;

			foreach (DataRow record in records)
			{
				var order = new Order()
				{
					Id = (int) record["Id"],
					CreatedDate = (DateTime) record["CreatedDate"],
					UpdatedDate = (DateTime) record["UpdatedDate"],
					ProductId = (int) record["ProductId"]
				};

				orderList.Add(order);
			}

			return orderList;
		}

		public void DeleteOrderById(int id)
		{
			var record = _dataSet.Tables[0].AsEnumerable().FirstOrDefault(x => x.Field<int>("Id") == id);
			_ = record ?? throw new InvalidOperationException($"Can not find record by id {id}");

			_dataSet.Tables[0].Rows.Remove(record);
			_dataSet.Tables[0].AcceptChanges();

			using (Connection)
			{
				_adapter.Fill(_dataSet);
			}
		}

		private void FillAdapter()
		{
			var sql = "SELECT * FROM [dbo].[Order]";
			using (Connection)
			{
				_adapter = new SqlDataAdapter(sql, Connection);
				_adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				_dataSet = new DataSet();
				_adapter.Fill(_dataSet);
			}
		}
	}
}
