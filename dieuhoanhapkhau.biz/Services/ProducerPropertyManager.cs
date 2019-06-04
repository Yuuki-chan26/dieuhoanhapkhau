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
    public class ProducerPropertyManager : DataManagerBase<ProducerProperties>
    {
        public ProducerPropertyManager()
            : base()
        { }

        public ProducerPropertyManager(IDataProvider<ProducerProperties> provider)
            : base(provider)
        {
        }
        private IProducerPropertyProvider ProducerPropertyProvider
        {
            get { return (IProducerPropertyProvider)Provider; }
        }
        public List<ProducerProperties> GetByProducerId(int producerid, string culture)
        {
            return ProducerPropertyProvider.GetByProducerId(producerid, culture);
        }
        public ProducerProperties GetByShortName(ProducerProperties model, string culture)
        {
            return ProducerPropertyProvider.GetByShortName(model, culture);
        }
        public ProducerProperties GetByProducerId(ProducerProperties model, string culture)
        {
            return ProducerPropertyProvider.GetByProducerId(model, culture);
        }
        public List<ProducerProperties> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            return ProducerPropertyProvider.Search(startIndex, lenght, ref totalItem, culture);
        }
        public void Add(ProducerProperties model, string Culture)
        {
            ProducerPropertyProvider.Add(model, Culture);
        }
        public List<ProducerProperties> GetAllProducerProperty(string culture)
        {
            return ProducerPropertyProvider.GetAllProducerProperty(culture);
        }
    }
}
