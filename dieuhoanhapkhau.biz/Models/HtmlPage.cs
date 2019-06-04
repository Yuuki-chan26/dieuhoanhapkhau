using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public class HtmlPage:EntityBase
    {
        [DataColum]
        public int HtmlPageId { get; set; }

        [DataColum]
        public int HtmlPageCategoryId { get; set; }

        [DataColum]
        public string HtmlPageBody { get; set; }

        [Length(250)]
        [DataColum]
        [RequireField]
        public string HtmlPageTitle { get; set; }

        [DataColum]
        public string HtmlPageShortName { get; set; }

        [DataColum]
        public string HtmlPageDescription { get; set; }

        [DataColum]
        public string HtmlPageKeyword { get; set; }

        [DataColum]
        public bool IsActive { get; set; }

        [DataColum]
        public DateTime CreateDate { get; set; }

        [DataColum]
        public int OrderNo { get; set; }

        [DataColum]
        public bool IsHotNews { get; set; }


        [DataColum]
        public string HtmlPageOther { get; set; }

        [DataColum]
        public string CreateBy { get; set; }

        [DataColum]
        public string HtmlPageImage { get; set; }

        [DataColum]
        public string HtmlPageCategoryTitle { get; set; }


    }
}
