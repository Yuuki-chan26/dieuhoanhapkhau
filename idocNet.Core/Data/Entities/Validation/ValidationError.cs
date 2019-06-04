using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Data.Entities.Validation
{
	public class ValidationError : Exception
	{
		public string ErrorMessage
		{
			get;
			set;
		}

		public ValidationError()
		{
			
		}

		public ValidationError(string fieldName, ValidationErrorType type, string errorMessage)
		{
			this.FieldName = fieldName;
			this.Type = type;

			this.ErrorMessage = !string.IsNullOrEmpty(errorMessage)?errorMessage: string.Format("{0}: {1}", fieldName, type);
		}

		public string FieldName
		{
			get;
			set;
		}

		public ValidationErrorType Type
		{
			get;
			set;
		}
	}
}
