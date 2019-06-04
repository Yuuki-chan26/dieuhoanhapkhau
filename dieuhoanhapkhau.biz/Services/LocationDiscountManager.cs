using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Services
{
    public class LocationDiscountManager : DataManagerBase<LocationDiscount>
    {
        public LocationDiscountManager()
            : base()
        { }

        public LocationDiscountManager(IDataProvider<LocationDiscount> provider)
            : base(provider)
        {
        }

        private ILocationDiscountProvider LocationDiscountProvider
        {
            get { return (ILocationDiscountProvider)Provider; }
        }


        public List<LocationDiscount> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return LocationDiscountProvider.Search(startIndex, lenght, ref totalItem, culture);
        }
		public List<LocationDiscount> GetAllActive(string culture)
		{
			return LocationDiscountProvider.GetAllActive(culture);
		}
        public void Add(LocationDiscount model, string culture)
        {
            LocationDiscountProvider.Add(model, culture);
        }
    }
}
