using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Models
{
    public class OrderDetail : EntityBase
    {
        [DataColum]
        public int OrderId { get; set; }
        [DataColum]
        public int ProductId { get; set; }
        [DataColum]
        public int Quality { get; set; }
        [DataColum]
        public string Note { get; set; }
        [DataColum]
        public decimal Price { get; set; }
        [DataColum]
        public double Total { get; set; }
        [DataColum]
        public string ProductName { get; set; }
    }
}
