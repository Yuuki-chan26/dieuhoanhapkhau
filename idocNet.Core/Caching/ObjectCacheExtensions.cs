#region usings
using System;
using System.Runtime.Caching;

#endregion

namespace idocNet.Core.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectCacheExtensions
    {

        #region Properties
        public static System.Runtime.Caching.CacheItemPolicy DefaultCacheItemPolicy = new System.Runtime.Caching.CacheItemPolicy()
      {
          SlidingExpiration = TimeSpan.Parse("00:30:00"),
          RemovedCallback = (arguments) => { if (arguments.RemovedReason == CacheEntryRemovedReason.Removed) { CacheExpiredNotification.Notify(arguments.Source.Name, arguments.CacheItem.Key); } }
      };
        #endregion

        #region Methods
        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectCache">The object cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="dataFunc">The data func.</param>
        /// <returns></returns>
        public static T GetCache<T>(this ObjectCache objectCache, string cacheKey, Func<T> dataFunc)
            where T : class
        {
            T data = default(T);
            var cached = objectCache.Get(cacheKey);
            if (!(cached is CachedNullValue))
            {
                data = cached as T;
                if (data == null)
                {
                    data = dataFunc();
                    if (data == null)
                    {
                        objectCache.Add(cacheKey, CachedNullValue.Value, DefaultCacheItemPolicy);
                    }
                    else
                    {
                        objectCache.Add(cacheKey, data, DefaultCacheItemPolicy);
                    }
                }

            }
            return data;
        }

        /// <summary>
        /// Sets the expired.
        /// </summary>
        /// <param name="objectCache">The object cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        public static void SetExpired(this ObjectCache objectCache, string cacheKey)
        {
            var cacheItem = objectCache.GetCacheItem(cacheKey);
            if (cacheItem != null)
            {
                objectCache.Set(cacheItem, new System.Runtime.Caching.CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.Now) });
            }
        } 
        #endregion
    }
}
