using idocNet.Core.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Data
{
	public static class SQLDataManagerExtensions
	{

		public static IDataProvider<T> GetTransactionedProvider<T>(this DataManagerBase<T> dm, string transactionId)
			where T : idocNet.Core.Data.Entities.EntityBase
		{

			if (string.IsNullOrEmpty(transactionId)) return dm.Provider;
			var type = dm.Provider.GetType();

			if (!(dm.Provider is DataAccessBase)) throw new Exception(string.Format("{0} is not Type of DataAccessBase", type));


			var instance = (DataAccessBase)Activator.CreateInstance(type);

			instance.TransactionId = transactionId;

			return instance as IDataProvider<T>;

		}

		public static DataManagerBase<T> ToTransactionedManager<T>(this DataManagerBase<T> dm, string transactionId)
			where T : idocNet.Core.Data.Entities.EntityBase
		{

			if (string.IsNullOrEmpty(transactionId)) return dm;
			var type = dm.Provider.GetType();

			if (!(dm.Provider is DataAccessBase)) throw new Exception(string.Format("{0} is not Type of DataAccessBase", type));

			var provider = (DataAccessBase)Activator.CreateInstance(type);

			provider.TransactionId = transactionId;


			var manager = (DataManagerBase<T>)Activator.CreateInstance(dm.GetType(), provider );

			//manager.Provider = (provider as IDataProvider<T>);



			return manager;

		}
	}
}
