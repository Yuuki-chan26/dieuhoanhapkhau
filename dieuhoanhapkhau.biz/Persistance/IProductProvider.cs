using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IProductProvider : IDataProvider<Product>
    {
        List<Product> Search(int startIndex, int lenght, ref int totalItem, string culture);
        List<Product> ProductGetByCateId(int cateid, string culture);
        List<Product> ProductGetByPrpducerPropertyId(int producerid,int producerpropertyid, int productid);
        List<Product> ProductGetByProducer(int id, int parentid);
        void Add(Product model, string culture);
        List<Product> ProductGetAllActive(string culture);
        List<Product> ProductGetAllRegister(string culture);
        List<Product> GetListHotProduct(string culture);
        List<Product> GetListNewProduct(string culture);
        List<Product> GetListSaleProduct(string culture);
        List<Product> GetListHotNewSaleProduct(string culture);
        Product GetByCode(Product model, string culture);
        List<Product> Searchkey(int startIndex, int length, ref int total, string key);
        List<Product> GetByPropertyShortName(string shortname, string culture, int startIndex, int length, ref int total);
    }
}
