﻿using AdoNetApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdoNetApp.WebApp.DataAccess
{
	public class OrderRepository : IOrderRepository
	{
		private SqlConnection _connection;
		private readonly IConfiguration _configuration;
		private SqlDataAdapter _adapter;
		private SqlCommandBuilder _commandBuilder;
		private DataSet _dataSet;

		public OrderRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			FillAdapter();
		}

		private SqlConnection Connection
		{
			get
			{
				if (_connection is null || _connection.State is ConnectionState.Closed or ConnectionState.Broken)
				{
					var connectionString = _configuration.GetConnectionString("Database");
					_connection = new SqlConnection(connectionString);
					_connection.Open();
					return _connection;
				}

				return _connection;
			}
		}

		public void AddOrder(Order order)
		{
			throw new NotImplementedException();
		}

		public void UpdateOrder(int id, Order order)
		{
			throw new NotImplementedException();
		}

		public Order GetOrderById(int id)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		private void FillAdapter()
		{
			var sql = "SELECT * FROM [dbo].[Order]";
			using (Connection)
			{
				_adapter = new SqlDataAdapter(sql, Connection);
				_dataSet = new DataSet();
				_adapter.Fill(_dataSet);
			}
		}
	}
}
