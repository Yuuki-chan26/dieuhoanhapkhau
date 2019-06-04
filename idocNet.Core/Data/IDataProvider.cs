using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idocNet.Core.Data.DataAccess;
using idocNet.Core.Data.Entities.Paging;

namespace idocNet.Core.Data
{
	public interface IDataProvider<T>
		where T: Entities.EntityBase
	{
		
		T Get(T dummy);
		void Add(T item);
		void Update(T @new, T old);
		void Remove(T item);
		List<T> GetAll(int startIndex, int count, ref int totalItems, string culture);
        List<T> GetAll(int v, int pageSize, ref int total);
    }
}
