
using System.Linq;

using System.Web.Mvc;
using System.Web.Security;
using OnlineDepartmentAutomationSystem.Models;

namespace OnlineDepartmentAutomationSystem.Controllers
{
    public class LoginController : Controller
    {
        readonly DeptAutDbContext _db = new DeptAutDbContext();


        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.loginErrorId = 1;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.Role == "Student")
                {
                    var u =
                        _db.Logins.FirstOrDefault(m => m.Username.Equals(model.Username) && m.Password.Equals(model.Password));

                    if (u == null)
                    {
                        ModelState.AddModelError("", "* Invalid Username and Password Combination.");
                    }
                    else
                    {
                       
                        FormsAuthentication.SetAuthCookie(model.Username, false);
                        return RedirectToAction("Index", "Student");
                    }
                }
                if (model.Role == "Teacher")
                {
                  
                        ModelState.AddModelError("", "* Not Possible now.");
                 
                }
            }



            return View();
        }

        public RedirectToRouteResult LogOff()
        {
            FormsAuthentication.SignOut();
          return  RedirectToAction("Index", "Home");
        }
    }
}
