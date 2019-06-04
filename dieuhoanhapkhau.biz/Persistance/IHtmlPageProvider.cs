using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;


namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IHtmlPageProvider : IDataProvider<HtmlPage>
    {
        List<HtmlPage> Search(int startIndex, int lenght, ref int totalItem, string culture);
        void Add(HtmlPage model, string culture);
        List<HtmlPage> GetHtmlPageByCateId(int Cateid, string culture);
        HtmlPage GetHtmlPageByShortName(HtmlPage model, string culture);
        HtmlPage HtmlPageGetByShortName(string shortname, string culture);
        List<HtmlPage> GetOtherNews(int htmlid, string culture);
        HtmlPage GetDetail(HtmlPage model);
        HtmlPageCategory GetHtmlPageCategoryGetByHtmlPageCategoryShortName(string HtmlPageCategoryShortName);
        List<HtmlPage> GetHotNewsTop(int topcount, string culture);
        List<HtmlPage> GetByShortNameCate(string name, int startIndex, int count, ref int totalItems, string culture);
    }
}
