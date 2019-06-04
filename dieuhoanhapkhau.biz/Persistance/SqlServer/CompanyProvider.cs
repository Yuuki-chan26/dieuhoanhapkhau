using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class CompanyProvider : DataAccessBase, IProductImageProvider
    {
        public Company Get(Company dummy)
        {
            var comm = this.GetCommand("sp_CompanyGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "CompanyId", dummy.CompanyId);
            var dt = this.GetTable(comm);
            var company = EntityBase.ParseListFromTable<Company>(dt).FirstOrDefault();
            return company ?? null;
            //throw new NotImplementedException();
        }

        public void Add(Company item)
        {
            //var comm = this.GetCommand("sp_Company_Insert");
            //if (comm == null) return;
            //comm.AddParameter<string>(this.Factory, "Url", item.Url);
            //comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            ////comm.AddParameter<DateTime>(this.Factory, "CreateDate", item.CreateDate);
            ////comm.AddParameter<DateTime>(this.Factory, "EditDate", item.EditDate);
            //comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            //comm.AddParameter<string>(this.Factory, "Image", item.Image);
            //comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            //comm.SafeExecuteNonQuery();
            throw new NotImplementedException();
        }

        public void Add(Company item, string culture)
        {
            //var comm = this.GetCommand("sp_Company_Insert");
            //if (comm == null) return;
            //comm.AddParameter<string>(this.Factory, "CompanyName", item.CompanyName);
        
            //comm.SafeExecuteNonQuery();
            
        }
        public void Update(Company @new, Company old)
        {
            var item = @new;
            item.CompanyId = old.CompanyId;
            var comm = this.GetCommand("sp_CompanyUpdate");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "CompanyId", item.CompanyId);
            comm.AddParameter<string>(this.Factory, "CompanyName", item.CompanyName);        
            comm.AddParameter<string>(this.Factory, "CompanyHotline", item.CompanyHotline);
            comm.AddParameter<string>(this.Factory, "CompanyInfo", item.CompanyInfo);
            comm.AddParameter<string>(this.Factory, "CompanyMail", item.CompanyMail);
            comm.AddParameter<string>(this.Factory, "CompanyYoutube", item.CompanyYoutube);
            comm.AddParameter<string>(this.Factory, "CompanyFace", item.CompanyFace);
            comm.AddParameter<string>(this.Factory, "CompanyGoogle", item.CompanyGoogle);
            comm.AddParameter<string>(this.Factory, "UpdateBy", item.UpdateBy);
            comm.SafeExecuteNonQuery();
        }

        public void Remove(Company item)
        {
            var comm = this.GetCommand("sp_company_delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "companyid", item.CompanyId);
            comm.SafeExecuteNonQuery();
            
        }

        public List<Company> GetAll(int startIndex, int count, ref int totalItems)
        {
            throw new NotImplementedException();
        }

        public List<Company> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
