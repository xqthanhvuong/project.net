using Microsoft.AspNetCore.Mvc;

namespace Webhotel.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
