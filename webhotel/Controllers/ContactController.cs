using Microsoft.AspNetCore.Mvc;

namespace webhotel.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
