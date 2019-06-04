using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public class Location:EntityBase
    {
        [DataColum]
        public int LocationId{ get; set; }

        [DataColum]
        public int LocationDiscountId { get; set; }

        [DataColum]
        public string LocationName { get; set; }

       [DataColum]
        public bool IsActive { get; set; }

        [DataColum]
        public DateTime CreateDate { get; set; }

        

        [DataColum]
        public string CreateBy { get; set; }
    }
}
