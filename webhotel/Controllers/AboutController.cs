using Microsoft.AspNetCore.Mvc;

namespace Webhotel.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
