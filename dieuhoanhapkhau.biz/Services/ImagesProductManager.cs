using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Services
{
    public class ImagesProductManager : DataManagerBase<ProductImages>
    {
        public ImagesProductManager()
            : base()
        { }

        public ImagesProductManager(IDataProvider<ProductImages> provider)
            : base(provider)
        {
        }

        private IImageProductProvider VideoProvider
        {
            get { return (IImageProductProvider)Provider; }
        }
        public List<ProductImages> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return VideoProvider.Search(startIndex, lenght, ref totalItem, culture);
        }

        public void Add(ProductImages model, string Culture)
        {
            VideoProvider.Add(model, Culture);
        }
        public List<ProductImages> GetListImagesByCateId(int catenewsid, int startIndex, int lenght, ref int totalItem, string culture)
        {
            return VideoProvider.GetListImagesByCateId(catenewsid, startIndex, lenght, ref totalItem, culture);
        }
    }
}
