using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Services
{
    public class LocationManager : DataManagerBase<Location>
    {
        public LocationManager()
            : base()
        { }

        public LocationManager(IDataProvider<Location> provider)
            : base(provider)
        {
        }

        private ILocationProvider LocationProvider
        {
            get { return (ILocationProvider)Provider; }
        }
        public List<Location> GetAllActive(string culture)
        {
            return LocationProvider.GetAllActive(culture);
        }
        public List<Location> GetAllActiveByParentId(int id)
        {
            return LocationProvider.GetAllActiveParentId(id);
        }
        public void Add(Location model, string Culture)
        {
            LocationProvider.Add(model, Culture);
        }
    }
}
