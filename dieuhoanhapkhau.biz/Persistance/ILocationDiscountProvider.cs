using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface ILocationDiscountProvider : IDataProvider<LocationDiscount>
    {
        void Add(LocationDiscount model, string culture);
        List<LocationDiscount> Search(int startIndex, int lenght, ref int totalItem, string culture);
		List<LocationDiscount> GetAllActive(string culture);
    }
}
