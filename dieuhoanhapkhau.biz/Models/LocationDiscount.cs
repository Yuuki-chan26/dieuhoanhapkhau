using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public class LocationDiscount:EntityBase
    {
        [DataColum]
        public int LocationDiscountId { get; set; }

        [DataColum]
        public string LocationDiscountName { get; set; }

        [DataColum]
        public int LocationDiscountValue { get; set; }

        [DataColum]
        public decimal LocationValue { get; set; }


        [DataColum]
        public bool IsActive { get; set; }

        [DataColum]
        public DateTime CreateDate { get; set; }

        [DataColum]
        public int OrderNo { get; set; }

        [DataColum]
        public string CreateBy { get; set; }

        public List<Location> Locations
        {
            get;
            set;
        }
    }
}
