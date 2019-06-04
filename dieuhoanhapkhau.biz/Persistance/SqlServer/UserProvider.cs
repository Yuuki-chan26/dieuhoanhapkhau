using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idocNet.Core.Utils;
using idocNet.Core.Data.Entities;
using System.Data;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class UserProvider : DataAccessBase, IUserProvider
    {
        public List<User> GetAll(int startIndex, int count, ref int totalItems)
        {
            var comm = this.GetCommand("acc_UserGetAll");
            if (comm == null) return null;

            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<int>(this.Factory, "Count", count);
            var totalItemsParam = comm.AddParameter(this.Factory, "totalItems", DbType.Int32, null);
            totalItemsParam.Direction = ParameterDirection.Output;

            var dt = this.GetTable(comm);
            if (totalItemsParam.Value != DBNull.Value)
            {
                totalItems = Convert.ToInt32(totalItemsParam.Value);
            }
            return EntityBase.ParseListFromTable<User>(dt);
        }

        public User Get(User dummy)
        {
            //get user info by name, id or email
            var comm = this.GetCommand("acc_UserGet");
            if (comm == null)
            {
                return null;
            }

            comm.AddParameter<string>(this.Factory, "UserId", dummy.UserId);
            comm.AddParameter<string>(this.Factory, "UserName", dummy.UserName);
            comm.AddParameter<string>(this.Factory, "Email", dummy.Email);

            var dt = this.GetTable(comm);
            var list = EntityBase.ParseListFromTable<User>(dt);

            return (list != null && list.Count == 1) ? list[0] : null;
        }

        public User GetByEmail(string email)
        {
            var comm = this.GetCommand("acc_UserGetByEmail");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "Email", email);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<User>(dt).FirstOrDefault();
        }

        public User GetByUserName(string username)
        {
            var comm = this.GetCommand("acc_UserGetByName");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "UserName", username);
            var dt = this.GetTable(comm);
            var list = EntityBase.ParseListFromTable<User>(dt);
            return (list != null && list.Count == 1) ? list[0] : null;
        }

        public void Add(User item)
        {
            var comm = this.GetCommand("acc_UserAdd");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "UserId", item.UserId);
            comm.AddParameter<string>(this.Factory, "UserName", item.UserName);
            comm.AddParameter<string>(this.Factory, "Email", item.Email);
            comm.AddParameter<string>(this.Factory, "Password", item.Password);
            comm.AddParameter<string>(this.Factory, "PasswordSalt", item.PasswordSalt);
            comm.AddParameter<int>(this.Factory, "UserRole", item.intUserRole);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);

            comm.SafeExecuteNonQuery();
        }

        public void Update(User @new, User old)
        {
            var item = @new;
            item.UserId = old.UserId;
            var comm = this.GetCommand("acc_UserUpdatePassword");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "UserId", item.UserId);
            comm.AddParameter<string>(this.Factory, "UserName", item.UserName);
            comm.AddParameter<string>(this.Factory, "Email", item.Email);
            comm.AddParameter<string>(this.Factory, "Password", item.Password);
            comm.AddParameter<string>(this.Factory, "PasswordSalt", item.PasswordSalt);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }

        public void Remove(User item)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(string applicationId, string userName, string password)
        {
            var user = Get(new User() { UserName = userName });
            if (user == null) return false;
            var enPassword = EncryptUtils.EncryptPassword(password, user.PasswordSalt);
            var comm = this.GetCommand("acc_UserGetByValidation");
            if (comm == null)
            {
                return false;
            }
            comm.AddParameter<string>(this.Factory, "ApplicationId", applicationId);
            comm.AddParameter<string>(this.Factory, "UserName", userName);
            comm.AddParameter<string>(this.Factory, "Password", enPassword);
            comm.AddParameter<string>(this.Factory, "PasswordSalt", user.PasswordSalt);
            var dt = this.GetTable(comm);

            var list = EntityBase.ParseListFromTable<User>(dt);
            return list != null && list.Count == 1;
        }

        public bool ChangePassword(string applicationId, string userName, string oldPassword, string newPassword)
        {

            if (ValidateUser(applicationId, userName, oldPassword) == false)
            {
                return false;
            }
            var user = Get(new User() { UserName = userName });
            var salt = EncryptUtils.GenerateSalt();
            var password = EncryptUtils.EncryptPassword(newPassword, salt);
            var comm = this.GetCommand("acc_UserChangePassword");
            if (comm == null)
            {
                return false;
            }
            comm.AddParameter<string>(this.Factory, "ApplicationId", applicationId);
            comm.AddParameter<string>(this.Factory, "UserId", user.UserId);
            comm.AddParameter<string>(this.Factory, "Password", password);
            comm.AddParameter<string>(this.Factory, "PasswordSalt", salt);
            return comm.SafeExecuteNonQuery() != 0;

            //throw new NotImplementedException();
        }

        public bool ChangePassword(string applicationId, string userName, string newPassword)
        {
            var user = Get(new User() { UserName = userName });
            if (user == null) return false;
            var salt = EncryptUtils.GenerateSalt();
            var password = EncryptUtils.EncryptPassword(newPassword, salt);
            var comm = this.GetCommand("acc_UserChangePassword");
            if (comm == null)
            {
                return false;
            }
            comm.AddParameter<string>(this.Factory, "ApplicationId", applicationId);
            comm.AddParameter<string>(this.Factory, "UserId", user.UserId);
            comm.AddParameter<string>(this.Factory, "Password", password);
            comm.AddParameter<string>(this.Factory, "PasswordSalt", salt);
            return comm.SafeExecuteNonQuery() != 0;
        }

        public bool ResetPassword(string applicationId, string userName)
        {
            var user = Get(new User() { UserName = userName });
            if (user == null) return false;
            var salt = EncryptUtils.GenerateSalt();
            var rd = new RandomPassword();
            var password = rd.Generate(8, 10);
            password = EncryptUtils.EncryptPassword(password, salt);
            var comm = GetCommand("acc_UserChangePassword");
            if (comm == null)
            {
                return false;
            }
            comm.AddParameter<string>(this.Factory, "ApplicationId", applicationId);
            comm.AddParameter<string>(this.Factory, "UserId", user.UserId);
            comm.AddParameter<string>(this.Factory, "Password", password);
            comm.AddParameter<string>(this.Factory, "PasswordSalt", salt);
            return (comm.SafeExecuteNonQuery() != 0);
        }

        public bool ChangeEmail(string applicationId, string userId, string email)
        {
            var user = Get(new User() { UserId = userId });
            if (user == null) return false;
            var comm = this.GetCommand("acc_UserChangeEmail");
            if (comm == null) return false;
            comm.AddParameter<string>(this.Factory, "ApplicationId", applicationId);
            comm.AddParameter<string>(this.Factory, "UserId", userId);
            comm.AddParameter<string>(this.Factory, "Email", email);
            return comm.SafeExecuteNonQuery() != 0;
        }

        public bool UpdateLoginLogout(string applicationId, string userId, bool flag)
        {
            var user = Get(new User() { UserId = userId });
            if (user == null) return false;
            var comm = this.GetCommand("acc_UserUpdateLoginLogout");
            if (comm == null) return false;
            comm.AddParameter<string>(this.Factory, "ApplicationId", applicationId);
            comm.AddParameter<string>(this.Factory, "UserId", userId);
            comm.AddParameter<bool>(this.Factory, "Flag", flag);
            return comm.SafeExecuteNonQuery() != 0;
        }
        public void Promote(string userId)
        {
            var comm = this.GetCommand("acc_UserPromote");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "UserId", userId);

            comm.SafeExecuteNonQuery();
        }
        public void Demote(string userId)
        {
            var comm = this.GetCommand("acc_UserDemote");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "UserId", userId);

            comm.SafeExecuteNonQuery();
        }
        public void Delete(string userId, bool status)
        {
            var comm = this.GetCommand("acc_UserDelete");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "UserId", userId);
            comm.AddParameter<bool>(this.Factory, "IsActive", status);
            comm.SafeExecuteNonQuery();
        }
        public string GetLoginInfo(string applicationId, string userId)
        {
            var user = Get(new User() { UserId = userId });
            var ret = new AuthenticationResult();
            if (user != null)
            {
                ret.LoginResult = LoginResults.SUCCESS;

                ret.UserId = user.UserId;
                ret.UserName = user.UserName;
                ret.Email = user.Email;
            }
            return idocNet.Core.Data.Serialization.JsonSerializer.Serialize(ret);
        }

        public List<User> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
