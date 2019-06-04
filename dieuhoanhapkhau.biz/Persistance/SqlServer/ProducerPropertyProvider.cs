using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
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
    public class ProducerPropertyProvider : DataAccessBase, IProducerPropertyProvider
    {
        public void Add(ProducerProperties item)
        {
            throw new NotImplementedException();
        }

        public void Add(ProducerProperties item, string culture)
        {
            var comm = this.GetCommand("sp_ProducerProperty_Insert");
            if (comm == null) return;
            //comm.AddParameter<int>(this.Factory, "ProducerPropertyId", item.ProducerPropertyId);
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.AddParameter<string>(this.Factory, "PropertyName", item.PropertyName);
            comm.AddParameter<string>(this.Factory, "ShortName", item.ShortName);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.SafeExecuteNonQuery();
        }

        public ProducerProperties Get(ProducerProperties dummy)
        {
            var comm = this.GetCommand("sp_ProducerPropertyGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "ProducerPropertyId", dummy.ProducerPropertyId);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<ProducerProperties>(dt).FirstOrDefault();
            return htmlPage ?? null;
        }

        public List<ProducerProperties> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<ProducerProperties> GetAll(int v, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public List<ProducerProperties> GetAllProducerProperty(string culture)
        {
            var comm = this.GetCommand("sp_ProducerPropertyGetAll");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<ProducerProperties>(dt);
        }

        public List<ProducerProperties> GetByProducerId(int producerid, string culture)
        {
            var comm = this.GetCommand("sp_ProducerGetByProducerId");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "ProducerId", producerid);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<ProducerProperties>(dt);
        }

        public ProducerProperties GetByProducerId(ProducerProperties dummy, string culture)
        {
            var comm = this.GetCommand("sp_ProducerPropertyGetByProducerId");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "ProducerId", dummy.ProducerId);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<ProducerProperties>(dt).FirstOrDefault();
            return htmlPage ?? null;
        }

        public ProducerProperties GetByShortName(ProducerProperties dummy, string culture)
        {
            var comm = this.GetCommand("sp_ProducerPropertyGetByShortName");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "ShortName", dummy.ShortName);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<ProducerProperties>(dt).FirstOrDefault();
            return htmlPage ?? null;
        }

        public void Remove(ProducerProperties item)
        {
            var comm = this.GetCommand("sp_ProducerProperty_Delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProducerPropertyId", item.ProducerPropertyId);
            comm.SafeExecuteNonQuery();
        }

        public List<ProducerProperties> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_ProducerPropertySearch");
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
            return EntityBase.ParseListFromTable<ProducerProperties>(dt);
        }

        public void Update(ProducerProperties @new, ProducerProperties old)
        {
            var item = @new;
            item.ProducerPropertyId = old.ProducerPropertyId;
            var comm = this.GetCommand("sp_ProducerProperty_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProducerPropertyId", item.ProducerPropertyId);
            comm.AddParameter<string>(this.Factory, "PropertyName", item.PropertyName);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.SafeExecuteNonQuery();
        }
    }
}
