using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class AuthenticationProvider : DataAccessBase, IAuthenticationProvider
    {
        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public AuthenticationResult ValidateUser(string applicationId, string email, string password)
        {
            throw new NotImplementedException();
        }
        public AuthenticationResult ValidateUser(string applicationId, string userId)
        {
            var userprovider = new UserProvider();
            var json = userprovider.GetLoginInfo(applicationId, userId);
            var ret = JsonSerializer.Deserialise<AuthenticationResult>(json);

            return ret;
            //throw new NotImplementedException();
        }

    }
}
