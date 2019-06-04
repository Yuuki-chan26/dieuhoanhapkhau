using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class LocationProvider : DataAccessBase, ILocationProvider
    {
       

        public Location Get(Location dummy)
        {
            var comm = this.GetCommand("sp_LocationsGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "LocationId", dummy.LocationId);
            var dt = this.GetTable(comm);
            var sliderBanner = EntityBase.ParseListFromTable<Location>(dt).FirstOrDefault();
            return sliderBanner ?? null;
        }

        public List<Location> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetAll(int v, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetAllActive(string culture)
        {
            var comm = this.GetCommand("sp_LocationsGetAllActive");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Location>(dt);
        }

        public void Remove(Location item)
        {
            throw new NotImplementedException();
        }

        public void Update(Location @new, Location old)
        {
            var item = @new;
            item.LocationId = old.LocationId;
            var comm = this.GetCommand("sp_Location_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "LocationId", item.LocationId);
            comm.AddParameter<string>(this.Factory, "LocationName", item.LocationName);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.SafeExecuteNonQuery();
        }
        public List<Location> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_LocationsSearch");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<int>(this.Factory, "Length", lenght);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var totalItemsParam = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItemsParam.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItemsParam.Value != DBNull.Value)
            {
                totalItem = Convert.ToInt32(totalItemsParam.Value);
            }
            return EntityBase.ParseListFromTable<Location>(dt);
        }

        public List<Location> GetAllActiveParentId(int id)
        {
            var comm = this.GetCommand("Location_GetAllActiveByParentId");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "LocationDiscountId", id);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Location>(dt);
        }

        public void Add(Location item)
        {
            throw new NotImplementedException();
        }

        public void Add(Location item, string culture)
        {
            var comm = this.GetCommand("sp_Location_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "LocationName", item.LocationName);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<int>(this.Factory, "LocationDiscountId", item.LocationDiscountId);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            
            comm.SafeExecuteNonQuery();
        }
    }
}
