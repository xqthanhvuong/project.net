using Microsoft.AspNetCore.Mvc;

namespace webhotel.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
