using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.web.Helpers;
using dieuhoanhapkhau.web.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace dieuhoanhapkhau.web.Filters
{
    public class RequireAuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Required minimal User role
        /// </summary>
        public UserRole[] UserRoles
        {
            get;
            set;
        }

        public RouteCollection Routes
        {
            get;
            set;
        }

        public RequireAuthorizationAttribute()
        {
            this.Routes = RouteTable.Routes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRole">Required minimal role to access</param>
        public RequireAuthorizationAttribute(params UserRole[] userRole)
            : this()
        {
            this.UserRoles = userRole;
        }

        /// <summary>
        /// Determines if the filter must return forbidden status in the case the user is not logged in
        /// </summary>
        public bool RefuseOnFail
        {
            get;
            set;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = new SessionWrapper(httpContext.Session);
            return IsAuthorized(session.CurrentUser);
        }

        /// <summary>
        /// Determines if a user is authorized
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected virtual bool IsAuthorized(UserState user)
        {
            if (user == null)
            {
                return false;
            }
            if (this.UserRoles.Count() != 0)
            {
                foreach (UserRole ur in UserRoles)
                {
                    if (user.Role == UserRole.Admin)
                    {
                        return true;
                    }
                    if (user.Role == ur)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Handles the request when the user is not authorized
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (RefuseOnFail)
            {
                filterContext.Result = ResultHelper.ForbiddenResult(filterContext.Controller as dieuhoanhapkhau.web.Controllers.ControllerBase);
            }
            else
            {
                string redirectOnSuccess = filterContext.HttpContext.Request.Url.PathAndQuery;
                VirtualPathData path = this.Routes.GetVirtualPath(filterContext.RequestContext, new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login",
                    returnUrl = redirectOnSuccess,
                    role = this.UserRoles
                }));
                if (path == null)
                {
                    throw new ArgumentException("Route for Account>Login not found.");
                }
                string loginUrl = path.VirtualPath;
                filterContext.Result = new RedirectResult(loginUrl, false);
            }
        }
    }
}