using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class SlideBanneProvider : DataAccessBase, ISlideBannerProvider
    {
        public SlideBanner Get(SlideBanner dummy)
        {
            var comm = this.GetCommand("sp_SlideBannerGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "SlideBannerId", dummy.SlideBannerId);
            var dt = this.GetTable(comm);
            var sliderBanner = EntityBase.ParseListFromTable<SlideBanner>(dt).FirstOrDefault();
            return sliderBanner ?? null;
            //throw new NotImplementedException();
        }

        public void Add(SlideBanner item)
        {
            
            throw new NotImplementedException();
        }

        public void Add(SlideBanner item, string culture)
        {
            var comm = this.GetCommand("sp_SlideBanner_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "Url", item.Url);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            //comm.AddParameter<DateTime>(this.Factory, "EditDate", item.EditDate);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<string>(this.Factory, "Image", item.Image);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<string>(this.Factory, "SlideTitle", item.SlideTitle);            
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }
        public void Update(SlideBanner @new, SlideBanner old)
        {
            var item = @new;
            item.SlideBannerId = old.SlideBannerId;
            var comm = this.GetCommand("sp_SlideBanner_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "SlideBannerId", item.SlideBannerId);
            comm.AddParameter<string>(this.Factory, "Url", item.Url);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            //comm.AddParameter<DateTime>(this.Factory, "CreateDate", item.CreateDate);
            //comm.AddParameter<DateTime>(this.Factory, "EditDate", item.EditDate);
            comm.AddParameter<string>(this.Factory, "Image", item.Image);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<string>(this.Factory, "SlideTitle", item.SlideTitle);
            
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }

        public void Remove(SlideBanner item)
        {
            var comm = this.GetCommand("sp_SlideBanner_Delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "SlideBannerId", item.SlideBannerId);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }

        public List<SlideBanner> GetAll(int startIndex, int count, ref int totalItems)
        {
            throw new NotImplementedException();
        }

        public List<SlideBanner> SelectTop(int topcount, string culture)
        {
            var comm = this.GetCommand("sp_SlideBanner_SelectTop");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<int>(this.Factory, "TopCount", topcount);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<SlideBanner>(dt);
        }

        public List<SlideBanner> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_SlideBannerSearch");
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
            return EntityBase.ParseListFromTable<SlideBanner>(dt);
        }

        public List<SlideBanner> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
