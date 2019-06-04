<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<object>" %>
<%@ Import Namespace="idocNet.Core.Extensions" %>
<%@ Import Namespace="idocNet.Core.Web.Mvc.Html" %>
<% 
	ViewData.TemplateInfo.HtmlFieldPrefix = ViewData.TemplateInfo.HtmlFieldPrefix.Replace(ViewData.ModelMetadata.PropertyName, "").Trim('.');
	var propertyName = ViewData["name"] == null ? ViewData.ModelMetadata.PropertyName : ViewData["name"].ToString(); %>
<div class="form-group">
																<label class="control-label col-xs-12 col-sm-2 no-padding-right" for="<%:ViewData.TemplateInfo.GetFullHtmlFieldId(propertyName) %>"><%: ViewData.ModelMetadata.GetDisplayName()%></label>

																<div class="col-xs-12 col-sm-10">
																	<div class="clearfix">
																		<%: Html.TextBox(propertyName, Model, Html.GetUnobtrusiveValidationAttributes(propertyName,ViewData.ModelMetadata).Merge("class",ViewData["class"]).Merge("required",ViewData["required"]))%>
		<%: Html.ValidationMessage(ViewData.ModelMetadata, new { name = ViewData["name"] })%>
																	</div>
																</div>
															</div>

