using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace idocNet.Core.Caching
{
    public class CacheUtils<T>
    {

        private static bool enabled = true;


        /// <summary>
        /// thoi gian cache mac dinh (30')
        /// </summary>
        public const int CACHE_DEFAULT_TIME = 30;


        private static object tobj = new object();


        /// <summary>
        /// set cache voi mot key xac dinh va thoi gian 
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <param name="obj">object can cahe</param>
        /// <param name="time">cach trong bao nhieu phu't</param>
        public static void SetCache(string cacheKey, T obj, int time)
        {
            lock (tobj)
            {
                if (obj == null) return;
                HttpRuntime.Cache.Remove(cacheKey);
                HttpRuntime.Cache.Insert(cacheKey, obj, null, DateTime.Now.AddMinutes(time), TimeSpan.Zero);
            }
        }


        /// <summary>
        /// set cache voi thoi gian mac dinh
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="obj"></param>
        public static void SetCache(string cacheKey, T obj)
        {
            SetCache(cacheKey, obj, CACHE_DEFAULT_TIME);
        }


        /// <summary>
        /// set cache voi key va group
        /// </summary>
        /// <param name="keys">list cac object tham gia vao cahekey </param>
        /// <param name="obj"> object can cache</param>
        public static void SetCacheByKeys(T obj, int time, params object[] keys)
        {
            if (!enabled) return;
            if (obj == null) return;
            string key = ConstructCacheKey(keys);

            if (!string.IsNullOrEmpty(key)) SetCache(key, obj, time);
        }


        /// <summary>
        /// get cache voi key va groups
        /// </summary>
        /// <param name="keys">list cac object tham gia vao cahekey </param>
        /// <returns></returns>
        public static T GetCacheByKeys(params object[] keys)
        {
            if (!enabled) return default(T);
            string ckey = ConstructCacheKey(keys);
            return GetCache(ckey);

        }

        /// <summary>
        /// get cache voi key xac dinh
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static T GetCache(string cacheKey)
        {
            if (!enabled) return default(T);
            lock (tobj)
            {
                object obj = HttpRuntime.Cache[cacheKey];

                return (obj != null && obj is T) ? (T)obj : default(T);
            }
        }


        /// <summary>
        /// xoa cache co key xac dinh
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void RemoveCache(string cacheKey)
        {
            if (!enabled) return;

            lock (tobj)
            {
                HttpRuntime.Cache.Remove(cacheKey);
            }
        }

        /// <summary>
        /// remove all cache
        /// </summary>
        public static void RemoveAll()
        {
            lock (tobj)
            {
                IDictionaryEnumerator ce = HttpRuntime.Cache.GetEnumerator();
                while (ce.MoveNext()) HttpRuntime.Cache.Remove(ce.Key as string);
            }
        }
        /// <summary>
        /// xoa cache neu key chua mot trong cac object xac dinh
        /// </summary>
        /// <param name="oKeys">list cac object tham gia vao cachekey </param>

        public static void RemoveCacheByKeys_OR(params object[] oKeys)
        {
            if (!enabled) return;

            if (oKeys == null || oKeys.Length <= 0) return;

            string[] keys = GetCacheItemStrings(oKeys);

            lock (tobj)
            {
                IDictionaryEnumerator ce = HttpRuntime.Cache.GetEnumerator();

                while (ce.MoveNext())
                {

                    string key = ce.Key as string;
                    foreach (string k in keys)
                    {
                        if (!string.IsNullOrEmpty(k))
                            if (key.Contains(k))
                            {
                                HttpRuntime.Cache.Remove(key);
                                break;
                            }
                    }


                }
            }
        }



        /// <summary>
        /// xoa cache neu key chua list cac object xac dinh
        /// </summary>
        /// <param name="oKeys">list cac object tham gia vao cachekey </param>

        public static void RemoveCacheByKeys(params object[] oKeys)
        {
            if (!enabled) return;

            if (oKeys == null || oKeys.Length <= 0) return;

            string[] keys = GetCacheItemStrings(oKeys);

            lock (tobj)
            {
                IDictionaryEnumerator ce = HttpRuntime.Cache.GetEnumerator();

                while (ce.MoveNext())
                {

                    string key = ce.Key as string;
                    bool ok = true;
                    foreach (string k in keys)
                    {
                        if (!string.IsNullOrEmpty(k))
                            if (!key.Contains(k))
                            {
                                ok = false;
                                break;
                            }
                    }

                    if (ok)
                    {
                        HttpRuntime.Cache.Remove(key);
                    }
                }
            }
        }


        /// <summary>
        /// xoa cache neu trong cache key chua cac xau kt nao do
        /// </summary>
        /// <param name="keys"></param>

        public static void RemoveCacheIfKeyContains(params string[] keys)
        {
            if (!enabled) return;

            if (keys == null || keys.Length == 0) return;


            lock (tobj)
            {
                IDictionaryEnumerator ce = HttpRuntime.Cache.GetEnumerator();

                while (ce.MoveNext())
                {

                    string key = ce.Key as string;
                    bool ok = true;
                    foreach (string k in keys)
                    {
                        if (!string.IsNullOrEmpty(k))
                            if (!key.Contains(k))
                            {
                                ok = false;
                                break;
                            }
                    }

                    if (ok) HttpRuntime.Cache.Remove(key);
                }
            }
        }



        /// <summary>
        /// xoa cache neu trong cache key chua mot trong cac xau kt nao do
        /// 
        /// </summary>
        /// <param name="keys"></param>

        public static void RemoveCacheIfKeyContains_OR(params string[] keys)
        {
            if (!enabled) return;

            if (keys == null || keys.Length == 0) return;


            lock (tobj)
            {
                IDictionaryEnumerator ce = HttpRuntime.Cache.GetEnumerator();

                while (ce.MoveNext())
                {

                    string key = ce.Key as string;
                    foreach (string k in keys)
                    {
                        if (!string.IsNullOrEmpty(k))
                            if (key.Contains(k))
                            {
                                HttpRuntime.Cache.Remove(key);
                                break;
                            }
                    }

                }
            }
        }





        private static string[] GetCacheItemStrings(object[] arr)
        {
            if (arr == null || arr.Length == 0) return null;

            string[] keys = new string[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {

                object ob = arr[i];
                if (ob != null)
                {
                    if (ob is CacheGroups)
                    {
                        keys[i] = "[" + ob.ToString().ToLower() + "]";
                    }
                    else keys[i] = "(" + ob.ToString().ToLower() + ")";
                }
            }

            return keys;

        }

        public static string ConstructCacheKey(params object[] objs)
        {
            string[] keys = GetCacheItemStrings(objs);

            if (keys == null) return string.Empty;


            //sap xep lai keys
            for (int i = 0; i < keys.Length; ++i)
            {
                for (int j = i; j < keys.Length; ++j)
                {
                    if (keys[i] != null)
                        if (keys[i].CompareTo(keys[j]) > 0)
                        {
                            string tmp = keys[i];
                            keys[i] = keys[j];
                            keys[j] = tmp;

                        }
                }
            }


            StringBuilder sb = new StringBuilder();
            foreach (string ob in keys)
            {
                sb.Append(ob);
            }
            return sb.ToString();
        }


        public static void SetPersistanceCacheByKeys(T obj, DateTime toDateCache, int time, params object[] keys)
        {
            if (!enabled) return;
            if (obj == null) return;
            string key = ConstructCacheKey(keys);

            double cacheTime = (toDateCache - DateTime.Now).TotalMinutes;

            if (cacheTime < (double)time) cacheTime = (double)time;
            if (cacheTime > 100.0) cacheTime = 100.0;

            time = (int)Math.Truncate(cacheTime);


            if (!string.IsNullOrEmpty(key)) SetCache(key, obj, time);
        }
    }



    public enum CacheGroups
    {
        ALL = 0,
        USERS = 1,
        TOPICS = 2,
        USERDETAIL = 3,
    }
}
