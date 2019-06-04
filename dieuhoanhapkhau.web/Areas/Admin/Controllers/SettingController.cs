using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using dieuhoanhapkhau.web.Filters;
using dieuhoanhapkhau.web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dieuhoanhapkhau.web.Areas.Admin.Controllers
{
    public class SettingController : ControllerBase
    {
        // GET: Admin/Setting
        #region LocationDiscount
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult LocationDiscount()
        {
            var total = 0;
            var listLocation = ServiceFactory.LocationDiscountManager.Search(0, 1000, ref total, Culture);
            return View(listLocation);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult CreateLocation()
        {
            LocationDiscount data = new LocationDiscount();
            return View("UpdateLocation", data);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateLocation(LocationDiscount model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ServiceFactory.LocationDiscountManager.Add(model, Culture);
                    return RedirectToAction("LocationDiscount", "Setting");
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            return View("UpdateLocation", model);

        }

        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult UpdateLocation(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = (int)Id });
                if (obj != null)
                {
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }

        [HttpPost]
        public ActionResult UpdateLocation(LocationDiscount model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = model.LocationDiscountId });
                if (obj != null)
                {
                    try
                    {
                        ServiceFactory.LocationDiscountManager.Update(model, obj);

                        return RedirectToAction("LocationDiscount", "Setting");
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

        [RequireAuthorization(UserRole.Admin)]
        public ActionResult DeleteLocation(int? Id)
        {

            if (Id.HasValue)
            {
                var obj = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = (int)Id });
                if (obj != null)
                {
                    try
                    {
                        ServiceFactory.LocationDiscountManager.Remove(obj);
                        return RedirectToAction("LocationDiscount", "Setting");
                    }
                    catch (Exception)
                    {

                        return RedirectToAction("LocationDiscount", "Setting");
                    }
                }
            }
            return RedirectToAction("LocationDiscount", "Setting");
        }

        public ActionResult LocationSearch(int LocationDiscountId)
        {
            LocationDiscount location = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = LocationDiscountId });
            List<Location> listver = new List<Location>();
            listver = ServiceFactory.LocationManager.GetAllActiveByParentId(LocationDiscountId);
            location.Locations = listver;
            return View(location);
        }
        [HttpGet]
        public ActionResult CreatePro(int locationdiscountid)
        {
            Location data = new Location();
            data.LocationDiscountId = locationdiscountid;
            var pro = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = locationdiscountid });
            ViewBag.LocationDiscount = pro;
            return View("UpdatePro", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePro(Location model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ServiceFactory.LocationManager.Add(model, Culture);

                    return RedirectToAction("LocationSearch", "Setting", new { locationdiscountid = model.LocationDiscountId });
                }
                catch (Exception e)
                {

                    //throw;
                }
            }
            var pro = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = model.LocationDiscountId });
            ViewBag.LocationDiscount = pro;
            return View("UpdatePro", model);
        }
        [HttpGet]
        public ActionResult UpdatePro(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.LocationManager.Get(new Location { LocationId = (int)Id });
                if (obj != null)
                {
                    ViewBag.IsEdit = true;
                    var pro = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = obj.LocationDiscountId });
                    ViewBag.LocationDiscount = pro;
                    return View(obj);
                }
            }
            return null;
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePro(Location model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.LocationManager.Get(new Location { LocationId = model.LocationId });
                if (obj != null)
                {
                    try
                    {
                        ServiceFactory.LocationManager.Update(model, obj);

                        return RedirectToAction("LocationSearch", "Setting", new { locationdiscountid = model.LocationDiscountId });
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            ViewBag.IsEdit = true;
            var pro = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = model.LocationDiscountId });
            ViewBag.LocationDiscount = pro;
            return View(model);
        }
        #endregion
    }
}