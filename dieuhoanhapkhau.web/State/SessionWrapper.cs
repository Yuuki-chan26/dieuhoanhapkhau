using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Web;


namespace dieuhoanhapkhau.web.State
{
    public class SessionWrapper
    {
        public static SessionWrapper Current
        {
            get
            {
                HttpSessionStateBase session = new HttpSessionStateWrapper(HttpContext.Current.Session);
                return new SessionWrapper(session);
            }
        }


        public HttpSessionStateBase Session
        {
            get;
            set;
        }
        public static string objectList = "objectList";
        #region Get / Set to session
        /// <summary>
        /// Gets typed values from the session
        /// </summary>
        /// <param name="key">The key name of the session value</param>
        public T GetItem<T>(string key)
        {
            return (T)Session[key];
        }

        /// <summary>
        /// Gets typed values from the session
        /// </summary>
        /// <param name="key">The key name of the session value</param>
        /// <param name="create">Determines if a new instance of the type T should be created in case it does not exist in session for that key</param>
        public T GetItem<T>(string key, bool create) where T : new()
        {

            if (Session[key] == null)
            {
                if (create)
                {
                    T value = new T();
                    Session[key] = value;
                    return value;
                }
                else
                {
                    return (T)Session[key];
                }
            }
            else
            {
                return (T)Session[key];
            }
        }

        public void SetItem<T>(string key, T value)
        {
            Session[key] = value;
        }

        public SessionWrapper(HttpSessionStateBase session)
        {
            Session = session;
        }

        public SessionWrapper(HttpContextBase context)
            : this(context.Session)
        {

        }
        #endregion

        #region Props
        /// <summary>
        /// Current logged user. If the user is not logged in, its null.
        /// </summary>
        public UserState CurrentUser
        {
            get
            {
                return GetItem<UserState>("CurrentUser");
            }
            set
            {

                SetItem<UserState>("CurrentUser", value);
            }
        }


        /// <summary>
        /// Gets a unique private token for this session. This token is not related to session id.
        /// </summary>
        public string SessionToken
        {
            get
            {
                const string sessionTokenKey = "SessionToken";
                if (GetItem<string>(sessionTokenKey) == null)
                {
                    SetItem<string>(sessionTokenKey, Guid.NewGuid().ToString("N"));
                }
                return GetItem<string>(sessionTokenKey);
            }
        }

        /// <summary>
        /// Gets or sets the current Captcha hash for captcha validation
        /// </summary>
        public string CaptchaHash
        {
            get
            {
                return GetItem<string>("CaptchaHash");
            }
            set
            {
                SetItem<string>("CaptchaHash", value);
            }
        }

        public bool IsPasswordReset
        {
            get
            {
                return GetItem<bool>("IsPasswordReset", true);
            }
            set
            {
                SetItem<bool>("IsPasswordReset", value);
            }
        }

        #endregion
    }
}