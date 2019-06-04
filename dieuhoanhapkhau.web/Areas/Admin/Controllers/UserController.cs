using dieuhoanhapkhau.biz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dieuhoanhapkhau.web.Areas.Admin.Controllers
{
    public class UserController : ControllerBase
    {
        //[RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult List()
        {
            var totalItem = 0;
            var list = ServiceFactory.UserManager.GetAll(0, 1000, ref totalItem);
            return View(list);
        }
        #region Promote / Demote / Delete
        //[RequireAuthorization(UserRole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Promote(string id)
        {
            ServiceFactory.UserManager.Promote(id);
            return RedirectToAction("List");
        }

        //[RequireAuthorization(UserRole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Demote(string id)
        {
            ServiceFactory.UserManager.Demote(id);
            return RedirectToAction("List");
        }

        //[RequireAuthorization(UserRole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, bool IsActive)
        {
            var status = false;
            if (!IsActive)
            {
                status = true;
            }
            ServiceFactory.UserManager.Delete(id, status);
            return RedirectToAction("List");
        }
        //[RequireAuthorization(UserRole.Admin)]
        [HttpPost]
        public ActionResult ChangePass(string userName, string newPass)
        {
            var flag = ServiceFactory.UserManager.ChangePassword("", userName, newPass);
            return RedirectToAction("List");
        }
        #endregion
    }
}