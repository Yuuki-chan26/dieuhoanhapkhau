using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idocNet.Core.Data.Entities.Paging;
using idocNet.Core.Data.DataAccess;

namespace idocNet.Core.Data
{
	public abstract class DataManagerBase<T>
		where T : Entities.EntityBase
	{
		public IDataProvider<T> Provider
		{
			get;
			set;
		}
		public DataManagerBase()
		{
		}
		public DataManagerBase(IDataProvider<T> provider)
		{
			this.Provider = provider;
		}

		public virtual T Get(T dummy)
		{
			return Provider.Get(dummy);
		}

		public virtual void Update(T @new, T @old)
		{
			@new.ValidateFields();
			Provider.Update(@new, @old);
		}

		public virtual void Add(T o)
		{
			o.ValidateFields();
			Provider.Add(o);
		}

		public virtual void Remove(T o)
		{
			Provider.Remove(o);
		}


		public virtual PageInfo<T> GetPage(int pageIndex, int pageSize)
		{
			PageInfo<T> page = new PageInfo<T>(pageIndex, pageSize);
			int total = 0;

			page.DataList = Provider.GetAll((page.PageIndex - 1) * page.PageSize, page.PageSize, ref total);

			page.ItemCount = total;

			return page;
		}

		public void BeginTransaction()
		{
			(this.Provider as DataAccessBase).BeginTransaction();
		}

		public void RollbackTransaction()
		{
			(this.Provider as DataAccessBase).RollBackTransaction();
		}

		public void CommitTransaction()
		{
			(this.Provider as DataAccessBase).CommitTransaction();
		}

	}
}
