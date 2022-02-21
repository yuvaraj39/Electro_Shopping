using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electro_shopp.Models;
using Electro_shopp.ViewModel;
using System.Web.Security;
namespace Electro_shopp.Controllers
{
    public class HomeController : Controller
    {
        electro_shop_dbEntities3 objelectro_shop_dbEntities = new electro_shop_dbEntities3();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Service()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login login)
        {
            var user = objelectro_shop_dbEntities.Logins.Where(x => x.Username.Equals(login.Username) && x.Password.Equals(login.Password)).FirstOrDefault();

            if (user!=null)
            {
                Session["Id"] = login.Id.ToString();
                Session["Username"] = login.Username.ToString();
                return RedirectToAction("Dashboard","Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Username or Password";
            }
            return View();
        }
        
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SignUp()
        {
                return View();
        }
        [HttpPost]
        public ActionResult SignUp(Login login)
        {
            if(objelectro_shop_dbEntities.Logins.Any(x=>x.Username == login.Username))
            {
                ViewBag.notification = "The account has already existed";
                return View();
            }
            else
            {
                objelectro_shop_dbEntities.Logins.Add(login);
                objelectro_shop_dbEntities.SaveChanges();
                Session["Id"] = login.Id.ToString();
                Session["Username"] = login.Username.ToString();
                return RedirectToAction("Dashboard","Home");
            }
        }
        public ActionResult Dashboard()
        {
            IEnumerable<ShoppingViewModel> listOfShoppingViewModels = (from objItem in objelectro_shop_dbEntities.Items
                                                                       join
                                                                       objCate in objelectro_shop_dbEntities.Categories
                                                                       on objItem.CategoryId equals objCate.CategoryId
                                                                       select new ShoppingViewModel()
                                                                       {
                                                                           ImagePath = objItem.ImagePath,
                                                                           ItemName = objItem.ItemName,
                                                                           Description = objItem.Description,
                                                                           ItemPrice = objItem.ItemPrice,
                                                                           ItemId = objItem.ItemId,
                                                                           Category = objCate.CategoryName,
                                                                           ItemCode = objItem.ItemCode
                                                                       }

                                                                     ).ToList();
            return View(listOfShoppingViewModels);
        }

    }
    

}