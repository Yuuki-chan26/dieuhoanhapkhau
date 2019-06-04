﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace idocNet.Core.Web.Mvc.Grid
{

    public class GridItemAction
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string DisplayName { get; set; }
        public string ConfirmMessage { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public bool IsDelete { get; set; }
        private bool visible = true;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
        public RouteValueDictionary RouteValues { get; set; }
    }
}
