using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electro_shopp.Models;
using Electro_shopp.ViewModel;

namespace Electro_shopp.Controllers
{
    public class ShoppingController : Controller
    {
        private electro_shop_dbEntities objelectro_shop_dbEntities;

        public ShoppingController()
        {
            objelectro_shop_dbEntities=new electro_shop_dbEntities();
        }
        public ActionResult Index()
        {
            IEnumerable<ShoppingViewModel> listOfShoppingViewModels = (from objItem in objelectro_shop_dbEntities.Items
                                                                       join
                                                                       objCate in objelectro_shop_dbEntities.Categories
                                                                       on objItem.CategoryId equals objCate.CategoryId
                                                                       select new ShoppingViewModel()
                                                                       {
                                                                           ImagePath=objItem.ImagePath,
                                                                           ItemName=objItem.ItemName,
                                                                           Description=objItem.Description,
                                                                           ItemPrice=objItem.ItemPrice,
                                                                           ItemId=objItem.ItemId,
                                                                           Category=objCate.CategoryName,
                                                                           ItemCode=objItem.ItemCode
                                                                       }

                                                                     ).ToList();
            return View(listOfShoppingViewModels);
        }

    }
}