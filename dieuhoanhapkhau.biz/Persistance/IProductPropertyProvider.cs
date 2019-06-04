using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance
{
	public interface IProductPropertyProvider : IDataProvider<ProductProperty>
	{
        List<ProductProperty> Search(int startIndex, int lenght, ref int totalItem, string culture);
        List<ProductProperty> GetByPrdId(int productid, string culture);
        List<ProductProperty> GetByProducer(int id, int parentid);
        ProductProperty GetByPrdTitle(ProductProperty model, string culture);
        List<ProductProperty> GetAllActive(string culture);
		void Add(ProductProperty model, string culture);
        List<ProductProperty> GetByCateId(int id);
        ProductProperty GetByShortName(string shortname, string culture);
        List<ProductProperty> GetAllProperty(string culture);
    }
}
