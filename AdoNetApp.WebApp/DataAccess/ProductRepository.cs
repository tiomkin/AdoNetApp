using AdoNetApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace AdoNetApp.WebApp.DataAccess
{
	public class ProductRepository : IProductRepository
	{
		private SqlConnection _connection;
		private readonly IConfiguration _configuration;

		public ProductRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		private SqlConnection Connection
		{
			get
			{
				if (_connection is null ||_connection.State is ConnectionState.Closed or ConnectionState.Broken)
				{
					_connection = new SqlConnection(_configuration.GetConnectionString("Database"));
					_connection.Open();
					return _connection;
				}

				return _connection;
			}
		}

		public void AddProduct(Product product)
		{
			var	sql = $"INSERT INTO Product (Name, Description, Weight, Height, Width, Length) " +
				          "VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";

			using (Connection)
			{
				var command = new SqlCommand(sql, Connection);
				command.Parameters.Add(new SqlParameter("@Name", product.Name));
				command.Parameters.Add(new SqlParameter("@Description", product.Description));
				command.Parameters.Add(new SqlParameter("@Weight", product.Weight));
				command.Parameters.Add(new SqlParameter("@Height", product.Height));
				command.Parameters.Add(new SqlParameter("@Width", product.Width));
				command.Parameters.Add(new SqlParameter("@Length", product.Length));

				command.ExecuteNonQuery();
			}
		}

		public void UpdateProduct(int id, Product product)
		{
			var sql = $"UPDATE Product " +
			          "SET Name=@Name, Description=@Description, Weight=@Weight, Height=@Height, Width=@Width, Length=@Length " +
			          $"WHERE Id='{product.Id}'";

			using (Connection)
			{
				var command = new SqlCommand(sql, Connection);
				command.Parameters.Add(new SqlParameter("@Name", product.Name));
				command.Parameters.Add(new SqlParameter("@Description", product.Description));
				command.Parameters.Add(new SqlParameter("@Weight", product.Weight));
				command.Parameters.Add(new SqlParameter("@Height", product.Height));
				command.Parameters.Add(new SqlParameter("@Width", product.Width));
				command.Parameters.Add(new SqlParameter("@Length", product.Length));

				command.ExecuteNonQuery();
			}
		}

		public Product GetProductById(int id)
		{
			var sql = "SELECT * FROM Product WHERE Id=@Id";

			using (Connection)
			{
				var command = new SqlCommand(sql, Connection);
				command.Parameters.Add(new SqlParameter("@Id", id));

				using (var reader = command.ExecuteReader())
				{
					if (reader.HasRows && reader.Read())
					{
						var product = new Product();
						product.Id = id;
						product.Name = reader["Name"].ToString();
						product.Description = reader["Description"].ToString();
						product.Height = (int) reader["Height"];
						product.Weight = (int) reader["Weight"];
						product.Length = (int) reader["Length"];
						product.Width = (int) reader["Width"];

						return product;
					}
				}
			}

			return null;
		}

		public IEnumerable<Product> GetAllProducts()
		{
			var productList = new List<Product>();
			var sql = $"SELECT * FROM Product";

			using (Connection)
			{
				var command = new SqlCommand(sql, Connection);

				using (var reader = command.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							var product = new Product()
							{
								Id = (int) reader["Id"],
								Name = reader["Name"].ToString(),
								Description = reader["Description"].ToString(),
								Height = (int)reader["Height"],
								Weight = (int)reader["Weight"],
								Length = (int)reader["Length"],
								Width = (int)reader["Width"]
							};

							productList.Add(product);
						}

						return productList;
					}
				}
			}

			return null;
		}

		public void DeleteProductById(int id)
		{
			var sql = $"DELETE FROM Product WHERE Id=@Id";

			using (Connection)
			{
				var command = new SqlCommand(sql, Connection);
				command.Parameters.Add(new SqlParameter("@Id", id));

				command.ExecuteNonQuery();
			}
		}
	}
}
