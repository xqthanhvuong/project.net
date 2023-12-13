using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using webhotel.Models;

namespace webhotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebhotelContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new WebhotelContext();
        }

        public IActionResult Index()
        {
            var rooms = _context.Rooms.Include(r => r.Roomimgs).Include(r => r.Type).Take(3).ToList();
            return View(rooms);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}