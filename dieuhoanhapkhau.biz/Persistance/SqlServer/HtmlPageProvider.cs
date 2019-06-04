using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class HtmlPageProvider : DataAccessBase, IHtmlPageProvider
    {
        public HtmlPage Get(HtmlPage dummy)
        {
            var comm = this.GetCommand("sp_HtmlPageGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "HtmlPageId", dummy.HtmlPageId);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<HtmlPage>(dt).FirstOrDefault();
            return htmlPage ?? null;
            //throw new NotImplementedException();
        }
        public HtmlPage GetHtmlPageByShortName(HtmlPage dummy, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageGetByShortName");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "HtmlPageShortName", dummy.HtmlPageShortName);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<HtmlPage>(dt).FirstOrDefault();
            return htmlPage ?? null;
            //throw new NotImplementedException();
        }
        public HtmlPage HtmlPageGetByShortName(string shortname, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageGetByShortName");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "HtmlPageShortName", shortname);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<HtmlPage>(dt).FirstOrDefault();
            return htmlPage ?? null;
            //throw new NotImplementedException();
        }
        public void Add(HtmlPage item, string Culture)
        {
            var comm = this.GetCommand("sp_HtmlPage_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "HtmlPageTitle", item.HtmlPageTitle);
            comm.AddParameter<int>(this.Factory, "HtmlPageCategoryId", item.HtmlPageCategoryId);
            comm.AddParameter<string>(this.Factory, "HtmlPageShortName", item.HtmlPageShortName);
            comm.AddParameter<string>(this.Factory, "HtmlPageDescription", item.HtmlPageDescription);
            comm.AddParameter<string>(this.Factory, "HtmlPageKeyword", item.HtmlPageKeyword);
            comm.AddParameter<string>(this.Factory, "HtmlPageBody", item.HtmlPageBody);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<bool>(this.Factory, "IsHotNews", item.IsActive);
            comm.AddParameter<string>(this.Factory, "HtmlPageImage", item.HtmlPageImage);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<string>(this.Factory, "Culture", Culture);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }
        public void Add(HtmlPage item)
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
        public void Update(HtmlPage @new, HtmlPage old)
        {
            var item = @new;
            item.HtmlPageId = old.HtmlPageId;
            var comm = this.GetCommand("sp_HtmlPage_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "HtmlPageId", item.HtmlPageId);
            comm.AddParameter<int>(this.Factory, "HtmlPageCategoryId", item.HtmlPageCategoryId);
            comm.AddParameter<string>(this.Factory, "HtmlPageTitle", item.HtmlPageTitle);
            comm.AddParameter<string>(this.Factory, "HtmlPageShortName", item.HtmlPageShortName);
            comm.AddParameter<string>(this.Factory, "HtmlPageDescription", item.HtmlPageDescription);
            comm.AddParameter<string>(this.Factory, "HtmlPageKeyword", item.HtmlPageKeyword);
            comm.AddParameter<string>(this.Factory, "HtmlPageBody", item.HtmlPageBody);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<bool>(this.Factory, "IsHotNews", item.IsHotNews);
            comm.AddParameter<string>(this.Factory, "HtmlPageImage", item.HtmlPageImage);
            //comm.AddParameter<DateTime>(this.Factory, "CreateDate", item.CreateDate);
            //comm.AddParameter<DateTime>(this.Factory, "EditDate", item.EditDate);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }

        public void Remove(HtmlPage item)
        {
            var comm = this.GetCommand("sp_HtmlPage_Delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "HtmlPageId", item.HtmlPageId);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }

        public List<HtmlPage> GetAll(int startIndex, int lenght, ref int totalItem)
        {
            throw new NotImplementedException();
        }

        //public List<HtmlPage> SelectTop4()
        //{
        //    var comm = this.GetCommand("sp_SliderBanner_SelectTop4");
        //    if (comm == null) return null;
        //    var dt = this.GetTable(comm);
        //    return EntityBase.ParseListFromTable<HtmlPage>(dt);
        //}

        public List<HtmlPage> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageSearch");
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
            return EntityBase.ParseListFromTable<HtmlPage>(dt);
        }
        public List<HtmlPage> GetHtmlPageByCateId(int cateid, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageGetByCateId");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "HtmlPageCategoryId", cateid);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<HtmlPage>(dt);
        }

        public List<HtmlPage> GetAll(int startIndex, int length, ref int totalItems, string culture)
        {
            var comm = this.GetCommand("sp_HtmlPageSearch");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<int>(this.Factory, "Length", length);
            comm.AddParameter<string>(this.Factory, "Culture", culture);

            var totalItemsParam = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItemsParam.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItemsParam.Value != DBNull.Value)
            {
                totalItems = Convert.ToInt32(totalItemsParam.Value);
            }
            return EntityBase.ParseListFromTable<HtmlPage>(dt);
        }

        public List<HtmlPage> GetOtherNews(int htmlid, string culture)
        {
            var comm = this.GetCommand("sp_NewsGetOther");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<int>(this.Factory, "HtmlPageId", htmlid);
            var dt = this.GetTable(comm);

            return EntityBase.ParseListFromTable<HtmlPage>(dt);
        }

        public HtmlPage GetDetail(HtmlPage dummy)
        {
            var comm = this.GetCommand("sp_HtmlGetDetail");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "HtmlPageId", dummy.HtmlPageId);
            comm.AddParameter<string>(this.Factory, "HtmlPageShortName", dummy.HtmlPageShortName);
            comm.AddParameter<int>(this.Factory, "HtmlPageCategoryId", dummy.HtmlPageCategoryId);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<HtmlPage>(dt).FirstOrDefault();
            return htmlPage ?? null;
        }

        public HtmlPageCategory GetHtmlPageCategoryGetByHtmlPageCategoryShortName(string HtmlPageCategoryShortName)
        {
                var comm = this.GetCommand("sp_HtmlPageCategoryGetByHtmlPageCategoryShortName");
                if (comm == null)
                {
                    return null;
                }
                comm.AddParameter<string>(this.Factory, "HtmlPageCategoryShortName", HtmlPageCategoryShortName);
                var dt = this.GetTable(comm);
                var htmlPage = EntityBase.ParseListFromTable<HtmlPageCategory>(dt).FirstOrDefault();
                return htmlPage ?? null;
        }

        public List<HtmlPage> GetHotNewsTop(int topcount, string culture)
        {
            var comm = this.GetCommand("sp_NewsGetHot");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<int>(this.Factory, "TopCount", topcount);
            var dt = this.GetTable(comm);

            return EntityBase.ParseListFromTable<HtmlPage>(dt);
        }
        public List<HtmlPage> GetByShortNameCate(string name, int startIndex, int length, ref int total, string culture)
        {
            var comm = this.GetCommand("sp_News_GetByShortNameCate");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "HtmlPageCategoryShortName", name);
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<int>(this.Factory, "Length", length);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var totalItem = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItem.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItem.Value != DBNull.Value)
            {
                total = Convert.ToInt32(totalItem.Value);
            }
            return EntityBase.ParseListFromTable<HtmlPage>(dt);
        }
    }
}
