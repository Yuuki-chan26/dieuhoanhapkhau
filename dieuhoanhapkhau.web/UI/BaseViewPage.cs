using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idocNet.Core.Extensions;
using idocNet.Core.Localization;
using idocNet.Core.Configuration;
using dieuhoanhapkhau.web.State;

namespace dieuhoanhapkhau.Web.UI
{
	public class BaseViewPage<TModel> : WebViewPage<TModel> where TModel : class
	{
		#region Initialization
		public BaseViewPage()
		{

		}

		protected override void InitializePage()
		{
            var cultureName = Culture;
            Localizer = Localizer.GetCurrent(cultureName);
			base.InitializePage();
		}
		#endregion

		#region Properties
		private SessionWrapper _session;
		public new SessionWrapper Session
		{
			get
			{
				if (_session == null && Context != null)
				{
					_session = new SessionWrapper(Context.Session);
				}
				return _session;
			}
			set
			{
				_session = value;
			}
		}

		private CacheWrapper _cache;
		public new CacheWrapper Cache
		{
			get
			{
				if (_cache == null && Context != null)
				{
					_cache = new CacheWrapper(Context.Cache);
				}
				return _cache;
			}
			set
			{
				_cache = value;
			}
		}

        public UserState CurrentUser { get { return Session.CurrentUser; } }

        public SiteConfiguration Config
        {
            get
            {
                return SiteConfiguration.Current;
            }
        }

        public string ControllerName
        {
            get
            {
                return ViewContext.RouteData.GetRequiredString("controller");
            }
        }

        public string ActionName
        {
            get
            {
                return ViewContext.RouteData.GetRequiredString("action");
            }
        }



        #region Domain
        /// <summary>
        /// Gets the application current domain (Host) including Protocol and delimiter. Example: http://www.contoso.com (without slash).
        /// </summary>
        public string Domain
		{
			get
			{
				if (this.ViewContext == null)
				{
					return "http://www.contoso.com";
				}
				return this.ViewContext.HttpContext.Request.Url.Scheme + Uri.SchemeDelimiter + this.ViewContext.HttpContext.Request.Url.Host;
			}
		}
		#endregion

		/// <summary>
		/// Gets the current action name
		/// </summary>
	
		/// <summary>
		/// Determines if it is the current actionName
		/// </summary>
		/// <param name="actionName"></param>
		/// <returns></returns>
		public bool IsAction(string actionName)
		{
			return this.ActionName.ToUpper() == actionName.ToUpper();
		}
		#endregion

		#region Methods

        public virtual Localizer Localizer
        {
            get;
            set;
        }
        protected ViewDataDictionary CreateViewData(object values)
        {
            return ViewDataExtensions.CreateViewData(values);
        }

        /// <summary>
        /// Returns an HtmlString containing the localized value
        /// </summary>
        /// <param name="neutralValue">The text to be localized</param>
        public virtual IHtmlString T(string neutralValue)
        {
            return S(neutralValue).ToHtmlString();
        }

        /// <summary>
        /// Returns an HtmlString containing the localized value
        /// </summary>
        /// <param name="neutralValue">The text to be localized</param>
        public virtual IHtmlString T(string neutralValue, params object[] args)
        {
            return S(neutralValue, args).ToHtmlString();
        }


        /// <summary>
        /// Returns the localized representation of the string
        /// </summary>
        /// <param name="neutralValue">The text to be localized</param>
        public virtual string S(string neutralValue, params object[] args)
        {
            var text = neutralValue;
            if (Localizer != null)
            {
                text = Localizer.Get(neutralValue, args);
            }
            return text;
        }

        /// <summary>
        /// Returns the localized representation of the string
        /// </summary>
        /// <param name="neutralValue">The text to be localized</param>
        public virtual string S(string neutralValue)
        {
            var text = neutralValue;
            if (Localizer != null)
            {
                text = Localizer[neutralValue];
            }
            return text;
        }

        public override void Execute()
        {

        }

			
		#endregion


	}

	public class BaseViewPage : BaseViewPage<object>
	{

	}
}