using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electro_shopp.Models;

namespace Electro_shopp.Controllers
{
    public class AdminController : Controller
    {
        electro_shop_dbEntities3 objelectro_shop_dbEntities = new electro_shop_dbEntities3();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin login)
        {
            var user = objelectro_shop_dbEntities.Admins.Where(x => x.Username.Equals(login.Username) && x.Password.Equals(login.Password)).FirstOrDefault();

            if (user != null)
            {
                Session["AdminId"] = login.Id.ToString();
                Session["AdminUsername"] = login.Username.ToString();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ViewBag.Notification = "Wrong Username or Password";
            }
            return View();
        }

        public ActionResult Dashboard(Login login)
        {
            return View(objelectro_shop_dbEntities.Logins.ToList());
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Admin");
        }
    }
}