
using webhotel.Models;

namespace Webhotel.Models
{
	public class VMRooms
	{
		public List<Roomtype>? RoomTypes { get; set; }
		public List<Roomtype>? RoomTypename { get; set; }
		public bool check { get; set; } = false;
	}
}