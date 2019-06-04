#region License
// 
// Copyright (c) 2013, NextPortal team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
#region usings
using System.Collections.Generic;
using System.Runtime.Caching;

#endregion

namespace idocNet.Core.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryCacheManager : CacheManager
    {
        #region Fields
        static IDictionary<string, MemoryCache> objectCaches = new Dictionary<string, MemoryCache>();
        #endregion

        #region Methods
        public override ObjectCache GetObjectCache(string name)
        {
            if (!objectCaches.ContainsKey(name))
            {
                lock (objectCaches)
                {
                    if (!objectCaches.ContainsKey(name))
                    {
                        MemoryCache memoryCache = new MemoryCache(name);
                        objectCaches.Add(name, memoryCache);
                    }
                }
            }
            return objectCaches[name];
        }

        protected override void RemoveObjectCache(string name)
        {
            if (objectCaches.ContainsKey(name))
            {
                lock (objectCaches)
                {
                    if (objectCaches.ContainsKey(name))
                    {

                        MemoryCache memoryCache = objectCaches[name];
                        objectCaches.Remove(name);

                        memoryCache.Dispose();
                    }
                }
            }

        } 
        #endregion
    }
}
