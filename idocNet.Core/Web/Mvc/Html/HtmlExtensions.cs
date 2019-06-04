using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using idocNet.Core.Web.Url;

namespace idocNet.Core.Web.Mvc.Html
{
	public static class HtmlExtensions
	{
		/// <summary>
		/// Normalizes a url in the form ~/path/to/resource.aspx.
		/// </summary>
		/// <param name="html"></param>
		/// <param name="relativeUrl"></param>
		/// <returns></returns>
		public static string ResolveUrl(this HtmlHelper html, string relativeUrl)
		{
			return UrlUtility.ResolveUrl(relativeUrl);
		}
		/// <summary>
		/// Renders a script tag referencing the javascript file. 
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="scriptFile">The script file.</param>
		/// <returns></returns>
		public static IHtmlString Script(this HtmlHelper html, string scriptUrl)
		{
			string url = ResolveUrl(html, scriptUrl);
			return new HtmlString(string.Format("<script type=\"text/javascript\" src=\"{0}\" ></script>\n", url));
		}
		/// <summary>
		/// Renders a link tag referencing the stylesheet.  
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="cssUrl">The CSS file.</param>
		/// <returns></returns>
		public static IHtmlString Stylesheet(this HtmlHelper html, string cssUrl)
		{
			string url = ResolveUrl(html, cssUrl);
			return new HtmlString(string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" />\n", url));
		}
		/// <summary>
		/// Renders a link tag referencing the stylesheet.  
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="cssUrl">The CSS file.</param>
		/// <param name="media">The media.</param>
		/// <returns></returns>
		public static IHtmlString Stylesheet(this HtmlHelper html, string cssUrl, string media)
		{
			string url = ResolveUrl(html, cssUrl);
			return new HtmlString(string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" media=\"{1}\" />\n", url, media));
		}

        public static MvcHtmlString CanonicalUrl(this HtmlHelper html, string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                var rawUrl = html.ViewContext.RequestContext.HttpContext.Request.Url;
                path = String.Format("{0}://{1}{2}", rawUrl.Scheme, rawUrl.Host, rawUrl.AbsolutePath);
            }

            path = path.ToLower();

            if (path.Count(c => c == '/') > 3)
            {
                path = path.TrimEnd('/');
            }

            if (path.EndsWith("/index"))
            {
                path = path.Substring(0, path.Length - 6);
            }

            var canonical = new TagBuilder("link");
            canonical.MergeAttribute("rel", "canonical");
            canonical.MergeAttribute("href", path);
            return new MvcHtmlString(canonical.ToString(TagRenderMode.SelfClosing));
        }
        public static MvcHtmlString CanonicalUrl(this HtmlHelper html)
        {
            var rawUrl = html.ViewContext.RequestContext.HttpContext.Request.Url;
            if (rawUrl.Host.Contains("www"))
            {
                return CanonicalUrl(html, String.Format("{0}://{1}{2}", rawUrl.Scheme, rawUrl.Host.Substring(4), rawUrl.AbsolutePath));
            }
            else
            {
                return CanonicalUrl(html, String.Format("{0}://{1}{2}", rawUrl.Scheme, rawUrl.Host, rawUrl.AbsolutePath));
            }
        }

	}
}
