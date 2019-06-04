using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Services
{
    public class CompanyManager : DataManagerBase<Company>
    {
        public CompanyManager(Persistance.SqlServer.CompanyProvider companyProvider)
            : base()
        { }

        public CompanyManager(IDataProvider<Company> provider)
            : base(provider)
        {
        }

        private IProductImageProvider CompanyProvider
        {
            get { return (IProductImageProvider)Provider; }
        }

    }
}
