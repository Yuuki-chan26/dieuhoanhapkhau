using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Models
{
    public class ProducerBase : EntityBase
    {
        [DataColum]
        public int ProducerId { get; set; }

        
        [DataColum]
        public string ProducerName { get; set; }
        private ProducerBase _parent;
        public ProducerBase Parent
        {
            get
            {
                return _parent ?? (_parent = new ProducerBase()
                {
                    ProducerId = ProducerId
                });
            }
            set { _parent = value; }
        }

        public List<ProducerBase> Children { get; set; }

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
                    return string.Format("{0}{1}", l, ProducerName);

                }

                return ProducerName;
            }
        }
    }
}
