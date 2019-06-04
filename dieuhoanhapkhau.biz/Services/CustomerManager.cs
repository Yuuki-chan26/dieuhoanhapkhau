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
   public class CustomerManager : DataManagerBase<Customers>
    {
        public CustomerManager() : base() { }
        public CustomerManager(IDataProvider<Customers> provider) : base(provider) { }

        private ICustomerProvider CustomerProvider
        {
            get { return (ICustomerProvider)Provider; }
        }
        public List<Customers> GetAll()
        {
            return CustomerProvider.GetAll();
        }
        public List<Customers> GetAllActive()
        {
            return CustomerProvider.GetAllActive();
        }
        public Customers GetByPhone(string phone)
        {
            return CustomerProvider.GetByPhone(phone);
        }
        public bool Delete(int id)
        {
            return CustomerProvider.Delete(id);
        }
        public int GetLastId()
        {
            return CustomerProvider.GetLastId();
        }
        public List<Customers> Search(int startIndex, int lenght, ref int totalItem)
        {
            return CustomerProvider.Search(startIndex, lenght, ref totalItem);
        }
    }
}
