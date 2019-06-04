using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IOrderDetailProvider : IDataProvider<OrderDetail>
    {
        List<OrderDetail> GetByOrderId(int id);
        bool Delete_Detail(int proid, int orderid);
    }
}
