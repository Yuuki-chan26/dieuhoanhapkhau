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
    public class ImageProductManager : DataManagerBase<ProductImages>
    {

        public ImageProductManager()
            : base()
        { }

        public ImageProductManager(IDataProvider<ProductImages> provider)
            : base(provider)
        {
        }

        private IImageProductProvider ImageProductProvider
        {
            get { return (IImageProductProvider)Provider; }
        }
        public List<ProductImages> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return ImageProductProvider.Search(startIndex, lenght, ref totalItem, culture);
        }

        public void Add(ProductImages model, string Culture)
        {
            ImageProductProvider.Add(model, Culture);
        }
        public List<ProductImages> GetListImagesByCateId(int catenewsid, int startIndex, int lenght, ref int totalItem, string culture)
        {
            return ImageProductProvider.GetListImagesByCateId(catenewsid, startIndex, lenght, ref totalItem, culture);
        }
        public List<ProductImages> GetByPrdId(int productid, string culture)
        {
            return ImageProductProvider.GetByPrdId(productid, culture);
        }
        public List<ProductImages> GetAllActive(int productid, string culture)
        {
            return ImageProductProvider.GetAllActive(productid, culture);
        }
    }
}
