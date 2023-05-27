using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class MenuDAL : PrpDBConnect
	{
		public MenuDAL()
		{
		}

		public Message AddUpdate(Menu obj)
		{
			Message message = new Message();
			try
			{
				tblMenu _tblMenu = new tblMenu();
				if (obj.menuId != 0)
				{
					_tblMenu = this.db.tblMenus.FirstOrDefault<tblMenu>((tblMenu x) => x.menuId == obj.menuId);
					_tblMenu.menuId = obj.menuId;
					_tblMenu.name = obj.name;
					_tblMenu.nameDisplay = obj.nameDisplay;
					_tblMenu.url = obj.url;
					_tblMenu.iconClass = obj.iconClass;
					_tblMenu.sortOrder = obj.sortOrder;
					_tblMenu.isMenu = obj.isMenu;
					_tblMenu.typeId = obj.typeId;
					_tblMenu.appId = obj.appId;
					_tblMenu.isActive = obj.isActive;
					_tblMenu.parentId = obj.parentId;
					_tblMenu.dated = obj.dated;
					_tblMenu.adminId = obj.adminId;
				}
				else
				{
					_tblMenu = MapMenu.ToTable(obj);
					this.db.tblMenus.Add(_tblMenu);
				}
				this.db.SaveChanges();
				message.id = _tblMenu.menuId;
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public Message AddUpdateUserAccess(int userId, string menuIds)
		{
			Message message = new Message();
			try
			{
				this.db.spMenuAccessAddUpdate(new int?(userId), menuIds);
				message.status = true;
				message.msg = "Data saved";
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.status = false;
			}
			return message;
		}

		public Message AddUpdateUserTypeAccess(int typeId, string menuIds)
		{
			Message message = new Message();
			try
			{
				this.db.spMenuUserTypeAddUpdate(new int?(typeId), menuIds);
				message.status = true;
				message.msg = "Data saved";
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.status = false;
			}
			return message;
		}

		public bool CheckPageHasRight(int appId, int userId, string url)
		{
			bool flag = false;
			try
			{
				List<spMenuGetRightByUrl_Result> list = this.db.spMenuGetRightByUrl(url, new int?(userId), new int?(appId)).ToList<spMenuGetRightByUrl_Result>();
				if ((list == null ? false : list.Count > 0))
				{
					flag = true;
				}
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public List<Menu> GetAll()
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				List<tvwMenu> list = this.db.tvwMenus.ToList<tvwMenu>();
				menus = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
				menus = new List<Menu>();
			}
			return menus;
		}

		public List<Menu> GetByApp(int appId)
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				List<tvwMenu> list = (
					from x in this.db.tvwMenus
					where x.appId == appId
					select x).ToList<tvwMenu>();
				menus = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return menus;
		}

		public Menu GetById(int menuId)
		{
			Menu menu = new Menu();
			try
			{
				tvwMenu _tvwMenu = this.db.tvwMenus.FirstOrDefault<tvwMenu>((tvwMenu x) => x.menuId == menuId);
				menu = MapMenu.ToEntity(_tvwMenu);
			}
			catch (Exception exception)
			{
				menu = new Menu();
			}
			return menu;
		}

		public List<Menu> GetByType(int typeId)
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				List<tvwMenu> list = (
					from x in this.db.tvwMenus
					where x.typeId == typeId
					select x).ToList<tvwMenu>();
				menus = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return menus;
		}

		public List<Menu> GetByUser(int userId, int appId)
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				List<spMenuGetByUserId_Result> list = this.db.spMenuGetByUserId(new int?(userId), new int?(appId)).ToList<spMenuGetByUserId_Result>();
				menus = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return menus;
		}

		public List<EntityDDL> GetMenuForDDL(DDLMenu obj)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spMenuForDDL_Result> list = this.db.spMenuForDDL(new int?(obj.typeId), new int?(obj.parentId), new int?(obj.reffId), new int?(obj.userId), obj.reffIds, obj.condition).ToList<spMenuForDDL_Result>();
				entityDDLs = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}

		public List<Menu> GetMenuListForUserId(int userId)
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				List<spMenuGetAllForUser_Result> list = this.db.spMenuGetAllForUser(new int?(userId)).ToList<spMenuGetAllForUser_Result>();
				menus = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return menus;
		}

		public List<Menu> GetMenuListForUserType(int userType)
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				List<spMenuGetAllForUserType_Result> list = this.db.spMenuGetAllForUserType(new int?(userType)).ToList<spMenuGetAllForUserType_Result>();
				menus = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return menus;
		}

		public List<Menu> GetParent(int appId)
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				List<tvwMenu> list = (
					from x in this.db.tvwMenus
					where x.appId == appId && x.parentId == 0
					orderby x.sortOrder
					select x).ToList<tvwMenu>();
				menus = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return menus;
		}

		public List<Menu> Search(int appId, int parentId)
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				List<spMenuSearch_Result> list = this.db.spMenuSearch(new int?(appId), new int?(parentId)).ToList<spMenuSearch_Result>();
				menus = MapMenu.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return menus;
		}
	}
}