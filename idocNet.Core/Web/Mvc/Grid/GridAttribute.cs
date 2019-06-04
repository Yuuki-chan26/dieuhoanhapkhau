﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Web.Mvc.Grid
{
    public class GridAttribute : Attribute
    {
        public bool Checkable { get; set; }
        public string IdProperty { get; set; }
        public string ItemDetailView { get; set; }
        public Type CheckVisible { get; set; }
    }
}
