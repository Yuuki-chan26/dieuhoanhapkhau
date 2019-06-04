using dieuhoanhapkhau.biz.Models;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dieuhoanhapkhau.biz.Persistance.SqlServer
{
    public class ImageProductProvider : DataAccessBase, IImageProductProvider
    {
        public void Add(ProductImages item, string Culture)
        {
            var comm = this.GetCommand("sp_Images_Insert");
            if (comm == null) return;
            comm.AddParameter<string>(this.Factory, "ProductImage", item.ProductImage);
            comm.AddParameter<int>(this.Factory, "ProductId", item.ProductId);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<string>(this.Factory, "Url", item.Url);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.AddParameter<string>(this.Factory, "Culture", Culture);
            comm.SafeExecuteNonQuery();
        }

        public void Add(ProductImages item)
        {
            throw new NotImplementedException();
        }
        
        public ProductImages Get(ProductImages dummy)
        {
            var comm = this.GetCommand("sp_ImagesGet");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "ProductImageId", dummy.ProductImageId);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<ProductImages>(dt).FirstOrDefault();
            return htmlPage ?? null;
        }

        public List<ProductImages> GetAll(int startIndex, int count, ref int totalItems, string culture)
        {
            throw new NotImplementedException();
        }

        public List<ProductImages> GetAll(int v, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public List<ProductImages> GetListImagesByCateId(int cateid, int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_ImagesSearchByCateId");
            if (comm == null) return null;
            comm.AddParameter<int>(this.Factory, "ProductImageId", cateid);
            comm.AddParameter<int>(this.Factory, "StartIndex", startIndex);
            comm.AddParameter<int>(this.Factory, "Length", lenght);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var totalItemsParam = comm.AddParameter(this.Factory, "TotalItems", DbType.Int32, null);
            totalItemsParam.Direction = ParameterDirection.Output;
            var dt = this.GetTable(comm);
            if (totalItemsParam.Value != DBNull.Value)
            {
                totalItem = Convert.ToInt32(totalItemsParam.Value);
            }
            return EntityBase.ParseListFromTable<ProductImages>(dt);
        }

        public void Remove(ProductImages item)
        {
            var comm = this.GetCommand("sp_Images_Delete");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProductImageId", item.ProductImageId);
            comm.SafeExecuteNonQuery();
        }

        public List<ProductImages> Search(int startIndex, int lenght, ref int totalItem, string culture)
        {
            var comm = this.GetCommand("sp_ImagesSearch");
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
            return EntityBase.ParseListFromTable<ProductImages>(dt);
        }

        public void Update(ProductImages @new, ProductImages old)
        {
            var item = @new;
            item.ProductImageId = old.ProductImageId;
            var comm = this.GetCommand("sp_Images_Update");
            if (comm == null) return;
            comm.AddParameter<int>(this.Factory, "ProductImageId", item.ProductImageId);
            comm.AddParameter<string>(this.Factory, "ProductImage", item.ProductImage);
            comm.AddParameter<int>(this.Factory, "ProductId", item.ProductId);
            comm.AddParameter<int>(this.Factory, "OrderNo", item.OrderNo);
            comm.AddParameter<bool>(this.Factory, "IsActive", item.IsActive);
            comm.AddParameter<string>(this.Factory, "Url", item.Url);
            comm.AddParameter<string>(this.Factory, "CreateBy", item.CreateBy);
            comm.SafeExecuteNonQuery();
        }
        public List<ProductImages> GetByPrdId(int productId, string culture)
        {
            var comm = this.GetCommand("sp_ProductImageGetByPrdId");
            if (comm == null)
            {
                return null;
            }
            comm.AddParameter<int>(this.Factory, "ProductId", productId);
            comm.AddParameter<string>(this.Factory, "Culture", culture);
            var dt = this.GetTable(comm);
            var htmlPage = EntityBase.ParseListFromTable<ProductImages>(dt);
            return htmlPage ?? null;
            //throw new NotImplementedException();
        }

        public List<ProductImages> GetAllActive(int productId, string culture)
        {
                var comm = this.GetCommand("sp_ProductImageGetAllActiveByPrdId");
                if (comm == null)
                {
                    return null;
                }
                comm.AddParameter<int>(this.Factory, "ProductId", productId);
                comm.AddParameter<string>(this.Factory, "Culture", culture);
                var dt = this.GetTable(comm);
                var htmlPage = EntityBase.ParseListFromTable<ProductImages>(dt);
                return htmlPage ?? null;
                //throw new NotImplementedException();

        }
    }
}
