﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TCG.QLTV.Web.Models
{
    public class FieldError
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class JsonResultEntry
    {
        public JsonResultEntry()
        {
            this.Success = true;
            Messages = new string[0];
            FieldErrors = new FieldError[0];
        }

        public JsonResultEntry(ModelStateDictionary modelState)
            : this()
        {
            this.AddModelState(modelState);
        }

        public JsonResultEntry SetFailed()
        {
            this.Success = false;
            return this;
        }

        public JsonResultEntry SetSuccess()
        {
            this.Success = true;
            return this;
        }

        public JsonResultEntry AddModelState(ModelStateDictionary modelState)
        {
            foreach (var ms in modelState)
            {
                foreach (var err in ms.Value.Errors)
                {
                    this.AddFieldError(ms.Key, err.ErrorMessage);
                }
            }

            return this;
        }

        public bool Success { get; set; }
        public string[] Messages { get; set; }
        public object Model { get; set; }
        public string RedirectUrl { get; set; }
        private bool redirectToOpener = false;
        public bool RedirectToOpener { get { return redirectToOpener; } set { redirectToOpener = value; } }
        public FieldError[] FieldErrors { get; set; }
        public bool IsServiceException { get; set; }
        public string Detail { get; set; }


        public JsonResultEntry AddFieldError(string fieldName, string message)
        {
            Success = false;
            FieldErrors = FieldErrors.Concat(new[] { new FieldError() { FieldName = fieldName, ErrorMessage = message } }).ToArray();
            return this;
        }
        public JsonResultEntry AddMessage(string message)
        {
            Messages = Messages.Concat(new[] { message }).ToArray();
            return this;
        }
    }
}