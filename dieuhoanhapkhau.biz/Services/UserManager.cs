using dieuhoanhapkhau.biz.Models;
using dieuhoanhapkhau.biz.Persistance;
using idocNet.Core.Data;
using idocNet.Core.Data.Entities;
using idocNet.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Services
{
    public class UserManager: DataManagerBase<User>
    {
        public UserManager()
            : base()
        {
        }

        public UserManager(IDataProvider<User> provider)
            : base(provider)
        {
        }

        private IUserProvider UserProvider
        {
            get { return (IUserProvider)Provider; }
        }

        public override void Add(User item)
        {
            //item.UserId = User.GenerateNewGuid();
            item.UserId = EntityBase.GenerateNewGuid();
            item.Email = item.Email.ToLower();
            var salt = EncryptUtils.GenerateSalt();
            var password = EncryptUtils.EncryptPassword(item.Password, salt);

            item.Password = password;
            item.PasswordSalt = salt;
            base.Add(item);
        }

        public User GetByUserName(string username)
        {
            return UserProvider.GetByUserName(username);
        }
        public List<User> GetAll(int startIndex, int lenght, ref int totalItem)
        {
            return UserProvider.GetAll(startIndex, lenght, ref totalItem);
        }
        public bool UpdateLoginLogout(string applicationId, string userId, bool flag)
        {
            return UserProvider.UpdateLoginLogout(applicationId, userId, flag);
        }
        public bool ChangePassword(string applicationId, string userName, string newPassword)
        {
            return UserProvider.ChangePassword(applicationId, userName, newPassword);
        }
        public virtual User ValidateUser(string applicationId, string userName, string password, out bool isLockout)
        {
            isLockout = false;
            var setting = Setting.Current;
            var user = Provider.Get(new User() { UserName = userName });
            if (user == null) return null;
            isLockout = user.IsLockedOut;
            if (user.IsLockedOut)
            {
                var time = (DateTime.UtcNow - user.LastLockoutDate);
                if (time.Minutes > setting.MinutesToUnlock)
                {
                    user.IsLockedOut = false;
                    user.FailedPasswordAttemptCount = 0;
                }
                else
                {
                    isLockout = true;
                    return null;
                }
            }

            var isValid = UserProvider.ValidateUser(applicationId, userName, password);
            if (!isValid)
            {
                user.FailedPasswordAttemptCount += 1;
                if (setting.EnableLockout && user.FailedPasswordAttemptCount >= setting.FailedPasswordAttemptCount)
                {
                    user.IsLockedOut = true;
                    user.LastLockoutDate = DateTime.UtcNow;
                    isLockout = true;
                }
                return null;
            }
            else
            {
                user.FailedPasswordAttemptCount = 0;
            }
            return user;
        }

        public string ValidateUser(string applicationId, string userId, string password)
        {
            bool isLockout = false;
            var user = ValidateUser(applicationId, userId, password, out isLockout);
            var ret = new AuthenticationResult();

            if (user != null)
            {
                ret.LoginResult = LoginResults.SUCCESS;

                ret.UserId = user.UserId;
                ret.UserName = user.UserName;
                ret.Email = user.Email;
            }
            if (isLockout) ret.LoginResult = LoginResults.LOCKOUT;

            return idocNet.Core.Data.Serialization.JsonSerializer.Serialize(ret);
        }

        public bool ChangePassword(string applicationId, string userName, string oldPassword, string newPassword)
        {
            return UserProvider.ChangePassword(applicationId, userName, oldPassword, newPassword);
        }
        public void Promote(string userId)
        {
            UserProvider.Promote(userId);
        }
        public void Demote(string userId)
        {
            UserProvider.Demote(userId);
        }
        public void Delete(string userId, bool status)
        {
            UserProvider.Delete(userId, status);
        }
    }
}
