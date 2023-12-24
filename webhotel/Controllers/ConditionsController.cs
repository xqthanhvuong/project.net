using Microsoft.AspNetCore.Mvc;

namespace Webhotel.Controllers
{
	public class ConditionsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
