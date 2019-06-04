using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dieuhoanhapkhau.web.Utils
{
    public class JsonSuccess : JsonResult
    {
        public JsonSuccess(string message)
        {
            Data = new
            {
                success = true,
                message = message
            };
        }
    }

    public class JsonError : JsonResult
    {
        public JsonError(string message, string log = "")
            : base()
        {
            Data = new
            {
                success = false,
                message = message,
                log = log
            };
        }
    }
}