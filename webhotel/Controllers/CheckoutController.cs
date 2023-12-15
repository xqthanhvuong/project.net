using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webhotel.Models;

namespace webhotel.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly WebhotelContext _context;

		public CheckoutController()
		{
			_context = new WebhotelContext();
		}

		public IActionResult Index()
		{
			if (TempData["CustomerID"] != null &&
				TempData["RoomID"] != null &&
				TempData["CheckIn"] != null &&
				TempData["CheckOut"] != null &&
				TempData["QuantityRoom"] != null &&
				TempData["QuantityPerson"] != null
			)
			{
				var room = _context.Rooms.Include(r => r.Type).Where(r => r.Id == (int)TempData["RoomID"]).FirstOrDefault();
				var customer = _context.Customers.Where(c => c.Id == TempData["CustomerID"]).FirstOrDefault();
				var rs = Tuple.Create(room, customer, TempData["CheckIn"].ToString(), TempData["CheckOut"].ToString(), TempData["QuantityRoom"].ToString(), TempData["QuantityPerson"].ToString());
				var calculatedValue = Int32.Parse(TempData["QuantityRoom"].ToString()) * Int32.Parse(TempData["QuantityPerson"].ToString()) * room.Type.Price;
				ViewBag.CalculatedValue = calculatedValue;
				return View(rs);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("CheckOut")]
		public IActionResult CheckOut(string name, string address, string phone, string citizenID, string checkIn, string checkOut, string roomID)
		{
			string idCustomer = "C001";
			Reservation r = new Reservation();
			r.CustomerId = idCustomer;
			r.CheckIn = DateTime.Parse(checkIn);
			r.CheckOut = DateTime.Parse(checkOut);
			r.CustomerId = idCustomer;
			r.RoomId = Int32.Parse(roomID);
			r.CitizenId = citizenID;
			r.Phone = phone;

			_context.Add(r);
			_context.SaveChanges();

			return RedirectToAction("Index", "Checkout");
		}
	}
}
