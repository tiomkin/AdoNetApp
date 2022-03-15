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
			ViewData["AddOrEditButton"] = "Add";
			return View("Edit", new Order());
		}

		// GET: OrderController/Edit/5
		public ActionResult Edit(int orderId)
		{
			ViewData["Title"] = "Edit Order";
			ViewData["AddOrEditButton"] = "Edit";
			var model = _repository.GetOrderById(orderId);
			return View(model);
		}

		// POST: OrderController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(CreateOrder order)
		{
			if (ModelState.IsValid)
			{
				_repository.AddOrder(order);
				TempData["Message"] = "Order successfully added.";
				return RedirectToAction("Index");
			}

			TempData["Message"] = "Something wrong happened. Try create order again.";
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
