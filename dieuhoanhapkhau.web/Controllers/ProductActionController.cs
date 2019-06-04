using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Services;
using dieuhoanhapkhau.web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TCG.QLTV.Web.Models;

namespace dieuhoanhapkhau.web.Controllers
{
    public class ProductActionController : ControllerBase
    {
        // GET: ProductAction
        [HttpGet]
        public ActionResult OrderDetail(int cityid = 0)
        {
            var listLocationDis = ServiceFactory.LocationDiscountManager.GetAllActive(Culture);
            ViewBag.ListLocationDis = new SelectList(listLocationDis, "LocationDiscountId", "LocationDiscountName");
            var listLocations = ServiceFactory.LocationManager.GetAllActiveByParentId(cityid);
            ViewBag.Locations = new SelectList(listLocations, "LocationId", "LocationName");
            ShoppingCartModels model = new ShoppingCartModels();
            model.Cart = (Carts)System.Web.HttpContext.Current.Session["Cart"];

            return View(model);
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quanlity, string url)
        {
            var product = ServiceFactory.ProductManager.Get(new Product { ProductId = id });
            if (product != null && product.Quality >= quanlity)
            {
                Carts objCart = (Carts)System.Web.HttpContext.Current.Session["Cart"];
                if (objCart == null)
                {
                    objCart = new Carts();
                }
                CartItem item = new CartItem()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Quantity = quanlity,
                    ProductImage = product.ProductImage,
                    Price = product.ProductPrice,
                    Total = product.ProductPrice,
                    ShortName = product.ProductCode

                };
                objCart.AddToCart(item);

                System.Web.HttpContext.Current.Session["Cart"] = objCart;
                //return RedirectToAction(url);
                return Json(new { Success = true, Flag = "0", Cart = objCart });
            }
            else
            {
                ViewBag.Message = "Sản phẩm chỉ còn " + product.Quality;
                //return RedirectToAction(url);
                return Json(new { Success = false, Flag = "1", Message = "Sản phẩm " + product.ProductName + " còn  " + product.Quality + " sản phẩm" });
            }

        }
        [HttpPost]
        public ActionResult UpdateCart(int proid, int quanlity)
        {
            try
            {
                Carts objCart = (Carts)System.Web.HttpContext.Current.Session["Cart"];
                var product = ServiceFactory.ProductManager.Get(new Product { ProductId = proid });
                if (product.Quality >= quanlity)
                {
                    if (objCart != null)
                    {
                        objCart.UpdateCart(proid, quanlity);
                        System.Web.HttpContext.Current.Session["Cart"] = objCart;
                    }
                    return Json(new { Success = true, Flag = "0", Cart = objCart });
                    //return RedirectToAction(url);
                }
                else
                {
                    //return RedirectToAction(url);
                    return Json(new { Success = false, Flag = "1", Message = "Sản phẩm " + product.ProductName + " còn  " + product.Quality + " sản phẩm" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.StackTrace.ToString() });
            }


        }
        public ActionResult Remove(int id)
        {
            Carts objCart = (Carts)System.Web.HttpContext.Current.Session["Cart"];
            if (objCart != null)
            {
                objCart.RemoveFromCart(id);
                System.Web.HttpContext.Current.Session["Cart"] = objCart;
            }
            return RedirectToAction("OrderDetail", "ProductAction");
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Order(string CustomerName, string Phone, string Email, string Address, string City, string District, string Message)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            int idOrder = 0;
            int customer = 0;
            double total = 0;
            ShoppingCartModels model = new ShoppingCartModels();
            model.Cart = (Carts)System.Web.HttpContext.Current.Session["Cart"];
            var totalMoney = 0;
            var city = ServiceFactory.LocationDiscountManager.Get(new LocationDiscount { LocationDiscountId = Convert.ToInt32(City) });
            var district = ServiceFactory.LocationManager.Get(new Location {  LocationId = Convert.ToInt32(District) });
            var customerFind = ServiceFactory.CustomerManager.GetByPhone(Phone);
            if (customerFind == null)
            {
                ServiceFactory.CustomerManager.Add(new Customers
                {
                    Name = CustomerName,
                    Phone = Phone,
                    Email = Email,
                    Address = Address + "," + district.LocationName + ", " + city.LocationDiscountName
                });
                customer = ServiceFactory.CustomerManager.GetLastId();
                //Customers customer = ServiceFactory.CustomerManager.GetByPhone(Phone);
                string strChuoi = "<li>Đơn đặt hàng:</li>";
                if (model.Cart != null)
                {

                    Orders orders = new Orders
                    {
                        CustomerId = customer,
                        Note = Message,
                        IsActive = 0
                    };
                    ServiceFactory.OrderManager.Add(orders);
                    idOrder = ServiceFactory.OrderManager.GetLastId();
                    foreach (var item in model.Cart.ListItem)
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            ProductId = item.ProductId,
                            Quality = item.Quantity,
                            Price = item.Price,
                            OrderId = idOrder
                        };
                        var product = ServiceFactory.ProductManager.Get(new Product { ProductId = item.ProductId });
                        Product newPro = ServiceFactory.ProductManager.Get(new Product { ProductId = item.ProductId });
                        newPro.Quality -= item.Quantity;
                        ServiceFactory.ProductManager.Update(newPro, product);
                        total += (double)item.Price * item.Quantity;

                        ServiceFactory.OrderDetailManager.Add(orderDetail);
                        strChuoi += "<li>Tên sản phẩm: " + item.ProductName + "</li> ";
                        strChuoi += "<li> Giá: " + item.Price.ToString("#,##") + " đ</li> ";
                        strChuoi += "<li> Số lượng: " + item.Quantity + "</li> ";
                    }
                }

                var order = ServiceFactory.OrderManager.Get(new Orders { OrderId = idOrder });
                var orderNew = ServiceFactory.OrderManager.Get(new Orders { OrderId = idOrder });
                orderNew.ToTalMoney = total;
                ServiceFactory.OrderManager.Update(orderNew, order);
                strChuoi += "<li>Tổng tiền: " + total.ToString("#,##") + " đ </li>";
                SendEmail(Email, strChuoi);
                System.Web.HttpContext.Current.Session["Cart"] = null;

                return Json(new { Success = true, Title = "Đã đặt hàng thành công!", CheckSuccess = "1", strUrl = Url.Action("OrderSuccess", "ProductAction") });
                //return RedirectToAction("OrderSuccess", "ProductAction");
            }
            else
            {
                string strChuoi = "<li>Đơn đặt hàng:</li>";
                if (model.Cart != null)
                {

                    Orders orders = new Orders
                    {
                        CustomerId = customerFind.CustomerId,
                        Note = Message,
                        IsActive = 0
                    };
                    ServiceFactory.OrderManager.Add(orders);
                    idOrder = ServiceFactory.OrderManager.GetLastId();
                    foreach (var item in model.Cart.ListItem)
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            ProductId = item.ProductId,
                            Quality = item.Quantity,
                            Price = item.Price,
                            OrderId = idOrder
                        };
                        total += (double)item.Price * item.Quantity;
                        ServiceFactory.OrderDetailManager.Add(orderDetail);
                        var product = ServiceFactory.ProductManager.Get(new Product { ProductId = item.ProductId });
                        Product newPro = ServiceFactory.ProductManager.Get(new Product { ProductId = item.ProductId });
                        newPro.Quality -= item.Quantity;
                        ServiceFactory.ProductManager.Update(newPro, product);
                        strChuoi += "<li>Tên sản phẩm: " + item.ProductName + "</li> ";
                        strChuoi += "<li> Giá: " + item.Price.ToString("#,##") + " đ</li> ";
                        strChuoi += "<li> Số lượng: " + item.Quantity + "</li> ";
                    }
                }
                var order = ServiceFactory.OrderManager.Get(new Orders { OrderId = idOrder });
                var orderNew = ServiceFactory.OrderManager.Get(new Orders { OrderId = idOrder });
                orderNew.ToTalMoney = total;
                ServiceFactory.OrderManager.Update(orderNew, order);
                strChuoi += "<li>Tổng tiền: " + total.ToString("#,##") + " đ </li>";
                SendEmail(Email, strChuoi);
                System.Web.HttpContext.Current.Session["Cart"] = null;
               
            }

            return Json(new { Success = true, Title = "Đã đặt hàng thành công!", CheckSuccess = "1", strUrl = Url.Action("OrderSuccess", "ProductAction") });
            //return RedirectToAction("OrderDetail", "ProductAction");
        }
        [HttpPost]
        public void SendEmail(string email, string strChuoi)
        {
            string emailsend = ConfigurationManager.AppSettings["email"];
            string pass = ConfigurationManager.AppSettings["password"];
            MailAddress mailCompany = new MailAddress("dieuhoanhapkhautest@gmail.com");
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.To.Add(mailCompany);
            mail.From = new MailAddress(emailsend, "Điều hòa nhập khẩu chính hãng");
            mail.Subject = "Thông tin đơn hàng đã đặt";
            mail.SubjectEncoding = Encoding.GetEncoding("utf-8");
            mail.BodyEncoding = Encoding.GetEncoding("utf-8");

            mail.IsBodyHtml = true;
            mail.Body = strChuoi;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            NetworkCredential mailAuthentication = new NetworkCredential();
            mailAuthentication.UserName = emailsend;
            mailAuthentication.Password = pass;

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
        public ActionResult GetDistrict(int cityid)
        {
            var data = ServiceFactory.LocationManager.GetAllActiveByParentId(cityid);
            return View(data);
        }
        [HttpGet]
        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}