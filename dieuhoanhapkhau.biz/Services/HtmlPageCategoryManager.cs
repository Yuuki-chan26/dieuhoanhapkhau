using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using dieuhoanhapkhau.biz.Persistance.SqlServer;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Services
{
    public class HtmlPageCategoryManager : DataManagerBase<HtmlPageCategory>
    {
        public HtmlPageCategoryManager()
            : base()
        { }

        public HtmlPageCategoryManager(IDataProvider<HtmlPageCategory> provider)
            : base(provider)
        {
        }
        private IHtmlPageCategoryProvider HtmlPageCategoryProvider
        {
            get { return (IHtmlPageCategoryProvider)Provider; }
        }

        public List<HtmlPageCategory> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return HtmlPageCategoryProvider.Search(startIndex, lenght, ref totalItem, culture);
        }

        public List<HtmlPageCategoryBase> GetAllHtmlPageCategory(string culture)
        {
            return HtmlPageCategoryProvider.GetAllHtmlPageCategory(culture);
        }
        public HtmlPageCategory GetByShortName(string htmlPageCategoryShortName)
        {
            return HtmlPageCategoryProvider.GetByShortName(htmlPageCategoryShortName);
        }
        public List<HtmlPageCategoryBase> GetAllHtmlPageCategoryById(int id, string culture)
        {
            return HtmlPageCategoryProvider.GetAllHtmlPageCategoryById(id, culture);
        }
        public List<HtmlPageCategory> GetAllActiveByParentId(int parentid, string culture)
        {
            return HtmlPageCategoryProvider.GetAllActiveByParentId(parentid, culture);
        }
        public List<HtmlPageCategory> GetAllActiveByShortName(string parentid, string culture)
        {
            return HtmlPageCategoryProvider.GetAllActiveByShortName(parentid, culture);
        }
        public void Add(HtmlPageCategory model, string Culture)
        {
            HtmlPageCategoryProvider.Add(model, Culture);
        }
        public List<HtmlPageCategory> ListAllNewsCategory(string culture)
        {
            return HtmlPageCategoryProvider.ListAllNewsCategory(culture);
        }
    }
}
