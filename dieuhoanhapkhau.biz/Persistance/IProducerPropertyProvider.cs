using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IProducerPropertyProvider : IDataProvider<ProducerProperties>
    {
        List<ProducerProperties> GetByProducerId(int producerid, string culture);
        ProducerProperties GetByShortName(ProducerProperties model, string culture);
        List<ProducerProperties> Search(int startIndex, int lenght, ref int totalItem, string culture);
        void Add(ProducerProperties model, string culture);
        ProducerProperties GetByProducerId(ProducerProperties model, string culture);
        List<ProducerProperties> GetAllProducerProperty(string culture);
    }
}
