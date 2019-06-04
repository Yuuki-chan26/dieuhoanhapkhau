using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dieuhoanhapkhau.web.Areas.Admin.Controllers;
using dieuhoanhapkhau.web.Helpers;

namespace dieuhoanhapkhau.web.Controllers
{
    public class HtmlPagesController : ControllerBase
    {
        
        public ActionResult ShowListNew(string shortname, string shortnamehtml)
        {

            var cate = ServiceFactory.HtmlPageCategoryManager.GetByShortName(shortname);
            var categories = ServiceFactory.HtmlPageCategoryManager.GetAllActiveByParentId(cate.ParentId, Culture);

            var listhtmlpage = ServiceFactory.HtmlPageManager.GetHtmlPageByCateId(cate.HtmlPageCategoryId, Culture);
            if (cate != null)
            {
                cate.ListPage = listhtmlpage;
                if (cate.ListPage.Count > 0)
                {

                    if (String.IsNullOrEmpty(shortnamehtml))
                    {
                        ViewBag.Keywords = cate.HtmlPageCategoryKeyword + " - " + cate.ListPage[0].HtmlPageKeyword;
                        ViewBag.Desciption = cate.HtmlPageCategoryDescription + " - " + cate.ListPage[0].HtmlPageDescription;
                    }
                    else
                    {
                        foreach (HtmlPage item in cate.ListPage)
                        {
                            if (item.HtmlPageShortName == shortnamehtml)
                            {
                                ViewBag.Keywords = cate.HtmlPageCategoryKeyword + " - " + item.HtmlPageKeyword;
                                ViewBag.Desciption = cate.HtmlPageCategoryDescription + " - " + item.HtmlPageDescription;
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Keywords = cate.HtmlPageCategoryKeyword;
                    ViewBag.Desciption = cate.HtmlPageCategoryDescription;
                }
            }
            ViewBag.ListCates = categories;
            ViewBag.ShortNameHtml = shortnamehtml;
            return View(cate);
        }
        public ActionResult Detail(string category, string shortname, int htmlid)
        {
            var list = ServiceFactory.HtmlPageCategoryManager.ListAllNewsCategory(Culture);
            var Get = ServiceFactory.HtmlPageManager.GetHtmlPageCategoryGetByHtmlPageCategoryShortName(category);
            var HtmlPageCategoryId = Get.HtmlPageCategoryId;
            var data = ServiceFactory.HtmlPageManager.GetDetail(new HtmlPage { HtmlPageId = htmlid, HtmlPageShortName = shortname, HtmlPageCategoryId = HtmlPageCategoryId });
            var othernews = ServiceFactory.HtmlPageManager.GetOtherNews(data.HtmlPageId, Culture);
            if (data.HtmlPageOther != null)
            {
                var arr = data.HtmlPageOther.Split('/');
                foreach (var it in arr)
                {
                    if (it != null || it != "")
                    {
                        var itNew = ServiceFactory.HtmlPageManager.Get(new HtmlPage());
                        if (itNew != null)
                        {
                            othernews.Add(itNew);
                        }
                    }
                }
            }
            ViewBag.ListOthers = othernews;
            ViewBag.categoryshortname = category;
            ViewBag.Keywords = data.HtmlPageKeyword;
            ViewBag.Desciption = data.HtmlPageDescription;
            ViewBag.MetaOGImage = data.HtmlPageImage;
            return View(data);
        }
    }
}