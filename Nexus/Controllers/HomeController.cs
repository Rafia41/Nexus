using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NEXUS.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using NEXUS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NEXUS.Controllers
{
    public class HomeController : Controller
    {
        private readonly NexusDbContext db;
        public HomeController(NexusDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult Order()
        {
            return View();
        }

        public IActionResult About()
        {
            return View(db.Teams.ToList());
        }

        //public IActionResult Contact()
        //{
        //    ViewBag.Package = new SelectList(db.ConnectionPackages, "PackageId", "PackageName");
        //    ViewBag.Connection = new SelectList(db.TypeOfConnections, "ConnId", "ConnName");
        //    return View();
        //}

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult contact_us(Contact cont)
        {
            try
            {
                if (string.IsNullOrEmpty(cont.ContEmail))
                {
                    ModelState.AddModelError("ContEmail", "Email is required");
                    return View("Contact", cont);
                }

                db.Add(cont);
                db.SaveChanges();

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true; // Enable SSL
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("sherryop121@gmail.com", "lfwj wawa xoqt cfub");

                    using (MailMessage msg = new MailMessage("sherryop121@gmail.com", cont.ContEmail))
                    {
                        msg.Subject = "Thank you for contacting us!";
                        msg.Body = cont.ContName + ", thank you for contacting us! ";

                        client.Send(msg);
                    }
                }


                TempData["Success"] = "Your message has been successfully sent! We will get back to you soon.";

                return RedirectToAction(nameof(Contact));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while processing your request. Please try again later. Error Message: " + ex.Message;
                return RedirectToAction("Contact", cont);
            }
        }

        [Authorize(Roles = "User")]

        public IActionResult UpDowngrade()
        {
            ViewBag.Upgrade1 = new SelectList(db.ConnectionPackages, "PackageId", "PackageName");
            ViewBag.Upgrade2 = new SelectList(db.ConnectionPackages, "PackageId", "PackageName");
            return View();

        }
        [Authorize(Roles = "User")]
        public IActionResult Plan()
        {
            return View(db.PlanDb2s.ToList());
        }


        public IActionResult TeleCommunication()
        {
            return View(db.TeleServices.ToList());
        }


        public IActionResult InternetService()
        {
            return View(db.InternetServices.ToList());
        }

        public IActionResult DialupConnection()
        {
            return View(db.DialServices.ToList());
        }
        public IActionResult BroadbandConnection()
        {
            return View(db.BroadServices.ToList());
        }
  
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserRecord user)
        {
            if (ModelState.IsValid)
            {
                user.UserRoleId = 2;
                db.Add(user);
                db.SaveChanges();
                TempData["Message"] = "User Registered Successfully..";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        public IActionResult Login(UserRecord User)
        {
            var data = db.UserRecords.FirstOrDefault(x => x.UserEmail == User.UserEmail && x.UserPassword == User.UserPassword);
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            if (data != null)
            {

                if (data.UserRoleId == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
               new Claim(ClaimTypes.Name, data.UserName),
               new Claim(ClaimTypes.Email, data.UserEmail),
                new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
               new Claim(ClaimTypes.Role,"Admin")
           }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                else
                {
                    identity = new ClaimsIdentity(new[]
                 {
              new Claim(ClaimTypes.Name, data.UserName),
               new Claim(ClaimTypes.Email, data.UserEmail),
                new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
               new Claim(ClaimTypes.Role,"User")

           }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (data.UserRoleId == 1)
                    {
                        return RedirectToAction("IndexAdmin", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }


        public IActionResult ShowProdCatg(int? id)
        {
            return View(db.Products.Where(x=>x.ProdCatgId==id).Include("ProdCatg").ToList());
        }


        public IActionResult Connection()
        {

            return View();
        }

        public IActionResult SubmitOrder()
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
