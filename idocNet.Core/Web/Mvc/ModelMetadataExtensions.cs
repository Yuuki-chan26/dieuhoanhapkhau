﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace idocNet.Core.Web.Mvc
{
	public static class ModelMetadataExtensions
	{
		public static ISelectListDataSource GetDataSource(this ModelMetadata modelMetadata)
		{
			ISelectListDataSource dataSource = null;
			if (modelMetadata is NextPortalModelMetadata)
			{
				var dataSourceAttribute = ((NextPortalModelMetadata)modelMetadata).DataSourceAttribute;
				if (dataSourceAttribute != null)
				{
					dataSource = (ISelectListDataSource)Activator.CreateInstance(dataSourceAttribute.DataSourceType);
				}
				if (dataSource == null)
				{
					dataSource = ((NextPortalModelMetadata)modelMetadata).DataSource;
				}
			}
			else
			{
			}
			if (dataSource == null)
			{
				return new EmptySelectListDataSource();
			}
			else
			{
				return dataSource;
			}
		}
		public static Type GetDataSourceType(this ModelMetadata modelMetadata)
		{
			Type dataSourceType = null;
			if (modelMetadata is NextPortalModelMetadata)
			{
				var dataSourceAttribute = ((NextPortalModelMetadata)modelMetadata).DataSourceAttribute;
				dataSourceType = dataSourceAttribute.DataSourceType;
			}
			else
			{
			}
			if (dataSourceType == null)
			{
				dataSourceType = typeof(EmptySelectListDataSource);
			}

			return dataSourceType;
		}
	}
}
