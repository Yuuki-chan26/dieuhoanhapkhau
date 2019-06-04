using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public class ProductProperty : EntityBase
    {
        [DataColum]
        public int ProductPropertyId { get; set; }

        [DataColum]
        public int ProductId { get; set; }

        [DataColum]
        public int ProducerId { get; set; }

        [DataColum]
        public int PrdCategoryId { get; set; }

        [DataColum]
        public string ProductPropertyBody { get; set; }

        [Length(250)]
        [DataColum]
        public string ProductPropertyTitle { get; set; }

        [Length(250)]
        [DataColum]
        public string ShortName { get; set; }

        [DataColum]
        public int OrderNo { get; set; }

        [DataColum]
        public bool IsActive { get; set; }

        [Length(25)]
        [DataColum]
        public string ProductCode { get; set; }

        [Length(250)]
        [DataColum]
        public string ProductName { get; set; }

        public List<Product> ListProduct { get; set; }
    }
}
