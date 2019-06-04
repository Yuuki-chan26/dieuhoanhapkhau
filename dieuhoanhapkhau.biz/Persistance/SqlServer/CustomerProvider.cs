using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class CustomerProvider : DataAccessBase, ICustomerProvider
    {
        public void Add(Customers item)
        {
            var comm = this.GetCommand("Customer_Add");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "Name", item.Name);
            comm.AddParameter<string>(this.Factory, "Address", item.Address);
            comm.AddParameter<string>(this.Factory, "Email", item.Email);
            comm.AddParameter<string>(this.Factory, "Phone", item.Phone);
            comm.AddParameter<string>(this.Factory, "Username", item.UserName);
            comm.AddParameter<string>(this.Factory, "Password", item.Password);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);

            comm.SafeExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            var comm = this.GetCommand("Customer_Delete");
            if (comm == null) return false;
            comm.AddParameter<int>(this.Factory, "CustomerId", id);

            if (comm.SafeExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }

        public Customers Get(Customers dummy)
        {
            var comm = this.GetCommand("Customer_Get");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "CustomerId", dummy.CustomerId);

            var dt = this.GetTable(comm);
            var item = EntityBase.ParseListFromTable<Customers>(dt).FirstOrDefault();
            return item ?? null;
        }

        public List<Customers> GetAll()
        {
            var comm = this.GetCommand("Customer_GetAll");
            if (comm == null) return null;
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Customers>(dt);
        }

        public List<Customers> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<Customers> GetAll(int v, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public List<Customers> GetAllActive()
        {
            var comm = this.GetCommand("Customer_GetAllActive");
            if (comm == null) return null;
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Customers>(dt);
        }

        public Customers GetByPhone(string phone)
        {
            var comm = this.GetCommand("Customer_GetByPhone");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Phone", phone);

            var dt = this.GetTable(comm);
            var item = EntityBase.ParseListFromTable<Customers>(dt).FirstOrDefault();
            return item ?? null;
        }

        public int GetLastId()
        {
            var comm = this.GetCommand("Get_LastCustomerId");
            var dt = this.GetTable(comm);
            int id = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i].Field<int>("LastId");
            }
            return id;
        }

        public void Remove(Customers item)
        {
            var comm = this.GetCommand("Customer_Delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "CustomerId", item.CustomerId);

            comm.SafeExecuteNonQuery();
        }

        public List<Customers> Search(int startIndex, int lenght, ref int totalItem)
        {
            var comm = this.GetCommand("sp_CustomerSearch");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);           
            comm.AddParameter<int>(this.Factory, "Length", lenght);
            var totalItemsParam = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItemsParam.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItemsParam.Value != DBNull.Value)
            {
                totalItem = Convert.ToInt32(totalItemsParam.Value);
            }
            return EntityBase.ParseListFromTable<Customers>(dt);
        }

        public void Update(Customers @new, Customers old)
        {
            var item = @new;
            var comm = this.GetCommand("Customer_Update");
            if (comm == null) return;

            comm.AddParameter<int>(this.Factory, "CustomerId", old.CustomerId);
            comm.AddParameter<string>(this.Factory, "Name", item.Name);
            comm.AddParameter<string>(this.Factory, "Address", item.Address);
            comm.AddParameter<string>(this.Factory, "Email", item.Email);
            comm.AddParameter<string>(this.Factory, "Phone", item.Phone);
            comm.AddParameter<string>(this.Factory, "Username", item.UserName);
            comm.AddParameter<string>(this.Factory, "Password", item.Password);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);

            comm.SafeExecuteNonQuery();
        }
    }
}
