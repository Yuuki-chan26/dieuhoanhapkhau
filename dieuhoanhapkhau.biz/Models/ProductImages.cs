using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Models
{
    public class ProductImages : EntityBase
    {
        [DataColum]
        public int ProductImageId { get; set; }

        [DataColum]
        public int ProductId { get; set; }

        [DataColum]
        public string ProductImage { get; set; }

        [DataColum]
        public string Url { get; set; }

        [DataColum]
        public DateTime CreateDate { get; set; }

        [DataColum]
        public DateTime UpdateDate { get; set; }

        [DataColum]
        public string CreateBy { get; set; }

        [DataColum]
        public bool IsActive { get; set; }

        [DataColum]
        public int OrderNo { get; set; }

        [DataColum]
        public string Culture { get; set; }

        
    }
}
