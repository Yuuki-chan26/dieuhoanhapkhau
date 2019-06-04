using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using dieuhoanhapkhau.biz.Services;
using dieuhoanhapkhau.web.Controllers;
using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Extensions;
using dieuhoanhapkhau.web.Helpers;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using dieuhoanhapkhau.web.Filters;

namespace dieuhoanhapkhau.web.Areas.Admin.Controllers
{
    public class HtmlPageController : ControllerBase
    {
        private const int Pagesize = 20;
        #region Admin
        //
        // GET: /HtmlPage/
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult SearchCategory()
        {
            var totalItem = 0;
            var list = ServiceFactory.HtmlPageCategoryManager.Search(0, 1000, ref totalItem, Culture);
            return View(list);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult UpdateCate(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.HtmlPageCategoryManager.Get(new HtmlPageCategory { HtmlPageCategoryId = (int)Id });
                if (obj != null)
                {
                    var categories = ServiceFactory.HtmlPageCategoryManager.GetAllHtmlPageCategory(Culture);
                    ViewBag.Categories = new SelectList(categories, "HtmlPageCategoryId", "HlevelTitle");
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCate(HtmlPageCategory model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.HtmlPageCategoryManager.Get(new HtmlPageCategory { HtmlPageCategoryId = model.HtmlPageCategoryId });
                if (obj != null)
                {
                    try
                    {
                        model.HtmlPageCategoryShortName = model.HtmlPageCategoryTitle.ToUrlSegment(250).ToLower();

                        ServiceFactory.HtmlPageCategoryManager.Update(model, obj);

                        return RedirectToAction("SearchCategory", "HtmlPage");
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            var categories = ServiceFactory.HtmlPageCategoryManager.GetAllHtmlPageCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "HtmlPageCategoryId", "HlevelTitle");
            ViewBag.IsEdit = true;
            return View(model);
        }

        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult CreateCate()
        {
            HtmlPageCategory data = new HtmlPageCategory();
            var categories = ServiceFactory.HtmlPageCategoryManager.GetAllHtmlPageCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "HtmlPageCategoryId", "HlevelTitle");
            return View("UpdateCate", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCate(HtmlPageCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.HtmlPageCategoryShortName = model.HtmlPageCategoryTitle.ToUrlSegment(250).ToLower();
                    model.CreateBy = CurrentUser.Name;
                    ServiceFactory.HtmlPageCategoryManager.Add(model, Culture);

                    return RedirectToAction("SearchCategory", "HtmlPage");
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            var categories = ServiceFactory.HtmlPageCategoryManager.GetAllHtmlPageCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "HtmlPageCategoryId", "HlevelTitle");
            return View("UpdateCate", model);
        }

        //HtmlPage
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Search()
        {
            var totalItem = 0;
            var list = ServiceFactory.HtmlPageManager.Search(0, 1000, ref totalItem, Culture);
            return View(list);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Create()
        {
            HtmlPage data = new HtmlPage();
            var categories = ServiceFactory.HtmlPageCategoryManager.GetAllHtmlPageCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "HtmlPageCategoryId", "HlevelTitle");
            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HtmlPage model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.HtmlPageShortName = model.HtmlPageTitle.ToUrlSegment(250).ToLower();
                    model.CreateBy = CurrentUser.Name;
                    ServiceFactory.HtmlPageManager.Add(model, Culture);

                    return RedirectToAction("Search", "HtmlPage");
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            var categories = ServiceFactory.HtmlPageCategoryManager.GetAllHtmlPageCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "HtmlPageCategoryId", "HlevelTitle");
            return View("Update", model);
        }
        [RequireAuthorization(UserRole.Admin)]
        [HttpGet]
        public ActionResult Update(int? Id)
        {
            if (Id.HasValue)
            {
                var obj = ServiceFactory.HtmlPageManager.Get(new HtmlPage { HtmlPageId = (int)Id });
                if (obj != null)
                {
                    var categories = ServiceFactory.HtmlPageCategoryManager.GetAllHtmlPageCategory(Culture);
                    ViewBag.Categories = new SelectList(categories, "HtmlPageCategoryId", "HlevelTitle");
                    ViewBag.IsEdit = true;
                    return View(obj);
                }
            }
            return null;
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(HtmlPage model)
        {
            if (ModelState.IsValid)
            {
                var obj = ServiceFactory.HtmlPageManager.Get(new HtmlPage { HtmlPageId = model.HtmlPageId });
                if (obj != null)
                {
                    try
                    {
                        model.HtmlPageShortName = model.HtmlPageTitle.ToUrlSegment(250).ToLower();

                        ServiceFactory.HtmlPageManager.Update(model, obj);

                        return RedirectToAction("Search", "HtmlPage");
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
            }
            var categories = ServiceFactory.HtmlPageCategoryManager.GetAllHtmlPageCategory(Culture);
            ViewBag.Categories = new SelectList(categories, "HtmlPageCategoryId", "HlevelTitle");
            ViewBag.IsEdit = true;
            return View(model);
        }
        public ActionResult AddProp(string name = "")
        {
            ViewData["PropName"] = name.Trim();
            ViewData["PropId"] = name.Trim().ToUrlSegment();
            return PartialView("AddProp");
        }

        [RequireAuthorization(UserRole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ServiceFactory.HtmlPageManager.Remove(new HtmlPage { HtmlPageId = id });
            return RedirectToAction("Search");
        }
        #endregion
        #region User
        public ActionResult ContactSuccess()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ServicesContact(string shortname, int htmlpageid = 0)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Keywords = "liên hệ, thông tin liên hệ, Hyundai An Phú";
            ViewBag.Desciption = "Thông tin hẹn lịch dịch vụ của Hyundai An Phú - Đại lý ủy quyền của Hyundai Thành Công ! Bạn cần hẹn lịch dịch vụ hãy vào đây.";
            return View();
        }
        [HttpPost]
        public ActionResult ServicesContact(string Subject, string Name, string Email, string Address, string Phone, string Content)
        {
            try
            {
                SendmailHenlichdichvu(Subject, Name, Email, Phone, Address, Content);
            }
            catch (Exception)
            { }

            return RedirectToAction("ContactSuccess");
        }
        [HttpGet]
        public ActionResult Detail(string shortname)
        {
            var data = ServiceFactory.HtmlPageManager.GetHtmlPageByShortName(new HtmlPage { HtmlPageShortName = shortname }, Culture);
            if (data != null)
            {
                ViewBag.Keywords = data.HtmlPageKeyword;
                ViewBag.Desciption = data.HtmlPageDescription;
                return View(data);
            }
            return null;
        }

        [HttpGet]
        public ActionResult Services(string shortname)
        {
            var obj = ServiceFactory.HtmlPageCategoryManager.GetAllActiveByShortName(shortname, Culture);
            HtmlPageCategory htmlpage = new HtmlPageCategory();
            if (obj.Count > 0)
            {
                return RedirectToAction("ServicesSub", new { HtmlPageCateId = obj[0].HtmlPageCategoryId, shortname = obj[0].HtmlPageCategoryShortName, htmlpageid = 1 });
            }
            else
            {
                return View();
            }

        }
        public ActionResult ServicesSub(string shortname, string shortnamehtml)
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

        public ActionResult About(string shortname)
        {
            var obj = ServiceFactory.HtmlPageCategoryManager.GetAllActiveByShortName(shortname, Culture);
            if (obj.Count > 0)
            {
                return RedirectToAction("AboutSub", new { HtmlPageCateId = obj[0].HtmlPageCategoryId, shortname = obj[0].HtmlPageCategoryShortName });
            }
            else
            {
                return View();
            }
        }
        public ActionResult AboutSub(string shortname, string shortnamehtml)
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

        public ActionResult Support(string shortname)
        {
            var obj = ServiceFactory.HtmlPageCategoryManager.GetAllActiveByShortName(shortname, Culture);
            if (obj.Count > 0)
            {
                return RedirectToAction("SupportSub", new { HtmlPageCateId = obj[0].HtmlPageCategoryId, shortname = obj[0].HtmlPageCategoryShortName });
            }
            else
            {
                return View();
            }
        }
        public ActionResult SupportSub(int HtmlPageCateId, string shortname, int htmlpageid = 0)
        {
            var cate = ServiceFactory.HtmlPageCategoryManager.Get(new HtmlPageCategory { HtmlPageCategoryId = HtmlPageCateId });
            var categories = ServiceFactory.HtmlPageCategoryManager.GetAllActiveByParentId(cate.ParentId, Culture);

            var listhtmlpage = ServiceFactory.HtmlPageManager.GetHtmlPageByCateId(HtmlPageCateId, Culture);
            if (cate != null)
            {
                cate.ListPage = listhtmlpage;
                if (cate.ListPage.Count > 0)
                {

                    if (htmlpageid == 0)
                    {
                        ViewBag.Keywords = cate.HtmlPageCategoryKeyword + " - " + cate.ListPage[0].HtmlPageKeyword;
                        ViewBag.Desciption = cate.HtmlPageCategoryDescription + " - " + cate.ListPage[0].HtmlPageDescription;
                    }
                    else
                    {
                        foreach (HtmlPage item in cate.ListPage)
                        {
                            if (item.HtmlPageId == htmlpageid)
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
            ViewBag.HtmlPageId = htmlpageid;
            return View(cate);
        }

        [HttpPost]
        public void SendmailHenlichdichvu(string subject, string name, string email, string phone, string address, string content)
        {
            string strChuoi = "";
            strChuoi += "<div style=\"width: 700px; padding:5px 0 10px 0; overflow: hidden;\">";
            strChuoi += strHeader(name, subject, content, phone, email, address);
            strChuoi += "</div>";
            Sendmail(name, email, strChuoi);
        }
        public string strHeader(string strName, string subject, string content, string phone, string email, string address)
        {
            string strChuoi = "";
            strChuoi += "<div style=\"width: 700px; height: 120px;\"> <h3><b>Mail hẹn lịch sửa chữa</b></h3> <div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px; font-weight: bold;\">Xin chào bạn <br /> </div> ";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Chúng tôi đã nhận được hẹn lịch sửa chữa của khách hàng \"" + strName + "\" với nội dung sau:<br /> </div>";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Số điện thoại liên hệ: <b>" + phone + "</b> </div>";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Email đăng ký: <b>" + email + "</b> </div>";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Địa chỉ: " + address + "</div>";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Tiêu đề: <b>" + subject + "</b> </div>";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Nội dung: " + content + "</div>";
            // strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Chúng tôi sẽ sớm liên hệ với bạn:<br /> </div>";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Trân trọng!<br /> </div>";

            strChuoi += "</div>";
            return strChuoi;
        }
        public void Sendmail(string name, string ContactEmail, string strChuoi)
        {
            string mailcontact = ConfigurationManager.AppSettings["emailcontact"];
            string mailsend = ConfigurationManager.AppSettings["mailsend"];
            string pass = ConfigurationManager.AppSettings["pass"];
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(mailcontact);
            mail.From = new MailAddress(ContactEmail, "Hyundai An Phú", System.Text.Encoding.UTF8);
            mail.Subject = "Mail hẹn lịch sửa chữa của khách hàng " + name;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = Encoding.GetEncoding("utf-8");

            mail.IsBodyHtml = true;
            mail.Body = strChuoi;

            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpClient client = new SmtpClient();

            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential();
            mailAuthentication.UserName = mailsend;//testmailidocnet
            mailAuthentication.Password = pass;//defaultidocnet

            client.Port = 587;

            client.Host = "smtp.gmail.com";

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = mailAuthentication;
            try
            {
                client.Send(mail);
            }
            catch
            {
                return;
            }
        }
        #endregion
    }
}