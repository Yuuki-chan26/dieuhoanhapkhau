using idocNet.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using dieuhoanhapkhau.web.State;
using dieuhoanhapkhau.web.Security;
using System.IO;

namespace dieuhoanhapkhau.web.Controllers
{
    public class ControllerBase : Controller
    {

        public string Culture
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.ToString();
            }
        }
        public string Now

        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        public string Today
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd"); }
        }

        public string GetRandomId(string pre, int length = 8)
        {
            string ran = Guid.NewGuid().ToString("N").Substring(0, length).ToUpper();
            return string.Format("{0}{1}", pre, ran);
        }


        //public string renderrazorviewtostring(string viewname, object model)
        //{
        //    viewdata.model = model;
        //    using (var sw = new stringwriter())
        //    {
        //        var viewresult = viewengines.engines.findpartialview(controllercontext,viewname);
        //        var viewcontext = new viewcontext(controllercontext, viewresult.view,
        //                                     viewdata, tempdata, sw);
        //        viewresult.view.render(viewcontext, sw);
        //        viewresult.viewengine.releaseview(controllercontext, viewresult.view);
        //        return sw.getstringbuilder().tostring();
        //    }


        //}

        //public string RenderRazorViewToString(string viewName, string masterName, object model)
        //{
        //    ViewData.Model = model;
        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindView(ControllerContext,
        //                                                                 viewName, masterName);
        //        var viewContext = new ViewContext(ControllerContext, viewResult.View,
        //                                     ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);
        //        viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
        //        return sw.GetStringBuilder().ToString();
        //    }


        //}

        //public ActionResult JsonView(string viewname, string masterName, object model)
        //{


        //    if (string.IsNullOrEmpty(viewname))
        //        viewname = (string)ControllerContext.RouteData.Values["action"];
        //    string html = "";

        //    try
        //    {
        //        html = RenderRazorViewToString(viewname, masterName, model);
        //        var data = new
        //        {
        //            Success = true,
        //            Html = html
        //        };
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }

        //    catch (Exception e)
        //    {
        //        var data = new
        //        {
        //            Success = false,
        //            Message = e.Message,
        //            Detail = e.StackTrace
        //        };
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }


        //}

        //public ActionResult JsonView(string viewname, object model)
        //{


        //    if (string.IsNullOrEmpty(viewname))
        //        viewname = (string)ControllerContext.RouteData.Values["action"];
        //    string html = "";

        //    try
        //    {
        //        html = RenderRazorViewToString(viewname, model);
        //        var data = new
        //        {
        //            Success = true,
        //            Html = html
        //        };
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }

        //    catch (Exception e)
        //    {
        //        var data = new
        //        {
        //            Success = false,
        //            Message = e.Message,
        //            Detail = e.StackTrace
        //        };
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }


        //}

        //public ActionResult JsonView(object model = null)
        //{
        //    return JsonView("", model);
        //}

        #region State management
        private SessionWrapper _session;
        public new SessionWrapper Session
        {
            get
            {
                if (_session == null)
                {
                    _session = new SessionWrapper(ControllerContext.HttpContext.Session);
                }
                return _session;
            }
            set
            {
                _session = value;
            }
        }

        private CacheWrapper _cache;
        public CacheWrapper Cache
        {
            get
            {
                if (_cache == null)
                {
                    _cache = new CacheWrapper(this.ControllerContext.HttpContext.Cache);
                }
                return _cache;
            }
            set
            {
                _cache = value;
            }
        }

        /// <summary>
        /// Gets the current user in session
        /// </summary>
        public new UserState CurrentUser
        {
            get
            {
                return Session.CurrentUser;
            }
        }
        public SiteConfiguration Config
        {
            get
            {
                return SiteConfiguration.Current;
            }
        }
        ///// <summary>
        ///// Gets the role of user making the request
        ///// </summary>
        //public UserRole? Role
        //{
        //	get
        //	{
        //		UserRole? role = null;
        //		if (HttpContext != null && Session.CurrentUser != null)
        //		{
        //			role = Session.CurrentUser.Role;
        //		}
        //		return role;
        //	}
        //}
        #endregion

        #region Init
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            this.Init();
        }

        protected virtual void Init()
        {
            if (Session.CurrentUser == null)
            {
                SecurityHelper.TryLoginFromProviders(Session, Cache, HttpContext);
            }

        }

        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);


            string culture = (string)RouteData.Values["CultureName"];


            if (string.IsNullOrEmpty(culture))
            {
                culture = (string)RouteData.Values["Culture"];


                var cList = idocNet.Core.Configuration.SiteConfiguration.Current.AcceptedCultures;


                foreach (var c in cList)
                {
                    if (c.TwoLetterISOLanguageName.Equals(culture, StringComparison.InvariantCultureIgnoreCase))
                    {
                        culture = c.Name;
                        break;
                    }
                }

            }

            if (string.IsNullOrEmpty(culture))
            {



                culture = SiteConfiguration.Current.DefaultCultureName;
            }

            CultureInfo ci = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }
        #region[""]

        public virtual string GetDefaultMasterName(string v)
        {
            return null;
            //string masterName = "Site";
            //return masterName;
        }
        #endregion
        public string GenExcelExportFilePath(string prefix, ref string virualPath)
        {

            String subpath = string.Format("/TempFiles/{0}", DateTime.Now.ToString("yyyy-MM-dd"));

            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));



            string subpathPhys = Server.MapPath(subpath);

            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }


            virualPath = string.Format("{0}/{1}.xlsx", subpath, filename);
            string filePath = Server.MapPath(virualPath);


            int i = 0;
            while (System.IO.File.Exists(filePath))
            {

                virualPath = string.Format("{0}/{1}_{2}.xlsx", subpath, filename, ++i);
                filePath = Server.MapPath(virualPath);
            }


            return filePath;
        }
        public virtual string GetDefaultMasterName()
        {
            return null;
            //string masterName = "Site";
            //return masterName;
        }
    }
}