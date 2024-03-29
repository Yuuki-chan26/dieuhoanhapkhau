﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace idocNet.Core.Configuration
{
	public class DataAccessElement : ConfigurationElement
	{
		protected override void PostDeserialize()
		{
		}

		public DataAccessElement()
		{
			#region Default ParameterPrefix
			switch (this.ConnectionString.ProviderName.ToUpper())
			{
				case "SYSTEM.DATA.SQLCLIENT":
					this.ParameterPrefix = "@";
					break;
				case "MYSQL.DATA.MYSQLCLIENT":
					this.ParameterPrefix = "param_";
					break;
			}
			#endregion
		}

		[ConfigurationProperty("parameterPrefix", IsRequired = false)]
		public string ParameterPrefix
		{
			get
			{
				return (string)this["parameterPrefix"];
			}
			set
			{
				this["parameterPrefix"] = value;
			}
		}

		private ConnectionStringSettings _connectionString;
		public ConnectionStringSettings ConnectionString
		{
			get
			{
				if (_connectionString == null)
				{
					//Key used in connectionStrings dictionary
					const string _connectionKey = "DefaultConnectionString";
					//Default provider
					var providerName = "System.Data.SqlClient";
					ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings[_connectionKey];

					if (conn == null)
					{
						throw new ConfigurationErrorsException("You must specify a SQL Connection string in the configuration file, with the key " + _connectionKey + ".");
					}

					if (!String.IsNullOrEmpty(conn.ProviderName))
					{
						providerName = conn.ProviderName;
					}
					_connectionString = new ConnectionStringSettings(_connectionKey, conn.ConnectionString, providerName);
				}
				return _connectionString;
			}
		}
	}
}
