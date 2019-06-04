using dieuhoanhapkhau.biz.Extensions;
using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class ProducerProvider : DataAccessBase, IProducerProvider
    {
        public void Add(Producers item, string culture)
        {
            var comm = this.GetCommand("sp_Producer_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "ProducerName", item.ProducerName);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.SafeExecuteNonQuery();
        }

        public void Add(Producers item)
        {
            throw new NotImplementedException();
        }

        public Producers Get(Producers dummy)
        {
            var comm = this.GetCommand("sp_ProducerGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "ProducerId", dummy.ProducerId);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<Producers>(dt).FirstOrDefault();
            return htmlPage ?? null;
        }

        public List<Producers> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<Producers> GetAll(int v, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public List<Producers> GetAllActive(string culture)
        {
            var comm = this.GetCommand("Manufacters_GetAllActive");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Producers>(dt);
        }

        public List<Producers> GetAllProducer(string culture)
        {
            var comm = this.GetCommand("sp_ProducerGetAll");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Producers>(dt);
            
        }

        public List<Producers> GetByPrdId(int productid, string culture)
        {
            throw new NotImplementedException();
        }

        public List<Producers> GetListProducerByCateId(int cateid, int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_ProducerSearchByCateId");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "ProductImageId", cateid);
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
            return EntityBase.ParseListFromTable<Producers>(dt);
        }

        public void Remove(Producers item)
        {
            var comm = this.GetCommand("sp_Producer_Delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.SafeExecuteNonQuery();
        }

        public List<Producers> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_ProducerSearch");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<int>(this.Factory, "Length", lenght);
            var totalItemsParam = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItemsParam.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItemsParam.Value != DBNull.Value)
            {
                totalItem = Convert.ToInt32(totalItemsParam.Value);
            }
            return EntityBase.ParseListFromTable<Producers>(dt);
        }

        public void Update(Producers @new, Producers old)
        {
            var item = @new;
            item.ProducerId= old.ProducerId;
            var comm = this.GetCommand("sp_Producer_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.AddParameter<string>(this.Factory, "ProducerName", item.ProducerName);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.SafeExecuteNonQuery();
        }
    }
}
