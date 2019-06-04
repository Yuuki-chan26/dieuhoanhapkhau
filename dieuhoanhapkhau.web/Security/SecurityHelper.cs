using dieuhoanhapkhau.web.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Configuration;

namespace dieuhoanhapkhau.web.Security
{
	public class SecurityHelper
	{

		/// <summary>
		/// Checks if a external provider is trying to post a login on this website.
		/// </summary>
		public static bool TryLoginFromProviders(SessionWrapper session, CacheWrapper cache, HttpContextBase context)
		{
			bool logged = false;

			if (TryFinishMembershipLogin(context, session))
			{
				logged = true;
			}
			return logged;
		}

		#region Membership

		/// <summary>
		/// If enabled by configuration, tries to login the current membership user (reading cookie / identity) as nearforums user
		/// </summary>
        public static bool TryFinishMembershipLogin(HttpContextBase context, SessionWrapper session)
        {
            if (SiteConfiguration.Current.AuthenticationProviders.FormsAuth.IsDefined && (!String.IsNullOrEmpty(context.User.Identity.Name)))
            {
                return TryFinishMembershipLogin(session, Membership.GetUser());
            }
            else
            {
                return false;
            }
        }
        public static bool TryFinishMembershipLogin(SessionWrapper session, MembershipUser MembershipUser, bool login = true)
        {
            int flag = 0;
            bool logged = false;

            if (MembershipUser != null)
            {
                //var siteUser = UsersServiceClient.GetByProviderId(AuthenticationProvider.Membership, MembershipUser.ProviderUserKey.ToString());

                //if (siteUser == null)
                //{
                //	//User does not exist on Nearforums db
                //	siteUser = new User();
                //	siteUser.UserName = MembershipUser.UserName;
                //	siteUser.Email = MembershipUser.Email;
                //	siteUser = UsersServiceClient.Add(siteUser, AuthenticationProvider.Membership, MembershipUser.ProviderUserKey.ToString());
                //}

                //if (login)
                //{
                //	session.User = new UserState(siteUser, AuthenticationProvider.Membership);
                //	logged = true;
                //	//UsersServiceClient.AddUserLoginDate(siteUser.Id);
                //	flag = UsersServiceClient.GetUserLoginDateById(siteUser.Id);
                //	if (flag == 0)
                //	{
                //		UsersServiceClient.AddUserLoginDate(siteUser.Id);
                //	}
                //	else
                //	{
                //		UsersServiceClient.EditUserLoginDate(siteUser.Id);
                //	}
                //}
            }

            return logged;
        }
		/// <summary>
		/// Logs the user in or creates the a site user account if the user does not exist, based on membership user.
		/// Sets the logged user in the session.
		/// </summary>
		/// <exception cref="ValidationException"></exception>
        #region["CustomDb"]
        public static bool TryFinishCustomDbLogin(SessionWrapper session, User user, bool login = true)
        {
            int flag = 0;
            bool logged = false;
            if (user != null)
            {
                if (login)
                {
                    logged = true;
                    session.CurrentUser = new UserState(user);
                }
            }
            return logged;
        }
        #endregion



		#endregion
	}
}