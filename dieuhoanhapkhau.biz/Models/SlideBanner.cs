using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public class SlideBanner : EntityBase
    {
        [DataColum]
        public int SlideBannerId { get; set; }
        
        [DataColum]
        public string Image { get; set; }

        [DataColum]
        public string Url { get; set; }

        [DataColum]
        public bool IsActive { get; set; }

        [DataColum]
        public DateTime CreateDate { get; set; }

        [DataColum]
        public DateTime UpdateDate { get; set; }

        [DataColum]
        public int OrderNo { get; set; }

        [DataColum]
        public string CreateBy { get; set; }

        [DataColum]
        public string SlideTitle { get; set; }

    }
}
