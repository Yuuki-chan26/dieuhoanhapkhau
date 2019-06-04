using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using idocNet.Core.Configuration;
using System.Data.SqlClient;

namespace idocNet.Core.Data.DataAccess
{
	public class DataAccessBase
	{

		static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();

		protected System.Threading.ReaderWriterLockSlim GetLocker()
		{
			return locker;
		}

		protected static Dictionary<string, DbConnection> TranConnnections;
		protected static Dictionary<string, DbTransaction> TranTransactions;

		public string TransactionId
		{
			get;
			set;
		}

		protected DbConnection currentConnection;
		protected DbTransaction currentTransaction;

		/// <summary>
		/// Current configuration
		/// </summary>
		protected virtual SiteConfiguration Config
		{
			get
			{
				return SiteConfiguration.Current;
			}
		}

		public DataAccessBase()
		{
			this.Factory = DbProviderFactories.GetFactory(Config.DataAccess.ConnectionString.ProviderName);
		}

		/// <summary>
		/// Gets a new connection.
		/// </summary>
		/// <returns></returns>
		public virtual DbConnection GetConnection()
		{
			if (!string.IsNullOrEmpty(TransactionId))
			{
				if (TranConnnections != null && TranConnnections.ContainsKey(TransactionId))
				{
					return TranConnnections[TransactionId];
				}

				else
				{
				}
			}
			DbConnection conn = this.Factory.CreateConnection();
			conn.ConnectionString = Config.DataAccess.ConnectionString.ConnectionString;
			if (!string.IsNullOrEmpty(TransactionId))
			{
				if (TranConnnections == null) TranConnnections = new Dictionary<string, DbConnection>();


				TranConnnections[TransactionId] = conn;
			}

			return conn;
		}

		public DbTransaction GetTransaction()
		{
			if (!string.IsNullOrEmpty(TransactionId) && TranTransactions != null && TranTransactions.ContainsKey(TransactionId))
			{
				return TranTransactions[TransactionId];

			}

			return null;
		}

		/// <summary>
		/// The database provider factory to create the connections and commands to access the db.
		/// </summary>
		protected DbProviderFactory Factory
		{
			get;
			set;
		}

		/// <summary>
		/// Gets a new command for procedure executing.
		/// </summary>
		/// <param name="procedureName"></param>
		/// <returns></returns>
		protected DbCommand GetCommand(string procedureName)
		{
			DbCommand comm = this.Factory.CreateCommand();
			comm.Connection = GetConnection();
			comm.CommandText = procedureName;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Transaction = GetTransaction();
			return comm;
		}

		/// <summary>
		/// Gets a new command for sql querry string executing.
		/// </summary>
		/// <param name="procedureName"></param>
		/// <returns></returns>
		protected DbCommand GetCommandSQL(string sql)
		{
			DbCommand comm = this.Factory.CreateCommand();
			comm.Connection = GetConnection();
			comm.CommandText = sql;
			comm.CommandType = CommandType.Text;

			comm.Transaction = GetTransaction();
			return comm;
		}

		/// <summary>
		/// Gets a datatable filled with the results of executing the command.
		/// </summary>
		protected DataTable GetTable(DbCommand command)
		{
			var dt = new DataTable();
			var da = this.Factory.CreateDataAdapter();
			da.SelectCommand = command;
			da.Fill(dt);

			return dt;
		}

		/// <summary>
		/// Gets a datatable filled with the first result of executing the command.
		/// </summary>
		protected DataRow GetFirstRow(DbCommand command)
		{
			DataRow dr = null;
			DataTable dt = GetTable(command);
			if (dt.Rows.Count > 0)
			{
				dr = dt.Rows[0];
			}
			return dr;
		}

		/// <summary>
		/// Disposes the reader.
		/// </summary>
		/// <param name="reader"></param>
		protected void SafeDispose(DbDataReader reader)
		{
			if (reader != null)
			{
				reader.Dispose();
			}
		}

		/// <summary>
		/// Safely opens the connection, executes and closes the connection
		/// </summary>
		/// <param name="comm"></param>
		/// <returns></returns>
		protected int SafeExecuteNonQuery(DbCommand comm)
		{

			var tran = GetTransaction();

			if (tran != null) return comm.ExecuteNonQuery();

			return comm.SafeExecuteNonQuery();
		}

		public void BeginTransaction()
		{

			if (string.IsNullOrEmpty(TransactionId)) throw new Exception("Invalid transctionId");

			GetLocker().EnterWriteLock();
			
			var conn = GetConnection();
			conn.Open();

			if (TranTransactions == null) TranTransactions = new Dictionary<string, DbTransaction>();
			TranTransactions[TransactionId] = conn.BeginTransaction();


		}

		public void RollBackTransaction()
		{
			if (string.IsNullOrEmpty(TransactionId)) throw new Exception("Invalid transctionId");
			GetLocker().ExitWriteLock();
			TranTransactions[TransactionId].Rollback();
			TranTransactions.Remove(TransactionId);
			TranConnnections[TransactionId].Close();
			TranConnnections.Remove(TransactionId);
		}

		public void CommitTransaction()
		{
			if (string.IsNullOrEmpty(TransactionId)) throw new Exception("Invalid transctionId");

			GetLocker().ExitWriteLock();
			TranTransactions[TransactionId].Commit();
			TranTransactions.Remove(TransactionId);
			TranConnnections[TransactionId].Close();
			TranConnnections.Remove(TransactionId);
		}

	}
}
