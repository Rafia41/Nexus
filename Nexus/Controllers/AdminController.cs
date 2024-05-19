using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NEXUS.Data;
using NEXUS.Models;
using System.Net.Mail;
using System.Net;

namespace NEXUS.Controllers
{
    public class AdminController : Controller
    {
        private readonly NexusDbContext db;
        public AdminController(NexusDbContext db)
        {
            this.db = db;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult IndexAdmin()
        {
            return View();
        }

        public IActionResult Conn()
        {
            return View();
        }

        public IActionResult AdditionalService()
        {
            return View();
        }

        public IActionResult CategoryAdmin()
        {
            return View();
        }

        public IActionResult AddCategory(CategoryAdmin catg)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(catg);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchCategory));
            }
            return View();
        }

        public IActionResult FetchCategory()
        {
            return View(db.CategoryAdmins.ToList());
        }

        public IActionResult deletecategory2(int? id)
        {
            var data = db.CategoryAdmins.FirstOrDefault(x => x.CatgId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchCategory));
        }

        public IActionResult ProductAdmin()
        {
            ViewBag.Category = new SelectList(db.CategoryAdmins, "CatgId", "CatgName");
            return View();
        }

        public IActionResult AddProduct(Product prod, IFormFile ProdImage)
        {
            if (ProdImage != null && ProdImage.Length > 0)
            {
                var fileName = Path.GetFileName(ProdImage.FileName);
                string folderPath = Path.Combine("wwwroot/assets/img", fileName);
                var dbPath = Path.Combine("assets/img", fileName);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    ProdImage.CopyTo(stream);
                }
                prod.ProdImage = dbPath;
                db.Add(prod);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchProduct));
            }
            return View("ProductAdmin");
        }
        public IActionResult FetchProduct()                                                           
        {
            return View(ViewData["Products"] = db.Products.Include("ProdCatg").ToList());
        }

        public IActionResult deleteProduct(int? id)
        {
            var data = db.Products.FirstOrDefault(x => x.ProdId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchProduct));
        }

        public IActionResult editProduct(int? id)
        {
            var data = db.Products.FirstOrDefault(x => x.ProdId == id);
            ViewBag.Category = new SelectList(db.CategoryAdmins, "CatgId", "CatgName");
            return View(data);
        }

        public IActionResult EditProduct2(Product prod, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbAddress = Path.Combine("assets/img", unique_name);
                prod.ProdImage = dbAddress;
                db.Update(prod);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchProduct));
            }
            else
            {
                var existingProduct = db.Products.FirstOrDefault(p => p.ProdId == prod.ProdId);
                if (existingProduct != null)
                {
                    prod.ProdImage = existingProduct.ProdImage;

                    // Detach existing tracked entity
                    db.Entry(existingProduct).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchProduct));
                }
            }

            // Update entity state
            db.Update(prod);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Data";
            return RedirectToAction(nameof(FetchProduct));
        }

        [HttpGet]
        public IActionResult ConnPackage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConnPackage(ConnectionPackage pack2)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(pack2);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchConnPackage));
            }
            return View();
        }

        public IActionResult FetchConnPackage()
        {
            return View(db.ConnectionPackages.ToList());
        }

        public IActionResult deletePackage2(int? id)
        {
            var data = db.ConnectionPackages.FirstOrDefault(x => x.PackageId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchConnPackage));
        }

        [HttpGet]
        public IActionResult TypeOfConnection()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Typeofconnection2(TypeOfConnection conn2)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(conn2);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchConnection));
            }
            return View();
        }

        public IActionResult FetchConnection()
        {
            return View(db.TypeOfConnections.ToList());
        }

        public IActionResult deleteConnection2(int? id)
        {
            var data = db.TypeOfConnections.FirstOrDefault(x => x.ConnId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchConnection));
        }

        [HttpGet]
        public IActionResult Additionalservice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddService2(AdditionalService add2)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(add2);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchAddService));
            }
            return View();
        }

        public IActionResult deleteService(int? id)
        {
            AdditionalService? additionalService = db.AdditionalServices.FirstOrDefault(x => x.AddId == id);
            var data = additionalService;
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchAddService));
        }

        public IActionResult FetchAddService()
        {
            return View(db.AdditionalServices.ToList());
        }

        public IActionResult Packages()
        {
            ViewBag.Package = new SelectList(db.ConnectionPackages, "PackageId", "PackageName");
            return View();
        }

        public IActionResult AddPackages(PackagePlan pack)
        {
            if (ModelState.IsValid)
            {
                db.Add(pack);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchPackages));
            }
            return View();
        }

        public IActionResult FetchPackages()
        {
            return View(db.PackagePlans.ToList());
        }

        public IActionResult deletePackage(int? id)
        {
            var data = db.PackagePlans.FirstOrDefault(x => x.PackId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchPackages));
        }

        public IActionResult editPackage(int? id)
        {
            ViewBag.Package = new SelectList(db.ConnectionPackages, "PackageId", "PackageName");
            var data = db.PackagePlans.FirstOrDefault(x => x.PackId == id);
            return View(data);
        }

        public IActionResult EditPackage2(PackagePlan pack)
        {
            // Update entity state
            db.Update(pack);
            db.SaveChanges();

            TempData["UpdateMessage"] = "Record Updated Successfully";
            return RedirectToAction(nameof(FetchPackages));
        }


        [HttpGet]
        public IActionResult SecurityDepost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSecurity(SecurityDeposit SD)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(SD);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchSD));
            }
            return View();
        }

        public IActionResult FetchSD()
        {
            return View(db.SecurityDeposits.ToList());
        }

        public IActionResult deleteSD2(int? id)
        {
            var data = db.SecurityDeposits.FirstOrDefault(x => x.SecurityId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchSD));
        }

        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPayment(PaymentMethod pay)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(pay);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchPayment));
            }
            return View();
        }

        public IActionResult FetchPayment()
        {
            return View(db.PaymentMethods.ToList());
        }

        public IActionResult deletepayment2(int? id)
        {
            var data = db.PaymentMethods.FirstOrDefault(x => x.PayId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchPayment));
        }

        public IActionResult TeleService()
        {
            return View();
        }

        public IActionResult AddTele(TeleService tele)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(tele);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchTeleService));
            }
            return View();
        }

        public IActionResult FetchTeleService()
        {
            return View(db.TeleServices.ToList());
        }

        public IActionResult deleteteleservice2(int? id)
        {
            var data = db.TeleServices.FirstOrDefault(x => x.TeleId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchTeleService));
        }


        public IActionResult InternetService()
        {
            return View();
        }

        public IActionResult AddInternet(InternetService inter)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(inter);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchInternetService));
            }
            return View();
        }

        public IActionResult FetchInternetService()
        {
            return View(db.InternetServices.ToList());
        }

        public IActionResult deleteInternet(int? id)
        {
            var data = db.InternetServices.FirstOrDefault(x => x.InternetId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchInternetService));
        }

        public IActionResult DialService()
        {
            return View();
        }

        public IActionResult AddDial(DialService dial)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(dial);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchDialService));
            }
            return View();
        }

        public IActionResult FetchDialService()
        {
            return View(db.DialServices.ToList());
        }

        public IActionResult deletedial(int? id)
        {
            var data = db.DialServices.FirstOrDefault(x => x.DialId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchDialService));
        }

        public IActionResult BroadService()
        {
            return View();
        }

        public IActionResult AddBroad(BroadService broad)
        {
            if (ModelState.IsValid) // validation
            {
                db.Add(broad);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchBroadService));
            }
            return View();
        }
        public IActionResult FetchBroadService()
        {
            return View(db.BroadServices.ToList());
        }

        public IActionResult deletebroad(int? id)
        {
            var data = db.BroadServices.FirstOrDefault(x => x.BroadId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchBroadService));
        }


        [HttpGet]
        public IActionResult Plan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Plan(PlanDb2 plan)
        {
            
                db.Add(plan);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchPlan));
            
            
        }
        public IActionResult FetchPlan()
        {
            return View(db.PlanDb2s.ToList());
        }

        public IActionResult deletePlan2(int? id)
        {
            var data = db.PlanDb2s.FirstOrDefault(x => x.Plan2Id == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchPlan));
        }

        public IActionResult Team()
        {
            return View();
        }

        public IActionResult AddTeam(Team team, IFormFile PersonImage)
        {
            if (PersonImage != null && PersonImage.Length > 0)
            {
                var fileName = Path.GetFileName(PersonImage.FileName);
                string folderPath = Path.Combine("wwwroot/assets/img", fileName);
                var dbPath = Path.Combine("assets/img", fileName);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    PersonImage.CopyTo(stream);
                }
                team.PersonImage = dbPath;
                db.Add(team);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchTeam));
            }
            return View("Team");
        }
        public IActionResult FetchTeam()
        {
            return View(db.Teams.ToList());
        }

        public IActionResult deleteTeam(int? id)
        {
            var data = db.Teams.FirstOrDefault(x => x.PersonId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchTeam));
        }

        public IActionResult editTeam(int? id)
        {
            var data = db.Teams.FirstOrDefault(x => x.PersonId == id);
            return View(data);
        }

        public IActionResult EditTeam2(Team team, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbAddress = Path.Combine("assets/img", unique_name);
                team.PersonImage = dbAddress;
                db.Update(team);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchTeam));
            }
            else
            {
                var existingProduct = db.Teams.FirstOrDefault(p => p.PersonId == team.PersonId);
                if (existingProduct != null)
                {
                    team.PersonId = existingProduct.PersonId;

                    // Detach existing tracked entity
                    db.Entry(existingProduct).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchTeam));
                }
            }

            // Update entity state
            db.Update(team);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Data";
            return RedirectToAction(nameof(FetchTeam));
        }

    }

}