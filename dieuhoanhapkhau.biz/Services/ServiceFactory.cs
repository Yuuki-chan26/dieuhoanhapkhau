using dieuhoanhapkhau.biz.Persistance.SqlServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Services
{
    public static class ServiceFactory
    {

        static Hashtable services = new Hashtable();
        static ServiceFactory()
        {
            services.Add(typeof(CustomerManager), new CustomerManager(new CustomerProvider()));
            services.Add(typeof(UserManager), new UserManager(new UserProvider()));
            services.Add(typeof(SlideBannerManager), new SlideBannerManager(new SlideBanneProvider()));
            services.Add(typeof(HtmlPageManager), new HtmlPageManager(new HtmlPageProvider()));
            services.Add(typeof(HtmlPageCategoryManager), new HtmlPageCategoryManager(new HtmlPageCategoryProvider()));
            services.Add(typeof(OrderManager), new OrderManager(new OrderProvider()));
            services.Add(typeof(ImageProductManager), new ImageProductManager(new ImageProductProvider()));
            services.Add(typeof(OrderDetailManager), new OrderDetailManager(new OrderDetailProvider()));
            services.Add(typeof(ProducerPropertyManager), new ProducerPropertyManager(new ProducerPropertyProvider()));
            services.Add(typeof(ProductCategoryManager), new ProductCategoryManager(new ProductCategoryProvider()));
            services.Add(typeof(ProductManager), new ProductManager(new ProductProvider()));
            
            services.Add(typeof(ProducerManager), new ProducerManager(new ProducerProvider()));
            //services.Add(typeof(AppDicDomainManager), new AppDicDomainManager(new AnPhu.biz.Persistance.SqlServer.AppDicDomainProvider()));
            services.Add(typeof(ProductPropertyManager), new ProductPropertyManager(new ProductPropertyProvider()));
            services.Add(typeof(LocationDiscountManager), new LocationDiscountManager(new LocationDiscountProvider()));
            services.Add(typeof(LocationManager), new LocationManager(new LocationProvider()));
            //services.Add(typeof(PriceOptionManager), new PriceOptionManager(new AnPhu.biz.Persistance.SqlServer.PriceOptionProvider()));
            //services.Add(typeof(AppDicDomainManager), new AppDicDomainManager(new AnPhu.biz.Persistance.SqlServer.AppDicDomainProvider()));
            //services.Add(typeof(QuestionManager), new QuestionManager(new QuestionProvider()));
            services.Add(typeof(CompanyManager), new CompanyManager(new CompanyProvider()));
            //services.Add(typeof(HitCounterManager), new HitCounterManager(new HitCounterProvider()));
            //services.Add(typeof(AdBannerLeftManager), new AdBannerLeftManager(new AdBannerLeftProvider()));
            //services.Add(typeof(AdBannerRightManager), new AdBannerRightManager(new AdBannerRightProvider()));
            //services.Add(typeof(NewBannerLeftManager), new NewBannerLeftManager(new NewBannerLeftProvider()));
            //services.Add(typeof(NewBannerRightManager), new NewBannerRightManager(new NewBannerRightProvider()));
            //services.Add(typeof(PopupAdvertisementManager), new PopupAdvertisementManager(new PopupAdvertisementProvider()));
        }
        

        public static UserManager UserManager
        {
            get
            {
                return (UserManager)services[typeof(UserManager)];
            }
            set
            {
                services[typeof(UserManager)] = value;
            }
        }
        public static SlideBannerManager SlideBannerManager
        {
            get
            {
                return (SlideBannerManager)services[typeof(SlideBannerManager)];
            }
            set
            {
                services[typeof(SlideBannerManager)] = value;
            }
        }
        public static HtmlPageManager HtmlPageManager
        {
            get
            {
                return (HtmlPageManager)services[typeof(HtmlPageManager)];
            }
            set
            {
                services[typeof(HtmlPageManager)] = value;
            }
        }
        public static HtmlPageCategoryManager HtmlPageCategoryManager
        {
            get
            {
                return (HtmlPageCategoryManager)services[typeof(HtmlPageCategoryManager)];
            }
            set
            {
                services[typeof(HtmlPageCategoryManager)] = value;
            }
        }
        public static ProducerManager ProducerManager
        {
            get
            {
                return (ProducerManager)services[typeof(ProducerManager)];
            }
            set
            {
                services[typeof(ProducerManager)] = value;
            }
        }
        //public static NewsCategoryManager NewsCategoryManager
        //{
        //    get
        //    {
        //        return (NewsCategoryManager)services[typeof(NewsCategoryManager)];
        //    }
        //    set
        //    {
        //        services[typeof(NewsCategoryManager)] = value;
        //    }
        //}
        //public static NewsManager NewsManager
        //{
        //    get
        //    {
        //        return (NewsManager)services[typeof(NewsManager)];
        //    }
        //    set
        //    {
        //        services[typeof(NewsManager)] = value;
        //    }
        //}
        public static ProductCategoryManager ProductCategoryManager
        {
            get
            {
                return (ProductCategoryManager)services[typeof(ProductCategoryManager)];
            }
            set
            {
                services[typeof(ProductCategoryManager)] = value;
            }
        }
        public static ProductManager ProductManager
        {
            get
            {
                return (ProductManager)services[typeof(ProductManager)];
            }
            set
            {
                services[typeof(ProductManager)] = value;
            }
        }
        public static OrderDetailManager OrderDetailManager
        {
            get
            {
                return (OrderDetailManager)services[typeof(OrderDetailManager)];
            }
            set
            {
                services[typeof(OrderDetailManager)] = value;
            }
        }
        public static OrderManager OrderManager
        {
            get
            {
                return (OrderManager)services[typeof(OrderManager)];
            }
            set
            {
                services[typeof(OrderManager)] = value;
            }
        }
        public static ProductPropertyManager ProductPropertyManager
        {
            get
            {
                return (ProductPropertyManager)services[typeof(ProductPropertyManager)];
            }
            set
            {
                services[typeof(ProductPropertyManager)] = value;
            }
        }
        public static CustomerManager CustomerManager
        {
            get
            {
                return (CustomerManager)services[typeof(CustomerManager)];
            }
            set
            {
                services[typeof(CustomerManager)] = value;
            }
        }
        public static ProducerPropertyManager ProducerPropertyManager
        {
            get
            {
                return (ProducerPropertyManager)services[typeof(ProducerPropertyManager)];
            }
            set
            {
                services[typeof(ProducerPropertyManager)] = value;
            }
        }
        public static ImageProductManager ImageProductManager
        {
            get
            {
                return (ImageProductManager)services[typeof(ImageProductManager)];
            }
            set
            {
                services[typeof(ImageProductManager)] = value;
            }
        }
        public static LocationDiscountManager LocationDiscountManager
        {
            get
            {
                return (LocationDiscountManager)services[typeof(LocationDiscountManager)];
            }
            set
            {
                services[typeof(LocationDiscountManager)] = value;
            }
        }
        public static LocationManager LocationManager
        {
            get
            {
                return (LocationManager)services[typeof(LocationManager)];
            }
            set
            {
                services[typeof(LocationManager)] = value;
            }
        }
        //public static PriceOptionManager PriceOptionManager
        //{
        //    get
        //    {
        //        return (PriceOptionManager)services[typeof(PriceOptionManager)];
        //    }
        //    set
        //    {
        //        services[typeof(PriceOptionManager)] = value;
        //    }
        //}
        public static CompanyManager CompanyManager
        {
            get
            {
                return (CompanyManager)services[typeof(CompanyManager)];
            }
            set
            {
                services[typeof(CompanyManager)] = value;
            }
        }
        public static T GetService<T>()
        {
            foreach (var service in services.Values)
            {
                if (service is T)
                {
                    return (T)service;
                }
            }
            return default(T);
        }




    }
}
