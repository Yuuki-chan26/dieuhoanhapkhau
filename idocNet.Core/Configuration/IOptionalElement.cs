using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Configuration
{
	/// <summary>
	/// Defines an optional ConfigurationElement, that will be always instanciated but not always defined by the user.
	/// </summary>
	public interface IOptionalElement
	{
		bool IsDefined
		{
			get;
		}
	}
}
