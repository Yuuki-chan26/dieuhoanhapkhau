using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.IO;

namespace idocNet.Core.Configuration
{
	[DataContract(Name = "ActionParam")]
	[KnownTypeAttribute(typeof(ActionParam))]
	public class ActionParam
	{
		[DataMember(Order = 1)]
		public string Name { get; set; }

		[DataMember(Order = 2)]
		public object Value { get; set; }
	}

	public enum NavItemTargetTypes
	{
		CURRENT_WINDOW = 0,
		NEW_WINDOW = 1
	}

	[DataContract(Name = "NavLink")]
	[KnownTypeAttribute(typeof(NavLink))]
	public abstract class NavLink
	{
		public virtual string GetLinkType()
		{
			return "Link";
		}
	}

	[DataContract(Name = "NavPageLink")]
	[KnownTypeAttribute(typeof(NavPageLink))]
	public class NavPageLink : NavLink
	{
		[DataMember(Order = 1)]
		public string PageName { get; set; }


		public override string GetLinkType()
		{
			return "Trang trong";
		}
	}



	[DataContract(Name = "NavProductCategoryLink")]
	[KnownTypeAttribute(typeof(NavProductCategoryLink))]
	public class NavProductCategoryLink : NavLink
	{
		[DataMember(Order = 1)]
		public string CategoryName { get; set; }


		public override string GetLinkType()
		{
			return "Danh mục Sản phẩm";
		}

	}


	[DataContract(Name = "NavNewsCategoryLink")]
	[KnownTypeAttribute(typeof(NavNewsCategoryLink))]
	public class NavNewsCategoryLink : NavLink
	{
		[DataMember(Order = 1)]
		public string CategoryName { get; set; }


		public override string GetLinkType()
		{
			return "Danh mục Tin";
		}

	}


	[DataContract(Name = "NavHtmlContentCategoryLink")]
	[KnownTypeAttribute(typeof(NavHtmlContentCategoryLink))]
	public class NavHtmlContentCategoryLink : NavLink
	{
		[DataMember(Order = 1)]
		public string CategoryName { get; set; }


		public override string GetLinkType()
		{
			return "Danh mục trang tĩnh";
		}

	}



	[DataContract(Name = "NavHtmlContentLink")]
	[KnownTypeAttribute(typeof(NavHtmlContentLink))]
	public class NavHtmlContentLink : NavLink
	{
		[DataMember(Order = 1)]
		public string ContentName { get; set; }


		public override string GetLinkType()
		{
			return "Trang tĩnh";
		}

	}


	[DataContract(Name = "NavUrlLink")]
	[KnownTypeAttribute(typeof(NavUrlLink))]
	public class NavUrlLink : NavLink
	{
		[DataMember(Order = 1)]
		public string DirectUrl { get; set; }

		public override string GetLinkType()
		{
			return "Trang ngoài";
		}

	}


	[DataContract(Name = "NavItem")]
	[KnownTypeAttribute(typeof(NavItem))]
	public class NavItem
	{

		[DataMember(Order = 1)]
		public string Name { get; set; }

		[DataMember(Order = 2)]
		public string Title { get; set; }

		[DataMember(Order = 3)]
		public string Description { get; set; }

		[DataMember(Order = 4)]
		public string Icon { get; set; }

		[DataMember(Order = 5)]
		public NavLink Link { get; set; }


		public int orderNo = 0;

		[DataMember(Order = 6)]
		public int OrderNo
		{
			get
			{
				return this.orderNo;
			}
			set
			{
				this.orderNo = value;
			}
		}


		private bool published = true;

		[DataMember(Order = 7)]
		public bool Published
		{
			get
			{
				return this.published;
			}
			set
			{
				this.published = value;
			}
		}


		private NavItemTargetTypes targetType = NavItemTargetTypes.CURRENT_WINDOW;

		[DataMember(Order = 8)]
		public NavItemTargetTypes TargetType
		{
			get
			{
				return this.targetType;
			}
			set
			{
				this.targetType = value;
			}
		}

		public NavItem Parent { get; set; }

		[DataMember(Order = 9)]
		public List<NavItem> Childs { get; set; }


		private int hLevel = -1;

		public void SetHlevel(int val)
		{
			this.hLevel = val;
		}
		public int HLevel
		{
			get
			{
				if (hLevel > 0) return hLevel;
				if (Parent != null) return Parent.HLevel + 1;
				return 0;
			}
		}


		public string HlevelTitle
		{
			get
			{
				if (HLevel > 0)
				{
					string l = "";
					for (int i = 1; i <= HLevel; ++i)
					{
						l += "---";
					}
					return string.Format("{0}{1}", l, Title);

				}

				return Title;
			}
		}

		public NavItem Clone()
		{
			return new NavItem()
			{
				Name = this.Name,
				Title = this.Title,
				Parent = this.Parent,
				OrderNo = this.OrderNo,
				Published = this.Published,
				TargetType = this.TargetType,
				Link = this.Link,
				Childs = this.Childs,
				Description = this.Description,
				Icon = this.Icon

			};
		}
	}



	[DataContract]
	public class Nav
	{


		public string Name { get; set; }

		[DataMember(Order = 2)]
		public string Title { get; set; }

		[DataMember(Order = 3)]
		public string Description { get; set; }


		private List<NavItem> items;
		[DataMember(Order = 4)]
		public List<NavItem> Items
		{
			get
			{
				return items;
			}
			set
			{
				items = value;

				if (items != null)
				{
					foreach (var c in items)
						SetParent(c);
				}
			}
		}



		List<NavItem> flatItems = null;
		public List<NavItem> FlatItems
		{
			get
			{
				if (flatItems == null)
				{
					flatItems = GetFlatItem(Items).ToList();
				}
				return flatItems;
			}
		}


		public void RefreshFlatItems()
		{
			this.flatItems = null;
		}

		private void SetParent(NavItem item)
		{
			if (item.Childs != null)
				foreach (var c in item.Childs)
				{
					c.Parent = item;
					SetParent(c);
				}
		}

		private IEnumerable<NavItem> GetFlatItem(List<NavItem> list)
		{
			if (list != null)
				foreach (var item in list)
				{
					yield return item;

					if (item.Childs != null)
					{

						var childList = GetFlatItem(item.Childs);

						foreach (var c in childList)
						{

							yield return c;
						}
					}
				}


		}


		public static Nav InitTest()
		{
			Nav nav = new Nav();
			nav.Name = "mainmenu";
			nav.Title = "Menu chính";
			nav.Description = "menu chính";
			nav.Items = new List<NavItem>();


			nav.Items.Add(new NavItem()
			{
				Name = "company",
				Description = "Công ty",
				Title = "Công ty",
				Published = true,
				OrderNo = 0,
				Link = new NavPageLink() { PageName = "Company" }

			});


			nav.Items.Add(new NavItem()
			{
				Name = "company",
				Description = "Đại lý",
				Title = "Đại lý",
				Published = true,
				OrderNo = 0,
				Link = new NavPageLink() { PageName = "Company" }

			});


			nav.Items.Add(new NavItem()
			{
				Name = "news",
				Description = "Tin tức",
				Title = "Tin tức - Khuyến mại",
				Published = true,
				OrderNo = 0,
				Link = new NavPageLink() { PageName = "News" }

			});


			nav.Items.Add(new NavItem()
			{
				Name = "service",
				Description = "Dịch vụ",
				Title = "Dịch vụ",
				Published = true,
				OrderNo = 0,
				Link = new NavPageLink() { PageName = "Service" }

			});






			nav.Items[0].Childs = new List<NavItem>();
			nav.Items[0].Childs.Add(new NavItem()
			{
				Name = "gioi-thieu-chung1",
				Title = "Giới thiệu chung",
				Published = true,
				OrderNo = 0,
				Link = new NavHtmlContentLink() { ContentName = "gioi-thieu-chung1" }

			});


			nav.Items[0].Childs.Add(new NavItem()
			{
				Name = "thu-ngo",
				Title = "Thư ngỏ",
				Published = true,
				OrderNo = 1,
				Link = new NavHtmlContentLink() { ContentName = "thu-ngo" }

			});




			nav.Items[0].Childs.Add(new NavItem()
			{
				Name = "co-cau-to-chuc",
				Title = "Cơ cấu tổ chức",
				Published = true,
				OrderNo = 1,
				Link = new NavHtmlContentLink() { ContentName = "co-cau-to-chuc" }

			});




			nav.Items[1].Childs = new List<NavItem>();
			nav.Items[1].Childs.Add(new NavItem()
			{
				Name = "dai-ly-bac",
				Title = "Bắc",
				Published = true,
				OrderNo = 0,
				Link = new NavHtmlContentLink() { ContentName = "dai-ly-bac" }

			});



			nav.Items[1].Childs.Add(new NavItem()
			{
				Name = "dai-ly-trung",
				Title = "Trung",
				Published = true,
				OrderNo = 0,
				Link = new NavHtmlContentLink() { ContentName = "dai-ly-trung" }

			});


			nav.Items[1].Childs.Add(new NavItem()
			{
				Name = "dai-ly-nam",
				Title = "Nam",
				Published = true,
				OrderNo = 0,
				Link = new NavHtmlContentLink() { ContentName = "dai-ly-nam" }

			});






			nav.Items[2].Childs = new List<NavItem>();
			nav.Items[2].Childs.Add(new NavItem()
			{
				Name = "khuyen-mai",
				Title = "Khuyến mại",
				Published = true,
				OrderNo = 0,
				Link = new NavNewsCategoryLink() { CategoryName = "khuyen-mai" }

			});

			nav.Items[2].Childs.Add(new NavItem()
			{

				Name = "tin-tuc-chung",
				Title = "Tin tức chung",
				Published = true,
				OrderNo = 0,
				Link = new NavNewsCategoryLink() { CategoryName = "tin-tuc-chung" }

			});


			nav.Items[2].Childs.Add(new NavItem()
			{
				Name = "tin-cong-ty",
				Title = "Tin công ty",
				Published = true,
				OrderNo = 0,
				Link = new NavNewsCategoryLink() { CategoryName = "tin-cong-ty" }

			});


			nav.Items[2].Childs.Add(new NavItem()
			{
				Name = "tin-tuyen-dung",
				Title = "Tin tuyển dụng",
				Published = true,
				OrderNo = 0,
				Link = new NavNewsCategoryLink() { CategoryName = "tin-tuyen-dung" }

			});



			nav.Items[3].Childs = new List<NavItem>();
			nav.Items[3].Childs.Add(new NavItem()
			{
				Name = "mang-luoi-dich-vu",
				Title = "Mạng lưới dịch vụ",
				Published = true,
				OrderNo = 0,
				Link = new NavHtmlContentLink() { ContentName = "mang-luoi-dich-vu" }

			});

			nav.Items[3].Childs.Add(new NavItem()
			{
				Name = "dich-vu-sau-ban-hang",
				Title = "Dịch vụ sau bán hàng",
				Published = true,
				OrderNo = 0,
				Link = new NavHtmlContentLink() { ContentName = "dich-vu-sau-ban-hang" }

			});


			nav.Items[3].Childs.Add(new NavItem()
			{
				Name = "cham-soc-khach-hang",
				Title = "Chăm sóc khách hàng",
				Published = true,
				OrderNo = 0,
				Link = new NavHtmlContentLink() { ContentName = "cham-soc-khach-hang" }

			});


			return nav;
		}


		public static void SortItems(List<NavItem> list)
		{
			if (list == null || list.Count == 0) return;
			for (int i = 0; i < list.Count - 1; ++i)
			{
				for (int j = i + 1; j < list.Count; ++j)
					//swap  
					if (list[i].OrderNo > list[j].OrderNo)
					{

						var tmp = list[i];
						list[i] = list[j];
						list[j] = tmp;
					}
			}

			foreach (var item in list)
				SortItems(item.Childs);

		}


	}
}
