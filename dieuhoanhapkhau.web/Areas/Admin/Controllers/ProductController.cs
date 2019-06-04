using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using dieuhoanhapkhau.web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idocNet.Core.Extensions;
using System.IO;
using System.Configuration;
using dieuhoanhapkhau.web.Utils;
using dieuhoanhapkhau.web.Filters;

namespace dieuhoanhapkhau.web.Areas.Admin.Controllers
{
    public class ProductController : ControllerBase
    {
        //
        // GET: /Product/
        private int _userPageSize = 12;
        #region Admin
        
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult SearchCate()
        {
            var totalItem = 0;
            var list = ServiceFactory.ProductCategoryManager.Search(0, 1000, ref totalItem, Culture);
            return View(list);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult UpdateCate(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.ProductCategoryManager.Get(new ProductCategory { PrdCategoryId = (int)Id });
                if (obj != null)
                {
                    var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
                    ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
                    var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
                    ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCate(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.ProductCategoryManager.Get(new ProductCategory { PrdCategoryId = model.PrdCategoryId });
                if (obj != null)
                {
                    try
                    {
                        model.PrdCategoryShortName = model.PrdCategoryTitle.ToUrlSegment(250).ToLower();

                        ServiceFactory.ProductCategoryManager.Update(model, obj);

                        return RedirectToAction("SearchCate", "Product");
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            ViewBag.IsEdit = true;
            return View(model);
        }

        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult CreateCate()
        {
            ProductCategory data = new ProductCategory();
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            return View("UpdateCate", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCate(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.PrdCategoryShortName = model.PrdCategoryTitle.ToUrlSegment(250).ToLower();
                    model.CreateBy = CurrentUser.Name;
                    ServiceFactory.ProductCategoryManager.Add(model, Culture);

                    return RedirectToAction("SearchCate", "Product");
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            return View("UpdateCate", model);
        }

       
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Search()
        {
            var totalItem = 0;
            var list = ServiceFactory.ProductManager.Search(0, 1000, ref totalItem, Culture);
            return View(list);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Create(int producerId=0)
        {
            Product data = new Product();
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            var property = ServiceFactory.ProductPropertyManager.GetAllProperty(Culture);
            ViewBag.Property = new SelectList(property, "ProductPropertyId", "ProductPropertyTitle");
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ProductCode = model.ProductName.ToUrlSegment(250).ToLower();
                    model.CreateBy = CurrentUser.Name;
                    ServiceFactory.ProductManager.Add(model, Culture);
                    return RedirectToAction("Search", "Product");
                }
                catch (Exception e)
                {

                    //throw;
                }
            }
            //var productpty = ServiceFactory.ProducerPropertyManager.GetAllProducerProperty(Culture);
            var property = ServiceFactory.ProductPropertyManager.GetAllProperty(Culture);
            ViewBag.Property = new SelectList(property, "ProductPropertyId", "ProductPropertyTitle");
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            return View("Update", model);
            //RedirectToAction()

        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Update(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.ProductManager.Get(new Product { ProductId = (int)Id });
                if (obj != null)
                {
                    var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);                   
                    ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
                    var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
                    ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
                    var property = ServiceFactory.ProductPropertyManager.GetAllProperty(Culture);
                    ViewBag.Property = new SelectList(property, "ProductPropertyId", "ProductPropertyTitle");
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Update(Product model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.ProductManager.Get(new Product { ProductId = model.ProductId });
                if (obj != null)
                {
                    try
                    {
                        model.ProductCode = model.ProductName.ToUrlSegment(250).ToLower();
                        model.UpdateBy = CurrentUser.Name;
                        ServiceFactory.ProductManager.Update(model, obj);

                        return RedirectToAction("Search", "Product");
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            var property = ServiceFactory.ProductPropertyManager.GetAllProperty(Culture);
            ViewBag.Property = new SelectList(property, "ProductPropertyId", "ProductPropertyTitle");
            ViewBag.IsEdit = true;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = ServiceFactory.ProductManager.Get(new Product() { ProductId = id });
                if (product != null)
                {
                    ServiceFactory.ProductManager.Remove(new Product() { ProductId = id });
                    return new JsonSuccess("Sản phẩm" + Constants.Message.DELETE_SUCCESS_MESSAGE_SUFIX);
                }

                return new JsonError("Sản phẩm" + Constants.Message.NOT_FOUND_MESSAGE_SUFIX);
            }
            catch (Exception ex)
            {
                return new JsonError(message: Constants.Message.SERVER_ERROR_MESSAGE, log: ex.ToString());
            }
        }

        public ActionResult AddProp(string name = "")
        {
            ViewData["PropName"] = name.Trim();
            ViewData["PropId"] = name.Trim().ToUrlSegment();
            return PartialView("AddProp");
        }

        //Properties
        
        #endregion
        #region Property
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Properties(/*int ProductId*/)
        {
            //Product product = ServiceFactory.ProductManager.Get(new Product { ProductId = ProductId });
            //List<ProductProperty> listver = new List<ProductProperty>();
            //listver = ServiceFactory.ProductPropertyManager.GetByPrdId(ProductId, Culture);
            //product.Properties = listver;
            //return View(product);
            var totalItem = 0;
            var list = ServiceFactory.ProductPropertyManager.Search(0, 1000, ref totalItem, Culture);
            return View(list);

        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult UpdatePro(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.ProductPropertyManager.Get(new ProductProperty { ProductPropertyId = (int)Id });
                if (obj != null)
                {
                    ViewBag.IsEdit = true;
                    var pro = ServiceFactory.ProductManager.Get(new Product { ProductId = obj.ProductId });
                    var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
                    ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
                    var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
                    ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
                    ViewBag.Products = pro;
                    return View(obj);
                }
            }
            return null;
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePro(ProductProperty model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.ProductPropertyManager.Get(new ProductProperty { ProductPropertyId = model.ProductPropertyId });
                if (obj != null)
                {
                    try
                    {
                        model.ShortName = model.ProductPropertyTitle.ToUrlSegment(250).ToLower();
                        ServiceFactory.ProductPropertyManager.Update(model, obj);

                        return RedirectToAction("Properties", "Product", new { productid = model.ProductId });
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            ViewBag.IsEdit = true;
            var pro = ServiceFactory.ProductManager.Get(new Product { ProductId = model.ProductId });
            ViewBag.Products = pro;
            return View(model);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult CreatePro()
        {
            ProductProperty data = new ProductProperty();
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            return View("UpdatePro", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePro(ProductProperty model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ShortName = model.ProductPropertyTitle.ToUrlSegment(250).ToLower();
                    ServiceFactory.ProductPropertyManager.Add(model, Culture);

                    return RedirectToAction("Properties", "Product");
                }
                catch (Exception e)
                {

                    //throw;
                }
            }
            
            var categories = ServiceFactory.ProductCategoryManager.GetAllProductCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "PrdCategoryId", "HlevelTitle");
            var producers = ServiceFactory.ProducerManager.GetAllProducer(Culture);
            ViewBag.Producers = new SelectList(producers, "ProducerId", "ProducerName");
            
            return View("UpdatePro", model);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePro(int id)
        {
            ServiceFactory.ProductPropertyManager.Remove(new ProductProperty { ProductPropertyId = id });
            return RedirectToAction("Properties", "Product");
        }
        public ActionResult GetProperty(int producerId)
        {
            var data = ServiceFactory.ProducerPropertyManager.GetByProducerId(producerId, Culture);
            return View(data);
        }
        
        #endregion

        #region User
        public ActionResult ListProduct()
        {
            string keyword = ConfigurationManager.AppSettings["keyword"];
            string decsription = ConfigurationManager.AppSettings["description"];
            var categories = ServiceFactory.ProductCategoryManager.ListAllProductCategory(Culture);
            if (categories.Count > 0)
            {
                return RedirectToAction("ListProductByCate", new { shortname = categories[0].PrdCategoryShortName });
            }
            else
            {
                ViewBag.Keywords = keyword;
                ViewBag.Desciption = decsription;
                return View();
            }
        }
        public ActionResult ListProductByCate(string shortname)
        {
            var categories = ServiceFactory.ProductCategoryManager.ListAllProductCategory(Culture);
            var category = ServiceFactory.ProductCategoryManager.GetByShortName(new ProductCategory { PrdCategoryShortName = shortname }, Culture);
            if (category != null)
            {
                category.ListProducts = ServiceFactory.ProductManager.ProductGetByCateId(category.PrdCategoryId, Culture);
                ViewBag.Keywords = category.PrdCategoryKeyword;
                ViewBag.Desciption = category.PrdCategoryDescription;

            }
            else
            {
                return null;
            }
            //var data = ServiceFactory.NewsManager.get
            ViewBag.ListCates = categories;
            return View(category);
        }
        public ActionResult ListProductSale()
        {
            var categories = ServiceFactory.ProductCategoryManager.ListAllProductCategory(Culture);
            if (categories.Count > 0)
            {
                return RedirectToAction("ListProductByCateSale", new { shortname = categories[0].PrdCategoryShortName });
                //return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult ListProductHotNewSale()
        {
            var listpro = ServiceFactory.ProductManager.ProductGetAllActive(Culture);
            ViewData["ListHotNewSaleProduct"] = listpro;
            return View();
        }
        public ActionResult ListProductByCateSale(string shortname)
        {
            var categories = ServiceFactory.ProductCategoryManager.ListAllProductCategory(Culture);
            var category = ServiceFactory.ProductCategoryManager.GetByShortName(new ProductCategory { PrdCategoryShortName = shortname }, Culture);
            if (category != null)
            {
                category.ListProducts = ServiceFactory.ProductManager.ProductGetByCateId(category.PrdCategoryId, Culture);
                ViewBag.Keywords = category.PrdCategoryKeyword;
                ViewBag.Desciption = category.PrdCategoryDescription;
            }
            else
            {
                return null;
            }
            //var data = ServiceFactory.NewsManager.get
            ViewBag.ListCates = categories;
            return View(category);
        }
        public ActionResult Detail(string productcode, string shortname)
        {
            
                return View(this);
        }
        
       
        #endregion

    }
}
