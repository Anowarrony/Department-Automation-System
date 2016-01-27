
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using OnlineDepartmentAutomationSystem.Models;

namespace OnlineDepartmentAutomationSystem.Controllers
{
    public class AdminLoginController : Controller
    {
        readonly DeptAutDbContext _db = new DeptAutDbContext();

        public ActionResult LogOn()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.loginErrorId = 1;
            }
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(AdminLoginModel admin)
        {
            if (!ModelState.IsValid) return View();
            if (
                _db.AdminLoginTables.SingleOrDefault(
                    m => m.Username.Equals(admin.Username) && m.Password.Equals(admin.Password)) == null)
            {
                ModelState.AddModelError("", "Invalid Username and Password Combination!");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(admin.Username, false);
                return RedirectToAction("DisplayAdminHomePage","Admin");
            }
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
