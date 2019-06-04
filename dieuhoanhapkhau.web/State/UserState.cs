using dieuhoanhapkhau.biz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using idocNet.Core.HealthMonitoring;
using dieuhoanhapkhau.biz.Persistance.SqlServer;

namespace dieuhoanhapkhau.web.State
{
    public class UserState
    {
        public IIdentity Identity { get; set; }

        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The hierarchical role of the user
        /// </summary>
        public UserRole Role
        {
            get;
            set;
        }

        public int IntRole { get; set; }

        /// <summary>
        /// Determines if the user is in role admin
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                return Role == UserRole.Admin;
            }
        }

        public bool IsManager
        {
            get
            {
                return Role == UserRole.Managers;
            }
        }
        /// <summary>
        /// Determines if the user has the priviledges of a moderator (is moderator or admin)
        /// </summary>
        public bool HasModeratorPriviledges
        {
            get
            {
                return Role >= UserRole.Moderator;
            }
        }

        public string Email
        {
            get;
            set;
        }

        public TimeSpan? TimeZone
        {
            get;
            set;
        }

        public Guid Guid
        {
            get;
            set;
        }

        public string ExternalProfileUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Determines the authentication provider used by the user
        /// </summary>
        public AuthenticationProvider Provider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the info from the authentication provider that authenticated this account.
        /// </summary>
        

        public User ToUser()
        {
            return new User
            {
                UserId = this.Identity.GetIdentityUserId()
            };
        }

        public UserState()
        {

        }

        public UserState(User user)
        {
            Type type = typeof(UserRole);
            //_userRole = (UserRole)Enum.Parse(type, UserRole.ToString());
            Name = user.UserName;
            IntRole = user.intUserRole;
            Role = (UserRole)Enum.Parse(type, user.intUserRole.ToString());
            Email = user.Email;
            //try
            //{
            //	//var allValues = (AttributeType[])Enum.GetValues(typeof(AttributeType));

            //	//var array = allValues.Select(value => new object[] { value, value.ToString() })
            //	//					 .ToArray();
            //	//var list = ListUserRole.GetListValues(typeof(UserRole));
            //	var list1 = ListUserRole.GetListStringEnumNames<UserRole>();
            //	//var list2 = ListUserRole.GetListValues(UserRole);
            //	var list3 = Enum<UserRole>.GetValues();
            //	foreach (var userRole in list3)
            //	{
            //		//if(userRole.)
            //	}
            //	var list4 = Enum.GetValues(typeof(UserRole));

            //}
            //catch (Exception)
            //{

            //	//throw;
            //}
        }


    }
}