using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webhotel.Models;

namespace webhotel.Controllers
{
	public class RoomsController : Controller
	{

		private readonly WebhotelContext _context;

		public RoomsController()
		{
			_context = new WebhotelContext();
		}

		public IActionResult Index()
		{
			var rooms = _context.Rooms.Include(r => r.Roomimgs).Include(r => r.Type).ToList();
			return View(rooms);
		}

		[ActionName("Detail")]
		public IActionResult Detail(int? id)
		{
			var roomsDetail = _context.Rooms.Include(r => r.Roomimgs).Include(r => r.Type).Where(r => r.Id == id).FirstOrDefault();
			var roomOther = _context.Rooms.Include(r => r.Roomimgs).Include(r => r.Type).ToList();
			var rs = Tuple.Create(roomsDetail, roomOther);

			return View(rs);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Check")]
		public IActionResult Check(string ci, string co, string t)
		{
			var i = DateTime.Parse(ci);
			var o = DateTime.Parse(co);
			var rs = _context.Rooms.Include(r => r.Roomimgs).
				Include(r => r.Type).
				Include(r => r.Reservations).
				Where(r => String.Equals(r.Type.Name, t) &&
			!(r.Reservations.FirstOrDefault() != null && r.Reservations.FirstOrDefault().CheckIn > i && r.Reservations.FirstOrDefault().CheckOut < o)).

				ToList();
			if (rs.Count > 0)
			{
				return View("Index", rs);
			}
			else
			{
				ViewBag.Message = "Hết phòng";
				return RedirectToAction("Index", "Rooms");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Book")]
		public IActionResult Book(int roomID, string ciF, string coF, string quantityRoom, string quantityPerson)
		{
			string id = "C001";
			TempData["CustomerID"] = id;
			TempData["RoomID"] = roomID;
			TempData["CheckIn"] = ciF;
			TempData["CheckOut"] = coF;
			TempData["QuantityRoom"] = quantityRoom;
			TempData["QuantityPerson"] = quantityPerson;

			return RedirectToAction("Index", "Checkout");
		}
	}
}
