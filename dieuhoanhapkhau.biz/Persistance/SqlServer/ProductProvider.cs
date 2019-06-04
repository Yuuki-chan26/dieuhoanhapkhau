using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class ProductProvider : DataAccessBase, IProductProvider
    {
        public Product Get(Product dummy)
        {
            var comm = this.GetCommand("sp_ProductGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "ProductId", dummy.ProductId);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<Product>(dt).FirstOrDefault();
            return htmlPage ?? null;
            //throw new NotImplementedException();
        }
        public Product GetByCode(Product dummy,string culture)
        {
            var comm = this.GetCommand("sp_ProductGetByCode");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<string>(this.Factory, "ProductCode", dummy.ProductCode);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<Product>(dt).FirstOrDefault();
            return htmlPage ?? null;
            //throw new NotImplementedException();
        }
        public void Add(Product item, string Culture)
        {
            var comm = this.GetCommand("sp_Product_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "ProductName", item.ProductName);
			comm.AddParameter<string>(this.Factory, "ProductTitle", item.ProductTitle);
            comm.AddParameter<string>(this.Factory, "ProductCode", item.ProductCode);
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", item.PrdCategoryId);
            comm.AddParameter<string>(this.Factory, "ProductSummary", item.ProductSummary);
            comm.AddParameter<string>(this.Factory, "ProductDescription", item.ProductDescription);
            comm.AddParameter<string>(this.Factory, "ProductKeyword", item.ProductKeyword);           
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<decimal>(this.Factory, "ProductPrice", item.ProductPrice);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<bool>(this.Factory, "IsHotProduct", item.IsHotProduct);
            comm.AddParameter<bool>(this.Factory, "IsNewProduct", item.IsNewProduct);
            comm.AddParameter<bool>(this.Factory, "IsSaleProduct", item.IsSaleProduct);
            comm.AddParameter<string>(this.Factory, "ProductImage", item.ProductImage);
            comm.AddParameter<int>(this.Factory, "ProductPropertyId", item.ProductPropertyId);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.AddParameter<int>(this.Factory, "Quality", item.Quality);
            comm.AddParameter<string>(this.Factory, "Culture", Culture);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }
        public void Add(Product item)
        {
            //var comm = this.GetCommand("sp_HtmlPage_Insert");
            //if (comm == null) return;
            //comm.AddParameter<string>(this.Factory, "HtmlPageTitle", item.HtmlPageTitle);
            //comm.AddParameter<string>(this.Factory, "HtmlPageShortName", item.HtmlPageShortName);
            //comm.AddParameter<string>(this.Factory, "HtmlPageSummary", item.HtmlPageSummary);
            //comm.AddParameter<string>(this.Factory, "HtmlPageDescription", item.HtmlPageDescription);
            //comm.AddParameter<string>(this.Factory, "HtmlPageKeyword", item.HtmlPageKeyword);
            //comm.AddParameter<string>(this.Factory, "HtmlPageBody", item.HtmlPageBody);
            //comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            //comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            //comm.AddParameter<string>(this.Factory, "HtmlPageImage", item.HtmlPageImage);
            //comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            //comm.AddParameter<string>(this.Factory, "Culture", Culture);
            //comm.SafeExecuteNonQuery();
            throw new NotImplementedException();
        }
        public void Update(Product @new, Product old)
        {
            var item = @new;
            item.ProductId = old.ProductId;
            var comm = this.GetCommand("sp_Product_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProductId", item.ProductId);
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", item.PrdCategoryId);
            comm.AddParameter<string>(this.Factory, "ProductName", item.ProductName);
			comm.AddParameter<string>(this.Factory, "ProductTitle", item.ProductTitle);
            comm.AddParameter<string>(this.Factory, "ProductCode", item.ProductCode);
            comm.AddParameter<string>(this.Factory, "ProductSummary", item.ProductSummary);
            comm.AddParameter<string>(this.Factory, "ProductDescription", item.ProductDescription);
            comm.AddParameter<string>(this.Factory, "ProductKeyword", item.ProductKeyword);
            comm.AddParameter<string>(this.Factory, "ProductSlogan", item.ProductSlogan);
            comm.AddParameter<decimal>(this.Factory, "ProductPrice", item.ProductPrice);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<bool>(this.Factory, "IsHotProduct", item.IsHotProduct);
            comm.AddParameter<bool>(this.Factory, "IsNewProduct", item.IsNewProduct);
            comm.AddParameter<bool>(this.Factory, "IsSaleProduct", item.IsSaleProduct);
            comm.AddParameter<string>(this.Factory, "ProductImage", item.ProductImage);
            comm.AddParameter<int>(this.Factory, "ProductPropertyId", item.ProductPropertyId);
            comm.AddParameter<string>(this.Factory, "UpdateBy", item.UpdateBy);
            comm.AddParameter<int>(this.Factory, "ProducerId", item.ProducerId);
            comm.AddParameter<int>(this.Factory, "Quality", item.Quality);
            //comm.AddParameter<DateTime>(this.Factory, "CreateDate", item.CreateDate);
            //comm.AddParameter<DateTime>(this.Factory, "EditDate", item.EditDate);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }

        public void Remove(Product item)
        {
            var comm = this.GetCommand("sp_Product_Delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProductId", item.ProductId);
            comm.SafeExecuteNonQuery();
            //throw new NotImplementedException();
        }

        public List<Product> GetAll(int startIndex, int lenght, ref int totalItem)
        {
            throw new NotImplementedException();
        }

        //public List<HtmlPage> SelectTop4()
        //{
        //    var comm = this.GetCommand("sp_SliderBanner_SelectTop4");
        //    if (comm == null) return null;
        //    var dt = this.GetTable(comm);
        //    return EntityBase.ParseListFromTable<HtmlPage>(dt);
        //}

        public List<Product> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_ProductSearch");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<int>(this.Factory, "Length", lenght);
            var totalItemsParam = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItemsParam.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItemsParam.Value != DBNull.Value)
            {
                totalItem = Convert.ToInt32(totalItemsParam.Value);
            }
            return EntityBase.ParseListFromTable<Product>(dt);
        }
        public List<Product> GetListHotProduct(string culture)
        {
            var comm = this.GetCommand("sp_ProductGetHotProduct");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Product>(dt);
        }
        public List<Product> GetListNewProduct(string culture)
        {
            var comm = this.GetCommand("sp_ProductGetNewProduct");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Product>(dt);
        }

        public List<Product> GetListSaleProduct(string culture)
        {
            var comm = this.GetCommand("sp_ProductGetSaleProduct");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Product>(dt);
        }

        public List<Product> GetListHotNewSaleProduct(string culture)
        {
            var comm = this.GetCommand("sp_ProductGetHotNewSaleProduct");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            return EntityBase.ParseListFromTable<Product>(dt);
        }

        public List<Product> ProductGetByCateId(int cateid, string culture)
        {
            var comm = this.GetCommand("sp_ProductGetByCateId");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", cateid);
            var dt = this.GetTable(comm);
            
            return EntityBase.ParseListFromTable<Product>(dt);
        }
        public List<Product> ProductGetAllActive(string culture)
        {
            var comm = this.GetCommand("sp_ProductGetAllActive");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);

            return EntityBase.ParseListFromTable<Product>(dt);
        }
        public List<Product> ProductGetAllRegister(string culture)
        {
            var comm = this.GetCommand("sp_ProductGetAllRegister");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);

            return EntityBase.ParseListFromTable<Product>(dt);
        }

        public List<Product> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<Product> ProductGetByProducer(int id, int parentid)
        {
            var comm = this.GetCommand("sp_Product_GetByManufacter");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "ProducerId", id);
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", parentid);
            var dt = this.GetTable(comm);

            return EntityBase.ParseListFromTable<Product>(dt);
        }

        public List<Product> ProductGetByPrpducerPropertyId(int producerid, int producerpropertyid, int productid)
        {
            var comm = this.GetCommand("sp_ProductGetByPrty");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "PrdCategoryId", productid);
            comm.AddParameter<int>(this.Factory, "ProducerId", producerid);
            comm.AddParameter<int>(this.Factory, "ProducerPropertyId", producerpropertyid);
            
            var dt = this.GetTable(comm);

            return EntityBase.ParseListFromTable<Product>(dt);
        }

        public List<Product> Searchkey(int startIndex, int length, ref int total, string key)
        {
            
            var comm = this.GetCommand("sp_Product_Searchkey");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<int>(this.Factory, "Length", length);

            comm.AddParameter<string>(this.Factory, "Keyword", key);

            var totalItem = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItem.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItem.Value != DBNull.Value)
            {
                total = Convert.ToInt32(totalItem.Value);
            }
            return EntityBase.ParseListFromTable<Product>(dt);

        }

        public List<Product> GetByPropertyShortName(string shortname, string culture, int startIndex, int length, ref int total)
        {
            var comm = this.GetCommand("sp_Product_GetByPropertyShortName");
            if (comm == null) return null;
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            comm.AddParameter<string>(this.Factory, "ShortName", shortname);

            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<int>(this.Factory, "Length", length);

            var totalItem = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItem.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItem.Value != DBNull.Value)
            {
                total = Convert.ToInt32(totalItem.Value);
            }
            return EntityBase.ParseListFromTable<Product>(dt);
        }
    }
}
