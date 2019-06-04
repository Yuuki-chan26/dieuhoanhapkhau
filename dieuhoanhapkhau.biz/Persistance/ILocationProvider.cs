using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface ILocationProvider : IDataProvider<Location>
    {
        //void Add(LocationDiscount model, string culture);
        //List<LocationDiscount> Search(int startIndex, int lenght, ref int totalItem, string culture);
        List<Location> GetAllActive(string culture);
        List<Location> GetAllActiveParentId(int id);
        void Add(Location model, string culture);
    }
}
