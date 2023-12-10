using Microsoft.AspNetCore.Mvc;

namespace webhotel.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            return View(id);
        }
    }
}
