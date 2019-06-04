using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Data.Entities
{
	public class DataColumAttribute : Attribute
	{

		public string Name { get; set; }
		public DataColumAttribute(string name)
		{
			Name = name;
		}
		public DataColumAttribute()
		{
		}
	}

	public class DataColumExAttribute : Attribute
	{

		public string Name { get; set; }
		public DataColumExAttribute(string name)
		{
			Name = name;
		}

		public DataColumExAttribute()
		{
		}
	}


	public class DataExAttribute : Attribute
	{
		public DataExAttribute()
		{
		}
	}


	public class UniqueAttribute : Attribute
	{
	}


}
