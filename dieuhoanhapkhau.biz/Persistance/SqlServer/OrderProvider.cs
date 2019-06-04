using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
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
    public class OrderProvider : DataAccessBase, IOrderProvider
    {
        public void Add(Orders item)
        {
            var comm = this.GetCommand("Order_Add");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "CustomerId", item.CustomerId);
            comm.AddParameter<string>(this.Factory, "Note", item.Note);
            comm.AddParameter<int>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<double>(this.Factory, "TotalMoney", item.ToTalMoney);
            comm.SafeExecuteNonQuery();
        }

        public bool Delete(int orderid)
        {
            var comm = this.GetCommand("Order_Delete");
            if (comm == null) return false;
            comm.AddParameter<int>(this.Factory, "OrderId", orderid);

            if (comm.SafeExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }

        public List<Orders> FilterByIsActive()
        {
            var comm = this.GetCommand("Order_FilterByIsActive");
            if (comm == null) return null;
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Orders>(dt);
        }

        public Orders Get(Orders dummy)
        {
            var comm = this.GetCommand("Order_Get");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "OrderId", dummy.OrderId);
            var dt = this.GetTable(comm);
            var item = EntityBase.ParseListFromTable<Orders>(dt).FirstOrDefault();
            return item ?? null;
        }

        public List<Orders> GetAll()
        {
            var comm = this.GetCommand("Order_GetAll");
            if (comm == null) return null;
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Orders>(dt);
        }

        public List<Orders> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<Orders> GetAll(int v, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public int GetLastId()
        {
            var comm = this.GetCommand("Get_LastOrderId");
            var dt = this.GetTable(comm);
            int id = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i].Field<int>("LastId");
            }
            return id;
        }

        public void Remove(Orders item)
        {
            throw new NotImplementedException();
        }

        public void Update(Orders @new, Orders old)
        {
            var item = @new;
            var comm = this.GetCommand("Order_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "OrderId", old.OrderId);
            comm.AddParameter<string>(this.Factory, "Note", item.Note);
            comm.AddParameter<int>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<double>(this.Factory, "ToTalMoney", item.ToTalMoney);

            comm.SafeExecuteNonQuery();
        }
    }
}
