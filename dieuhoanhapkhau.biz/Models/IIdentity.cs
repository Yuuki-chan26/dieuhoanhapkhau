using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Models
{
    public interface IIdentity
    {
        int GetIdentityUserRole();
        string GetIdentityName();
        string GetIdentityUserId();
    }
}
