namespace dieuhoanhapkhau.web.Areas.Admin.Models
{
    using dieuhoanhapkhau.biz.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TypeNew
    {
        [Key]
        [StringLength(3)]
        public string TypeNewsId { get; set; }

        [StringLength(100)]
        public string TitleNews { get; set; }

        [StringLength(100)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(3)]
        public string NewsId { get; set; }

        //public virtual News News { get; set; }
    }
}
