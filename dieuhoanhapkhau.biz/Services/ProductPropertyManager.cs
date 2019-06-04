using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Services
{
	public class ProductPropertyManager : DataManagerBase<ProductProperty>
	{
		public ProductPropertyManager()
			: base()
		{ }

		public ProductPropertyManager(IDataProvider<ProductProperty> provider)
			: base(provider)
		{
		}

		private IProductPropertyProvider ProductPropertyProvider
		{
			get { return (IProductPropertyProvider)Provider; }
		}

        public List<ProductProperty> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return ProductPropertyProvider.Search(startIndex, lenght, ref totalItem, culture);
        }
        public List<ProductProperty> GetByPrdId(int productid, string culture)
		{
			return ProductPropertyProvider.GetByPrdId(productid, culture);
		}
        
        public ProductProperty GetByPrdTitle(ProductProperty model, string culture)
        {
            return ProductPropertyProvider.GetByPrdTitle(model, culture);
        }
        public List<ProductProperty> GetAllActive(string culture)
        {
            return ProductPropertyProvider.GetAllActive(culture);
        }
		public void Add(ProductProperty model, string Culture)
		{
			ProductPropertyProvider.Add(model, Culture);
		}
        public List<ProductProperty> GetByCateId(int id)
        {
            return ProductPropertyProvider.GetByCateId(id);
        }
       
        public List<ProductProperty> GetByProducer(int id, int parentid)
        {
            return ProductPropertyProvider.GetByProducer(id, parentid);
        }
        public ProductProperty GetByShortName(string shortname, string culture)
        {
            return ProductPropertyProvider.GetByShortName(shortname, culture);
        }
        public List<ProductProperty> GetAllProperty(string culture)
        {
            return ProductPropertyProvider.GetAllProperty(culture);
        }
    }
}
