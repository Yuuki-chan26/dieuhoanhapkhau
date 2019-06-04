using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using dieuhoanhapkhau.web.Filters;
using dieuhoanhapkhau.web.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace dieuhoanhapkhau.web.Controllers
{

    public class HomeController : ControllerBase
    {

        public ActionResult Index()
        {
            var total = 0;
            var data = ServiceFactory.SlideBannerManager.SelectTop(5, Culture);
            ViewData["ListSlide"] = data;
            var listhotproudct = ServiceFactory.ProductManager.GetListHotProduct(Culture);
            ViewData["ListHotProduct"] = listhotproudct;
            var listhotnews = ServiceFactory.HtmlPageManager.GetHotNewsTop(1000,Culture);
            ViewData["ListHotNews"] = listhotnews;
            var listhuongdan = ServiceFactory.HtmlPageCategoryManager.GetAllActiveByShortName("huong-dan", Culture);
            ViewData["ListHuongDan"] = listhuongdan;
            var services = ServiceFactory.HtmlPageCategoryManager.GetAllActiveByShortName("chinh-sach", Culture);
            ViewData["Services"] = services;
            var listProjects = ServiceFactory.HtmlPageManager.GetByShortNameCate("du-an-da-thuc-hien", 0, 5, ref total, Culture);
            ViewData["ListProjects"] = listProjects;
            var listCate = ServiceFactory.ProductCategoryManager.GetByParentId(0);
            ViewBag.ListCate = listCate;

            return View();
        }
        
        public ActionResult About(string shortname, string shortnamehtml)
        {
            ViewBag.Message = "Your application description page.";

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
        [HttpGet]
        public ActionResult Contact(string shortname, string shortnamehtml)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Keywords = "liên hệ, thông tin liên hệ, Hyundai An Phú";
            ViewBag.Desciption = "Thông tin liên hệ của Hyundai An Phú - Đại lý ủy quyền của Hyundai Thành Công ! Bạn cần đóng góp ý kiến hãy vào đây.";
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string Subject, string Name, string Email, string Address, string Phone, string Content)
        {
            try
            {
                SendmailLienHe(Subject, Name, Email, Phone, Address, Content);
                //SendMailToCustomer(Email);
            }
            catch (Exception e)
            { }

            return RedirectToAction("ContactSuccess");
        }
        public void SendmailLienHe(string subject, string name, string email, string phone, string address, string content)
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
            strChuoi += "<div style=\"width: 700px; height: 120px;\"> <h3><b>Mail ý kiến khách hàng</b></h3> <div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px; font-weight: bold;\">Xin chào bạn <br /> </div> ";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Chúng tôi đã nhận được ý kiến đóng góp của khách hàng \"" + strName + "\" với nội dung sau:<br /> </div>";
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
            var companyinfo = ServiceFactory.CompanyManager.Get(new Company { CompanyId = 1 });
            string emailsend = ConfigurationManager.AppSettings["emailsend"];
            string pass = ConfigurationManager.AppSettings["pass"];
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(companyinfo.CompanyMail);
            mail.From = new MailAddress(ContactEmail, "Điều hòa nhập khẩu", System.Text.Encoding.UTF8);
            mail.Subject = "Mail đóng góp ý kiến của khách hàng " + name;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = Encoding.GetEncoding("utf-8");

            mail.IsBodyHtml = true;
            mail.Body = strChuoi;

            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpClient client = new SmtpClient();

            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential();
            mailAuthentication.UserName = emailsend;//testmailidocnet
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
            catch (Exception e)
            {

                return;
            }
        }
        public void SendMail(string ContactEmail, string strChuoi)
        {
            var companyinfo = ServiceFactory.CompanyManager.Get(new Company { CompanyId = 1 });
            string emailsend = ConfigurationManager.AppSettings["emailsend"];
            string pass = ConfigurationManager.AppSettings["pass"];
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress(emailsend, "Điều hòa nhập khẩu", System.Text.Encoding.UTF8);
            mail.To.Add(companyinfo.CompanyMail);
            mail.Subject = "Đăng ký nhận tin từ email";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = Encoding.GetEncoding("utf-8");

            mail.IsBodyHtml = true;
            mail.Body = strChuoi;

            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpClient client = new SmtpClient();

            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential();
            mailAuthentication.UserName = emailsend;//testmailidocnet
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
        [HttpPost]
        public ActionResult SendMailTinTuc(string Email)
        {
            try
            {
                SendmailDangKy(Email);
                SendMailToCustomer(Email);
            }
            catch (Exception)
            { }
            return Json(new { Successful = true });
        }
        public void SendmailDangKy(string email)
        {
            string strChuoi = "";
            strChuoi += "<div style=\"width: 700px; padding:5px 0 10px 0; overflow: hidden;\">";
            strChuoi += strHeader(email);
            strChuoi += "</div>";
            SendMail(email, strChuoi);
        }
        public string strHeader(string email)
        {
            string strChuoi = "";
            strChuoi += "<div style=\"width: 700px; height: 120px;\"> <h3><b> Đăng ký nhận tin từ email </b></h3> <div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px; font-weight: bold;\">Xin chào bạn <br /> </div> ";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Chúng tôi đã nhận được thông tin đăng ký nhận tin của bạn " + email + "</div>";
            strChuoi += "<div style=\"width: 650px; clear: both; height: 25px; float: left; padding: 8px 0px 0px 25px;\">Trân trọng!<br /> </div>";

            strChuoi += "</div>";
            return strChuoi;
        }
        public void SendMailToCustomer(string ContactEmail)
        {
            var companyinfo = ServiceFactory.CompanyManager.Get(new Company { CompanyId = 1 });

            string emailsend = ConfigurationManager.AppSettings["emailsend"];
            string pass = ConfigurationManager.AppSettings["pass"];

            string strChuoi = "";
            strChuoi += "<div style=\"width: 700px; padding:5px 0 10px 0; overflow: hidden;\">";
            strChuoi += "<div style=\"width: 700px\"> <h3><b> Cám ơn Quý Khách Hàng đã quan tâm đến Điều hòa nhập khẩu. </b></h3> <div style=\"width: 650px; clear: both; float: left; padding: 8px 0px 0px 25px; font-weight: bold;\">Những tin tức/ chương trình khuyến mãi mới nhất sẽ được gửi email đến quý khách trong thời gian tới. <br />Hoặc Quý khách có thể truy cập link sau để cập nhật tin tức được nhanh chóng nhất tại <a href=\"www.anphuhyundai.com.vn/vi/tin-tuc/tin-tuc-chung\">đây</a><br /><p>---</p><img src=\"www.anphuhyundai.com.vn/Images/logo-mail.png\" class=\"img-responsive\"><br />Showroom 1S: 5A Nguyễn Thị Minh Khai, P. Bến Nghé, Q. 1, TPHCM<br />Showroom 3S: Mai Chí Thọ, P. An Phú, Q. 2, TPHCM (khai trương Tháng 3/2019)<br />Hotline: <span style=\"color: red\">1900 988 978</span></div></div> ";
            strChuoi += "</div>";

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress(emailsend, "Điều hòa nhập khẩu", System.Text.Encoding.UTF8);
            mail.To.Add(ContactEmail);
            mail.Bcc.Add(emailsend);
            mail.Subject = "Cảm ơn đã đăng ký nhận tin từ email";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = Encoding.GetEncoding("utf-8");

            mail.IsBodyHtml = true;
            mail.Body = strChuoi;

            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpClient client = new SmtpClient();

            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential();
            mailAuthentication.UserName = emailsend;//testmailidocnet
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
            catch (Exception e)
            {

                return;
            }
        }
        public ActionResult ContactSuccess()
        {
            return View();
        }
        public ActionResult Buy()
        {
            return View();
        }
        public ActionResult Baogiavattu(string shortname, string shortnamehtml)
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
        [HttpPost]
        public ActionResult Search(FormCollection Fields)
        {
            int total = 0;
            string key = Fields["txtSearch"].ToString();
            
            var list = ServiceFactory.ProductManager.Searchkey(0, 1000, ref total, key);
            ViewBag.Key = key;

            return View(list);
        }
       
    }
}