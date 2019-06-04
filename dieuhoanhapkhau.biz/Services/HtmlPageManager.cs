using System;
using System.Collections.Generic;
using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;

namespace dieuhoanhapkhau.biz.Services
{
   
    public class HtmlPageManager : DataManagerBase<HtmlPage>
    {
        public HtmlPageManager()
            : base()
        { }

        public HtmlPageManager(IDataProvider<HtmlPage> provider)
            : base(provider)
        {
        }

        private IHtmlPageProvider HtmlPageProvider
        {
            get { return (IHtmlPageProvider)Provider; }
        }


        public List<HtmlPage> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return HtmlPageProvider.Search(startIndex, lenght, ref totalItem, culture);
        }
        public List<HtmlPage> GetHtmlPageByCateId(int cateid, string culture)
        {
            return HtmlPageProvider.GetHtmlPageByCateId(cateid, culture);
        }

      
        public HtmlPage GetHtmlPageByShortName(HtmlPage model, string culture)
        {
            return HtmlPageProvider.GetHtmlPageByShortName(model, culture);
        }
        public List<HtmlPage> GetOtherNews(int htmlid, string culture)
        {
            return HtmlPageProvider.GetOtherNews(htmlid, culture);
        }
        public HtmlPage HtmlPageGetByShortName(string shortname, string culture)
        {
            return HtmlPageProvider.HtmlPageGetByShortName(shortname, culture);
        }
        public void Add(HtmlPage model, string Culture)
        {
            HtmlPageProvider.Add(model, Culture);
        }
        public HtmlPage GetDetail(HtmlPage model)
        {
            return HtmlPageProvider.GetDetail(model);
        }
        public HtmlPageCategory GetHtmlPageCategoryGetByHtmlPageCategoryShortName(string HtmlPageCategoryShortName)
        {
            return HtmlPageProvider.GetHtmlPageCategoryGetByHtmlPageCategoryShortName(HtmlPageCategoryShortName);
        }
        public List<HtmlPage> GetHotNewsTop(int topcount, string culture)
        {
            return HtmlPageProvider.GetHotNewsTop(topcount, culture);
        }
        public List<HtmlPage> GetByShortNameCate(string name, int startIndex, int length, ref int total, string culture)
        {
            return HtmlPageProvider.GetByShortNameCate(name, startIndex, length, ref total, culture);
        }
    }
}