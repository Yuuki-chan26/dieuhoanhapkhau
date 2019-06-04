using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idocNet.Core.Data;
using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;

namespace dieuhoanhapkhau.biz.Services
{
    public class SlideBannerManager: DataManagerBase<SlideBanner>
    {
        public SlideBannerManager()
            : base()
        { }
        public SlideBannerManager(IDataProvider<SlideBanner> provider)
            :base(provider)
        { }
        private ISlideBannerProvider SlideBannerProvider
        {
            get { return (ISlideBannerProvider)Provider; }
        }
        public List<SlideBanner> Search(int startIndex, int lenght, ref int totalItem,string culture)
        {
            return SlideBannerProvider.Search(startIndex, lenght, ref totalItem, culture);
        }
        public void Add(SlideBanner model, string culture)
        {
            SlideBannerProvider.Add(model, culture);
        }
        public List<SlideBanner> SelectTop(int topcount, string culture)
        {
            return SlideBannerProvider.SelectTop(topcount, culture);
        }
    }
}
