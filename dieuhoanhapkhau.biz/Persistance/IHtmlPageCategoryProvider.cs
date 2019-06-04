using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idocNet.Core.Data;
using dieuhoanhapkhau.biz.Models;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IHtmlPageCategoryProvider : IDataProvider<HtmlPageCategory>
    {
        List<HtmlPageCategory> Search(int startIndex, int lenght, ref int totalItem, string culture);
        List<HtmlPageCategoryBase> GetAllHtmlPageCategory(string culture);
        void Add(HtmlPageCategory model, string culture);
        List<HtmlPageCategoryBase> GetAllHtmlPageCategoryById(int id, string culture);
        List<HtmlPageCategory> GetAllActiveByParentId(int parentid, string culture);
        List<HtmlPageCategory> GetAllActiveByShortName(string parentshortname, string culture);
        HtmlPageCategory GetByShortName(string htmlPageCategoryShortName);
        List<HtmlPageCategory> ListAllNewsCategory(string culture);
    }
}
