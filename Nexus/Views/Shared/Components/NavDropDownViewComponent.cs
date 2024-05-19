using NEXUS.Data;
using Microsoft.AspNetCore.Mvc;

namespace NEXUS.Views.Shared.Components
{
	public class NavDropDownViewComponent : ViewComponent 
	{
        private readonly NexusDbContext db;

        public NavDropDownViewComponent(NexusDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            return View(db.CategoryAdmins.ToList());
        }
    }
}
