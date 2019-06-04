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
    public class SlideBannerController : ControllerBase
    {
        [HttpGet]
        public ActionResult Search()
        {
            var total = 0;
            var listSlideBanner = ServiceFactory.SlideBannerManager.Search(0, 1000, ref total, Culture);
            return View(listSlideBanner);
        }
        [HttpGet]
        public ActionResult Update(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.SlideBannerManager.Get(new SlideBanner { SlideBannerId = (int)Id });
                if (obj != null)
                {
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }
        [HttpGet]
        public ActionResult Create()
        {
            SlideBanner data = new SlideBanner();
            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SlideBanner model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ServiceFactory.SlideBannerManager.Add(model, Culture);
                    return RedirectToAction("Search", "SlideBanner");
                }
                catch (Exception e)
                {

                }
            }
            return View("Update", model);
        }
        [HttpPost]
        public ActionResult Update(SlideBanner model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.SlideBannerManager.Get(new SlideBanner { SlideBannerId = model.SlideBannerId });
                if (obj != null)
                {
                    try
                    {
                        ServiceFactory.SlideBannerManager.Update(model, obj);

                        return RedirectToAction("Search", "SlideBanner");
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            ViewBag.IsEdit = true;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.SlideBannerManager.Get(new SlideBanner { SlideBannerId = (int)Id });
                if (obj != null)
                {
                    try
                    {
                        ServiceFactory.SlideBannerManager.Remove(obj);
                        return RedirectToAction("Search", "SlideBanner");
                    }
                    catch (Exception e)
                    {

                        return RedirectToAction("Search", "SlideBanner");
                    }
                }
            }
            return RedirectToAction("Search", "SlideBanner");
        }

    }

}