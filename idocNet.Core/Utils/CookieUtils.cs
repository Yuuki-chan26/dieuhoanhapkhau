using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace idocNet.Core.Utils
{
    public class CookieUtils
    {
        public static string CookieName = "ck";
        public static string GetCookie(string key)
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(CookieName);

            if (cookie == null) return string.Empty;
            return cookie.Values[key];
        }


        public static void SetCookie(string key, string value)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            myCookie.Values.Add(key, value);

            HttpContext.Current.Response.Cookies.Add(myCookie);
        }




        public static string GetCookie(string name, string key)
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(name);

            if (cookie == null) return string.Empty;
            return cookie.Values[key];
        }


        public static void SetCookie(string name, string key, string value)
        {
            HttpCookie myCookie = new HttpCookie(name);
            myCookie.Values.Add(key, value);

            HttpContext.Current.Response.Cookies.Add(myCookie);
        }



        public static void SetCookie(string name, string key, string value, DateTime expireDate)
        {
            HttpCookie myCookie = new HttpCookie(name);
            myCookie.Values.Add(key, value);
            myCookie.Expires = expireDate;

            HttpContext.Current.Response.Cookies.Add(myCookie);
        }


        public static void SetCookie(HttpCookie ck)
        {
            HttpContext.Current.Response.Cookies.Add(ck);
        }

        public static HttpCookie GetCookies(string name)
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(name);

            return cookie;
        }
    }
}
