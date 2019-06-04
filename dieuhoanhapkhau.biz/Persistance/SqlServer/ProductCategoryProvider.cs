using dieuhoanhapkhau.biz.Extensions;
using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class ProductCategoryProvider : DataAccessBase, IProductCategoryProvider
    {
        public ProductCategory Get(ProductCategory dummy)
        {
            var comm = this.GetCommand("sp_ProductCategoryGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", dummy.PrdCategoryId);
            var dt = this.GetTable(comm);
            var htmlPageCate = EntityBase.ParseListFromTable<ProductCategory>(dt).FirstOrDefault();
            return htmlPageCate ?? null;
            //throw new NotImplementedException();
        }
        public ProductCategory GetByShortName(ProductCategory dummy,string culture)
        {
            var comm = this.GetCommand("sp_ProductCategoryGetByShortName");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "PrdCategoryShortName", dummy.PrdCategoryShortName);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPageCate = EntityBase.ParseListFromTable<ProductCategory>(dt).FirstOrDefault();
            return htmlPageCate ?? null;
            //throw new NotImplementedException();
        }
        public void Add(ProductCategory item, string Culture)
        {
            var comm = this.GetCommand("sp_ProductCategory_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "PrdCategoryTitle", item.PrdCategoryTitle);
            comm.AddParameter<int>(this.Factory, "ParentId", item.ParentId);
            comm.AddParameter<string>(this.Factory, "PrdCategoryShortName", item.PrdCategoryShortName);
            comm.AddParameter<string>(this.Factory, "PrdCategorySummary", item.PrdCategorySummary);
            comm.AddParameter<string>(this.Factory, "PrdCategoryDescription", item.PrdCategoryDescription);
            comm.AddParameter<string>(this.Factory, "PrdCategoryKeyword", item.PrdCategoryKeyword);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<string>(this.Factory, "Culture", Culture);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }
        public void Add(ProductCategory item)
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
        public void Update(ProductCategory @new, ProductCategory old)
        {
            var item = @new;
            item.PrdCategoryId = old.PrdCategoryId;
            var comm = this.GetCommand("sp_ProductCategory_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", item.PrdCategoryId);
            comm.AddParameter<string>(this.Factory, "PrdCategoryTitle", item.PrdCategoryTitle);
            comm.AddParameter<int>(this.Factory, "ParentId", item.ParentId);
            comm.AddParameter<string>(this.Factory, "PrdCategorySummary", item.PrdCategorySummary);
            comm.AddParameter<string>(this.Factory, "PrdCategoryDescription", item.PrdCategoryDescription);
            comm.AddParameter<string>(this.Factory, "PrdCategoryKeyword", item.PrdCategoryKeyword);
            comm.AddParameter<string>(this.Factory, "PrdCategoryShortName", item.PrdCategoryShortName);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            //comm.AddParameter<DateTime>(this.Factory, "CreateDate", item.CreateDate);
            //comm.AddParameter<DateTime>(this.Factory, "EditDate", item.EditDate);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }
        public List<ProductCategory> GetAll(int startIndex, int lenght, ref int totalItem)
        {
            throw new NotImplementedException();
        }
        public void Remove(ProductCategory item)
        {
            //var comm = this.GetCommand("sp_HtmlPage_Delete");
            //if (comm == null) return;
            //comm.AddParameter<int>(this.Factory, "HtmlPageId", item.HtmlPageId);
            //comm.SafeExecuteNonQuery();
            throw new NotImplementedException();
        }
        public List<ProductCategory> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_ProductCategorySearch");
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
            return EntityBase.ParseListFromTable<ProductCategory>(dt);
        }
        public List<ProductCategoryBase> GetAllProductCategory(string culture)
        {
            var comm = this.GetCommand("sp_ProductCategoryGetAll");
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            if (comm == null) return null;
            var dt = this.GetTable(comm);
            var listGroupNews = EntityBase.ParseListFromTable<ProductCategory>(dt);
            try
            {
                var toGroupNewsTree = ProductCategoryExtensions.ToCategoryTree(GetProductCategoryBaseList(listGroupNews));
                var toFlatGroupNewsTree = ProductCategoryExtensions.ToFlatCategoryTree(toGroupNewsTree);
                return toFlatGroupNewsTree;
            }
            catch (Exception)
            {

                //throw;
            }
            return null;
            //throw new NotImplementedException();
        }
        public List<ProductCategory> ListAllProductCategory(string culture)
        {
            var comm = this.GetCommand("sp_ProductCategoryGetAll");
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            if (comm == null) return null;
            var dt = this.GetTable(comm);
            var listGroupNews = EntityBase.ParseListFromTable<ProductCategory>(dt);

            return listGroupNews;
            //throw new NotImplementedException();
        }
        public static List<ProductCategoryBase> GetProductCategoryBaseList(List<ProductCategory> s)
        {
            var listPageBase = new List<ProductCategoryBase>();
            if (s != null)
            {
                listPageBase.AddRange(s.Cast<ProductCategoryBase>());
            }

            return listPageBase;
        }

        public List<ProductCategory> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<ProductCategory> GetAllActiveByParentId(int parentid, string culture)
        {
                var comm = this.GetCommand("sp_ProductCategoryGetActiveByParentId");
                if (comm == null) return null;
                comm.AddParameter<int>(this.Factory, "ParentId", parentid);
                comm.AddParameter<string>(this.Factory, "Culture", culture);
                var dt = this.GetTable(comm);
                return EntityBase.ParseListFromTable<ProductCategory>(dt);
        }

        public List<ProductCategoryBase> GetAllProductProducer(string culture)
        {
            var comm = this.GetCommand("sp_ProductProducerGetAll");
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            if (comm == null) return null;
            var dt = this.GetTable(comm);
            var listGroupNews = EntityBase.ParseListFromTable<ProductCategory>(dt);
            try
            {
                var toGroupNewsTree = ProductCategoryExtensions.ToCategoryTree(GetProductCategoryBaseList(listGroupNews));
                var toFlatGroupNewsTree = ProductCategoryExtensions.ToFlatCategoryTree(toGroupNewsTree);
                return toFlatGroupNewsTree;
            }
            catch (Exception)
            {

                //throw;
            }
            return null;
            //throw new NotImplementedException();
        }

        public List<ProductCategory> GetByParentId(int id)
        {
            var comm = this.GetCommand("sp_ProductCate_GetParentId");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "ParentId", id);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<ProductCategory>(dt);
        }
    }
}
