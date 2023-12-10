using Microsoft.AspNetCore.Mvc;

namespace webhotel.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
