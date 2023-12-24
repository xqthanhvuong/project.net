using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Index(int roomID, string ciF, string coF, string quantityRoom, string quantityPerson)
		{

			var ci = DateTime.Parse(ciF);
			var co = DateTime.Parse(coF);
			var avr = from Room in _context.Rooms
					  join roomType in _context.Roomtypes on Room.TypeId equals roomType.Id
					  where roomType.Id == roomID
							&& !_context.Reservations.Any(reservation =>
								  reservation.RoomId == Room.Id &&
								  (
									  (ci >= reservation.CheckIn && ci < reservation.CheckOut) ||
									  (co > reservation.CheckIn && co <= reservation.CheckOut) ||
									  (reservation.CheckIn >= ci && reservation.CheckIn < co) ||
									  (reservation.CheckOut > ci && reservation.CheckOut <= co)
								  )
							)
					  select new
					  {
						  RoomID = Room.Id,
					  };
			if (avr.Count() >= Int32.Parse(quantityRoom))
			{
				var nights = (co - ci).Days;
				var room = _context.Roomtypes.Where(r => r.Id == (int)roomID).FirstOrDefault();
				var rs = Tuple.Create(room, ciF
					, coF
					, quantityRoom
					, quantityPerson);
				ViewBag.CalculatedValue = Int32.Parse(quantityRoom) * room.Price * nights;
				return View(rs);
			}
			else
			{
				TempData["Message"] = "The room type you selected for this date is sold out";
				return RedirectToAction("detail", "Rooms", new { id = roomID });
			}


		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("CheckOut")]
		[Authorize]
		public IActionResult CheckOut(string name, string address, string phone, string email, string citizenid, string checkin, string checkout, string roomid, string quantity)
		{
			var check = _context.Customers.Where(c => c.Id == citizenid).FirstOrDefault();
			if (check == null)
			{
				Customer customer = new Customer()
				{
					Name = name,
					Address = address,
					Id = citizenid,
					Email = email,
					Phone = phone,
				};
				_context.Customers.Add(customer);
				_context.SaveChanges();
			}
			var ci = DateTime.Parse(checkin);
			var co = DateTime.Parse(checkout);
			var roomtypeid = Int32.Parse(roomid);
			var rid = _context.Rooms.Where(r => r.TypeId == roomtypeid && !_context.Reservations.Any(rv =>
						r.Id == rv.RoomId && (
								(ci >= rv.CheckIn && ci < rv.CheckOut) ||
								(co > rv.CheckIn && co <= rv.CheckOut) ||
								(rv.CheckIn >= ci && rv.CheckIn < co) ||
								(rv.CheckOut > ci && rv.CheckOut <= co)
							)
							)
			).ToList();
			int nb = Int32.Parse(quantity);
			var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			if (username != null)
			{
				for (int i = 0; i < nb; i++)
				{
					Reservation reservation = new Reservation()
					{
						CustomerId = citizenid,
						CheckIn = DateTime.Parse(checkin),
						CheckOut = DateTime.Parse(checkout),
						RoomId = rid.ElementAt(i).Id,
						Username = username
					};
					_context.Reservations.Add(reservation);
					_context.SaveChanges();
				}
			}
			return RedirectToAction("Index", "home");
		}
	}
}
