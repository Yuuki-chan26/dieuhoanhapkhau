using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idocNet.Core.Data.Entities.Paging
{
	public class PageInfo<T>
		where T : Entities.EntityBase
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }


		private int pageCount;
		public int PageCount
		{
			get
			{
				return pageCount;
			}
			set
			{
				this.pageCount = value;

			}
		}

		private int itemCount;
		public int ItemCount
		{
			get
			{
				return itemCount;
			}
			set
			{
				this.itemCount = value;

				this.pageCount = value / this.PageSize + 1;
			}
		}


		public PageInfo(int index, int pageSize)
		{
			this.PageIndex = index;
			this.PageSize = pageSize;
		}

		public List<T> DataList { get; set; }
	}
}
