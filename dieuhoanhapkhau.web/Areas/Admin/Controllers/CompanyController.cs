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
    public class CompanyController : ControllerBase
    {
        private const int Pagesize = 20;
        // GET: /SliderBanner/
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Search()
        {
            var Company = ServiceFactory.CompanyManager.Get(new Company { CompanyId = 1 });
            return View(Company);
        }

        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Update(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.CompanyManager.Get(new Company { CompanyId = 1 });
                if (obj != null)
                {
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Company model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.CompanyManager.Get(new Company { CompanyId = model.CompanyId });
                if (obj != null)
                {
                    try
                    {
                        model.UpdateBy = CurrentUser.Name;
                        ServiceFactory.CompanyManager.Update(model, obj);

                        return RedirectToAction("Search", "Company");
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
    }
}
