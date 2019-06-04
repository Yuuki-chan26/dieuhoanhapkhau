using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IImageProductProvider : IDataProvider<ProductImages>
    {
        List<ProductImages> GetAllActive(int productid, string culture);
        List<ProductImages> GetByPrdId(int productid, string culture);
        List<ProductImages> Search(int startIndex, int lenght, ref int totalItem, string culture);
        void Add(ProductImages model, string culture);
        List<ProductImages> GetListImagesByCateId(int cateid, int startIndex, int lenght, ref int totalItem, string culture);
    }
}
