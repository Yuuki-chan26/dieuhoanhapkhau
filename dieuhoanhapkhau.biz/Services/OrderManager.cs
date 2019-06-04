
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
    public class OrderManager: DataManagerBase<Orders>
    {
        public OrderManager() : base() { }
        public OrderManager(IDataProvider<Orders> provider) : base(provider) { }

        private IOrderProvider OrderProvider
        {
            get { return (IOrderProvider)Provider; }
        }
        public List<Orders> GetAll()
        {
            return OrderProvider.GetAll();
        }
        public List<Orders> FilterByIsActive()
        {
            return OrderProvider.FilterByIsActive();
        }
        public int GetLastId()
        {
            return OrderProvider.GetLastId();
        }
        public bool Delete(int orderid)
        {
            return OrderProvider.Delete(orderid);
        }
    }
}
