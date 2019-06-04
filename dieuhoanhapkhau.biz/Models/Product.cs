using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public class Product : EntityBase
    {
        [DataColum]
        public int ProductId { get; set; }

        [DataColum]
        public int PrdCategoryId { get; set; }

        [DataColum]
        public string ProductName { get; set; }

		[DataColum]
		public string ProductTitle { get; set; }

        [DataColum]
        public string ProductCode { get; set; }

        [DataColum]
        public string ProductImage { get; set; }

        [DataColum]
        public int ProducerPropertyId { get; set; }

        [DataColum]
        public decimal ProductPrice { get; set; }

        [DataColum]
        public string ProductSlogan { get; set; }

        [DataColum]
        public string ProductDescription { get; set; }

        [DataColum]
        public string ProductKeyword { get; set; }

        [DataColum]
        public string ProductSummary { get; set; }

        [DataColum]
        public DateTime CreateDate { get; set; }

        [DataColum]
        public string CreateBy { get; set; }

        [DataColum]
        public DateTime UpdateDate { get; set; }

        [DataColum]
        public string UpdateBy { get; set; }

        [DataColum]
        public string Culture { get; set; }

        [DataColum]
        public bool IsActive { get; set; }

        [DataColum]
        public int OrderNo { get; set; }

        [DataColum]
        public bool IsHotProduct { get; set; }

        [DataColum]
        public bool IsNewProduct { get; set; }

        [DataColum]
        public bool IsSaleProduct { get; set; }

        [DataColum]
        public string PrdCategoryTitle { get; set; }

        [DataColum]
        public int Quality { get; set; }

        [DataColum]
        public int ProducerId { get; set; }

        [DataColum]
        public int ProductPropertyId { get; set; }

        public List<ProductProperty> Properties
        {
            get;
            set;
        }
        public List<Product> ListProduc { get; set; }
        public List<Producers> Producers
        {
            get;
            set;
        }
        public List<ProductImages> ProductImages
        {
            get;
            set;
        }
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
                    return string.Format("{0}{1}", l, ProductTitle);

                }

                return ProductTitle;
            }
        }

    }
}
