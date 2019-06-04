using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Models
{
    public class Orders : EntityBase
    {
        [DataColum]
        public int OrderId { get; set; }
        [DataColum]
        public int CustomerId { get; set; }
        [DataColum]
        public DateTime CreateDate { get; set; }
        [DataColum]
        public DateTime DeliveryDate { get; set; }
        [DataColum]
        public string Note { get; set; }
        [DataColum]
        public int IsActive { get; set; }
        [DataColum]
        public double ToTalMoney { get; set; }

        [DataColum]
        public string CustomerName { get; set; }
    }
}
