using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using webhotel.Models;
using Webhotel.Models;

namespace Webhotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebhotelContext _context;
        public HomeController(ILogger<HomeController> logger)
        {
            _context = new WebhotelContext();
            _logger = logger;
        }

        public IActionResult Index()
        {
            var rooms = _context.Roomtypes.Include(r => r.Roomimgs).Take(3).ToList();
            return View(rooms);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}