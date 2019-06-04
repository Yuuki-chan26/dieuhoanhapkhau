using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace dieuhoanhapkhau.biz.Models
{
    public class ProductCategory : ProductCategoryBase
    {
        public ProductCategory()
            : base()
        {

        }
        public ProductCategory(int id)
            : this()
        {

            this.PrdCategoryId = id;
        }
        public ProductCategory(string name)
            : this()
        {

            this.PrdCategoryTitle = name;
        }

        [DataColum]
        public string PrdCategoryShortName { get; set; }

        [DataColum]
        public string PrdCategorySummary { get; set; }

        [DataColum]
        public string PrdCategoryDescription { get; set; }

        [DataColum]
        public string PrdCategoryKeyword { get; set; }

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
        public int ProducerId { get; set; }

        public List<Product> ListProducts { get; set; }
        
        public string ProductCategoryParentName { get; set; }
    }
}
