using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Models
{
    public class Producers : EntityBase
    {
        [DataColum]
        public int ProducerId { get; set; }

        [DataColum]
        public string ProducerName { get; set; }

        [DataColum]
        public string CreateBy { get; set; }

        [DataColum]
        public bool IsActive { get; set; }

        [DataColum]
        public int OrderNo { get; set; }

        [DataColum]
        public string Culture { get; set; }
        
        public List<ProducerProperties> Producer
        {
            get;
            set;
        }
    }
}
