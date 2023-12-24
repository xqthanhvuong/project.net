using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using webhotel.Models;
using Webhotel.Helper;
using Webhotel.Models;

namespace webhotel.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly WebhotelContext _webHotelContext;

        public ForgotPasswordController()
        {
            _webHotelContext = new WebhotelContext();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            // create a random code
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string code = new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());


            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}";
            var username = _webHotelContext.Accounts.Where(i => i.Email == email).Select(i => i.Username).FirstOrDefault();

            Forgotpass fg = new Forgotpass()
            {
                Username = username,
                ResetPass = code,
                CreatedAt = DateTime.Now
            };
            _webHotelContext.Forgotpasses.Add(fg);
            _webHotelContext.SaveChanges();

            string resetLink = $"{baseUrl}/ForgotPassword/ResetPass?username={username}&code={code}";
            string emailBody = $@"
    <p>Hello,</p>
    <p>You have requested to reset your account password.</p>
    <p>Please click the following link to reset your password:</p>
    <p><a href='{resetLink}'>{resetLink}</a></p>
    <p>If you did not request a password reset, please ignore this email.</p>
    <p>Best regards,</p>
    <p>CIAO hotel management team</p>";
            var myAppConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var userName = myAppConfig.GetValue<string>("EmailConfig:Username");
            var Password = myAppConfig.GetValue<string>("EmailConfig:Password");
            var Host = myAppConfig.GetValue<string>("EmailConfig:Host");
            var Port = myAppConfig.GetValue<int>("EmailConfig:Port");
            var FromEmail = myAppConfig.GetValue<string>("EmailConfig:FromEmail");

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            message.To.Add(email);
            message.Subject = "link to retrieve password of Ciao hotel";
            message.IsBodyHtml = true;
            message.Body = emailBody;
            SmtpClient smtpClient = new SmtpClient(Host);
            try
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(userName, Password);
                smtpClient.Host = Host;
                smtpClient.Port = Port;
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                smtpClient.Dispose();
            }
            return View("index");
        }
        [HttpGet]
        public IActionResult ResetPass(string username, string code)
        {
            if (username != null || code != null)
            {
                bool valid = _webHotelContext.Forgotpasses.Any(i => i.Username == username && i.ResetPass == code);
                if (valid)
                {
                    string encryptedUsername = CommonMethods.convertToEncrypt(username);
                    HttpContext.Session.SetString("Username", encryptedUsername);
                    return View();
                }
                return NotFound();
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult ResetPass(string Password)
        {
            var username = CommonMethods.convertToDecrypt(HttpContext.Session.GetString("Username"));
            if (username != null)
            {
                Account ac = _webHotelContext.Accounts.FirstOrDefault(i => i.Username == username);
                if (ac != null)
                {
                    ac.Password = CommonMethods.convertToEncrypt(Password);
                    _webHotelContext.Accounts.Update(ac);
                    _webHotelContext.SaveChanges();
                }

            }
            return BadRequest();
        }


    }
}
