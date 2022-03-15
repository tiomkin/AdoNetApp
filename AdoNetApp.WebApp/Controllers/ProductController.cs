using AdoNetApp.WebApp.DataAccess;
using AdoNetApp.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdoNetApp.WebApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepository _repository;

		public ProductController(IProductRepository repository)
		{
			_repository = repository;
		}
		// GET: ProductController
		public ActionResult Index()
		{
			var model = _repository.GetAllProducts();
			return View(model);
		}

		// GET: ProductController/Create
		public ActionResult Create()
		{
			ViewData["Title"] = "Create Product";
			return View(new Product());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Product product)
		{
			if (ModelState.IsValid)
			{
				_repository.AddProduct(product);
				TempData["Message"] = "Product successfully added.";
				return RedirectToAction("Index");
			}

			TempData["Message"] = "Something wrong happened. Try add product again.";
			return RedirectToAction("Create");
		}

		// GET: ProductController/Edit/5
		public ActionResult Edit(int productId)
		{
			ViewData["Title"] = "Edit Product";
			ViewData["AddOrEditButton"] = "Edit";
			var model = _repository.GetProductById(productId);
			return View(model);
		}

		// POST: ProductController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				_repository.UpdateProduct(product.Id, product);
				TempData["Message"] = "Product successfully edited.";
				return RedirectToAction("Index");
			}

			TempData["Message"] = "Something wrong happened. Try edit product again.";
			return RedirectToAction("Create");
		}

		// POST: ProductController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int productId)
		{
			try
			{
				_repository.DeleteProductById(productId);
				TempData["Message"] = "Product successfully deleted.";
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["Message"] = "Could not delete product.";
				return RedirectToAction(nameof(Index));
			}
		}
	}
}
