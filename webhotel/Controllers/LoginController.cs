using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Webhotel.Helper;
using Webhotel.Models;
using webhotel.Models;

namespace webhotel.Controllers
{
    public class LoginController : Controller
    {
        private readonly WebhotelContext _webhotelContext;
        public LoginController()
        {
            _webhotelContext = new WebhotelContext();
        }
        public IActionResult Access()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "home");
            }
            return View("index");
        }
        [HttpPost]
        public async Task<IActionResult> Access(VMLogin mLogin)
        {
            var ac = _webhotelContext.Accounts.Where(i => i.Username == mLogin.Username).FirstOrDefault();
            if (ac != null)
            {
                if (ac.Password.Equals(CommonMethods.convertToEncrypt(mLogin.Password)))
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, ac.Username),
                        new Claim("Role",ac.Role)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = mLogin.KeepLoggedIn
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Home");
                }
                ViewData["ValidateMessage"] = "Incorrect password";
                return View("index");
            }
            ViewData["ValidateMessage"] = "User not found";
            return View("index");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Signup(VMLogin ac, string email, string name)
        {
            if (_webhotelContext.Accounts.Where(i => i.Username == ac.Username).FirstOrDefault() == null)
            {
                Account account = new Account()
                {
                    Username = ac.Username,
                    Password = CommonMethods.convertToEncrypt(ac.Password),
                    Role = "user",
                    Email = email,
                    Name = name
                };
                _webhotelContext.Add(account);
                await _webhotelContext.SaveChangesAsync();
                ViewBag.registerMessage = "You have successfully registered";
                return View("index");
            }
            ViewBag.registerMessage = "Account with this Username Already Exists";
            return View("index");
        }
    }
}