using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idocNet.Core.Caching;
using idocNet.Core.Data.Entities;

namespace idocNet.Core.Data
{
	public abstract class CacheObjectProviderBase<T>
	where T :  EntityBase
	{
		private IDataProvider<T> innerRepository;
		public CacheObjectProviderBase(IDataProvider<T> inner)
		{
			this.innerRepository = inner;
		}

		public virtual T Get(T dummy)
		{
			var cacheKey = GetCacheKey(dummy);

			var cached = CacheManagerFactory.DefaultCacheManager.GlobalObjectCache().GetCache(cacheKey, () => innerRepository.Get(dummy));

			return cached;
		}

		protected abstract string GetCacheKey(T o);

		protected virtual void ClearObjectCache(T o)
		{
			var cacheKey = GetCacheKey(o);
			CacheManagerFactory.DefaultCacheManager.GlobalObjectCache().Remove(cacheKey);
		}

		public virtual void Add(T item)
		{
			innerRepository.Add(item);
			ClearObjectCache(item);
		}



		public virtual void Update(T @new, T old)
		{


			innerRepository.Update(@new, old);

			ClearObjectCache(@old);
		}

		public virtual void Remove(T item)
		{
			ClearObjectCache(item);
			innerRepository.Remove(item);
		}

	}
}
