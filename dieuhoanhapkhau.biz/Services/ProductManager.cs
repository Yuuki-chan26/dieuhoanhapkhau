using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace dieuhoanhapkhau.biz.Services
{
    public class ProductManager : DataManagerBase<Product>
    {
        public ProductManager()
            : base()
        { }

        public ProductManager(IDataProvider<Product> provider)
            : base(provider)
        {
        }

        private IProductProvider ProductProvider
        {
            get { return (IProductProvider)Provider; }
        }


        public List<Product> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return ProductProvider.Search(startIndex, lenght, ref totalItem, culture);
        }
        public List<Product> ProductGetByCateId(int cateid, string culture)
        {
            return ProductProvider.ProductGetByCateId(cateid, culture);
        }
        public List<Product> ProductGetByPrpducerPropertyId(int producerid, int producerpropertyid, int productid)
        {
            return ProductProvider.ProductGetByPrpducerPropertyId(producerid,producerpropertyid, productid);
        }
        public List<Product> ProductGetByProducer(int id, int parentid)
        {
            return ProductProvider.ProductGetByProducer(id, parentid);
        }
        public List<Product> ProductGetAllActive(string culture)
        {
            return ProductProvider.ProductGetAllActive( culture);
        }

        public List<Product> ProductGetAllRegister(string culture)
        {
            return ProductProvider.ProductGetAllRegister(culture);
        }

        public void Add(Product model, string Culture)
        {
            ProductProvider.Add(model, Culture);
        }
        public List<Product> GetListHotProduct(string culture)
        {
            return ProductProvider.GetListHotProduct(culture);
        }

        public List<Product> GetListNewProduct(string culture)
        {
            return ProductProvider.GetListNewProduct(culture);
        }

        public List<Product> GetListSaleProduct(string culture)
        {
            return ProductProvider.GetListSaleProduct(culture);
        }

        public List<Product> GetListHotNewSaleProduct(string culture)
        {
            return ProductProvider.GetListHotNewSaleProduct(culture);
        }
        public Product GetByCode(Product model,string culture)
        {
            return ProductProvider.GetByCode(model,culture);
        }
        public List<Product> Searchkey(int startIndex, int length, ref int total, string key)
        {
            return ProductProvider.Searchkey(startIndex, length, ref total, key);
        }
        public List<Product> GetByPropertyShortName(string culture, string shortname, int startIndex, int length, ref int total)
        {
            return ProductProvider.GetByPropertyShortName(shortname, culture, startIndex, length, ref total);
        }
    }
}
