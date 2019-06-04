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
	public class ProductPropertyProvider : DataAccessBase, IProductPropertyProvider
	{
		public ProductProperty Get(ProductProperty dummy)
		{
			var comm = this.GetCommand("sp_ProductPropertyGet");
			if (comm == null)
			{
				return null;
			}
            comm.AddParameter<int>(this.Factory, "ProductPropertyId", dummy.ProductPropertyId);
			var dt = this.GetTable(comm);
			var htmlPage = EntityBase.ParseListFromTable<ProductProperty>(dt).FirstOrDefault();
			return htmlPage ?? null;
			//throw new NotImplementedException();
		}
		public List<ProductProperty> GetByPrdId(int productId, string culture)
		{
			var comm = this.GetCommand("sp_ProductPropertyGetByPrdId");
			if (comm == null)
			{
				return null;
			}
			comm.AddParameter<int>(this.Factory, "ProductId", productId);
			comm.AddParameter<string>(this.Factory, "Culture", culture);
			var dt = this.GetTable(comm);
			var htmlPage = EntityBase.ParseListFromTable<ProductProperty>(dt);
			return htmlPage ?? null;
			//throw new NotImplementedException();
		}
		public void Add(ProductProperty item, string Culture)
		{
			var comm = this.GetCommand("sp_ProductProperty_Insert");
			if (comm == null) return;
			comm.AddParameter<string>(this.Factory, "ProductPropertyTitle", item.ProductPropertyTitle);
			comm.AddParameter<string>(this.Factory, "ProductPropertyBody", item.ProductPropertyBody);
			comm.AddParameter<string>(this.Factory, "ProductCode", item.ProductCode);
			comm.AddParameter<string>(this.Factory, "ProductName", item.ProductName);
			comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
			
			comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", item.PrdCategoryId);
            comm.AddParameter<string>(this.Factory, "ShortName", item.ShortName);
            comm.AddParameter<string>(this.Factory, "Culture", Culture);
            comm.SafeExecuteNonQuery();
			//throw new NotImplementedException();
		}
		public void Add(ProductProperty item)
		{
			//var comm = this.GetCommand("sp_HtmlPage_Insert");
			//if (comm == null) return;
			//comm.AddParameter<string>(this.Factory, "HtmlPageTitle", item.HtmlPageTitle);
			//comm.AddParameter<string>(this.Factory, "HtmlPageShortName", item.HtmlPageShortName);
			//comm.AddParameter<string>(this.Factory, "HtmlPageSummary", item.HtmlPageSummary);
			//comm.AddParameter<string>(this.Factory, "HtmlPageDescription", item.HtmlPageDescription);
			//comm.AddParameter<string>(this.Factory, "HtmlPageKeyword", item.HtmlPageKeyword);
			//comm.AddParameter<string>(this.Factory, "HtmlPageBody", item.HtmlPageBody);
			//comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
			//comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
			//comm.AddParameter<string>(this.Factory, "HtmlPageImage", item.HtmlPageImage);
			//comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
			//comm.AddParameter<string>(this.Factory, "Culture", Culture);
			//comm.SafeExecuteNonQuery();
			throw new NotImplementedException();
		}
		public void Update(ProductProperty @new, ProductProperty old)
		{
			var item = @new;
			item.ProductPropertyId = old.ProductPropertyId;
			var comm = this.GetCommand("sp_ProductProperty_Update");
			if (comm == null) return;
			comm.AddParameter<int>(this.Factory, "ProductPropertyId", item.ProductPropertyId);
			comm.AddParameter<string>(this.Factory, "ProductPropertyBody", item.ProductPropertyBody);
			comm.AddParameter<string>(this.Factory, "ProductCode", item.ProductCode);
			comm.AddParameter<string>(this.Factory, "ProductName", item.ProductName);
			comm.AddParameter<string>(this.Factory, "ProductPropertyTitle", item.ProductPropertyTitle);
			comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
			comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", item.PrdCategoryId);
            comm.AddParameter<string>(this.Factory, "ShortName", item.ShortName);
            //comm.AddParameter<DateTime>(this.Factory, "CreateDate", item.CreateDate);
            //comm.AddParameter<DateTime>(this.Factory, "EditDate", item.EditDate);
            comm.SafeExecuteNonQuery();
			//throw new NotImplementedException();
		}

		public void Remove(ProductProperty item)
		{
			var comm = this.GetCommand("sp_ProductProperty_Delete");
			if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProductPropertyId", item.ProductPropertyId);
			comm.SafeExecuteNonQuery();
			//throw new NotImplementedException();
		}

		public List<ProductProperty> GetAll(int startIndex, int lenght, ref int totalItem)
		{
			throw new NotImplementedException();
		}

        public List<ProductProperty> GetAllActive(string culture)
        {
            var comm = this.GetCommand("sp_ProductPropertyGetAllActiveByPrdId");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<ProductProperty>(dt);
            return htmlPage ?? null;
            //throw new NotImplementedException();
        }

        public List<ProductProperty> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }
       
        public ProductProperty GetByPrdTitle(ProductProperty dummy, string culture)
        {
            var comm = this.GetCommand("sp_ProductPropertyGetByTitle");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "ProductPropertyTitle", dummy.ProductPropertyTitle);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPageCate = EntityBase.ParseListFromTable<ProductProperty>(dt).FirstOrDefault();
            return htmlPageCate ?? null;
            //throw new NotImplementedException();
        }
        public List<ProductProperty> GetAllActive(ProductProperty model, string culture)
        {
            var comm = this.GetCommand("sp_ProductProperty_GetAllActive");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<ProductProperty>(dt);
        }

        public List<ProductProperty> GetByProducer(int id, int parentid)
        {
            var comm = this.GetCommand("sp_ProductProperty_GetByManufacter");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "ProducerId", id);
            comm.AddParameter<int>(this.Factory, "ParentId", parentid);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<ProductProperty>(dt);
        }

        public List<ProductProperty> GetByCateId(int id)
        {
            var comm = this.GetCommand("sp_ProductProperty_GetByCateId");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", id);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<ProductProperty>(dt);
        }

        public ProductProperty GetByShortName(string shortname, string culture)
        {
            var comm = this.GetCommand("sp_ProductProperty_GetShortName");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "ShortName", shortname);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var html = EntityBase.ParseListFromTable<ProductProperty>(dt).FirstOrDefault();
            return html ?? null;
        }

        public List<ProductProperty> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_ProductProSearch");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<int>(this.Factory, "Length", lenght);
            var totalItemsParam = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItemsParam.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItemsParam.Value != DBNull.Value)
            {
                totalItem = Convert.ToInt32(totalItemsParam.Value);
            }
            return EntityBase.ParseListFromTable<ProductProperty>(dt);
        }

        public List<ProductProperty> GetAllProperty(string culture)
        {
            var comm = this.GetCommand("sp_ProductPropertyGetAll");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<ProductProperty>(dt);
        }
    }
}
