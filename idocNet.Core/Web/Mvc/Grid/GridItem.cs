﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using idocNet.Core.Reflection;
using System.Web.Routing;
namespace idocNet.Core.Web.Mvc.Grid
{
    public class GridItem
    {
        public GridItem(int index, string idProperty, object dataItem, IEnumerable<GridColumn> columns, IEnumerable<GridAction> actions, IVisibleArbiter checkVisible)
        {
            Index = index;
            DataItem = dataItem;
            this.GridColumns = columns;
            this.GridActions = actions;
            this.checkVisible = checkVisible;

            if (!string.IsNullOrEmpty(idProperty))
            {
                Id = GetValue(dataItem, idProperty);
            }
        }
        public int Index { get; private set; }
        public IEnumerable<GridColumn> GridColumns { get; private set; }
        public IEnumerable<GridAction> GridActions { get; private set; }
        private IVisibleArbiter checkVisible;


        public bool IsAlternatingItem
        {
            get
            {
                return Index % 2 != 0;
            }
        }

        public object DataItem { get; private set; }
        public object Id { get; set; }

        public IEnumerable<IHtmlString> GetItemValues(ViewContext viewContext)
        {
            foreach (var column in GridColumns)
            {
                object value = column.GetValue(DataItem);
                IHtmlString formatted = new HtmlString(string.Empty);
                if (value != null)
                {
                    if (this.IsAlternatingItem && column.AlternatingItemRender != null)
                    {
                        formatted = column.AlternatingItemRender.Render(DataItem, value, viewContext);
                    }
                    else if (column.ItemRender != null)
                    {
                        formatted = column.ItemRender.Render(DataItem, value, viewContext);
                    }
                    else
                    {
                        formatted = new HtmlString(System.Web.HttpUtility.HtmlEncode(value));
                    }
                    if (column.ItemFormatString != null)
                    {
                        formatted = new HtmlString(string.Format(column.ItemFormatString, formatted));
                    }
                }
                yield return formatted;
            }
        }
        public IEnumerable<GridItemAction> GetItemActions(ViewContext viewContext)
        {
            foreach (var gridAction in GridActions)
            {
                GridItemAction itemAction = new GridItemAction()
                {
                    ActionName = gridAction.ActionName,
                    ControllerName = gridAction.ControllerName,
                    DisplayName = gridAction.DisplayName,
                    ConfirmMessage = gridAction.ConfirmMessage,
                    Icon = gridAction.Icon,
                    Title = gridAction.Title,
                    Class = gridAction.Class,
                    RouteValues = new System.Web.Routing.RouteValueDictionary(gridAction.InheritRouteValues ? viewContext.RequestContext.AllRouteValues() : new RouteValueDictionary())
                };

                if (!string.IsNullOrEmpty(itemAction.ControllerName))
                {
                    itemAction.RouteValues["controller"] = itemAction.ControllerName;
                }
                if (gridAction.RouteValuesSetting != null)
                {
                    foreach (var setting in gridAction.RouteValuesSetting)
                    {
                        itemAction.RouteValues[setting.RouteValueName] = GetValue(this.DataItem, setting.PropertyName);
                    }
                }
                if (gridAction.RouteValuesGetter != null)
                {
                    itemAction.RouteValues = gridAction.RouteValuesGetter.GetValues(this.DataItem, itemAction.RouteValues, viewContext);
                }
                if (gridAction.VisibleArbiter != null)
                {
                    itemAction.Visible = gridAction.VisibleArbiter.IsVisible(DataItem, viewContext);
                }
                else
                {
                    if (!string.IsNullOrEmpty(gridAction.VisibleProperty))
                    {
                        itemAction.Visible = (bool)GetValue(DataItem, gridAction.VisibleProperty);
                    }
                }

                yield return itemAction;
            }
        }
        private object GetValue(object dataItem, string propertyName)
        {
            return dataItem.Members().Properties[propertyName];
        }
        public bool GetCheckVisible(ViewContext viewContext)
        {
            if (checkVisible == null)
            {
                return true;
            }
            else
            {
                return checkVisible.IsVisible(this.DataItem, viewContext);
            }
        }
    }
}