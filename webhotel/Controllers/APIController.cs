using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webhotel.Models;

namespace webhotel.Controllers
{
	[Route("api/[action]")]
	[ApiController]
	public class APIController : ControllerBase
	{
		private readonly WebhotelContext _context;
		public APIController()
		{
			_context = new WebhotelContext();
		}
		[HttpGet]
		[ActionName("GetAllRoomTypes")]
		public ActionResult Get()
		{
			var roomTypes = _context.Roomtypes
				.Select(r => new
				{
					r.Id,
					Type = r.Name,
					Roomimgs = r.Roomimgs.Select(ri => new
					{
						ri.Link
					}).ToList(),
					r.Price,
					r.Des,
					QuantityEmptyRoom = r.Rooms.Count(ro => ro.Status == false)
				})
				.ToList();

			return Ok(roomTypes);
		}


		[HttpGet("{id}")]
		[ActionName("GetRoomTypeById")]
		public ActionResult<string> Get(int id)
		{
			var roomTypes = _context.Roomtypes
				.Select(r => new
				{
					r.Id,
					Type = r.Name,
					Roomimgs = r.Roomimgs.Select(ri => new
					{
						ri.Link
					}).ToList(),
					r.Price,
					r.Des,
					QuantityEmptyRoom = r.Rooms.Count(ro => ro.Status == false)
				})
				.FirstOrDefault(r => r.Id == id);

			return Ok(roomTypes);
		}

		[HttpGet("{type}")]
		[ActionName("GetRoomTypeByType")]
		public ActionResult<string> Get(string type)
		{
			var roomTypes = _context.Roomtypes
				.Select(r => new
				{
					r.Id,
					Type = r.Name,
					Roomimgs = r.Roomimgs.Select(ri => new
					{
						ri.Link
					}).ToList(),
					r.Price,
					r.Des,
					QuantityEmptyRoom = r.Rooms.Count(ro => ro.Status == false)
				})
				.FirstOrDefault(r => r.Type == type);

			return Ok(roomTypes);
		}
	}
}

