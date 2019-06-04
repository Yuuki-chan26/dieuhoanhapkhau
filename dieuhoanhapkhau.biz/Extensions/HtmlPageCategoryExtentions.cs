﻿using dieuhoanhapkhau.biz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Extensions
{
    public static class HtmlPageCategoryExtentions
    {
        public static void BuildCateTree(this HtmlPageCategoryBase cate, List<HtmlPageCategoryBase> categoryList)
        {
            if (cate.Children == null) cate.Children = new List<HtmlPageCategoryBase>();

            foreach (var c in categoryList)
            {
                if (c.Parent.HtmlPageCategoryId == cate.HtmlPageCategoryId)
                {

                    if (!cate.Children.Contains(c))
                    {
                        cate.Children.Add(c);
                        c.Parent = cate;

                        BuildCateTree(c, categoryList);
                    }
                }
            }
        }


        public static List<HtmlPageCategoryBase> ToCategoryTree(this List<HtmlPageCategoryBase> categoryList)
        {
            List<HtmlPageCategoryBase> list = new List<HtmlPageCategoryBase>();

            foreach (var cate in categoryList)
            {
                if (cate.Parent.HtmlPageCategoryId == 0)
                {
                    if (cate.Children == null) cate.BuildCateTree(categoryList);

                    list.Add(cate);
                }
            }

            return list;
        }



        public static List<HtmlPageCategoryBase> ToFlatCategoryTree(this List<HtmlPageCategoryBase> categoryList, int level = 0)
        {
            List<HtmlPageCategoryBase> list = new List<HtmlPageCategoryBase>();

            foreach (var cate in categoryList)
            {
                cate.HLevel = level;
                list.Add(cate);
                list.AddRange(cate.Children.ToFlatCategoryTree(level + 1));
            }

            return list;
        }



        public static List<HtmlPageCategoryBase> GetParentListFromRoot(this HtmlPageCategoryBase cate)
        {

            HtmlPageCategoryBase parent = cate.Parent;

            List<HtmlPageCategoryBase> list = null;
            while (parent != null)
            {

                if (list == null) list = new List<HtmlPageCategoryBase>();
                list.Insert(0, parent);
                parent = parent.Parent;
            }

            return list;

        }
        public static List<HtmlPageCategoryBase> ToHtmlPageCateTreeByParentId(List<HtmlPageCategoryBase> groupNewsBaseList, int id)
        {
            var list = new List<HtmlPageCategoryBase>();

            foreach (var cate in groupNewsBaseList)
            {
                //if (cate.Parent.GroupId == 0)
                //{
                if (cate.HtmlPageCategoryId == id)
                {
                    if (cate.Children == null) cate.BuildCateTree(groupNewsBaseList);
                    list.Add(cate);
                    break;
                }

                //}
            }

            return list;
        }
        //public static ProductCategory AsActual(this ProductCategory cate, string culture)
        //{
        //    return (ProductCategory)cate.AsActual(ProductsServiceClient.GetCategoryList(culture));
        //}

        //public static NewsCategory AsActual(this NewsCategory cate, string culture)
        //{

        //    return (NewsCategory)cate.AsActual(NewsServiceClient.GetCategoryList(culture));

        //}
        //public static AgentCategory AsActual(this AgentCategory cate, string culture)
        //{

        //    return (AgentCategory)cate.AsActual(AgentsServiceClient.GetCategoryList(culture));

        //}
        //public static AdsPositions AsActual(this AdsPositions cate, string culture)
        //{

        //    return (AdsPositions)cate.AsActual(AdsServiceClient.GetCategoryList(culture));

        //}
        //public static FieldText AsActual(this FieldText cate, string culture)
        //{

        //    return (FieldText)cate.AsActual(TextsServiceClient.GetAllFieldText(culture));

        //}
        //public static IssuedText AsActual(this IssuedText cate, string culture)
        //{

        //    return (IssuedText)cate.AsActual(TextsServiceClient.GetAllIssuedText(culture));

        //}
        //public static TypeText AsActual(this TypeText cate, string culture)
        //{

        //    return (TypeText)cate.AsActual(TextsServiceClient.GetAllTypeText(culture));

        //}
    }
}