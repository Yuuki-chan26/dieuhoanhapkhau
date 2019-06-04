using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using dieuhoanhapkhau.web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dieuhoanhapkhau.web.Areas.Admin.Controllers
{
    public class CustomerController : ControllerBase
    {
        public ActionResult Search()
        {
            var list = ServiceFactory.CustomerManager.GetAll();
            return View(list);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = ServiceFactory.CustomerManager.Get(new Customers { CustomerId = (int)id });
                
                ViewBag.IsEdit = true;
                return View(obj);
            }

            return null;
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Customers model)
        {
            if (ModelState.IsValid)
            {
                var pro = ServiceFactory.CustomerManager.Get(new Customers { CustomerId = model.CustomerId });
                if (pro != null)
                {
                    try
                    {
                        
                        ServiceFactory.CustomerManager.Update(model, pro);
                        return RedirectToAction("Search", "Customer");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            ViewBag.IsEdit = true;
            return View(model);
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (ServiceFactory.CustomerManager.Delete(id))
            {
                return Json(new { Success = true, Message = "Xóa thành công!" });
            }
            return Json(new { Success = false, Message = "Xóa thất bại!" });
        }
    }
}