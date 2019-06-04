using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface ISlideBannerProvider : IDataProvider<SlideBanner>
    {
        void Add(SlideBanner model, string culture);
        List<SlideBanner> SelectTop(int topcount, string culture);
        List<SlideBanner> Search(int startIndex, int lenght, ref int totalItem, string culture);

    }
}
