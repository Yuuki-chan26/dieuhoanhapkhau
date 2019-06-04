using dieuhoanhapkhau.biz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Extensions
{
    public static class ProductCategoryExtensions
    {
        public static void BuildCateTree(this ProductCategoryBase cate, List<ProductCategoryBase> categoryList)
        {
            if (cate.Children == null) cate.Children = new List<ProductCategoryBase>();

            foreach (var c in categoryList)
            {
                if (c.Parent.PrdCategoryId == cate.PrdCategoryId)
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


        public static List<ProductCategoryBase> ToCategoryTree(this List<ProductCategoryBase> categoryList)
        {
            List<ProductCategoryBase> list = new List<ProductCategoryBase>();

            foreach (var cate in categoryList)
            {
                if (cate.Parent.PrdCategoryId == 0)
                {
                    if (cate.Children == null) cate.BuildCateTree(categoryList);

                    list.Add(cate);
                }
            }

            return list;
        }



        public static List<ProductCategoryBase> ToFlatCategoryTree(this List<ProductCategoryBase> categoryList, int level = 0)
        {
            List<ProductCategoryBase> list = new List<ProductCategoryBase>();

            foreach (var cate in categoryList)
            {
                cate.HLevel = level;
                list.Add(cate);
                list.AddRange(cate.Children.ToFlatCategoryTree(level + 1));
            }

            return list;
        }



        public static List<ProductCategoryBase> GetParentListFromRoot(this ProductCategoryBase cate)
        {

            ProductCategoryBase parent = cate.Parent;

            List<ProductCategoryBase> list = null;
            while (parent != null)
            {

                if (list == null) list = new List<ProductCategoryBase>();
                list.Insert(0, parent);
                parent = parent.Parent;
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
