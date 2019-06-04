using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Services
{
    public class ProductCategoryManager : DataManagerBase<ProductCategory>
    {
        public ProductCategoryManager()
            : base()
        { }

        public ProductCategoryManager(IDataProvider<ProductCategory> provider)
            : base(provider)
        {
        }

        private IProductCategoryProvider ProductCategoryProvider
        {
            get { return (IProductCategoryProvider)Provider; }
        }
        public List<ProductCategory> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return ProductCategoryProvider.Search(startIndex, lenght, ref totalItem, culture);
        }
        public ProductCategory GetByShortName(ProductCategory model, string culture)
        {
            return ProductCategoryProvider.GetByShortName(model, culture);
        }

        public List<ProductCategoryBase> GetAllProductCategory(string culture)
        {
            return ProductCategoryProvider.GetAllProductCategory(culture);
        }
        public List<ProductCategoryBase> GetAllProductProducer(string culture)
        {
            return ProductCategoryProvider.GetAllProductProducer(culture);
        }
        public List<ProductCategory> ListAllProductCategory(string culture)
        {
            return ProductCategoryProvider.ListAllProductCategory(culture);
        }
        public void Add(ProductCategory model, string Culture)
        {
            ProductCategoryProvider.Add(model, Culture);
        }
        public List<ProductCategory> GetAllActiveByParentId(int parentid, string culture)
        {
            return ProductCategoryProvider.GetAllActiveByParentId(parentid, culture);
        }
        public List<ProductCategory> GetByParentId(int id)
        {
            return ProductCategoryProvider.GetByParentId(id);
        }
    }
}
