using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IProducerProvider : IDataProvider<Producers>
    {
        List<Producers> GetAllActive(string culture);
        List<Producers> GetByPrdId(int productid, string culture);
        List<Producers> Search(int startIndex, int lenght, ref int totalItem, string culture);
        void Add(Producers model, string culture);
        List<Producers> GetAllProducer(string culture);
        List<Producers> GetListProducerByCateId(int cateid, int startIndex, int lenght, ref int totalItem, string culture);
    }
}
