using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Services
{
   public class OrderDetailManager : DataManagerBase<OrderDetail>
    {
        public OrderDetailManager() : base() { }
        public OrderDetailManager(IDataProvider<OrderDetail> provider) : base(provider) { }

        private IOrderDetailProvider OrderDetailProvider
        {
            get { return (IOrderDetailProvider)Provider; }
        }
        public List<OrderDetail> GetByOrderId(int id)
        {
            return OrderDetailProvider.GetByOrderId(id);
        }
        public bool Delete_Detail(int proid,int orderid)
        {
            return OrderDetailProvider.Delete_Detail(proid, orderid);
        }
    }
}
