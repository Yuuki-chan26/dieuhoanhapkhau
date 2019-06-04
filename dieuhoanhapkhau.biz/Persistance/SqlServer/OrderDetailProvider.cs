using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class OrderDetailProvider : DataAccessBase, IOrderDetailProvider
    {
        public void Add(OrderDetail item)
        {
            var comm = this.GetCommand("Order_Product_Add");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "OrderId", item.OrderId);
            comm.AddParameter<int>(this.Factory, "ProductId", item.ProductId);
            comm.AddParameter<int>(this.Factory, "Quality", item.Quality);
            comm.AddParameter<string>(this.Factory, "Note", item.Note);

            comm.SafeExecuteNonQuery();
        }

        public bool Delete_Detail(int proid, int orderid)
        {
            var comm = this.GetCommand("Order_Detail_Delete");
            if (comm == null) return false;
            comm.AddParameter<int>(this.Factory, "OrderId", orderid);
            comm.AddParameter<int>(this.Factory, "ProductId", proid);

            if (comm.SafeExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }

        public OrderDetail Get(OrderDetail dummy)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetAll(int v, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetByOrderId(int id)
        {
            var comm = this.GetCommand("Order_GetDetails");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "OrderId", id);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<OrderDetail>(dt);
        }

        public void Remove(OrderDetail item)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDetail @new, OrderDetail old)
        {
            throw new NotImplementedException();
        }
    }
}
