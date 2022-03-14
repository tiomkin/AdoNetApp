using System.Collections.Generic;
using AdoNetApp.WebApp.Models;

namespace AdoNetApp.WebApp.DataAccess
{
	public interface IProductRepository
	{
		void AddProduct(Product product);
		void UpdateProduct(int id, Product product);
		Product GetProductById(int id);
		IEnumerable<Product> GetAllProducts();
		void DeleteProductById(int id);
	}
}
