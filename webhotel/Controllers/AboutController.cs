using Microsoft.AspNetCore.Mvc;

namespace webhotel.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
