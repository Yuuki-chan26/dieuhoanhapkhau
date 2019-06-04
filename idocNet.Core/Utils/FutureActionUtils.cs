using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idocNet.Core.Utils
{
    public static class FutureActionUtils
    {
        private static Dictionary<string, long> _keylist;

        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();

        public static void CallFutureAction(Action action, int timeOut, string key = "")
        {

            var kv = DateTime.Now.AddMilliseconds(timeOut).Ticks;
            if (!string.IsNullOrEmpty(key))
            {
                locker.EnterReadLock();
                try
                {
                    if (_keylist == null)
                    {
                        _keylist = new Dictionary<string, long>();

                    }


                    if (_keylist.ContainsKey(key))
                    {
                        var okv = _keylist[key];
                        if (okv >= kv)
                        {
                            kv = okv + 1;
                        }

                    }
                    _keylist[key] = kv;
                }

                finally
                {
                    locker.ExitReadLock();
                }
            }


            Task.Factory.StartNew(
                    () =>
                    {
                        System.Threading.Thread.Sleep(timeOut);

                        if (!string.IsNullOrEmpty(key))
                        {
                            locker.EnterReadLock();
                            try
                            {
                                if (_keylist.ContainsKey(key))
                                {

                                    if (_keylist[key] <= kv)
                                    {
                                        _keylist.Remove(key);
                                        action.Invoke();
                                    }
                                }
                            }

                            finally
                            {

                                locker.ExitReadLock();
                            }


                        }
                        else
                            action.Invoke();
                    });
        }
    }
}
