using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using dieuhoanhapkhau.web.Filters;
using idocNet.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dieuhoanhapkhau.web.Areas.Admin.Controllers
{
    public class ProducersController : ControllerBase
    {
        [HttpGet]
        public ActionResult Search()
        {
            var totalItem = 0;
            var list = ServiceFactory.ProducerManager.Search(0, 1000, ref totalItem, Culture);
            return View(list);
        }
        [HttpGet]
        public ActionResult Update(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.ProducerManager.Get(new Producers { ProducerId = (int)Id });
                if (obj != null)
                {
                    var categories = ServiceFactory.ProductManager.ProductGetAllActive(Culture);
                    ViewBag.Categories = new SelectList(categories, "ProducerId", "HlevelTitle");
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Producers model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.ProducerManager.Get(new Producers { ProducerId = model.ProducerId });
                if (obj != null)
                {
                    try
                    {
                        ServiceFactory.ProducerManager.Update(model, obj);

                        return RedirectToAction("Search", "Producers");
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            
            ViewBag.IsEdit = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Producers data = new Producers();
            var categories = ServiceFactory.ProductManager.ProductGetAllActive(Culture);
            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producers model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateBy = CurrentUser.Name;
                    ServiceFactory.ProducerManager.Add(model, Culture);
                    return RedirectToAction("Search", "Producers");
                }
                catch (Exception ex)
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
            ServiceFactory.ProducerManager.Remove(new Producers { ProducerId = id });
            return RedirectToAction("Search");
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult ProducerProperties(int ProducerId)
        {
            Producers producer = ServiceFactory.ProducerManager.Get(new Producers { ProducerId = ProducerId });
            List<ProducerProperties> listver = new List<ProducerProperties>();
            listver = ServiceFactory.ProducerPropertyManager.GetByProducerId(ProducerId, Culture);
            producer.Producer = listver;
            return View(producer);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult UpdatePro(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.ProducerPropertyManager.Get(new ProducerProperties { ProducerPropertyId = (int)Id });
                if (obj != null)
                {
                    ViewBag.IsEdit = true;
                    var pro = ServiceFactory.ProducerManager.Get(new Producers { ProducerId = obj.ProducerId });
                    ViewBag.Producer = pro;
                    return View(obj);
                }
            }
            return null;
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePro(ProducerProperties model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.ProducerPropertyManager.Get(new ProducerProperties { ProducerPropertyId = model.ProducerPropertyId });
                if (obj != null)
                {
                    try
                    {
                        ServiceFactory.ProducerPropertyManager.Update(model, obj);

                        return RedirectToAction("ProducerProperties", "Producers", new { producerId = model.ProducerId });
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            ViewBag.IsEdit = true;
            var pro = ServiceFactory.ProducerManager.Get(new Producers { ProducerId = model.ProducerId });
            ViewBag.Producer = pro;
            return View(model);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult CreatePro(int producerId)
        {
            ProducerProperties data = new ProducerProperties();
            data.ProducerId = producerId;
            var pro = ServiceFactory.ProducerManager.Get(new Producers { ProducerId = producerId });
            ViewBag.Producer = pro;
            return View("UpdatePro", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePro(ProducerProperties model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    model.CreateBy = CurrentUser.Name;
                    ServiceFactory.ProducerPropertyManager.Add(model, Culture);

                    return RedirectToAction("ProducerProperties", "Producers", new { producerId = model.ProducerId });
                }
                catch (Exception e)
                {

                    //throw;
                }
            }
            var pro = ServiceFactory.ProducerManager.Get(new Producers { ProducerId = model.ProducerId });
            ViewBag.Producer = pro;
            return View("UpdatePro", model);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePro(int id, int producerid)
        {
            ServiceFactory.ProducerPropertyManager.Remove(new ProducerProperties { ProducerPropertyId = id });
            return RedirectToAction("ProducerProperties", "Producers", new { producerid = producerid });
        }

    }
}