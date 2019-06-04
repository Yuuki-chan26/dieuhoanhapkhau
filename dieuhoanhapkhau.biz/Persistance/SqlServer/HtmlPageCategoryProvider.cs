using dieuhoanhapkhau.biz.Extensions;
using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class HtmlPageCategoryProvider : DataAccessBase, IHtmlPageCategoryProvider
    {
        public HtmlPageCategory Get(HtmlPageCategory dummy)
        {
            var comm = this.GetCommand("sp_HtmlPageCategoryGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "HtmlPageCategoryId", dummy.HtmlPageCategoryId);
            var dt = this.GetTable(comm);
            var htmlPageCate = EntityBase.ParseListFromTable<HtmlPageCategory>(dt).FirstOrDefault();
            return htmlPageCate ?? null;
            //throw new NotImplementedException();
        }
        public HtmlPageCategory GetByShortName(string htmlPageCategoryShortName)
        {
            var comm = this.GetCommand("sp_HtmlPageCategoryGetByShortName");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryShortName", htmlPageCategoryShortName);
            var dt = this.GetTable(comm);
            var htmlPageCate = EntityBase.ParseListFromTable<HtmlPageCategory>(dt).FirstOrDefault();
            return htmlPageCate ?? null;
            //throw new NotImplementedException();
        }
        public void Add(HtmlPageCategory item, string Culture)
        {
            var comm = this.GetCommand("sp_HtmlPageCategory_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryTitle", item.HtmlPageCategoryTitle);
            comm.AddParameter<int>(this.Factory, "ParentId", item.ParentId);
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryShortName", item.HtmlPageCategoryShortName);
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryDescription", item.HtmlPageCategoryDescription);
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryKeyword", item.HtmlPageCategoryKeyword);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<string>(this.Factory, "Culture", Culture);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }
        public void Add(HtmlPageCategory item)
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
        public void Update(HtmlPageCategory @new, HtmlPageCategory old)
        {
            var item = @new;
            item.HtmlPageCategoryId = old.HtmlPageCategoryId;
            var comm = this.GetCommand("sp_HtmlPageCategory_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "HtmlPageCategoryId", item.HtmlPageCategoryId);
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryTitle", item.HtmlPageCategoryTitle);
            comm.AddParameter<int>(this.Factory, "ParentId", item.ParentId);
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryDescription", item.HtmlPageCategoryDescription);
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryKeyword", item.HtmlPageCategoryKeyword);
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryShortName", item.HtmlPageCategoryShortName);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            //comm.AddParameter<DateTime>(this.Factory, "CreateDate", item.CreateDate);
            //comm.AddParameter<DateTime>(this.Factory, "EditDate", item.EditDate);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }
        public List<HtmlPageCategory> GetAll(int startIndex, int lenght, ref int totalItem)
        {
            throw new NotImplementedException();
        }
        public void Remove(HtmlPageCategory item)
        {
            //var comm = this.GetCommand("sp_HtmlPage_Delete");
            //if (comm == null) return;
            //comm.AddParameter<int>(this.Factory, "HtmlPageId", item.HtmlPageId);
            //comm.SafeExecuteNonQuery();
            throw new NotImplementedException();
        }
        public List<HtmlPageCategory> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageCategorySearch");
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
            return EntityBase.ParseListFromTable<HtmlPageCategory>(dt);
        }
        public List<HtmlPageCategory> GetAllActiveByParentId(int htmlpagecateid, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageCategoryGetActiveByParentId");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "ParentId", htmlpagecateid);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<HtmlPageCategory>(dt);
        }
        public List<HtmlPageCategory> GetAllActiveByShortName(string htmlpageshortname, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageCategoryGetActiveByShortName");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "ShortName", htmlpageshortname);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<HtmlPageCategory>(dt);
        }
        public List<HtmlPageCategoryBase> GetAllHtmlPageCategory(string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageCategoryGetAll");
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            if (comm == null) return null;
            var dt = this.GetTable(comm);
            var listGroupNews = EntityBase.ParseListFromTable<HtmlPageCategory>(dt);
            try
            {
                var toGroupNewsTree = HtmlPageCategoryExtentions.ToCategoryTree(GetHtmlPageCategoryBaseList(listGroupNews));
                var toFlatGroupNewsTree = HtmlPageCategoryExtentions.ToFlatCategoryTree(toGroupNewsTree);
                return toFlatGroupNewsTree;
            }
            catch (Exception)
            {

                //throw;
            }
            return null;
            throw new NotImplementedException();
        }
        public List<HtmlPageCategoryBase> GetAllHtmlPageCategoryById(int id, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageCategoryGetAll");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var listGroupNews = EntityBase.ParseListFromTable<HtmlPageCategory>(dt);
            try
            {

                var toGroupNewsTree = HtmlPageCategoryExtentions.ToHtmlPageCateTreeByParentId(GetHtmlPageCategoryBaseList(listGroupNews), id);
                var toFlatGroupNewsTree = HtmlPageCategoryExtentions.ToFlatCategoryTree(toGroupNewsTree);
                return toFlatGroupNewsTree;
            }
            catch (Exception)
            {

                //throw;
            }
            return null;
            //throw new NotImplementedException();
        }
        public static List<HtmlPageCategoryBase> GetHtmlPageCategoryBaseList(List<HtmlPageCategory> s)
        {
            var listPageBase = new List<biz.Models.HtmlPageCategoryBase>();
            if (s != null)
            {
                listPageBase.AddRange(s.Cast<biz.Models.HtmlPageCategoryBase>());
            }

            return listPageBase;
        }
        
        public List<HtmlPageCategory> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<HtmlPageCategory> ListAllNewsCategory(string culture)
        {
                var comm = this.GetCommand("sp_HtmlPageCategoryGetAll");
                comm.AddParameter<string>(this.Factory, "Culture", culture);
                if (comm == null) return null;
                var dt = this.GetTable(comm);
                var listGroupNews = EntityBase.ParseListFromTable<HtmlPageCategory>(dt);

                return listGroupNews;
        }
    }
}
