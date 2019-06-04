using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IOrderProvider: IDataProvider<Orders>
    {
        List<Orders> GetAll();
        List<Orders> FilterByIsActive();
        int GetLastId();
        bool Delete(int orderid);
    }
}
