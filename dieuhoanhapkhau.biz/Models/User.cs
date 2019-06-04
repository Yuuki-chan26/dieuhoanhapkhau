using idocNet.Core.Data.Entities;
using idocNet.Core.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public class User : EntityBase, IIdentity
    {
        public User()
        {

        }

        [RequireField]
        [DataColum]
        public string UserId
        {
            get;
            set;
        }

        [RequireField]
        [DataColum]
        public string UserName
        {
            get;
            set;
        }

        [RequireField]
        [DataColum]
        public string Email
        {
            get;
            set;
        }

        [DataColum]
        public DateTime CreateDate
        {
            get;
            set;
        }

        [DataColum]
        public bool IsActive
        {
            get;
            set;
        }
        [DataColum]
        public string GroupRoleName
        {
            get;
            set;
        }
        [DataColum]
        public string GroupRoleCode
        {
            get;
            set;
        }
        [DataColum]
        [DataColumEx("UserRole")]
        public int intUserRole
        {
            get;
            set;
        }

        private UserRole _userRole;

        //[DataColum]
        //[DataColumEx("UserRole")]
        public UserRole UserRoleEnum
        {
            get
            { //return _userRole;
                if (_userRole != null)
                {
                    return _userRole;
                }
                Type type = typeof(UserRole);
                _userRole = (UserRole)Enum.Parse(type, intUserRole.ToString());
                return _userRole;
            }
            set { _userRole = value; }
        }

        #region Membership

        [DataColum]
        public string Password { get; set; }
        [DataColum]
        public string PasswordSalt { get; set; }

        [DataColum]
        public bool IsLockedOut { get; set; }

        [DataColum]
        public DateTime LastLoginDate { get; set; }

        [DataColum]
        public DateTime LastPasswordChangedDate { get; set; }

        [DataColum]
        public DateTime LastLockoutDate { get; set; }

        [DataColum]
        public int FailedPasswordAttemptCount { get; set; }

        [DataColum]
        public string Comment { get; set; }

        #endregion

        public int GetIdentityUserRole()
        {
            return this.intUserRole;
        }

        public string GetIdentityName()
        {
            return this.UserName;
        }

        public string GetIdentityUserId()
        {
            return this.UserId;
        }

        public override void ParseData(DataRow dr)
        {
            base.ParseData(dr);
            base.ParseDataEx(dr);
        }

    }
}
