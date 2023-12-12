using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webhotel.Models;

namespace webhotel.Controllers
{
	public class RoomViewModel
	{
		public IQueryable RoomDetail { get; set; }
		public IEnumerable<Room> RoomOthers { get; set; }
	}
	public class RoomsController : Controller
    {

		private readonly WebhotelContext _context;

        public RoomsController(ILogger<RoomsController> logger)
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
			var roomOther = _context.Rooms.Include(r => r.Roomimgs).Include(r => r.Type).Take(2).ToList();
			var rs = Tuple.Create(roomsDetail, roomOther);

			return View(rs);
        }
    }
}
