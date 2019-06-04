<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<%@ Import Namespace="idocNet.Core.Extensions" %>
<% 
	string prefix = ViewData.TemplateInfo.HtmlFieldPrefix.Replace(ViewData.ModelMetadata.PropertyName, "").Trim('.');
	ViewData.TemplateInfo.HtmlFieldPrefix = prefix;
	var propertyName = ViewData["name"] == null ? ViewData.ModelMetadata.PropertyName : ViewData["name"].ToString();
%>
<tr>
	<th>
		<label class="label-checkbox" for="<%:ViewData.TemplateInfo.GetFullHtmlFieldId(propertyName)%>">
			<%: ViewData.ModelMetadata.GetDisplayName()%></label>
		<%
			if (!string.IsNullOrEmpty(ViewData.ModelMetadata.Description))
			{%>
		<a href="javascript:;" class="tooltip-link" title="<%: ViewData.ModelMetadata.Description %>"></a>
		<%} %>
	</th>
	<td>
		<input type="checkbox" name="<%:propertyName %>" id="<%:propertyName %>" value="1" <%:(!string.IsNullOrEmpty(Model)&& Model.Equals("1")?"checked":"") %> class="<%:ViewData["class"] %>" />
		<input type="hidden" value="0" name="<%:propertyName %>" />

	</td>
</tr>
