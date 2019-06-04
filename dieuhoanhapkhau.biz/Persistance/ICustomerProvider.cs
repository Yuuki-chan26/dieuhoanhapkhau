using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance
{
   public interface ICustomerProvider : IDataProvider<Customers>
    {
        List<Customers> GetAll();
        List<Customers> GetAllActive();
        int GetLastId();
        Customers GetByPhone(string phone);
        bool Delete(int id);
        List<Customers> Search(int startIndex, int lenght, ref int totalItem);
    }
}
