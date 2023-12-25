using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webhotel.Models;
using System.Net.Mail;
using Webhotel.Helper;

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
			string emailBody = $@"
    <p>Hello {name},</p>
    <p>Thank you for your order.</p>
    <p>We have confirmed your order and will contact you soon</p>
	<p>
		Your order details:
		<ul>
			<li>Check in: {checkin}</li>
			<li>Check out: {checkout}</li>
			<li>Room type: {_context.Roomtypes.Where(r => r.Id == roomtypeid).FirstOrDefault().Name}</li>
			<li>Quantity: {quantity}</li>
			<li>Total price: {ViewBag.CalculatedValue}</li>
			<li>Customer name: {name}</li>
			<li>Customer address: {address}</li>
			<li>Customer phone: {phone}</li>
			<li>Customer email: {email}</li>
			<li>Customer citizen id: {citizenid}</li>
		</ul>
	</p>
    <p>If you did not request order, please ignore this email.</p>
    <p>Best regards,</p>
    <p>CIAO hotel management team</p>";
            var myAppConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var userName = myAppConfig.GetValue<string>("EmailConfig:Username");
            var Password = myAppConfig.GetValue<string>("EmailConfig:Password");
            var Host = myAppConfig.GetValue<string>("EmailConfig:Host");
            var Port = myAppConfig.GetValue<int>("EmailConfig:Port");
            var FromEmail = myAppConfig.GetValue<string>("EmailConfig:FromEmail");

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            message.To.Add(email);
            message.Subject = "link to retrieve password of Ciao hotel";
            message.IsBodyHtml = true;
            message.Body = emailBody;
            SmtpClient smtpClient = new SmtpClient(Host);
            try
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(userName, Password);
                smtpClient.Host = Host;
                smtpClient.Port = Port;
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                smtpClient.Dispose();
            }
			return RedirectToAction("Index", "home");
		}
	}
}
