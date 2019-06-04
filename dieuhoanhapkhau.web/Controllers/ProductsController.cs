using dieuhoanhapkhau.biz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.web.Helpers;
using System.Configuration;

namespace dieuhoanhapkhau.web.Controllers
{
    public class ProductsController : ControllerBase
    {
        private int _userPageSize = 20;
        public ActionResult Detail(string productcode, string shortname)
        {
            
            var product = ServiceFactory.ProductManager.GetByCode(new Product { ProductCode = productcode }, Culture);
            if (product != null)
            {
                product.Properties = ServiceFactory.ProductPropertyManager.GetByPrdId(product.ProductId, Culture);
                product.ProductImages = ServiceFactory.ImageProductManager.GetByPrdId(product.ProductId, Culture);
                

                
                #region["ProductImages"]
                product.ProductImages = ServiceFactory.ImageProductManager.GetAllActive(product.ProductId, Culture);

                #endregion
                ViewBag.Keywords = product.ProductKeyword;
                ViewBag.Desciption = product.ProductDescription;
                ViewBag.MetaOGImage = product.ProductImage;
                string code = "%" + product.ProductCode + product.ProductId + "%";
                
                return View(product);
            }
            else
            {
                return ResultHelper.NotFoundResult(this);
            }
        }
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
        public ActionResult GetByParentId(int parentId, int? page = 1)
        {

            var listCateParent = ServiceFactory.ProductCategoryManager.GetByParentId(parentId);
            ViewBag.ParentId = parentId;

            return View(listCateParent);

        }
        public ActionResult GetByProducer(int id, int parentid)
        {
            var list = ServiceFactory.ProductManager.ProductGetByProducer(id, parentid);
            ViewBag.ParentId = parentid;
            ViewBag.Manufacter = ServiceFactory.ProducerManager.Get(new Producers { ProducerId = id });
            return View(list);
            //var procate = ServiceFactory.ProductManager.ProductGetByCateId(productid, Culture);
            //var listByProducer = ServiceFactory.ProductManager.ProductGetByPrpducerPropertyId(producerId,producerpropertyid, productid);
            //ViewBag.Procate = procate;
            //return View(listByProducer);
        }
        public ActionResult GetByPropertyShortName(string shortname, string culture, int? page = 1)
        {
            var total = 0;
            var listPro = ServiceFactory.ProductManager.GetByPropertyShortName(Culture, shortname, 0, 1000, ref total);
            ViewData["Page"] = page;
            ViewData["TotalItems"] = total;
            ViewBag.PageSize = _userPageSize;
            var propety = ServiceFactory.ProductPropertyManager.GetByShortName(shortname, Culture);
            ViewBag.Property = propety;
            ViewBag.Pager = dieuhoanhapkhau.web.Models.Pager.Items(total).PerPage(_userPageSize).Move((int)page).Segment(5).Center();
            return View(listPro);
        }
    }
    
}