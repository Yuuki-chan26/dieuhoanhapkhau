namespace dieuhoanhapkhau.web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TypeNewId_table
    {
        [Key]
        [StringLength(3)]
        public string TypeNewsId { get; set; }

        [Required]
        [StringLength(3)]
        public string NewsId { get; set; }

        [StringLength(500)]
        public string TitleNews { get; set; }

        [StringLength(100)]
        public string ShortName { get; set; }

        public string Content { get; set; }

        public int? IsHost { get; set; }

        //public virtual News News { get; set; }
    }
}
