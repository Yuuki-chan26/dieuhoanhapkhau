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
    public class ProducerManager : DataManagerBase<Producers>
    {
        public ProducerManager()
            : base()
        { }

        public ProducerManager(IDataProvider<Producers> provider)
            : base(provider)
        {
        }

        private IProducerProvider ProducerProvider
        {
            get { return (IProducerProvider)Provider; }
        }
        public List<Producers> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return ProducerProvider.Search(startIndex, lenght, ref totalItem, culture);
        }

        public void Add(Producers model, string Culture)
        {
            ProducerProvider.Add(model, Culture);
        }
        public List<Producers> GetListProducerByCateId(int catenewsid, int startIndex, int lenght, ref int totalItem, string culture)
        {
            return ProducerProvider.GetListProducerByCateId(catenewsid, startIndex, lenght, ref totalItem, culture);
        }
        public List<Producers> GetByPrdId(int productid, string culture)
        {
            return ProducerProvider.GetByPrdId(productid, culture);
        }
        public List<Producers> GetAllActive(string culture)
        {
            return ProducerProvider.GetAllActive(culture);
        }
        public List<Producers> GetAllProducer(string culture)
        {
            return ProducerProvider.GetAllProducer(culture);
        }
    }
}
