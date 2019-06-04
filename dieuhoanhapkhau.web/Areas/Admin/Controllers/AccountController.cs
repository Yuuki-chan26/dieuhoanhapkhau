using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using dieuhoanhapkhau.web.Areas.Admin.Models;
using dieuhoanhapkhau.web.Security;
using dieuhoanhapkhau.web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idocNet.Core.Data.Entities.Validation;

namespace dieuhoanhapkhau.web.Areas.Admin.Controllers
{
    [RequireAuthorization(UserRole.Admin)]
    public class AccountController : ControllerBase
    {
        
        private static UserManager UserManager
        {
            get
            {
                return ServiceFactory.UserManager;
            }
        }
        //
        // GET: /Account/Login
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult LoginError()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.IsLogin = true;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.IsLogin = true;
            try
            {
                ValidateLogOn(model.UserName ?? "", model.Password ?? "");
                var isLockout = false;
                var user = ServiceFactory.UserManager.ValidateUser("", model.UserName, model.Password, out isLockout);
                if (user != null)
                {
                    ServiceFactory.UserManager.UpdateLoginLogout("", user.UserId, true);
                    System.Web.Security.FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    SecurityHelper.TryFinishCustomDbLogin(base.Session, user);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Admin", "Account");
                    }
                    //return RedirectToLocal(returnUrl);
                }
                else
                {
                    var errors = new List<ValidationError>();
                    if (errors.Count == 0)
                    {
                        errors.Add(new ValidationError("userName", ValidationErrorType.CompareNotMatch, "error"));
                    }
                    if (errors.Count > 0)
                    {
                        throw new ValidationException(errors);
                    }
                }
            }
            catch (ValidationException ex)
            {
                AddErrors(ModelState, ex);
                ViewBag.ReturnUrl = returnUrl;
                //throw;
            }
            return View("LoginError");
        }
        protected virtual void ValidateLogOn(string userName, string password)
        {
            var errors = new List<ValidationError>();
            if (String.IsNullOrEmpty(userName))
            {
                errors.Add(new ValidationError("userName", ValidationErrorType.NullOrEmpty, ""));
            }
            if (String.IsNullOrEmpty(password))
            {
                errors.Add(new ValidationError("password", ValidationErrorType.NullOrEmpty, ""));
            }
            //var a = MembershipService.ValidateUser(userName, password);
            //if (errors.Count == 0 && !MembershipService.ValidateUser(userName, password))
            //{
            //	errors.Add(new ValidationError("userName", ValidationErrorType.CompareNotMatch));
            //}

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
        }
        protected void AddErrors(ModelStateDictionary modelState, ValidationException ex)
        {
            foreach (ValidationError error in ex.ValidationErrors)
            {
                modelState.AddModelError(error.FieldName, error);
            }
        }
    }
}