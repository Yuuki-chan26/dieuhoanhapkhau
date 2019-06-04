namespace dieuhoanhapkhau.web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [StringLength(3)]
        public string UseriD { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(500)]
        public string Adress { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public bool? Status { get; set; }
    }
}
