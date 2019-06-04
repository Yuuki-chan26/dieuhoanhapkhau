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
    public class LocationDiscountProvider : DataAccessBase, ILocationDiscountProvider
    {
        public void Add(LocationDiscount item, string culture)
        {
            var comm = this.GetCommand("sp_LocationDiscount_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "LocationDiscountName", item.LocationDiscountName);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<decimal>(this.Factory, "LocationValue", item.LocationValue);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<int>(this.Factory, "LocationDiscountValue", item.LocationDiscountValue);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.SafeExecuteNonQuery();
        }

        public void Add(LocationDiscount item)
        {
            throw new NotImplementedException();
        }

        public LocationDiscount Get(LocationDiscount dummy)
        {
            var comm = this.GetCommand("sp_LocationDiscountsGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "LocationDiscountId", dummy.LocationDiscountId);
            var dt = this.GetTable(comm);
            var sliderBanner = EntityBase.ParseListFromTable<LocationDiscount>(dt).FirstOrDefault();
            return sliderBanner ?? null;
        }

        public List<LocationDiscount> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<LocationDiscount> GetAll(int v, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public List<LocationDiscount> GetAllActive(string culture)
        {
            var comm = this.GetCommand("sp_LocationDiscountsGetAllActive");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<LocationDiscount>(dt);
        }

        public void Remove(LocationDiscount item)
        {
            var comm = this.GetCommand("sp_LocationDiscount_Delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "LocationDiscountId", item.LocationDiscountId);
            comm.SafeExecuteNonQuery();
        }

        public List<LocationDiscount> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_LocationDiscountsSearch");
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
            return EntityBase.ParseListFromTable<LocationDiscount>(dt);
        }

        public void Update(LocationDiscount @new, LocationDiscount old)
        {
            var item = @new;
            item.LocationDiscountId = old.LocationDiscountId;
            var comm = this.GetCommand("sp_LocationDiscount_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "LocationDiscountId", item.LocationDiscountId);
            comm.AddParameter<string>(this.Factory, "LocationDiscountName", item.LocationDiscountName);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<int>(this.Factory, "LocationDiscountValue", item.LocationDiscountValue);
            comm.AddParameter<decimal>(this.Factory, "LocationValue", item.LocationValue);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.SafeExecuteNonQuery();
        }
    }
}
