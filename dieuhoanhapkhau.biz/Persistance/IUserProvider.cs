using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idocNet.Core.Data;
using dieuhoanhapkhau.biz.Models;

namespace dieuhoanhapkhau.biz.Persistance
{
    public interface IUserProvider :IDataProvider<User>
    {
        User GetByEmail(string email);
        User GetByUserName(string username);
        bool ValidateUser(string applicationId, string userName, string password);
        bool ChangePassword(string applicationId, string userName, string oldPassword, string newPassword);
        bool ChangePassword(string applicationId, string userName, string newPassword);
        bool ResetPassword(string applicationId, string userName);
        bool ChangeEmail(string applicationId, string userId, string email);
        bool UpdateLoginLogout(string applicationId, string userId, bool flag);
        void Promote(string userId);
        void Demote(string userId);
        void Delete(string userId, bool status);
    }
}
