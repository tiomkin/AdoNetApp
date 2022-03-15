using AdoNetApp.WebApp.DataAccess;
using AdoNetApp.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdoNetApp.WebApp.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderRepository _repository;

		public OrderController(IOrderRepository repository)
		{
			_repository = repository;
		}

		// GET: OrderController
		public ActionResult Index()
		{
			var model = _repository.GetAllOrders();
			return View(model);
		}

		// GET: OrderController/Create
		public ActionResult Create()
		{
			ViewData["Title"] = "Create Order";
			return View( new CreateOrder());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateOrder order)
		{
			if (ModelState.IsValid)
			{
				_repository.AddOrder(order);
				TempData["Message"] = "Product successfully added.";
				return RedirectToAction("Index");
			}

			TempData["Message"] = "Something wrong happened. Try add product again.";
			return RedirectToAction("Create");
		}

		// GET: OrderController/Edit/5
		public ActionResult Edit(int orderId)
		{
			ViewData["Title"] = "Edit Order";
			var model = _repository.GetOrderById(orderId);
			return View(model);
		}

		// POST: OrderController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Order order)
		{
			if (ModelState.IsValid)
			{
				_repository.UpdateOrder(order.Id, order);
				TempData["Message"] = "Order successfully edited.";
				return RedirectToAction("Index");
			}

			TempData["Message"] = "Something wrong happened. Try edit order again.";
			return RedirectToAction("Create");
		}

		// POST: OrderController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int orderId)
		{
			try
			{
				_repository.DeleteOrderById(orderId);
				TempData["Message"] = "Order successfully deleted.";
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["Message"] = "Could not delete order.";
				return RedirectToAction(nameof(Index));
			}
		}
	}
}
