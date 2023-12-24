using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webhotel.Models;
using Webhotel.Models;

namespace Webhotel.Controllers
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
            var roomTypes = _context.Roomtypes.Include(r => r.Roomimgs).ToList();
            VMRooms result = new VMRooms()
            {
                RoomTypename = roomTypes,
                RoomTypes = roomTypes
            };
            return View(result);
        }

        [ActionName("Detail")]
        public IActionResult Detail(int id)
        {
            var roomsDetail = _context.Roomtypes.Where(r => r.Id == id).FirstOrDefault();
            var roomOther = _context.Roomtypes.Include(r => r.Roomimgs).ToList();
            var roomImage = _context.Roomimgs.Where(r => r.TypeId == id).ToList();
            var max = _context.Rooms.Where(r => r.TypeId == id).Count();
            var rs = Tuple.Create(roomsDetail, roomOther, roomImage, max);
            return View(rs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Check")]
        public IActionResult Check(string ci, string co, int? t, int? quantity)
        {
            var nb = quantity.HasValue ? quantity : 1;
            if (t != null)
            {
                var i = DateTime.Parse(ci);
                var o = DateTime.Parse(co);
                var rs = from room in _context.Rooms
                         join roomType in _context.Roomtypes on room.TypeId equals roomType.Id
                         where roomType.Id == t
                               && !_context.Reservations.Any(reservation =>
                                     reservation.RoomId == room.Id &&
                                     (
                                         (i >= reservation.CheckIn && i < reservation.CheckOut) ||
                                         (o > reservation.CheckIn && o <= reservation.CheckOut) ||
                                         (reservation.CheckIn >= i && reservation.CheckIn < o) ||
                                         (reservation.CheckOut > i && reservation.CheckOut <= o)
                                     )
                               )
                         select new
                         {
                             RoomID = room.Id,
                         };

                if (rs.Count() >= nb)
                {
                    var roomTypes = _context.Roomtypes.Include(r => r.Roomimgs).Where(r => r.Id == t).ToList();
                    var roomTypeName = _context.Roomtypes.ToList();
                    VMRooms result = new VMRooms()
                    {
                        RoomTypes = roomTypes,
                        RoomTypename = roomTypeName,
                    };
                    return View("Index", result);
                }
                else
                {
                    TempData["Message"] = "The room type you selected for this date is sold out";
                    return RedirectToAction("Index", "Rooms");
                }
            }
            else
            {
                var i = DateTime.Parse(ci);
                var o = DateTime.Parse(co);
                var roomTypeIds = _context.Roomtypes
                    .Where(rt => _context.Rooms.Count(r =>
                        rt.Id == r.TypeId &&
                        r.Status == false &&
                        !_context.Reservations.Any(rv =>
                            r.Id == rv.RoomId &&
                            (
                                (i >= rv.CheckIn && i < rv.CheckOut) ||
                                (o > rv.CheckIn && o <= rv.CheckOut) ||
                                (rv.CheckIn >= i && rv.CheckIn < o) ||
                                (rv.CheckOut > i && rv.CheckOut <= o)
                            )
                        )
                    ) >= nb)
                    .Select(rt => rt.Id)
                    .ToList();
                List<Roomtype> roomTypes = new List<Roomtype>();
                foreach (var rt in roomTypeIds)
                {
                    var roomtype = _context.Roomtypes.Include(r => r.Roomimgs).Where(r => r.Id == rt).FirstOrDefault();
                    roomTypes.Add(roomtype);
                }
                if (roomTypes.Count > 0)
                {
                    var roomTypeName = _context.Roomtypes.ToList();
                    VMRooms result = new VMRooms()
                    {
                        RoomTypes = roomTypes,
                        RoomTypename = roomTypeName,
                    };
                    return View("Index", result);
                }
                else
                {
                    TempData["Message"] = "The room for this date is sold out";
                    return RedirectToAction("Index", "Rooms");
                }
            }
        }
    }
}
