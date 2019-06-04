using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public class HtmlPageCategoryBase : EntityBase
    {
        [DataColum]
        public int HtmlPageCategoryId { get; set; }

        [DataColum]
        public int ParentId { get; set; }

        [DataColum]
        public string HtmlPageCategoryTitle { get; set; }

        private HtmlPageCategoryBase _parent;
        public HtmlPageCategoryBase Parent
        {
            get
            {
                return _parent ?? (_parent = new HtmlPageCategoryBase()
                {
                    HtmlPageCategoryId = ParentId
                });
            }
            set { _parent = value; }
        }

        public List<HtmlPageCategoryBase> Children { get; set; }

        public int HLevel
        {
            get;
            set;
        }

        public string HlevelTitle
        {
            get
            {
                if (HLevel > 0)
                {
                    var l = "";
                    for (var i = 1; i <= HLevel; ++i)
                    {
                        l += "|--";
                    }
                    return string.Format("{0}{1}", l, HtmlPageCategoryTitle);

                }

                return HtmlPageCategoryTitle;
            }
        }
    }
}
