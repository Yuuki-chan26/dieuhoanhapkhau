using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IProductCategoryProvider : IDataProvider<ProductCategory>
    {
        List<ProductCategory> Search(int startIndex, int lenght, ref int totalItem, string culture);
        List<ProductCategoryBase> GetAllProductCategory(string culture);
        List<ProductCategory> ListAllProductCategory(string culture);
        void Add(ProductCategory model, string culture);
        ProductCategory GetByShortName(ProductCategory model, string culture);
        List<ProductCategory> GetAllActiveByParentId(int parentid, string culture);
        List<ProductCategoryBase> GetAllProductProducer(string culture);
        List<ProductCategory> GetByParentId(int id);
    }
}
