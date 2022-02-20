using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electro_shopp.Models;
using Electro_shopp.ViewModel;
using System.IO;

namespace Electro_shopp.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item

        private electro_shop_dbEntities2 objelectro_Shop_DbEntities; 

        public ItemController()
        {
            objelectro_Shop_DbEntities=new electro_shop_dbEntities2();

        }

        public ActionResult Index()
        {
            ItemViewModel objItemViewModel = new ItemViewModel();

            objItemViewModel.CategorySelectListItem = (from objCat in objelectro_Shop_DbEntities.Categories
                                                        select new SelectListItem()
                                                        {
                                                            Text = objCat.CategoryName,
                                                            Value=objCat.CategoryId.ToString(),
                                                            Selected=true

                                                        });
            return View(objItemViewModel);
        }
        [HttpPost]
        public JsonResult Index(ItemViewModel objItemViewModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName); //Guid will give Image name path ... is a extension
            objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Item objItem = new Item();
            objItem.ImagePath = "~/Images/" + NewImage;
            objItem.CategoryId = objItemViewModel.CategoryId;
            objItem.Description = objItemViewModel.Description;
            objItem.ItemCode = objItemViewModel.ItemCode;
            objItem.ItemId = Guid.NewGuid();
            objItem.ItemName = objItemViewModel.ItemName;
            objItem.ItemPrice = objItemViewModel.ItemPrice;
            objelectro_Shop_DbEntities.Items.Add(objItem);
            objelectro_Shop_DbEntities.SaveChanges();



            return Json(new {Success=true ,Message="Item is added Successfully"}, JsonRequestBehavior.AllowGet);
        }
    }
}