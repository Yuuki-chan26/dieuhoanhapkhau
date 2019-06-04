using dieuhoanhapkhau.biz.Models;
using idocNet.Core.ComponentModel;
using idocNet.Core.Web.Mvc;
using idocNet.Core.Web.Mvc.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dieuhoanhapkhau.web.Areas.Admin.Models
{
    [MetadataFor(typeof(HtmlPage))]
    [GridAction(ActionName = "Update", RouteValueProperties = "Id=HtmlPageId", Order = 1, Title = "Thay đổi", Class = "o-icon edit")]
    public class HtmlPage_Metadata
    {

        [DisplayName("Mã")]
        public int HtmlPageId { get; set; }

        [GridColumn(Order = 3)]
        [DisplayName("Tiêu đề")]
        [Required]
        public string HtmlPageTitle { get; set; }

        [DisplayName("Tóm tắt")]
        [UIHint("TextArea")]
        public string HtmlPageSummary { get; set; }

        
        [DisplayName("Nội dung")]
        public string HtmlPageBody { get; set; }

        [DisplayName("Ảnh đại diện")]
        [UIHint("File")]
        public string HtmlPageImage { get; set; }

        [GridColumn(Order = 4)]
        [DisplayName("Thứ tự")]
        public int OrderNo { get; set; }

        [GridColumn(Order = 6)]
        [DisplayName("Trạng thái")]
        [UIHint("Boolean")]
        public bool IsActive { get; set; }

    }
   
}