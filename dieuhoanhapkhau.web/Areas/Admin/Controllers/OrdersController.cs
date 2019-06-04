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
    public class OrdersController : ControllerBase
    {

        // GET: Orders
        
        [HttpGet]
        public ActionResult Search()
        {
            var list = ServiceFactory.OrderManager.GetAll();
            return View(list);
        }
        
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = ServiceFactory.OrderManager.Get(new Orders { OrderId = (int)id });

                ViewBag.IsEdit = true;
                return View(obj);
            }

            return null;

        }
        
        [HttpPost]
        public ActionResult Update(Orders model)
        {
            if (ModelState.IsValid)
            {
                var order = ServiceFactory.OrderManager.Get(new Orders { OrderId = model.OrderId });
                if (order != null)
                {
                    try
                    {

                        ServiceFactory.OrderManager.Update(model, order);
                        return RedirectToAction("Search", "Orders");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }


            ViewBag.IsEdit = true;
            return View(model);
        }
        
        [HttpGet]
        public ActionResult OrderDetail(int id)
        {
            var listDetails = ServiceFactory.OrderDetailManager.GetByOrderId(id);
            return View(listDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Detail(int proid, int orderid)
        {
            if (ServiceFactory.OrderDetailManager.Delete_Detail(proid, orderid))
            {
                return Json(new { Success = true, Message = "Xóa thành công!" });
            }
            return Json(new { Success = false, Message = "Xóa thất bại!" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int orderid)
        {
            if (ServiceFactory.OrderManager.Delete(orderid))
            {
                return Json(new { Success = true, Message = "Xóa thành công!" });
            }
            return Json(new { Success = false, Message = "Xóa thất bại!" });
        }
    }
}

