using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Data.Entities.Validation
{
	public enum ValidationErrorType
	{
		NullOrEmpty,
		Format,
		Range,
		MaxLength,
		MinLength,
		CompareNotMatch,
		DuplicateNotAllowed,
		FileFormat,
		AccessRights
	}
}
