<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<object>" %>
<%@ Import Namespace="idocNet.Core.Extensions" %>
<%@ Import Namespace="idocNet.Core.Web.Mvc.Html" %>
<% 
	ViewData.TemplateInfo.HtmlFieldPrefix = ViewData.TemplateInfo.HtmlFieldPrefix.Replace(ViewData.ModelMetadata.PropertyName, "").Trim('.');
	var propertyName = ViewData["name"] == null ? ViewData.ModelMetadata.PropertyName : ViewData["name"].ToString(); %>
<tr>
	<th>
		<label for="<%:ViewData.TemplateInfo.GetFullHtmlFieldId(propertyName) %>">
			<%: ViewData.ModelMetadata.GetDisplayName()%></label>
		<% if (!string.IsNullOrEmpty(ViewData.ModelMetadata.Description))
	 {%>
		<a href="javascript:;" class="tooltip-link" title="<%: ViewData.ModelMetadata.Description %>"></a>
		<%} %>
	</th>
	<td>
		<%: Html.TextBox(propertyName, Model, Html.GetUnobtrusiveValidationAttributes(propertyName,ViewData.ModelMetadata).Merge("class",ViewData["class"]+ " date"))%>
		<%: Html.ValidationMessage(ViewData.ModelMetadata, new { name = ViewData["name"] })%>
	</td>
</tr>
<script type="text/javascript">
    $('input.date').datetimepicker({
        changeMonth: true,
        changeYear: true,
                stepHour: 2,
                stepMinute: 10,
                stepSecond: 10,
                dateFormat: 'yy-mm-dd',
                timeFormat: 'hh:mm:ss'
    });
</script>