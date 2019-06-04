using dieuhoanhapkhau.biz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IAuthenticationProvider
    {
        User GetByEmail(string email);
        AuthenticationResult ValidateUser(string applicationId, string email, string password);
        AuthenticationResult ValidateUser(string applicationId, string userId);
    }
}
