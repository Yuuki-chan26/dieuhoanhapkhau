using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Models
{
    public class ProducerProperties : EntityBase
    {
        [DataColum]
        public int ProducerPropertyId { get; set; }

        [DataColum]
        public int ProducerId { get; set; }

        [DataColum]
        public string CreateBy { get; set; }

        [DataColum]
        public DateTime CreateDate { get; set; }

        [DataColum]
        public int OrderNo { get; set; }

        [DataColum]
        public bool IsActive { get; set; }

        [DataColum]
        public string PropertyName { get; set; }

        [DataColum]
        public string ShortName { get; set; }

        public List<Product> ListProduct { get; set; }
    }
}
