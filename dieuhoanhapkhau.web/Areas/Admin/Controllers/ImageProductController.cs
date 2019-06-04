using dieuhoanhapkhau.biz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.web.Helpers;

namespace dieuhoanhapkhau.web.Areas.Admin.Controllers
{
    public class ImageProductController : ControllerBase
    {
        // GET: Admin/ImageProduct
       
        [HttpGet]
        public ActionResult Search()
        {
            var totalItem = 0;
            var list = ServiceFactory.ImageProductManager.Search(0, 1000, ref totalItem, Culture);
            return View(list);
        }
        [HttpGet]
        public ActionResult Update(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.ImageProductManager.Get(new ProductImages  { ProductImageId = (int)Id });
                if (obj != null)
                {
                    var categories = ServiceFactory.ProductManager.ProductGetAllActive(Culture);
                    ViewBag.Categories = new SelectList(categories, "ProductId", "HlevelTitle");
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductImages model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.ImageProductManager.Get(new ProductImages { ProductImageId = model.ProductImageId });
                if (obj != null)
                {
                    try
                    {
                        ServiceFactory.ImageProductManager.Update(model, obj);

                        return RedirectToAction("Search", "ImageProduct");
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            var categories = ServiceFactory.ProductManager.ProductGetAllActive(Culture);
            ViewBag.Categories = new SelectList(categories, "ProductId", "HlevelTitle");
            ViewBag.IsEdit = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductImages data = new ProductImages();
            var categories = ServiceFactory.ProductManager.ProductGetAllActive(Culture);
            ViewBag.Categories = new SelectList(categories, "ProductId", "HlevelTitle");
            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductImages model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateBy = CurrentUser.Name;
                    ServiceFactory.ImageProductManager.Add(model, Culture);

                    return RedirectToAction("Search", "ImageProduct");
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            var categories = ServiceFactory.ProductManager.ProductGetAllActive(Culture);
            ViewBag.Categories = new SelectList(categories, "ProductId", "HlevelTitle");
            return View("Update", model);
        }
        public ActionResult Delete(int id)
        {
            ServiceFactory.ImageProductManager.Remove(new ProductImages { ProductImageId = id });
            return RedirectToAction("Search");
        }
    }
}